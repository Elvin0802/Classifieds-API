using ClassifiedsApp.Application.Features.Commands.Auth.ConfirmResetToken;
using ClassifiedsApp.Application.Features.Commands.Auth.Login;
using ClassifiedsApp.Application.Features.Commands.Auth.PasswordReset;
using ClassifiedsApp.Application.Features.Commands.Auth.RefreshTokenLogin;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedsApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
	readonly IMediator _mediator;

	public AuthController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> Login(LoginCommand command)
	{
		try
		{
			LoginCommandResponse response = await _mediator.Send(command);

			Response.Headers.Append("Authorization", $"Bearer {response.AuthToken.AccessToken}");

			Response.Cookies.Append("refreshToken", response.AuthToken.RefreshToken!, new CookieOptions
			{
				HttpOnly = true,
				Secure = false,
				SameSite = SameSiteMode.Lax,
				Expires = response.AuthToken.RefreshTokenExpiresAt,
			});

			return Ok(new { token = response.AuthToken.AccessToken });
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
			var refreshToken = Request.Cookies["refreshToken"];
			if (string.IsNullOrEmpty(refreshToken))
				return Unauthorized(new { Error = "Refresh token not found." });

			RefreshTokenLoginCommandResponse response = await _mediator.Send(new RefreshTokenLoginCommand { RefreshToken = refreshToken });

			Response.Headers.Append("Authorization", $"Bearer {response.AuthToken.AccessToken}");

			Response.Cookies.Append("refreshToken", response.AuthToken.RefreshToken!, new CookieOptions
			{
				HttpOnly = true,
				Secure = false,
				SameSite = SameSiteMode.Lax,
				Expires = response.AuthToken.RefreshTokenExpiresAt,
			});

			return Ok(new { token = response.AuthToken.AccessToken });
		}
		catch (Exception ex)
		{
			return BadRequest(new { Error = ex.Message });
		}
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<IActionResult> Logout()
	{
		Response.Cookies.Delete("refreshToken", new CookieOptions
		{
			HttpOnly = true,
			Secure = false,
			SameSite = SameSiteMode.Lax,
		});

		Response.Headers.Append("Authorization", "");

		return Ok(new { Message = "Log out success." });
	}

	[HttpPost("reset-password")]
	public async Task<ActionResult<PasswordResetCommandResponse>> PasswordReset([FromBody] PasswordResetCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("confirm-reset-token")]
	public async Task<ActionResult<ConfirmResetTokenCommandResponse>> ConfirmResetToken([FromBody] ConfirmResetTokenCommand command)
	{
		return Ok(await _mediator.Send(command));
	}
}

