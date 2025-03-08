using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Locations.UpdateLocation;

public class UpdateLocationCommand : IRequest<UpdateLocationCommandResponse>
{
	public Guid Id { get; set; }
	public string City { get; set; }
	public string Country { get; set; }
}
