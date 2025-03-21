namespace ClassifiedsApp.Application.Interfaces.Services.Cache;

public interface ICacheService
{
	Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiration = null);
	Task<T> GetAsync<T>(string key);
	Task SetAsync<T>(string key, T value, TimeSpan? expiration = null);
	Task RemoveAsync(string key);
	Task RemoveByPrefixAsync(string prefix);
	Task<bool> ExistsAsync(string key);
}
