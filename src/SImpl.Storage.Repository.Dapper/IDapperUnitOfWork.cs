using System.Data;

namespace SImpl.Storage.Repository.Dapper
{
    public interface IDapperUnitOfWork : IUnitOfWork
    {
        IDbConnection GetConnection();
        IDbTransaction GetTransaction();
    }
}