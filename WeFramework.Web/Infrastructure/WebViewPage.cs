using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Web.Properties;

namespace WeFramework.Web.Infrastructure
{
    public delegate string Localizer(string name, params object[] args);

    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private readonly Localizer localizer;

        public Localizer T { get { return localizer; } }

        public WebViewPage(Localizer localizer = null)
        {
            this.localizer = localizer ?? ((name, args) => string.Format(Resources.ResourceManager.GetString(name), args));
        }
    }
}
