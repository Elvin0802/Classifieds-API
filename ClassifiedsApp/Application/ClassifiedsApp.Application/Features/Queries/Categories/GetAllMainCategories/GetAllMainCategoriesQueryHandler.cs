using AutoMapper;
using ClassifiedsApp.Core.Dtos.Categories;
using ClassifiedsApp.Core.Interfaces.Repositories.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetAllMainCategories;

public class GetAllMainCategoriesQueryHandler : IRequestHandler<GetAllMainCategoriesQuery, GetAllMainCategoriesQueryResponse>
{
	readonly IMainCategoryReadRepository _repository;
	readonly IMapper _mapper;

	public GetAllMainCategoriesQueryHandler(IMainCategoryReadRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<GetAllMainCategoriesQueryResponse> Handle(GetAllMainCategoriesQuery request, CancellationToken cancellationToken)
	{
		var query = _repository.GetAll(false)
							   .Where(mc => mc.CreatedAt > mc.ArchivedAt)  // sadece aktiv olanlari secirik.
							   .Include(mc => mc.SubCategories.OrderBy(sc => sc.SortOrder)) // xususi prop ile siralamaq.
							   .ThenInclude(sc => sc.Options.OrderBy(op => op.SortOrder)) // xususi prop ile siralamaq.
							   .OrderByDescending(p => p.UpdatedAt); // yeniden kohneye dogru siralamaq.

		var totalCount = await query.CountAsync(cancellationToken);

		var list = await query.Skip((request.PageNumber - 1) * request.PageSize)
							  .Take(request.PageSize)
							  .Select(mc => _mapper.Map<MainCategoryDto>(mc))
							  .ToListAsync(cancellationToken);

		return new GetAllMainCategoriesQueryResponse
		{
			Items = list,
			PageNumber = request.PageNumber,
			PageSize = request.PageSize,
			TotalCount = totalCount
		};
	}
}
