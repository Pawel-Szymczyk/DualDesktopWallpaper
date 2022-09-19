using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WallpaperManager
{
    public static class DisplayEventHandler
    {

        /// <summary>
        /// Draws label which represents the display number.
        /// </summary>
        internal static void DrawLabel(object sender, PaintEventArgs e, string text)
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
        /// Pick image for monitor.
        /// </summary>
        public static void DisplayDoubleClick(object sender, EventArgs e, Panel panel, Button applyBtn, Button cancelBtn)
        {
            var pictureBox = (PictureBox)sender;
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var img = Image.FromFile(dialog.FileName);
                pictureBox.Image = img;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Tag = dialog.FileName;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Picture Box</param>
        /// <param name="e"></param>
        /// <param name="screens">Connected monitors (peripherals)</param>
        /// <param name="panel">freeSpaceArea</param>
        /// <param name="button">search button</param>
        public static void DisplaySingleClick(object sender, MouseEventArgs e, Screen[] screens, Panel panel, Button button)
        {
            var pictureBox = (PictureBox)sender;

            // remove any labels first
            var dynamicLabels = panel.Controls.OfType<Label>().Where(c => c.Tag != null && c.Tag.ToString() == "resolution").ToList();
            foreach (Label lbl in dynamicLabels)
            {
                panel.Controls.Remove(lbl);
            }

            // add label to each click picturebox with its resoultion, remove old label
            var label = new Label
            {
                Font = new Font("Segoe UI", 14),
                Size = new Size(120, 20),
                Location = new Point(pictureBox.Bounds.Location.X, pictureBox.Bounds.Location.Y - 50),
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
