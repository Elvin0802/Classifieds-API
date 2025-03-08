using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateCategory;

public class CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
{
	public string Name { get; set; }
}
