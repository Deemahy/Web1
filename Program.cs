using Microsoft.EntityFrameworkCore;
using Web1.Models;
using System;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add database connection
string connectionString = builder.Configuration.GetConnectionString("AzureSqlDbConnection");
builder.Services.AddDbContext<Gp2Context>(options => options.UseSqlServer(connectionString));

// Configure session
builder.Services.AddSession(o => { o.IdleTimeout = TimeSpan.FromMinutes(120); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
