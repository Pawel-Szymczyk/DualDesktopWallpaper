using System;
using System.Windows.Forms;

namespace DualWallpaper.Interfaces
{
    public interface IVirtualDisplayEventHandler
    {
        /// <summary>
        /// Opens Windows Search window, allowing to pick background image for specified virtual display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="panel">Parent panel (the wall).</param>
        /// <param name="applyBtn">Apply (OK) buttun.</param>
        /// <param name="cancelBtn">Cancel button.</param>
        void DisplayDoubleClick(object sender, EventArgs e, Panel panel, Button applyBtn, Button cancelBtn);

        /// <summary>
        /// Hanle single click by displaying label next to virtual display, as well as show specified button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="panel">Parent panel (the wall).</param>
        /// <param name="button">Specified button to show up.</param>
        void DisplaySingleClick(object sender, MouseEventArgs e, Panel panel, Button button);

        /// <summary>
        /// Draws label which represents the display number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="text">Message displyed in the virtual display. </param>
        void DrawLabel(object sender, PaintEventArgs e, string text);
    }
}