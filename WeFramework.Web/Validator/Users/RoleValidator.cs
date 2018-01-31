using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeFramework.Web.Models.Users;

namespace WeFramework.Web.Validator.Users
{
    public class RoleValidator: AbstractValidator<RoleModel>
    {
        public RoleValidator()
        {
            RuleFor(roleModel => roleModel.Name).NotNull().Length(2,15);
        }
    }
}