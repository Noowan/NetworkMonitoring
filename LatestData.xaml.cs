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
        public LatestData()
        {
            InitializeComponent();

        }


        private void ViewButtonClick(object sender, MouseButtonEventArgs e)
        {
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var values = db.Values.Where(v => v.DeviceId == 7 && v.MetricName.Contains("0/1")) ;
                datagrid.ItemsSource = values.ToList();
            } 
                

        }
    }

}
