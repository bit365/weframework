using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Products;

namespace WeFramework.Data.Mapping
{
    public class ProductMap: EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            this.HasKey(t=>t.ID);

            this.Property(t => t.Name).IsRequired().HasMaxLength(50);
        }
    }
}
