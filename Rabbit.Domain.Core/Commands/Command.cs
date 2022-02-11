using System;
using System.Collections.Generic;
using System.Text;
using Rabbit.Domain.Core.Events;

namespace Rabbit.Domain.Core.Commands
{
    public abstract class Command : Message
    {
        protected Command()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; protected set; }
    }
}
