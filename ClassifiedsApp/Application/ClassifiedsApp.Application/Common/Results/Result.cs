namespace ClassifiedsApp.Application.Common.Results;

/// <summary>
/// Represents the result of an operation with success or failure status and additional information.
/// </summary>
public class Result
{
	protected Result(bool isSucceeded, string message)
	{
		IsSucceeded = isSucceeded;
		Message = message;
	}

	public bool IsSucceeded { get; }
	public string Message { get; }
	public bool IsFailed => !IsSucceeded;

	public static Result Success() => new Result(true, string.Empty);
	public static Result Success(string message) => new Result(true, message);
	public static Result Failure(string message) => new Result(false, message);

	public static Result<T> Success<T>(T data) => new Result<T>(data, true, string.Empty);
	public static Result<T> Success<T>(T data, string message) => new Result<T>(data, true, message);
	public static Result<T> Failure<T>(string message) => new Result<T>(default, false, message);
	public static Result<T> Failure<T>(T data, string message) => new Result<T>(data, false, message);
}

/// <summary>
/// Represents the result of an operation with success or failure status and data payload.
/// </summary>
public class Result<T> : Result
{
	public T Data { get; }

	protected internal Result(T data, bool isSucceeded, string message)
		: base(isSucceeded, message)
	{
		Data = data;
	}
}