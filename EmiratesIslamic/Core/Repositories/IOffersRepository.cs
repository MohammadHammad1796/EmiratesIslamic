using EmiratesIslamic.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmiratesIslamic.Core.Repositories;

public interface IOffersRepository : IRepository<Offer>
{
    Task<IEnumerable<Offer>> GetLatestAsync(int count);
}