using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeFramework.Core.Domain.Common;

namespace WeFramework.Core.Domain.Logging
{
    public class Log : BaseEntity
    {
        /// <summary>
        /// 事件编号
        /// </summary>
        public int? EventID { get; set; }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 错误级别
        /// </summary>
        public string Severity { get; set; }

        /// <summary>
        /// 附加标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 计算机名
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        ///归档名称
        /// </summary>
        public string Categories { get; set; }

        /// <summary>
        /// 应用程序域
        /// </summary>
        public string AppDomainName { get; set; }

        /// <summary>
        /// 进程编号
        /// </summary>
        public string ProcessID { get; set; }

        /// <summary>
        /// 进程名
        /// </summary>

        public string ProcessName { get; set; }

        /// <summary>
        /// 线程名
        /// </summary>
        public string ThreadName { get; set; }

        /// <summary>
        /// 线程编号
        /// </summary>
        public string ThreadId { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string FormattedMessage { get; set; }
    }
}
