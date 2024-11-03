using Store.WebAPI.Entities;

namespace Store.WebAPI.Util;

public static class Constants
{
    public static class Roles
    {
        public const string Admin = nameof(UserRoles.Administrator);
        public const string Seller = nameof(UserRoles.Seller);
        public const string Customer = nameof(UserRoles.Customer);
    }
}
