using AutoMapper;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetSubCategoryById;

public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, GetSubCategoryByIdQueryResponse>
{
	readonly ISubCategoryReadRepository _readRepository;
	readonly IMapper _mapper;

	public GetSubCategoryByIdQueryHandler(ISubCategoryReadRepository readRepository, IMapper mapper)
	{
		_readRepository = readRepository;
		_mapper = mapper;
	}

	public async Task<GetSubCategoryByIdQueryResponse> Handle(GetSubCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		var item = await _readRepository.GetSubCategoryByIdWithIncludesAsync(request.Id, false);

		if (item is null)
			return null!;

		return new()
		{
			SubCategoryDto = _mapper.Map<SubCategoryDto>(item)
		};
	}
}
