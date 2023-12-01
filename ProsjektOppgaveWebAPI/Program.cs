using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProsjektOppgaveWebAPI.EntityFramework;
using ProsjektOppgaveWebAPI.Services.JwtServices;
using ProsjektOppgaveWebAPI.Services.JwtServices.Models;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddCors(options =>
{
    options.AddPolicy("AllowAllRequests", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(builder.Configuration["ConnectionStrings:SqliteConnection"]));

services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        byte[] byteKey = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions:Key").Value);

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            IssuerSigningKey = new SymmetricSecurityKey(byteKey),
            ValidateIssuerSigningKey = true
        };
    });

services.Configure<JwtOptions>(builder.Configuration.GetSection("JwtOptions"));

services.AddTransient<IJwtService, JwtService>();

var app = builder.Build();

app.UseCors("AllowAllRequests");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
