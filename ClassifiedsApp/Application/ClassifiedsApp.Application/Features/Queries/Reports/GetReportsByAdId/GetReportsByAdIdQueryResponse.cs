using ClassifiedsApp.Application.Dtos.Reports;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetReportsByAdId;

public class GetReportsByAdIdQueryResponse
{
	public IList<ReportDto> Items { get; set; }
}
