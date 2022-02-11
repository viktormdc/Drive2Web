using System;
using System.Threading.Tasks;
using System.Transactions;
using Analytics.Data.Context;
using Analytics.Data.Interface;
using Microsoft.EntityFrameworkCore;

namespace Analytics.Data.Implemetation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        #region Fields 

        private readonly DbContext _context;
        private TransactionScope _transaction;
        #endregion

        #region Ctor

        public UnitOfWork(AnalyticsDbContext context)
        {
            this._context = context;
        }

        public UnitOfWork()
        {
            this.StartTransaction();
        }

        #endregion

        #region Methods

        public void StartTransaction()
        {
            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TransactionManager.MaximumTimeout // use a sensible value here
            };
           
            this._transaction = new TransactionScope(TransactionScopeOption.Required,
                transactionOptions,
                TransactionScopeAsyncFlowOption.Enabled);
            
        }
        public void Commit()
        {
            this._transaction.Complete();
            this.Dispose();

        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            this._transaction.Dispose();
        }

        #endregion

       
    }
}
