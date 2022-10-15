using EmiratesIslamic.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmiratesIslamic.Core.Repositories;

public interface IProductsRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetLatestAsync(int count);
}