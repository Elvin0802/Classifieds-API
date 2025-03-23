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
	public async Task<ActionResult<CreateReportCommandResponse>> CreateReport([FromBody] CreateReportCommand command)
	{
		command.AppUserId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		return Ok(await _mediator.Send(command));
	}

	[HttpGet("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<ActionResult<GetAllReportsQueryResponse>> GetAllReports([FromQuery] ReportStatus? status = null)
	{
		return Ok(await _mediator.Send(new GetAllReportsQuery { Status = status }));
	}

	[HttpGet("[action]/{id}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<ActionResult<GetReportByIdQueryResponse>> GetReportById(Guid id)
	{
		return Ok(await _mediator.Send(new GetReportByIdQuery { ReportId = id }));
	}

	[HttpGet("[action]/{adId}")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<ActionResult<GetReportsByAdIdQueryResponse>> GetReportsByAdId(Guid adId)
	{
		return Ok(await _mediator.Send(new GetReportsByAdIdQuery { AdId = adId }));
	}

	[HttpPost("[action]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
	public async Task<ActionResult<UpdateReportStatusCommandResponse>> UpdateReportStatus(UpdateReportStatusCommand command)
	{
		command.AppUserId = Guid.Parse(User.FindFirst("UserId")?.Value!);

		return Ok(await _mediator.Send(command));
	}

}
