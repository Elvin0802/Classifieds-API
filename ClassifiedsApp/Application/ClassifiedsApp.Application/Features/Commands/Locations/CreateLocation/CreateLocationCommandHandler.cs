using AutoMapper;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Locations;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Locations.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, CreateLocationCommandResponse>
{
	readonly ILocationWriteRepository _repository;
	readonly IMapper _mapper;

	public CreateLocationCommandHandler(ILocationWriteRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<CreateLocationCommandResponse> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
	{
		try
		{
			request.City = request.City.Trim();
			request.Country = request.Country.Trim();

			if (string.IsNullOrEmpty(request.Country) || string.IsNullOrEmpty(request.City))
				throw new ArgumentNullException("Country or city name must be fill.");

			var location = _mapper.Map<Location>(request);

			await _repository.AddAsync(location);
			await _repository.SaveAsync();

			return new()
			{
				IsSucceeded = true,
				Message = "Location created."
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = $"Location creating failed.  {ex.Message}"
			};
		}
	}
}
