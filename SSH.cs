using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using Renci.SshNet;

namespace NetworkMonitoring
{

    class SSHGetConfig
    {

        public static string config;

        MainWindow Form = Application.Current.Windows[0] as MainWindow;
        private string host;
        private string login;
        private string password;
        private string command;
        private string name;

        public SSHGetConfig(string _h, string _l, string _p, string _c, string _n)
        {
            this.host = _h;
            this.login = _l;
            this.password = _p;
            this.command = _c;
            this.name = _n;
        }

        public void GetConfigOnTimer(object sender, EventArgs e)
        {
            using (var client = new SshClient(host, login, password))
            {
                client.Connect();
                MainWindow.isSSHConnected = true;
                var terminal = client.RunCommand(command);
                Form.SSHLamp.Background = new SolidColorBrush(Colors.Gray);
                config = terminal.Result.ToString();
                client.Disconnect();
                Form.WriteLamp.Background = new SolidColorBrush(Colors.Green);
                WriteConfig(name);

            }
        }

        public async void WriteConfig(string name)
        {
            string writePath = "Router Configs\\" + name + ".txt";
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    await sw.WriteAsync(config);
                }
                Form.SSHLamp.Background = new SolidColorBrush(Colors.Green);
                Form.WriteLamp.Background = new SolidColorBrush(Colors.Gray);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
