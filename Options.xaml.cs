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
    public partial class Options : Window
    {
        public Options()
        {
            InitializeComponent();
            //Поставить везде null, если в базе нет значений
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var devices = db.Devices.ToList();
                if (devices.Count == 0)
                {
                    Pos1Name.Text = "null";
                    Pos1IP.Text = "null";
                    Pos1Login.Text = "null";
                    Pos1Pass.Text = "null";
                    Pos2Name.Text = "null";
                    Pos2IP.Text = "null";
                    Pos2Login.Text = "null";
                    Pos2Pass.Text = "null";
                    Pos3Name.Text = "null";
                    Pos3IP.Text = "null";
                    Pos3Login.Text = "null";
                    Pos3Pass.Text = "null";
                    Pos4Name.Text = "null";
                    Pos4IP.Text = "null";
                    Pos4Login.Text = "null";
                    Pos4Pass.Text = "null";
                    return;
                }
            }
            //Подставить значения для элемента на позиции 1
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var devices = db.Devices.Join(db.Credentials,
                    d => d.DeviceId,
                    c => c.DeviceId,
                    (d, c) => new
                    {
                        Name = d.Name,
                        Ip = d.Ipaddress,
                        Login = c.Login,
                        Pass = c.Password,
                        WindowPos = d.WindowPosition
                    }).Where(p => p.WindowPos == 1);
                foreach (var d in devices)
                {
                    Pos1Name.Text = d.Name;
                    Pos1IP.Text = d.Ip;
                    Pos1Pass.Text = d.Pass;
                    Pos1Login.Text = d.Login;
                }
            }
            //Подставить значения для элемента на позиции 2
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var devices = db.Devices.Join(db.Credentials,
                    d => d.DeviceId,
                    c => c.DeviceId,
                    (d, c) => new
                    {
                        Name = d.Name,
                        Ip = d.Ipaddress,
                        Login = c.Login,
                        Pass = c.Password,
                        WindowPos = d.WindowPosition
                    }).Where(p => p.WindowPos == 2);
                foreach (var d in devices)
                {
                    Pos2Name.Text = d.Name;
                    Pos2IP.Text = d.Ip;
                    Pos2Pass.Text = d.Pass;
                    Pos2Login.Text = d.Login;
                }
            }
            //Подставить значения для элемента на позиции 3
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var devices = db.Devices.Join(db.Credentials,
                    d => d.DeviceId,
                    c => c.DeviceId,
                    (d, c) => new
                    {
                        Name = d.Name,
                        Ip = d.Ipaddress,
                        Login = c.Login,
                        Pass = c.Password,
                        WindowPos = d.WindowPosition
                    }).Where(p => p.WindowPos == 3);
                foreach (var d in devices)
                {
                    Pos3Name.Text = d.Name;
                    Pos3IP.Text = d.Ip;
                    Pos3Pass.Text = d.Pass;
                    Pos3Login.Text = d.Login;
                }
            }
            //Подставить значения для элемента на позиции 4
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var devices = db.Devices.Join(db.Credentials,
                    d => d.DeviceId,
                    c => c.DeviceId,
                    (d, c) => new
                    {
                        Name = d.Name,
                        Ip = d.Ipaddress,
                        Login = c.Login,
                        Pass = c.Password,
                        WindowPos = d.WindowPosition
                    }).Where(p => p.WindowPos == 4);
                foreach (var d in devices)
                {
                    Pos4Name.Text = d.Name;
                    Pos4IP.Text = d.Ip;
                    Pos4Pass.Text = d.Pass;
                    Pos4Login.Text = d.Login;
                }
            }
        }

        private void CloseButton_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Pos1Button_Click(object sender, MouseButtonEventArgs e)
        {
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {

            }
        }
        private void Pos2Button_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Pos3Button_Click(object sender, MouseButtonEventArgs e)
        {

        }

        private void Pos4Button_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
