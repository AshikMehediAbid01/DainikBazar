//using System.Net.Http.Headers;

//namespace DainikBazar.UI.ApiServices.Implementations;

//public class ProfileService(IHttpClientFactory factory, IHttpContextAccessor httpContextAccessor)
//{
//    public async Task<string> GetUserProfileAsync()
//    {
//        var token = httpContextAccessor.HttpContext?.Session.GetString("access_token");

//        var client = factory.CreateClient();
//        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

//        var response = await client.GetAsync("https://localhost:5001/api/secure/profile");

//        return await response.Content.ReadAsStringAsync();
//    }
//}