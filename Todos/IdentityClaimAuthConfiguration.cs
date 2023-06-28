using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Security.Claims;
using System.Text;
using Todos.Database;
using Todos.Database.Models;
using Todos.Options;

namespace Platform.Configuration;

public static class IdentityClaimAuthConfiguration
{
    public static void AddIdentityClaimAuth(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        Console.WriteLine("Setting up identity, claims and auth...");

        builder.Services.AddIdentity<AppUser, IdentityRole>(o =>
        {
            // Password settings.
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequireUppercase = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireDigit = false;

            // Lockout settings.
            o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            o.Lockout.MaxFailedAccessAttempts = 5;
            o.Lockout.AllowedForNewUsers = true;

            // User settings.
            o.User.RequireUniqueEmail = true;

        }).AddEntityFrameworkStores<TodoContext>()
          .AddDefaultTokenProviders();

        builder.Services.Configure<DataProtectionTokenProviderOptions>(o => o.TokenLifespan = TimeSpan.FromHours(3));

        var jwtOptions = new JwtOptions(configuration);
        var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret));

        builder.Services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = issuerSigningKey,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,

                NameClaimType = ClaimTypes.NameIdentifier,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero,
            };
        });
        //.AddFacebook(o =>
        //{
        //    var facebookOptions = new FacebookOptions(configuration);
        //    o.AppId = facebookOptions.AppId;
        //    o.AppSecret = facebookOptions.AppSecret;
        //    o.SignInScheme = IdentityConstants.ExternalScheme;
        //});

        Log.Information("Identity, claims and auth configured");
    }

    public static void UseIdentityClaimAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}
