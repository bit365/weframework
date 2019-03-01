using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WeFramework.Web.Core.Paging
{
    public static class PagerUrlHelperExtensions
    {
        public static string FormParameters(this System.Web.Mvc.UrlHelper urlHelper, string url)
        {
            var request = urlHelper.RequestContext.HttpContext.Request;

            UriBuilder uriBuilder = new UriBuilder(new Uri(request.Url, url));

            var queryParameters = HttpUtility.ParseQueryString(uriBuilder.Query);

            if (string.Equals(request.HttpMethod, HttpMethod.Get.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                request.QueryString.CopyTo(queryParameters, false);
            }

            if (string.Equals(request.HttpMethod, HttpMethod.Post.Method, StringComparison.InvariantCultureIgnoreCase))
            {
                request.Form.CopyTo(queryParameters, false);
            }

            RemoveValueIsNullOrEmptyEntries(queryParameters);

            uriBuilder.Query = queryParameters.ToString();

            return uriBuilder.Uri.PathAndQuery;
        }

        public static void CopyTo(this NameValueCollection collection, NameValueCollection destination, bool replaceEntries)
        {
            foreach (string key in collection.AllKeys)
            {
                if (replaceEntries || !destination.AllKeys.Contains(key))
                {
                    destination[key] = collection[key];
                }
            }
        }

        private static void RemoveValueIsNullOrEmptyEntries(NameValueCollection collection)
        {
            foreach (string key in collection.AllKeys)
            {
                if (string.IsNullOrEmpty(collection[key]))
                {
                    collection.Remove(key);
                }
            }
        }
    }
}
