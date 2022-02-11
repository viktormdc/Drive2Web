using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Rabbit.Domain.Core.Commands;
using Rabbit.Domain.Core.Events;

namespace Rabbit.Domain.Core.Bus
{
    public interface IEventBus
    {
        /// <summary>
        /// SendCommand
        /// </summary>
        /// <typeparam name="TCommand"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        Task SendCommand<TCommand>(TCommand command) where TCommand : Command;

        /// <summary>
        /// Publish
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <param name="event"></param>
        void Publish<TEvent>(TEvent @event) where TEvent : Event;

        /// <summary>
        /// Subscribe
        /// </summary>
        /// <typeparam name="TEvent"></typeparam>
        /// <typeparam name="TEventHandler"></typeparam>
        void Subscribe<TEvent, TEventHandler>() where TEvent : Event where TEventHandler : IEventHandler<TEvent>;
    }
}
