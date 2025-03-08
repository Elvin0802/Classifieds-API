using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.DeleteCategory;

public class DeleteCategoryCommand : IRequest<DeleteCategoryCommandResponse>
{
	public Guid Id { get; set; }
}
