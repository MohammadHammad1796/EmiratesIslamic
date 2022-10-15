using System.Threading.Tasks;

namespace EmiratesIslamic.Core.Repositories;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}