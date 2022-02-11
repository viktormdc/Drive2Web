using Analytics.Service.Interface.ScheduleTask;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Analytics.Service.Scheduler.LinkedIn
{
    public class ScheduleTaskLinkedIn : ScheduleProcessorLinkedIn
    {
        #region Fields

        private readonly IScheduleTaskService _scheduleTaskService;
        protected override string Schedule => "10 18 * * *";

        #endregion

        #region Ctor

        public ScheduleTaskLinkedIn(IServiceScopeFactory serviceScopeFactory, IScheduleTaskService scheduleTaskService) : base(serviceScopeFactory)
        {
            this._scheduleTaskService = scheduleTaskService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ProcessInScopeLinkedin
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override Task ProcessInScopeLinkedIn(IServiceProvider serviceProvider)
        {
            this._scheduleTaskService.ProcessingLinkedInApi();
            return Task.CompletedTask;
        }

        #endregion
    }
}
