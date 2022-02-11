using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Data.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        void StartTransaction();
        void Commit();
    }
}
