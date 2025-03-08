using ClassifiedsApp.Application.Features.Commands.Ads.CreateAd;
using ClassifiedsApp.Application.Features.Queries.Ads.GetAdById;
using ClassifiedsApp.Application.Features.Queries.Ads.GetAllAds;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
	[Authorize]
	public async Task<ActionResult<CreateAdCommandResponse>> CreateAd([FromBody] CreateAdCommand createAdDto)
	{
		var userId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		if (userId == Guid.Empty)
			return Unauthorized();

		createAdDto.AppUserId = userId;

		var result = await _mediator.Send(createAdDto);

		return Ok(result);
	}

	[HttpPost("all")]
	public async Task<ActionResult<GetAllAdsQueryResponse>> GetAll([FromBody] GetAllAdsQuery? getAllDto)
	{
		var result = await _mediator.Send(getAllDto!);

		return Ok(result);
	}

	[HttpGet("byId")]
	public async Task<ActionResult<GetAdByIdResponse>> GetById([FromQuery] GetAdByIdQuery getByIdDto)
	{
		var result = await _mediator.Send(getByIdDto);

		return Ok(result);
	}

}
