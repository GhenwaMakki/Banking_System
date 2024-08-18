namespace Keycloak.Settings;

public class AuthenticationSettings

{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigningKey { get; set; }
    
    public string Authority { get; set; }
    
}