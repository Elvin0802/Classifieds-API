using ClassifiedsApp.Application.Common.Results;
using ClassifiedsApp.Application.Features.Commands.Reports.CreateReport;
using ClassifiedsApp.Application.Features.Commands.Reports.UpdateReportStatus;
using ClassifiedsApp.Application.Features.Queries.Reports.GetAllReports;
using ClassifiedsApp.Application.Features.Queries.Reports.GetReportById;
using ClassifiedsApp.Application.Features.Queries.Reports.GetReportsByAdId;
using ClassifiedsApp.Core.Enums;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassifiedsApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportsController : ControllerBase
{
	readonly IMediator _mediator;

	public ReportsController(IMediator mediator)
	{
		_mediator = mediator;
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
	public async Task<ActionResult<Result>> CreateReport([FromBody] CreateReportCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

	[HttpGet("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<ActionResult<Result<GetAllReportsQueryResponse>>> GetAllReports([FromQuery] ReportStatus? status = null)
	{
		return Ok(await _mediator.Send(new GetAllReportsQuery { Status = status }));
	}

	[HttpGet("[action]/{id}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<ActionResult<Result<GetReportByIdQueryResponse>>> GetReportById(Guid id)
	{
		return Ok(await _mediator.Send(new GetReportByIdQuery { Id = id }));
	}

	[HttpGet("[action]/{adId}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<ActionResult<Result<GetReportsByAdIdQueryResponse>>> GetReportsByAdId(Guid adId)
	{
		return Ok(await _mediator.Send(new GetReportsByAdIdQuery { Id = adId }));
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<ActionResult<Result>> UpdateReportStatus(UpdateReportStatusCommand command)
	{
		return Ok(await _mediator.Send(command));
	}

}
