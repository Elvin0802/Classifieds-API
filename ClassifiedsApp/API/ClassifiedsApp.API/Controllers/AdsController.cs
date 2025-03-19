using ClassifiedsApp.Application.Features.Commands.Ads.CreateAd;
using ClassifiedsApp.Application.Features.Commands.Ads.DeleteAd;
using ClassifiedsApp.Application.Features.Commands.Ads.UpdateAd;
using ClassifiedsApp.Application.Features.Commands.Users.SelectAdCommand;
using ClassifiedsApp.Application.Features.Commands.Users.UnselectAd;
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

	[HttpPost("[action]")]
	[Authorize]
	public async Task<ActionResult<CreateAdCommandResponse>> Create([FromBody] CreateAdCommand command)
	{
		var userId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		if (userId == Guid.Empty)
			return Unauthorized();

		command.AppUserId = userId;

		var result = await _mediator.Send(command);

		return Ok(result);
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<GetAllAdsQueryResponse>> GetAll([FromBody] GetAllAdsQuery? command)
	{
		try
		{
			var userIdClaim = User.FindFirst("UserId")?.Value;

			if (!(string.IsNullOrWhiteSpace(userIdClaim)))
				if (Guid.TryParse(userIdClaim, out var userId))
					command!.CurrentAppUserId = userId;

			return Ok(await _mediator.Send(command!));
		}
		catch (Exception ex)
		{
			var messages = new List<string>();
			while (ex != null)
			{
				messages.Add(ex.Message);
				ex = ex.InnerException!;
			}

			return BadRequest(messages);
		}
	}

	[HttpGet("[action]")]
	public async Task<ActionResult<GetAdByIdResponse>> GetById([FromQuery] GetAdByIdQuery command)
	{
		try
		{
			var result = await _mediator.Send(command);

			return Ok(result);
		}
		catch (Exception ex)
		{
			var messages = new List<string>();
			while (ex != null)
			{
				messages.Add(ex.Message);
				ex = ex.InnerException!;
			}

			return BadRequest(messages);
		}
	}

	[HttpGet("[action]")]
	[Authorize]
	public async Task<ActionResult<DeleteAdCommandResponse>> Delete([FromQuery] DeleteAdCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize]
	public async Task<ActionResult<SelectAdCommandResponse>> SelectAd([FromBody] SelectAdCommand command)
	{
		var userId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		if (userId == Guid.Empty)
			return Unauthorized();

		command.SelectorAppUserId = userId;

		var result = await _mediator.Send(command);

		return Ok(result);
	}

	[HttpPost("[action]")]
	[Authorize]
	public async Task<ActionResult<UnselectAdCommandResponse>> UnselectAd([FromBody] UnselectAdCommand command)
	{
		var userId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		if (userId == Guid.Empty)
			return Unauthorized();

		command.SelectorAppUserId = userId;

		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize]
	public async Task<ActionResult<UpdateAdCommandResponse>> Update([FromBody] UpdateAdCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

}
