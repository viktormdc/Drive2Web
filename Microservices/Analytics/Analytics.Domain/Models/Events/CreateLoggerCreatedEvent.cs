using System;
using System.Collections.Generic;
using System.Text;
using Rabbit.Domain.Core.Events;

namespace Analytics.Domain.Models.Events
{
    public class CreateLoggerCreatedEvent : Event
    {
        #region Fields

        public string LoggerLevel { get; set; }
        public string ShortDescription { get; set; }
        public string ExceptionMessage { get; set; }
        public string CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }

        #endregion

        #region Ctor
        public CreateLoggerCreatedEvent(string logerLevel, string shortDescription, string exceptionMessage, string customerId, DateTime createdOn)
        {

            LoggerLevel = logerLevel;
            ShortDescription = shortDescription;
            ExceptionMessage = exceptionMessage;
            CustomerId = customerId;
            CreatedOn = createdOn;
        }

        #endregion



    }
}
