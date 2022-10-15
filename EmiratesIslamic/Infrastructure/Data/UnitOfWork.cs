using EmiratesIslamic.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace EmiratesIslamic.Infrastructure.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly ApplicationDbContext _dbContext;

    public UnitOfWork(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveChangesAsync()
    {
        try
        {
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}