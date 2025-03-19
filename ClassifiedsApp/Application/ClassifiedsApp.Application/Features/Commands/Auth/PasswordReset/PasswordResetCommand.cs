using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Auth.PasswordReset;

public class PasswordResetCommand : IRequest<PasswordResetCommandResponse>
{
	public string Email { get; set; }
}
