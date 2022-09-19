﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualWallpaper.Interfaces
{
    public interface IVirtualDisplay
    {
        PictureBox Draw();
        void AddLabel(PictureBox pictureBox, string text);
        void AddSingleClick(PictureBox pictureBox, Panel panel, Button searchBtn);
        void AddDoubleClick(PictureBox pictureBox, Panel panel, Button applyBtn, Button cancelBtn);
    }
}
