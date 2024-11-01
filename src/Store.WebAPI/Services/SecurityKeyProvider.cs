using Microsoft.IdentityModel.Tokens;

namespace Store.WebAPI.Services;

public class SecurityKeyProvider
{
    public required SymmetricSecurityKey Key { get; init; }
}
