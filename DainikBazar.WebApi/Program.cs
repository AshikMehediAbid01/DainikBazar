using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Application.Services.Implementations;
using DainikBazar.Application.Services.Interfaces;
using DainikBazar.Application.Mapping;
using DainikBazar.Infrastructure.Data;
using DainikBazar.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper( 
    typeof( ApplicationMappingProfile )
);
builder.Services.AddDbContext<AppDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString( "DefaultConnection" ))
);
builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartService, CartService>();




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

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var seeder = new ProductAndUserSeeder();
    await seeder.SeedProductsAsync(dbContext);
    await seeder.SeedUsersAsync( dbContext );
}

app.Run();
