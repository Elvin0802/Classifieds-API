using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateMainCategory;

public class CreateMainCategoryCommand : IRequest<Result>
{
	public string Name { get; set; }
	public Guid ParentCategoryId { get; set; }
}
