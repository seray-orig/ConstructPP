using GMServerWebPanel.API.Data;
using GMServerWebPanel.API.Program;
using GMServerWebPanel.API.Services;
using GMServerWebPanel.API.Services.Interfaces;
using GMServerWebPanel.API.Settings;
using GMServerWebPanel.API.Shared;
using Microsoft.EntityFrameworkCore;
using ProtoBuf.Grpc.ClientFactory;

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
builder.Services.AddHostedService<LogStreamerService>();

builder.Services.AddCodeFirstGrpcClient<IServerProcessController>(options =>
{
    options.Address = new Uri(config["ServerUrls"] ?? "http://localhost:50051");
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<LogHub>("/hub/logs");

CreateFirstAdminUserDB.Create(app);

app.Run();
