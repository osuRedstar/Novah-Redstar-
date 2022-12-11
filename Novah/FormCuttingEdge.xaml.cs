using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Shapes;
using IWshRuntimeLibrary;
using Novah.core;

namespace Novah
{
    /// <summary>
    /// FormCuttingEdge.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class FormCuttingEdge : Window
    {
        public static string osuroot = "";

        bool state = false;
        bool clickable = true;
        Stopwatch stopwatch = new Stopwatch();
        BitmapImage[] frames = new BitmapImage[30];
        BitmapImage on = new BitmapImage(new Uri("pack://application:,,,/assets/img/Frame29.png"));
        BitmapImage off = new BitmapImage(new Uri("pack://application:,,,/assets/img/Frame0.png"));
        BitmapImage returnb = new BitmapImage(new Uri("pack://application:,,,/assets/close_default.png"));
        BitmapImage exitb = new BitmapImage(new Uri("pack://application:,,,/assets/close_hover.png"));

        Thread imgThread;

        private void imageframe()
        {
            for (int i = 0; i < 30; i++)
            {
                frames[i] = new BitmapImage(new Uri("pack://application:,,,/assets/img/Frame" + i.ToString() + ".png"));
            }

            img.MouseLeftButtonDown += BtnChange;
        }

        public FormCuttingEdge()
        {
            InitializeComponent();
            state = false;
            border_sub.Opacity = 0;
            border.Opacity = 100;
            test.Text = "Click The Button!";
            imageframe();
            UninstallCerts();
            changefilename();
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

        private void UninstallCerts()
        {
            try
            {
                HostsCore.removehosts();
                CertificateCore.UninstallCertificates();
            }
            catch { MessageBox.Show("REMOVE CERTIFICATE FAILED"); }
        }

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
                        }));

                        stopwatch.Stop();
                        Thread.Sleep(stopwatch.ElapsedMilliseconds <= 33 ? 33 - (int)stopwatch.ElapsedMilliseconds : 0);
                        stopwatch.Reset();
                    }
                    Dispatcher.Invoke(new Action(() =>
                    {
                        test.Text = state ? "Welcome To Redstar, GLHF!" : "Click The Button!";

                    }));
                    clickable = true;
                });
                imgThread.Start();
            }
        }

        private void runOsu(bool state)
        {
            ProcessStartInfo psi = new ProcessStartInfo();
            string osudir = osuroot + "\\osu!.exe";
            osudir = osudir.Substring(1);
            psi.FileName = osudir;
            //psi.Arguments = state ? "" : @"-devserver debian.moe";
            psi.Arguments = state ? "" : @"-devserver redstar.moe";
            if (!state)
            {
                Process.Start(psi);
            }
        }

        private void checkosu()
        {
            foreach(Process process in Process.GetProcesses())
            {
                if (process.ProcessName.StartsWith("osu!"))
                {
                    process.Kill();
                }
            }
            Bu.Toggled1 = !Bu.Toggled1;
            runOsu(state);
        }


        #region Events
        private void img_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            checkosu();
        }

        private void border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void border_sub_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.Source = exitb;
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            exit.Source = returnb;
        }

        private void exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        #endregion
    }
}
