using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualWallpaper.Interfaces
{
    public interface IVirtualDisplay
    {
        /// <summary>
        /// Adds label to virtual display.
        /// </summary>
        /// <param name="pictureBox">Virtual Display.</param>
        void AddLabel(PictureBox pictureBox);

        /// <summary>
        /// Adds label to virtual display.
        /// </summary>
        /// <param name="pictureBox">Virtual Display.</param>
        /// <param name="text">Message.</param>
        void AddLabel(PictureBox pictureBox, string text);

        /// <summary>
        /// Adds single click functionality to virtual display.
        /// </summary>
        /// <param name="pictureBox">Virtual Display.</param>
        /// <param name="panel">Parent panel (the wall).</param>
        /// <param name="searchBtn">Search button.</param>
        void AddSingleClick(PictureBox pictureBox, Panel panel, Button searchBtn);

        /// <summary>
        /// Adds double click functionality to virtual display.
        /// </summary>
        /// <param name="pictureBox">Virtual Display.</param>
        /// <param name="panel">Parent panel (the wall).</param>
        /// <param name="applyBtn">Apply (OK) button.</param>
        /// <param name="cancelBtn">Cancel button.</param>
        void AddDoubleClick(PictureBox pictureBox, Panel panel, Button applyBtn, Button cancelBtn);

        /// <summary>
        /// Create (PictureBox) virtual display.
        /// </summary>
        /// <returns>Virtual display.</returns>
        PictureBox Draw();

        /// <summary>
        /// Adds system display name to virtual display.
        /// Note: Must be used before "Draw" method.
        /// </summary>
        void SetDisplayName();

        /// <summary>
        /// Adds system display name to virtual display.
        /// Note: Must be used before "Draw" method.
        /// </summary>
        /// <param name="displayName">Virtual display name.</param>
        void SetDisplayName(string displayName);

        /// <summary>
        /// Provides (sets) system resolution (width x height) for virtual display.
        /// Note: Must be used before "Draw" method.
        /// </summary>
        void SetResolution();

        /// <summary>
        /// Provides (sets) system resolution (width x height) for virtual display.
        /// Note: Must be used before "Draw" method.
        /// </summary>
        /// <param name="height">Virtual display height.</param>
        /// <param name="width">Virtual display width.</param>
        void SetResolution(int height, int width);
    }
}
