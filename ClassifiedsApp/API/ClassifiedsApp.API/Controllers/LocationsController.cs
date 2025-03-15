using ClassifiedsApp.Application.Features.Commands.Locations.CreateLocation;
using ClassifiedsApp.Application.Features.Commands.Locations.DeleteLocation;
using ClassifiedsApp.Application.Features.Commands.Locations.UpdateLocation;
using ClassifiedsApp.Application.Features.Queries.Locations.GetAllLocations;
using ClassifiedsApp.Application.Features.Queries.Locations.GetLocationById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedsApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LocationsController : ControllerBase
{
	readonly IMediator _mediator;

	public LocationsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<CreateLocationCommandResponse>> Create([FromBody] CreateLocationCommand command)
	{
		var result = await _mediator.Send(command);

		return Ok(result);
	}

	[HttpGet("[action]")]
	public async Task<ActionResult<GetAllLocationsQueryResponse>> GetAll([FromQuery] GetAllLocationsQuery command)
	{
		var result = await _mediator.Send(command);

		return Ok(result);
	}

	[HttpGet("[action]")]
	public async Task<ActionResult<GetLocationByIdQueryResponse>> GetById([FromQuery] GetLocationByIdQuery command)
	{
		var result = await _mediator.Send(command);

		return Ok(result);
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<UpdateLocationCommandResponse>> Update([FromBody] UpdateLocationCommand command)
	{
		var result = await _mediator.Send(command);

		return Ok(result);
	}

	[HttpPost("[action]")]
	public async Task<ActionResult<DeleteLocationCommandResponse>> Delete([FromBody] DeleteLocationCommand command)
	{
		var result = await _mediator.Send(command);

		return Ok(result);
	}
}
