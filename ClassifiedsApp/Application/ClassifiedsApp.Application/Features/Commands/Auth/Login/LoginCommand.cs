using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Login;

public class LoginCommand : IRequest<LoginCommandResponse>
{
	public string Email { get; set; }
	public string Password { get; set; }
}
