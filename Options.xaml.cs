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

        //Закрыть форму
        private void CloseButton_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        //Записать данные о устройстве в позиции 1
        private void Pos1Button_Click(object sender, MouseButtonEventArgs e)
        {

            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var device = db.Devices.Where(d => d.WindowPosition == 1).FirstOrDefault();

                if (device.WindowPosition == 1)
                {
                    device.Name = Pos1Name.Text;
                    device.Ipaddress = Pos1IP.Text;
                    device.Name = MainWindow.nameDevice1;
                    device.Ipaddress = MainWindow.ipDevice1;
                }

                var credential = db.Credentials.Where(d => d.Device.WindowPosition == 1).FirstOrDefault();
                if (credential.Device.WindowPosition == 1)
                {
                    credential.Login = Pos1Login.Text;
                    credential.Password = Pos1Pass.Text;
                    credential.Password = MainWindow.passwordDevice1;
                    credential.Password = MainWindow.ipDevice1;
                }

                db.SaveChanges();
                MessageBox.Show("Сохранено");

            }
        }

        //Записать данные о устройстве в позиции 2
        private void Pos2Button_Click(object sender, MouseButtonEventArgs e)
        {
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var device = db.Devices.Where(d => d.WindowPosition == 2).FirstOrDefault();

                if (device.WindowPosition == 2)
                {
                    device.Name = Pos2Name.Text;
                    device.Ipaddress = Pos2IP.Text;
                    device.Name = MainWindow.nameDevice2;
                    device.Ipaddress = MainWindow.ipDevice2;
                }

                var credential = db.Credentials.Where(d => d.Device.WindowPosition == 2).FirstOrDefault();
                if (credential.Device.WindowPosition == 2)
                {
                    credential.Login = Pos2Login.Text;
                    credential.Password = Pos2Pass.Text;
                    credential.Password = MainWindow.passwordDevice2;
                    credential.Password = MainWindow.ipDevice2;
                }

                db.SaveChanges();
                MessageBox.Show("Сохранено");
            }
        }

        //Записать данные о устройстве в позиции 3
        private void Pos3Button_Click(object sender, MouseButtonEventArgs e)
        {
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var device = db.Devices.Where(d => d.WindowPosition == 3).FirstOrDefault();

                if (device.WindowPosition == 3)
                {
                    device.Name = Pos3Name.Text;
                    device.Ipaddress = Pos3IP.Text;
                    device.Name = MainWindow.nameDevice3;
                    device.Ipaddress = MainWindow.ipDevice3;
                }

                var credential = db.Credentials.Where(d => d.Device.WindowPosition == 3).FirstOrDefault();
                if (credential.Device.WindowPosition == 3)
                {
                    credential.Login = Pos3Login.Text;
                    credential.Password = Pos3Pass.Text;
                    credential.Password = MainWindow.passwordDevice3;
                    credential.Password = MainWindow.ipDevice3;
                }

                db.SaveChanges();
                MessageBox.Show("Сохранено");
            }
        }

        //Записать данные о устройстве в позиции 3
        private void Pos4Button_Click(object sender, MouseButtonEventArgs e)
        {
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var device = db.Devices.Where(d => d.WindowPosition == 4).FirstOrDefault();

                if (device.WindowPosition == 4)
                {
                    device.Name = Pos4Name.Text;
                    device.Ipaddress = Pos4IP.Text;
                    device.Name = MainWindow.nameDevice4;
                    device.Ipaddress = MainWindow.ipDevice4;
                }

                var credential = db.Credentials.Where(d => d.Device.WindowPosition == 4).FirstOrDefault();
                if (credential.Device.WindowPosition == 4)
                {
                    credential.Login = Pos4Login.Text;
                    credential.Password = Pos4Pass.Text;
                    credential.Password = MainWindow.passwordDevice4;
                    credential.Password = MainWindow.ipDevice4;
                }

                db.SaveChanges();
                MessageBox.Show("Сохранено");
            }
        }
    }
}
