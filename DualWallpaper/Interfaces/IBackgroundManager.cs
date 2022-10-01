using System.Windows.Forms;

namespace DualWallpaper.Interfaces
{
    public interface IBackgroundManager
    {
        void CleanWallpapers(Panel panel);
        void SaveBackground(Panel panel, CheckBox checkBox);
    }
}