using DainikBazar.Domain.Extensions;
using DainikBazar.Service.Mapping;
using DainikBazar.Storage.Extensions;
using DainikBazar.Storage.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole().SetMinimumLevel( LogLevel.Information );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddEndpointsApiExplorer();


// Registration of Managers for DI in Application
builder.Services.ManagerRegistration(builder.Configuration);

// Registration of Repositories for DI in Application
builder.Services.RepositoryRegistration(builder.Configuration);

builder.Services.AddAutoMapper( 
    typeof( StorageMappingProfile ),
    typeof( ServiceMappingProfile)
);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var domain = builder.Configuration["Auth0:Domain"];
        options.Authority = $"https://{domain}/";
        options.Audience = builder.Configuration["Auth0:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Auth0:Audience"],
            ValidateIssuer = true,
            ValidIssuer = $"https://{domain}/",
            ValidateLifetime = true
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Frontend", policy =>
    {
        policy.WithOrigins(builder.Configuration["FrontendBaseUrl"])
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("Frontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

//Seed Data using Storage extension method
await app.Services.SeedStorageDataAsync();

app.Run();
