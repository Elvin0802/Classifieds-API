using ClassifiedsApp.Application.Features.Commands.Auth.ConfirmResetToken;
using ClassifiedsApp.Application.Features.Commands.Auth.Login;
using ClassifiedsApp.Application.Features.Commands.Auth.PasswordReset;
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
				Secure = false,
				SameSite = SameSiteMode.Lax,
				Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.Expiration)
			});

			Response.Cookies.Append("refreshToken", response.AuthToken.RefreshToken!, new CookieOptions
			{
				HttpOnly = true,
				Secure = false,
				SameSite = SameSiteMode.Lax,
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
	public async Task<IActionResult> RefreshTokenLogin()
	{
		try
		{
			var refreshToken = Request.Cookies["refreshToken"]!;

			if (refreshToken is null) return Unauthorized("No Refresh Token");

			RefreshTokenLoginCommandResponse response = await _mediator.Send(new RefreshTokenLoginCommand() { RefreshToken = refreshToken });

			Response.Cookies.Append("accessToken", response.AuthToken.AccessToken, new CookieOptions
			{
				HttpOnly = true,
				Secure = false,
				SameSite = SameSiteMode.Lax,
				Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.Expiration)
			});

			Response.Cookies.Append("refreshToken", response.AuthToken.RefreshToken!, new CookieOptions
			{
				HttpOnly = true,
				Secure = false,
				SameSite = SameSiteMode.Lax,
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

	[HttpPost("reset-password")]
	public async Task<ActionResult<PasswordResetCommandResponse>> PasswordReset([FromBody] PasswordResetCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("confirm-reset-token")]
	public async Task<ActionResult<ConfirmResetTokenCommandResponse>> VerifyResetToken([FromBody] ConfirmResetTokenCommand command)
	{
		return Ok(await _mediator.Send(command));
	}
}

