using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Reports.UpdateReportStatus;

public class UpdateReportStatusCommand : IRequest<UpdateReportStatusCommandResponse>
{
	public Guid ReportId { get; set; }
	public Guid AppUserId { get; set; }
	public ReportStatus Status { get; set; }
	public string ReviewNotes { get; set; }
}
