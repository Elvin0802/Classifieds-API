using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using ClassifiedsApp.Core.Entities;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateMainCategory;

public class CreateMainCategoryCommandHandler : IRequestHandler<CreateMainCategoryCommand, Result>
{
	readonly IMainCategoryWriteRepository _writeRepository;

	public CreateMainCategoryCommandHandler(IMainCategoryWriteRepository writeRepository)
	{
		_writeRepository = writeRepository;
	}

	public async Task<Result> Handle(CreateMainCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			MainCategory newMainCategory = new()
			{
				Name = request.Name.Trim(),
				Slug = request.Name.Trim().ToLower().Replace(" ", "-"),
				ParentCategoryId = request.ParentCategoryId,
			};

			await _writeRepository.AddAsync(newMainCategory);
			await _writeRepository.SaveAsync();

			return Result.Success("Main Category created.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
