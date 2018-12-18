using System;


namespace Ehasan.Core.Repository_Interfaces
{
    public interface IReadOnlyUnitOfWork : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;
    }
}
