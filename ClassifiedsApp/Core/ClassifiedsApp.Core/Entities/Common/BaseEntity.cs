namespace ClassifiedsApp.Core.Entities.Common;

public class BaseEntity
{
	public Guid Id { get; set; }
	public DateTimeOffset CreatedAt { get; set; }
	public DateTimeOffset UpdatedAt { get; set; }
	public DateTimeOffset ArchivedAt { get; set; }

	public BaseEntity()
	{
		Id = Guid.NewGuid();
		CreatedAt = DateTimeOffset.UtcNow;
		UpdatedAt = CreatedAt;
		ArchivedAt = DateTimeOffset.MinValue;
	}
}
