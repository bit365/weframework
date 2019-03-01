using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Web.Mvc;
using WeFramework.Web.Properties;

namespace WeFramework.Web.Infrastructure
{
    public class CustomModelMetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var modelMetadata = base.CreateMetadata(attributes, containerType, modelAccessor, modelType, propertyName);

            if (string.IsNullOrWhiteSpace(modelMetadata.DisplayName) && containerType != null && containerType.Assembly == typeof(Resources).Assembly)
            {
                string propertyDisplayName = Resources.ResourceManager.GetString(containerType.Name.Replace(".", string.Empty) + propertyName + nameof(modelMetadata.DisplayName));

                if (!string.IsNullOrWhiteSpace(propertyDisplayName))
                {
                    modelMetadata.DisplayName = propertyDisplayName;
                }
            }

            return modelMetadata;
        }
    }
}
