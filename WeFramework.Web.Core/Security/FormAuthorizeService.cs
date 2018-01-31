using System;
using System.Web;
using System.Web.Security;
using WeFramework.Core.Domain.Users;
using WeFramework.Service.Security;
using WeFramework.Service.Users;

namespace WeFramework.Web.Core.Security
{
    public class FormAuthorizeService : IAuthorizeService
    {
        private readonly IUserService userService;

        public FormAuthorizeService(IUserService userService)
        {
            this.userService = userService;
        }

        public User GetAuthorizedUser()
        {
            HttpContext httpContext = HttpContext.Current;

            if (httpContext != null && httpContext.Request != null && httpContext.Request.IsAuthenticated && (httpContext.User.Identity is FormsIdentity))
            {
                FormsIdentity formIdentity = (FormsIdentity)httpContext.User.Identity;
                string userName = formIdentity.Ticket.Name;
                string userData = formIdentity.Ticket.UserData;

                if (!string.IsNullOrWhiteSpace(userName))
                {
                    return userService.GetUser(userName);
                }
            }

            return null;
        }

        public void SignIn(User user, bool rememberMe)
        {
            string userData = Guid.NewGuid().ToString();

            var ticket = new FormsAuthenticationTicket(1, user.Name, DateTime.Now, DateTime.Now.AddDays(1), rememberMe, userData);

            string encryptedTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket) { HttpOnly = true };

            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public void SignOut()
        {
            FormsAuthentication.SignOut();
        }
    }
}
