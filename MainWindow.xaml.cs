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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetworkMonitoring
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        public void Window_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

            //var myButton = new Button();
            //myButton.Width = 100;
            //myButton.Height = 30;
            //myButton.Content = "Кнопка";
            //myButton.Click += mybutton_click;
            //myButton.Name = "dynamic_button";
            //TranslateTransform transform = new TranslateTransform();
            //transform.X = Mouse.GetPosition(grid1).X - 350;
            //transform.Y = Mouse.GetPosition(grid1).Y - 180;
            //myButton.RenderTransform = transform;
            //grid1.Children.Add(myButton);


            var sp = new StackPanel();
            sp.Children.Add(new TextBlock { Text = "Доступные команды", Width = 130, Height = 30, Name = "dynamic_stackpanel", Tag = "sp" });
            sp.Children.Add(new Button { Width = 100, Height = 30, Content = "Опция 1", Name = "Command1" });
            sp.Children.Add(new Button { Width = 100, Height = 30, Content = "Опция 2", Name = "Command2" });
            sp.Children.Add(new Button { Width = 100, Height = 30, Content = "Опция 3", Name = "Command3" });
            sp.Children.Add(new Button { Width = 100, Height = 30, Content = "Опция 4", Name = "Command4" });
            TranslateTransform transform1 = new TranslateTransform();
            transform1.X = Mouse.GetPosition(grid1).X - 350;
            transform1.Y = Mouse.GetPosition(grid1).Y - 100;
            sp.RenderTransform = transform1;
            sp.AddHandler(Button.ClickEvent, new RoutedEventHandler(mybutton_click2));
            grid1.Children.Add(sp);
            123
                123
        }

        private void mybutton_click2(object sender, RoutedEventArgs e)
        {
            if (e.Source.ToString().Contains("Опция 1"))
            {
                UIElement sp = null;
                foreach (StackPanel c in grid1.Children)
                    if (c.Name == "dynamic_stackpanel")
                        sp = (UIElement)c;
                grid1.Children.Remove(sp);

                //MessageBox.Show("ok1");
            }


        }

        private void mybutton_click(object sender, RoutedEventArgs e)
        {
            UIElement btn = null;
            foreach (Control c in grid1.Children)
                if (c.Name == "dynamic_button")
                    btn = (UIElement)c;
            grid1.Children.Remove(btn);
        }

    }
}
