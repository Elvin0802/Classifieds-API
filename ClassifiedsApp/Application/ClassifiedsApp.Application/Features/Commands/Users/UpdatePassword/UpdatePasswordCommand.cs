using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Users.UpdatePassword;

public class UpdatePasswordCommand : IRequest<Result>
{
	public string UserId { get; set; }
	public string ResetToken { get; set; }
	public string Password { get; set; }
	public string PasswordConfirm { get; set; }
}
