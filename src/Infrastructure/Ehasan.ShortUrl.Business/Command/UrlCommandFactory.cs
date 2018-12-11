using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ehasan.ShortUrl.Business.Command
{
    public class UrlCommandFactory : IUrlCommandFactory
    {
        private IContainer container;

        public UrlCommandFactory(IContainer container)
        {
            this.container = container;
        }
        IUrlCommand IUrlCommandFactory.CreateCommand(string serviceName)
        {
            return container.ResolveNamed<IUrlCommand>(serviceName);
        }
    }
}
