using DualWallpaper.Interfaces;
using System.Linq;
using System.Windows.Forms;

namespace DualWallpaper
{
    /// <summary>
    /// Browser class is responsible for managing the Internet browsing.
    /// </summary>
    public class Search : ISearch
    {
        private readonly string searchUrl = "https://www.google.com/search?q=wallpaper+";

        public Search() { }

        /// <summary>
        /// Search wallpaper over the Internet, with the use of default search browser.
        /// </summary>
        /// <param name="panel">Parent panel to which is added label with resolution.</param>
        public void OpenDefaultBrowserWithWallpaperSearchResults(Panel panel)
        {
            string resolution = panel.Controls.OfType<Label>().FirstOrDefault().Text;

            System.Diagnostics.Process.Start($"{this.searchUrl}{resolution}");
        }
    }
}
