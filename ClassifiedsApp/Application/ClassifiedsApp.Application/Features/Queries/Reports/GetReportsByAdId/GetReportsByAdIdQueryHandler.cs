using ClassifiedsApp.Application.Dtos.Reports;
using ClassifiedsApp.Application.Interfaces.Repositories.Reports;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Reports.GetReportsByAdId;

public class GetReportsByAdIdQueryHandler : IRequestHandler<GetReportsByAdIdQuery, GetReportsByAdIdQueryResponse>
{
	readonly IReportReadRepository _repository;

	public GetReportsByAdIdQueryHandler(IReportReadRepository repository)
	{
		_repository = repository;
	}

	public async Task<GetReportsByAdIdQueryResponse> Handle(GetReportsByAdIdQuery request, CancellationToken cancellationToken)
	{
		var reportDtos = (await _repository.GetByAdIdAsync(request.AdId)).Select(r =>
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

		return new() { Items = reportDtos };
	}
}
