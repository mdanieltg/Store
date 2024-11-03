using StoreWebAPI.Entities;

namespace StoreWebAPI.Services;

public interface IUserService
{
    ValueTask<User?> GetUser(string username);

    ValueTask<User> CreateUser(string username, string password, string role, string email, string firstName,
        string? middleName, string lastName);
}
