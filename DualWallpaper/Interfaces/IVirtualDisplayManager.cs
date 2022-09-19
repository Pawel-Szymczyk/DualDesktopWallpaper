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
        PictureBox Show(int parentContainerMiddleWidth, int parentContainerMiddleHeight);
        List<PictureBox> ShowAll();
    }
}
