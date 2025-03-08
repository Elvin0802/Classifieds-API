using AutoMapper;
using ClassifiedsApp.Core.Dtos.Locations;
using ClassifiedsApp.Core.Interfaces.Repositories.Locations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Queries.Locations.GetAllLocations;

public class GetAllLocationsQueryHandler : IRequestHandler<GetAllLocationsQuery, GetAllLocationsQueryResponse>
{
	readonly ILocationReadRepository _repository;
	readonly IMapper _mapper;

	public GetAllLocationsQueryHandler(ILocationReadRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<GetAllLocationsQueryResponse> Handle(GetAllLocationsQuery request, CancellationToken cancellationToken)
	{
		var query = _repository.GetAll(false)
								.OrderByDescending(p => p.CreatedAt); // yaradilma tarixine gore yeniden kohneye dogru siralamaq.

		var totalCount = await query.CountAsync(cancellationToken);

		var list = await query.Skip((request.PageNumber - 1) * request.PageSize)
							  .Take(request.PageSize)
							  .Select(p => _mapper.Map<LocationDto>(p))
							  .ToListAsync(cancellationToken);

		await Task.Delay(400);

		return new GetAllLocationsQueryResponse
		{
			Items = list,
			PageNumber = request.PageNumber,
			PageSize = request.PageSize,
			TotalCount = totalCount
		};
	}
}