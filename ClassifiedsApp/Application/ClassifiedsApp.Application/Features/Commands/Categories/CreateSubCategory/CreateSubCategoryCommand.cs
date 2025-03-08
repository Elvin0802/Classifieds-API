using ClassifiedsApp.Core.Enums;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateSubCategory;

public class CreateSubCategoryCommand : IRequest<CreateSubCategoryCommandResponse>
{
	public string Name { get; set; }
	public bool IsRequired { get; set; }
	public SubCategoryType Type { get; set; }
	public Guid MainCategoryId { get; set; }
	public IList<string>? Options { get; set; }
}
