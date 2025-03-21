using Azure.Identity;
using ClassifiedsApp.Application.Common.Helpers;
using ClassifiedsApp.Application.Dtos.Auth.Token;
using ClassifiedsApp.Application.Interfaces.Services.Auth;
using ClassifiedsApp.Application.Interfaces.Services.Mail;
using ClassifiedsApp.Application.Interfaces.Services.Users;
using ClassifiedsApp.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ClassifiedsApp.Infrastructure.Services.Auth;

public class AuthService : IAuthService
{
	readonly UserManager<AppUser> _userManager;
	readonly IMailService _mailService;
	readonly SignInManager<AppUser> _signInManager;
	readonly ITokenService _tokenService;
	readonly IUserService _userService;

	public AuthService(UserManager<AppUser> userManager,
						IMailService mailService,
						SignInManager<AppUser> signInManager,
						ITokenService tokenService,
						IUserService userService)
	{
		_userManager = userManager;
		_mailService = mailService;
		_signInManager = signInManager;
		_tokenService = tokenService;
		_userService = userService;
	}

	public async Task<bool> ConfirmResetTokenAsync(string userId, string resetToken)
	{
		AppUser? user = await _userManager.FindByIdAsync(userId);

		if (user is null || string.IsNullOrEmpty(resetToken)) return false;

		return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider,
														"ResetPassword", resetToken.UrlDecode());
	}

	public async Task<AuthTokenDto> LoginAsync(string email, string password)
	{
		AppUser? user = await _userManager.FindByEmailAsync(email) ??
						throw new KeyNotFoundException($"User Not Found with this email: {email}.");

		SignInResult signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);

		if (signInResult.Succeeded)
		{
			var token = new AuthTokenDto();

			var roles = await _userManager.GetRolesAsync(user);
			var userClaims = await _userManager.GetClaimsAsync(user);

			token.AccessToken = _tokenService.GenerateAccessToken(user.Id, email, roles, userClaims);
			token.RefreshToken = _tokenService.GenerateRefreshToken();
			token.RefreshTokenExpiresAt = DateTimeOffset.UtcNow.AddMinutes(30);

			await _userService.UpdateRefreshTokenAsync(user, token.RefreshToken, token.RefreshTokenExpiresAt.Value);

			return token;
		}
		throw new AuthenticationFailedException("Login Failed.");
	}

	public async Task<bool> PasswordResetAsnyc(string email)
	{
		AppUser? user = await _userManager.FindByEmailAsync(email);

		if (user is null) return false;

		string resetToken = (await _userManager.GeneratePasswordResetTokenAsync(user)).UrlEncode();

		await _mailService.SendPasswordResetMailAsync(email, user.Id.ToString(), resetToken);

		return true;
	}

	public async Task<AuthTokenDto> RefreshTokenLoginAsync(string refreshToken)
	{
		AppUser? user = await _userManager.Users.FirstOrDefaultAsync(user => user.RefreshToken == refreshToken) ??
						throw new SecurityTokenSignatureKeyNotFoundException("User Not Found.");

		if (user.RefreshTokenExpiresAt > DateTimeOffset.UtcNow)
		{
			var token = new AuthTokenDto();

			var roles = await _userManager.GetRolesAsync(user);
			var userClaims = await _userManager.GetClaimsAsync(user);

			token.AccessToken = _tokenService.GenerateAccessToken(user.Id, user.Email!, roles, userClaims);
			token.RefreshToken = _tokenService.GenerateRefreshToken();
			token.RefreshTokenExpiresAt = DateTimeOffset.UtcNow.AddMinutes(30);

			await _userService.UpdateRefreshTokenAsync(user, token.RefreshToken, token.RefreshTokenExpiresAt.Value);

			return token;
		}
		throw new AuthenticationFailedException("Refresh Token Login Failed.");
	}
}
