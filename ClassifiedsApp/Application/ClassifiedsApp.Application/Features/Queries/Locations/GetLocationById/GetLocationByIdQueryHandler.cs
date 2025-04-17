using AutoMapper;
using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Locations;
using ClassifiedsApp.Application.Interfaces.Repositories.Locations;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Locations.GetLocationById;

public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, Result<GetLocationByIdQueryResponse>>
{
	readonly ILocationReadRepository _readRepository;
	readonly IMapper _mapper;

	public GetLocationByIdQueryHandler(ILocationReadRepository readRepository, IMapper mapper)
	{
		_readRepository = readRepository;
		_mapper = mapper;
	}

	public async Task<Result<GetLocationByIdQueryResponse>> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var item = _mapper.Map<LocationDto>(await _readRepository.GetByIdAsync(request.Id, false));

			return item is null
					? throw new KeyNotFoundException("Location not found.")
					: Result.Success(new GetLocationByIdQueryResponse() { Item = item }, "Location retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetLocationByIdQueryResponse>($"Failed to retrieve location: {ex.Message}");
		}
	}
}