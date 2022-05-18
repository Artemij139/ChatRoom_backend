using AuthorizeIdentityServer.Configuration;
using AuthorizeIdentityServer.Data;
using AuthorizeIdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(config =>
{
    config.UseSqlServer(builder.Configuration.GetConnectionString("default"));
})
    .AddIdentity<AppUser, AppRole>(config =>
    {
        config.Password.RequireDigit = false;
        config.Password.RequireLowercase = false;
        config.Password.RequireNonAlphanumeric = false;
        config.Password.RequireUppercase = false;
        config.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddIdentityServer(
    options =>
    {
        options.UserInteraction.LoginUrl = "/Auth/Login";
    })
    .AddAspNetIdentity<AppUser>()
    .AddInMemoryClients(Configuration.GetClients() )
    .AddInMemoryApiResources(Configuration.GetApiResources())
    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
    .AddInMemoryApiScopes(Configuration.GetApiScopes())
    .AddDeveloperSigningCredential();

var app = builder.Build();

app.UseStaticFiles();
app.UseIdentityServer();
app.MapDefaultControllerRoute();

app.Run();
