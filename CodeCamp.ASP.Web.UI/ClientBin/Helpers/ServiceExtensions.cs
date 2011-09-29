using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.ServiceModel.DomainServices.Server;
using System.Web;
using System.Web.Hosting;

namespace CodeCamp.ASP.Web.UI
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// Initializes the domain service by creating a new <see cref="DomainServiceContext"/>
        /// and calling the base DomainService.Initialize(DomainServiceContext) method.
        /// </summary>
        /// <typeparam name="TService">The type of the service.</typeparam>
        /// <param name="service">The service.</param>        
        /// <param name="operationType">The Operation Type.</param>
        /// <returns></returns>
        public static TService WebInitialize<TService>(this TService service, DomainOperationType operationType)
            where TService : DomainService
        {
            DomainServiceContext context = null;
            switch (operationType)
            {
                case DomainOperationType.Invoke:
                    {
                        context = CreateDomainServiceContextForInvoke();
                        break;
                    }
                case DomainOperationType.Query:
                    {
                        context = CreateDomainServiceContextForQuery();
                        break;
                    }
                case DomainOperationType.Submit:
                    {
                        context = CreateDomainServiceContextForSubmit();
                        break;
                    }
            }
            service.Initialize(context);
            return service;
        }

        private static DomainServiceContext CreateDomainServiceContextForInvoke()
        {
            var provider = new ServiceProvider(new HttpContextWrapper(GetHttpContext()));
            return new DomainServiceContext(provider, DomainOperationType.Invoke);
        }
        private static DomainServiceContext CreateDomainServiceContextForSubmit()
        {
            var provider = new ServiceProvider(new HttpContextWrapper(GetHttpContext()));
            return new DomainServiceContext(provider, DomainOperationType.Submit);
        }

        private static DomainServiceContext CreateDomainServiceContextForQuery()
        {
            var provider = new ServiceProvider(new HttpContextWrapper(GetHttpContext()));
            return new DomainServiceContext(provider, DomainOperationType.Query);
        }

        private static HttpContext GetHttpContext()
        {
            var context = HttpContext.Current;
#if DEBUG
            // create a mock HttpContext to use during unit testing...
            if (context == null)
            {
                var writer = new StringWriter();
                var request = new SimpleWorkerRequest("/", "/", String.Empty, String.Empty, writer);
                context = new HttpContext(request){ User = new GenericPrincipal(new GenericIdentity("debug"), null) };
            }
#endif
            return context;
        }
    }
}