using Analytics.Data.Context;
using Analytics.Data.Implemetation;
using Analytics.Data.Interface;
using Analytics.Domain.Models.CommandHandlers.Logger;
using Analytics.Domain.Models.Commands.Logger;
using Analytics.Service.Implementaion;
using Analytics.Service.Implementaion.Analytics;
using Analytics.Service.Implementaion.ScheduleTask;
using Analytics.Service.Interface;
using Analytics.Service.Interface.Analytics;
using Analytics.Service.Interface.ScheduleTask;
using Analytics.Service.Scheduler.AdPointer;
using Analytics.Service.Scheduler.Facebook;
using Analytics.Service.Scheduler.Google;
using Analytics.Service.Scheduler.Instagram;
using Analytics.Service.Scheduler.LinkedIn;
using Analytics.Service.Scheduler.Twiter;
using Analytics.Service.Scheduler.Youtube;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Rabbit.Infrastructure.IoC.RegisterServices;

namespace Analytics.Microservice
{
    public class Startup
    {
        /// <summary>
        /// Represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {


          services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
                .AddFluentValidation(s =>
            {
                s.RegisterValidatorsFromAssemblyContaining<Startup>();

            }); ;
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Analytics.Api", Version = "v1" });
            });


            services.AddEntityFrameworkNpgsql().AddDbContext<AnalyticsDbContext>(opt =>
                opt.UseNpgsql(Configuration.GetConnectionString("DTWDbConnection")), ServiceLifetime.Transient, ServiceLifetime.Transient);

            services.AddSingleton<IHostedService, ScheduleTaskAdPointer>();
            services.AddSingleton<IHostedService, ScheduleTaskFacebook>();
            services.AddSingleton<IHostedService, ScheduleTaskInstagram>();
            services.AddSingleton<IHostedService, ScheduleTaskTwiter>();
            services.AddSingleton<IHostedService, ScheduleTaskLinkedIn>();
            services.AddSingleton<IHostedService, ScheduleTaskGoogle>();
            services.AddSingleton<IHostedService, ScheduleTaskYoutube>();
            
            services.AddMediatR(typeof(Startup));

            DependencyContainer.ConfigureServices(services);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Analytics.Api v1"));
            }

            app.UseRouting();
          
            // global cors policy
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
