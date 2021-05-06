using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NetworkMonitoring
{
    /// <summary>
    /// Логика взаимодействия для AlertsWindow.xaml
    /// </summary>
    public partial class AlertsWindow : Window
    {

        string comboBoxSelection;
        DateTime dateFrom;
        DateTime dateTill;

        public AlertsWindow()
        {
            InitializeComponent();

            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var comboBoxItems = db.Devices;
                foreach (var c in comboBoxItems)
                {
                    ComboBox.Items.Add(c.Name);
                }
            }
        }

        private void ViewButtonClick(object sender, MouseButtonEventArgs e)
        {
            int deviceId = GetDeviceID(comboBoxSelection);
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var alerts = db.Alerts.Where(v => v.Deviceid == deviceId && v.Alertdate >= dateFrom && v.Alertdate <= dateTill);
                datagrid.ItemsSource = alerts.ToList();
            }
        }

        public int GetDeviceID(string name)
        {
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var device = db.Devices.Where(d => d.Name == name).FirstOrDefault();
                return device.DeviceId;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            comboBoxSelection = ComboBox.SelectedItem.ToString();
        }

        private void DateFrom_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dateFrom = (DateTime)DateFrom.SelectedDate;
        }

        private void DateTill_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            dateTill = (DateTime)DateTill.SelectedDate;
            dateTill = dateTill.AddHours(23).AddMinutes(59).AddSeconds(59);
        }
    } 
}
