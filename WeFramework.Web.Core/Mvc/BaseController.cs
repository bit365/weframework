using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WeFramework.Web.Core.Mvc
{
    public class BaseController : Controller
    {
        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return Json(data, contentType, contentEncoding, behavior, null);
        }

        protected JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior, JsonSerializerSettings settings)
        {
            return new JsonNetResult { Data = data, ContentType = contentType, ContentEncoding = contentEncoding, JsonRequestBehavior = behavior, JsonSerializerSettings = settings };
        }

        protected virtual JsonResult Json(object data, JsonSerializerSettings jsonSerializerSettings)
        {
            return Json(data, null, null, JsonRequestBehavior.DenyGet, jsonSerializerSettings);
        }
    }
}
