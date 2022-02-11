using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Administration.Data.Implementation;
using Administration.Data.Interface;
using Administration.Service.Interfaice;
using Administration.Service.Interfaice.Admin;
using Microsoft.Extensions.Logging;
using ILogger = Administration.Service.Interfaice.Logging.ILogger;

namespace Administration.Service.Implementation
{
    public class AdminService : IAdminService
    {
        #region Fields

        //private readonly IRepository<User> _userRepository;
        private readonly ILogger _logger;

        #endregion


        #region Ctor

        public AdminService( ILogger logger)
        {
            this._logger = logger;
        }

        #endregion

        #region Public Methods
        
        ///// <summary>
        ///// GetUsers
        ///// </summary>
        ///// <returns></returns>
        //public async Task<IList<User>> GetUsers()
        //{
        //    try
        //    {
        //        var users = await _userRepository.GetAllAsync();

        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //    return null;
        //}

        ///// <summary>
        ///// CreateUser
        ///// </summary>
        ///// <returns></returns>
        //public async Task<string> CreateUser()
        //{

        //    using (var scope = new UnitOfWork())
        //    {
        //        var user = new User()
        //        {
        //           // Name = string.Empty,
        //            Email = "nikola@gmail.com"
        //        };

        //        await _userRepository.InsertAsync(user);
        //        await _logger.InsertLog(LogLevel.Information.ToString(), "Logger Short Message", "logger long message", 1);

        //        scope.Commit();
        //    }

        //    return null;
        //}

        #endregion

        #region Private Methods

        

        #endregion

    }
}
