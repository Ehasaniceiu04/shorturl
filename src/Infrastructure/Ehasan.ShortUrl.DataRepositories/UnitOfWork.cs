using Ehasan.Core.Repository_Interfaces;
using Ehasan.ShortUrl.DataRepositories.Context;
using Ehasan.ShortUrl.DataRepositories.Repository;
using System.Threading;
using System.Threading.Tasks;

namespace Ehasan.ShortUrl.DataRepositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ShortUrlDbContext shortUrlDbContext;
       
       

        public UnitOfWork(ShortUrlDbContext shortUrlDbContext)
        {
            this.shortUrlDbContext = shortUrlDbContext;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            return new RepositoryBase<TEntity>(this.shortUrlDbContext);
        }

        public void SaveChanges()
        {
            //this.RunBeforeSave(this.dbContextScope);
            this.shortUrlDbContext.SaveChanges();
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            //this.RunBeforeSave(this.dbContextScope);
            await this.shortUrlDbContext.SaveChangesAsync(cancellationToken);
        }

        //protected virtual void RunBeforeSave(IDbContextScope currentDbContextScope)
        //{
        //}

        private bool disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed)
            {
                return;
            }

            if (disposing)
            {
                this.shortUrlDbContext.Dispose();
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
