using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Ads;
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

			ad.Title = request.Title;
			ad.Description = request.Description;
			ad.Price = request.Price;
			ad.CategoryId = request.CategoryId;
			ad.LocationId = request.LocationId;

			ad.UpdatedAt = DateTimeOffset.Now;

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
