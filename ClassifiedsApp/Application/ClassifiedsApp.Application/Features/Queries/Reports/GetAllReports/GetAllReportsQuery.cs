using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetAllReports;

public class GetAllReportsQuery : IRequest<GetAllReportsQueryResponse>
{
	public ReportStatus? Status { get; set; }
}
