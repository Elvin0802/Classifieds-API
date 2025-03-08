using ClassifiedsApp.Core.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.UpdateCategory;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryCommandResponse>
{
	readonly ICategoryReadRepository _readRepository;
	readonly ICategoryWriteRepository _writeRepository;

	public UpdateCategoryCommandHandler(ICategoryReadRepository readRepository,
										ICategoryWriteRepository writeRepository)
	{
		_readRepository = readRepository;
		_writeRepository = writeRepository;
	}

	public async Task<UpdateCategoryCommandResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			request.Name = request.Name.Trim();

			if (string.IsNullOrEmpty(request.Name))
				throw new ArgumentNullException(nameof(request), "Category name must be fill.");

			var category = await _readRepository.GetByIdAsync(request.Id);

			category.UpdatedAt = DateTimeOffset.Now;
			category.Name = request.Name;
			category.Slug = request.Name.ToLower().Replace(" ", "-");

			_writeRepository.Update(category);

			await _writeRepository.SaveAsync();

			return new()
			{
				IsSucceeded = true,
				Message = "Category updated."
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = $"Category updating failed. {ex.Message}"
			};
		}
	}
}
