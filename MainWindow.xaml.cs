﻿using System.Threading;
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
        static string nameDevice1 = "PE-1";
        static string nameDevice2 = "PE-2";
        static string nameDevice3 = "PE-3";
        static string nameDevice4 = "PE-4";
        static string ipDevice1 = "192.168.1.10";
        static string ipDevice2 = "192.168.1.10";
        static string ipDevice3 = "192.168.1.10";
        static string loginDevice1 = "test";
        static string loginDevice2 = "test";
        static string loginDevice3 = "test";
        static string loginDevice4 = "test";
        static string passwordDevice1 = "test";
        static string passwordDevice2 = "test";
        static string passwordDevice3 = "test";
        static string passwordDevice4 = "test";


        string name = "dynamic_stackpanel";

        public MainWindow()
        {
            InitializeComponent();

        }

        //При нажатии на роутер открывает stackbox
        public void RouterClick(object sender, MouseButtonEventArgs e)
        {
            //Создаем StackPanel с кнопками и помещаем рядом с курсором
            GUI_Logic gl = new GUI_Logic();
            gl.CreateStackPanel(name);
        }

        //Действия при нажатии на кнопку stackpanel
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
            gl.EnableSSHConfigTimer("PE-1");
            gl.EnableSSHConfigTimer("PE-2");
            gl.EnableSSHConfigTimer("PE-3");
            gl.EnableSSHConfigTimer("PE-4");
        }

        //Открыть окно опций
        private void OpenOptions(object sender, MouseButtonEventArgs e)
        {
            GUI_Logic gl = new GUI_Logic();
            gl.ShowOptions();
        }
    }




}
