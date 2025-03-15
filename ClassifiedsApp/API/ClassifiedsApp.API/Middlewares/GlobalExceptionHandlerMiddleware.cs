using Serilog;
using System.Net;
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
		var userId = context.User.FindFirst("sub")?.Value ?? "Unknown";
		var username = context.User.Identity?.Name ?? "Anonymous";

		Log.Error("Unhandled Exception | User: {Username} ({UserId}) | Path: {Path} | Error: {ErrorMessage}",
			username, userId, context.Request.Path, ex.Message);
	}

	private static Task HandleExceptionAsync(HttpContext context, Exception ex)
	{
		var response = new { message = ex.Message, status = HttpStatusCode.InternalServerError };
		var payload = JsonSerializer.Serialize(response);

		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

		return context.Response.WriteAsync(payload);
	}
}