﻿using System;
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

        public static string str_elem_name;
        public void DeleteStackPanel(string name)
        {

            UIElement elem_name = null;
            foreach (StackPanel c in Form.grid1.Children)
                if (c.Name == name)
                    elem_name = (UIElement)c;
            Form.grid1.Children.Remove(elem_name);
        }

        public void CreateStackPanel()
        {
            var sp = new StackPanel();
            sp.Name = "dynamic_stackpanel";
            GUI_Logic.str_elem_name = sp.Name;
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
            Form.grid1.Children.Add(sp);
        }
    }
}
