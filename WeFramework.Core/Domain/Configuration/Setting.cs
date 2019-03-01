using WeFramework.Core.Domain.Common;

namespace WeFramework.Core.Domain.Configuration
{
    /// <summary>
    /// 系统设置信息
    /// </summary>
    public partial class Setting 
    {
        /// <summary>
        ///设置信息标识
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 设置信息值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 设置信息备注
        /// </summary>
        public string Remark { get; set; }
    }
}
