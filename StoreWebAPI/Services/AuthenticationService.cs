using StoreWebAPI.Entities;
using StoreWebAPI.Util;

namespace StoreWebAPI.Services;

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
