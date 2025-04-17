using ClassifiedsApp.Application.Common.Helpers;
using ClassifiedsApp.Application.Dtos.Auth.Token;
using ClassifiedsApp.Application.Interfaces.Services.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ClassifiedsApp.Application.Services;

public class TokenService : ITokenService
{
	readonly JwtConfigDto _jwtConfig;

	public TokenService(JwtConfigDto jwtConfig)
	{
		_jwtConfig = jwtConfig;
	}

	public string GenerateAccessToken(Guid id, string email, IEnumerable<string> roles, IEnumerable<Claim> userClaims)
	{
		var claims = new List<Claim>
		{
			new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
			new Claim(JwtRegisteredClaimNames.Email, email),
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			new Claim("UserId", id.ToString()) // delete this line. api have Sub.
		};

		foreach (var role in roles)
			claims.Add(new Claim(ClaimTypes.Role, role));

		claims.AddRange(userClaims);

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
		var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		var token = new JwtSecurityToken(
			issuer: _jwtConfig.Issuer,
			audience: _jwtConfig.Audience,
			claims: claims,
			expires: DateTime.UtcNow.AddMinutes(_jwtConfig.Expiration),
			signingCredentials: signingCredentials
		);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}

	public string GenerateRefreshToken()
	{
		return (Convert.ToBase64String(Guid.NewGuid().ToByteArray())).UrlEncode() +
			   (Convert.ToBase64String(Guid.NewGuid().ToByteArray())).UrlEncode();
	}
}
