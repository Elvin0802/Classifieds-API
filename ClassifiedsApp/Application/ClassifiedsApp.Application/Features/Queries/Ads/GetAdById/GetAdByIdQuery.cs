using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Ads.GetAdById;

public class GetAdByIdQuery : IRequest<GetAdByIdResponse>
{
	public Guid Id { get; set; }
	public Guid CurrentUserId { get; set; }
}
