using AutoMapper;
using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetAllCategories;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<GetAllCategoriesQueryResponse>>
{
	readonly ICategoryReadRepository _repository;
	readonly IMapper _mapper;

	public GetAllCategoriesQueryHandler(ICategoryReadRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<Result<GetAllCategoriesQueryResponse>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var query = _repository.GetAll(false)
								   .Where(c => c.CreatedAt > c.ArchivedAt)  // sadece aktiv olanlari secirik.
								   .Include(c => c.MainCategories.OrderBy(mc => mc.UpdatedAt)) // yeniden kohneye dogru siralamaq.
								   .ThenInclude(mc => mc.SubCategories.OrderBy(sc => sc.SortOrder)) // xususi prop ile siralamaq.
								   .ThenInclude(sc => sc.Options.OrderBy(op => op.SortOrder)) // xususi prop ile siralamaq.
								   .OrderByDescending(p => p.UpdatedAt); // yeniden kohneye dogru siralamaq.

			var totalCount = await query.CountAsync(cancellationToken);

			var list = await query.Skip((request.PageNumber - 1) * request.PageSize)
								  .Take(request.PageSize)
								  .Select(c => _mapper.Map<CategoryDto>(c))
								  .ToListAsync(cancellationToken);

			var data = new GetAllCategoriesQueryResponse
			{
				Items = list,
				PageNumber = request.PageNumber,
				PageSize = request.PageSize,
				TotalCount = totalCount
			};

			return Result.Success(data, "Categories retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetAllCategoriesQueryResponse>($"Failed to retrieve Categories: {ex.Message}");
		}
	}
}
