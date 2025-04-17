using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Features.Commands.Ads.ChangeAdStatus;
using ClassifiedsApp.Application.Features.Commands.Ads.CreateAd;
using ClassifiedsApp.Application.Features.Commands.Ads.DeleteAd;
using ClassifiedsApp.Application.Features.Commands.Ads.FeatureAd;
using ClassifiedsApp.Application.Features.Commands.Ads.UpdateAd;
using ClassifiedsApp.Application.Features.Commands.Users.SelectAdCommand;
using ClassifiedsApp.Application.Features.Commands.Users.UnselectAd;
using ClassifiedsApp.Application.Features.Queries.Ads.GetAdById;
using ClassifiedsApp.Application.Features.Queries.Ads.GetAllAds;
using ClassifiedsApp.Application.Features.Queries.Ads.GetFeaturedPricing;
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
	public async Task<ActionResult<Result>> Create([FromBody] CreateAdCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<Result<GetAllAdsQueryResponse>>> GetAll([FromBody] GetAllAdsQuery? query)
	{
		return Ok(await _mediator.Send(query!));
	}

	[HttpGet("[action]")]
	public async Task<ActionResult<Result<GetAdByIdQueryResponse>>> GetById([FromQuery] GetAdByIdQuery query)
	{
		return Ok(await _mediator.Send(query));
	}

	[HttpGet("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<Result>> Delete([FromQuery] DeleteAdCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<Result>> SelectAd([FromBody] SelectAdCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<Result>> UnselectAd([FromBody] UnselectAdCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<Result>> Update([FromBody] UpdateAdCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpGet("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<Result<GetFeaturedPricingQueryResponse>>> GetPricingOptions()
	{
		return Ok(await _mediator.Send(new GetFeaturedPricingQuery()));
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<Result>> FeatureAd(FeatureAdCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<ActionResult<Result>> ChangeAdStatus([FromBody] ChangeAdStatusCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

}
