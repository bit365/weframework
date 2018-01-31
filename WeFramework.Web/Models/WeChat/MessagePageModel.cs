using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Models.WeChat
{
    public class MessagePageModel
    {
        public bool HasError { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }
    }
}