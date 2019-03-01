using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Common;

namespace WeFramework.Core.Domain.Products
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }

        public double Price { get; set; }

    }
}
