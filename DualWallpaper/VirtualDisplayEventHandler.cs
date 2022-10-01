using DualWallpaper.Enums;
using DualWallpaper.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DualWallpaper
{
    public class VirtualDisplayEventHandler : IVirtualDisplayEventHandler
    {
        /// <summary>
        /// Secondary virtual display position.
        /// </summary>
        private VirtualDisplayLayout VirtualDisplayLayout { get; set; }

        public VirtualDisplayEventHandler()
        {
            this.VirtualDisplayLayout = VirtualDisplayLayout.None;
        }

        public VirtualDisplayEventHandler(VirtualDisplayLayout secondaryVirtualDisplayLayout)
        {
            this.VirtualDisplayLayout = secondaryVirtualDisplayLayout;
        }

        /// <summary>
        /// Draws label which represents the display number.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="text">Message displyed in the virtual display. </param>
        public void DrawLabel(object sender, PaintEventArgs e, string text)
        {
            var display = (PictureBox)sender;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            var font = new Font("Segoe UI", 40);

            SizeF textSize = e.Graphics.MeasureString(text, font);
            var locationToDraw = new PointF
            {
                X = (display.Width / 2) - (textSize.Width / 2),
                Y = (display.Height / 2) - (textSize.Height / 2)
            };

            e.Graphics.DrawString(text, font, Brushes.White, locationToDraw);
        }

        /// <summary>
        /// Opens Windows Search window, allowing to pick background image for specified virtual display.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="panel">Parent panel (the wall).</param>
        /// <param name="applyBtn">Apply (OK) buttun.</param>
        /// <param name="cancelBtn">Cancel button.</param>
        public void DisplayDoubleClick(object sender, EventArgs e, Panel panel, Button applyBtn, Button cancelBtn)
        {
            try
            {
                var pictureBox = (PictureBox)sender;
                var dialog = new OpenFileDialog();

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var img = Image.FromFile(dialog.FileName);
                    pictureBox.Image = img;
                    pictureBox.Image.Tag = dialog.FileName;
                    pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                }

                // check if both images are set 
                var list = new List<PictureBox>();

                foreach (object u in panel.Controls)
                {
                    if (u is PictureBox)
                    {
                        list.Add((PictureBox)u);
                    }
                }

                bool o = list.All(x => x.Image != null);
                if (o)
                {
                    // show buttons
                    applyBtn.Visible = true;
                    cancelBtn.Visible = true;
                }
                else
                {
                    applyBtn.Visible = false;
                    cancelBtn.Visible = false;
                }
            }
            catch(Exception)
            {
                // TODO: bubble up error message 
                // PROBLEM: How to catch event handler exception?

                // TEMPORARY SOLUTION: do nothing, at least it will not break the program.
            }
        }


        /// <summary>
        /// Hanle single click by displaying label next to virtual display, as well as show specified button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="panel">Parent panel (the wall).</param>
        /// <param name="button">Specified button to show up.</param>
        public void DisplaySingleClick(object sender, MouseEventArgs e, Panel panel, Button button)
        {
            var pictureBox = (PictureBox)sender;

            // remove any labels first
            var dynamicLabels = panel.Controls.OfType<Label>().Where(c => c.Tag != null && c.Tag.ToString() == "resolution").ToList();
            foreach (Label lbl in dynamicLabels)
            {
                panel.Controls.Remove(lbl);
            }

            // label location
            Point labelLocation = new Point();
            switch(this.VirtualDisplayLayout)
            {
                case VirtualDisplayLayout.Bottom:
                case VirtualDisplayLayout.Top:
                    labelLocation = new Point(pictureBox.Bounds.Right + 10, pictureBox.Bounds.Y);
                    break;
                case VirtualDisplayLayout.Left:
                case VirtualDisplayLayout.Right:
                case VirtualDisplayLayout.None:
                    labelLocation = new Point(pictureBox.Bounds.X, pictureBox.Bounds.Top - 30);
                    break;
                default:
                    labelLocation = new Point(pictureBox.Bounds.X, pictureBox.Bounds.Top - 30);
                    break;
            }

            // add label to each click picturebox with its resoultion, remove old label
            var label = new Label
            {
                Font = new Font("Segoe UI", 14),
                Size = new Size(120, 20),
                Location = labelLocation,
                Name = $"{pictureBox.Name}",
                Tag = "resolution",
                Text = pictureBox.Text.ToString()
            };

            panel.Controls.Add(label);

            // set search button to be visible
            button.Visible = true;
        }
    }
}