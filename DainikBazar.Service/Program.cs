using DainikBazar.Service.Mapping;
using DainikBazar.Storage.Extensions;
using DainikBazar.Domain.Extensions;
using DainikBazar.Storage.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole().SetMinimumLevel( LogLevel.Information );

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();


// Registration of Managers for DI in Application
builder.Services.ManagerRegistration();

// Registration of Repositories for DI in Application
builder.Services.RepositoryRegistration(builder.Configuration);

builder.Services.AddAutoMapper( 
    typeof( StorageMappingProfile ),
    typeof( ServiceMappingProfile)
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//Seed Data using Storage extension method
await app.Services.SeedStorageDataAsync();

app.Run();
