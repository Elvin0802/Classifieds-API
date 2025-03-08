using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Categories.GetSubCategoryById;

public class GetSubCategoryByIdQuery : IRequest<GetSubCategoryByIdQueryResponse>
{
	public Guid Id { get; set; }
}
