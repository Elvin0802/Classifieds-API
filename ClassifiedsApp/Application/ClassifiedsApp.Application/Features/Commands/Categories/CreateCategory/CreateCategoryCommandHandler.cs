using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryCommandResponse>
{
	readonly ICategoryWriteRepository _writeRepository;

	public CreateCategoryCommandHandler(ICategoryWriteRepository writeRepository)
	{
		_writeRepository = writeRepository;
	}

	public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			Category newCategory = new()
			{
				Name = request.Name.Trim(),
				IsActive = true,
				IsSubCategory = false,
				Slug = request.Name.Trim().ToLower().Replace(" ", "-"),
			};

			if (request.IsSubCategory)
			{
				newCategory.IsSubCategory = true;
				newCategory.ParentCategoryId = request.ParentCategoryId!.Value;
			}

			await _writeRepository.AddAsync(newCategory);
			await _writeRepository.SaveAsync();

			return new() { IsSucceeded = true, Message = "Category created." };
		}
		catch (Exception ex)
		{
			return new() { IsSucceeded = false, Message = $"Category creating failed.\n\n{ex.Message}" };
		}
	}
}
