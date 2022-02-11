using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Administration.Data.Domain.Logging;
using Administration.Data.Implementation;
using Administration.Data.Interface;
using Microsoft.Extensions.Logging;
using ILogger = Administration.Service.Interfaice.Logging.ILogger;

namespace Administration.Service.Implementation.Logging
{
    public class Logger : ILogger
    {

        #region Fields

        private readonly IRepository<Log> _logRepository;

        #endregion

        #region Ctor

        public Logger(IRepository<Log> logRepository)
        {
            this._logRepository = logRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// InsertLog
        /// </summary>
        /// <param name="logLevel"></param>
        /// <param name="shortMessage"></param>
        /// <param name="exceptionMessage"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public async Task<Log> InsertLog(string logLevel, string shortMessage, string exceptionMessage = "", string customerId = null)
        {
            try
            {
                using (var scope = new UnitOfWork())
                {
                    var log = new Log
                    {
                        LogLevel = logLevel,
                        ShortMessage = shortMessage,
                        ExceptionMessage = exceptionMessage,
                        CustomerId = customerId,
                        CreatedOn = DateTime.Now
                    };

                    await _logRepository.InsertAsync(log);

                    scope.Commit();

                    return log;
                }
            }
            catch (Exception exception)
            {
                throw new ApplicationException(exception.Message);
            }
        }

        #endregion


    }
}
