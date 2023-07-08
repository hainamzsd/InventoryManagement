using Microsoft.EntityFrameworkCore;
using project.Business;
using project.Models;
using project.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddRazorPages();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<NorthwindContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddTransient<IRepository<Product>, Repository<Product>>();
builder.Services.AddTransient<ProductManager>();

builder.Services.AddSession(options =>
{
    options.Cookie.Name = "DefaultCookie";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
