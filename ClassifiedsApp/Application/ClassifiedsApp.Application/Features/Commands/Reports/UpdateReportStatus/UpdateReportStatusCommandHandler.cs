using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Reports.UpdateReportStatus;

public class UpdateReportStatusCommandHandler : IRequestHandler<UpdateReportStatusCommand, UpdateReportStatusCommandResponse>
{
	readonly IReportReadRepository _reportReadRepository;
	readonly IReportWriteRepository _reportWriteRepository;

	public UpdateReportStatusCommandHandler(IReportReadRepository reportReadRepository,
											IReportWriteRepository reportWriteRepository)
	{
		_reportReadRepository = reportReadRepository;
		_reportWriteRepository = reportWriteRepository;
	}

	public async Task<UpdateReportStatusCommandResponse> Handle(UpdateReportStatusCommand request, CancellationToken cancellationToken)
	{
		var report = await _reportReadRepository.GetByIdWithIncludesAsync(request.ReportId);

		if (report is null)
			return new() { IsSucceeded = false };

		report.Status = request.Status;
		report.ReviewNotes = request.ReviewNotes;
		report.ReviewedByUserId = request.AppUserId;
		report.ReviewedAt = DateTimeOffset.UtcNow;
		report.UpdatedAt = DateTimeOffset.UtcNow;

		_reportWriteRepository.Update(report);

		await _reportWriteRepository.SaveAsync();

		return new() { IsSucceeded = true };
	}
}
