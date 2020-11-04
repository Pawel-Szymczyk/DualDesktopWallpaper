using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperManager
{
    public static class DisplayEventHandler
    {

        /// <summary>
        /// Draws label which represents the display number.
        /// </summary>
        internal static void draw_Label(object sender, PaintEventArgs e, string text)
        {
            PictureBox display = (PictureBox)sender;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            //string text = display.Name.Replace(@"display", "");
            Font font = new Font("Segoe UI", 40);

            SizeF textSize = e.Graphics.MeasureString(text, font);
            PointF locationToDraw = new PointF();
            locationToDraw.X = (display.Width / 2) - (textSize.Width / 2);
            locationToDraw.Y = (display.Height / 2) - (textSize.Height / 2);

            e.Graphics.DrawString(text, font, Brushes.White, locationToDraw);
        }



        /// <summary>
        /// Pick image for monitor.
        /// </summary>
        public static void display_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var img = Image.FromFile(dialog.FileName);
                pictureBox.Image = img;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
                pictureBox.Tag = dialog.FileName;
            }

        }

        public static void display_SingleClick(object sender, MouseEventArgs e, Screen[] screens, Panel panel, Button button)
        {
            // change button visisbility to true
            // add button event
            // add screen refrences to button



            PictureBox pictureBox = (PictureBox)sender;

            // remove any labels first
            var dynamicLabels = panel.Controls.OfType<Label>().Where(c => c.Tag != null && c.Tag.ToString() == "resolution").ToList();
            foreach (var lbl in dynamicLabels)
            {
                panel.Controls.Remove(lbl);
            }


            // add label to each click picturebox with its resoultion, remove old label
            Label label = new Label();
            label.Font = new Font("Segoe UI", 14);
            label.Size = new Size(120, 20);
            label.Location = new Point(pictureBox.Bounds.Location.X, pictureBox.Bounds.Location.Y - 50);
            label.Name = $"{pictureBox.Name}";
            label.Tag = "resolution";
            
            // decorate label with resolution text
            if (pictureBox.Name == "display1")
            {
                label.Text = $"{screens[0].Bounds.Width} x {screens[0].Bounds.Height}";
            }
            else if (pictureBox.Name == "display2")
            {
                label.Text = $"{screens[1].Bounds.Width} x {screens[1].Bounds.Height}";
            }

            panel.Controls.Add(label);

            // set search button to be visible
            button.Visible = true;
        }



        ///// <summary>
        ///// Pick image for 1st monitor.
        ///// </summary>
        //public void wallpaper1_DoubleClick(object sender, EventArgs e)
        //{
        //    PictureBox pictureBox = (PictureBox)sender;
        //    var dialog = new OpenFileDialog();

        //    if (dialog.ShowDialog() == DialogResult.OK)
        //    {
        //        var img = Image.FromFile(dialog.FileName);
        //        pictureBox.Image = img;
        //        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        //        //this.setWallpaperBtnEnabled();
        //    }
        //}

        ///// <summary>
        ///// Pick image for 2nd monitor.
        ///// </summary>
        //public void wallpaper2_DoubleClick(object sender, EventArgs e)
        //{
        //    PictureBox pictureBox = (PictureBox)sender;

        //    var dialog = new OpenFileDialog();

        //    if (dialog.ShowDialog() == DialogResult.OK)
        //    {

        //        var img = Image.FromFile(dialog.FileName);
        //        pictureBox.Image = img;
        //        pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

        //        //this.setWallpaperBtnEnabled();
        //    }
        //}

    }
}
