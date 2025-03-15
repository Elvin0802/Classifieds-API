using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Users.GetAllSelectedAds;

public class GetAllSelectedAdsQuery : IRequest<GetAllSelectedAdsQueryResponse>
{
	public Guid CurrentAppUserId { get; set; }

	public int PageNumber { get; set; } = 1;
	public int PageSize { get; set; } = 10;
}
