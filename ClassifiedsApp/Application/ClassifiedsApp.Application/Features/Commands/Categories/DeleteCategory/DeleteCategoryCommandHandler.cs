using ClassifiedsApp.Core.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.DeleteCategory;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeleteCategoryCommandResponse>
{
	readonly ICategoryWriteRepository _repository;

	public DeleteCategoryCommandHandler(ICategoryWriteRepository repository)
	{
		_repository = repository;
	}

	public async Task<DeleteCategoryCommandResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!await _repository.RemoveAsync(request.Id))
				throw new KeyNotFoundException($"Category with this id: \" {request.Id} \" , was not found.");

			await _repository.SaveAsync();

			return new()
			{
				IsSucceeded = true,
				Message = $"Category deleted."
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = $"Category deleting failed. {ex.Message}"
			};
		}
	}
}
