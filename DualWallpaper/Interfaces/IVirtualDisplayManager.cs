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
        int TotalHeight { get; }
        int TotalWidth { get; }
        VirtualDisplayLayout SecondaryVirtualDisplayLayout { get; }
        PictureBox Show(int centerPointX, int centerPointY);
        List<PictureBox> ShowAll(int centerPointX, int centerPointY);
    }
}
