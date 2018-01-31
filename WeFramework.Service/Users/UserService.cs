using System;
using System.Linq;
using WeFramework.Core.Data;
using WeFramework.Core.Domain.Users;
using WeFramework.Core.Paging;
using WeFramework.Service.Security;

namespace WeFramework.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> userRepository;

        private readonly IEncryptionService encryptionService;

        public UserService(IRepository<User> userRepository, IEncryptionService encryptionService)
        {
            this.userRepository = userRepository;
            this.encryptionService = encryptionService;
        }

        public void CreateUser(User user)
        {
            user.Password = encryptionService.HashPassword(user.Password);
            userRepository.Insert(user);
        }

        public User GetUser(string userName)
        {
            return userRepository.Table.SingleOrDefault(u => u.Name == userName);
        }

        public bool ValidateUser(string userName, string password)
        {
            User user = this.GetUser(userName);

            return user != null && encryptionService.VerifyHashedPassword(user.Password, password);
        }

        public void DeleteUser(User user)
        {
            userRepository.Delete(user);
        }

        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }

        public IPagedList<User> GetUsers(string keyword, int pageNumber, int pageSize)
        {
            var users = userRepository.Table;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                users = users.Where(m => m.Name.Contains(keyword));
            }

            return users.ToPagedList(m => m.ID, pageNumber, pageSize);
        }

        public User GetUser(int id)
        {
            return userRepository.GetById(id);
        }

        public void DeleteUser(int id)
        {
            DeleteUser(userRepository.GetById(id));
        }

        public User GetUserByWeChatOpenID(string weChatOpenId)
        {
           return userRepository.Table.SingleOrDefault(u=>u.WeChatOpenID==weChatOpenId);
        }
    }
}
