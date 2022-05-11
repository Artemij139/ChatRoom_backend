

using AuthorizeIdentityServer.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<>
builder.Services.AddIdentityServer(
    options =>
    {
        options.UserInteraction.LoginUrl = "/Auth/Login";
    })
    .AddInMemoryClients(Configuration.GetClients() )
    .AddInMemoryApiResources(Configuration.GetApiResources())
    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
    .AddInMemoryApiScopes(Configuration.GetApiScopes())
    .AddDeveloperSigningCredential();

var app = builder.Build();


app.UseIdentityServer();
app.MapDefaultControllerRoute();

app.Run();
