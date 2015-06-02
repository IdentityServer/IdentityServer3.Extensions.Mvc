using IdentityServer3.Extensions.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace IdentityServer3.Extensions.Mvc.Filters
{
    public abstract class OwinAuthenticationAttribute : FilterAttribute, IAuthenticationFilter
    {
        public string AuthenticationType { get; set; }

        protected OwinAuthenticationAttribute(string authenticationType)
        {
            if (String.IsNullOrWhiteSpace(authenticationType)) throw new ArgumentNullException("authenticationType");

            AuthenticationType = authenticationType;
        }

        public virtual void OnAuthentication(AuthenticationContext filterContext)
        {
            var ctx = filterContext.HttpContext.Request.GetOwinContext();
            var result = AsyncHelper.RunSync(() => ctx.Authentication.AuthenticateAsync(AuthenticationType));
            if (result != null &&
                result.Identity != null &&
                result.Identity.IsAuthenticated)
            {
                filterContext.Principal = new ClaimsPrincipal(result.Identity);
            }
        }

        public abstract void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext);
    }
}
