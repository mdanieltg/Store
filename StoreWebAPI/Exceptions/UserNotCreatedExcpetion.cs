namespace StoreWebAPI.Exceptions;

public class UserNotCreatedExcpetion : Exception
{
    public UserNotCreatedExcpetion(Exception dbUpdateException)
        : base("The user was not created due to an error. See the inner exception for more details.", dbUpdateException)
    {
    }
}
