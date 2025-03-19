using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Users.GetUserData;

public class GetUserDataQuery : IRequest<GetUserDataQueryResponse>
{
	public Guid? AppUserId { get; set; }
}
