using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Core.Entities;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.UpdateAd;

public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, UpdateAdCommandResponse>
{
	readonly IAdWriteRepository _writeRepository;
	readonly IAdReadRepository _readRepository;

	public UpdateAdCommandHandler(IAdWriteRepository writeRepository, IAdReadRepository readRepository)
	{
		_writeRepository = writeRepository;
		_readRepository = readRepository;
	}

	public async Task<UpdateAdCommandResponse> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Ad ad = await _readRepository.GetByIdAsync(request.Id);

			if (ad is null)
				throw new ArgumentNullException(nameof(ad), "Ad Not Found.");

			ad.Description = request.Description;
			ad.Price = request.Price;
			ad.IsNew = request.IsNew;

			ad.UpdatedAt = DateTimeOffset.UtcNow;

			_writeRepository.Update(ad);

			await _writeRepository.SaveAsync();

			return new()
			{
				IsSucceeded = true,
				Message = "Ad updated."
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = $"Ad updating failed. {ex.Message}"
			};
		}
	}
}
