using System.Data.Entity.ModelConfiguration;
using WeFramework.Core.Domain.Users;

namespace WeFramework.Data.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasKey(t => t.ID);

            this.Property(t => t.Name).HasMaxLength(20);

            this.Property(t => t.Password).IsRequired().HasMaxLength(128);

            this.Property(t => t.Remark).HasMaxLength(20);

            this.Property(t => t.WeChatOpenID).HasMaxLength(28);

            this.HasMany(t => t.Roles).WithMany().Map(m => { m.ToTable("UserRole"); m.MapLeftKey("UserID"); m.MapRightKey("RoleID"); });
        }
    }
}
