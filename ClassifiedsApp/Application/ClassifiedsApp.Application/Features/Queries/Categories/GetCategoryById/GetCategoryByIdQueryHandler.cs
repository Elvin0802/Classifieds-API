using AutoMapper;
using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetCategoryById;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryByIdQueryResponse>>
{
	readonly ICategoryReadRepository _categoryReadRepository;
	readonly IMapper _mapper;

	public GetCategoryByIdQueryHandler(ICategoryReadRepository categoryReadRepository, IMapper mapper)
	{
		_categoryReadRepository = categoryReadRepository;
		_mapper = mapper;
	}

	public async Task<Result<GetCategoryByIdQueryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var item = _mapper.Map<CategoryDto>(await _categoryReadRepository.GetCategoryByIdWithIncludesAsync(request.Id, false))
						?? throw new KeyNotFoundException("Category not found.");

			var data = new GetCategoryByIdQueryResponse()
			{
				Item = item,
			};

			return Result.Success(data, "Category retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetCategoryByIdQueryResponse>($"Failed to retrieve category: {ex.Message}");
		}

	}
}