using System.Data.Entity.ModelConfiguration;
using WeFramework.Core.Domain.Configuration;

namespace WeFramework.Data.Mapping
{
    public class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            this.HasKey(t => t.Name);

            this.Property(t => t.Name).IsRequired().HasMaxLength(30);

            this.Property(t => t.Value).HasMaxLength(100);

            this.Property(t => t.Remark).HasMaxLength(20);
        }
    }
}
