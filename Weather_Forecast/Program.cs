using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Weather_Forcast.Repository.Repositories.Users;
using Weather_Forecast.Application.Command;
using Weather_Forecast.Infrastructure;
using Weather_Forecast.Middleware;
using Weather_Forecast.Repository;
using Weather_Forecast.Repository.Abstractions.Users;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("48d9e5e6-347a-4633-adad-e7043a4d40fe"))
    };
});


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(SignInUser)));


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<WeatherDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllersWithViews();
builder.Services.AddInfrastructureExtensions();

builder.Services.AddCors(options =>
{
    options.AddPolicy("app", policy => { policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin(); });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



//Custom auth middleware
//app.UseMiddleware<JwtAuthenticationMiddleware>();
app.UseCors("app");
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<JwtAuthenticationMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


//app.UseCors("app");


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
