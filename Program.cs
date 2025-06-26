using Microsoft.EntityFrameworkCore;
using RailwayTicketSystem.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

// Register RailwayDbContext using SQL Server
builder.Services.AddDbContext<RailwayDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Enable sessions (used for login)
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // Enable static files like CSS, JS, Bootstrap

app.UseRouting();
app.UseSession();     // Enable session
app.UseAuthorization();

// Set default route to Account/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
