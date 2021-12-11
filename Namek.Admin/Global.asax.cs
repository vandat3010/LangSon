using System;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Namek.Admin.AttributeCustom;
using Namek.Entity.AutoMappers;
using Namek.Library.Entity.Logging;
using Namek.Library.Enums;
using Namek.Library.Helpers;
using Namek.Library.Infrastructure;
using Namek.LogServices.Logging;

namespace Namek.Admin
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            //disable "X-AspNetMvc-Version" header name
            MvcHandler.DisableMvcResponseHeader = true;
            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
            ImageResizer.Configuration.Config.Current.Plugins.LoadPlugins();

            //initialize engine context
            EngineContext.Initialize(true);

            AreaRegistration.RegisterAllAreas();

            ModelBinderConfig.Register(ModelBinders.Binders);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Đăng ký mapper
            AutoMapperConfig.RegisterMappings();

            //GlobalFilters.Filters.Add(new XFrameOptionsFilterAttribute() { AlwaysApply = true });

            //log application start
            try
            {
                //log
                var logger = EngineContext.Current.Resolve<ILogService>();
                logger.Information("Application admin started");
            }
            catch (Exception)
            {
                //don't throw new exception if occurs
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // hide the following headers.
            // Server
            // X - AspNet - Version
            // X - AspNetMvc - Version
            // X - Powered - By
            // Removing Server Header
            if (sender is HttpApplication app)
                app.Context.Response.Headers.Remove("Server");
        }

        /// <summary>
        /// PreSend Request Headers
        /// </summary>
        protected void Application_PreSendRequestHeaders()
        {
            //Remove Server Header
            Response.Headers.Remove("Server");
            //Remove X-AspNet-Version Header
            Response.Headers.Remove("X-AspNet-Version");
             
        }


        private void Application_Error(object sender, EventArgs e)
        {
            try
            {
                var ex = Server.GetLastError();
                var httpException = ex as HttpException ?? ex.InnerException as HttpException; 

            }
            catch
            {
            }

        }
         
    }
}