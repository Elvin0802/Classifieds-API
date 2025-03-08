using AutoMapper;
using ClassifiedsApp.Core.Dtos.Locations;
using ClassifiedsApp.Core.Interfaces.Repositories.Locations;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Locations.GetLocationById;

public class GetLocationByIdQueryHandler : IRequestHandler<GetLocationByIdQuery, GetLocationByIdQueryResponse>
{
	readonly ILocationReadRepository _readRepository;
	readonly IMapper _mapper;

	public GetLocationByIdQueryHandler(ILocationReadRepository readRepository, IMapper mapper)
	{
		_readRepository = readRepository;
		_mapper = mapper;
	}

	public async Task<GetLocationByIdQueryResponse> Handle(GetLocationByIdQuery request, CancellationToken cancellationToken)
	{
		var item = await _readRepository.GetByIdAsync(request.Id, false);

		if (item is null)
			return null!;

		await Task.Delay(400);

		return new()
		{
			Location = _mapper.Map<LocationDto>(item)
		};
	}
}