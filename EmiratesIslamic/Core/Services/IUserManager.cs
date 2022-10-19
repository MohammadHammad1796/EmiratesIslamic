using EmiratesIslamic.Core.Helpers;
using EmiratesIslamic.Core.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmiratesIslamic.Core.Services;

public interface IUserManager
{
    Task<Result> CreateAsync(User user);

    Task<IEnumerable<User>> GetUsersWithRolesExceptAdminAsync();

    Task<User?> GetByIdWithRoleAsync(int id);

    Task<Result> RemoveFromRoleAsync(string role);

    Task<Result> AddToRoleAsync(string role);

    Task<User?> FindByEmailAsync(string email);

    Task<User?> FindByIdAsync(int id);

    Task<Result> DeleteAsync();

    Task<string> GeneratePasswordResetTokenAsync();

    Task<Result> ResetPasswordAsync(string token, string newPassword);

    Task<string> GenerateChangeEmailTokenAsync(string newEmail);

    Task<Result> ChangeEmailAsync(string newEmail, string token);

    Task UpdateNormalizedEmailAsync();

    Task<Result> SetUserNameAsync(string userName);

    Task UpdateNormalizedUserNameAsync();

    Task<User> GetUserAsync(ClaimsPrincipal principal);

    Task<bool> CheckPasswordAsync(User user, string password);

    Task<Result> ChangePasswordAsync(string currentPassword, string newPassword);

    Task<Result> UpdateAsync();
}