using System;
using System.Windows.Forms;

namespace WallpaperManager.Interfaces
{
    public interface IVirtualDisplayEventHandler
    {
        void DisplayDoubleClick(object sender, EventArgs e, Panel panel, Button applyBtn, Button cancelBtn);
        void DisplaySingleClick(object sender, MouseEventArgs e, Screen[] screens, Panel panel, Button button);
        void DrawLabel(object sender, PaintEventArgs e, string text);
    }
}