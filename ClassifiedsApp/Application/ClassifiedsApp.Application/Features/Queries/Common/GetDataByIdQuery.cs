using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Queries.Common;

public class GetDataByIdQuery<T> : IRequest<Result<T>> where T : class
{
	public Guid Id { get; set; }
}
