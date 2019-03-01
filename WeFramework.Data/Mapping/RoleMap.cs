using System.Data.Entity.ModelConfiguration;
using WeFramework.Core.Domain.Users;

namespace WeFramework.Data.Mapping
{
    public class RoleMap : EntityTypeConfiguration<Role>
    {
        public RoleMap()
        {
            this.HasKey(t => t.ID);

            this.Property(t => t.Name).IsRequired().HasMaxLength(50);

            this.HasMany(t => t.Permissions).WithMany().Map(m => { m.ToTable("RolePermission"); m.MapLeftKey("RoleID"); m.MapRightKey("PermissionID"); });
        }
    }
}
