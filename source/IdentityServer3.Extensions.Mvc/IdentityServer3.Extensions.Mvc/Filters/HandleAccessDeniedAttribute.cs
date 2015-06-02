using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace IdentityServer3.Extensions.Mvc.Filters
{
    public class HandleAccessDeniedAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (!filterContext.IsChildAction && !filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var statusCodeResult = filterContext.Result as HttpStatusCodeResult;
                if (statusCodeResult != null)
                {
                    string viewName = null;
                    if (statusCodeResult.StatusCode == 401)
                    {
                        viewName = "AccessDenied";
                    }
                    if (statusCodeResult.StatusCode == 403)
                    {
                        viewName = "Forbidden";
                    }
                    if (viewName != null)
                    {
                        filterContext.Result = new ViewResult()
                        {
                            ViewName = viewName
                        };
                    }
                }
            }
        }
    }
}