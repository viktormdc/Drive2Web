using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Analytics.Domain.Models.Commands.Logger;
using Analytics.Domain.Models.Events;
using MediatR;
using Rabbit.Domain.Core.Bus;

namespace Analytics.Domain.Models.CommandHandlers.Logger
{
    public class CreateLoggerCommandHandler : IRequestHandler<CreateLoggerCommand, bool>
    {
        #region Fields
        private readonly IEventBus _eventBus;
        #endregion

        #region Ctor

        public CreateLoggerCommandHandler(IEventBus eventBus)
        {
            this._eventBus = eventBus ?? throw new ArgumentNullException();

        }
        #endregion

        #region Methods
        public Task<bool> Handle(CreateLoggerCommand request, CancellationToken cancellationToken)
        {

            this._eventBus.Publish(new CreateLoggerCreatedEvent(request.LogLevel, request.ShortMessage, request.ExceptionMessage, request.CustomerId, request.CreatedOn));
            return Task.FromResult(true);
        }

        #endregion
    }
}
