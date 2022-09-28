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
        private Screen Screen { get; set; }
        private int Scale { get; set; }
        private Color BackgroundColor { get; set; }
        private BorderStyle BorderStyle { get; set; }

        /// <summary>
        /// Virtual display resolution (width x height).
        /// </summary>
        private string Resolution { get; set; }

        /// <summary>
        /// Virtual display name.
        /// </summary>
        private string DisplayName { get; set; }

        public VirtualDisplay() 
        {
            this.Scale = 1;
            this.BackgroundColor = Color.LightGray;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Resolution = string.Empty;
            this.DisplayName = string.Empty;
        }

        public VirtualDisplay(Screen screen, int scale)
        {
            this.Screen = screen;
            this.Scale = scale;

            this.BackgroundColor = Color.LightGray;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.Resolution = string.Empty;
            this.DisplayName = string.Empty;
        }

        /// <summary>
        /// Create (PictureBox) virtual display.
        /// </summary>
        /// <returns>PictureBox.</returns>
        public PictureBox Draw()
        {
            var size = this.GetSize();
            var location = this.GetLocation();

            var pictureBox = new PictureBox
            {
                BackColor = this.BackgroundColor,
                BorderStyle = this.BorderStyle,
                Text = this.Resolution,
                Name = this.DisplayName,
                Size = size,
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

            this.AddLabel(pictureBox, text);
        }

        public void AddLabel(PictureBox pictureBox, string text)
        {
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


        /// <summary>
        /// Adds system display name to virtual display.
        /// Note: Must be used before "Draw" method.
        /// </summary>
        public void  SetDisplayName()
        {
            this.SetDisplayName(this.Screen.DeviceName.Replace(@"\.", "").Replace(@"\", "").ToLower());
        }

        /// <summary>
        /// Adds system display name to virtual display.
        /// Note: Must be used before "Draw" method.
        /// </summary>
        /// <param name="displayName">Virtual display name.</param>
        public void SetDisplayName(string displayName)
        {
            this.DisplayName = displayName;
        }

        /// <summary>
        /// Provides (sets) system resolution (width x height) for virtual display.
        /// Note: Must be used before "Draw" method.
        /// </summary>
        public void SetResolution()
        {
            this.SetResolution(this.Screen.Bounds.Height, this.Screen.Bounds.Width);
        }

        /// <summary>
        /// Provides (sets) system resolution (width x height) for virtual display.
        /// Note: Must be used before "Draw" method.
        /// </summary>
        /// <param name="height">Virtual display height.</param>
        /// <param name="width">Virtual display width.</param>
        public void SetResolution(int height, int width)
        {
            this.Resolution = $"{width} x {height}";
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
