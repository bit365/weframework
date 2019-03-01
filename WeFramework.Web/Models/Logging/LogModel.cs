using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Models.Logging
{
    [DisplayName("日志")]
    public class LogModel
    {
        [DisplayName("编号")]
        public int ID { get; set; }

        [DisplayName("事件编号")]
        public int? EventID { get; set; }

        [DisplayName("优先级")]
        public int Priority { get; set; }

        [DisplayName("等级")]
        public string Severity { get; set; }

        [DisplayName("标题")]
        public string Title { get; set; }

        [DisplayName("时间")]
        public DateTime Timestamp { get; set; }

        [DisplayName("计算机名")]
        public string MachineName { get; set; }

        [DisplayName("分类")]
        public string Categories { get; set; }

        [DisplayName("应用程序域")]
        public string AppDomainName { get; set; }

        [DisplayName("进程编号")]
        public string ProcessID { get; set; }

        [DisplayName("进程名")]
        public string ProcessName { get; set; }

        [DisplayName("线程名")]
        public string ThreadName { get; set; }

        [DisplayName("线程编号")]
        public string ThreadId { get; set; }

        [DisplayName("消息")]
        public string Message { get; set; }

        [ScaffoldColumn(false)]
        [DisplayName("日志详情")]
        public string FormattedMessage { get; set; }
    }
}