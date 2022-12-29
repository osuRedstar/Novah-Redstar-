/*using System;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Linq;
using System.Threading.Tasks;

namespace Novah.core
{
    class HostsCore
    {
        public static string[] gethosts()
        {
            return File.ReadAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts");
        }

        public static void changehosts(string[] hosts)
        {
            try
            {
                try
                {
                    File.WriteAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts", hosts);
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("Error! \r\r\r" + ex.Message, "Novah", MessageBoxButton.YesNo, MessageBoxImage.Error) == MessageBoxResult.Yes)
                    {
                        try
                        {
                            File.WriteAllLines(Environment.SystemDirectory + @"\drivers\etc\hosts", hosts);
                        }
                        catch
                        {
                            LogCore.Log(ex);
                            MessageBox.Show("Error! \r\rPlease Send Redstar's Discord server the Switcher Logs", "Novah", MessageBoxButton.OK, MessageBoxImage.Error);
                            string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                            Process.Start(filepath);
                            Environment.Exit(0);

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogCore.Log(ex);
                MessageBox.Show("Error! \r\rPlease Send Redstar's Discord server the Switcher Logs", "Novah", MessageBoxButton.OK, MessageBoxImage.Error);
                string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                Process.Start(filepath);
                Environment.Exit(0);
            }
        }

        public static void removehosts()
        {
            string[] _hosts = gethosts();
            _hosts = _hosts.Where(x => !x.Contains(".ppy.sh")).ToArray();
            changehosts(_hosts);
        }
    }
}
public enum Server
{
    bancho, debian, redstar
}*/
