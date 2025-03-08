using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetCategoryById;

public class GetCategoryByIdQuery : IRequest<GetCategoryByIdQueryResponse>
{
	public Guid Id { get; set; }
}
