using System;
using Namek.Admin;
using Namek.Admin.AttributeCustom;
using Namek.Core; 
using Namek.Library.Config;
using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup("Admin", typeof(Namek.Admin.Startup))]

namespace Namek.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            
        }
         
    }
}