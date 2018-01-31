using System;

namespace WeFramework.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ExcelColumnAttribute : Attribute
    {
        /// <summary>
        /// 单元格格式化
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// 单引号前缀
        /// </summary>
        public bool Quote { get; set; }

        /// <summary>
        /// 水平居中
        /// </summary>
        public bool IsCenter { get; set; }

        /// <summary>
        /// 列排序号
        /// </summary>
        public int Sort { get; set; }
    }
}
