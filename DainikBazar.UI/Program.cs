
using DainikBazar.UI.ApiServices.Implementations;
using DainikBazar.UI.ApiServices.Interfaces;


using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddHttpClient<IProductApiService, ProductApiService>();
builder.Services.AddHttpClient<IReviewApiService, ReviewApiService>();
//builder.Services.AddHttpClient<ProfileService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSession(); // Needed for Session.GetString("access_token")


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.LogoutPath = "/Account/Logout";  // Explicit logout path
})
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
{
    // Auth0 configuration
    options.Authority = builder.Configuration["Auth0:Domain"];
    options.ClientId = builder.Configuration["Auth0:ClientId"];
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];

    // OIDC settings
    options.ResponseType = "code";
    options.Scope.Clear();
    options.Scope.Add("openid");
    options.Scope.Add("profile");
    options.Scope.Add("email");
    options.SaveTokens = true;

    // Callback paths
    options.CallbackPath = new PathString("/callback");
    options.SignedOutCallbackPath = new PathString("/signout-callback");
    options.RemoteSignOutPath = new PathString("/logout");

    // Token validation
    options.TokenValidationParameters = new TokenValidationParameters
    {
        NameClaimType = "name",
        ValidateIssuer = true
    };

    // Event handlers
    options.Events = new OpenIdConnectEvents
    {
        // Store both access token and ID token in session
        OnTokenValidated = context =>
        {
            // Store both tokens in session
            var tokens = context.TokenEndpointResponse;
            if(tokens != null)
            {
                context.HttpContext.Session.SetString("access_token", tokens.AccessToken);
                context.HttpContext.Session.SetString("id_token", tokens.IdToken);
            }
            return Task.CompletedTask;
        },

        // Ensure the port matches your development server
        OnRedirectToIdentityProvider = context =>
        {
            context.ProtocolMessage.RedirectUri = "https://localhost:7168/callback";
            return Task.CompletedTask;
        },

        // Configure logout parameters
        OnRedirectToIdentityProviderForSignOut = context =>
        {
            // Get ID token from session
            var idToken = context.HttpContext.Session.GetString("id_token");

            if(!string.IsNullOrEmpty(idToken))
            {
                context.ProtocolMessage.IdTokenHint = idToken;
                context.ProtocolMessage.PostLogoutRedirectUri = "https://localhost:7168/";
            }

            return Task.CompletedTask;
        }
    };
});

builder.Services.AddHttpClient("ApiClient", client => {
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]);
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
