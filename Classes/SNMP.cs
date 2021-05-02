using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace NetworkMonitoring.Classes
{
    class SNMP
    {

        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        private string ip;
        private string community;
        private string name;


        public SNMP(string _i, string _c, string _n)
        {
            this.ip = _i;
            this.community = _c;
            this.name = _n;
        }

        public int GetDeviceID()
        {
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var device = db.Devices.Where(d => d.Name == name).FirstOrDefault();
                return device.DeviceId;
            }
        }


        public void GetSnmpIntStatusValue(object sender, EventArgs e)
        {
            Form.SNMPLamp.Background = new SolidColorBrush(Colors.Gray);
            var intstatus = new List<Variable>();
            Messenger.Walk(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(ip), 161),
                           new OctetString(community),
                           new ObjectIdentifier("1.3.6.1.2.1.2.2.1.8"),
                           intstatus,
                           60000,
                           WalkMode.WithinSubtree);

            var intname = new List<Variable>();
            Messenger.Walk(VersionCode.V1,
                           new IPEndPoint(IPAddress.Parse(ip), 161),
                           new OctetString(community),
                           new ObjectIdentifier("1.3.6.1.2.1.2.2.1.2"),
                           intname,
                           60000,
                           WalkMode.WithinSubtree);

            Form.WriteLamp.Background = new SolidColorBrush(Colors.Green);

            for (int j = 0; j < intstatus.Count; j++)
            {
                string stringintname = intname[j].Data.ToString();
                int int32intstatus = Convert.ToInt32(intstatus[j]);
                WriteSNMPIntStatusValueToDB(stringintname, int32intstatus);
            }

        }

        public void WriteSNMPIntStatusValueToDB(string interfacename, int intstatus)
        {
            int id = GetDeviceID();

            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                Value value = new Value { DeviceId = id, MetricName = interfacename, Value1 = intstatus, ValueDate = DateTime.Now };
                db.Values.Add(value);
            }

            Form.SNMPLamp.Background = new SolidColorBrush(Colors.Gray);
            Form.WriteLamp.Background = new SolidColorBrush(Colors.Gray);
        }

    }

}
