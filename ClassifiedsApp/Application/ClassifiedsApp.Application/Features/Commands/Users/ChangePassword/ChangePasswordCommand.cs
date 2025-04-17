using ClassifiedsApp.Application.Common.Results;
using MediatR;

namespace ClassifiedsApp.Application.Features.Commands.Users.ChangePassword;

public class ChangePasswordCommand : IRequest<Result>
{
	public string UserId { get; set; }
	public string OldPassword { get; set; }
	public string NewPassword { get; set; }
	public string NewPasswordConfirm { get; set; }
}
