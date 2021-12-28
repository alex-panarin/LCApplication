using LC.Backend.Common.Auth;
using LC.Backend.Common.Commands;
using LC.Backend.Common.Commands.Models;
using LC.Backend.Common.DB;
using LC.Backend.Common.Events;
using LC.Backend.Common.Events.Models;
using LC.Backend.Common.MessageBus;
using LC.Backend.Common.MessageBus.RawRabbit;
using LC.Services.Identity.Handlers;
using LC.Services.Identity.Logging;
using LC.Services.Identity.Repositories;
using LC.Services.Identity.Repositories.Encrypter;
using LC.Services.Identity.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LC.Services.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddJwt(Configuration);
            services.AddMongoDb(Configuration);
            services.AddRabbitMq(Configuration);
            services.AddMqLogging();
            services.AddScoped<UserDbContext>();
            services.AddScoped<ICommandHandler<CreateUser>, CreateUserHandler>();
            services.AddScoped<IEventHandler<AuthenticateRequest>, AuthenticateHandler>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordEncrypter, PasswordEcnrypter>();
            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<IdentityService>();
                
            });
            app.ApplicationServices.GetService<IDbInitializer>().InitializeAsync();
            app.SubscribeToCommand<CreateUser>();
            app.SubscribeToEvent<AuthenticateRequest>();
        }
    }
}
