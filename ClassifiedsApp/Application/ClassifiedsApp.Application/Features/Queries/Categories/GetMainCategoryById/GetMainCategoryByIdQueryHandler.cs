using AutoMapper;
using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetMainCategoryById;

public class GetMainCategoryByIdQueryHandler : IRequestHandler<GetMainCategoryByIdQuery, Result<GetMainCategoryByIdQueryResponse>>
{
	readonly IMainCategoryReadRepository _readRepository;
	readonly IMapper _mapper;

	public GetMainCategoryByIdQueryHandler(IMainCategoryReadRepository readRepository, IMapper mapper)
	{
		_readRepository = readRepository;
		_mapper = mapper;
	}

	public async Task<Result<GetMainCategoryByIdQueryResponse>> Handle(GetMainCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var item = _mapper.Map<MainCategoryDto>(await _readRepository.GetMainCategoryByIdWithIncludesAsync(request.Id, false))
						?? throw new KeyNotFoundException("Main Category not found.");

			var data = new GetMainCategoryByIdQueryResponse()
			{
				Item = item,
			};

			return Result.Success(data, "Main Category retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetMainCategoryByIdQueryResponse>($"Failed to retrieve main category : {ex.Message}");

		}
	}
}
