using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Analytics.Service.Interface;
using Analytics.Service.Interface.ScheduleTask;
using Microsoft.Extensions.DependencyInjection;

namespace Analytics.Service.Scheduler.AdPointer
{
    public class ScheduleTaskAdPointer : ScheduleProcessorAdPointer
    {
        #region Fields

        private readonly IScheduleTaskService _scheduleTaskService;
        protected override string Schedule => "30 18 * * *";

        #endregion

        #region Ctor

        public ScheduleTaskAdPointer(IServiceScopeFactory serviceScopeFactory, IScheduleTaskService scheduleTaskService) : base(serviceScopeFactory)
        {
            this._scheduleTaskService = scheduleTaskService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ProcessInScopeFacebook
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override Task ProcessInScopeAdPointer(IServiceProvider serviceProvider)
        {
            this._scheduleTaskService.ProcessingAdPointer();
            return Task.CompletedTask;
        }


        #endregion
    }
}