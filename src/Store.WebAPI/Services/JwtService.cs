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

        TimeSpan expirationTime = _environment.Environment switch
        {
            DevelopmentEnvironment.Development => TimeSpan.FromHours(1),
            _ => TimeSpan.FromMinutes(5)
        };

        SecurityTokenDescriptor tokenDescriptor = new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(expirationTime),
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
