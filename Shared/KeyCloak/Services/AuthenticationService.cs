using Keycloak.Services.Abstraction;
using Keycloak.Settings;

namespace Keycloak.Services;

public class AuthenticationService(ITokenService tokenService, JwtSettings jwtSettings) : IAuthenticationService
{
    public string Login(string username, string password)
    {
        return tokenService.GenerateJwt(jwtSettings, [ "SuperAdmin"]);
    }
}