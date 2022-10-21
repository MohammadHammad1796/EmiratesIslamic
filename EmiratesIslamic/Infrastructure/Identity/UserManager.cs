using EmiratesIslamic.Core.Helpers;
using EmiratesIslamic.Core.Models;
using EmiratesIslamic.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EmiratesIslamic.Infrastructure.Identity;

public class UserManager : IUserManager
{
    private readonly UserManager<ApplicationUser> _userManager;
    private ApplicationUser _currentUser;

    public UserManager(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }


    public async Task<Result> CreateAsync(User user)
    {
        var appUser = new ApplicationUser()
        {
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            FullName = user.FullName,
            UserRoles = new List<ApplicationUserRole>()
            {
                new()
                {
                    RoleId = user.RoleId
                }
            },
            UserName = user.Email,
            EmailConfirmed = true,
            ImagePath = user.ImagePath
        };

        var defaultPassword = user.Email;
        var identityResult = await _userManager.CreateAsync(appUser, defaultPassword);
        return new Result()
        {
            Succeeded = identityResult.Succeeded,
            Errors = identityResult.Errors.Select(e => new Error
            {
                Code = e.Code,
                Description = e.Description
            })
        };
    }

    public async Task<IEnumerable<User>> GetUsersWithRolesExceptAdminAsync()
    {
        var applicationUsers = await _userManager.Users
            .Where(u => !u.UserRoles
                .Any(ur => ur.Role.Name.ToLower().Equals("admin")))
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .ToListAsync();

        return applicationUsers.Select(au =>
            new User
            {
                FullName = au.FullName,
                Email = au.Email,
                ImagePath = au.ImagePath,
                Id = au.Id,
                PhoneNumber = au.PhoneNumber,
                Role = au.UserRoles.First().Role.Name,
                UserName = au.UserName
            }
        );
    }

    public async Task<User?> GetByIdWithRoleAsync(int id)
    {
        var user = await _userManager.Users.Where(u => u.Id == id)
            .Include(u => u.UserRoles)
            .SingleOrDefaultAsync();

        if (user == null)
            return null;

        _currentUser = user;
        return InitializaUserFormApplicationUser(user);
    }

    public async Task<Result> RemoveFromRoleAsync(string role)
    {
        var identityResult = await _userManager.RemoveFromRoleAsync(_currentUser, role);
        return InitializeResultFromIdentityResult(identityResult);
    }

    public async Task<Result> AddToRoleAsync(string role)
    {
        var identityResult = await _userManager.AddToRoleAsync(_currentUser, role);
        return InitializeResultFromIdentityResult(identityResult);
    }

    public async Task<User?> FindByEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            return null;

        _currentUser = user;
        return InitializaUserFormApplicationUser(user);
    }

    public async Task<User?> FindByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        if (user == null)
            return null;

        _currentUser = user;
        return InitializaUserFormApplicationUser(user);
    }

    public async Task<Result> DeleteAsync()
    {
        var identityResult = await _userManager.DeleteAsync(_currentUser);
        return InitializeResultFromIdentityResult(identityResult);
    }

    public async Task<string> GeneratePasswordResetTokenAsync()
    {
        return await _userManager.GeneratePasswordResetTokenAsync(_currentUser);
    }

    public async Task<Result> ResetPasswordAsync(string token, string newPassword)
    {
        var identityResult = await _userManager.ResetPasswordAsync(_currentUser, token, newPassword);
        return InitializeResultFromIdentityResult(identityResult);
    }

    public async Task<string> GenerateChangeEmailTokenAsync(string newEmail)
    {
        return await _userManager.GenerateChangeEmailTokenAsync(_currentUser, newEmail);
    }

    public async Task<Result> ChangeEmailAsync(string newEmail, string token)
    {
        var identityResult = await _userManager.ChangeEmailAsync(_currentUser, newEmail, token);
        return InitializeResultFromIdentityResult(identityResult);
    }

    public async Task UpdateNormalizedEmailAsync()
    {
        await _userManager.UpdateNormalizedEmailAsync(_currentUser);
    }

    public async Task<Result> SetUserNameAsync(string userName)
    {
        var identityResult = await _userManager.SetUserNameAsync(_currentUser, userName);
        return InitializeResultFromIdentityResult(identityResult);
    }

    public async Task UpdateNormalizedUserNameAsync()
    {
        await _userManager.UpdateNormalizedUserNameAsync(_currentUser);
    }

    public async Task<User> GetUserAsync(ClaimsPrincipal principal)
    {
        var applicationUser = await _userManager.GetUserAsync(principal);
        _currentUser = applicationUser;
        return InitializaUserFormApplicationUser(applicationUser);
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(_currentUser, password);
    }

    public async Task<Result> ChangePasswordAsync(string currentPassword, string newPassword)
    {
        var identityResult = await _userManager.ChangePasswordAsync(_currentUser,
            currentPassword, newPassword);
        return InitializeResultFromIdentityResult(identityResult);
    }

    public async Task<Result> UpdateAsync(User user)
    {
        _currentUser.FullName = user.FullName;
        _currentUser.PhoneNumber = user.PhoneNumber;
        _currentUser.Email = user.Email;
        _currentUser.Email = user.UserName;
        if (!string.IsNullOrWhiteSpace(user.ImagePath))
            _currentUser.ImagePath = user.ImagePath;

        var identityResult = await _userManager.UpdateAsync(_currentUser);
        return InitializeResultFromIdentityResult(identityResult);
    }

    private static User InitializaUserFormApplicationUser(ApplicationUser applicationUser)
    {
        var user = new User
        {
            FullName = applicationUser.FullName,
            Email = applicationUser.Email,
            ImagePath = applicationUser.ImagePath,
            Id = applicationUser.Id,
            PhoneNumber = applicationUser.PhoneNumber,
            UserName = applicationUser.UserName
        };
        if (applicationUser.UserRoles.FirstOrDefault() != null)
            user.RoleId = applicationUser.UserRoles.First().RoleId;

        return user;
    }

    private static Result InitializeResultFromIdentityResult(IdentityResult identityResult)
    {
        return new Result()
        {
            Succeeded = identityResult.Succeeded,
            Errors = identityResult.Errors.Select(e => new Error
            {
                Code = e.Code,
                Description = e.Description
            })
        };
    }
}