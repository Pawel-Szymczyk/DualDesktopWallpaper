﻿using DualWallpaper;
using DualWallpaper.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WallpaperManager
{
    public partial class DualWallpaperApp : Form
    {
        // https://devblogs.microsoft.com/oldnewthing/?p=25003 


        private IVirtualDisplayManager virtualDisplayManager;
        private IBackgroundManager backgroundManager;

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
            this.Version();
            this.BuildNumber();

            
            this.virtualDisplayManager = new VirtualDisplayManager(this.wall,
                  this.searchBtn,
                  this.applyBtn,
                  this.cancelBtn);

            this.virtualDisplayManager.Initialize();


            this.backgroundManager = new BackgroundManager(
                this.virtualDisplayManager.SecondaryVirtualDisplayLayout,
                this.virtualDisplayManager.TotalHeight, 
                this.virtualDisplayManager.TotalWidth);


            this.DisplaysResolutions();

            this.DrawDisplays();
        }



        #endregion
        // -------------------------------------------------------------------

        // -------------------------------------------------------------------
        // Form Event Handlers
        //
        #region Form Event Handlers

        /// <summary>
        /// Hide controls and blank out wall on mouse single click.
        /// </summary>
        private void DualWallpaperApp_MouseClick(object sender, MouseEventArgs e)
        {
            this.HideControls();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            Search.GetBackgrounds(this.wall);
        }

        private async void identifyBtn_Click(object sender, EventArgs e)
        {
            await DisplayIdentity.DetectIdentity(this.detectScreenBtn);
        }


        // Merge images and store them in the windows registry.
        private void applyBtn_Click(object sender, EventArgs e)
        {
            

            this.backgroundManager.SaveBackground(this.wall, this.mergeBtn);

            this.HideControls();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            // clear images from pictureboxes
            this.backgroundManager.CleanWallpapers(this.wall);

            this.HideControls();
        }

        private void mergeBtn_CheckedChanged(object sender, EventArgs e)
        {
            // remove displays and drow 1 common for both displays 
            // allow to click on it

            // in the case checkbox is unchecked it should display two displays

            if (this.mergeBtn.Checked)
            {
                this.DrawMergedDisplays();
            }
            else
            {
                this.DrawDisplays();
            }
        }

        private void DrawMergedDisplays()
        {
            this.wall.Controls.Clear();
            this.applyBtn.Visible = false;
            this.cancelBtn.Visible = false;
            this.searchBtn.Visible = false;

            PictureBox  display = this.virtualDisplayManager.Show(this.wall.Bounds.Width / 2, this.wall.Bounds.Height / 2);

            this.wall.Controls.Add(display);
        }


        #endregion
        // -------------------------------------------------------------------


        /// <summary>
        /// Dynamically draw displays.
        /// </summary>
        /// <param name="ShowSingleDisplay">True, if we want to show single monitor, otherwise false.</param>
        private void DrawDisplays()
        {
            this.wall.Controls.Clear();
            this.applyBtn.Visible = false;
            this.cancelBtn.Visible = false;
            this.searchBtn.Visible = false;

            List<PictureBox> displays = this.virtualDisplayManager.ShowAll(this.wall.Bounds.Width / 2, this.wall.Bounds.Height / 2);


            foreach (PictureBox display in displays)
            {
                this.wall.Controls.Add(display);
            }
        }

        /// <summary>
        /// Display assembly version.
        /// </summary>
        private void Version()
        {
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();
            var fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

            string text = $"Ver. {fvi.FileVersion}";
            for (int i = 1; i <= text.Length; i += 1)
            {
                text = text.Insert(i, " ");
                i++;
            }

            this.versionLbl.Text = text;
        }

        /// <summary>
        /// Display build number.
        /// </summary>
        private void BuildNumber()
        {
            string text = $"B u i l d: {Const.BuildNumber}";
            this.buildLbl.Text = text;
        }

        /// <summary>
        /// Hide form controles.
        /// </summary>
        private void HideControls()
        {
            // Hides search button and resolution labels.
            this.searchBtn.Visible = false;
            this.applyBtn.Visible = false;
            this.cancelBtn.Visible = false;

            var resolutionLabels = this.wall.Controls.OfType<Label>()
                .Where(c => c.Tag != null && c.Tag.ToString() == "resolution")
                .ToList();

            foreach (Label lbl in resolutionLabels)
            {
                this.wall.Controls.Remove(lbl);
            }
        }

        /// <summary>
        /// List of displays and their resolutions.
        /// </summary>
        private void DisplaysResolutions()
        {
            foreach (Screen screen in Screen.AllScreens)
            {
                this.resolutionsList.Items.Add($"{screen.DeviceName.Replace(@"\.", "").Replace(@"\", "")}: {screen.Bounds.Width} x {screen.Bounds.Height}");
            }
        }
    }
}
