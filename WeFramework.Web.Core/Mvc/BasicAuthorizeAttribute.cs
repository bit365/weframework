using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WeFramework.Web.Core.Mvc
{
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        public const string AuthorizationHeaderName = "Authorization";

        public const string WwwAuthenticationHeaderName = "WWW-Authenticate";

        public const string BasicAuthenticationScheme = "Basic";

        public string UserName { get; set; }

        public string Password { get; set; }

        public BasicAuthorizeAttribute(string userName, string password)
        {
            this.UserName = userName;
            this.Password = password;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if (!string.IsNullOrWhiteSpace(filterContext.HttpContext.Request.Headers[AuthorizationHeaderName]))
            {
                string rawValue = filterContext.RequestContext.HttpContext.Request.Headers[AuthorizationHeaderName];
                string[] authenticationHeaderSplit = rawValue.Split(' ');
                var token = new AuthenticationHeaderValue(authenticationHeaderSplit[0], authenticationHeaderSplit[1]);
                if (token != null && token.Scheme == BasicAuthenticationScheme)
                {
                    string credential = Encoding.Default.GetString(Convert.FromBase64String(token.Parameter));

                    if (string.Equals(credential, string.Format("{0}:{1}", UserName, Password))) { return; }
                }
            }

            HandleUnauthorizedRequest(filterContext);
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            AuthenticationHeaderValue challenge = new AuthenticationHeaderValue(BasicAuthenticationScheme);
            filterContext.HttpContext.Response.Headers[WwwAuthenticationHeaderName] = challenge.ToString();
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}
