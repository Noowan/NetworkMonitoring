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
            using (NetworkMonitoringContext db = new NetworkMonitoringContext())
            {
                // получаем объекты из бд и выводим на консоль
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
                }
            }

        }

        private void CloseButton_Click(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Pos1Button_Click(object sender, MouseButtonEventArgs e)
        {

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
