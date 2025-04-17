using AutoMapper;
using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetAllSubCategories;

public class GetAllSubCategoriesQueryHandler : IRequestHandler<GetAllSubCategoriesQuery, Result<GetAllSubCategoriesQueryResponse>>
{
	readonly ISubCategoryReadRepository _repository;
	readonly IMapper _mapper;

	public GetAllSubCategoriesQueryHandler(ISubCategoryReadRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<Result<GetAllSubCategoriesQueryResponse>> Handle(GetAllSubCategoriesQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var query = _repository.GetAll(false)
								   .Where(c => c.CreatedAt > c.ArchivedAt)  // sadece aktiv olanlari secirik.
								   .Include(sc => sc.Options.OrderBy(op => op.SortOrder)) // xususi prop ile siralamaq.
								   .OrderByDescending(p => p.UpdatedAt); // yeniden kohneye dogru siralamaq.

			var totalCount = await query.CountAsync(cancellationToken);

			var list = await query.Skip((request.PageNumber - 1) * request.PageSize)
								  .Take(request.PageSize)
								  .Select(c => _mapper.Map<SubCategoryDto>(c))
								  .ToListAsync(cancellationToken);

			var data = new GetAllSubCategoriesQueryResponse
			{
				Items = list,
				PageNumber = request.PageNumber,
				PageSize = request.PageSize,
				TotalCount = totalCount
			};

			return Result.Success(data, "Sub Categories retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetAllSubCategoriesQueryResponse>($"Failed to retrieve Sub Categories: {ex.Message}");
		}
	}
}
