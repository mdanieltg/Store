using Store.WebAPI.Entities;

namespace Store.WebAPI.Services;

public interface IAuthenticationService
{
    ValueTask<User?> Authenticate(string username, string password);
}
