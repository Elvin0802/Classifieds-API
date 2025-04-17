using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.DeleteAd;

public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, Result>
{
	readonly IAdWriteRepository _adWriteRepository;

	public DeleteAdCommandHandler(IAdWriteRepository adWriteRepository)
	{
		_adWriteRepository = adWriteRepository;
	}

	public async Task<Result> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!await _adWriteRepository.RemoveAsync(request.Id))
				throw new KeyNotFoundException($"Ad not found.");

			await _adWriteRepository.SaveAsync();

			return Result.Success("Ad deleted successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
