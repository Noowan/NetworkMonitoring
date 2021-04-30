﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkMonitoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {

        public static bool isSSHConnected = false;
        public static bool isSNMPConnected = false;

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
            SSHGetConfig sSHGetConfig = new("192.168.1.10", "test", "test", "cat /tmp/PE-1", "PE-1");
            TimerCallback sshConfigTimerCallback = new(sSHGetConfig.GetConfigOnTimer);
            Timer timer = new(sshConfigTimerCallback, null, 1000, 30000);

        }
    }




}
