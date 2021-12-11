using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace Namek.Admin
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            if (ConfigurationManager.AppSettings["ExpireTimeMinutes"] == null ||
                !int.TryParse(ConfigurationManager.AppSettings.Get("ExpireTimeMinutes"), out var expireTimeSpan))
                expireTimeSpan = 1;

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                ExpireTimeSpan = TimeSpan.FromMinutes(expireTimeSpan),
                SlidingExpiration = true,
                CookieHttpOnly = true,
                Provider = new CookieAuthenticationProvider
                {
                    OnValidateIdentity = ctx => Task.FromResult(0),
                    OnApplyRedirect = ctx =>
                    {
                        if (!IsAjaxRequest(ctx.Request))
                            ctx.Response.Redirect(ctx.RedirectUri);
                    }
                }
            });

            AntiForgeryConfig.UniqueClaimTypeIdentifier = "UserId";
        }

        private static bool IsAjaxRequest(IOwinRequest request)
        {
            var query = request.Query;
            if (query != null && query["X-Requested-With"] == "XMLHttpRequest")
                return true;
            var headers = request.Headers;
            return headers != null && headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}