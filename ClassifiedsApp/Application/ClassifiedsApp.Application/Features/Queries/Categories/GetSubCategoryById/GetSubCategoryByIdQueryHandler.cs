using AutoMapper;
using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Categories;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetSubCategoryById;

public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, Result<GetSubCategoryByIdQueryResponse>>
{
	readonly ISubCategoryReadRepository _readRepository;
	readonly IMapper _mapper;

	public GetSubCategoryByIdQueryHandler(ISubCategoryReadRepository readRepository, IMapper mapper)
	{
		_readRepository = readRepository;
		_mapper = mapper;
	}

	public async Task<Result<GetSubCategoryByIdQueryResponse>> Handle(GetSubCategoryByIdQuery request, CancellationToken cancellationToken)
	{
		try
		{
			var item = _mapper.Map<SubCategoryDto>(await _readRepository.GetSubCategoryByIdWithIncludesAsync(request.Id, false))
						?? throw new KeyNotFoundException("Sub Category not found.");

			var data = new GetSubCategoryByIdQueryResponse()
			{
				Item = item,
			};

			return Result.Success(data, "Sub Category retrieved successfully.");
		}
		catch (Exception ex)
		{
			return Result.Failure<GetSubCategoryByIdQueryResponse>($"Failed to retrieve sub category : {ex.Message}");
		}
	}
}
