using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Reports;
using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetReportById;

public class GetReportByIdQueryHandler : IRequestHandler<GetReportByIdQuery, Result<GetReportByIdQueryResponse>>
{
	readonly IReportReadRepository _repository;

	public GetReportByIdQueryHandler(IReportReadRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<GetReportByIdQueryResponse>> Handle(GetReportByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var report = await _repository.GetByIdAsync(request.Id);

			if (report is null) throw new KeyNotFoundException("Report not found.");

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

			var data = new GetReportByIdQueryResponse()
			{
				Item = reportDto
			};

			return Result.Success(data, "Report retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetReportByIdQueryResponse>($"Failed to retrieve report: {ex.Message}");
		}
	}
}
