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
    public partial class ConfigWindow : Window
    {
        string pressedDevice = null;
        int deviceId;
        public ConfigWindow()
        {
            if (MainWindow.whichPressed == "R1") { pressedDevice = MainWindow.nameDevice1; }
            if (MainWindow.whichPressed == "R2") { pressedDevice = MainWindow.nameDevice2; }
            if (MainWindow.whichPressed == "R3") { pressedDevice = MainWindow.nameDevice3; }
            if (MainWindow.whichPressed == "R4") { pressedDevice = MainWindow.nameDevice4; }
            InitializeComponent();

            deviceId = GetDeviceID();

            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var config = db.Configs.Where(d => d.Device.DeviceId == deviceId).FirstOrDefault();
                richtextbox.Document.Blocks.Clear();
                richtextbox.Document.Blocks.Add(new Paragraph(new Run(config.ConfigString)));
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
    }
}
