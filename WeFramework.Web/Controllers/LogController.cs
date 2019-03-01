using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeFramework.Core.Domain.Logging;
using WeFramework.Core.Paging;
using WeFramework.Service.Logging;
using WeFramework.Web.Core.Mvc;
using WeFramework.Web.Core.Security;
using WeFramework.Web.Models.Logging;

namespace WeFramework.Web.Controllers
{
    [ActionAuthorize]
    public class LogController : BaseController
    {
        private readonly ILogService logService;

        private readonly IMapper mapper;

        public LogController(ILogService logService, IMapper mapper)
        {
            this.logService = logService;
            this.mapper = mapper;
        }

        public ActionResult Index(DateTime? startDate = null, DateTime? endDate = null, string severity = null, int page = 1)
        {
            ViewBag.severity = new SelectList(Enum.GetNames(typeof(System.Diagnostics.TraceEventType)));
            IPagedList<Log> logs = logService.GetLogs(startDate, endDate, severity, page, 15);
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Log, LogModel>().ForMember(m => m.FormattedMessage, p => p.Ignore()));
            var logModels = config.CreateMapper().Map<IEnumerable<Log>, IEnumerable<LogModel>>(logs);
            var viewModel = new StaticPagedList<LogModel>(logModels, logs.GetMetaData());
            return Request.IsAjaxRequest() ? (ActionResult)PartialView("LogListPartial", viewModel) : View(viewModel);
        }

        [HttpPost]
        [ActionAuthorize("LogIndex")]
        public ActionResult Delete(int[] check)
        {
            check = check ?? new int[0];
            Array.ForEach(check, id => logService.DeleteLog(id));
            return RedirectToAction("Index");
        }

        [ActionAuthorize("LogIndex")]
        public ActionResult Details(int id)
        {
            return View(mapper.Map<Log, LogModel>(logService.GetLog(id)));
        }
    }
}