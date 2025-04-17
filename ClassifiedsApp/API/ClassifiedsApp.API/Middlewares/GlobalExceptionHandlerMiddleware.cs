using FluentValidation;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace ClassifiedsApp.API.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;

	public GlobalExceptionHandlerMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task Invoke(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			LogError(context, ex);
			await HandleExceptionAsync(context, ex);
		}
	}

	private static void LogError(HttpContext context, Exception ex)
	{
		var userId = context.User.FindFirstValue(JwtRegisteredClaimNames.Sub) ?? "Unknown";
		var username = context.User.Identity?.Name ?? "Anonymous";

		Log.Error("Unhandled Exception | User: {Username} ({UserId}) | Path: {Path} | Error: {ErrorMessage}",
			username, userId, context.Request.Path, ex.Message);
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception ex)
	{
		var statusCode = HttpStatusCode.InternalServerError;
		var message = ex.Message;

		if (ex is ValidationException validationEx)
		{
			statusCode = HttpStatusCode.BadRequest;
			var errors = validationEx.Errors.Select(e => e.ErrorMessage).ToList();
			message = string.Join("; ", errors);
		}
		else if (ex is UnauthorizedAccessException)
		{
			statusCode = HttpStatusCode.Unauthorized;
		}
		else if (ex is KeyNotFoundException || ex is ArgumentNullException)
		{
			statusCode = HttpStatusCode.NotFound;
		}

		var response = new
		{
			success = false,
			message = message,
			status = statusCode
		};

		var payload = JsonSerializer.Serialize(response);
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)statusCode;

		return context.Response.WriteAsync(payload);
	}
}