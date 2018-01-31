using FluentValidation.Mvc;
using System.Web.Mvc;
using WeFramework.Web.Validator;
using System.Linq;
using WeFramework.Web.Infrastructure;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WeFramework.Web.App_Start.ExtensionsActivator), "Start")]
namespace WeFramework.Web.App_Start
{
    public static class ExtensionsActivator
    {
        public static void Start()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            UnityValidatorFactory factory = new UnityValidatorFactory(UnityConfig.GetConfiguredContainer());

            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(factory));
            ModelMetadataProviders.Current = new CustomModelMetadataProvider();

            var clientDataTypeValidator = ModelValidatorProviders.Providers.OfType<ClientDataTypeModelValidatorProvider>().FirstOrDefault();
            if (clientDataTypeValidator != null)
            {
                ModelValidatorProviders.Providers.Remove(clientDataTypeValidator);
            }
            ModelValidatorProviders.Providers.Add(new CustomClientDataTypeModelValidatorProvider());
        }
    }
}
