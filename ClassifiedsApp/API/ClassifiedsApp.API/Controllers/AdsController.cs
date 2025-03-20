using ClassifiedsApp.Application.Features.Commands.Ads.CreateAd;
using ClassifiedsApp.Application.Features.Commands.Ads.DeleteAd;
using ClassifiedsApp.Application.Features.Commands.Ads.UpdateAd;
using ClassifiedsApp.Application.Features.Commands.Users.SelectAdCommand;
using ClassifiedsApp.Application.Features.Commands.Users.UnselectAd;
using ClassifiedsApp.Application.Features.Queries.Ads.GetAdById;
using ClassifiedsApp.Application.Features.Queries.Ads.GetAllAds;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<CreateAdCommandResponse>> Create([FromBody] CreateAdCommand command)
	{
		command.AppUserId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<GetAllAdsQueryResponse>> GetAll([FromBody] GetAllAdsQuery? query)
	{
		try
		{
			var userIdClaim = User.FindFirst("UserId")?.Value;

			if (!(string.IsNullOrWhiteSpace(userIdClaim)))
				if (Guid.TryParse(userIdClaim, out var userId))
					query!.CurrentAppUserId = userId;

			return Ok(await _mediator.Send(query!));
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
	public async Task<ActionResult<GetAdByIdResponse>> GetById([FromQuery] GetAdByIdQuery query)
	{
		try
		{
			return Ok(await _mediator.Send(query));
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
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<DeleteAdCommandResponse>> Delete([FromQuery] DeleteAdCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<SelectAdCommandResponse>> SelectAd([FromBody] SelectAdCommand command)
	{
		command.SelectorAppUserId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<UnselectAdCommandResponse>> UnselectAd([FromBody] UnselectAdCommand command)
	{
		command.SelectorAppUserId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<UpdateAdCommandResponse>> Update([FromBody] UpdateAdCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

}
