using Namek.Resources.MultiLanguage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Namek.Admin.AttributeCustom;
using Namek.Admin.CustomFilters;
using Namek.Admin.Models.Customer;
using Namek.Admin.Utilities;
using Namek.Core.ActionResult;
using Namek.Core.Utility;
using Namek.Data.Database;
using Namek.Entity.Config;
using Namek.Entity.InfoModel;
using Namek.Entity.Result; 
using Namek.Library.Data;
using Namek.Library.Entity.Logging;
using Namek.Library.Entity.Users;
 
using Namek.Library.Enums;
using Namek.Library.Helpers;
using Namek.Library.Infrastructure;
using Namek.LogServices.Logging;
using Namek.Resources.MultiLanguage;
using Newtonsoft.Json;
using Action = Namek.Library.Enums.Action;
using StringHelper = Namek.Core.StringHelper;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class CustomerController : BaseController
    {
        private readonly IActivityLogService _activityLogService = EngineContext.Current.Resolve<IActivityLogService>();
        //private readonly IUserRepository _userRepo = EngineContext.Current.Resolve<IUserRepository>(); 
   
        #region Private Methods
 

        private class CusSales
        {
            public int CustomerId { get; set; }
            public int SalesId { get; set; }
            public string SalesFullName { get; set; }
        }

        #endregion
         
    }
}