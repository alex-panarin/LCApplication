﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LC.Backend.Common.Auth
{
    public static class Extentions
    {
        public static void AddJwt(this IServiceCollection services, IConfiguration config)
        {
            var jwtConfig = new JwtConfig();
            config.GetSection("jwt").Bind(jwtConfig);
            services.AddSingleton(cfg => cfg.GetService<IOptions<JwtConfig>>().Value);
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidIssuer = jwtConfig.Issuer,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey))
                    };
                });
        }
    }
}
