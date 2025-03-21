using ClassifiedsApp.Application.Interfaces.Services.Cache;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClassifiedsApp.Application.Common.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
													where TRequest : ICacheableQuery
{
	private readonly ICacheService _cacheService;
	private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

	public CachingBehavior(
		ICacheService cacheService,
		ILogger<CachingBehavior<TRequest, TResponse>> logger)
	{
		_cacheService = cacheService;
		_logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		// Only cache if the request implements ICacheableQuery
		if (request is ICacheableQuery cacheableQuery)
		{
			TResponse response;

			var cacheKey = cacheableQuery.CacheKey;

			_logger.LogError("Checking cache for key: {CacheKey}", cacheKey);

			response = await _cacheService.GetOrSetAsync(
				cacheKey,
				async () =>
				{
					_logger.LogError("Cache miss for key: {CacheKey}", cacheKey);
					return await next();
				},
				cacheableQuery.CacheTime);

			return response;
		}

		// If not cacheable, just pass through
		return await next();
	}
}
