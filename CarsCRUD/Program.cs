using CarsCRUD.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<CarsContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Cars}/{action=Index}/{id?}");

app.Run();
