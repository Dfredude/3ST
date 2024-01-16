using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FinalProject.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using HaulMaster.Services;
using HaulMaster.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<FinalProjectContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FinalProjectContext") ?? throw new InvalidOperationException("Connection string 'FinalProjectContext' not found.")));

// Add cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.LoginPath = "/Admin/Login";
        options.LogoutPath = "/Admin/Logout";
        options.AccessDeniedPath = "/Products/AccessDenied";
    });

builder.Services.AddHttpClient();
builder.Services.AddScoped<IDriverService, DriverService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapGet("/api/Drivers", (IDriverService driverService) => Results.Ok(driverService.GetDrivers()));

app.MapPost("/api/Drivers", (IDriverService driverService, Driver driver) =>
{
    driverService.AddDriver(driver);
    return Results.Ok();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
