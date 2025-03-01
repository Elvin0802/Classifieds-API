using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetAllCategories;

public class CategoryQueryHandler : IRequestHandler<CategoryQuery, CategoryQueryResponse>
{
	readonly ICategoryReadRepository _repository;

	public CategoryQueryHandler(ICategoryReadRepository repository)
	{
		_repository = repository;
	}

	public async Task<CategoryQueryResponse> Handle(CategoryQuery request, CancellationToken cancellationToken)
	{
		var totalCount = _repository.GetAll(false)
									.Where(p => p.IsActive)
									.Count();

		var list = _repository.GetAll(false)
							  .Where(p => p.IsActive)
							  .Skip((request.PageNumber - 1) * request.PageSize)
							  .Take(request.PageSize)
							  .Include(c => c.SubCategories)
							  .ThenInclude(sc => sc.ParentCategory)
							  .Select(p => new Category()
							  {
								  Id = p.Id,
								  Name = p.Name,
								  Slug = p.Slug,
								  IsActive = p.IsActive,
								  IsSubCategory = p.IsSubCategory,
								  CreatedAt = p.CreatedAt,
								  UpdatedAt = p.UpdatedAt
							  }).ToList();

		return new CategoryQueryResponse()
		{
			Items = list,
			PageNumber = request.PageNumber,
			PageSize = request.PageSize,
			TotalCount = totalCount
		};
	}
}
