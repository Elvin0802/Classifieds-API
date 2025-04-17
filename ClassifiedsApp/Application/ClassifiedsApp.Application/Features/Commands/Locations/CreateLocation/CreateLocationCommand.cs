using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Locations.CreateLocation;

public class CreateLocationCommand : IRequest<Result>
{
	public string City { get; set; }
	public string Country { get; set; }
}
