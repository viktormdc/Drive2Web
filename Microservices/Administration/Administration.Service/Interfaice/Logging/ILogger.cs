using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Administration.Data.Domain.Logging;

namespace Administration.Service.Interfaice.Logging
{
   public interface ILogger
    {
        /// <summary>
        /// Inserts a log item
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <param name="shortMessage">The short message</param>
        /// <param name="fullMessage">The full message</param>
        /// <param name="customerId">The customer to associate log record with</param>
        /// <returns>A log item</returns>
        Task<Log> InsertLog(string logLevel, string shortMessage, string fullMessage = "", string customerId = null);
    }
}
