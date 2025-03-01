using ClassifiedsApp.Application.Features.Commands.Ads.CreateAd;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedsApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdsController : ControllerBase
{
	readonly IMediator _mediator;

	public AdsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("create")]
	public async Task<ActionResult<CreateAdCommandResponse>> CreateAd([FromBody] CreateAdCommand createAdDto)
	{
		var result = await _mediator.Send(createAdDto);

		return Ok(result);
	}

}
