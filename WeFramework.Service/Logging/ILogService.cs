using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Logging;
using WeFramework.Core.Paging;

namespace WeFramework.Service.Logging
{
    public interface ILogService
    {
        void CreateLog(Log log);

        void DeleteLog(int id);

        Log GetLog(int id);

        IPagedList<Log> GetLogs(DateTime? startDate = null, DateTime? endDate = null, string severity = null, int pageNumber = 1, int pageSize = ushort.MaxValue);

        void ClearLogs(DateTime? startDate = null, DateTime? endDate = null, string severity = null);
    }
}
