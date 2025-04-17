using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using ClassifiedsApp.Core.Entities;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result>
{
	readonly ICategoryWriteRepository _writeRepository;

	public CreateCategoryCommandHandler(ICategoryWriteRepository writeRepository)
	{
		_writeRepository = writeRepository;
	}

	public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Category newCategory = new()
			{
				Name = request.Name.Trim(),
				Slug = request.Name.Trim().ToLower().Replace(" ", "-"),
			};

			await _writeRepository.AddAsync(newCategory);
			await _writeRepository.SaveAsync();

			return Result.Success("Category created.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}

