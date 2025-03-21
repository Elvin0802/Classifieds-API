namespace ClassifiedsApp.Application.Interfaces.Services.Cache;

public interface ICacheableQuery
{
	string CacheKey { get; }
	TimeSpan CacheTime { get; }
}
