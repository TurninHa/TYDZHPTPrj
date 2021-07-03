using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bul.System.Extension.NetCore
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddBulAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                var audience = configuration.GetSection("Authentication:Audience").Value;
                var issuer = configuration.GetSection("Authentication:Issuer").Value;
                var key = configuration.GetSection("Authentication:Security").Value;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证Issuer
                    ValidateAudience = true,//是否验证Audience
                    ValidateLifetime = true,//是否验证失效时间
                    ClockSkew = TimeSpan.FromSeconds(30),
                    ValidateIssuerSigningKey = true,//是否验证SecurityKey
                    ValidAudience = audience,//Audience
                    ValidIssuer = issuer,//Issuer，这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))//拿到SecurityKey
                };
            });

            return services;
        }

        public static IServiceCollection AddTurninCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(option =>
            {
                option.AddDefaultPolicy(corsPolicyBuider =>
                {
                    var origins = configuration.GetSection("Cors").Value;
                    corsPolicyBuider.AllowAnyMethod().AllowAnyHeader().WithOrigins(origins.Split(";"));
                    //corsPolicyBuider.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });

            return services;
        }
    }
}
