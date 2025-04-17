using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Application.Interfaces.Services.Ads;
using ClassifiedsApp.Application.Interfaces.Services.Users;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Commands.Ads.FeatureAd;

public class FeatureAdCommandHandler : IRequestHandler<FeatureAdCommand, Result>
{
	readonly IFeaturedAdTransactionWriteRepository _transactionWriteRepository;
	readonly IAdReadRepository _readRepository;
	readonly IFeaturedAdService _featuredAdService;
	readonly ICurrentUserService _currentUserService;

	public FeatureAdCommandHandler(IFeaturedAdTransactionWriteRepository transactionWriteRepository,
								   IFeaturedAdService featuredAdService,
								   IAdReadRepository readRepository,
								   ICurrentUserService currentUserService)
	{
		_transactionWriteRepository = transactionWriteRepository;
		_featuredAdService = featuredAdService;
		_readRepository = readRepository;
		_currentUserService = currentUserService;
	}

	public async Task<Result> Handle(FeatureAdCommand request, CancellationToken cancellationToken)
	{
		try
		{
			// yalniz active elanlari vip mumkundur.
			var ad = await _readRepository.Table
						.FirstOrDefaultAsync(a => (a.Id == request.AdId && a.Status == AdStatus.Active),
											 cancellationToken);

			if (ad is null)
				throw new KeyNotFoundException("Ad Not Found.");

			if (ad.AppUserId != _currentUserService.UserId!.Value)
				throw new Exception("Ids must be the same.");

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

			return Result.Success("Ad feature completed successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}

