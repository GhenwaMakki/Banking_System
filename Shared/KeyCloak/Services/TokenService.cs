using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Keycloak.Services.Abstraction;
using Keycloak.Settings;
using Microsoft.IdentityModel.Tokens;

namespace Keycloak.Services;

public class TokenService : ITokenService
{
    public string GenerateJwt(JwtSettings jwtSettings, string[] permissions)
    {
        var keyBytes = Encoding.UTF8.GetBytes(jwtSettings.SigningKey);
        var symmetricKey = new SymmetricSecurityKey(keyBytes);
        var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("myclaim", "Some extra data")
        };

        var roleClaims = permissions.Select(x => new Claim("roles", x));
        claims.AddRange(roleClaims);

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddSeconds(jwtSettings.ExpirationSecond),
            signingCredentials: signingCredentials);

        var rawToken = new JwtSecurityTokenHandler().WriteToken(token);
        return rawToken;
    }

}