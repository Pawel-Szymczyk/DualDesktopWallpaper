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
        private Color BackgroundColor { get; set; }
        private BorderStyle BorderStyle { get; set; }
        private int Scale { get; set; }
        private Screen Screen { get; set; }

        public VirtualDisplay(Screen screen, int scale)
        {
            this.Screen = screen;
            this.Scale = scale;

            this.BackgroundColor = Color.LightGray;
            this.BorderStyle = BorderStyle.FixedSingle;
        }

        /// <summary>
        /// Create (PictureBox) virtual display.
        /// </summary>
        /// <returns>PictureBox.</returns>
        public PictureBox Draw()
        {
            var name = this.GetDisplayName();
            var size = this.GetSize();
            var text = this.GetResolution();
            var location = this.GetLocation();

            var pictureBox = new PictureBox
            {
                BackColor = this.BackgroundColor,
                BorderStyle = this.BorderStyle,
                Name = name,
                Size = size,
                Text = text,
                Location = location
            };

            return pictureBox;
        }

        /// <summary>
        /// Adds label to virtual display.
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="text"></param>
        public void AddLabel(PictureBox pictureBox)
        {
            string text = this.Screen.DeviceName.Replace(@"\.", "").Replace(@"\", "").ToLower().Replace(@"display", "");

            pictureBox.Paint += new PaintEventHandler((sender, e) => VirtualDisplayEventHandler.DrawLabel(sender, e, text));
        }

        /// <summary>
        /// Adds single click functionality to virtual display.
        /// </summary>
        /// <param name="pictureBox"></param>
        /// <param name="panel"></param>
        /// <param name="searchBtn"></param>
        public void AddSingleClick(PictureBox pictureBox, Panel panel, Button searchBtn)
        {
            pictureBox.MouseClick += new MouseEventHandler((sender, e) => VirtualDisplayEventHandler.DisplaySingleClick(sender, e, Screen.AllScreens, panel, searchBtn));
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
            pictureBox.MouseDoubleClick += new MouseEventHandler((sender, e) => VirtualDisplayEventHandler.DisplayDoubleClick(sender, e, panel, applyBtn, cancelBtn));
        }

        private string GetDisplayName()
        {
            return this.Screen.DeviceName.Replace(@"\.", "").Replace(@"\", "").ToLower();
        }

        private string GetResolution()
        {
            return $"{this.Screen.Bounds.Width} x {this.Screen.Bounds.Height}";
        }

        private Size GetSize()
        {
            int width = this.Screen.Bounds.Size.Width / this.Scale;
            int height = this.Screen.Bounds.Size.Height / this.Scale;
            return new Size(width, height);
        }

        private Point GetLocation()
        {
            int X = this.Screen.Bounds.X / this.Scale;
            int Y = this.Screen.Bounds.Y / this.Scale;

            return new Point(X, Y);
        }

       

    }
}
