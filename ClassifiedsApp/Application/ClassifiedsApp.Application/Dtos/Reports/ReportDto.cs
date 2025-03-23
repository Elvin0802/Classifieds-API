using ClassifiedsApp.Application.Dtos.Common;
using ClassifiedsApp.Core.Enums;

namespace ClassifiedsApp.Application.Dtos.Reports;

public class ReportDto : BaseEntityDto
{
	public Guid AdId { get; set; }
	public string AdTitle { get; set; }
	public Guid ReportedByUserId { get; set; }
	public string ReportedByUserName { get; set; }
	public ReportReason Reason { get; set; }
	public string Description { get; set; }
	public ReportStatus Status { get; set; }
	public Guid? ReviewedByUserId { get; set; }
	public string ReviewedByUserName { get; set; }
	public DateTimeOffset? ReviewedAt { get; set; }
	public string ReviewNotes { get; set; }
}