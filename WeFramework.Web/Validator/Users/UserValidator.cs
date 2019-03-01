using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WeFramework.Web.Models.Users;

namespace WeFramework.Web.Validator.Users
{
    public class UserValidator : AbstractValidator<UserModel>
    {
        public UserValidator()
        {
            RuleFor(userModel => userModel.Name).NotNull().Length(5, 20);
            RuleFor(userModel => userModel.Password).NotNull().Length(5, 20);
            RuleFor(userModel => userModel.ConfirmPassword).NotNull().Equal(m => m.Password);
        }
    }
}