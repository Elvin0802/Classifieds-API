using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Locations.DeleteLocation;

public class DeleteLocationCommand : IRequest<DeleteLocationCommandResponse>
{
	public Guid Id { get; set; }
}
