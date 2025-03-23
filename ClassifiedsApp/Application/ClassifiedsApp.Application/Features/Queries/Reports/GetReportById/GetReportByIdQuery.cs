using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetReportById;

public class GetReportByIdQuery : IRequest<GetReportByIdQueryResponse>
{
	public Guid ReportId { get; set; }
}
