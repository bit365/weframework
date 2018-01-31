using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFramework.Web.Properties;

namespace WeFramework.Web.Validator
{
    public class CustomClientDataTypeModelValidatorProvider : ClientDataTypeModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            var allValidators = base.GetValidators(metadata, context);
            var validators = new List<ModelValidator>();
            foreach (var v in allValidators)
            {
                if (v.GetType().Name == "NumericModelValidator")
                {
                    NumericAttribute attribute = new NumericAttribute { ErrorMessage = Resources.FieldMustBeNumeric };
                    DataAnnotationsModelValidator validator = new DataAnnotationsModelValidator(metadata, context, attribute);
                    validators.Add(validator);
                }

                else if (v.GetType().Name == "DateModelValidator")
                {
                    DateAttribute attribute = new DateAttribute { ErrorMessage = Resources.FieldMustBeDate };
                    DataAnnotationsModelValidator validator = new DataAnnotationsModelValidator(metadata, context, attribute);
                    validators.Add(validator);
                }
                else
                {
                    validators.Add(v);
                }
            }
            return validators;
        }

        public class NumericAttribute : ValidationAttribute, IClientValidatable
        {
            public override bool IsValid(object value)
            {
                return true;
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                yield return new ModelClientValidationRule { ValidationType = "number", ErrorMessage = this.FormatErrorMessage(metadata.DisplayName) };
            }
        }

        public class DateAttribute : ValidationAttribute, IClientValidatable
        {
            public override bool IsValid(object value)
            {
                return true;
            }

            public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
            {
                yield return new ModelClientValidationRule { ValidationType = "date", ErrorMessage = this.FormatErrorMessage(metadata.DisplayName) };
            }
        }
    }
}