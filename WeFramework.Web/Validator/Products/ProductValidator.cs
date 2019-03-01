using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeFramework.Web.Models.Products;

namespace WeFramework.Web.Validator.Products
{
    public class ProductValidator: AbstractValidator<ProductModel>
    {
        public ProductValidator()
        {
            RuleFor(t => t.Name).NotNull().Length(5, 20);
            RuleFor(t => t.Price).GreaterThanOrEqualTo(10);
        }
    }
}