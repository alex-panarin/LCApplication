using LC.Backend.Common.DB;
using LC.Backend.Common.Events;
using LC.Backend.Common.Logging;
using LC.Backend.Common.MessageBus;
using LC.Backend.Common.MessageBus.RawRabbit;
using LC.Services.Logging.Entities;
using LC.Services.Logging.Handlers;
using LC.Services.Logging.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LC.Services.Logging
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
            services.AddLogging();
            services.AddControllers();
            services.AddSingleton<IEventHandler<LogEvent>, LogEventHandler>();
            services.AddSingleton<IDbContext<LogRow>, LogDbContext>();
            services.AddSingleton<ILogRepository, LogRepositiry>();
            services.AddMongoDb(Configuration);
            services.AddRabbitMq(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.ApplicationServices.GetService<IDbInitializer>().InitializeAsync();
            app.ApplicationServices.SubscribeToEvent<LogEvent>();
        }
    }
}
