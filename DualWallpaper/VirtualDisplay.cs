using DualWallpaper.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WallpaperManager;

namespace DualWallpaper
{
    public class VirtualDisplay : IVirtualDisplay
    {
        private string DisplayName { get; set; }
        private Size Size { get; set; }
        private Color BackgroundColor { get; set; }
        private BorderStyle BorderStyle { get; set; }
        private string Resolution { get; set; }

        public VirtualDisplay(int width, int height, string deviceName, string resolution)
        {
            this.DisplayName = deviceName;
            this.Size = new Size(width, height);
            this.BackgroundColor = Color.LightGray;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Resolution = resolution;
        }

        /// <summary>
        /// Create (PictureBox) virtual display.
        /// </summary>
        /// <returns>PictureBox.</returns>
        public PictureBox Draw()
        {
            var pictureBox = new PictureBox
            {
                Name = this.DisplayName,
                Size = this.Size,
                BackColor = this.BackgroundColor,
                BorderStyle = this.BorderStyle,
                Text = this.Resolution
            };

            return pictureBox;
        }

        /// <summary>
        /// Adds label to virtual display.
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="text"></param>
        public void AddLabel(PictureBox pictureBox, string text)
        {
            pictureBox.Paint += new PaintEventHandler((sender, e) => DisplayEventHandler.DrawLabel(sender, e, text));
        }

        /// <summary>
        /// Adds single click functionality to virtual display.
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="panel"></param>
        /// <param name="searchBtn"></param>
        public void AddSingleClick(PictureBox pictureBox, Panel panel, Button searchBtn)
        {
            pictureBox.MouseClick += new MouseEventHandler((sender, e) => DisplayEventHandler.DisplaySingleClick(sender, e, Screen.AllScreens, panel, searchBtn));
        }

        /// <summary>
        /// Adds double click functionality to virtual display.
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="panel"></param>
        /// <param name="applyBtn"></param>
        /// <param name="cancelBtn"></param>
        public void AddDoubleClick(PictureBox pictureBox, Panel panel, Button applyBtn, Button cancelBtn)
        {
            pictureBox.MouseDoubleClick += new MouseEventHandler((sender, e) => DisplayEventHandler.DisplayDoubleClick(sender, e, panel, applyBtn, cancelBtn));
        }
    }
}
