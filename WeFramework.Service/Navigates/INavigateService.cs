using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Navigates;

namespace WeFramework.Service.Navigates
{
    public interface INavigateService
    {
        void CreateNavigate(Navigate navigate);

        Navigate GetNavigate(int id);

        List<Navigate> GetNavigates();

        void Delete(int id);

        void Update(Navigate navigate);
    }
}
