using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class LatestData : Window
    {
        string pressedDevice;
        int deviceId;
        string comboBoxSelection;
        DateTime dateFrom;
        DateTime dateTill;
        public LatestData()
        {

            InitializeComponent();
            if (MainWindow.whichPressed == "R1") { pressedDevice = MainWindow.nameDevice1; }
            if (MainWindow.whichPressed == "R2") { pressedDevice = MainWindow.nameDevice2; }
            if (MainWindow.whichPressed == "R3") { pressedDevice = MainWindow.nameDevice3; }
            if (MainWindow.whichPressed == "R4") { pressedDevice = MainWindow.nameDevice4; }
            deviceId = GetDeviceID();

            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var comboBoxItems = from v in db.Values
                                    where v.DeviceId == deviceId
                                    group v by v.MetricName into g
                                    select new
                                    {
                                        g.Key
                                    };

                foreach (var c in comboBoxItems)
                {
                    ComboBox.Items.Add(c.Key);
                }
            }
        }


        private void ViewButtonClick(object sender, MouseButtonEventArgs e)
        {
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                //var values1 = db.Values.Where(v => v.DeviceId == 7 && v.MetricName.Contains("0/1"));
                //datagrid.ItemsSource = values1.ToList();
                var values = db.Values.Where(v => v.DeviceId == deviceId && v.MetricName == comboBoxSelection && v.ValueDate >= dateFrom && v.ValueDate <= dateTill);
                datagrid.ItemsSource = values.ToList();
            }
        }

        public int GetDeviceID()
        {
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var device = db.Devices.Where(d => d.Name == pressedDevice).FirstOrDefault();
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
        }
    }

}
