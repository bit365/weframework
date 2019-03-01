using AutoMapper;
using WeFramework.Core.Domain.Configuration;
using WeFramework.Core.Domain.Navigates;
using WeFramework.Web.Models;
using WeFramework.Web.Models.Navigates;
using System.Linq;
using System;

namespace WeFramework.Web.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        private readonly string MvcViewModelClassSuffixName = "Model";

        public AutoMapperProfile()
        {
            var modelTypes = this.GetType().Assembly.GetTypes().Where(t => t.Name.EndsWith(MvcViewModelClassSuffixName));

            var domainTypes = typeof(WeFramework.Core.Domain.Common.BaseEntity).Assembly.GetTypes();

            foreach (Type modelType in modelTypes)
            {
                var modelTypeRelateDomainType = domainTypes.SingleOrDefault(domainType => domainType.Name + MvcViewModelClassSuffixName == modelType.Name);
                if (modelTypeRelateDomainType != null)
                {
                    this.CreateMap(modelType, modelTypeRelateDomainType);
                    this.CreateMap(modelTypeRelateDomainType, modelType).MaxDepth(10);
                }
            }
        }
    }
}