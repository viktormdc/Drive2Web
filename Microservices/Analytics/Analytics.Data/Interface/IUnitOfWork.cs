namespace Analytics.Data.Interface
{
   public interface IUnitOfWork
    {
        void StartTransaction();
        void Commit();
    }
}
