using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace NetworkMonitoring.Classes
{
    class Alerts
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        private string deviceName;

        public Alerts(string _d)
        {
            this.deviceName = _d;
        }

        public int GetDeviceID(string deviceName)
        {
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var device = db.Devices.Where(d => d.Name == deviceName).FirstOrDefault();
                return device.DeviceId;
            }
        }

        public void CheckIfLastDataChanged(object sender, EventArgs e)
        {
            string[,] lastData = GetMetricNamesAndValues();
            Thread.Sleep(15000);
            string[,] lastDataPlus15Sec = GetMetricNamesAndValues();
        }




        public string[,] GetMetricNamesAndValues()
        {
            int deviceId = GetDeviceID(deviceName);
            
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var metricNames = from v in db.Values
                                    where v.DeviceId == deviceId
                                    group v by v.MetricName into g
                                    select new
                                    {
                                        g.Key
                                    };
                //string[] masMetricNames = new string[metricNames.Count()];
                //string[] masMetricValues = new string[metricNames.Count()];

                string[,] masMetricValuesAndNames = new string[2,metricNames.Count()];
                int i = 0;
                foreach (var m in metricNames)
                {
                    masMetricValuesAndNames[0,i] = m.Key;
                    i++;
                }

                for (i=0; i< masMetricValuesAndNames.Length / 2;i++)
                {
                    var metricValue = db.Values.OrderByDescending(u => u.ValueDate).Where(m => m.MetricName == masMetricValuesAndNames[0,i]).FirstOrDefault();
                    masMetricValuesAndNames[1,i] = metricValue.Value1.ToString();
                }

                return masMetricValuesAndNames;


            }

        }

        public void EnableAlertCheckTimer()
        {

            DispatcherTimer getConfigTimer = new DispatcherTimer();
            getConfigTimer.Tick += new EventHandler(CheckIfLastDataChanged);
            getConfigTimer.Interval = new TimeSpan(0, 0, 15);
            getConfigTimer.Start();
        }

    }
}
