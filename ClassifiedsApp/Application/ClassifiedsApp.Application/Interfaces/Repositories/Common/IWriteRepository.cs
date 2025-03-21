using ClassifiedsApp.Core.Entities.Common;

namespace ClassifiedsApp.Application.Interfaces.Repositories.Common;

public interface IWriteRepository<T> : IBaseRepository<T> where T : BaseEntity
{
	Task<bool> AddAsync(T model);
	Task<bool> AddRangeAsync(List<T> datas);
	bool Remove(T model);
	bool RemoveRange(List<T> datas);
	Task<bool> RemoveAsync(Guid id);
	bool Update(T model);

	Task<int> SaveAsync();
}