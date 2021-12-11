
using System;
using System.Configuration;

using Namek.Resources.MultiLanguage;

using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Namek.Data.UnitOfWork;
using Namek.Entity.EntityModel; 
using Namek.Library.Entity.Logging;
using Namek.Library.Enums;
using Namek.Library.Helpers;
using Namek.Library.Infrastructure;
using Namek.LogServices.Logging;
using Namek.LogServices.VerboseReporter;
using Namek.Resources.MultiLanguage;
using Namek.Services.AuthenticationServices;
using Namek.Services.Services;
using Microsoft.AspNet.Identity;
using Action = Namek.Library.Enums.Action;
using System.Reflection; 
using Namek.Admin.Models;
using System.IO;
using System.Collections.Generic;

namespace Namek.Admin.Controllers
{
    public class BaseController : Controller
    {
        //private readonly IUserRepository _userRepo = EngineContext.Current.Resolve<IUserRepository>();

        private User _currentUser;
        //protected IVerboseReporterService VerboseReporter;
        //public Data.EFramework.IDbTransaction _trans;
        //public IUnitOfWork _unit;  
        //private readonly IPermissionActionRepository _permissionActionRepo = EngineContext.Current.Resolve<IPermissionActionRepository>();
        public BaseController()
        { 
       
            //VerboseReporter = EngineContext.Current.Resolve<IVerboseReporterService>();
        }

        //public User CurrentUser
        //{
        //    get
        //    {
        //        if (!User.Identity.IsAuthenticated)
        //        {
        //            _currentUser = null;
        //            return _currentUser;
        //        }

        //        if (_currentUser != null)
        //            return _currentUser;

        //        if (Session["currentUser"] != null)
        //        {
        //            _currentUser = Session["currentUser"] as User;

        //            return _currentUser;
        //        }

        //        var identity = User.Identity as ClaimsIdentity;

        //        var userName = identity.FindFirstValue("UserName");
        //        var email = identity.FindFirstValue("Email");

        //        _currentUser =  ApiService.UserService.UserGetByEmailOrUserName(userName, email, null);

        //        return _currentUser;
        //    }
        //}
        public User CurrentUser
        {
            get
            {
                if (!User.Identity.IsAuthenticated)
                {
                    _currentUser = null;
                    return _currentUser;
                }

                if (_currentUser != null)
                    return _currentUser;

                var identity = User.Identity as ClaimsIdentity;

                var userName = identity.FindFirstValue("UserName");
                var email = identity.FindFirstValue("Email");

                _currentUser = new User
                {
                    Email = email,
                    UserName = userName
                };
                _currentUser = ApiService.UserService.UserGetByEmailOrUserName(userName, email);
                return _currentUser;
            }
            set => _currentUser = value;
        }

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            ViewBag.User = CurrentUser;

            ViewBag.IsStationCompany = "" + ConfigurationManager.AppSettings["IsStationCompany"];
        }

        protected override void OnException(ExceptionContext filterContext)
        {

            if (filterContext.Exception == null)
            {
                base.OnException(filterContext);
                return;
            }

            if (filterContext.Exception is HttpException httpException && httpException.GetHttpCode() == 404)
            {
                base.OnException(filterContext);
                return;
            }

            try
            {
                //DucAnh: rollback transaction khi bi exception
                //if (_trans != null)
                //{
                //    _trans.Rollback();
                //}
                // todo: Lưu lại Exception vào DB
                //filterContext.RequestContext.HttpContext.Request.Params.AllKeys
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            base.OnException(filterContext);
        }

        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
        {
            // Attempt to read the display language cookie from Request
            var cultureCookie = Request.Cookies["_displayLanguage"];
            var cultureName = cultureCookie != null ? cultureCookie.Value : CultureHelper.GetDefaultCulture();

            //if (cultureName != (string)HttpContext.Session["_displayLanguage"])
            {
                // Validate culture name
                cultureName = CultureHelper.GetImplementedCulture(cultureName);

                // Modify current thread's cultures
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

                // Get current language display name
                var supportedLanguages = CultureHelper.SupportedLanguages();
                var sls = supportedLanguages.Where(x => !x.Trim().Equals(Thread.CurrentThread.CurrentUICulture.Name)).Select(x => new CultureInfo(x.Trim()));

                HttpContext.Session["_CurrentLanguage"] = Thread.CurrentThread.CurrentUICulture.GetParent().NativeName;
                HttpContext.Session["_SupportedLanguages"] = sls;
                HttpContext.Session["_displayLanguage"] = cultureName;
            }


            //var culture = CultureInfo.CreateSpecificCulture(Thread.CurrentThread.CurrentCulture.Name);
            //if (Request.IsAuthenticated)
            //    Task.Run(async () =>
            //    {
            //        Thread.CurrentThread.CurrentCulture = culture;
            //        Thread.CurrentThread.CurrentUICulture = culture;
            //        var rs = await ApiService.AccountService.GetCurrentUserAndNotifys();

            //        CurrentUser = rs.CurrentUser;
            //        ViewBag.User = CurrentUser;

            //        ViewBag.NotifyMessage = rs.NotifyMessages;
            //        ViewBag.TotalMessage = rs.TotalMessage;

            //        ViewBag.Notify = rs.Notifies;
            //        ViewBag.TotalNotify = rs.TotalNotify;

            //        ViewBag.NotifyTask = rs.NotifyTask;
            //        ViewBag.NotifyTask = rs.NotifyTask;
            //        ViewBag.TotalTask = rs.TotalTask;

            //        // Đếm số lượng trong giỏ hàng.
            //        ViewBag.NoItemInCart = rs.NoItemInCart;
            //    })
            //        .Wait();
            //else
            //    ViewBag.User = null;

            return base.BeginExecuteCore(callback, state);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            //if (_trans != null)
            //{
            //    _trans.Rollback();
            //}
            base.OnResultExecuted(filterContext);
        }

        private ApiServiceFactory _apiServiceFactory;
        private ApiServiceFactory _apiServiceFactoryAutomation;
        public ApiServiceFactory ApiService
        {
            get
            {
                if (_apiServiceFactory != null)
                    return _apiServiceFactory;
                _apiServiceFactory = new ApiServiceFactory(ConfigurationManager.AppSettings["ApiGateWay"],
                    CurrentUser, "");

                return _apiServiceFactory;
            }
        }
        public ApiServiceFactory ApiServiceAutomation
        {
            get
            {
                if (_apiServiceFactoryAutomation != null)
                    return _apiServiceFactoryAutomation;
                string lstRole = "" + ConfigurationManager.AppSettings["lstRole"];
                _apiServiceFactoryAutomation = new ApiServiceFactory(ConfigurationManager.AppSettings["ApiGateWay_Automation"],
                    CurrentUser, lstRole);

                return _apiServiceFactoryAutomation;
            }
        }
        private ApiAuthenticationFactory _apiAuthenticationFactory;
        public ApiAuthenticationFactory ApiAuthentication
        {
            get
            {
                if (_apiAuthenticationFactory != null)
                    return _apiAuthenticationFactory;
                string lstRole = "" + ConfigurationManager.AppSettings["lstRole"];
                _apiAuthenticationFactory = new ApiAuthenticationFactory(ConfigurationManager.AppSettings["ApiAuthentication"], lstRole);

                return _apiAuthenticationFactory;
            }
        }
        /// <summary>
        /// Thienxxx - Kiem tra quyen cua user theo PageId va Action
        /// 07/11/2018
        /// </summary>
        /// <param name="CurrentUser"></param>
        /// <param name="PageId"></param>
        /// <param name="Action"></param>
        /// <returns></returns>
        public bool CheckPermision(User CurrentUser, PageId PageId, Action Action)
        {
            if (CurrentUser != null && CurrentUser.UserName == "admin")
                return true;
            var permissionActions = ApiService.PermissionActionService.GetByPermissionIdAndUserId(CurrentUser.GroupPermissionId ?? 0,
    CurrentUser.Id);
            // Kiểm tra quyền truy cập tới với PageId và Action
            return permissionActions.Any(x => x.PageId == (int)PageId && x.ActionKey == (byte)Action);

        }

    }
}