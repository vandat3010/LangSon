using Namek.Resources.MultiLanguage;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Namek.Admin.AttributeCustom;
using Namek.Admin.CustomFilters;
using Namek.Admin.Extensions;
using Namek.Admin.Models.ActivityLog;
using Namek.Library.Entity.Logging;
using Namek.Library.Enums;
using Namek.Library.Helpers;
using Namek.Library.Infrastructure;
using Namek.LogServices.Logging;
using Hangfire;
using Action = Namek.Library.Enums.Action;
using Namek.Admin.Utilities;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class ActivityLogController : BaseController
    {
       
    }
}