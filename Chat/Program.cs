
using Chat.Hubs;
using Chat.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<Manager>();

builder.Services.AddCors(options =>
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();
    })
);

var app = builder.Build();

app.UseRouting();

app.UseCors();

app.MapHub<CommHub>("/chat");

app.Run();

