using ClassifiedsApp.Application.Dtos.Reports;
using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetAllReports;

public class GetAllReportsQueryHandler : IRequestHandler<GetAllReportsQuery, GetAllReportsQueryResponse>
{
	readonly IReportReadRepository _repository;

	public GetAllReportsQueryHandler(IReportReadRepository repository)
	{
		_repository = repository;
	}

	public async Task<GetAllReportsQueryResponse> Handle(GetAllReportsQuery request, CancellationToken cancellationToken)
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

		return new() { Items = reportDtos };
	}
}
