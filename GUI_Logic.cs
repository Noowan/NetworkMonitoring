using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NetworkMonitoring
{
    class GUI_Logic
    {
        //Даем возможность обращаться к контролам
        MainWindow Form = Application.Current.Windows[0] as MainWindow;

        public static string str_elem_name;
        public void DeleteStackpanel(string name)
        {

                UIElement elem_name = null;
            foreach (StackPanel c in Form.grid1.Children)
                if (c.Name == name)
                    elem_name = (UIElement)c;
            Form.grid1.Children.Remove(elem_name);
            

        }
    }
}
