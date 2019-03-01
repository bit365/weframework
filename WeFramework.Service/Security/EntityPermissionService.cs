using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Data;
using WeFramework.Core.Domain.Common;
using WeFramework.Core.Domain.Security;
using WeFramework.Core.Domain.Users;
using WeFramework.Core.Infrastructure;

namespace WeFramework.Service.Security
{
    public class EntityPermissionService : IEntityPermissionService
    {
        private readonly IWorkContext workContext;

        private readonly IRepository<EntityPermission> entityPermissionRepository;

        public EntityPermissionService(IWorkContext workContext, IRepository<EntityPermission> entityPermissionRepository)
        {
            this.workContext = workContext;
            this.entityPermissionRepository = entityPermissionRepository;
        }

        public bool Authorize<T>(T entity) where T : BaseEntity
        {
            return Authorize(entity, workContext.CurrentUser);
        }

        public virtual bool Authorize<T>(T entity, User user) where T : BaseEntity
        {
            var roleids = user.Roles.Select(r => r.ID).ToArray();
            var entityPermissions = entityPermissionRepository.TableNoTracking;
            return entityPermissions.Any(p => p.EntityName == typeof(T).Name && p.EntityID == entity.ID && roleids.Contains(p.RoleID));
        }

        public bool Authorize(int roleID, string entityName, int entityID)
        {
            return entityPermissionRepository.Table.Any(p => p.RoleID == roleID && p.EntityName == entityName && p.EntityID == entityID);
        }

        public void CreateEntityPermission(int roleID, string entityName, int entityID)
        {
            if (!Authorize(roleID, entityName, entityID))
            {
                entityPermissionRepository.Insert(new EntityPermission { RoleID = roleID, EntityName = entityName, EntityID = entityID });
            }
        }

        public void DeleteEntityPermission(int roleID, string entityName, int entityID)
        {
            var ep = entityPermissionRepository.Table.SingleOrDefault(p => p.RoleID == roleID && p.EntityName == entityName && p.EntityID == entityID);
            if (ep != null)
            {
                entityPermissionRepository.Delete(ep);
            }
        }
    }
}
