using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Dtos.Auth.Token;
using ClassifiedsApp.Application.Features.Commands.Auth.ConfirmResetToken;
using ClassifiedsApp.Application.Features.Commands.Auth.Login;
using ClassifiedsApp.Application.Features.Commands.Auth.PasswordReset;
using ClassifiedsApp.Application.Features.Commands.Auth.RefreshTokenLogin;
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
		AuthTokenDto authToken = (await _mediator.Send(command)).Data;

		Response.Headers.Append("Authorization", $"Bearer {authToken.AccessToken}");

		Response.Cookies.Append("refreshToken", authToken.RefreshToken!, new CookieOptions
		{
			HttpOnly = true,
			Secure = false,
			SameSite = SameSiteMode.Lax,
			Expires = authToken.RefreshTokenExpiresAt,
		});

		return Ok(new { token = authToken.AccessToken });
		//return Ok(); // use this line for production.
	}

	[HttpPost("[action]")]
	public async Task<IActionResult> RefreshTokenLogin()
	{
		var refreshToken = Request.Cookies["refreshToken"];
		if (string.IsNullOrEmpty(refreshToken))
			return Unauthorized(new { Error = "Refresh token not found." });

		AuthTokenDto authToken = (await _mediator.Send(new RefreshTokenLoginCommand { RefreshToken = refreshToken })).Data;

		Response.Headers.Append("Authorization", $"Bearer {authToken.AccessToken}");

		Response.Cookies.Append("refreshToken", authToken.RefreshToken!, new CookieOptions
		{
			HttpOnly = true,
			Secure = false,
			SameSite = SameSiteMode.Lax,
			Expires = authToken.RefreshTokenExpiresAt,
		});

		return Ok(new { token = authToken.AccessToken });
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
	public async Task<ActionResult<Result>> PasswordReset([FromBody] PasswordResetCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("confirm-reset-token")]
	public async Task<ActionResult<Result>> ConfirmResetToken([FromBody] ConfirmResetTokenCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

}

