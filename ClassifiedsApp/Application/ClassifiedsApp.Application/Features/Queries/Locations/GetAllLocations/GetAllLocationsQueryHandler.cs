using AutoMapper;
using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Locations;
using ClassifiedsApp.Application.Interfaces.Repositories.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Queries.Locations.GetAllLocations;

public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, Result<GetAllLocationsQueryResponse>>
{
	readonly ILocationReadRepository _repository;
	readonly IMapper _mapper;

	public GetAllLocationsQueryHandler(ILocationReadRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<Result<GetAllLocationsQueryResponse>> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
	{
		/*
			Caching Behavior executes caching with data.
			This code called when store does not have any data.
		*/
		try
		{
			var query = _repository.GetAll(false);

			var list = await query.Select(p => _mapper.Map<LocationDto>(p))
								.OrderBy(l => l.City)
								.ToListAsync(cancellationToken);

			var data = new GetAllLocationsQueryResponse
			{
				Items = list,
				PageNumber = 1,
				PageSize = list.Count,
				TotalCount = list.Count
			};

			return Result.Success(data, "Locations retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetAllLocationsQueryResponse>($"Failed to retrieve locations: {ex.Message}");
		}
	}

}
