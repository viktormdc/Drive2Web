using System;
using System.Threading.Tasks;
using Analytics.Service.Interface;
using Analytics.Service.Interface.ScheduleTask;
using Castle.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Analytics.Service.Scheduler.Twiter
{
    public class ScheduleTaskTwiter : ScheduleProcessorTwiter
    {
        #region Fields

        private readonly IScheduleTaskService _scheduleTaskService;
        protected override string Schedule => "10 18 * * *";

        #endregion

        #region Ctor

        public ScheduleTaskTwiter(IServiceScopeFactory serviceScopeFactory, IScheduleTaskService scheduleTaskService) : base(serviceScopeFactory)
        {
            this._scheduleTaskService = scheduleTaskService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ProcessInScopeTwitter
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override Task ProcessInScopeTwiter(IServiceProvider serviceProvider)
        {
            this._scheduleTaskService.ProcessingTwiterApi();
            return Task.CompletedTask;
        }



        #endregion
    }
}