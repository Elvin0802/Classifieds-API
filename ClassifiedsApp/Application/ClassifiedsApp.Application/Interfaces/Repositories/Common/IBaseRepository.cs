using ClassifiedsApp.Core.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ClassifiedsApp.Application.Interfaces.Repositories.Common;

public interface IBaseRepository<T> where T : BaseEntity
{
	DbSet<T> Table { get; }
}