using System.Security.Claims;
using System.Threading.Tasks;

namespace EmiratesIslamic.Core.Services;

public interface ISignInManager
{
    Task<bool> PasswordSignInAsync(string userName, string password,
        bool isPersistent, bool lockoutOnFailure);

    Task SignOutAsync();

    Task SignInAsync(string email, bool isPersistent);

    bool IsSignedIn(ClaimsPrincipal principal);
}