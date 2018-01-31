using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Logging;

namespace WeFramework.Data.Mapping
{
    public class LogMap : EntityTypeConfiguration<Log>
    {
        public LogMap()
        {
            this.Property(t => t.Severity).IsRequired().HasMaxLength(32);
            this.Property(t => t.Title).HasMaxLength(256);
            this.Property(t => t.MachineName).HasMaxLength(32);
            this.Property(t => t.Categories).HasMaxLength(64).IsRequired();
            this.Property(t => t.AppDomainName).HasMaxLength(512);
            this.Property(t => t.ProcessID).HasMaxLength(256);
            this.Property(t => t.ProcessName).HasMaxLength(512);
            this.Property(t => t.ThreadName).HasMaxLength(512);
            this.Property(t => t.ThreadId).HasMaxLength(128);
            this.Property(t => t.Message).HasMaxLength(1500);
            this.Property(t => t.FormattedMessage).HasColumnType("ntext");
        }
    }
}
