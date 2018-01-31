using System;

namespace WeFramework.Web.Infrastructure
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ExcelSheetAttribute : Attribute
    {
        /// <summary>
        /// 工作表名称
        /// </summary>
        public string Name { get; set; } = "Sheet1";

        /// <summary>
        /// 表格行高
        /// </summary>
        public double RowHeight { get; set; } = 28;
    }
}
