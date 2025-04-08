using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.ChangeAdStatus;

public class ChangeAdStatusCommandHandler : IRequestHandler<ChangeAdStatusCommand, ChangeAdStatusCommandResponse>
{
	readonly IAdReadRepository _adReadRepository;
	readonly IAdWriteRepository _adWriteRepository;

	public ChangeAdStatusCommandHandler(IAdReadRepository adReadRepository,
										IAdWriteRepository adWriteRepository)
	{
		_adReadRepository = adReadRepository;
		_adWriteRepository = adWriteRepository;
	}

	public async Task<ChangeAdStatusCommandResponse> Handle(ChangeAdStatusCommand request, CancellationToken cancellationToken)
	{
		Ad ad = await _adReadRepository.GetAdByIdWithIncludesAsync(request.AdId);

		if (ad is null)
			return new() { IsSucceeded = false, Message = "Ad Not Found." };

		ad.Status = request.NewAdStatus;
		ad.UpdatedAt = DateTimeOffset.UtcNow;

		if (ad.Status == AdStatus.Active)
			ad.ExpiresAt = ad.UpdatedAt.AddDays(7);

		return new() { IsSucceeded = true, Message = "Ad Status Changed." };
	}
}
