using Store.WebAPI.Entities;

namespace Store.WebAPI.Services;

public interface IUserService
{
    ValueTask<User?> GetUser(string username);

    ValueTask<User?> CreateUser(string username, string password, string email, string firstName, string? middleName,
        string lastName);
}
