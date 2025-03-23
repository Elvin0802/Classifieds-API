using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Reports.CreateReport;

public class CreateReportCommand : IRequest<CreateReportCommandResponse>
{
	public Guid AdId { get; set; }
	public Guid AppUserId { get; set; }
	public ReportReason Reason { get; set; }
	public string Description { get; set; }
}
