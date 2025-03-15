using Azure.Identity;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.Application.Features.Commands.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResponse>
{
	readonly UserManager<AppUser> _userManager;
	readonly SignInManager<AppUser> _signInManager;
	readonly ITokenService _tokenService;

	public LoginCommandHandler(UserManager<AppUser> userManager,
								SignInManager<AppUser> signInManager,
								ITokenService tokenService)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_tokenService = tokenService;
	}

	public async Task<LoginCommandResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
	{
		AppUser? user = await _userManager.FindByEmailAsync(request.Email);

		if (user is null)
			throw new Exception("User Not Found.");

		SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

		if (signInResult.Succeeded)
		{
			LoginCommandResponse response = new LoginCommandResponse() { AuthToken = new() };

			var roles = await _userManager.GetRolesAsync(user);
			var userClaims = await _userManager.GetClaimsAsync(user);

			response.AuthToken.AccessToken = _tokenService.GenerateAccessToken(user.Id, request.Email, roles, userClaims);
			response.AuthToken.RefreshToken = _tokenService.GenerateRefreshToken();
			response.AuthToken.RefreshTokenExpiresAt = DateTimeOffset.UtcNow.AddMinutes(10);

			// bu kisim bir user service olucak. ( _userService.UpdateRefreshToken(); )
			user.RefreshToken = response.AuthToken.RefreshToken;
			user.RefreshTokenExpiresAt = response.AuthToken.RefreshTokenExpiresAt;
			await _userManager.UpdateAsync(user);

			return response;
		}
		throw new AuthenticationFailedException("Login Failed.");
	}
}
