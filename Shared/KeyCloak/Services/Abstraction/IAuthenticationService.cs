namespace Keycloak.Services.Abstraction;

public interface IAuthenticationService
{
    public string Login(string username, string password);
}