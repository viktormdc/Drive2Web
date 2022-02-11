using System;
using System.Collections.Generic;
using System.Text;

namespace Rabbit.Domain.Core.Events
{
    public abstract class Event
    {
        protected Event()
        {
            Timestamp = DateTime.UtcNow;
        }

        public DateTime Timestamp { get; protected set; }
    }
}
