namespace ClassifiedsApp.Application.Interfaces.Services.Common;

public interface IReadService<T> where T : class
{
	Task<IList<T>> GetAllAsync(bool tracking = true);
	Task<T> GetByIdAsync(Guid id, bool tracking = true);
	IQueryable<T> QuerySort(IQueryable<T> query, string sortBy, bool isDescending);
}

/*

// sorting by property

if (!string.IsNullOrEmpty(filterDto.SortBy))
		{
			query = filterDto.IsDescending
					? query.OrderByDescending(e => EF.Property<object>(e, filterDto.SortBy))
					: query.OrderBy(e => EF.Property<object>(e, filterDto.SortBy));
		}

*/