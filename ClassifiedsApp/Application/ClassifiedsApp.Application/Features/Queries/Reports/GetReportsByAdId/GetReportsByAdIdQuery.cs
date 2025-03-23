using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetReportsByAdId;

public class GetReportsByAdIdQuery : IRequest<GetReportsByAdIdQueryResponse>
{
	public Guid AdId { get; set; }
}
