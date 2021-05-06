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
using LiveCharts;
using LiveCharts.Wpf;

namespace NetworkMonitoring
{
    /// <summary>
    /// Логика взаимодействия для Graph.xaml
    /// </summary>
    public partial class Graph : Window
    {
        string pressedDevice;
        int deviceId;
        string comboBoxSelection;
        DateTime dateFrom;
        DateTime dateTill;

        public Graph()
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
               var values = db.Values.Where(v => v.DeviceId == deviceId && v.MetricName == comboBoxSelection && v.ValueDate >= dateFrom && v.ValueDate <= dateTill);

                //foreach (var v in values)
                //    if(v.MetricName.Contains("Ethernet"))
                //    {
                //        sly.MaxValue = 3;
                //        sly.MinValue = 1;
                //    }
                
                LineSeries line1 = new LineSeries();
                line1.Title = comboBoxSelection;
                Labels = new List<string>();
                foreach (var item in values)
                {
                    Labels.Add(item.ValueDate.ToString());
                }
                int i = 0;
                int[] temp = new int[values.Count()];
                foreach(var v in values)
                {
                    temp[i] = Convert.ToInt32(v.Value1);
                    i++;
                }
                line1.Values = new ChartValues<int>(temp);
                seriesCollection = new SeriesCollection();
                seriesCollection.Add(line1);

                DataContext = this;
                this.sl.Series = seriesCollection;
                slx.Labels = Labels;

            
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
            dateTill = dateTill.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public SeriesCollection seriesCollection { get; set; }
        public List<string> Labels { get; set; }
    }
}
