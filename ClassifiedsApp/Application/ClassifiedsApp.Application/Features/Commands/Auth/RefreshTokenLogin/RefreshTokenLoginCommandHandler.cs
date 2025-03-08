using Azure.Identity;
using ClassifiedsApp.Core.Entities;
using ClassifiedsApp.Core.Interfaces.Services.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace ClassifiedsApp.Application.Features.Commands.Auth.RefreshTokenLogin;

public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommand, RefreshTokenLoginCommandResponse>
{
	readonly UserManager<AppUser> _userManager;
	readonly ITokenService _tokenService;

	public RefreshTokenLoginCommandHandler(UserManager<AppUser> userManager, ITokenService tokenService)
	{
		_userManager = userManager;
		_tokenService = tokenService;
	}

	public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommand request, CancellationToken cancellationToken)
	{
		AppUser? user = await _userManager.Users
										.FirstOrDefaultAsync(user =>
											user.RefreshToken == request.RefreshToken,
											cancellationToken: cancellationToken);

		if (user is null)
			throw new SecurityTokenSignatureKeyNotFoundException("User Not Found.");

		if (user.RefreshTokenExpiresAt > DateTimeOffset.UtcNow)
		{
			RefreshTokenLoginCommandResponse response = new() { AuthToken = new() };

			var roles = await _userManager.GetRolesAsync(user);
			var userClaims = await _userManager.GetClaimsAsync(user);

			response.AuthToken.AccessToken = _tokenService.GenerateAccessToken(user.Id, user.Email!, roles, userClaims);
			response.AuthToken.RefreshToken = _tokenService.GenerateRefreshToken();
			response.AuthToken.RefreshTokenExpiresAt = DateTimeOffset.UtcNow.AddMinutes(1);

			// bu kisim bir user service olucak. ( _userService.UpdateRefreshToken(); )
			user.RefreshToken = response.AuthToken.RefreshToken;
			user.RefreshTokenExpiresAt = response.AuthToken.RefreshTokenExpiresAt;
			await _userManager.UpdateAsync(user);

			return response;
		}
		throw new AuthenticationFailedException("Refresh Token Login Failed.");
	}

}
