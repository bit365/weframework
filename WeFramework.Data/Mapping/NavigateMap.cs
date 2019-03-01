using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using WeFramework.Core.Domain.Navigates;

namespace WeFramework.Data.Mapping
{
    public class NavigateMap : EntityTypeConfiguration<Navigate>
    {
        public NavigateMap()
        {
            this.HasKey(t => t.ID).Property(t => t.ID).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Name).HasMaxLength(20).IsRequired();

            this.Property(t => t.ControllerName).HasMaxLength(50).IsOptional();

            this.Property(t => t.ActionName).IsOptional().HasMaxLength(50);

            this.Property(t => t.IconClassCode).IsOptional().HasMaxLength(50);

            this.HasOptional(t=>t.Parent);
        }
    }
}
