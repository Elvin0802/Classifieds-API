using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Categories.CreateCategory;

public class CreateCategoryCommand : IRequest<Result>
{
	public string Name { get; set; }
}
