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
    /// CuttingEdgeForm.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CuttingEdgeForm : Window
    {
        public CuttingEdgeForm()
        {
            imageframe();
        }

        #region variables
        Stopwatch stopwatch = new Stopwatch();
        BitmapImage[] frames = new BitmapImage[30];
        BitmapImage on = new BitmapImage(new Uri("pack://application:,,,/assets/img/Frame29.png"));
        BitmapImage off = new BitmapImage(new Uri("pack://application:,,,/assets/img/Frame0.png"));
        BitmapImage returnb = new BitmapImage(new Uri("pack://application:,,,/assets/close_default.png"));
        BitmapImage exitb = new BitmapImage(new Uri("pack://application:,,,/assets/close_hover.png"));

        Thread imgThread;
        #endregion

        private void imageframe()
        {
            for (int i = 0; i < 30; i++)
            {
                frames[i] = new BitmapImage(new Uri("pack://application:,,,/assets/img/Frame" + i.ToString() + ".png"));
            }

        }
    }
}
