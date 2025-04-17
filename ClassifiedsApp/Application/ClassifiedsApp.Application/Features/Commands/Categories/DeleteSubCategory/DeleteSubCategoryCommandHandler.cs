using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Interfaces.Repositories.Categories;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.DeleteSubCategory;

public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand, Result>
{
	readonly ISubCategoryWriteRepository _repository;

	public DeleteSubCategoryCommandHandler(ISubCategoryWriteRepository repository)
	{
		_repository = repository;
	}

	public async Task<Result> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
	{
		try
		{
			if (!await _repository.RemoveAsync(request.Id))
				throw new KeyNotFoundException($"Sub Category with this id: \" {request.Id} \" , was not found.");

			await _repository.SaveAsync();

			return Result.Success("Sub Category deleted.");
		}
		catch (Exception ex)
		{
			return Result.Failure($"Error occoured. {ex.Message}");
		}
	}
}
