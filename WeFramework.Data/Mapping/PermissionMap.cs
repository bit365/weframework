using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WeFramework.Core.Domain.Security;

namespace WeFramework.Data.Mapping
{
    public class PermissionMap : EntityTypeConfiguration<Permission>
    {
        public PermissionMap()
        {
            this.HasKey(t => t.ID);
            this.Property(t => t.Name).HasMaxLength(50);
            this.Property(t => t.Category).HasMaxLength(50);
            this.Ignore(t => t.Implies);
            this.Property(t => t.Description).IsRequired().HasMaxLength(20); ;
        }
    }
}
