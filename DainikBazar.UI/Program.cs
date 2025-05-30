
using DainikBazar.UI.ApiServices.Implementations;
using DainikBazar.UI.ApiServices.Interfaces;


using DainikBazar.UI.Controllers;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddHttpClient<CartController>( client =>
//{
//    client.BaseAddress = new Uri( "https://localhost:7155/api/" );
//} );

builder.Services.AddHttpClient<IProductApiService, ProductApiService>();
builder.Services.AddHttpClient<IReviewApiService, ReviewApiService>();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
