using ClassifiedsApp.Application.Interfaces.Repositories.Locations;
using ClassifiedsApp.Application.Interfaces.Services.Cache;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Locations.UpdateLocation;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, UpdateLocationCommandResponse>
{
	readonly ILocationReadRepository _readRepository;
	readonly ILocationWriteRepository _writeRepository;
	readonly ICacheService _cacheService;

	public UpdateLocationCommandHandler(ILocationReadRepository readRepository,
										ILocationWriteRepository writeRepository,
										ICacheService cacheService)
	{
		_readRepository = readRepository;
		_writeRepository = writeRepository;
		_cacheService = cacheService;
	}

	public async Task<UpdateLocationCommandResponse> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			request.City = request.City.Trim();
			request.Country = request.Country.Trim();

			if (string.IsNullOrEmpty(request.Country) || string.IsNullOrEmpty(request.City))
				throw new ArgumentNullException(nameof(request), "Country or city name must be fill.");

			var location = await _readRepository.GetByIdAsync(request.Id) ??
							throw new Exception($"Location with this id: \" {request.Id} \" , was not found.");

			location.City = request.City;
			location.Country = request.Country;
			location.UpdatedAt = DateTimeOffset.UtcNow;

			bool result = _writeRepository.Update(location);

			if (result)
			{
				await _writeRepository.SaveAsync();

				await _cacheService.RemoveByPrefixAsync("locations_");

				return new()
				{
					IsSucceeded = true,
					Message = "Location updated."
				};
			}

			throw new Exception(nameof(result));
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = $"Location updating failed. {ex.Message}"
			};
		}
	}
}
