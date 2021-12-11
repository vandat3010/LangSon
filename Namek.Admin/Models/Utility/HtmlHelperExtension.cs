using System.Web.Mvc;
using Namek.Admin.Controllers;
using Namek.Admin.Models.ActivityLog;
using Namek.Admin.Models.Customer;
using Namek.Entity.RequestModel;

namespace Namek.Admin.Models.Utility
{
 
    public static class HtmlHelperExtensionBase
    { 

        public static MvcHtmlString BuildNextPreviousLinks(this HtmlHelper htmlHelper,
            BaseModel queryOptions, string actionName)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var output = string.Format(
                "<ul class=\"pagination\">" +
                "<li class=\"prev {0}\">{1}</li>",
                IsPreviousDisabled(queryOptions),
                BuildPreviousLink(urlHelper, queryOptions, actionName)
            );

            var numberPageShow = 5;
            var totalPages = queryOptions.TotalPages;
            var page = queryOptions.Page;

            if (queryOptions.TotalPages <= numberPageShow)
            {
                for (var i = 1; i <= totalPages; i++)
                    output += BuildPaginationLink(urlHelper, queryOptions, actionName, i);
            }
            else
            {
                if (page <= numberPageShow / 2 + 1)
                    for (var i = 1; i <= numberPageShow; i++)
                        output += BuildPaginationLink(urlHelper, queryOptions, actionName, i);
                else if (page >= totalPages - numberPageShow / 2)
                    for (var i = totalPages - numberPageShow + 1; i <= totalPages; i++)
                        output += BuildPaginationLink(urlHelper, queryOptions, actionName, i);
                else
                    for (var i = page - numberPageShow / 2; i <= page + numberPageShow / 2; i++)
                        output += BuildPaginationLink(urlHelper, queryOptions, actionName, i);
            }

            output += string.Format("<li class=\"next {0}\">{1}</li>",
                IsNextDisabled(queryOptions),
                BuildNextLink(urlHelper, queryOptions, actionName));

            output += "</ul>";
            return new MvcHtmlString(output);
        }

        public static MvcHtmlString BuildNextPreviousLinksReportChar(this HtmlHelper htmlHelper,
            ModelReportChar queryOptions, string actionName)
        {
            
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var output = string.Format(
                "<ul class=\"pagination\">" +
                "<li class=\"prev {0}\">{1}</li>",
                IsPreviousDisabledReportChar(queryOptions),
                BuildPreviousLinkReportChar(urlHelper, queryOptions, actionName)
            );

            var numberPageShow = 5;
            var totalPages = queryOptions.TotalPages;
            var page = queryOptions.Page;

            if (queryOptions.TotalPages <= numberPageShow)
            {
                for (var i = 1; i <= totalPages; i++)
                    output += BuildPaginationLinkReportChar(urlHelper, queryOptions, actionName, i);
            }
            else
            {
                if (page <= numberPageShow / 2 + 1)
                    for (var i = 1; i <= numberPageShow; i++)
                        output += BuildPaginationLinkReportChar(urlHelper, queryOptions, actionName, i);
                else if (page >= totalPages - numberPageShow / 2)
                    for (var i = totalPages - numberPageShow + 1; i <= totalPages; i++)
                        output += BuildPaginationLinkReportChar(urlHelper, queryOptions, actionName, i);
                else
                    for (var i = page - numberPageShow / 2; i <= page + numberPageShow / 2; i++)
                        output += BuildPaginationLinkReportChar(urlHelper, queryOptions, actionName, i);
            }

            output += string.Format("<li class=\"next {0}\">{1}</li>",
                IsNextDisabledReportChar(queryOptions),
                BuildNextLinkReportChar(urlHelper, queryOptions, actionName));

            output += "</ul>";
            return new MvcHtmlString(output);
        }
        private static string BuildPaginationLinkReportChar(UrlHelper urlHelper, ModelReportChar queryOptions, string actionName,
            int pageNo)
        {
            string output;
            if (pageNo == queryOptions.Page)
            {
                output = "<li class=\"active\">";
                output += string.Format("<a>{0}</a></li>", pageNo);
            }
            else
            {

                //output = "<li>";
                //output += string.Format("<a href=\"{0}&{1}\">{2}</a></li>", urlHelper.Action(actionName, queryOptions.RouteValues), "Page=" + pageNo, pageNo).Replace("?&Page", "?Page");
                output = "<li>";
                output += string.Format("<a href=\"{0}\">{1}</a></li>", urlHelper.Action(actionName, new
                {
                    Page = pageNo,
                    Id_ChartCategory= queryOptions.Id_ChartCategory,
                    Id_ChartProperties= queryOptions.Id_ChartProperties,
                    Year= queryOptions.Year
                }),
                    pageNo);

            }
            return output;
        }

        private static string IsPreviousDisabled(BaseModel queryOptions)
        {
            return queryOptions.Page == 1 ? "disabled" : string.Empty;
        }

        private static string IsPreviousDisabledReportChar(ModelReportChar queryOptions)
        {
            return queryOptions.Page == 1 ? "disabled" : string.Empty;
        }

        private static string IsNextDisabled(BaseModel queryOptions)
        {
            return queryOptions.Page == queryOptions.TotalPages ? "disabled" : string.Empty;
        }
        private static string IsNextDisabledReportChar(ModelReportChar queryOptions)
        {
            return queryOptions.Page == queryOptions.TotalPages ? "disabled" : string.Empty;
        }
        private static string BuildPreviousLinkReportChar(UrlHelper urlHelper, ModelReportChar queryOptions, string actionName)
        {
            var output = "<a ";
            if (IsPreviousDisabledReportChar(queryOptions) != "disabled")
                output += string.Format("href=\"{0}&{1}&{2}&{3}&{4}\"", urlHelper.Action(actionName, queryOptions.RouteValues), "Page=" + (queryOptions.Page - 1), 
                    "Id_ChartCategory="+ queryOptions.Id_ChartCategory, "Id_ChartProperties="+ queryOptions.Id_ChartProperties, "Year="+ queryOptions.Year).Replace("&Page", "?Page");
            output += "><i class=\"fa fa-angle-left\"></i></a>";
            return output;
        }
        private static string BuildPreviousLink(UrlHelper urlHelper, BaseModel queryOptions, string actionName)
        {
            var output = "<a ";
            if (IsPreviousDisabled(queryOptions) != "disabled")
                output += string.Format("href=\"{0}&{1}\"", urlHelper.Action(actionName, queryOptions.RouteValues), "Page=" + (queryOptions.Page - 1)).Replace("&Page", "?Page");
            output += "><i class=\"fa fa-angle-left\"></i></a>";
            return output;
        }
        private static string BuildNextLink(UrlHelper urlHelper, BaseModel queryOptions, string actionName)
        {
            var output = "<a ";
            if (IsNextDisabled(queryOptions) != "disabled")
                output += string.Format("href=\"{0}&{1}\"", urlHelper.Action(actionName, queryOptions.RouteValues), "Page=" + (queryOptions.Page + 1)).Replace("&Page", "?Page");
            output += "><i class=\"fa fa-angle-right\"></i></a>";
            return output;
        }
        private static string BuildNextLinkReportChar(UrlHelper urlHelper, ModelReportChar queryOptions, string actionName)
        {
            var output = "<a ";
            if (IsNextDisabledReportChar(queryOptions) != "disabled")
                output += string.Format("href=\"{0}&{1}&{2}&{3}&{4}\"", urlHelper.Action(actionName, queryOptions.RouteValues), "Page=" + (queryOptions.Page + 1),
                    "Id_ChartCategory=" + queryOptions.Id_ChartCategory, "Id_ChartProperties=" + queryOptions.Id_ChartProperties, "Year=" + queryOptions.Year).Replace("&Page", "?Page");
            output += "><i class=\"fa fa-angle-right\"></i></a>";
            return output;
        }
        private static string BuildPaginationLink(UrlHelper urlHelper, BaseModel queryOptions, string actionName,
            int pageNo)
        {
            string output;
            if (pageNo == queryOptions.Page)
            {
                output = "<li class=\"active\">";
                output += string.Format("<a>{0}</a></li>", pageNo);
            }
            else
            {

                //output = "<li>";
                //output += string.Format("<a href=\"{0}&{1}\">{2}</a></li>", urlHelper.Action(actionName, queryOptions.RouteValues), "Page=" + pageNo, pageNo).Replace("?&Page", "?Page");
                output = "<li>";
                output += string.Format("<a href=\"{0}\">{1}</a></li>", urlHelper.Action(actionName, new
                {
                    Page = pageNo
                }),
                    pageNo);

            }
            return output;
        }
    }



    #region Paging for ApiLog

    public static class HtmlApiLogHelperExtension
    {
        public static MvcHtmlString BuildApiLogNextPreviousLinks(this HtmlHelper htmlHelper,
            ApiLogRequestModel queryOptions, string actionName)
        {
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var output = string.Format(
                "<ul class=\"pagination\">" +
                "<li class=\"prev {0}\">{1}</li>",
                IsPreviousDisabled(queryOptions),
                BuildPreviousLink(urlHelper, queryOptions, actionName)
            );

            var numberPageShow = 5;
            var totalPages = queryOptions.TotalPages;
            var page = queryOptions.Page;

            if (queryOptions.TotalPages <= numberPageShow)
            {
                for (var i = 1; i <= totalPages; i++)
                    output += BuildPaginationLink(urlHelper, queryOptions, actionName, i);
            }
            else
            {
                if (page <= numberPageShow / 2 + 1)
                    for (var i = 1; i <= numberPageShow; i++)
                        output += BuildPaginationLink(urlHelper, queryOptions, actionName, i);
                else if (page >= totalPages - numberPageShow / 2)
                    for (var i = totalPages - numberPageShow + 1; i <= totalPages; i++)
                        output += BuildPaginationLink(urlHelper, queryOptions, actionName, i);
                else
                    for (var i = page - numberPageShow / 2; i <= page + numberPageShow / 2; i++)
                        output += BuildPaginationLink(urlHelper, queryOptions, actionName, i);
            }

            output += string.Format("<li class=\"next {0}\">{1}</li>",
                IsNextDisabled(queryOptions),
                BuildNextLink(urlHelper, queryOptions, actionName));

            output += "</ul>";
            return new MvcHtmlString(output);
        }

        private static string IsPreviousDisabled(ApiLogRequestModel queryOptions)
        {
            return queryOptions.Page == 1 ? "disabled" : string.Empty;
        }

        private static string IsNextDisabled(ApiLogRequestModel queryOptions)
        {
            return queryOptions.Page == queryOptions.TotalPages ? "disabled" : string.Empty;
        }

        private static string BuildPreviousLink(UrlHelper urlHelper, ApiLogRequestModel queryOptions, string actionName)
        {
            var fromDate = queryOptions.FromDate.HasValue ? queryOptions.FromDate.Value.ToString("dd/MM/yyyy") : null;
            var toDate = queryOptions.ToDate.HasValue ? queryOptions.ToDate.Value.ToString("dd/MM/yyyy") : null;
            var output = "<a ";
            if (IsPreviousDisabled(queryOptions) != "disabled")
                output += string.Format("href=\"{0}\"", urlHelper.Action(actionName, new
                {
                    queryOptions.UserKeywords,
                    FromDate = fromDate,
                    ToDate = toDate,
                    Page = queryOptions.Page - 1
                }));
            output += "><i class=\"fa fa-angle-left\"></i></a>";
            return output;
        }

        private static string BuildNextLink(UrlHelper urlHelper, ApiLogRequestModel queryOptions, string actionName)
        {
            var fromDate = queryOptions.FromDate.HasValue ? queryOptions.FromDate.Value.ToString("dd/MM/yyyy") : null;
            var toDate = queryOptions.ToDate.HasValue ? queryOptions.ToDate.Value.ToString("dd/MM/yyyy") : null;
            var output = "<a ";
            if (IsNextDisabled(queryOptions) != "disabled")
                output += string.Format("href=\"{0}\"", urlHelper.Action(actionName, new
                {
                    queryOptions.UserKeywords,
                    FromDate = fromDate,
                    ToDate = toDate,
                    Page = queryOptions.Page + 1
                }));
            output += "><i class=\"fa fa-angle-right\"></i></a>";
            return output;
        }

        private static string BuildPaginationLink(UrlHelper urlHelper, ApiLogRequestModel queryOptions,
            string actionName, int pageNo)
        {
            var fromDate = queryOptions.FromDate.HasValue ? queryOptions.FromDate.Value.ToString("dd/MM/yyyy") : null;
            var toDate = queryOptions.ToDate.HasValue ? queryOptions.ToDate.Value.ToString("dd/MM/yyyy") : null;
            string output;
            if (pageNo == queryOptions.Page)
            {
                output = "<li class=\"active\">";
                output += string.Format("<a>{0}</a></li>", pageNo);
            }
            else
            {
                output = "<li>";
                output += string.Format("<a href=\"{0}\">{1}</a></li>", urlHelper.Action(actionName, new
                {
                    queryOptions.UserKeywords,
                    FromDate = fromDate,
                    ToDate = toDate,
                    Page = pageNo
                }),
                    pageNo);
            }
            return output;
        }
    }

    #endregion

}