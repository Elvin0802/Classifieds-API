using AutoMapper;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdQueryResponse>
{
	readonly ICategoryReadRepository _categoryReadRepository;
	readonly IMapper _mapper;

	public GetCategoryByIdQueryHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper)
	{
		_categoryReadRepository = categoryReadRepository;
		_mapper = mapper;
	}

	public async Task<GetCategoryByIdQueryResponse> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		var item = await _categoryReadRepository.GetCategoryByIdWithIncludesAsync(request.Id, false);

		if (item is null)
			return null!;

		return new()
		{
			CategoryDto = _mapper.Map<CategoryDto>(item)
		};
	}
}