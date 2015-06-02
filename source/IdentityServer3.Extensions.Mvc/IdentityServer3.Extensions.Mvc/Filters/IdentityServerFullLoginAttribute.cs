using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;
using IdentityServer3.Core.Models;

namespace IdentityServer3.Extensions.Mvc.Filters
{
    public class IdentityServerFullLoginAttribute : OwinAuthenticationAttribute
    {
        public IdentityServerFullLoginAttribute()
            : base(Constants.PrimaryAuthenticationType)
        {
            this.Order = 1;
        }

        public override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var statusCodeResult = filterContext.Result as HttpStatusCodeResult;
            if (statusCodeResult != null && statusCodeResult.StatusCode == 401)
            {
                var ctx = filterContext.HttpContext.Request.GetOwinContext();
                var url = ctx.Environment.CreateSignInRequest(new SignInMessage
                {
                    ReturnUrl = filterContext.HttpContext.Request.Url.AbsoluteUri
                });
                filterContext.Result = new RedirectResult(url);
            }
        }
    }
}