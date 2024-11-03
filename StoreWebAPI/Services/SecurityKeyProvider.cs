using Microsoft.IdentityModel.Tokens;

namespace StoreWebAPI.Services;

public class SecurityKeyProvider
{
    public required SymmetricSecurityKey Key { get; init; }
}
