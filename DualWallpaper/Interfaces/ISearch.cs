using System.Windows.Forms;

namespace DualWallpaper.Interfaces
{
    public interface ISearch
    {
        /// <summary>
        /// Search wallpaper over the Internet, with the use of default search browser.
        /// </summary>
        /// <param name="panel">Parent panel to which is added label with resolution.</param>
        void OpenDefaultBrowserWithWallpaperSearchResults(Panel panel);
    }
}