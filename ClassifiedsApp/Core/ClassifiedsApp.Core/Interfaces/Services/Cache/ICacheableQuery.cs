namespace ClassifiedsApp.Core.Interfaces.Services.Cache;

public interface ICacheableQuery
{
	string CacheKey { get; }
	TimeSpan CacheTime { get; }
}
