namespace DainikBazar.Domain.Settings;

public class Auth0Settings
{
    public string Domain { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Audience { get; set; }
    public string ManagementApiAudience { get; set; }
    public string ManagementApiBaseUrl { get; set; }
}
