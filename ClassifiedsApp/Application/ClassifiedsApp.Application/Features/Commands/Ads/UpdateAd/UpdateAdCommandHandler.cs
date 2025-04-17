using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Core.Entities;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.UpdateAd;

public class UpdateAdCommandHandler : IRequestHandler<UpdateAdCommand, Result>
{
	readonly IAdWriteRepository _writeRepository;
	readonly IAdReadRepository _readRepository;

	public UpdateAdCommandHandler(IAdWriteRepository writeRepository, IAdReadRepository readRepository)
	{
		_writeRepository = writeRepository;
		_readRepository = readRepository;
	}

	public async Task<Result> Handle(UpdateAdCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Ad ad = await _readRepository.GetByIdAsync(request.Id);

			if (ad is null)
				throw new KeyNotFoundException("Ad Not Found.");

			ad.Description = request.Description;
			ad.Price = request.Price;
			ad.IsNew = request.IsNew;

			ad.UpdatedAt = DateTimeOffset.UtcNow;

			_writeRepository.Update(ad);

			await _writeRepository.SaveAsync();

			return Result.Success("Ad updated successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
