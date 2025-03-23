using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Application.Interfaces.Services.Ads;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Commands.Ads.FeatureAd;

public class FeatureAdCommandHandler : IRequestHandler<FeatureAdCommand, FeatureAdCommandResponse>
{
	readonly IFeaturedAdTransactionWriteRepository _transactionWriteRepository;
	readonly IAdReadRepository _readRepository;
	readonly IFeaturedAdService _featuredAdService;

	public FeatureAdCommandHandler(IFeaturedAdTransactionWriteRepository transactionWriteRepository,
									IFeaturedAdService featuredAdService,
									IAdReadRepository readRepository)
	{
		_transactionWriteRepository = transactionWriteRepository;
		_featuredAdService = featuredAdService;
		_readRepository = readRepository;
	}

	public async Task<FeatureAdCommandResponse> Handle(FeatureAdCommand request, CancellationToken cancellationToken)
	{
		// yalniz active elanlari vip mumkundur.
		var ad = await _readRepository.Table
					.FirstOrDefaultAsync(a => (a.Id == request.AdId && a.Status == AdStatus.Active),
										 cancellationToken);

		if (ad is null)
			return new() { IsSucceeded = false };

		if (ad.AppUserId != request.AppUserId)
			return new() { IsSucceeded = false };

		// Calculate price
		var price = await _featuredAdService.CalculateFeaturePrice(request.DurationDays);

		// Simulate payment

		// Verify payment (in a real application this might be a separate step)

		// Create transaction

		FeaturedAdTransaction transaction = new()
		{
			AdId = ad.Id,
			AppUserId = ad.AppUserId,
			Amount = price,
			DurationDays = request.DurationDays,
			PaymentReference = Guid.NewGuid().ToString("N").ToLower(), // simulated.
			IsCompleted = true
		};

		// Update ad

		ad.IsFeatured = true;
		ad.FeaturePriority = 1; // Default priority = 1  |--|  1 , 2 , 3
		ad.FeatureStartDate = DateTimeOffset.UtcNow;
		ad.FeatureEndDate = ad.FeatureStartDate.Value.AddDays(transaction.DurationDays);

		// elani vip etmek umumi olaraq elan muddetini 30 gun artirir.
		// eger vip muddeti daha coxdursa , vip muddeti qeder artir.
		ad.ExpiresAt = ad.ExpiresAt.AddDays((transaction.DurationDays <= 30) ? 30 : transaction.DurationDays);

		ad.UpdatedAt = ad.FeatureStartDate.Value;

		await _transactionWriteRepository.AddAsync(transaction);
		await _transactionWriteRepository.SaveAsync();

		return new() { IsSucceeded = true };
	}
}

