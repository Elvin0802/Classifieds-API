using AutoMapper;
using ClassifiedsApp.Core.Dtos.Locations;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Locations;
using ClassifiedsApp.Core.Interfaces.Services.Cache;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Queries.Locations.GetAllLocations;

public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, GetAllLocationsQueryResponse>
{
	readonly ILocationReadRepository _repository;
	readonly IMapper _mapper;
	readonly ICacheService _cacheService;

	public GetAllLocationsQueryHandler(ILocationReadRepository repository, IMapper mapper, ICacheService cacheService)
	{
		_repository = repository;
		_mapper = mapper;
		_cacheService = cacheService;
	}

	public async Task<GetAllLocationsQueryResponse> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
	{
		/*
			Caching Behavior executes caching with data.
			This code called when store does not have any data.
		*/

		var query = _repository.GetAll(false);

		if (!string.IsNullOrEmpty(request.SortBy))
			query = ApplySorting(query, request.SortBy, request.IsDescending);
		else
			query = request.IsDescending ?
					query.OrderByDescending(p => p.CreatedAt) :
					query.OrderBy(p => p.CreatedAt);

		var totalCount = await query.CountAsync(cancellationToken);

		var list = await query.Skip((request.PageNumber - 1) * request.PageSize)
							  .Take(request.PageSize)
							  .Select(p => _mapper.Map<LocationDto>(p))
							  .ToListAsync(cancellationToken);

		return new()
		{
			Items = list,
			PageNumber = request.PageNumber,
			PageSize = request.PageSize,
			TotalCount = totalCount
		};
	}

	private IQueryable<Location> ApplySorting(IQueryable<Location> query, string sortBy, bool isDescending)
	{
		var propertyName = typeof(Location).GetProperties()
											.FirstOrDefault(p =>
												string.Equals(p.Name, sortBy, StringComparison.OrdinalIgnoreCase))?
												.Name;

		if (propertyName == null)
		{
			return isDescending ?
					query.OrderByDescending(p => p.CreatedAt) :
					query.OrderBy(p => p.CreatedAt);
		}

		return propertyName switch
		{
			nameof(Location.City) => isDescending
				? query.OrderByDescending(p => p.City)
				: query.OrderBy(p => p.City),
			nameof(Location.Country) => isDescending
				? query.OrderByDescending(p => p.Country)
				: query.OrderBy(p => p.Country),
			nameof(Location.CreatedAt) => isDescending
				? query.OrderByDescending(p => p.CreatedAt)
				: query.OrderBy(p => p.CreatedAt),
			nameof(Location.UpdatedAt) => isDescending
				? query.OrderByDescending(p => p.UpdatedAt)
				: query.OrderBy(p => p.UpdatedAt),
			_ => isDescending
				? query.OrderByDescending(p => p.CreatedAt)
				: query.OrderBy(p => p.CreatedAt)
		};
	}
}
