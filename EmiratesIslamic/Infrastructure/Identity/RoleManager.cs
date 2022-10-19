using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmiratesIslamic.Infrastructure.Identity;

public class RoleManager : IRoleManager
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public RoleManager(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<IEnumerable<Role>> GetRolesExceptAdminAsync()
    {
        return await _roleManager.Roles
            .Where(r => !r.Name.ToLower().Equals("admin"))
            .Select(r => new Role() { Id = r.Id, Name = r.Name })
            .ToListAsync();
    }
}