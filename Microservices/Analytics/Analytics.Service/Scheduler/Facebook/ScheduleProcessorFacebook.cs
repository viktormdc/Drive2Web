using System;
using System.Threading;
using System.Threading.Tasks;
using Analytics.Service.HostedService;
using Microsoft.Extensions.DependencyInjection;
using NCrontab;

namespace Analytics.Service.Scheduler.Facebook
{
    public abstract class ScheduleProcessorFacebook : FacebookHostedService
    {
        #region Fields

        private CrontabSchedule _schedule;
        private DateTime _nextRun;
        protected abstract string Schedule { get; }
        #endregion

        #region Ctor

        public ScheduleProcessorFacebook(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory)
        {
            _schedule = CrontabSchedule.Parse(Schedule);
            _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            do
            {
                var now = DateTime.Now;
                var nextrun = _schedule.GetNextOccurrence(now);
                if (now > _nextRun)
                {
                    await ProcessFacebook();
                    _nextRun = _schedule.GetNextOccurrence(DateTime.Now);
                }
                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        #endregion
    }
}