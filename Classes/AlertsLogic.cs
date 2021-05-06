using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace NetworkMonitoring.Classes
{
    class AlertsLogic
    {
        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        private string deviceName;

        public AlertsLogic(string _d)
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

        public async void CheckIfLastDataChanged(object sender, EventArgs e)
        {
            if (deviceName == "PE-1") { Form.pos1Status.Content = "OK"; Form.pos1Status.Foreground = Brushes.Green; }
            if (deviceName == "PE-2") { Form.pos2Status.Content = "OK"; Form.pos2Status.Foreground = Brushes.Green; }
            if (deviceName == "PE-3") { Form.pos3Status.Content = "OK"; Form.pos3Status.Foreground = Brushes.Green; }
            if (deviceName == "PE-4") { Form.pos4Status.Content = "OK"; Form.pos4Status.Foreground = Brushes.Green; }
            string[,] lastData = GetMetricNamesAndValues();
            await Task.Delay(7000);
            string[,] lastDataPlus15Sec = GetMetricNamesAndValues();
            List<string> alertdata = new List<string>();

            for (int i = 0; i < lastData.Length / 2; i++)
            {
                if (lastData[0, i] == lastDataPlus15Sec[0, i])
                {
                    if (lastData[1, i] != lastDataPlus15Sec[1, i])
                    {
                        if (lastDataPlus15Sec[0, i].Contains("Temperature"))
                        {
                            if (Convert.ToInt32(lastDataPlus15Sec[1, i]) > 50)
                            {
                                alertdata.Add("High temperature");
                                break;
                            }
                        }
                        if (lastDataPlus15Sec[0, i].Contains("GigabitEthernet0/1"))
                        {
                            alertdata.Add($"{lastDataPlus15Sec[0, i]} changed");
                            //Добавить изменение цвета линии
                            break;
                        }
                        if (lastDataPlus15Sec[0, i].Contains("0/2"))
                        {
                            alertdata.Add($"{lastDataPlus15Sec[0, i]} changed");
                            //Добавить изменение цвета линии
                            break;
                        }
                        {
                            alertdata.Add($"{lastDataPlus15Sec[0, i]} changed");
                        }

                    }
                }
            }

            string[] alertarray = alertdata.ToArray();
            string alertmessage = null;
            foreach (string a in alertarray)
            {
                alertmessage = String.Concat(alertmessage, $";{a};");
                using (NetworkMonitoringContext db = new NetworkMonitoringContext())
                {
                    int deviceId = GetDeviceID(deviceName);
                    Alert alert = new Alert { Deviceid = deviceId, Alertstring = a, Alertdate = DateTime.Now };
                    db.Alerts.Add(alert);
                    db.SaveChanges();
                }
            }

            if (alertmessage != null)
            {
                if (deviceName == "PE-1") { Form.pos1Status.Content = alertmessage; Form.pos1Status.Foreground = Brushes.Red; }
                if (deviceName == "PE-2") { Form.pos2Status.Content = alertmessage; Form.pos2Status.Foreground = Brushes.Red; }
                if (deviceName == "PE-3") { Form.pos3Status.Content = alertmessage; Form.pos3Status.Foreground = Brushes.Red; }
                if (deviceName == "PE-4") { Form.pos4Status.Content = alertmessage; Form.pos4Status.Foreground = Brushes.Red; }
            }

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

                string[,] masMetricValuesAndNames = new string[2, metricNames.Count()];
                int i = 0;
                foreach (var m in metricNames)
                {
                    masMetricValuesAndNames[0, i] = m.Key;
                    i++;
                }

                for (i = 0; i < masMetricValuesAndNames.Length / 2; i++)
                {
                    var metricValue = db.Values.OrderByDescending(u => u.ValueDate).Where(m => m.MetricName == masMetricValuesAndNames[0, i]).FirstOrDefault();
                    masMetricValuesAndNames[1, i] = metricValue.Value1.ToString();
                }

                return masMetricValuesAndNames;


            }

        }

        public void EnableAlertCheckTimer()
        {

            DispatcherTimer getConfigTimer = new DispatcherTimer();
            getConfigTimer.Tick += new EventHandler(CheckIfLastDataChanged);
            getConfigTimer.Interval = new TimeSpan(0, 0, 25);
            getConfigTimer.Start();
        }

    }
}
