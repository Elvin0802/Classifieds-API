using ClassifiedsApp.Application.Features.Commands.Categories.CreateCategory;
using ClassifiedsApp.Application.Features.Queries.Categories.GetAllCategories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedsApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
	readonly IMediator _mediator;

	public CategoriesController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("create")]
	public async Task<ActionResult<CreateCategoryCommandResponse>> CreateAd([FromBody] CreateCategoryCommand createDto)
	{
		var result = await _mediator.Send(createDto);

		return Ok(result);
	}

	[HttpGet]
	public async Task<ActionResult<CategoryQueryResponse>> GetAll([FromQuery] CategoryQuery getDto)
	{
		var result = await _mediator.Send(getDto);

		return Ok(result);
	}

}



