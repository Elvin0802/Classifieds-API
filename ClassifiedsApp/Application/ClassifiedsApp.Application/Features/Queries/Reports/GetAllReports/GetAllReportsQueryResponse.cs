using ClassifiedsApp.Application.Dtos.Reports;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetAllReports;

public class GetAllReportsQueryResponse
{
	public IList<ReportDto> Items { get; set; }
}
