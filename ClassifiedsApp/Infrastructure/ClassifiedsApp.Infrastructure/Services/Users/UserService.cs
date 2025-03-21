using ClassifiedsApp.Application.Common.Helpers;
using ClassifiedsApp.Application.Dtos.Auth.Users;
using ClassifiedsApp.Application.Interfaces.Services.Users;
using ClassifiedsApp.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace ClassifiedsApp.Infrastructure.Services.Users;

public class UserService : IUserService
{
	readonly UserManager<AppUser> _userManager;

	public UserService(UserManager<AppUser> userManager)
	{
		_userManager = userManager;
	}

	public async Task<bool> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
	{
		AppUser? user = await _userManager.FindByIdAsync(userId);

		if (user is null)
			return false;

		var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);

		if (result.Succeeded)
			await _userManager.UpdateSecurityStampAsync(user);

		return result.Succeeded;
	}

	public async Task<bool> CreateAsync(CreateAppUserDto dto) // *
	{
		var exisitingUser = await _userManager.FindByEmailAsync(dto.Email);

		if (exisitingUser is not null)
			return false;

		var user = new AppUser
		{
			Email = dto.Email,
			UserName = dto.Email, // * gelecekde Phone number et ki , username login mumkun olsun. | elemeyede bilerem.
			Name = dto.Name,
			PhoneNumber = dto.PhoneNumber,
			CreatedAt = DateTimeOffset.UtcNow,
			UpdatedAt = DateTimeOffset.UtcNow,
			ArchivedAt = DateTimeOffset.MinValue
		};

		var result = await _userManager.CreateAsync(user, dto.Password);

		if (result.Succeeded)
			await _userManager.AddToRoleAsync(user, "User");

		return result.Succeeded;
	}

	public async Task<bool> UpdatePasswordAsync(string userId, string resetToken, string newPassword)
	{
		AppUser? user = await _userManager.FindByIdAsync(userId);

		if (user is null)
			return false;

		var result = await _userManager.ResetPasswordAsync(user, resetToken.UrlDecode(), newPassword);

		if (result.Succeeded)
			await _userManager.UpdateSecurityStampAsync(user);

		return result.Succeeded;
	}

	public async Task UpdateRefreshTokenAsync(AppUser user, string refreshToken, DateTimeOffset expiresAt)
	{
		if (user is null)
			throw new Exception("User is NULL !");

		user.RefreshToken = refreshToken;
		user.RefreshTokenExpiresAt = expiresAt;

		await _userManager.UpdateAsync(user);
	}
}
