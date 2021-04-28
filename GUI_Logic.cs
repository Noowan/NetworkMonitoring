using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NetworkMonitoring
{
    class GUI_Logic
    {
        //Даем возможность обращаться к контролам
        MainWindow Form = Application.Current.Windows[0] as MainWindow;

        public void DeleteStackPanel(string name)
        {
            UIElement foundStackPanel = Form.grid1.Children.OfType<StackPanel>().Where(x => x.Name.ToString() == name).FirstOrDefault();
            Form.grid1.Children.Remove(foundStackPanel);
        }

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
    }
}
