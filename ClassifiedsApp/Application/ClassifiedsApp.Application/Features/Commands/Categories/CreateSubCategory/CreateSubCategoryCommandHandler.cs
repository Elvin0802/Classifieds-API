using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using ClassifiedsApp.Core.Entities;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateSubCategory;

public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, Result>
{
	readonly ISubCategoryWriteRepository _writeRepository;

	public CreateSubCategoryCommandHandler(ISubCategoryWriteRepository writeRepository)
	{
		_writeRepository = writeRepository;
	}

	public async Task<Result> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			SubCategory newSubCategory = new()
			{
				Name = request.Name.Trim(),
				IsRequired = request.IsRequired,
				IsSearchable = true,
				Type = request.Type,
				MainCategoryId = request.MainCategoryId,
			};

			if (request.Options is not null && request.Options.Count > 0)
			{
				int sortOrderIndex = 0;
				newSubCategory.Options = new List<SubCategoryOption>();

				foreach (var str in request.Options)
				{
					newSubCategory.Options.Add(new()
					{
						SortOrder = sortOrderIndex++,
						SubCategoryId = newSubCategory.Id,
						Value = str
					});
				}
			}

			await _writeRepository.AddAsync(newSubCategory);
			await _writeRepository.SaveAsync();

			return Result.Success("Sub Category created.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
