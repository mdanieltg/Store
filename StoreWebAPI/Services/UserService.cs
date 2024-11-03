using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using StoreWebAPI.DataAccess;
using StoreWebAPI.Entities;
using StoreWebAPI.Exceptions;
using StoreWebAPI.Util;

namespace StoreWebAPI.Services;

public class UserService : IUserService
{
    private readonly StoreDbContext _dbContext;
    private readonly ILogger<UserService> _logger;

    public UserService(StoreDbContext dbContext, ILogger<UserService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async ValueTask<User?> GetUser(string username) =>
        await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == username);

    public async ValueTask<User> CreateUser(string username, string password, string role, string email,
        string firstName, string? middleName, string lastName)
    {
        byte[] hashedPassword = Security.ComputeHash(password, 32, out byte[] salt);

        UserRole userRole = await _dbContext.UserRoles.FirstOrDefaultAsync(r => r.Name == role)
                            ?? throw new InvalidUserRoleException(role);

        User user = new()
        {
            Username = username,
            Password = hashedPassword,
            Salt = salt,
            Email = email,
            CreationDate = DateTimeOffset.Now,
            LastLoginDate = DateTimeOffset.MinValue,
            RoleId = userRole.Id
        };
        Customer customer = new()
        {
            FirstName = firstName,
            MiddleName = middleName,
            LastName = lastName,
            User = user
        };

        using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            await _dbContext.AddAsync(user);
            await _dbContext.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return user;
        }
        catch (DbUpdateException e)
        {
            await transaction.RollbackAsync();
            _logger.LogError("An error has occured when updating the database, the changes were rolled back");
            _logger.LogError(e.Message);
            throw new UserNotCreatedExcpetion(e);
        }
    }
}
