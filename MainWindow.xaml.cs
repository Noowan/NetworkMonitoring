using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace NetworkMonitoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        static string nameDevice1 = null;
        static string nameDevice2 = null;
        static string nameDevice3 = null;
        static string nameDevice4 = null;

        string name = "dynamic_stackpanel";

        public MainWindow()
        {
            InitializeComponent();

        }


        public void RouterClick(object sender, MouseButtonEventArgs e)
        {
            //Создаем StackPanel с кнопками и помещаем рядом с курсором
            GUI_Logic gl = new GUI_Logic();
            gl.CreateStackPanel(name);
        }

        public void StackPanel_ButtonClick(object sender, RoutedEventArgs e)
        {
            GUI_Logic gl = new GUI_Logic();
            if (e.Source.ToString().Contains("Опция 1"))
            {
                gl.DeleteStackPanel(name);
            }

            if (e.Source.ToString().Contains("Опция 2"))
            {
                gl.DeleteStackPanel(name);
            }

            if (e.Source.ToString().Contains("Опция 3"))
            {
                gl.DeleteStackPanel(name);
            }

            if (e.Source.ToString().Contains("Опция 4"))
            {
                gl.DeleteStackPanel(name);
            }
        }

        private void AboutClick(object sender, MouseButtonEventArgs e)
        {
            GUI_Logic gl = new GUI_Logic();
            gl.ShowAbout();
        }

        private void AboutAuthorClick(object sender, MouseButtonEventArgs e)
        {
            GUI_Logic gl = new GUI_Logic();
            gl.ShowAboutAuthor();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            GUI_Logic gl = new();
            gl.EnableSSHConfigTimer("PE-1");
            gl.EnableSSHConfigTimer("PE-2");
            gl.EnableSSHConfigTimer("PE-3");
            gl.EnableSSHConfigTimer("PE-4");
        }

        private void OpenOptions(object sender, MouseButtonEventArgs e)
        {
            GUI_Logic gl = new GUI_Logic();
            gl.ShowOptions();
        }
    }




}
