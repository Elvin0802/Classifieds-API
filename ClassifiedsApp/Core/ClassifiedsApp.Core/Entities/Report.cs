using ClassifiedsApp.Core.Entities.Common;
using ClassifiedsApp.Core.Enums;

namespace ClassifiedsApp.Core.Entities;

public class Report : BaseEntity
{
	public Guid AdId { get; set; }
	public Ad Ad { get; set; }

	public Guid ReportedByUserId { get; set; }
	public AppUser ReportedByUser { get; set; }

	public ReportReason Reason { get; set; }
	public string Description { get; set; }

	public ReportStatus Status { get; set; } = ReportStatus.Pending;

	public Guid? ReviewedByUserId { get; set; } // admin.
	public AppUser ReviewedByUser { get; set; }

	public DateTimeOffset? ReviewedAt { get; set; }
	public string ReviewNotes { get; set; }
}
