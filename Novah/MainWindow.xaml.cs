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
        //string ver = "2.3";
        string ver = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public static bool isCuttingEdge = false;
        public static string certthumbprint = "350fce0f2a489229928e2ec7dc2b426b49192f16";
        //string GetServerIP = PhpCore.php("https://debian.moe/static/switcher/ip.txt");
        //string GetTServerIP = PhpCore.php("https://debian.moe/static/switcher/testip.txt");
        string GetServerIP = PhpCore.php("https://redstar.moe/static/switcher/ip.txt");
        string GetTServerIP = PhpCore.php("https://redstar.moe/static/switcher/testip.txt");
        #endregion

        #region FormEvents
        public MainWindow()
        {
            InitializeComponent();
            UpdateCore.version(ver);
            if (!UpdateCore.verchk)
            {
                UpdateForm frm = new UpdateForm();
                Hide();
                frm.Show();
            }
            else
            {
                changefilename();
                CuttingEdge.Osuroot();
                FormCuttingEdge frm2 = new FormCuttingEdge();
                Hide();
                frm2.Show();
            }
        }

        public void changefilename()
        {
            string filename = System.IO.Path.GetFileName(Assembly.GetEntryAssembly().Location);
            string kakao = "Novah - RedstarOSU.exe";
            if (filename != kakao)
            {
                string path = Environment.CurrentDirectory;
                FileInfo fileRename = new FileInfo(path + @"\" + kakao);
                try
                {
                    Process[] processList = Process.GetProcessesByName("Novah - RedstarOSU");
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
        #endregion
    }
}
