using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EmiratesIslamic.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>
{
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }

    [MaxLength(250)]
    public string? ImagePath { get; set; }

    public IEnumerable<ApplicationUserRole> UserRoles { get; set; }

    public ApplicationUser()
    {
        UserRoles = new List<ApplicationUserRole>();
    }
}