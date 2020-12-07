using System;
using System.Windows;
using WinHttp;

namespace Novah.core
{
    class PhpCore
    {
        public static string php(string url)
        {
            WinHttpRequest win = new WinHttpRequest();
            try
            {
                win.Open("GET", url);
                win.Send();
                win.WaitForResponse();
            }
            catch
            {
                MessageBox.Show("Connection Error", "Novah", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
            return win.ResponseText;
        }
    }
}
