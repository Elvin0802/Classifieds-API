using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Reports.CreateReport;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, CreateReportCommandResponse>
{
	readonly IAdReadRepository _adReadRepository;
	readonly IReportWriteRepository _reportWriteRepository;

	public CreateReportCommandHandler(IAdReadRepository adReadRepository,
									  IReportWriteRepository reportWriteRepository)
	{
		_adReadRepository = adReadRepository;
		_reportWriteRepository = reportWriteRepository;
	}

	public async Task<CreateReportCommandResponse> Handle(CreateReportCommand request, CancellationToken cancellationToken)
	{
		var ad = await _adReadRepository.GetByIdAsync(request.AdId, false);

		if (ad is null)
			return new() { IsSucceeded = false };

		var report = new Report()
		{
			AdId = request.AdId,
			ReportedByUserId = request.AppUserId,
			Reason = request.Reason,
			Description = request.Description,
			Status = ReportStatus.Pending,
			ReviewNotes = "",
			ReportedByUser = null,
			ReviewedAt = DateTimeOffset.MinValue,
		};

		await _reportWriteRepository.AddAsync(report);
		await _reportWriteRepository.SaveAsync();

		return new() { IsSucceeded = true };
	}

}
