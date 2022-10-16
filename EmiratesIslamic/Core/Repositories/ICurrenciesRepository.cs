using EmiratesIslamic.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmiratesIslamic.Core.Repositories;

public interface ICurrenciesRepository : IRepository<Currency>
{
    Task<Currency?> GetByCodeAsync(string code);

    Task<IEnumerable<Currency>> GetAvailableAsync();
}