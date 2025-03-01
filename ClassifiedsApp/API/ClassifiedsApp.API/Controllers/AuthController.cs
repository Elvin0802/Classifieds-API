using ClassifiedsApp.Application.Features.Commands.Auth.Login;
using FluentValidation;
using MediatR;
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
	public async Task<ActionResult<LoginCommandResponse>> Login(LoginCommand lc)
	{
		try
		{
			LoginCommandResponse response = await _mediator.Send(lc);
			return Ok(response);
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
}
