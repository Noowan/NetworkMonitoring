using System;
using System.Collections.Generic;

#nullable disable

namespace NetworkMonitoring
{
    public partial class Config
    {
        public int ConfigId { get; set; }
        public int DeviceId { get; set; }
        public string ConfigString { get; set; }

        public virtual Device Device { get; set; }
    }
}
