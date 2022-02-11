using System;
using System.Threading.Tasks;
using Administration.Data.Domain.Logging;
using Administration.Data.Interface;
using Administration.Domain.Models.Events;
using Administration.Service.Interfaice.Logging;
using Rabbit.Domain.Core.Bus;

namespace Administration.Domain.Models.EventHandlers
{
    public class CreateLoggerCreatedEventHandler : IEventHandler<CreateLoggerCreatedEvent>
    {
        #region Fields
        private readonly ILogger _logger;
        #endregion

        #region Ctor
        public CreateLoggerCreatedEventHandler(ILogger logger)
        {
            this._logger = logger;

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="event"></param>
        /// <returns></returns>
        public async Task Handle(CreateLoggerCreatedEvent @event)
        {
            await this._logger.InsertLog(@event.LoggerLevel, @event.ShortDescription, @event.ExceptionMessage,
                @event.CustomerId);
        }

        #endregion
    }
}
