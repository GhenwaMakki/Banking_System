using Keycloak.Settings;

namespace Keycloak.Services.Abstraction;

public interface ITokenService
{
    public string GenerateJwt(JwtSettings jwtSettings, string[] permissions);
}