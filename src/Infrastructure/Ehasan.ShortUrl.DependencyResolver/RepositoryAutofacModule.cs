using Autofac;
using Ehasan.Core.Repository_Interfaces;
using Ehasan.ShortUrl.DataRepositories;
using Microsoft.Extensions.DependencyInjection;

namespace Ehasan.ShortUrl.DependencyResolver
{
    public static class RepositoryAutofacModule
    {
        public static ContainerBuilder CreateAutofacRepositoryContainer(this IServiceCollection services, ContainerBuilder builder, string connectionString)
        {
            //var databaseInitializer = new MigrateToLatestVersion(new SampleDataSeeder());
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            return builder;
        }
    }
}
