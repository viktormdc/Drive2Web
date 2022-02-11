using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Rabbit.Domain.Core.Events
{
    public abstract class Message : IRequest<bool>
    {
        protected Message()
        {
            Type = GetType().Name;
        }

        public string Type { get; protected set; }
    }
}
