using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Store.WebAPI.Entities;

namespace Store.WebAPI.Services;

public class JwtService
{
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _tokenHandler = new();

    public JwtService(SecurityKeyProvider securityKeyProvider)
    {
        _signingCredentials = new SigningCredentials(securityKeyProvider.Key, SecurityAlgorithms.HmacSha256);
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
