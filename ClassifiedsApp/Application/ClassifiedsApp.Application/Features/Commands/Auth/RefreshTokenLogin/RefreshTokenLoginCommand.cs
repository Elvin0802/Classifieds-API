using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Auth.Token;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.RefreshTokenLogin;

public class RefreshTokenLoginCommand : IRequest<Result<AuthTokenDto>>
{
	public string RefreshToken { get; set; }
}
