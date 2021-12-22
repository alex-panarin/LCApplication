using Microsoft.Extensions.Configuration;
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
            var section = config.GetSection("jwt");
            section.Bind(jwtConfig);
            services.Configure<JwtConfig>(section);
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
