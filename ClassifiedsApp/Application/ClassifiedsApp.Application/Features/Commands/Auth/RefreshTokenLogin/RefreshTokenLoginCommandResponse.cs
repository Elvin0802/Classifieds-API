using ClassifiedsApp.Application.Dtos.Auth.Token;

namespace ClassifiedsApp.Application.Features.Commands.Auth.RefreshTokenLogin;

public class RefreshTokenLoginCommandResponse
{
	public AuthTokenDto AuthToken { get; set; }
}
