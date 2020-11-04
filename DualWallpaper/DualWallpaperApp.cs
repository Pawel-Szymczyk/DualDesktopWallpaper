using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace WallpaperManager
{
    public partial class DualWallpaperApp : Form
    {
        // https://devblogs.microsoft.com/oldnewthing/?p=25003 





        // -------------------------------------------------------------------
        // Form
        //
        #region Form

        /// <summary>
        /// Initialize App.
        /// </summary>
        public DualWallpaperApp()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Generate required components during loading form.
        /// </summary>
        private void DualWallpaperApp_Load(object sender, EventArgs e)
        {
            this.DrawDisplays(false);

            // get displays resolutions...
            foreach (Screen screen in Screen.AllScreens)
            {
                this.resolutionsList.Items.Add($"{screen.DeviceName.Replace(@"\.", "").Replace(@"\", "")}: {screen.Bounds.Width} x {screen.Bounds.Height}");
            }
        }

        /// <summary>
        /// Hide controls and blank out freeSpaceArea on mouse single click.
        /// </summary>
        private void DualWallpaperApp_MouseClick(object sender, MouseEventArgs e)
        {

            // Hides search button and resolution labels.
            this.searchBtn.Visible = false;

            var resolutionLabels = this.freeSpaceArea.Controls.OfType<Label>()
                .Where(c => c.Tag != null && c.Tag.ToString() == "resolution")
                .ToList();

            foreach (Label lbl in resolutionLabels)
            {
                this.freeSpaceArea.Controls.Remove(lbl);
            }

        }


        #endregion
        // -------------------------------------------------------------------


        // -------------------------------------------------------------------
        // Form Event Handlers
        //
        #region Form Event Handlers

        private void searchBtn_Click(object sender, EventArgs e)
        {
            Browser.SearchForWallpapers(this.freeSpaceArea);
        }

        private async void detectScreenBtn_Click(object sender, EventArgs e)
        {
            await DisplayIdentity.DetectIdentity(this.detectScreenBtn);
        }


        // Merge images and store them in the windows registry.
        private void setWallpaperBtn_Click(object sender, EventArgs e)
        {
            WallpaperManager.SetWallpaper(this.freeSpaceArea, this.mergeScreenBtn);
        }

        private void mergeScreenBtn_CheckedChanged(object sender, EventArgs e)
        {
            // remove displays and drow 1 common for both displays 
            // allow to click on it

            // in the case checkbox is unchecked it should display two displays

            if (this.mergeScreenBtn.Checked)
            {
                this.freeSpaceArea.Controls.Clear();

                this.DrawDisplays(true);
            }
            else
            {
                this.freeSpaceArea.Controls.Clear();

                this.DrawDisplays(false);
            }


        }


        #endregion
        // -------------------------------------------------------------------

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DrawSingleDisplay"></param>
        private void DrawDisplays(bool DrawSingleDisplay)
        {
            List<PictureBox> displays = new DisplayManager().DrawDisplays(
                    DrawSingleDisplay,
                    this.freeSpaceArea.Bounds.Width / 2,
                    this.freeSpaceArea.Bounds.Height / 2,
                    this.freeSpaceArea,
                    this.searchBtn);

            foreach (PictureBox display in displays)
            {
                this.freeSpaceArea.Controls.Add(display);
            }
        }









    }
}
