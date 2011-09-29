using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace CodeCamp.ASP.Web.UI
{
    public class ServiceProvider : IServiceProvider
    {
        private HttpContextWrapper wrapper;

        public ServiceProvider(HttpContextWrapper wrapper)
        {
            this.wrapper = wrapper;
        }

        private HttpContext _httpContext;
        public object GetService(Type serviceType)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }
            if (serviceType == typeof(IPrincipal))
                return this._httpContext.User;
            if (serviceType == typeof(HttpContextBase))
                return this._httpContext;
            return null;
        }
    }
}