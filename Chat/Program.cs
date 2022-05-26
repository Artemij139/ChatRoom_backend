
using Chat.Hubs;
using Chat.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddSingleton<Manager>();
builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            
        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ClockSkew = TimeSpan.FromSeconds(5)
            };

            options.Authority = "https://localhost:10001";
            
            options.Events = new JwtBearerEvents
            {   
                
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];

                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) &&
                        (path.StartsWithSegments("/chatHub")))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });



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


app.MapControllers();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();



app.MapHub<chatHub>("/chatHub");

app.Run();

