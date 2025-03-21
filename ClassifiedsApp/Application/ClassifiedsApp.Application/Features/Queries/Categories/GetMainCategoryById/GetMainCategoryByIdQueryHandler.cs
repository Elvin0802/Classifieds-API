using AutoMapper;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetMainCategoryById;

public class GetMainCategoryByIdQueryHandler : IRequestHandler<GetMainCategoryByIdQuery, GetMainCategoryByIdQueryResponse>
{
	readonly IMainCategoryReadRepository _readRepository;
	readonly IMapper _mapper;

	public GetMainCategoryByIdQueryHandler(IMainCategoryReadRepository readRepository, IMapper mapper)
	{
		_readRepository = readRepository;
		_mapper = mapper;
	}

	public async Task<GetMainCategoryByIdQueryResponse> Handle(GetMainCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		var item = await _readRepository.GetMainCategoryByIdWithIncludesAsync(request.Id, false);

		if (item is null)
			return null!;

		return new()
		{
			MainCategoryDto = _mapper.Map<MainCategoryDto>(item)
		};
	}
}
