using Store.WebAPI.Entities;
using Store.WebAPI.Util;

namespace Store.WebAPI.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserService _userService;

    public AuthenticationService(IUserService userService)
    {
        _userService = userService;
    }

    public async ValueTask<User?> Authenticate(string username, string password)
    {
        User? user = await _userService.GetUser(username);
        if (user is null) return null;

        bool passwordsMatch = Security.CompareHashes(password, user.Password, user.Salt);

        return passwordsMatch
            ? user
            : null;
    }
}
