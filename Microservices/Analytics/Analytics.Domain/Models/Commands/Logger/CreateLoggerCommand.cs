using System;
using System.Collections.Generic;
using System.Text;
using Rabbit.Domain.Core.Commands;

namespace Analytics.Domain.Models.Commands.Logger
{
    public class CreateLoggerCommand : Command
    {
        public string LogLevel { get; set; }
        public string CustomerId { get; set; }
        public string ShortMessage { get; set; }
        public string ExceptionMessage { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
