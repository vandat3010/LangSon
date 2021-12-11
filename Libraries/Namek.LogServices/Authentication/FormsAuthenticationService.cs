using System;
using System.Web;
using System.Web.Security;
using Heliosys.Ecommerce.Core.Config;
using Heliosys.Ecommerce.Core.Entity.Users;
using Heliosys.Ecommerce.Services.Users;

namespace Heliosys.Ecommerce.Services.Authentication
{
    /// <summary>
    /// Authentication service
    /// </summary>
    public partial class FormsAuthenticationService : IAuthenticationService
    {
        #region Fields

        private readonly HttpContextBase _httpContext;
        private readonly TimeSpan _expirationTimeSpan;
        private readonly IUserService _manageStaffService;
        private User _cachedManageStaff;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="httpContext">HTTP context</param>
        /// <param name="manageStaffService"></param>       
        public FormsAuthenticationService(HttpContextBase httpContext, IUserService manageStaffService)
        {
            this._httpContext = httpContext;
            _manageStaffService = manageStaffService;

            this._expirationTimeSpan = FormsAuthentication.Timeout;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <param name="ticket">Ticket</param>
        /// <returns>Customer</returns>
        protected virtual User GetAuthenticatedCustomerFromTicket(FormsAuthenticationTicket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            var usernameOrEmail = ticket.UserData;

            if (String.IsNullOrWhiteSpace(usernameOrEmail))
                return null;
            var manageStaff = _manageStaffService.FirstOrDefault(x =>x.Email == usernameOrEmail);
            return manageStaff;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sign in
        /// </summary>
        /// <param name="user">Customer</param>
        /// <param name="createPersistentCookie">A value indicating whether to create a persistent cookie</param>
        public virtual void SignIn(User user, bool createPersistentCookie)
        {
            var now = DateTime.UtcNow.ToLocalTime();

            var ticket = new FormsAuthenticationTicket(
                1 /*version*/,
                user.Email,
                now,
                now.Add(_expirationTimeSpan),
                createPersistentCookie,
                user.Email,
                FormsAuthentication.FormsCookiePath);

            var encryptedTicket = FormsAuthentication.Encrypt(ticket);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.HttpOnly = true;
            if (ticket.IsPersistent)
            {
                cookie.Expires = ticket.Expiration;
            }
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (FormsAuthentication.CookieDomain != null)
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            _httpContext.Response.Cookies.Add(cookie);

            SetSession(user, createPersistentCookie);
            _cachedManageStaff = user;
        }

       
        public void SetSession(User user, bool createPersistentCookie)
        {
           
            if (HttpContext.Current != null && HttpContext.Current.Session != null)
            {
                HttpContext.Current.Session[Constants.UserAdminSessionKey] = user;

                FormsAuthentication.SetAuthCookie(user.Email, createPersistentCookie);
                
            }
            
        }
        /// <summary>
        /// Sign out
        /// </summary>
        public virtual void SignOut()
        {
            _cachedManageStaff = null;
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.Abandon();
        }

        /// <summary>
        /// Get authenticated customer
        /// </summary>
        /// <returns>Customer</returns>
        public virtual User GetAuthenticatedUser()
        {
            
            _cachedManageStaff = GetCurrentUserSession();

            if (_cachedManageStaff != null)
                return _cachedManageStaff;

            if (_httpContext == null ||
                _httpContext.Request == null ||
                !_httpContext.Request.IsAuthenticated ||
                !(_httpContext.User.Identity is FormsIdentity))
            {
                return null;
            }

            var formsIdentity = (FormsIdentity)_httpContext.User.Identity;
            var customer = GetAuthenticatedCustomerFromTicket(formsIdentity.Ticket);
            if (customer != null)
                _cachedManageStaff = customer;
            return _cachedManageStaff;
        }
        public User GetCurrentUserSession()
        {
            if (_cachedManageStaff != null)
                return _cachedManageStaff;

            if (HttpContext.Current == null) return null;

            var loginUser = HttpContext.Current.Session[Constants.UserAdminSessionKey] as User;
            return loginUser;
        }
        #endregion
    }
}
