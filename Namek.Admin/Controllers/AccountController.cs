using Namek.Resources.MultiLanguage;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Namek.Core;
using Namek.Entity.Config;
using Namek.Entity.InfoModel;
using Namek.Library.Enums;
using Namek.Library.Infrastructure;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Namek.Resources.MultiLanguage;
using System.Threading;
using System.Globalization;
using System.Linq;
using Namek.Entity.EntityModel;
using System.Collections.Generic;

namespace Namek.Admin.Controllers
{
    public class AccountController : BaseController
    {
        //private readonly IUserRepository _userRepo = EngineContext.Current.Resolve<IUserRepository>();

        private IAuthenticationManager AuthenticationManager()
        {
            return HttpContext.GetOwinContext().Authentication;
        }

        public ActionResult Login(string returnUrl, string lang)
        {
            if (!string.IsNullOrWhiteSpace(lang))
            {
                string cultureName = CultureHelper.GetImplementedCulture(lang);

                // Modify current thread's cultures
                Thread.CurrentThread.CurrentCulture = new CultureInfo(cultureName);
                Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;

                // Get current language display name
                var supportedLanguages = CultureHelper.SupportedLanguages();
                var sls = supportedLanguages.Where(x => !x.Trim().Equals(Thread.CurrentThread.CurrentUICulture.Name)).Select(x => new CultureInfo(x.Trim()));
                HttpContext.Session["_CurrentLanguage"] = Thread.CurrentThread.CurrentUICulture.GetParent().NativeName;
                HttpContext.Session["_SupportedLanguages"] = sls;
                HttpContext.Session["_displayLanguage"] = cultureName;

                var cookie = Request.Cookies["_displayLanguage"];
                if (cookie != null)
                    cookie.Value = cultureName;
                else
                {
                    cookie = new HttpCookie("_displayLanguage")
                    {
                        Value = cultureName,
                        Expires = DateTime.Now.AddYears(1)
                    };
                }
                Response.Cookies.Add(cookie);
            }
            ViewBag.Notification = "";
            // check for existing session
            if (Session[SessionUserLoginFails] != null || Session[SessionUserCaptcha] != null)
            {
                var loginInfo = new LoginInfo
                {
                    FailedLogins = Convert.ToInt32(Session[SessionUserLoginFails])
                };
                return PartialView("Login", loginInfo);
            }

            ViewBag.ReturnUrl = returnUrl;
            return PartialView(new LoginInfo());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginInfo model, string returnUrl)
        {
            // incremental delay to prevent brute force attacks
            int incrementalDelay;
            if (HttpContext.Application[Request.UserHostAddress] != null)
            
            {
                // wait for delay if there is one
                incrementalDelay = (int)HttpContext.Application[Request.UserHostAddress];
            }

            if (!ModelState.IsValid)
            {
                model.Password = null;
                return PartialView(model);
            }

            // validate session captcha
            if (Convert.ToInt32(Session[SessionUserLoginFails]) > 2)
            {
                if (model.Captcha == null)
                {
                    ModelState.AddModelError("Error", LanguageHelper.GetValidation(ResourceConstants.Validation.CaptchaNotNull));

                    model.Captcha = null;
                    if (ModelState.ContainsKey("Captcha"))
                        ModelState["Captcha"].Value = null;
                    Session["captcha"] = null;
                    model.FailedLogins = Convert.ToInt32(Session[SessionUserLoginFails]);
                    return PartialView(model);
                }

                if (Session["captcha"] != null && model.Captcha != null
                    && !Encryption.CheckCaptcha(model.Captcha.ToLower(), (string)Session["captcha"]))
                {
                    ModelState.AddModelError("Error", LanguageHelper.GetValidation(ResourceConstants.Validation.CaptchaInvalid));

                    model.Captcha = null;
                    if (ModelState.ContainsKey("Captcha"))
                        ModelState["Captcha"].Value = null;
                    Session["captcha"] = null;
                    model.FailedLogins = Convert.ToInt32(Session[SessionUserLoginFails]);
                    return PartialView(model);
                }
            }
            var user = ApiService.UserService.UserGetByEmailOrUserName(model.UserName, model.UserName);
            if (!Encryption.EncryptPassword(model.Password).Equals(user.Password))
            {
                ModelState.AddModelError("Error", LanguageHelper.GetValidation(ResourceConstants.Validation.UserNameOrPasswordInvalid));
                model.Password = null;

                return await LoginFail(model);
            }
            ViewBag.Notification = "";
            if(user.RoleId==3)
            {
                //không có quyền truy cập
                ViewBag.Notification = "Bạn không có quyền truy cập";
                return PartialView(new LoginInfo());
            }    
            if (user == null)
            {

                ModelState.AddModelError("Error", LanguageHelper.GetValidation(ResourceConstants.Validation.UserNameOrPasswordInvalid));
                model.Password = null;

                return await LoginFail(model);
            }
            //if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["IsDevelopmentEnvironment"])
            //    || !Convert.ToBoolean(ConfigurationManager.AppSettings["IsDevelopmentEnvironment"]))
            //{
            //    bool authenSuccess;

            //    // Tài khoản là admin hệ thống
            //    authenSuccess = Encryption.CheckPassword(model.Password, user.Password);

            //    if (!authenSuccess)
            //    {
            //        ModelState.AddModelError("Error", LanguageHelper.GetValidation(ResourceConstants.Validation.UserNameOrPasswordInvalid));
            //        model.Password = null;

            //        return await LoginFail(model);
            //    }
            //}

            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["IsDevelopmentEnvironment"])
             || !Convert.ToBoolean(ConfigurationManager.AppSettings["IsDevelopmentEnvironment"]))
            {
                bool authenSuccess = true;

                // Tài khoản là admin hệ thống
                //if (user.RoleId == 255)
                //{
                //    authenSuccess = Encryption.CheckPassword(model.Password, user.Password);                    
                //}
                //else // Authen công Viettel IDC
                //{
                //    //var setting = await ApiService.SettingService.GetGlobalSetting();
                //    //var url = setting.ViettelIdcToolApiUrl;

                //    authenSuccess = Encryption.CheckPassword(model.Password, user.Password);
                //    //string passEncryt = Encryption.EncryptPassword(model.Password);
                //    //authenSuccess = await ApiService.UserService.CheckAD(user);
                //}

                authenSuccess = Encryption.CheckPassword(model.Password, user.Password);
                if (!authenSuccess)
                {
                    ModelState.AddModelError("Error", LanguageHelper.GetValidation(ResourceConstants.Validation.UserNameOrPasswordInvalid));
                    model.Password = null;

                    return await LoginFail(model);
                }
            }

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim("UserId", user.Id.ToString()),
                new Claim("UserName", user.UserName ?? ""),
                new Claim("Email", user.Email)
            }, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager().SignIn(new AuthenticationProperties
            {
                IsPersistent = model.RememberMe
            }, identity);

            // reset incremental delay on successful login
            if (HttpContext.Application[Request.UserHostAddress] != null)
            {
                HttpContext.Application.Remove(Request.UserHostAddress);
            }

            // Getting New Guid
            string guid = Convert.ToString(Guid.NewGuid());
            //Storing new Guid in Session
            Session["AuthenticationToken"] = guid;
            //Adding Cookie in Browser
            Response.Cookies.Add(new HttpCookie("AuthenticationToken", guid));

            if (StringHelper.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        private async Task<ActionResult> LoginFail(LoginInfo model)
        {
            int incrementalDelay;
            // increase user login fails
            var userLoginFails = Session[SessionUserLoginFails] == null
                ? 1
                : Convert.ToInt32(Session[SessionUserLoginFails]) + 1;
            model.FailedLogins = userLoginFails;
            Session[SessionUserLoginFails] = userLoginFails;
            Session["captcha"] = null;
            model.Captcha = null;
            if (ModelState.ContainsKey("Captcha"))
                ModelState["Captcha"].Value = null;

            // increment the delay on failed login attempts
            if (HttpContext.Application[Request.UserHostAddress] == null)
            {
                incrementalDelay = 1;
            }
            else
            {
                incrementalDelay = (int)HttpContext.Application[Request.UserHostAddress] * 2;
            }
            HttpContext.Application[Request.UserHostAddress] = incrementalDelay;

            // wait for delay if there is one
            await Task.Delay(incrementalDelay * 1000);

            return PartialView("Login", model);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AuthenticationManager().SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            //Removing Session
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            System.Web.HttpContext.Current.Session.Clear();
            System.Web.HttpContext.Current.Session.Abandon();
            System.Web.HttpContext.Current.Session.RemoveAll();
            //Removing ASP.NET_SessionId Cookie
            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-10);
            }

            if (Request.Cookies["AuthenticationToken"] != null)
            {
                Response.Cookies["AuthenticationToken"].Value = string.Empty;
                Response.Cookies["AuthenticationToken"].Expires = DateTime.Now.AddMonths(-10);
            }

            return RedirectToAction("Login", "Account");
        }

        public ActionResult CaptchaImage(string prefix, bool noisy = true)
        {
            var rand = new Random((int)DateTime.Now.Ticks);
            const string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            var captcha = new StringBuilder();

            for (var i = 0; i < 6; i++)
                captcha.Append(combination[rand.Next(combination.Length)]);

            // Set encrypted captcha to session
            string captchaSessionName = string.IsNullOrWhiteSpace(prefix) ? "captcha" : prefix;
            Session[captchaSessionName] = Encryption.EncryptCaptcha(captcha.ToString().ToLower());

            //image stream
            string z;
            using (var mem = new MemoryStream())
            using (var bmp = new Bitmap(120, 50))
            using (var gfx = Graphics.FromImage(bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //add noise
                if (noisy)
                {
                    int i;
                    var pen = new Pen(Color.Yellow);
                    for (i = 1; i < 10; i++)
                    {
                        pen.Color = Color.FromArgb(
                            rand.Next(0, 255),
                            rand.Next(0, 255),
                            rand.Next(0, 255));

                        var r = rand.Next(0, 120 / 3);
                        var x = rand.Next(0, 120);
                        var y = rand.Next(0, 50);

                        gfx.DrawEllipse(pen, x - r, y - r, r, r);
                    }
                }

                //add question
                gfx.DrawString(captcha.ToString(), new Font("Tahoma", 20), Brushes.Gray, 3, 6);

                //render as Jpeg
                bmp.Save(mem, ImageFormat.Jpeg);
                z = @"data:image/png;base64," + Convert.ToBase64String(mem.GetBuffer());
            }

            return Json(z, JsonRequestBehavior.AllowGet);
        }

        #region Definitions

        //STATIC
        private const string SessionUserLoginFails = "UserLoginFails";
        private const string SessionUserCaptcha = "UserCaptcha";

        #endregion


    }
}