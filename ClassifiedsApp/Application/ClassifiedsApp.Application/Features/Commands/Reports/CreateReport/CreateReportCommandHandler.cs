using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Ads;
using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using ClassifiedsApp.Application.Interfaces.Services.Users;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Reports.CreateReport;

public class CreateReportCommandHandler : IRequestHandler<CreateReportCommand, Result>
{
	readonly IAdReadRepository _adReadRepository;
	readonly IReportWriteRepository _reportWriteRepository;
	readonly ICurrentUserService _currentUserService;

	public CreateReportCommandHandler(IAdReadRepository adReadRepository,
									  IReportWriteRepository reportWriteRepository,
									  ICurrentUserService currentUserService)
	{
		_adReadRepository = adReadRepository;
		_reportWriteRepository = reportWriteRepository;
		_currentUserService = currentUserService;
	}

	public async Task<Result> Handle(CreateReportCommand request, CancellationToken cancellationToken)
	{
		try
		{
			var ad = await _adReadRepository.GetByIdAsync(request.AdId, false);

			if (ad is null)
				throw new KeyNotFoundException("Ad not found by id.");

			await _reportWriteRepository.AddAsync(
			new Report()
			{
				AdId = request.AdId,
				ReportedByUserId = _currentUserService.UserId!.Value,
				Reason = request.Reason,
				Description = request.Description,
				Status = ReportStatus.Pending,
				ReviewNotes = "",
				ReviewedAt = DateTimeOffset.MinValue
			});

			await _reportWriteRepository.SaveAsync();

			return Result.Success("Report created.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
