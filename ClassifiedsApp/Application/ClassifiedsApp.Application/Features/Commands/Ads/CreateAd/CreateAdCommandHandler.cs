using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Ads;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Ads.CreateAd;

public class CreateAdCommandHandler : IRequestHandler<CreateAdCommand, CreateAdCommandResponse>
{
	readonly IAdWriteRepository _writeRepository;

	public CreateAdCommandHandler(IAdWriteRepository writeRepository)
	{
		_writeRepository = writeRepository;
	}

	public async Task<CreateAdCommandResponse> Handle(CreateAdCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Ad newAd = new()
			{
				Title = request.Title.Trim(),
				Description = request.Description.Trim(),
				Price = request.Price,
				CategoryId = request.CategoryId,
				LocationId = request.LocationId,
				AppUserId = request.AppUserId
			};

			newAd.ExpiresAt = DateTimeOffset.UtcNow.AddDays(7);
			newAd.Images = new List<AdImage>();

			foreach (var i in request.ImageUrls)
				newAd.Images.Add(new()
				{
					AdId = newAd.Id,
					Url = i
				});

			await _writeRepository.AddAsync(newAd);
			await _writeRepository.SaveAsync();

			return new() { IsSucceeded = true, Message = "Product created." };
		}
		catch (Exception ex)
		{
			return new() { IsSucceeded = false, Message = $"Product creating failed.\n\n{ex.Message}" };
		}

	}
}
