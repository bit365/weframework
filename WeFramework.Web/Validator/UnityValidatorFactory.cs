using FluentValidation;
using Microsoft.Practices.Unity;
using System;
using System.Linq;
using WeFramework.Core.Infrastructure;

namespace WeFramework.Web.Validator
{
    public class UnityValidatorFactory : ValidatorFactoryBase
    {
        private readonly IUnityContainer container;

        public UnityValidatorFactory(IUnityContainer container)
        {
            this.container = container;
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return container.TryResolve(validatorType, validatorType.GetGenericArguments().First().FullName) as IValidator;
        }
    }
}
