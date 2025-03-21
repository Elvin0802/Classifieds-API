using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.DeleteAd;

public class DeleteAdCommandHandler : IRequestHandler<DeleteAdCommand, DeleteAdCommandResponse>
{
	readonly IAdWriteRepository _adWriteRepository;

	public DeleteAdCommandHandler(IAdWriteRepository adWriteRepository)
	{
		_adWriteRepository = adWriteRepository;
	}

	public async Task<DeleteAdCommandResponse> Handle(DeleteAdCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!await _adWriteRepository.RemoveAsync(request.Id))
				throw new KeyNotFoundException($"Ad with this id: {request.Id} was not found.");

			await _adWriteRepository.SaveAsync();

			return new()
			{
				IsSucceeded = true,
				Message = $"Ad deleted."
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = $"Ad deleting failed. {ex.Message}"
			};
		}
	}
}
