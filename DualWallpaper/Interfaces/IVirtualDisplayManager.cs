using DualWallpaper.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualWallpaper.Interfaces
{
    public interface IVirtualDisplayManager
    {
        /// <summary>
        /// Dynamic scale, used to nicely position both screens.
        /// </summary>
        int Scale { get; }

        /// <summary>
        /// Total Height of multiple displays.
        /// </summary>
        int TotalHeight { get; }

        /// <summary>
        /// Total Width of multiple displays.
        /// </summary>
        int TotalWidth { get; }

        /// <summary>
        /// Default position of the 2md virtual display.
        /// </summary>
        VirtualDisplayLayout SecondaryVirtualDisplayLayout { get; }

        /// <summary>
        /// Set up basic functionality.
        ///  - total width and height
        ///  - scale
        /// </summary>
        void Initialize();

        /// <summary>
        /// Draws single virtual display as merge (combain) two screens together. Strech one backround over two screens.
        /// </summary>
        /// <param name="centerPointX">Panel (the wall) central point X.</param>
        /// <param name="centerPointY">Panel (the wall) central point Y.</param>
        /// <returns>Virtual display.</returns>
        PictureBox Show(int centerPointX, int centerPointY);

        /// <summary>
        /// Draw number of virtual displays, mimic screen arrangemnt set up in the Windows display settings.
        /// </summary>
        /// <param name="centerPointX">Panel (the wall) central point X.</param>
        /// <param name="centerPointY">Panel (the wall) central point Y.</param>
        /// <returns>List of virtual displays.</returns>
        List<PictureBox> ShowAll(int centerPointX, int centerPointY);
    }
}
