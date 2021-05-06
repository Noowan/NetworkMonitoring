using NetworkMonitoring.Classes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace NetworkMonitoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        public static string nameDevice1 = "PE-1";
        public static string nameDevice2 = "PE-2";
        public static string nameDevice3 = "PE-3";
        public static string nameDevice4 = "PE-4";
        public static string ipDevice1 = "192.168.1.250";
        public static string ipDevice2 = "192.168.1.250";
        public static string ipDevice3 = "192.168.1.250";
        public static string ipDevice4 = "192.168.1.250";
        public static string loginDevice1 = "test";
        public static string loginDevice2 = "test";
        public static string loginDevice3 = "test";
        public static string loginDevice4 = "test";
        public static string passwordDevice1 = "test";
        public static string passwordDevice2 = "test";
        public static string passwordDevice3 = "test";
        public static string passwordDevice4 = "test";
        public static string whichPressed = null;
        public static bool isSNMPAvailable = true;
        public static bool isICMPAvailable = true;


        string name = "dynamic_stackpanel";

        public MainWindow()
        {
            InitializeComponent();
            AlertsLogic a1 = new(nameDevice1);
            a1.EnableAlertCheckTimer();
            AlertsLogic a2 = new(nameDevice2);
            a2.EnableAlertCheckTimer();
            AlertsLogic a3 = new(nameDevice3);
            a3.EnableAlertCheckTimer();
            AlertsLogic a4 = new(nameDevice4);
            a4.EnableAlertCheckTimer();


        }

        //При нажатии на роутер открывает stackbox
        public void RouterClick(object sender, MouseButtonEventArgs e)
        {
            //Создаем StackPanel с кнопками и помещаем рядом с курсором
            GUI_Logic gl = new GUI_Logic();
            gl.CreateStackPanel(name);
            whichPressed = (sender as Button).Name;
            

        }

        //Действия при нажатии на кнопку stackpanel
        public void StackPanel_ButtonClick(object sender, RoutedEventArgs e)
        {
            GUI_Logic gl = new GUI_Logic();
            if (e.Source.ToString().Contains("Проверить доступность"))
            {
                gl.DeleteStackPanel(name);
                gl.CheckAvailability(whichPressed);
            }

            if (e.Source.ToString().Contains("Последние данные"))
            {
                gl.DeleteStackPanel(name);
                LatestData ld = new();
                ld.Show();
                
            }

            if (e.Source.ToString().Contains("Посмотреть конфигурацию"))
            {
                gl.DeleteStackPanel(name);
                ConfigWindow cw = new();
                cw.Show();
            }

            if (e.Source.ToString().Contains("Посмотреть графики"))
            {
                gl.DeleteStackPanel(name);
                Graph g = new();
                g.Show();
            }
        }

        //Показать окно about
        private void AboutClick(object sender, MouseButtonEventArgs e)
        {
            GUI_Logic gl = new GUI_Logic();
            gl.ShowAbout();
        }

        //Показать окно об авторе
        private void AboutAuthorClick(object sender, MouseButtonEventArgs e)
        {
            GUI_Logic gl = new GUI_Logic();
            gl.ShowAboutAuthor();
        }

        //Включить опрос при нажатии чекбокса
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            GUI_Logic gl = new();
            gl.EnableSSHConfigTimer(nameDevice1, ipDevice1, loginDevice1, passwordDevice1);
            gl.EnableSSHConfigTimer(nameDevice2, ipDevice2, loginDevice2, passwordDevice2);
            gl.EnableSSHConfigTimer(nameDevice3, ipDevice3, loginDevice3, passwordDevice3);
            gl.EnableSSHConfigTimer(nameDevice4, ipDevice4, loginDevice4, passwordDevice4);

        }

        //Открыть окно опций
        private void OpenOptions(object sender, MouseButtonEventArgs e)
        {
            GUI_Logic gl = new GUI_Logic();
            gl.ShowOptions();
        }

        private void OpenAlerts(object sender, MouseButtonEventArgs e)
        {
            AlertsWindow aw = new AlertsWindow();
            aw.Show();
        }
    }




}
