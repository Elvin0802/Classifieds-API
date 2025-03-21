namespace ClassifiedsApp.Application.Dtos.Cache;

public class CacheConfigDto
{
	public string Configuration { get; set; }
	public string InstanceName { get; set; }
	public TimeSpan DefaultExpiration { get; set; } = TimeSpan.FromMinutes(10);
}
