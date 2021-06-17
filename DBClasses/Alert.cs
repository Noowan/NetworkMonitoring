using System;

#nullable disable

namespace NetworkMonitoring
{
    public partial class Alert
    {
        public int Alertid { get; set; }
        public int Deviceid { get; set; }
        public string Alertstring { get; set; }
        public DateTime Alertdate { get; set; }

        public virtual Device Device { get; set; }
    }
}
