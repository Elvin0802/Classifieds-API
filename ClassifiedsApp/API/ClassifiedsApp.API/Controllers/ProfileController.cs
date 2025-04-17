using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Features.Queries.Ads.GetAllAds;
using ClassifiedsApp.Application.Features.Queries.Users.GetAllSelectedAds;
using ClassifiedsApp.Application.Features.Queries.Users.GetUserData;
using ClassifiedsApp.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedsApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
public class ProfileController : ControllerBase
{
	readonly IMediator _mediator;

	public ProfileController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<Result<GetUserDataQueryResponse>>> GetUserData()
	{
		return Ok(await _mediator.Send(new GetUserDataQuery()));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<Result<GetAllAdsQueryResponse>>> GetActiveAds()
	{
		return Ok(await _mediator.Send(new GetAllAdsQuery()
		{
			AdStatus = AdStatus.Active,
			SearchedAppUserId = Guid.Parse(User.FindFirst("UserId")?.Value!)
		}));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<Result<GetAllAdsQueryResponse>>> GetPendingAds()
	{
		return Ok(await _mediator.Send(new GetAllAdsQuery()
		{
			AdStatus = AdStatus.Pending,
			SearchedAppUserId = Guid.Parse(User.FindFirst("UserId")?.Value!)
		}));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<Result<GetAllAdsQueryResponse>>> GetExpiredAds()
	{
		return Ok(await _mediator.Send(new GetAllAdsQuery()
		{
			AdStatus = AdStatus.Expired,
			SearchedAppUserId = Guid.Parse(User.FindFirst("UserId")?.Value!)
		}));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<Result<GetAllAdsQueryResponse>>> GetRejectedAds()
	{
		return Ok(await _mediator.Send(new GetAllAdsQuery()
		{
			AdStatus = AdStatus.Rejected,
			SearchedAppUserId = Guid.Parse(User.FindFirst("UserId")?.Value!),
		}));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<Result<GetAllSelectedAdsQueryResponse>>> GetSelectedAds()
	{
		return Ok(await _mediator.Send(new GetAllSelectedAdsQuery()
		{
			CurrentAppUserId = Guid.Parse(User.FindFirst("UserId")?.Value!)
		}));
	}

}
