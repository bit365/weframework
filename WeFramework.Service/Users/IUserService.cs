using WeFramework.Core.Domain.Users;
using WeFramework.Core.Paging;

namespace WeFramework.Service.Users
{
    public interface IUserService
    {
        void CreateUser(User user);

        void UpdateUser(User user);

        User GetUser(string userName);

        User GetUser(int id);

        void DeleteUser(User user);

        void DeleteUser(int id);

        bool ValidateUser(string userName, string password);

        User GetUserByWeChatOpenID(string weChatOpenId);

        IPagedList<User> GetUsers(string keyword, int pageNumber, int pageSize);
    }
}
