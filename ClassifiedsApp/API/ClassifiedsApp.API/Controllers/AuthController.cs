using ClassifiedsApp.Application.Features.Commands.Auth.Login;
using ClassifiedsApp.Application.Features.Commands.Auth.RefreshTokenLogin;
using ClassifiedsApp.Core.Dtos.Auth.Token;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedsApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	readonly IMediator _mediator;
	readonly JwtConfigDto _jwtConfig;

	public AuthController(IMediator mediator, JwtConfigDto jwtConfig)
	{
		_mediator = mediator;
		_jwtConfig = jwtConfig;
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> Login(LoginCommand lc)
	{
		try
		{
			LoginCommandResponse response = await _mediator.Send(lc);

			Response.Cookies.Append("accessToken", response.AuthToken.AccessToken, new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.None,
				Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.Expiration)
			});

			Response.Cookies.Append("refreshToken", response.AuthToken.RefreshToken, new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.None,
				Expires = response.AuthToken.RefreshTokenExpiresAt
			});

			return Ok();
		}
		catch (ValidationException ex)
		{
			var errors = ex.Errors.Select(e => new { Property = e.PropertyName, Error = e.ErrorMessage });
			return BadRequest(new { Errors = errors });
		}
		catch (Exception ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> RefreshTokenLogin(RefreshTokenLoginCommand rtlc)
	{
		try
		{
			RefreshTokenLoginCommandResponse response = await _mediator.Send(rtlc);

			Response.Cookies.Append("accessToken", response.AuthToken.AccessToken, new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.None,
				Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.Expiration)
			});

			Response.Cookies.Append("refreshToken", response.AuthToken.RefreshToken, new CookieOptions
			{
				HttpOnly = true,
				Secure = true,
				SameSite = SameSiteMode.None,
				Expires = response.AuthToken.RefreshTokenExpiresAt
			});

			return Ok();
		}
		catch (Exception ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> Logout()
	{
		Response.Cookies.Delete("accessToken");
		Response.Cookies.Delete("refreshToken");

		return Ok();
	}
}

