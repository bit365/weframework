using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WaterMonitor.Web.Core.Mvc;

namespace WaterMonitor.Web.Controllers
{
    public class TestController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}