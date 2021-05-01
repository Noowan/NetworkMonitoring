using System;
using System.Collections.Generic;

#nullable disable

namespace NetworkMonitoring
{
    public partial class Credential
    {
        public int CredentialsId { get; set; }
        public int DeviceId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual Device Device { get; set; }
    }
}
