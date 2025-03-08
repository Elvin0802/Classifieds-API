using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.UpdateCategory;

public class UpdateCategoryCommand : IRequest<UpdateCategoryCommandResponse>
{
	public Guid Id { get; set; }
	public string Name { get; set; }
}
