using System;
using System.Web;
using System.Web.Caching;
using CodeCamp.ASP.UI.Infrastructure;
using CodeCamp.ASP.UI.Infrastructure.Mappers;
using CodeCamp.ASP.UI.Infrastructure.ServiceContracts;
using CodeCamp.ASP.UI.Infrastructure.Services;
using Microsoft.Practices.Unity;

namespace CodeCamp.ASP.Web.UI
{
    
    public static class CodeCampUnityContainer
    {

        public static IUnityContainer Container { get; set; }

    }

    public class Global : System.Web.HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            CodeCampUnityContainer.Container = ConfigureCodeCampContainer();
        }

        //void InitializeContainerFromCache()
        //{
        //    IUnityContainer container = HttpRuntime.Cache.Get("Unity") as IUnityContainer;
            
        //    if (container == null)
        //    {
        //        container = ConfigureCodeCampContainer();

        //        HttpRuntime.Cache
        //                   .Add("Unity",
        //                         container,
        //                         null,
        //                         Cache.NoAbsoluteExpiration,
        //                         Cache.NoSlidingExpiration,
        //                         CacheItemPriority.NotRemovable,
        //                         null);
        //    }
        //    CodeCampUnityContainer.Container = container;
        //}

        private static IUnityContainer ConfigureCodeCampContainer()
        {
            IUnityContainer container = new UnityContainer();

            container.RegisterType<ICodeCampDataService, CodeCampDataService>(new ContainerControlledLifetimeManager())
                .RegisterType<ICacheService, CacheService>(new ContainerControlledLifetimeManager())
                .RegisterType<ISponsorMapper, SponsorMapper>(new ContainerControlledLifetimeManager())
                .RegisterType<IPresentationMapper, PresentationMapper>(new ContainerControlledLifetimeManager())
                .RegisterType<ILoggingService, LoggingService>(new ContainerControlledLifetimeManager())
                .RegisterType<IAnnouncementMapper, AnnouncementMapper>(new ContainerControlledLifetimeManager());

            return container;
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // TODO JAS
            if (CodeCampUnityContainer.Container != null)
            {
                var loggingSvc = CodeCampUnityContainer.Container.Resolve<ILoggingService>();

                var ccex = new CodeCampAuthorizationException("Last Chance Exception", Server.GetLastError());
                ccex.ProcessName = "Application_Error in: " + HttpContext.Current.Request.Url.ToString();

                loggingSvc.LogException(ccex);

                // Tell the user something broke.
                Server.Transfer("~/ErrorPage.aspx");
            }

            // Clear the error
            // Server.ClearError();
        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started

            // InitializeContainerFromCache();

        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

        }

    }
}
