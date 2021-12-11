using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Namek.Entity.Config;
using Namek.Entity.EntityModel;
using Namek.Infrastructure.Repository;
using Namek.Interface.Repository;
using Namek.Library.Enums;
using Namek.Library.Infrastructure;

namespace Namek.Api.Controllers
{
    public class BaseController : ApiController
    {
        private readonly IUserRepository _userRepo = EngineContext.Current.Resolve<IUserRepository>();
        private User _currentUser;
        public string lstRole = "";

        public User CurrentUser
        {
            get
            {
                if (!Request.Headers.Contains("UserName"))
                {
                    _currentUser = null;
                    return _currentUser;
                }

                if (_currentUser != null)
                    return _currentUser;

                var userName = Request.Headers.GetValues("UserName").FirstOrDefault();
                var email = Request.Headers.GetValues("Email").FirstOrDefault();
                lstRole = Request.Headers.GetValues("lstRole").FirstOrDefault();


                _currentUser = _userRepo.UserGetByEmailOrUserName(userName, email, null);


                return _currentUser;
            }
        }
         

        protected override ExceptionResult InternalServerError(Exception exception)
        {
            // todo: Lưu lại Exception vào DB

            return base.InternalServerError(exception);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="controllerContext"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<HttpResponseMessage> ExecuteAsync(HttpControllerContext controllerContext, CancellationToken cancellationToken)
        {
            // set language
            if (controllerContext.Request.Headers.AcceptLanguage != null && controllerContext.Request.Headers.AcceptLanguage.Count > 0)
            {
                var lang = controllerContext.Request.Headers.AcceptLanguage.First().Value;
                var culture = CultureInfo.CreateSpecificCulture(lang);

                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = culture;
            }

            return base.ExecuteAsync(controllerContext, cancellationToken);
        }

      
    }
}