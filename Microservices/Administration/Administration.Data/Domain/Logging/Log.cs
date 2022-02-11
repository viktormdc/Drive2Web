using System;
using System.Collections.Generic;
using System.Text;
using Administration.Data.Implementation;
using Administration.Data.Interface;
using Microsoft.Extensions.Logging;

namespace Administration.Data.Domain.Logging
{
   public class Log: BaseEntity
    {
        /// <summary>
        /// Gets or sets the log level identifier
        /// </summary>
        public string LogLevel { get; set; }

        /// <summary>
        /// Gets or sets the short message
        /// </summary>
        public string ShortMessage { get; set; }

        /// <summary>
        /// Gets or sets the exception message
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// Gets or sets the date and time of instance creation
        /// </summary>
        public DateTime CreatedOn { get; set; }


    }
}
