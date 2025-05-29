using DainikBazar.Application.Common.Repository_Interfaces;
using DainikBazar.Application.Mapping;
using Microsoft.EntityFrameworkCore;
using DainikBazar.Application.Managers.Interfaces;
using DainikBazar.Application.Managers.Implementations;
using DainikBazar.Repository.Data;
using DainikBazar.Repository.Repositories;

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
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewManager, ReviewManager>();

builder.Services.AddAutoMapper( 
    typeof( ApplicationMappingProfile )
);
builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<ICartManager, CartManager>();




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
