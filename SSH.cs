using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using Renci.SshNet;
using SshNet;

namespace NetworkMonitoring
{
 
    class SSHGetConfig
    {

        public static string config;

        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        private string host;
        private string login;
        private string password;

        public SSHGetConfig(string _h, string _l, string _p)
        {
            this.host = _h;
            this.login = _l;
            this.password = _p;
        }

        public void GetConfigOnTimer(object obj, string command)
        {
            using (var client = new SshClient(host, login, password))
            {
                client.Connect();
                Form.SSHLamp.Background = new SolidColorBrush(Colors.Gray);
                var terminal = client.RunCommand(command);
                if (terminal.Result != null)
                    Form.SSHLamp.Background = new SolidColorBrush(Colors.Green);
                client.Disconnect();
                config = terminal.Result.ToString();
                
            }
        }

        public async void WriteConfig(string path, string name)
        {
            string writePath = @"\RouterConfigs\" + name;
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    Form.SSHLamp.Background = new SolidColorBrush(Colors.Gray);
                    await sw.WriteAsync(config);
                    Form.SSHLamp.Background = new SolidColorBrush(Colors.Green);
                }
                Form.SSHLamp.Background = new SolidColorBrush(Colors.Gray);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
