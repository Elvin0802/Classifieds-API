using ClassifiedsApp.Application.Features.Commands.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedsApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
	readonly IMediator _mediator;

	public UsersController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("register")]
	public async Task<ActionResult<RegisterCommandResponse>> Register(RegisterCommand rc)
	{
		RegisterCommandResponse response = await _mediator.Send(rc);
		return Ok(response);
	}

}
