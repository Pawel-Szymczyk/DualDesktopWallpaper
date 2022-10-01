using DualWallpaper.Enums;
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
        private IVirtualDisplayEventHandler virtualDisplayEventHandler;

        /// <summary>
        /// System information about desktop screen.
        /// </summary>
        private Screen Screen { get; set; }

        /// <summary>
        /// Scale the virtual display size is going to be adjusted.
        /// </summary>
        private int Scale { get; set; }

        /// <summary>
        /// Virtual display background color.
        /// </summary>
        private Color BackgroundColor { get; set; }

        /// <summary>
        /// Virtual display border style.
        /// </summary>
        private BorderStyle BorderStyle { get; set; }

        /// <summary>
        /// Virtual display resolution (width x height).
        /// </summary>
        private string Resolution { get; set; }

        /// <summary>
        /// Virtual display name.
        /// </summary>
        private string DisplayName { get; set; }

        /// <summary>
        /// Virtual display position.
        /// </summary>
        private VirtualDisplayLayout VirtualDisplayLayout { get; set; }

        public VirtualDisplay() 
        {
            this.Scale = 1;
            this.BackgroundColor = Color.LightGray;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Resolution = string.Empty;
            this.DisplayName = string.Empty;
            this.VirtualDisplayLayout = VirtualDisplayLayout.None;
        }

        public VirtualDisplay(Screen screen, int scale)
        {
            this.Screen = screen;
            this.Scale = scale;

            this.BackgroundColor = Color.LightGray;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.Resolution = string.Empty;
            this.DisplayName = string.Empty;

            this.VirtualDisplayLayout = VirtualDisplayLayout.None;
        }

        public VirtualDisplay(Screen screen, int scale, VirtualDisplayLayout secondaryVirtualDisplayLayout)
        {
            this.Screen = screen;
            this.Scale = scale;

            this.BackgroundColor = Color.LightGray;
            this.BorderStyle = BorderStyle.FixedSingle;

            this.Resolution = string.Empty;
            this.DisplayName = string.Empty;

            this.VirtualDisplayLayout = secondaryVirtualDisplayLayout;
        }

        /// <summary>
        /// Create (PictureBox) virtual display.
        /// </summary>
        /// <returns>Virtual display.</returns>
        public PictureBox Draw()
        {
            // initialize virtual display handler...
            this.virtualDisplayEventHandler = new VirtualDisplayEventHandler(this.VirtualDisplayLayout);

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
        /// <param name="pictureBox">Virtual Display.</param>
        public void AddLabel(PictureBox pictureBox)
        {
            string text = this.Screen.DeviceName.Replace(@"\.", "").Replace(@"\", "").ToLower().Replace(@"display", "");

            this.AddLabel(pictureBox, text);
        }

        /// <summary>
        /// Adds label to virtual display.
        /// </summary>
        /// <param name="pictureBox">Virtual Display.</param>
        /// <param name="text">Message.</param>
        public void AddLabel(PictureBox pictureBox, string text)
        {
            if(this.virtualDisplayEventHandler.Equals(null))
                return;

            pictureBox.Paint += new PaintEventHandler((sender, e) => this.virtualDisplayEventHandler.DrawLabel(sender, e, text));
        }

        /// <summary>
        /// Adds single click functionality to virtual display.
        /// </summary>
        /// <param name="pictureBox">Virtual Display.</param>
        /// <param name="panel">Parent panel (the wall).</param>
        /// <param name="searchBtn">Search button.</param>
        public void AddSingleClick(PictureBox pictureBox, Panel panel, Button searchBtn)
        {
            if (this.virtualDisplayEventHandler.Equals(null))
                return;

            pictureBox.MouseClick += new MouseEventHandler((sender, e) => this.virtualDisplayEventHandler.DisplaySingleClick(sender, e, panel, searchBtn));
        }

        /// <summary>
        /// Adds double click functionality to virtual display.
        /// </summary>
        /// <param name="pictureBox">Virtual Display.</param>
        /// <param name="panel">Parent panel (the wall).</param>
        /// <param name="applyBtn">Apply (OK) button.</param>
        /// <param name="cancelBtn">Cancel button.</param>
        public void AddDoubleClick(PictureBox pictureBox, Panel panel, Button applyBtn, Button cancelBtn)
        {
            if (this.virtualDisplayEventHandler.Equals(null))
                return;

            pictureBox.MouseDoubleClick += new MouseEventHandler((sender, e) => this.virtualDisplayEventHandler.DisplayDoubleClick(sender, e, panel, applyBtn, cancelBtn));
        }

        /// <summary>
        /// Adds system display name to virtual display.
        /// Note: Must be used before "Draw" method.
        /// </summary>
        public void SetDisplayName()
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

        /// <summary>
        /// Returns virtual display size adjusted by scale.
        /// </summary>
        private Size GetSize()
        {
            int width = this.Screen.Bounds.Size.Width / this.Scale;
            int height = this.Screen.Bounds.Size.Height / this.Scale;
            return new Size(width, height);
        }

        /// <summary>
        /// Returns virtual display location (X and Y) adjusted by scale.
        /// </summary>
        private Point GetLocation()
        {
            int X = this.Screen.Bounds.X / this.Scale;
            int Y = this.Screen.Bounds.Y / this.Scale;

            return new Point(X, Y);
        }
    }
}
