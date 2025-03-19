using ClassifiedsApp.Application.Features.Queries.Ads.GetAllAds;
using ClassifiedsApp.Application.Features.Queries.Users.GetUserData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedsApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
	readonly IMediator _mediator;

	public ProfileController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<GetUserDataQueryResponse>> GetUserData()
	{
		var userId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		if (userId == Guid.Empty)
			return Unauthorized();

		return Ok(await _mediator.Send(new GetUserDataQuery()
		{
			AppUserId = userId
		}));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<GetAllAdsQueryResponse>> GetActiveAds()
	{
		var userId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		if (userId == Guid.Empty)
			return Unauthorized();

		GetAllAdsQuery query = new()
		{
			AdStatus = Core.Enums.AdStatus.Active,
			SearchedAppUserId = userId
		};

		return Ok(await _mediator.Send(query));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<GetAllAdsQueryResponse>> GetPendingAds()
	{
		var userId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		if (userId == Guid.Empty)
			return Unauthorized();

		return Ok(await _mediator.Send(new GetAllAdsQuery()
		{
			AdStatus = Core.Enums.AdStatus.Pending,
			SearchedAppUserId = userId
		}));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<GetAllAdsQueryResponse>> GetExpiredAds()
	{
		var userId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		if (userId == Guid.Empty)
			return Unauthorized();

		return Ok(await _mediator.Send(new GetAllAdsQuery()
		{
			AdStatus = Core.Enums.AdStatus.Expired,
			SearchedAppUserId = userId
		}));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<GetAllAdsQueryResponse>> GetRejectedAds()
	{
		var userId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		if (userId == Guid.Empty)
			return Unauthorized();

		return Ok(await _mediator.Send(new GetAllAdsQuery()
		{
			AdStatus = Core.Enums.AdStatus.Rejected,
			SearchedAppUserId = userId
		}));
	}

}
