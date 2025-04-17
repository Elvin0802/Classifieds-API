using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Reports;
using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetAllReports;

public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, Result<GetAllReportsQueryResponse>>
{
	readonly IReportReadRepository _repository;

	public GetAllReportsQueryHandler(IReportReadRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<GetAllReportsQueryResponse>> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var reportDtos = (await _repository.GetAllAsync(request.Status)).Select(r =>
			new ReportDto()
			{
				Id = r.Id,
				AdId = r.AdId,
				AdTitle = r.Ad.Title,
				ReportedByUserId = (r.ReportedByUserId == Guid.Empty) ? Guid.Empty : r.ReportedByUserId,
				ReportedByUserName = (r.ReportedByUser is null) ? "" : r.ReportedByUser.Name,
				Reason = r.Reason,
				Description = r.Description,
				Status = r.Status,
				ReviewedByUserId = (r.ReviewedByUserId == Guid.Empty) ? Guid.Empty : r.ReviewedByUserId,
				ReviewedByUserName = (r.ReviewedByUser is null) ? "" : r.ReviewedByUser.Name,
				ReviewedAt = r.ReviewedAt,
				ReviewNotes = r.ReviewNotes,
				CreatedAt = r.CreatedAt,
				UpdatedAt = r.UpdatedAt
			}).ToList();

			var data = new GetAllReportsQueryResponse
			{
				Items = reportDtos,
				PageNumber = 1,
				PageSize = reportDtos.Count,
				TotalCount = reportDtos.Count
			};

			return Result.Success(data, "Reports retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetAllReportsQueryResponse>($"Failed to retrieve reports: {ex.Message}");
		}
	}
}
