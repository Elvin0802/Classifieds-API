using MediatR;
using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Context;
using System.Diagnostics;
using System.Security.Claims;

namespace ClassifiedsApp.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
													where TRequest : IRequest<TResponse>
{
	private readonly IHttpContextAccessor _httpContextAccessor;

	public LoggingBehavior(IHttpContextAccessor httpContextAccessor)
	{
		_httpContextAccessor = httpContextAccessor;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
		var httpContext = _httpContextAccessor.HttpContext;
		var userId = httpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "Unknown";
		var username = httpContext?.User.Identity?.Name ?? "Anonymous";

		using (LogContext.PushProperty("UserId", userId))
		using (LogContext.PushProperty("Username", username))
		{
			var requestName = typeof(TRequest).Name;
			Log.Information("Handling {RequestName} | User: {Username} ({UserId}) | Request: {@Request}", requestName, username, userId, request);

			var stopwatch = Stopwatch.StartNew();
			var response = await next();
			stopwatch.Stop();

			Log.Information("Handled {RequestName} | User: {Username} ({UserId}) | Response: {@Response} | Execution Time: {ElapsedMs}ms",
				requestName, username, userId, response, stopwatch.ElapsedMilliseconds);

			return response;
		}
	}
}