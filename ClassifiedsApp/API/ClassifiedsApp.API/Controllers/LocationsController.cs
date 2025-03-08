using ClassifiedsApp.Application.Features.Commands.Locations.CreateLocation;
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

	[HttpPost("create")]
	public async Task<ActionResult<CreateLocationCommandResponse>> Create([FromBody] CreateLocationCommand createDto)
	{
		var result = await _mediator.Send(createDto);

		return Ok(result);
	}

	[HttpGet("all")]
	public async Task<ActionResult<GetAllLocationsQueryResponse>> GetAll([FromQuery] GetAllLocationsQuery getAllDto)
	{
		var result = await _mediator.Send(getAllDto);

		return Ok(result);
	}

	[HttpGet("byId")]
	public async Task<ActionResult<GetLocationByIdQueryResponse>> GetById([FromQuery] GetLocationByIdQuery getByIdDto)
	{
		var result = await _mediator.Send(getByIdDto);

		return Ok(result);
	}
}
