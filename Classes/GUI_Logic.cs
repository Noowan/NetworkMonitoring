using NetworkMonitoring.Classes;
using System;
using System.Linq;
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
            sp.Children.Add(new Button { Width = 100, Height = 30, Content = "Опция 1", Name = "Command1" });
            sp.Children.Add(new Button { Width = 100, Height = 30, Content = "Опция 2", Name = "Command2" });
            sp.Children.Add(new Button { Width = 100, Height = 30, Content = "Опция 3", Name = "Command3" });
            sp.Children.Add(new Button { Width = 100, Height = 30, Content = "Опция 4", Name = "Command4" });
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

        public void EnableSSHConfigTimer(string name, string iP, string login, string password)
        {
            //SSHGetConfig sSHGetConfig = new("192.168.1.10", "test", "test", "cat /tmp/" + name + ".txt", name);
            SSHGetConfig sSHGetConfig = new(iP, login, password, "cat /tmp/" + name + ".txt", name);
            DispatcherTimer getConfigTimer = new DispatcherTimer();
            getConfigTimer.Tick += new EventHandler(sSHGetConfig.GetConfigOnTimer);
            //getConfigTimer.Interval = new TimeSpan(0, 1, 0);
            getConfigTimer.Interval = new TimeSpan(0, 0, 10); //Debug use
            getConfigTimer.Start();
        }

        public void ShowOptions()
        {
            Options options = new Options();
            options.Show();
        }

        public void EnableSNMPGetTimer(string name, string iP, string community)
        {
            SNMP snmp = new SNMP(iP, community, name);
            DispatcherTimer snmptimer = new DispatcherTimer();
            snmptimer.Tick += new EventHandler(snmp.GetSnmpIntStatusValue);
            snmptimer.Interval = new TimeSpan(0, 0, 10);
            snmptimer.Start();
        }
    }
}
