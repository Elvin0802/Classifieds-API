using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.DeleteMainCategory;

public class DeleteMainCategoryCommand : IRequest<DeleteMainCategoryCommandResponse>
{
	public Guid Id { get; set; }
}
