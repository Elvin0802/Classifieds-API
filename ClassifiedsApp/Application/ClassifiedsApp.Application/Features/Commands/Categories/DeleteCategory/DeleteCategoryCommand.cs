using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.DeleteCategory;

public class DeleteCategoryCommand : IRequest<Result>
{
	public Guid Id { get; set; }
}
