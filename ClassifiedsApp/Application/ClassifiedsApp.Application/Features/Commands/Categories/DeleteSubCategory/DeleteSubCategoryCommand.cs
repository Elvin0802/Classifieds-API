using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.DeleteSubCategory;

public class DeleteSubCategoryCommand : IRequest<Result>
{
	public Guid Id { get; set; }
}