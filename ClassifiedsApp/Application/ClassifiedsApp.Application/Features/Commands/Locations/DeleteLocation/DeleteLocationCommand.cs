using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Locations.DeleteLocation;

public class DeleteLocationCommand : IRequest<Result>
{
	public Guid Id { get; set; }
}
