using System;
using System.Threading.Tasks;
using Analytics.Service.Interface.ScheduleTask;
using Microsoft.Extensions.DependencyInjection;

namespace Analytics.Service.Scheduler.Facebook
{
    public class ScheduleTaskFacebook : ScheduleProcessorFacebook
    {
        #region Fields

        private readonly IScheduleTaskService _scheduleTaskService;
        protected override string Schedule => "10 18 * * *";

        #endregion

        #region Ctor

        public ScheduleTaskFacebook(IServiceScopeFactory serviceScopeFactory, IScheduleTaskService scheduleTaskService) : base(serviceScopeFactory)
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
        public override Task ProcessInScopeFacebook(IServiceProvider serviceProvider)
        {

           this._scheduleTaskService.ProcessingFacebookApi();
            return Task.CompletedTask;
        }

        

        #endregion
    }
}