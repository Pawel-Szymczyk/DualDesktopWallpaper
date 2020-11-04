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
            int index = 2; // this is going to be always 3 element, after 2 displays.
            string resolution = !string.IsNullOrEmpty(panel.Controls[index].Text) ? panel.Controls[index].Text : string.Empty;

            System.Diagnostics.Process.Start(@"https://www.google.com/search?q=wallpaper+" + resolution);
        }
    }
}
