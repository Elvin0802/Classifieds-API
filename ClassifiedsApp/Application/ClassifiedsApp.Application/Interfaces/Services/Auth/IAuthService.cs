using ClassifiedsApp.Application.Dtos.Auth.Token;

namespace ClassifiedsApp.Application.Interfaces.Services.Auth;

public interface IAuthService
{
	Task<bool> PasswordResetAsnyc(string email);
	Task<bool> ConfirmResetTokenAsync(string userId, string resetToken);
	Task<AuthTokenDto> LoginAsync(string email, string password);
	Task<AuthTokenDto> RefreshTokenLoginAsync(string refreshToken);
}
