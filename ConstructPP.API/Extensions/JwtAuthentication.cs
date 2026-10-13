using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ConstructPP.API.Extensions
{
    public static class JwtAuthentication
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            GenerateRandomJWTKeyIfConfigIsEmpty(config);

            SetTokenParameters(services, config);
        }

        private static void GenerateRandomJWTKeyIfConfigIsEmpty(IConfiguration config)
        {
            if (string.IsNullOrEmpty(config["JWTSettings:Key"]))
            {
                // Каждый раз генерит новый, ибо нехуй. Пусть свой придумывают и записывают в appsettings.json
                config["JWTSettings:Key"] = $"{Guid.NewGuid().ToString()}_{Guid.NewGuid().ToString()}";
            }
        }

        private static void SetTokenParameters(IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = config["JWTSettings:Issuer"],
                    ValidAudience = config["JWTSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:Key"]!)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization();
        }
    }
}
