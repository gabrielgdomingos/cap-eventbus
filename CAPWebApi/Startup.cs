using CAPWebApi.DbContexts;
using CAPWebApi.Receivers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

namespace CAPWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<AppDbContext>(x => x.UseSqlServer
            (
               "Data Source=localhost;" +
               "Initial Catalog=EventBus;" +
               "Persist Security Info=True;" +
               "user id=sa;" +
               "password=Admin@123;" +
               "MultipleActiveResultSets=True;" +
               "Connection Timeout=120;")
            );

            services.AddCap(x =>
            {
                x.UseEntityFramework<AppDbContext>();

                x.DefaultGroupName = $"cap.queue.{Assembly.GetExecutingAssembly().GetName().Name.ToLower()}";
                x.Version = "v1";
                x.FailedRetryInterval = 60;
                x.ConsumerThreadCount = 1;
                x.FailedRetryCount = 50;

                x.UseRabbitMQ(x =>
                {
                    x.HostName = "localhost";
                    x.VirtualHost = "CAP";
                    x.Port = 5672;
                    x.UserName = "admin";
                    x.Password = "admin";
                    x.ExchangeName = "cap.default.router";
                });
            });

            services.AddTransient<HelloWorldReceiver>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
