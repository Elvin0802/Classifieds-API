using ClassifiedsApp.Application.Dtos.Reports;
using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetReportById;

public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, GetReportByIdQueryResponse>
{
	readonly IReportReadRepository _repository;

	public GetReportByIdQueryHandler(IReportReadRepository repository)
	{
		_repository = repository;
	}

	public async Task<GetReportByIdQueryResponse> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
	{
		var report = await _repository.GetByIdAsync(request.ReportId);

		if (report is null)
			return new() { Item = null };

		var reportDto = new ReportDto()
		{
			Id = report.Id,
			AdId = report.AdId,
			AdTitle = report.Ad.Title,
			ReportedByUserId = report.ReportedByUserId,
			ReportedByUserName = report.ReportedByUser.Name,
			Reason = report.Reason,
			Description = report.Description,
			Status = report.Status,
			ReviewedByUserId = report.ReviewedByUserId,
			ReviewedByUserName = report.ReviewedByUser!.Name,
			ReviewedAt = report.ReviewedAt,
			ReviewNotes = report.ReviewNotes,
			CreatedAt = report.CreatedAt,
			UpdatedAt = report.UpdatedAt
		};

		return new() { Item = reportDto };
	}
}
