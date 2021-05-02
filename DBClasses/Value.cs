using System;
using System.Collections.Generic;

#nullable disable

namespace NetworkMonitoring
{
    public partial class Value
    {
        public int ValueId { get; set; }
        public int DeviceId { get; set; }
        public int MetricId { get; set; }
        public string MetricName { get; set; }
        public int? Value1 { get; set; }
        public string ValueStr { get; set; }

        public virtual Device Device { get; set; }
    }
}
