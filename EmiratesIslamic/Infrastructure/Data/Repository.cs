using EmiratesIslamic.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmiratesIslamic.Infrastructure.Data;

public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet;
    private readonly ApplicationDbContext _dbContext;

    public Repository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        DbSet = dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await DbSet.ToListAsync();
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbSet.AddAsync(entity);
    }

    public void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}