using ClassifiedsApp.Application.Dtos.Ads;
using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Application.Interfaces.Services.Ads;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Infrastructure.Services.Ads;

public class FeaturedAdService : IFeaturedAdService
{
	private readonly IAdReadRepository _readRepository;

	public FeaturedAdService(IAdReadRepository readRepository)
	{
		_readRepository = readRepository;
	}

	public async Task<IList<FeaturedAdPricingDto>> GetFeaturePricingOptionsAsync()
	{
		return new List<FeaturedAdPricingDto>
		{
			new FeaturedAdPricingDto
			{
				DurationDays = 1,
				Price = 1.59m,
				Description = "1 day featured ad."
			},
			new FeaturedAdPricingDto
			{
				DurationDays = 5,
				Price = 5.59m,
				Description = "5 day featured ad."
			},
			new FeaturedAdPricingDto
			{
				DurationDays = 10,
				Price = 9.59m,
				Description = "10 day featured ad."
			},
			new FeaturedAdPricingDto
			{
				DurationDays = 15,
				Price = 14.59m,
				Description = "15 day featured ad."
			},
			new FeaturedAdPricingDto
			{
				DurationDays = 30,
				Price = 25.59m,
				Description = "30 day featured ad."
			}
		};
	}

	public async Task<decimal> CalculateFeaturePrice(int durationDays)
	{
		var options = await GetFeaturePricingOptionsAsync();
		var option = options.FirstOrDefault(o => o.DurationDays == durationDays);

		if (option is null)
			return Math.Round(durationDays * 1.59m, 2);

		return option.Price;
	}

	public async Task<bool> IsAdFeaturedAsync(Guid adId)
	{
		var ad = await _readRepository.Table
			.Where(a => a.Id == adId)
			.Select(a => new { a.IsFeatured, a.FeatureEndDate })
			.FirstOrDefaultAsync();

		if (ad is null)
			return false;

		return ad.IsFeatured && ad.FeatureEndDate > DateTimeOffset.UtcNow;
	}

	public async Task<DateTimeOffset?> GetFeatureEndDateAsync(Guid adId)
	{
		var ad = await _readRepository.Table
			.Where(a => a.Id == adId)
			.Select(a => new { a.FeatureEndDate })
			.FirstOrDefaultAsync();

		return ad?.FeatureEndDate;
	}
}