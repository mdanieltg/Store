using StoreWebAPI.Entities;

namespace StoreWebAPI.Services;

public interface IAuthenticationService
{
    ValueTask<User?> Authenticate(string username, string password);
}
