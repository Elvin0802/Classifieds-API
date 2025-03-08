using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.DeleteSubCategory;

public class DeleteSubCategoryCommand : IRequest<DeleteSubCategoryCommandResponse>
{
	public Guid Id { get; set; }
}