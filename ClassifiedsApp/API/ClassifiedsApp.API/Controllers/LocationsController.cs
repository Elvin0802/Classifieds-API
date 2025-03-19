using ClassifiedsApp.Application.Features.Commands.Locations.CreateLocation;
using ClassifiedsApp.Application.Features.Commands.Locations.DeleteLocation;
using ClassifiedsApp.Application.Features.Commands.Locations.UpdateLocation;
using ClassifiedsApp.Application.Features.Queries.Locations.GetAllLocations;
using ClassifiedsApp.Application.Features.Queries.Locations.GetLocationById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<CreateLocationCommandResponse>> Create([FromBody] CreateLocationCommand command)
	{
		try
		{
			return Ok(await _mediator.Send(command));

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
	public async Task<ActionResult<GetAllLocationsQueryResponse>> GetAll([FromQuery] GetAllLocationsQuery command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpGet("[action]")]
	public async Task<ActionResult<GetLocationByIdQueryResponse>> GetById([FromQuery] GetLocationByIdQuery command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<UpdateLocationCommandResponse>> Update([FromBody] UpdateLocationCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpPost("[action]")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<DeleteLocationCommandResponse>> Delete([FromBody] DeleteLocationCommand command)
	{
		return Ok(await _mediator.Send(command));
	}
}
