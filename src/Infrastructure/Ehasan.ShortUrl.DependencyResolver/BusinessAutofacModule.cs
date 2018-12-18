using Autofac;
using Ehasan.ShortUrl.Business;
using Ehasan.ShortUrl.Business.Command;
using Ehasan.ShortUrl.Business.ServiceQuery;
using Ehasan.ShortUrl.Core.Business_Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Ehasan.ShortUrl.DependencyResolver
{
    public static class BusinessAutofacModule
    {
        public static ContainerBuilder CreateAutofacBusinessContainer(this IServiceCollection services, ContainerBuilder builder)
        {
            builder.RegisterType<ShortenUrlService>().As<IShortenUrlService>();
            builder.RegisterType<ShortenUrlServiceQuery>().As<IShortenUrlServiceQuery>();
            builder.RegisterType<UrlCommandFactory>().As<IUrlCommandFactory>();
            builder.RegisterType<LongUrlCommand>().Named<IUrlCommand>("long");
            builder.RegisterType<ShortUrlCommand>().Named<IUrlCommand>("short");
            return builder;
        }
    }
}
