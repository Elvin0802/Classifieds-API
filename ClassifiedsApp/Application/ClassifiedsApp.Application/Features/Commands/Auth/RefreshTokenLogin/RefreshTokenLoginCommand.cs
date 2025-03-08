using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.RefreshTokenLogin;

public class RefreshTokenLoginCommand : IRequest<RefreshTokenLoginCommandResponse>
{
	public string RefreshToken { get; set; }
}
