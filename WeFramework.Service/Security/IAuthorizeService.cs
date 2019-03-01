using WeFramework.Core.Domain.Users;

namespace WeFramework.Service.Security
{
    public interface IAuthorizeService
    {
        void SignIn(User user, bool rememberMe);

        void SignOut();

        User GetAuthorizedUser();
    }
}
