using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Store.WebAPI.Entities;
using Store.WebAPI.Util;

namespace Store.WebAPI.Services;

public class JwtService
{
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();
    private readonly ApplicationEnvironment _environment;

    public JwtService(SecurityKeyProvider securityKeyProvider, ApplicationEnvironment environment)
    {
        _signingCredentials = new SigningCredentials(securityKeyProvider.Key, SecurityAlgorithms.HmacSha256);
        _environment = environment;
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(5),
            SigningCredentials = _signingCredentials
        };

        return _tokenHandler.CreateEncodedJwt(tokenDescriptor);
    }

    public DateTime GetExpirationDate(string token)
    {
        SecurityToken securityToken = _tokenHandler.ReadJwtToken(token);
        return securityToken.ValidTo;
    }
}
