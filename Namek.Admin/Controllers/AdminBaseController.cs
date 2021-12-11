using Namek.Resources.MultiLanguage;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using Namek.Admin.CustomFilters;
using Namek.Admin.Models;
using Namek.Admin.Utilities;
using Namek.Core.Utility;
using Namek.Library.Config;
using Namek.Library.Enums;
using Namek.Library.Infrastructure;
using Namek.LogServices.Logging;

namespace Namek.Admin.Controllers
{
    [Authorize]
    [AuthenticateUser]
    public class AdminBaseController : BaseController
    {
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            ViewBag.User = CurrentUser;
        }

        public JsonResult RespondSuccess()
        {
            return RespondSuccess(null);
        }

        public JsonResult RespondSuccess(dynamic additionalData)
        {
            //SuccessNotification(VerboseReporter.GetSuccessList());

            return CustomResponse(new RootResponseModel
            {
                Success = true,
                Code = HttpStatusCode.OK,
                //ErrorMessages = VerboseReporter.GetErrorsList(),
                //Messages = VerboseReporter.GetSuccessList(),
                ResponseData = additionalData
                //Total = total
            });
        }

        public JsonResult RespondSuccess(dynamic additionalData, int total)
        {
            //SuccessNotification(VerboseReporter.GetSuccessList());

            return CustomResponse(new RootResponseModel
            {
                Success = true,
                Code = HttpStatusCode.OK,
                //ErrorMessages = VerboseReporter.GetErrorsList(),
                //Messages = VerboseReporter.GetSuccessList(),
                ResponseData = additionalData,
                Total = total
            });
        }

        public JsonResult RespondSuccess(string successMessage, string contextName, dynamic additionalData = null)
        {
            //VerboseReporter.ReportSuccess(successMessage, contextName);
            return RespondSuccess(additionalData);
        }

        public JsonResult RespondFailure()
        {
            return RespondFailure(null);
        }

        public JsonResult RespondFailure(string errorMessage, string contextName, dynamic additionalData = null)
        {
            //VerboseReporter.ReportError(errorMessage, contextName);
            return RespondFailure(additionalData);
        }

        public JsonResult RespondFailure(dynamic additionalData)
        {
            //ErrorNotification(VerboseReporter.GetErrorsList());

            return CustomResponse(new RootResponseModel
            {
                Success = false,
                Code = HttpStatusCode.ExpectationFailed,
                //ErrorMessages = VerboseReporter.GetErrorsList(),
                //Messages = VerboseReporter.GetSuccessList(),
                ResponseData = additionalData
            });
        }

        protected JsonResult ReportRequest()
        {
            return CustomResponse(new RootResponseModel
            {
                Success = false,
                Code = HttpStatusCode.BadRequest,
                ErrorMessages = LanguageHelper.GetLabel(ResourceConstants.Label.AdminBaseNotFindAnyDataMatching),
                //Messages = VerboseReporter.GetSuccessList()
            });
        }

        protected JsonResult BadRequest()
        {
            return CustomResponse(new RootResponseModel
            {
                Success = false,
                Code = HttpStatusCode.BadRequest,
                ErrorMessages = LanguageHelper.GetLabel(ResourceConstants.Label.AdminBaseInvalidData),
                //Messages = VerboseReporter.GetSuccessList()
            });
        }

        public JsonResult CustomResponse(dynamic obj)
        {
            //return Json(obj);
            return new JsonDotNetResult
            {
                Data = obj
            };
        }

        /// <summary>
        ///     Log exception
        /// </summary>
        /// <param name="exc">Exception</param>
        protected void LogException(Exception exc)
        {
            var logger = EngineContext.Current.Resolve<ILogService>();

            //var customer = workContext.CurrentCustomer;
            logger.Error(exc.Message, exc); //CurrentUser
        }

        /// <summary>
        ///     Display success notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void SuccessNotification(string message, bool persistForTheNextRequest = true)
        {
            
        }

        /// <summary>
        ///     Display error notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void ErrorNotification(string message, bool persistForTheNextRequest = true)
        {
         
        }

        /// <summary>
        ///     Display error notification
        /// </summary>
        /// <param name="exception">Exception</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        /// <param name="logException">A value indicating whether exception should be logged</param>
        protected virtual void ErrorNotification(Exception exception, bool persistForTheNextRequest = true,
            bool logException = true)
        {
           
        }

        /// <summary>
        ///     Display warning notification
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void WarningNotification(string message, bool persistForTheNextRequest = true)
        {
        
        }

        /// <summary>
        ///     Display notification
        /// </summary>
        /// <param name="type">Notification type</param>
        /// <param name="message">Message</param>
        /// <param name="persistForTheNextRequest">A value indicating whether a message should be persisted for the next request</param>
        protected virtual void AddNotification(  string message, bool persistForTheNextRequest)
        {
             
        }


       
    }
}