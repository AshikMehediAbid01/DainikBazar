using Auth0.AspNetCore.Authentication;
using DainikBazar.UI.Models;
using Microsoft.AspNetCore.Authentication;

//using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace DainikBazar.UI.Controllers;

public class AccountController(IHttpClientFactory _httpClientFactory, IConfiguration _configuration) : Controller
{
    [HttpGet]
    public async Task Login(string returnUrl = "/")
    {
        //var props = new LoginAuthenticationPropertiesBuilder()
        //    .WithRedirectUri(returnUrl)
        //    .WithAudience(_configuration["Auth0:Audience"]) // For API access
        //    .Build();
        var props = new AuthenticationProperties
        {
            RedirectUri = returnUrl,
            Items =
            {
                { "audience", _configuration["Auth0:Audience"] }
            }
        };

        await HttpContext.ChallengeAsync(OpenIdConnectDefaults.AuthenticationScheme, props);
    }

    //[Authorize]
    public async Task Logout()
    {
        var idToken = await HttpContext.GetTokenAsync("id_token");
        var props = new AuthenticationProperties
        {
            RedirectUri = "/",
            Items = { { "id_token", idToken } }
        };
        HttpContext.Session.Clear();
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme, props);
    }
    [HttpGet("~/signout-callback")]
    public IActionResult SignoutCallback()
    {
        return RedirectToAction("Index", "Home");
    }
    //[Authorize]
    public IActionResult ChangePassword()
    {
        return View();
    }

    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var token = await HttpContext.GetTokenAsync("access_token");
        var client = _httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var response = await client.PostAsJsonAsync("api/auth/change-password", new {
            model.NewPassword
        });

        if (response.IsSuccessStatusCode)
        {
            return RedirectToAction("PasswordChanged");
        }

        ModelState.AddModelError("", "Password change failed");
        return View(model);
    }
}
