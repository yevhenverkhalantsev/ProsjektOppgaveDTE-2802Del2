using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProsjektOppgaveWebAPI.EntityFramework;
using ProsjektOppgaveWebAPI.EntityFramework.Repository;
using ProsjektOppgaveWebAPI.Hubs;
using ProsjektOppgaveWebAPI.Services.BlogServices;
using ProsjektOppgaveWebAPI.Services.CommentServices;
using ProsjektOppgaveWebAPI.Services.JwtServices;
using ProsjektOppgaveWebAPI.Services.JwtServices.Models;
using ProsjektOppgaveWebAPI.Services.PostServices;
using ProsjektOppgaveWebAPI.Services.TagServices;

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
services.AddSignalR();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Db Connection
services.AddDbContext<BlogDbContext>(options => 
    options.UseSqlite(builder.Configuration["ConnectionStrings:SqliteConnection"]));
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BlogDbContext>()
    .AddDefaultTokenProviders();

services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;

        var byteKey = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtOptions:Key").Value);

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
services.AddTransient<IBlogService, BlogService>();
services.AddTransient<ICommentService, CommentService>();
services.AddTransient<ITagService, TagService>();
services.AddTransient<IPostService, PostService>();

var app = builder.Build();

app.UseCors("AllowAllRequests");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<CommentHub>("/api/Comment/Create");

app.Run();
