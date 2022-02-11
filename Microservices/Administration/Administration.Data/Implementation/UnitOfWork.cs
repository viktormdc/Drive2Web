using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Administration.Data.Context;
using Administration.Data.Interface;
using Microsoft.EntityFrameworkCore;

namespace Administration.Data.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields 

        private readonly DbContext _context;
        private TransactionScope _transaction;
        #endregion

        #region Ctor

        public UnitOfWork(AdminDbContext context)
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
            this._transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        }

        public void Commit()
        {
            this._transaction.Complete();
            this.Dispose();

        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            this.Dispose();
        }

        public void Dispose()
        {
            this._transaction.Dispose();
        }

        #endregion
    }
}
