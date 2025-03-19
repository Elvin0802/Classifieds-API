using ClassifiedsApp.Application.Common.Helpers;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Services.Mail;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.Application.Features.Commands.Auth.PasswordReset;

public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommand, PasswordResetCommandResponse>
{
	readonly UserManager<AppUser> _userManager;
	readonly IMailService _mailService;

	public PasswordResetCommandHandler(UserManager<AppUser> userManager, IMailService mailService)
	{
		_userManager = userManager;
		_mailService = mailService;
	}

	public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
	{
		AppUser? user = await _userManager.FindByEmailAsync(request.Email);

		if (user is not null)
		{
			string resetToken = (await _userManager.GeneratePasswordResetTokenAsync(user)).UrlEncode();

			await _mailService.SendPasswordResetMailAsync(request.Email, user.Id.ToString(), resetToken);

			return new() { IsSucceeded = true };
		}

		return new() { IsSucceeded = false };
	}
}
