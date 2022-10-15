using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmiratesIslamic.Core.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetByIdAsync(int id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task AddAsync(TEntity entity);
    void Delete(TEntity entity);
}