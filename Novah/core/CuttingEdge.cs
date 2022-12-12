using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;

namespace Novah.core
{
    class CuttingEdge
    {
        public static string strOsuRoot = "";

        public static void Osuroot()
        {
            try
            {
                using (var key = Registry.LocalMachine.OpenSubKey("Software\\Classes\\osu\\DefaultIcon"))
                {
                    var RootValues = key?.GetValue("");
                    string RootValue = (string)RootValues;
                    RootValue = RootValue.Replace('"',' ');
                    RootValue = RootValue.Replace("\\osu!.exe ,1", "");
                    if (RootValue != null)
                    {
                        strOsuRoot = (string)RootValue;
                        CheckOsuVersion(strOsuRoot);
                        FormCuttingEdge.osuroot = strOsuRoot;
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

        private static void CheckOsuVersion(string osuRoot)
        {
            try
            {
                string path = osuRoot + "\\osu!.cfg";
                string[] cfgValues = System.IO.File.ReadAllLines(path);
                if (cfgValues.Length > 0)
                {
                    for (int i = 0; i < cfgValues.Length; i++)
                    {
                        bool releasestreamLine = cfgValues[i].Contains("_ReleaseStream");
                        if (releasestreamLine)
                        {
                            string releaseStream = cfgValues[i];
                            releaseStream = releaseStream.Replace("_ReleaseStream = ", "");
                            if (releaseStream == "CuttingEdge")
                            {
                                MainWindow.isCuttingEdge = true;
                            }
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
    }
}
