using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.DeleteMainCategory;

public class DeleteMainCategoryCommand : IRequest<Result>
{
	public Guid Id { get; set; }
}
