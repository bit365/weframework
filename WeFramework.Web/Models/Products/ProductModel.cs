using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Models.Products
{
    [DisplayName("产品")]
    public class ProductModel
    {
        [DisplayName("编号")]
        public int ID { get; set; }

        [DisplayName("名称")]
        public string Name { get; set; }

        [DisplayName("价格")]
        public double Price { get; set; }
    }
}