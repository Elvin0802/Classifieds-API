using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Reports.UpdateReportStatus;

public class UpdateReportStatusCommand : IRequest<Result>
{
	public Guid ReportId { get; set; }
	public ReportStatus Status { get; set; }
	public string ReviewNotes { get; set; }
}
