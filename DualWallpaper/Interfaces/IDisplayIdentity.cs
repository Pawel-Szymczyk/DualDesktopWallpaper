using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualWallpaper.Interfaces
{
    public interface IDisplayIdentity
    {
        /// <summary>
        /// Displays identity number on each of physical screens.
        /// </summary>
        /// <param name="button">Identity button.</param>
        Task DetectIdentity(Button button);
    }
}