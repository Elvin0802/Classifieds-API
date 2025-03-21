namespace ClassifiedsApp.Application.Dtos.Common;

public abstract class BaseEntityDto
{
	public Guid Id { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
}
