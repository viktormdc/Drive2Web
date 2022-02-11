using Analytics.Service.Interface.ScheduleTask;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Analytics.Service.Scheduler.Google
{
    public class ScheduleTaskGoogle : ScheduleProcessorGoogle
    {
        #region Fields

        private readonly IScheduleTaskService _scheduleTaskService;
        protected override string Schedule => "10 18 * * *";

        #endregion

        #region Ctor

        public ScheduleTaskGoogle(IServiceScopeFactory serviceScopeFactory, IScheduleTaskService scheduleTaskService) : base(serviceScopeFactory)
        {
            this._scheduleTaskService = scheduleTaskService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ProcessInScopeGoogle
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override Task ProcessInScopeGoogle(IServiceProvider serviceProvider)
        {

            this._scheduleTaskService.ProcessingGoogleApi();
            return Task.CompletedTask;
        }

        #endregion
    }
}
