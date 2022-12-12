using Novah.core;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ToggleSwitch
{
    /// <summary>
    /// Interaction logic for ToggleButton.xaml
    /// </summary>
    public partial class ToggleButton : UserControl
    {
        Thickness LeftSide = new Thickness(-65, 0, 0, 0);
        Thickness RightSide = new Thickness(0, 0, -65, 0);
        SolidColorBrush Off = new SolidColorBrush(Color.FromRgb(160, 160, 160));
        SolidColorBrush On = new SolidColorBrush(Color.FromRgb(130, 190, 125));
        public bool Toggled = false;

        public ToggleButton()
        {
            InitializeComponent();
            Back.Fill = Off;
            Toggled = false;
            Dot.Margin = LeftSide;
            GetServer();
        }

        public bool Toggled1 { get => Toggled; set => Toggled = value; }

        private void GetServer()
        {
            try
            {
                bool ServerChk = HostsCore.gethosts().Any(x => x.Contains("osu.ppy.sh") && !x.Contains("#"));
                string serverChk2 = Convert.ToString(ServerChk);
                if (ServerChk == true)
                {
                    Back.Fill = On;
                    Dot.Margin = RightSide;

                }
                if (ServerChk != true)
                {
                    Back.Fill = Off;
                    Dot.Margin = LeftSide;
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

        private void Dot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                Dot.Margin = RightSide;

            }
            else
            {

                Back.Fill = Off;
                Toggled = false;
                Dot.Margin = LeftSide;

            }




        }

        private void Back_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!Toggled)
            {
                Back.Fill = On;
                Toggled = true;
                Dot.Margin = RightSide;

            }
            else
            {

                Back.Fill = Off;
                Toggled = false;
                Dot.Margin = LeftSide;

            }

        }
    }
}
