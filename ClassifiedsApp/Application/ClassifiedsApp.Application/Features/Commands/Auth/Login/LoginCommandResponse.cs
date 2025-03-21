using ClassifiedsApp.Application.Dtos.Auth.Token;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Login;

public class LoginCommandResponse
{
	public AuthTokenDto AuthToken { get; set; }
}
