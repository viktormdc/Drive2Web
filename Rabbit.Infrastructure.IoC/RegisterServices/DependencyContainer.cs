using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Administration.Data.Context;
using Administration.Data.Domain.Logging;
using Administration.Data.Implementation;
using Administration.Data.Interface;
using Administration.Domain.Models.EventHandlers;
using Administration.Domain.Models.Events;
using Administration.Service.Implementation;
using Administration.Service.Implementation.Logging;
using Administration.Service.Interfaice.Admin;
using Administration.Service.Interfaice.Logging;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Rabbit.Domain.Core.Bus;
using Rabbit.Infrastructure.Bus.RabbitMQBus;
using Analytics.Data.Context;
using Analytics.Data.Implemetation;
using Analytics.Data.Interface;
using Analytics.Domain.Models.CommandHandlers.Logger;
using Analytics.Domain.Models.Commands.Logger;
using Analytics.Service.Implementaion.Analytics;
using Analytics.Service.Implementaion.ScheduleTask;
using Analytics.Service.Interface.Analytics;
using Analytics.Service.Interface.ScheduleTask;
using MediatR.Pipeline;

namespace Rabbit.Infrastructure.IoC.RegisterServices
{
    public static class DependencyContainer
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            // Rabbit Infrastructure Bus
            services.AddSingleton<IEventBus, RabbitMQBus>(serviceProvider =>
            {
                var mediator = serviceProvider.GetService<IMediator>();
                var serviceScopeFactory = serviceProvider.GetService<IServiceScopeFactory>();
                return new RabbitMQBus(mediator, serviceScopeFactory);
            });

            // Administration Service
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IAdminService, AdminService>();
            services.AddTransient<ILogger, Logger>();

            //Analytics Service
            services.AddTransient(typeof(IAnalyticsRepository<>), typeof(AnalyticsRepository<>));
            services.AddTransient<IScheduleTaskService, ScheduleTaskService>();
            services.AddTransient<IAnalyticsService, AnalyticsService>();
            
            //Commands
            services.AddTransient<IRequestHandler<CreateLoggerCommand, bool>, CreateLoggerCommandHandler>();

            //Events
            services.AddTransient<IEventHandler<CreateLoggerCreatedEvent>, CreateLoggerCreatedEventHandler>();

            // Event Handlers.
            services.AddTransient<CreateLoggerCreatedEventHandler>();

            //Database Context
            services.AddTransient<AdminDbContext>();
            services.AddTransient<AnalyticsDbContext>();
        }
    }
}
