using System;
using System.Threading.Tasks;
using Analytics.Service.Interface.ScheduleTask;
using Microsoft.Extensions.DependencyInjection;

namespace Analytics.Service.Scheduler.Instagram
{
    public class ScheduleTaskInstagram : ScheduleProcessorInstagram
    {
        #region Fields

        private readonly IScheduleTaskService _scheduleTaskService;
        protected override string Schedule => "10 18 * * *";

        #endregion

        #region Ctor

        public ScheduleTaskInstagram(IServiceScopeFactory serviceScopeFactory, IScheduleTaskService scheduleTaskService) : base(serviceScopeFactory)
        {
            this._scheduleTaskService = scheduleTaskService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ProcessInScopeInstagram
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override Task ProcessInScopeInstagram(IServiceProvider serviceProvider)
        {
            this._scheduleTaskService.ProcessingInstagramApi();
            return Task.CompletedTask;
        }



        #endregion
    }
}