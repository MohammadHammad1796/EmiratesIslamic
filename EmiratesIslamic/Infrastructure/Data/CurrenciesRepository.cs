using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmiratesIslamic.Infrastructure.Data;

public class CurrenciesRepository : Repository<Currency>, ICurrenciesRepository
{
    public CurrenciesRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<Currency?> GetByCodeAsync(string code)
    {
        return await DbSet.FindAsync(code);
    }

    public async Task<IEnumerable<Currency>> GetAvailableAsync()
    {
        return await DbSet.Where(c => c.IsAvailable == true).ToListAsync();
    }
}