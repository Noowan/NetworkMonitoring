using Lextm.SharpSnmpLib;
using Lextm.SharpSnmpLib.Messaging;
using NetworkMonitoring.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace NetworkMonitoring
{

    class GUI_Logic
    {

        //Даем возможность обращаться к контролам
        MainWindow Form = Application.Current.Windows[0] as MainWindow;

        //Удаление stackpanel
        public void DeleteStackPanel(string name)
        {
            UIElement foundStackPanel = Form.grid2.Children.OfType<StackPanel>().Where(x => x.Name.ToString() == name).FirstOrDefault();
            Form.grid2.Children.Remove(foundStackPanel);
        }

        //Создание stackpanel
        public void CreateStackPanel(string name)
        {
            var sp = new StackPanel();
            sp.Name = name;

            sp.Children.Add(new TextBlock { Text = "Доступные команды", Width = 130, Height = 30 });
            sp.Children.Add(new Button { Width = 200, Height = 30, Content = "Проверить доступность", Name = "Command1" });
            sp.Children.Add(new Button { Width = 200, Height = 30, Content = "Последние данные", Name = "Command2" });
            sp.Children.Add(new Button { Width = 200, Height = 30, Content = "Опция 3", Name = "Command3" });
            sp.Children.Add(new Button { Width = 200, Height = 30, Content = "Опция 4", Name = "Command4" });
            TranslateTransform transform1 = new TranslateTransform();
            transform1.X = Mouse.GetPosition(Form.grid1).X - 350;
            transform1.Y = Mouse.GetPosition(Form.grid1).Y - 100;
            sp.RenderTransform = transform1;
            sp.AddHandler(Button.ClickEvent, new RoutedEventHandler(Form.StackPanel_ButtonClick));
            Form.grid2.Children.Add(sp);
        }

        public void ShowAbout()
        {
            AboutWindow aboutWindow = new AboutWindow();
            aboutWindow.Show();
        }

        public void ShowAboutAuthor()
        {
            AuthorInfo aboutAuthorWindow = new AuthorInfo();
            aboutAuthorWindow.Show();
        }

        //Запуск таймера на выгрузку конфига по SSH
        public void EnableSSHConfigTimer(string name, string iP, string login, string password)
        {
            //SSHGetConfig sSHGetConfig = new("192.168.1.10", "test", "test", "cat /tmp/" + name + ".txt", name);
            SSHGetConfig sSHGetConfig = new(iP, login, password, "sh run", name);
            DispatcherTimer getConfigTimer = new DispatcherTimer();
            getConfigTimer.Tick += new EventHandler(sSHGetConfig.GetConfigOnTimer);
            //getConfigTimer.Interval = new TimeSpan(0, 1, 0);
            getConfigTimer.Interval = new TimeSpan(0, 0, 60); //Debug use
            getConfigTimer.Start();
        }

        public void ShowOptions()
        {
            Options options = new Options();
            options.Show();
        }

        //Запуск таймера на получение статусов интерфейсов по SNMP
        public void EnableSNMPIntStatusGetTimer(string name, string iP, string community)
        {
            SNMP snmp = new SNMP(iP, community, name);
            DispatcherTimer snmptimer = new DispatcherTimer();
            snmptimer.Tick += new EventHandler(snmp.GetSnmpIntStatusValue);
            snmptimer.Interval = new TimeSpan(0, 0, 30);
            snmptimer.Start();
        }

        //Запуск таймера на получение фиктивных значений температуры 
        public void EnableSNMPFakeTempGetTimer(string name, string iP, string community)
        {
            SNMP snmp = new SNMP(iP, community, name);
            DispatcherTimer snmptimer = new DispatcherTimer();
            snmptimer.Tick += new EventHandler(snmp.GetFakeSNMPTempValue);
            snmptimer.Interval = new TimeSpan(0, 0, 30);
            snmptimer.Start();
        }

        //Проверка доступности по ICMP и SNMP
        public void CheckAvailability(string pressedButtonName)
        {
            string pressedDevice = null;

            if (pressedButtonName == "R1") { pressedDevice = MainWindow.nameDevice1; }
            if (pressedButtonName == "R2") { pressedDevice = MainWindow.nameDevice2; }
            if (pressedButtonName == "R3") { pressedDevice = MainWindow.nameDevice3; }
            if (pressedButtonName == "R4") { pressedDevice = MainWindow.nameDevice4; }


            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                var device = db.Devices.Where(d => d.Name == pressedDevice).FirstOrDefault();

                try
                {
                    var result = Messenger.Get(VersionCode.V1,
                          new IPEndPoint(IPAddress.Parse(device.Ipaddress), 161),
                          new OctetString("test"),
                          new List<Variable> { new Variable(new ObjectIdentifier("1.3.6.1.2.1.1.1.0")) },
                          10000);
                }
                catch
                {
                    MainWindow.isSNMPAvailable = false;
                }

                Ping pingSender = new();
                PingReply reply = pingSender.Send(IPAddress.Parse(device.Ipaddress), 10000);
                if (reply.Status.ToString() != "Success") { MainWindow.isICMPAvailable = false; }
                MessageBox.Show($"{pressedDevice}:\nSNMP - {MainWindow.isSNMPAvailable.ToString()}\nICMP- {MainWindow.isSNMPAvailable.ToString()}");

            }
        }
    }
}
