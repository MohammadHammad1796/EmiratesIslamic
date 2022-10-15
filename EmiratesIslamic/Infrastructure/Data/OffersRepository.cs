using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmiratesIslamic.Infrastructure.Data;

public class OffersRepository : Repository<Offer>, IOffersRepository
{
    public OffersRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IEnumerable<Offer>> GetLatestAsync(int count)
    {
        return await DbSet.OrderByDescending(p => p.Id).Skip(0).Take(count).ToListAsync();
    }
}