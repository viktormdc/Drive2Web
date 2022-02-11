using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Analytics.Service.HostedService
{
    public abstract class FacebookHostedService : IHostedService
    {
        #region Fields

        private readonly IServiceScopeFactory _serviceScopeFactory;

        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts =
            new CancellationTokenSource();

        #endregion

        #region Ctor
        public FacebookHostedService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// StartAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            // Store the task we're executing
            _executingTask = ExecuteAsync(_stoppingCts.Token);
            // If the task is completed then return it,
            // this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }
            // Otherwise it's running
            return Task.CompletedTask;
        }

        /// <summary>
        /// StopAsync
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                    cancellationToken));
            }
        }

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected virtual async Task ExecuteAsync(CancellationToken stoppingToken)
        {
         
            do
            {
                await ProcessFacebook();

                await Task.Delay(5000, stoppingToken); //5 seconds delay
            }
            while (!stoppingToken.IsCancellationRequested);
        }

        /// <summary>
        /// ProcessFacebook
        /// </summary>
        /// <returns></returns>
        protected async Task ProcessFacebook()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            { 
                await ProcessInScopeFacebook(scope.ServiceProvider);
            }
        }

        /// <summary>
        /// ProcessInScopeFacebook
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public abstract Task ProcessInScopeFacebook(IServiceProvider serviceProvider);

        #endregion
    }
}