using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Locations.GetLocationById;

public class GetLocationByIdQuery : IRequest<GetLocationByIdQueryResponse>
{
	public Guid Id { get; set; }
}