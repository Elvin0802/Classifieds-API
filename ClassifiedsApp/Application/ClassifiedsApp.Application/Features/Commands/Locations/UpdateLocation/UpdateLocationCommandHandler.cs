using ClassifiedsApp.Core.Interfaces.Repositories.Locations;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Locations.UpdateLocation;

public class UpdateLocationCommandHandler : IRequestHandler<UpdateLocationCommand, UpdateLocationCommandResponse>
{
	readonly ILocationReadRepository _readRepository;
	readonly ILocationWriteRepository _writeRepository;

	public UpdateLocationCommandHandler(ILocationReadRepository readRepository,
										ILocationWriteRepository writeRepository)
	{
		_readRepository = readRepository;
		_writeRepository = writeRepository;
	}

	public async Task<UpdateLocationCommandResponse> Handle(UpdateLocationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			request.City = request.City.Trim();
			request.Country = request.Country.Trim();

			if (string.IsNullOrEmpty(request.Country) || string.IsNullOrEmpty(request.City))
				throw new ArgumentNullException(nameof(request), "Country or city name must be fill.");

			var location = await _readRepository.GetByIdAsync(request.Id);

			location.UpdatedAt = DateTimeOffset.UtcNow;
			location.City = request.City;
			location.Country = request.Country;

			_writeRepository.Update(location);

			await _writeRepository.SaveAsync();

			return new()
			{
				IsSucceeded = true,
				Message = "Location updated."
			};
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
