using AutoMapper;
using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Locations;
using ClassifiedsApp.Core.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ClassifiedsApp.Application.Features.Commands.Locations.CreateLocation;

public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Result>
{
	readonly ILocationWriteRepository _repository;
	readonly IMapper _mapper;
	readonly ILogger<CreateLocationCommandHandler> _logger;

	public CreateLocationCommandHandler(ILocationWriteRepository repository,
										IMapper mapper,
										ILogger<CreateLocationCommandHandler> logger)
	{
		_repository = repository;
		_mapper = mapper;
		_logger = logger;
	}

	public async Task<Result> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Handling CreateLocationCommand for City: {City}, Country: {Country}", request.City, request.Country);

		try
		{
			request.City = request.City.Trim();
			request.Country = request.Country.Trim();

			if (string.IsNullOrEmpty(request.Country) || string.IsNullOrEmpty(request.City))
				throw new ArgumentNullException("Country or city name must be fill.");

			var location = _mapper.Map<Location>(request);

			await _repository.AddAsync(location);
			await _repository.SaveAsync();

			_logger.LogInformation("Location created successfully for City: {City}, Country: {Country}", request.City, request.Country);

			return Result.Success("Location created successfully.");
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Failed to create location for City: {City}, Country: {Country}", request.City, request.Country);

			return Result.Failure($"Location creation failed. {ex.Message}");
		}
	}
}
