using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Locations.CreateLocation;

public class CreateLocationCommand : IRequest<CreateLocationCommandResponse>
{
	public string City { get; set; }
	public string Country { get; set; }
}
