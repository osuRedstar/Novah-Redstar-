using Novah.core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Reflection;
using System.IO;

namespace Novah
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        #region variables
        string ver = "2.2";
        public static bool isCuttingEdge = false;
        public static string certthumbprint = "350fce0f2a489229928e2ec7dc2b426b49192f16";
        string GetServerIP = PhpCore.php("https://debian.moe/static/switcher/ip.txt");
        string GetTServerIP = PhpCore.php("https://debian.moe/static/switcher/testip.txt");
        string ServerIP = "null";

        bool state = false;
        bool clickable = true;
        Stopwatch stopwatch = new Stopwatch();
        BitmapImage[] frames = new BitmapImage[30];
        BitmapImage on = new BitmapImage(new Uri("pack://application:,,,/assets/img/Frame29.png"));
        BitmapImage off = new BitmapImage(new Uri("pack://application:,,,/assets/img/Frame0.png"));
        BitmapImage returnb = new BitmapImage(new Uri("pack://application:,,,/assets/close_default.png"));
        BitmapImage exitb = new BitmapImage(new Uri("pack://application:,,,/assets/close_hover.png"));
         
        Thread imgThread;
        #endregion

        #region FormEvents
        public MainWindow()
        {
            InitializeComponent();
            CuttingEdge.Osuroot();

            CheckServer();
            GetServer();
            changefilename();
            imageframe();
            if (isCuttingEdge)
            {
                FormCuttingEdge frm2 = new FormCuttingEdge();
                Hide();
                frm2.Show();
            }
        }

        public void changefilename()
        {
            string filename = System.IO.Path.GetFileName(Assembly.GetEntryAssembly().Location);
            string kakao = "Novah.exe";
            if (filename != kakao)
            {
                string path = Environment.CurrentDirectory;
                FileInfo fileRename = new FileInfo(path + @"\" + kakao);
                try
                {
                    Process[] processList = Process.GetProcessesByName("Novah");
                    if (processList.Length > 0)
                    {
                        processList[0].Kill();
                    }
                    fileRename.Delete();
                    FileInfo fi = new FileInfo(path + @"\" + filename);
                    fi.MoveTo(path + @"\" + kakao);
                }
                catch { }
            }
        }

        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.Source = exitb;
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            exit.Source = returnb;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void imageframe()
        {
            for (int i = 0; i < 30; i++)
            {
                frames[i] = new BitmapImage(new Uri("pack://application:,,,/assets/img/Frame" + i.ToString() + ".png"));
            }

            img.MouseLeftButtonDown += BtnChange;
        }

        private void Border_Initialized(object sender, EventArgs e)
        {
            try
            {
                UpdateCore.version(ver);
                if (UpdateCore.verchk == "1")
                {
                    if (GetServerIP != "404 page not found")
                    {
                        ServerIP = GetServerIP;
                    }
                    if (GetServerIP == "404 page not found")
                    {
                        ServerIP = "49.165.136.97";
                    }
                }
                if (UpdateCore.verchk == "0")
                {
                    UpdateForm frm = new UpdateForm();
                    Hide();
                    ShowInTaskbar = false;
                    frm.Show();
                }
            }
            catch (Exception ex)
            {
                LogCore.Log(ex);

                MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "Novah", MessageBoxButton.OK, MessageBoxImage.Error);
                string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                Process.Start(filepath);
                Environment.Exit(0);
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
        #endregion

        #region Events
        private void CheckServer()
        {
            bool isDebian = HostsCore.gethosts().Any(x => x.Contains("osu.ppy.sh") && !x.Contains("#"));
            test.Text = (isDebian ? "You're Connected to Debian." : "You're Connected to Bancho.");
        }

        private void SwitchON()
        {
            try
            {
                CertificateCore.UninstallCertificates();
                List<string> hosts = HostsCore.gethosts().ToList();
                hosts.RemoveAll(x => x.Contains(".ppy.sh"));
                HostsCore.changehosts(hosts.ToArray());
                string[] osu_domains = new string[]
                {
                        "osu.ppy.sh",
                        "c.ppy.sh",
                        "c1.ppy.sh",
                        "c2.ppy.sh",
                        "c3.ppy.sh",
                        "c4.ppy.sh",
                        "c5.ppy.sh",
                        "c6.ppy.sh",
                        "ce.ppy.sh",
                        "a.ppy.sh",
                        "i.ppy.sh",
                };
                hosts = HostsCore.gethosts().ToList();
                foreach (string domain in osu_domains)
                {
                    hosts.Add(ServerIP + " " + domain);
                }
                HostsCore.changehosts(hosts.ToArray());

                CertificateCore.InstallCertificate();

            }
            catch (Exception ex)
            {
                LogCore.Log(ex);

                MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "NOVAH", MessageBoxButton.OK, MessageBoxImage.Error);
                string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                Process.Start(filepath);
                Environment.Exit(0);
            }
        }

        private void SwitchOFF()
        {
            try
            {
                string[] _hosts = HostsCore.gethosts();
                _hosts = _hosts.Where(x => !x.Contains(".ppy.sh")).ToArray();
                HostsCore.changehosts(_hosts);

                CertificateCore.UninstallCertificates();
            }
            catch (Exception ex)
            {
                LogCore.Log(ex);

                MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "NOVAH", MessageBoxButton.OK, MessageBoxImage.Error);
                string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                Process.Start(filepath);
                Environment.Exit(0);
            }
        }

        private void GetServer()
        {
            try
            {
                bool ServerChk = HostsCore.gethosts().Any(x => x.Contains("osu.ppy.sh") && !x.Contains("#"));
                string serverChk2 = Convert.ToString(ServerChk);
                if (ServerChk == true)
                {
                    img.Source = on;
                    state = true;
                    border_sub.Opacity = 100;

                }
                if (ServerChk != true)
                {
                    img.Source = off;
                    state = false;
                    border_sub.Opacity = 0;
                }
            }
            catch (Exception ex)
            {
                LogCore.Log(ex);

                MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "Novah", MessageBoxButton.OK, MessageBoxImage.Error);
                string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                Process.Start(filepath);
                Environment.Exit(0);
            }
        }
        #endregion

        #region Click Events
        private void BtnChange(object sender, RoutedEventArgs e)
        {
            state = !state;
            if (clickable)
            {
                imgThread = new Thread(() =>
                {
                    clickable = false;
                    for (int i = 0; i < 30; i++)
                    {
                        stopwatch.Start();

                        Dispatcher.Invoke(new Action(() =>
                        {
                            img.Source = frames[state ? i : 29 - i];
                            border_sub.Opacity = state ? i / 30.0 : 1 - i / 30.0;
                            //offtest.Opacity = state ? i / 30.0 : 1 - i / 30.0;

                        }));

                        stopwatch.Stop();
                        Thread.Sleep(stopwatch.ElapsedMilliseconds <= 33 ? 33 - (int)stopwatch.ElapsedMilliseconds : 0);
                        stopwatch.Reset();
                    }
                    Dispatcher.Invoke(new Action(() =>
                    {
                        test.Text = state ? "You're Connected to Debian." : "You're Connected To Bancho.";

                    }));
                    clickable = true;
                });
                imgThread.Start();
            }
        }

        private void checkosu()
        {
            Process[] processList = Process.GetProcessesByName("Osu!");
            MessageBox.Show("osu!를 재시작해야 변경된 서버가 적용됩니다.\rYou have to restart osu! to take effect.", "Novah");

            if (state != true)
            {
                SwitchON();
            }
            else
            {
                SwitchOFF();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CertificateCore.UninstallCertificates();
                List<string> hosts = HostsCore.gethosts().ToList();
                hosts.RemoveAll(x => x.Contains(".ppy.sh"));
                HostsCore.changehosts(hosts.ToArray());
                string[] osu_domains = new string[]
                {
                        "osu.ppy.sh",
                        "c.ppy.sh",
                        "c1.ppy.sh",
                        "c2.ppy.sh",
                        "c3.ppy.sh",
                        "c4.ppy.sh",
                        "c5.ppy.sh",
                        "c6.ppy.sh",
                        "ce.ppy.sh",
                        "a.ppy.sh",
                        "i.ppy.sh",
                };
                hosts = HostsCore.gethosts().ToList();
                foreach (string domain in osu_domains)
                {
                    hosts.Add("34.85.96.178" + " " + domain);
                }
                HostsCore.changehosts(hosts.ToArray());

                CertificateCore.InstallCertificate();

                Bu.Toggled1 = true;
                test.Text = "You're Connected to Test Server.";
            }
            catch (Exception ex)
            {
                LogCore.Log(ex);

                MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "Novah", MessageBoxButton.OK, MessageBoxImage.Error);
                string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                Process.Start(filepath);
                Environment.Exit(0);
            }
        }

        private void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            checkosu();
        }
        #endregion

        #region Tresh
        private void Bu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Bu.Toggled1 == true)
            {
                try
                {
                    CertificateCore.UninstallCertificates();
                    List<string> hosts = HostsCore.gethosts().ToList();
                    hosts.RemoveAll(x => x.Contains(".ppy.sh"));
                    HostsCore.changehosts(hosts.ToArray());
                    string[] osu_domains = new string[]
                    {
                        "osu.ppy.sh",
                        "c.ppy.sh",
                        "c1.ppy.sh",
                        "c2.ppy.sh",
                        "c3.ppy.sh",
                        "c4.ppy.sh",
                        "c5.ppy.sh",
                        "c6.ppy.sh",
                        "ce.ppy.sh",
                        "a.ppy.sh",
                        "i.ppy.sh",
                    };
                    hosts = HostsCore.gethosts().ToList();
                    foreach (string domain in osu_domains)
                    {
                        hosts.Add(ServerIP + " " + domain);
                    }
                    HostsCore.changehosts(hosts.ToArray());

                    CertificateCore.InstallCertificate();

                    CheckServer();

                }
                catch (Exception ex)
                {
                    LogCore.Log(ex);

                    MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "NOVAH", MessageBoxButton.OK, MessageBoxImage.Error);
                    string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                    Process.Start(filepath);
                    Environment.Exit(0);
                }
            }
            else
            {
                try
                {
                    string[] _hosts = HostsCore.gethosts();
                    _hosts = _hosts.Where(x => !x.Contains(".ppy.sh")).ToArray();
                    HostsCore.changehosts(_hosts);

                    CertificateCore.UninstallCertificates();

                    CheckServer();
                }
                catch (Exception ex)
                {
                    LogCore.Log(ex);

                    MessageBox.Show("Error! \r\rPlease Send Discrod Nerina#4444 the Switcher Logs", "NOVAH", MessageBoxButton.OK, MessageBoxImage.Error);
                    string filepath = Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData) + "\\novahlog.txt";
                    Process.Start(filepath);
                    Environment.Exit(0);
                }
            }
        }
        #endregion
    }
}
