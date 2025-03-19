using ClassifiedsApp.Core.Dtos.Auth.Token;
using ClassifiedsApp.Core.Interfaces.Services.Auth;
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
		//var claims = new[]
		//{
		//	new Claim (ClaimsIdentity.DefaultNameClaimType, email),
		//	new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(",", roles)),
		//	new Claim("UserId", id.ToString())
		//}.Concat(userClaims);

		//var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));

		//var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

		//var accessToken = new JwtSecurityToken
		//(
		//	issuer: _jwtConfig.Issuer,
		//	audience: _jwtConfig.Audience,
		//	expires: DateTime.UtcNow.AddMinutes(_jwtConfig.Expiration),
		//	signingCredentials: signingCredentials,
		//	claims: claims
		//);

		//return new JwtSecurityTokenHandler().WriteToken(accessToken);


		//----------------------


		var claims = new List<Claim>
			{
				new Claim(ClaimsIdentity.DefaultNameClaimType, email),
				new Claim("UserId", id.ToString())
			};

		// Add each role as a separate claim
		foreach (var role in roles)
		{
			claims.Add(new Claim(ClaimTypes.Role, role));
		}

		// Add any additional user claims
		claims.AddRange(userClaims);

		var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));
		var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
		var accessToken = new JwtSecurityToken
		(
			issuer: _jwtConfig.Issuer,
			audience: _jwtConfig.Audience,
			expires: DateTime.UtcNow.AddMinutes(_jwtConfig.Expiration),
			signingCredentials: signingCredentials,
			claims: claims
		);
		return new JwtSecurityTokenHandler().WriteToken(accessToken);
	}

	public string GenerateRefreshToken()
	{
		return (Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N")).ToLower();
	}
}
