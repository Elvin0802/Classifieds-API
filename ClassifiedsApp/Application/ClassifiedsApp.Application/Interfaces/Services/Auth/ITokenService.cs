using System.Security.Claims;

namespace ClassifiedsApp.Application.Interfaces.Services.Auth;

public interface ITokenService
{
	string GenerateAccessToken(
		Guid id,
		string email,
		IEnumerable<string> roles,
		IEnumerable<Claim> userClaims
		);

	string GenerateRefreshToken();
}
