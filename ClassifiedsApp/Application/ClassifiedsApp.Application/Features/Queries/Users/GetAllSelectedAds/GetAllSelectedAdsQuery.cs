using ClassifiedsApp.Application.Features.Queries.Common;

namespace ClassifiedsApp.Application.Features.Queries.Users.GetAllSelectedAds;

public class GetAllSelectedAdsQuery : GetAllDataQuery<GetAllSelectedAdsQueryResponse>
{
	public Guid CurrentAppUserId { get; set; }
}
