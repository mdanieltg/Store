namespace Store.WebAPI.Exceptions;

public class InvalidUserRoleException : Exception
{
    public InvalidUserRoleException(string role) : base($"The role '{role}' is invalid.")
    {
        Role = role;
    }

    public string Role { get; init; }
}
