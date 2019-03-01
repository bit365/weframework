using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Common;
using WeFramework.Core.Domain.Security;
using WeFramework.Core.Domain.Users;

namespace WeFramework.Service.Security
{
    public interface IEntityPermissionService
    {
        bool Authorize<T>(T entity) where T : BaseEntity;

        bool Authorize(int roleID, string entityName, int entityID);

        void CreateEntityPermission(int roleID, string entityName, int entityID);

        void DeleteEntityPermission(int roleID, string entityName, int entityID);

        bool Authorize<T>(T entity, User user) where T : BaseEntity;
    }
}
