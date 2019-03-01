using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WeFramework.Web.Core.Mvc
{
    /// <summary> 
    /// A JsonResult for asp.net mvc that uses the Newtonsoft.Json serializer. 
    /// </summary> 
    /// <remarks> 
    /// The initial code came from this stackoverflow answer
    /// Props to @asgerhallas for his contribution! 
    /// </remarks> 
    public class JsonNetResult : System.Web.Mvc.JsonResult
    {
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            JsonSerializerSettings jsonSerializerSettings = this.JsonSerializerSettings?? new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.None, jsonSerializerSettings);

            response.Write(serializedObject);
        }
    }

}
