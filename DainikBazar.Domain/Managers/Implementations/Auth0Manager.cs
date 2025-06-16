using DainikBazar.Domain.Managers.Interfaces;
using DainikBazar.Domain.Models;
using DainikBazar.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace DainikBazar.Domain.Managers.Implementations;

public class Auth0Manager : IAuth0Manager
{
    private readonly Auth0Settings _settings;
    private readonly HttpClient _httpClient;

    public Auth0Manager(
        IOptions<Auth0Settings> settings,
        IHttpClientFactory httpClientFactory)
    {
        _settings = settings.Value;
        _httpClient = httpClientFactory.CreateClient("Auth0Management");
    }

    public async Task<bool> ChangePassword(string userId, string newPassword)
    {
        var token = await GetManagementApiToken();

        var request = new
        {
            password = newPassword,
            connection = "Username-Password-Authentication"
        };

        var response = await _httpClient.PatchAsJsonAsync(
            $"api/v2/users/{Uri.EscapeDataString(userId)}",
            request);

        return response.IsSuccessStatusCode;
    }

    public async Task<User> GetUserInfo(string userId)
    {
        var token = await GetManagementApiToken();

        var request = new HttpRequestMessage(HttpMethod.Get,
            $"api/v2/users/{Uri.EscapeDataString(userId)}");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if(!response.IsSuccessStatusCode)
        {
            return null;
        }

        var content = await response.Content.ReadFromJsonAsync<JsonElement>();

        return new User
        {
            Id = userId,
            Email = content.GetProperty("email").GetString(),
            Name = content.GetProperty("name").GetString(),
        };
    }


    private async Task<string> GetManagementApiToken()
    {
        var request = new
        {
            client_id = _settings.ClientId,
            client_secret = _settings.ClientSecret,
            audience = _settings.ManagementApiAudience,
            grant_type = "client_credentials"
        };

        var response = await _httpClient.PostAsJsonAsync("oauth/token", request);
        var content = await response.Content.ReadFromJsonAsync<JsonElement>();
        return content.GetProperty("access_token").GetString();
    }
}
