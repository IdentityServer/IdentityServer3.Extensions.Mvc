using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using IdentityServer3.Core;
using IdentityServer3.Core.Extensions;

namespace IdentityServer3.Extensions.Mvc.Filters
{
    public class IdentityServerPartialLoginAttribute : OwinAuthenticationAttribute
    {
        public IdentityServerPartialLoginAttribute()
            : base(Constants.PartialSignInAuthenticationType)
        {
            this.Order = 2;
        }

        public override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var statusCodeResult = filterContext.Result as HttpStatusCodeResult;
            if (statusCodeResult != null && statusCodeResult.StatusCode == 401)
            {
                filterContext.Result = new ViewResult()
                {
                    ViewName = "AccessDenied"
                };
            }
        }
    }
}