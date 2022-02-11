using Analytics.Service.Interface.ScheduleTask;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Analytics.Service.Scheduler.Youtube
{
    public class ScheduleTaskYoutube : ScheduleProcessorYoutube
    {
        #region Fields

        private readonly IScheduleTaskService _scheduleTaskService;
        protected override string Schedule => "10 18 * * *";

        #endregion

        #region Ctor

        public ScheduleTaskYoutube(IServiceScopeFactory serviceScopeFactory, IScheduleTaskService scheduleTaskService) : base(serviceScopeFactory)
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
        public override Task ProcessInScopeYoutube(IServiceProvider serviceProvider)
        {

            this._scheduleTaskService.ProcessingYoutubeApi();
            return Task.CompletedTask;
        }

        #endregion
    }
}
