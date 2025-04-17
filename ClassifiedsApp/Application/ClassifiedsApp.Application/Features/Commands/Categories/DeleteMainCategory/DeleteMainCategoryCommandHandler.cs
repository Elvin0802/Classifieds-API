using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.DeleteMainCategory;

public class DeleteMainCategoryCommandHandler : IRequestHandler<DeleteMainCategoryCommand, Result>
{
	readonly IMainCategoryWriteRepository _repository;

	public DeleteMainCategoryCommandHandler(IMainCategoryWriteRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result> Handle(DeleteMainCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!await _repository.RemoveAsync(request.Id))
				throw new KeyNotFoundException($"Main Category with this id: \" {request.Id} \" , was not found.");

			await _repository.SaveAsync();

			return Result.Success("Main Category deleted.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
