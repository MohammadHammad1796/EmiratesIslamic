using EmiratesIslamic.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmiratesIslamic.Core.Services;

public interface IRoleManager
{
    Task<IEnumerable<Role>> GetRolesExceptAdminAsync();
}