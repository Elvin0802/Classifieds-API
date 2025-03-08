using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateMainCategory;

public class CreateMainCategoryCommandHandler : IRequestHandler<CreateMainCategoryCommand, CreateMainCategoryCommandResponse>
{
	readonly IMainCategoryWriteRepository _writeRepository;

	public CreateMainCategoryCommandHandler(IMainCategoryWriteRepository writeRepository)
	{
		_writeRepository = writeRepository;
	}

	public async Task<CreateMainCategoryCommandResponse> Handle(CreateMainCategoryCommand request, CancellationToken cancellationToken)
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

			return new() { IsSucceeded = true, Message = "Main Category created." };
		}
		catch (Exception ex)
		{
			return new() { IsSucceeded = false, Message = $"Main Category creating failed. {ex.Message}" };
		}
	}
}
