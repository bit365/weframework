using FluentValidation;
using Unity.Lifetime;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;
using Unity;
using WeFramework.Core.Infrastructure;

namespace WeFramework.Web.Validator
{
    public class ValidatorRegister : IDependencyRegister
    {
        public void RegisterTypes(IUnityContainer container)
        {
            ValidatorOptions.DisplayNameResolver = (type, memberInfo, lambdaExpression) =>
            {
                var displayColumnAttribute = memberInfo.GetCustomAttributes(true).OfType<DisplayAttribute>().FirstOrDefault();

                if (displayColumnAttribute != null)
                {
                    return displayColumnAttribute.Name;
                }

                var displayNameAttribute = memberInfo.GetCustomAttributes(true).OfType<DisplayNameAttribute>().FirstOrDefault();

                if (displayNameAttribute != null)
                {
                    return displayNameAttribute.DisplayName;
                }

                var resourceManager = new ResourceManager(typeof(WeFramework.Web.Properties.Resources));

                return resourceManager.GetString(type.Name + memberInfo.Name + nameof(displayNameAttribute.DisplayName));
            };

            //FluentValidation.Mvc.FluentValidationModelValidatorProvider.Configure();

            var validatorTypes = this.GetType().Assembly.GetTypes().Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IValidator<>)));

            foreach (Type instanceType in validatorTypes)
            {
                container.RegisterType(typeof(IValidator<>), instanceType, instanceType.BaseType.GetGenericArguments().First().FullName, new ContainerControlledLifetimeManager());
            }
        }
    }
}
