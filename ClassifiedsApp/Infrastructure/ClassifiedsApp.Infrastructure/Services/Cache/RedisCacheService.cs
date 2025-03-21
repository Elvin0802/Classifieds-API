using ClassifiedsApp.Application.Dtos.Cache;
using ClassifiedsApp.Application.Interfaces.Services.Cache;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace ClassifiedsApp.Infrastructure.Services.Cache
{
	public class RedisCacheService : ICacheService
	{
		readonly IDistributedCache _distributedCache;
		readonly ILogger<RedisCacheService> _logger;
		readonly CacheConfigDto _cacheConfig;
		readonly IHttpContextAccessor _contextAccessor;
		readonly IConnectionMultiplexer _connectionMultiplexer;

		public RedisCacheService(IDistributedCache distributedCache,
									ILogger<RedisCacheService> logger,
									CacheConfigDto cacheConfig,
									IHttpContextAccessor contextAccessor,
									IConnectionMultiplexer connectionMultiplexer)
		{
			_distributedCache = distributedCache;
			_logger = logger;
			_cacheConfig = cacheConfig;
			_contextAccessor = contextAccessor;
			_connectionMultiplexer = connectionMultiplexer;
		}

		public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null)
		{
			var cachedValue = await GetAsync<T>(key);
			if (cachedValue != null)
			{
				_logger.LogError("Cache hit for key: {Key}", key);
				return cachedValue;
			}

			_logger.LogError("Cache miss for key: {Key}", key);
			var result = await factory();
			await SetAsync(key, result, expiration);
			return result;
		}

		public async Task<T> GetAsync<T>(string key)
		{
			key = BuildUserSpecificKey(key);

			try
			{
				var cachedBytes = await _distributedCache.GetAsync(key);
				if (cachedBytes == null || cachedBytes.Length == 0)
					return default;

				var cachedValue = JsonSerializer.Deserialize<T>(cachedBytes);
				return cachedValue;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error retrieving cached value for key: {Key}", key);
				return default;
			}
		}

		public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
		{
			key = BuildUserSpecificKey(key);

			try
			{
				var options = new DistributedCacheEntryOptions
				{
					AbsoluteExpirationRelativeToNow = expiration ?? _cacheConfig.DefaultExpiration
				};

				var serializedValue = JsonSerializer.SerializeToUtf8Bytes(value);
				await _distributedCache.SetAsync(key, serializedValue, options);
				_logger.LogError("Cache set for key: {Key}", key);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error setting cache value for key: {Key}", key);
			}
		}

		public async Task RemoveAsync(string key)
		{
			key = BuildUserSpecificKey(key);

			try
			{
				await _distributedCache.RemoveAsync(key);
				_logger.LogDebug("Cache removed for key: {Key}", key);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error removing cache for key: {Key}", key);
			}
		}

		public async Task RemoveByPrefixAsync(string prefix)
		{
			// helelik remove da user id ya ehtiyac yoxdu. sonra elave et.
			//prefix = BuildUserSpecificKey(prefix);

			try
			{
				// Use StackExchange.Redis directly to scan for keys with the given prefix
				var database = _connectionMultiplexer.GetDatabase();
				var server = GetServer();

				if (server is null)
				{
					_logger.LogError("Unable to get Redis server instance for key pattern scan");
					return;
				}

				// Use SCAN command to find all keys matching the pattern
				var keysToDelete = new List<RedisKey>();
				var pattern = $"{_cacheConfig.InstanceName}{prefix}*";

				_logger.LogError("Scanning for keys with pattern: {Pattern}", pattern);

				foreach (var key in server.Keys(pattern: pattern))
				{
					keysToDelete.Add(key);
				}

				if (keysToDelete.Count == 0)
				{
					_logger.LogError("No keys found matching pattern: {Pattern}", pattern);
					return;
				}

				_logger.LogError("Found {Count} keys to delete with prefix: {Prefix}", keysToDelete.Count, prefix);

				// Delete each key individually
				foreach (var key in keysToDelete)
				{
					await database.KeyDeleteAsync(key);
					_logger.LogError("Deleted key: {Key}", key);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error removing cache by prefix: {Prefix}", prefix);
			}
		}

		private IServer? GetServer()
		{
			// Get the first available server in the Redis cluster
			var endPoints = _connectionMultiplexer.GetEndPoints();
			if (endPoints is null || endPoints.Length == 0)
				return null;

			return _connectionMultiplexer.GetServer(endPoints.First());
		}

		public async Task<bool> ExistsAsync(string key)
		{
			key = BuildUserSpecificKey(key);

			try
			{
				var cachedValue = await _distributedCache.GetAsync(key);
				return cachedValue != null && cachedValue.Length > 0;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error checking if key exists: {Key}", key);
				return false;
			}
		}

		private string BuildUserSpecificKey(string key)
		{

			_logger.LogError("Build User Specific Key: {Key}", key);

			var userId = _contextAccessor.HttpContext!.User.FindFirst("UserId")?.Value!;

			_logger.LogError("After accessor, User id : {userId}", userId);

			if (string.IsNullOrEmpty(userId))
				return $":user:anonymous:{key}";

			_logger.LogError("After accessor with not null , User id : {userId}", userId);
			return $":user:{userId}:{key}";
		}
	}
}
