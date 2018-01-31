using FluentValidation;
using WeFramework.Web.Models;
using WeFramework.Web.Models.Configuration;

namespace WeFramework.Web.Validator.Configuration
{
    public class SettingValidator : AbstractValidator<SettingModel>
    {
        public SettingValidator()
        {
            RuleFor(setting => setting.Name).NotNull().Length(5, 10);
            RuleFor(setting => setting.Value).NotNull().Length(0, 10);
        }
    }
}
