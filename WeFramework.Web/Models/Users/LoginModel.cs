namespace WeFramework.Web.Models.Users
{
    public class LoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///记住密码
        /// </summary>
        public bool RememberMe { get; set; }
    }
}