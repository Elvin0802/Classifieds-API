using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Reports;
using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetReportsByAdId;

public class GetReportsByAdIdQueryHandler : IRequestHandler<GetReportsByAdIdQuery, Result<GetReportsByAdIdQueryResponse>>
{
	readonly IReportReadRepository _repository;

	public GetReportsByAdIdQueryHandler(IReportReadRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result<GetReportsByAdIdQueryResponse>> Handle(GetReportsByAdIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var reportDtos = (await _repository.GetByAdIdAsync(request.Id)).Select(r =>
			new ReportDto()
			{
				Id = r.Id,
				AdId = r.AdId,
				ReportedByUserId = r.ReportedByUserId,
				ReportedByUserName = r.ReportedByUser.Name,
				Reason = r.Reason,
				Description = r.Description,
				Status = r.Status,
				ReviewedByUserId = r.ReviewedByUserId,
				ReviewedByUserName = r.ReviewedByUser!.Name,
				ReviewedAt = r.ReviewedAt,
				ReviewNotes = r.ReviewNotes,
				CreatedAt = r.CreatedAt,
				UpdatedAt = r.UpdatedAt
			}).ToList();

			var data = new GetReportsByAdIdQueryResponse()
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
			return Result.Failure<GetReportsByAdIdQueryResponse>($"Failed to retrieve reports: {ex.Message}");
		}
	}
}
