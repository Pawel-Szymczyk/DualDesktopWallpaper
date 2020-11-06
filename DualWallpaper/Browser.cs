using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperManager
{
    /// <summary>
    /// Browser class is responsible for managing the Internet browsing.
    /// </summary>
    public static class Browser
    {
        /// <summary>
        /// Search wallpaper over the Internet, with the use of default search browser.
        /// </summary>
        /// <param name="panel">Parent panel to which is added label with resolution.</param>
        public static void SearchForWallpapers(Panel panel)
        {
            string resolution = string.Empty;

            foreach(Control control in panel.Controls)
            {
                if(control is Label)
                {
                    resolution = control.Text;
                }
            }

            System.Diagnostics.Process.Start(@"https://www.google.com/search?q=wallpaper+" + resolution);
        }
    }
}
