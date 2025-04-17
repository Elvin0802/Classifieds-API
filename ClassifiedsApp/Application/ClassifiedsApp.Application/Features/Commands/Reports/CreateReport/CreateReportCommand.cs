using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Reports.CreateReport;

public class CreateReportCommand : IRequest<Result>
{
	public Guid AdId { get; set; }
	public ReportReason Reason { get; set; }
	public string Description { get; set; }
}
