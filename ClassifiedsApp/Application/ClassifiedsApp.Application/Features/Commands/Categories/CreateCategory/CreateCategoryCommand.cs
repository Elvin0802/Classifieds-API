using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateCategory;

public class CreateCategoryCommand : IRequest<CreateCategoryCommandResponse>
{
	public string Name { get; set; }
	public bool IsSubCategory { get; set; } // if false , this category is main , else , this category is sub
	public Guid? ParentCategoryId { get; set; } // if null , this category is main , else , this category is sub
}
