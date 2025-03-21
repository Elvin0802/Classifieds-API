namespace ClassifiedsApp.Application.Interfaces.Services.Common;

public interface IWriteService<T> where T : class
{
	Task<bool> AddAsync(T data);
	Task<bool> RemoveAsync(T data);
	Task<bool> RemoveByIdAsync(Guid id);
	Task<bool> UpdateAsync(T data);
}
