

using AuthorizeIdentityServer.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddIdentityServer()
    .AddInMemoryClients(Configuration.GetClients() )
    .AddInMemoryApiResources(Configuration.GetApiResources())
    .AddInMemoryIdentityResources(Configuration.GetIdentityResources())
    .AddDeveloperSigningCredential();

var app = builder.Build();


app.UseIdentityServer();
app.MapControllers();

app.Run();
