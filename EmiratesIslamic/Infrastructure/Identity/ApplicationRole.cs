using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace EmiratesIslamic.Infrastructure.Identity;

public class ApplicationRole : IdentityRole<int>
{
    public IEnumerable<ApplicationUserRole> RoleUsers { get; set; }

    public ApplicationRole()
    {
        RoleUsers = new List<ApplicationUserRole>();
    }
}