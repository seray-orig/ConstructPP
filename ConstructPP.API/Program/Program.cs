using ConstructPP.API.Data;
using ConstructPP.API.Program;
using ConstructPP.API.Services;
using ConstructPP.API.Services.Interfaces;
using ConstructPP.API.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

TokenJWTShit.GenerateRandomJWTKeyIfConfigIsEmpty(config);

TokenJWTShit.SetTokenParameters(builder, config);

builder.Services.AddControllers();

builder.Services.Configure<JwtSettings>(config.GetSection("JWTSettings"));
builder.Services.Configure<Argon2Settings>(config.GetSection("Argon2Settings"));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite("Data Source=users.db"));
builder.Services.AddScoped<ITokenServise, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPasswordHasher, Argon2Hasher>();
builder.Services.AddSignalR();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<LogHub>("/hub/logs");
app.MapFallbackToFile("index.html");

CreateFirstAdminUserDB.Create(app);

app.Run();
