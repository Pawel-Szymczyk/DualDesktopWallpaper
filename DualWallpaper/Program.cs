using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperManager
{

    // Task:
    // make app to detect montitors, and set wallpaper to each of desktop separetaly, or together, depends on its screen size/resolution and orientation.

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DualWallpaperApp());
        }
    }
}
