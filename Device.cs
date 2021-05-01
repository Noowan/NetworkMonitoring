using System;
using System.Collections.Generic;

#nullable disable

namespace NetworkMonitoring
{
    public partial class Device
    {
        public Device()
        {
            Configs = new HashSet<Config>();
            Credentials = new HashSet<Credential>();
            Values = new HashSet<Value>();
        }

        public int DeviceId { get; set; }
        public string Name { get; set; }
        public string Ipaddress { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public int WindowPosition { get; set; }

        public virtual ICollection<Config> Configs { get; set; }
        public virtual ICollection<Credential> Credentials { get; set; }
        public virtual ICollection<Value> Values { get; set; }
    }
}
