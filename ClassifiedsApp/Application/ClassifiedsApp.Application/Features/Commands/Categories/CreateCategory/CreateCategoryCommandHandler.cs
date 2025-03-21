using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using ClassifiedsApp.Core.Entities;
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
				Slug = request.Name.Trim().ToLower().Replace(" ", "-"),
			};

			await _writeRepository.AddAsync(newCategory);
			await _writeRepository.SaveAsync();

			return new()
			{
				IsSucceeded = true,
				Message = "Category created."
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = $"Category creating failed. {ex.Message}"
			};
		}
	}
}

