using ClusterSalesManagementSystem.Models;
using ClusterSalesManagementSystem.Repository.Implement;
using ClusterSalesManagementSystem.Repository.Interface;
using ClusterSalesManagementSystem.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SalesDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("dbcs")));
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSession();
var key = builder.Configuration["Jwt:Key"];


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            context.HandleResponse();
            context.Response.Redirect("/User/Login");
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            context.Token = context.Request.Cookies["JWToken"];
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Sales}/{action=SalesList}/{id?}");

app.Run();
