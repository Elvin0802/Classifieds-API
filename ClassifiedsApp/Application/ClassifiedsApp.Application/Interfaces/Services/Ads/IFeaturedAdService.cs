using ClassifiedsApp.Application.Dtos.Ads;

namespace ClassifiedsApp.Application.Interfaces.Services.Ads;

public interface IFeaturedAdService
{
	Task<IList<FeaturedAdPricingDto>> GetFeaturePricingOptionsAsync();
	Task<decimal> CalculateFeaturePrice(int durationDays);
	Task<bool> IsAdFeaturedAsync(Guid adId);
	Task<DateTimeOffset?> GetFeatureEndDateAsync(Guid adId);
}
