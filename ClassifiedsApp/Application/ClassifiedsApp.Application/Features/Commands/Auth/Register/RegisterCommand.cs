using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Register;

public class RegisterCommand : IRequest<RegisterCommandResponse>
{
	public string Name { get; set; }
	public string Surname { get; set; }
	public string Email { get; set; }
	public string Password { get; set; }
	public string PhoneNumber { get; set; }
}
