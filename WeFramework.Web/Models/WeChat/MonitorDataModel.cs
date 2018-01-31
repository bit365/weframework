using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeFramework.Web.Models.WeChat
{
    public class MonitorDataModel
    {
        public string ProvinceName { get; set; }

        public int MonitorID { get; set; }

        public string MonitorName { get; set; }

        public bool IsOnline { get; set; }

        public bool IsWarn { get; set; }

        public string Location { get; set; }

        public IEnumerable<MonitorSensorValueModel> SensorValues { get; set; }
    }

    public class MonitorSensorValueModel
    {
        public int SensorID { get; set; }

        public string SensorName { get; set; }

        public double? Value { get; set; }

        public string SensorUnit { get; set; }

        public DateTime CreateDate { get; set; }

        public bool? IsWarn { get; set; }
    }
}