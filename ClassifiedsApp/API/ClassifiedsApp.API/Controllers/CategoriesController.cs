using ClassifiedsApp.Application.Features.Commands.Categories.CreateCategory;
using ClassifiedsApp.Application.Features.Commands.Categories.CreateMainCategory;
using ClassifiedsApp.Application.Features.Commands.Categories.CreateSubCategory;
using ClassifiedsApp.Application.Features.Queries.Categories.GetAllCategories;
using ClassifiedsApp.Application.Features.Queries.Categories.GetAllMainCategories;
using ClassifiedsApp.Application.Features.Queries.Categories.GetAllSubCategories;
using ClassifiedsApp.Application.Features.Queries.Categories.GetCategoryById;
using ClassifiedsApp.Application.Features.Queries.Categories.GetMainCategoryById;
using ClassifiedsApp.Application.Features.Queries.Categories.GetSubCategoryById;
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

	#region Category Section

	[HttpPost("create/category")]
	public async Task<ActionResult<CreateCategoryCommandResponse>> Create([FromBody] CreateCategoryCommand createDto)
	{
		var result = await _mediator.Send(createDto);

		return Ok(result);
	}

	[HttpGet("all/category")]
	public async Task<ActionResult<GetAllCategoriesQueryResponse>> GetAll([FromQuery] GetAllCategoriesQuery getAllDto)
	{
		var result = await _mediator.Send(getAllDto);

		return Ok(result);
	}

	[HttpGet("byId/category")]
	public async Task<ActionResult<GetCategoryByIdQueryResponse>> GetById([FromQuery] GetCategoryByIdQuery getByIdDto)
	{
		var result = await _mediator.Send(getByIdDto);

		return Ok(result);
	}

	#endregion

	#region Main Category Section

	[HttpPost("create/main-category")]
	public async Task<ActionResult<CreateMainCategoryCommandResponse>> Create([FromBody] CreateMainCategoryCommand createDto)
	{
		var result = await _mediator.Send(createDto);

		return Ok(result);
	}

	[HttpGet("all/main-category")]
	public async Task<ActionResult<GetAllMainCategoriesQueryResponse>> GetAll([FromQuery] GetAllMainCategoriesQuery getAllDto)
	{
		var result = await _mediator.Send(getAllDto);

		return Ok(result);
	}

	[HttpGet("byId/main-category")]
	public async Task<ActionResult<GetMainCategoryByIdQueryResponse>> GetById([FromQuery] GetMainCategoryByIdQuery getByIdDto)
	{
		var result = await _mediator.Send(getByIdDto);

		return Ok(result);
	}

	#endregion

	#region Sub Category Section

	[HttpPost("create/sub-category")]
	public async Task<ActionResult<CreateSubCategoryCommandResponse>> Create([FromBody] CreateSubCategoryCommand createDto)
	{
		var result = await _mediator.Send(createDto);

		return Ok(result);
	}

	[HttpGet("all/sub-category")]
	public async Task<ActionResult<GetAllSubCategoriesQueryResponse>> GetAll([FromQuery] GetAllSubCategoriesQuery getAllDto)
	{
		var result = await _mediator.Send(getAllDto);

		return Ok(result);
	}

	[HttpGet("byId/sub-category")]
	public async Task<ActionResult<GetSubCategoryByIdQueryResponse>> GetById([FromQuery] GetSubCategoryByIdQuery getByIdDto)
	{
		var result = await _mediator.Send(getByIdDto);

		return Ok(result);
	}

	#endregion

}
