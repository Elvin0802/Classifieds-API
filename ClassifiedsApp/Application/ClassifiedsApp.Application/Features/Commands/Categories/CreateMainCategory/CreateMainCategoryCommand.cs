using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateMainCategory;

public class CreateMainCategoryCommand : IRequest<CreateMainCategoryCommandResponse>
{
	public string Name { get; set; }
	public Guid ParentCategoryId { get; set; }
}
