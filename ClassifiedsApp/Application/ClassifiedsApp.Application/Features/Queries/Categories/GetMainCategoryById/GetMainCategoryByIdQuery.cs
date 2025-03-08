using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetMainCategoryById;

public class GetMainCategoryByIdQuery : IRequest<GetMainCategoryByIdQueryResponse>
{
	public Guid Id { get; set; }
}
