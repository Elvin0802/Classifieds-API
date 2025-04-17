using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.PasswordReset;

public class PasswordResetCommand : IRequest<Result>
{
	public string Email { get; set; }
}
