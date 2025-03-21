using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.DeleteMainCategory;

public class DeleteMainCategoryCommandHandler : IRequestHandler<DeleteMainCategoryCommand, DeleteMainCategoryCommandResponse>
{
	readonly IMainCategoryWriteRepository _repository;

	public DeleteMainCategoryCommandHandler(IMainCategoryWriteRepository repository)
	{
		_repository = repository;
	}

	public async Task<DeleteMainCategoryCommandResponse> Handle(DeleteMainCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!await _repository.RemoveAsync(request.Id))
				throw new KeyNotFoundException($"Main Category with this id: \" {request.Id} \" , was not found.");

			await _repository.SaveAsync();

			return new()
			{
				IsSucceeded = true,
				Message = $"Main Category deleted."
			};
		}
		catch (Exception ex)
		{
			return new()
			{
				IsSucceeded = false,
				Message = $"Main Category deleting failed. {ex.Message}"
			};
		}
	}
}
