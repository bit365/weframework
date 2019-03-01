using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Data;
using WeFramework.Core.Domain.Navigates;
using WeFramework.Core.Domain.Security;
using WeFramework.Core.Infrastructure;

namespace WeFramework.Service.Navigates
{
    public class NavigateService : INavigateService
    {
        private readonly IRepository<Navigate> navigateRepository;

        public NavigateService(IRepository<Navigate> navigateRepository)
        {
            this.navigateRepository = navigateRepository;
        }

        public void CreateNavigate(Navigate navigate)
        {
            navigateRepository.Insert(navigate);
        }

        public void Delete(int id)
        {
            Navigate navigate = navigateRepository.GetById(id);

            if (navigate.Children.Any())
            {
                foreach (var nav in navigate.Children.ToList())
                {
                    Delete(nav.ID);
                }
            }
            navigateRepository.Delete(navigate);
        }

        public Navigate GetNavigate(int id)
        {
            return navigateRepository.GetById(id);
        }

        public List<Navigate> GetNavigates()
        {
            return navigateRepository.Table.ToList();
        }

        public void Update(Navigate navigate)
        {
            navigateRepository.Update(navigate);
        }
    }
}
