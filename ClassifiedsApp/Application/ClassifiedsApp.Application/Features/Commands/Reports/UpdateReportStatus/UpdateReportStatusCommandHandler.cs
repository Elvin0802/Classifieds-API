using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using ClassifiedsApp.Application.Interfaces.Services.Users;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Reports.UpdateReportStatus;

public class UpdateReportStatusCommandHandler : IRequestHandler<UpdateReportStatusCommand, Result>
{
	readonly IReportReadRepository _reportReadRepository;
	readonly IReportWriteRepository _reportWriteRepository;
	readonly ICurrentUserService _currentUserService;

	public UpdateReportStatusCommandHandler(IReportReadRepository reportReadRepository,
											IReportWriteRepository reportWriteRepository,
											ICurrentUserService currentUserService)
	{
		_reportReadRepository = reportReadRepository;
		_reportWriteRepository = reportWriteRepository;
		_currentUserService = currentUserService;
	}

	public async Task<Result> Handle(UpdateReportStatusCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var report = await _reportReadRepository.GetByIdWithIncludesAsync(request.ReportId);

			if (report is null)
				throw new KeyNotFoundException("Report not found by id.");

			report.Status = request.Status;
			report.ReviewNotes = request.ReviewNotes;
			report.ReviewedByUserId = _currentUserService.UserId!.Value;
			report.ReviewedAt = DateTimeOffset.UtcNow;
			report.UpdatedAt = DateTimeOffset.UtcNow;

			_reportWriteRepository.Update(report);

			await _reportWriteRepository.SaveAsync();

			return Result.Success("Report updated.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
