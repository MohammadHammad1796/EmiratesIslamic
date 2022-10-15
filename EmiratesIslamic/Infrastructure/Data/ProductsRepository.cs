using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmiratesIslamic.Infrastructure.Data;

public class ProductsRepository : Repository<Product>, IProductsRepository
{
    public ProductsRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<Product>> GetLatestAsync(int count)
    {
        return await DbSet.OrderByDescending(p => p.Id).Skip(0).Take(count).ToListAsync();
    }
}