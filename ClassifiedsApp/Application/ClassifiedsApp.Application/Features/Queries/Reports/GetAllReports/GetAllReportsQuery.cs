using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetAllReports;

public class GetAllReportsQuery : IRequest<Result<GetAllReportsQueryResponse>>
{
	public ReportStatus? Status { get; set; }
}
