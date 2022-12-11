using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinHttp;

namespace Novah.core
{
    class UpdateCore
    {
        public static bool verchk = false;

        public static void version(string version)
        {
            string result = PhpCore.php("https://debian.moe/static/switcher/new/ver.txt");
            if (result != version)
            {
                //verchk = false;
                verchk = true;
            }
            if (result == version)
            {
                verchk = true;
            }
        }

    }
}
