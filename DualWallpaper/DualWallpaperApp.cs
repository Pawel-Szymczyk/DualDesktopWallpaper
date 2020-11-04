using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperManager
{
    public partial class DualWallpaperApp : Form
    {
        // https://devblogs.microsoft.com/oldnewthing/?p=25003 

        /// <summary>
        /// SystemParametersInfo returns TRUE unless there is an error.
        /// </summary>
        private const int SPI_SETDESKWALLPAPER = 20;

        /// <summary>
        /// Writes the new system-wide parameter setting to the user profile.
        /// </summary>
        private const int SPIF_UPDATEINIFILE = 0x01;

        /// <summary>
        /// Broadcasts the WM_SETTINGCHANGE message after updating the user profile.
        /// </summary>
        private const int SPIF_SENDWININICHANGE = 0x02;

        /// <summary>
        /// Retrieves or sets the value of one of the system-wide parameters. 
        /// This function can also update the user profile while setting a parameter.
        /// </summary>
        /// <param name="uAction">The system-wide parameter to be retrieved or set.</param>
        /// <param name="uParam">See the uAction parameter.</param>
        /// <param name="lpvParam">See the uAction parameter.</param>
        /// <param name="fuWinIni">This parameter can be zero if you do not want to update the user profile or broadcast the WM_SETTINGCHANGE message, or it can be one or more of the following values.</param>
        /// <returns>
        /// If the function succeeds, the return value is a nonzero value.
        /// If the function fails, the return value is zero.
        /// </returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        /// <summary>
        /// 
        /// </summary>
        //private string wallpaperResolution;


        /// <summary>
        /// On what side wallpaper2 currenlty is: "right", "left", "top", "bottom".
        /// </summary>
        private string wallpaper2Side = "right";



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
            //this.DrawDisplays();

            var displays = new Display()
                .DrawDisplays(false, this.freeSpaceArea.Bounds.Width / 2, this.freeSpaceArea.Bounds.Height / 2, this.freeSpaceArea, this.searchBtn);
            foreach (var display in displays)
            {
                this.freeSpaceArea.Controls.Add(display);
            }

            this.GetDisplaysResolutions();
        }

        /// <summary>
        /// Hide controls and blank out freeSpaceArea on mouse single click.
        /// </summary>
        private void DualWallpaperApp_MouseClick(object sender, MouseEventArgs e)
        {
            HideSearchAndResolutionControls();
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
            this.detectScreenBtn.Enabled = false;

            int monitor = 0;
            var formList = new List<Form>();
            foreach (Screen screen in Screen.AllScreens)
            {
                monitor++;
                Form form = await DisplayIdentity.DisplayIdentityBox(screen, monitor);
                formList.Add(form);
            }

            // wait 3 seconds before close forms.
            await Task.Delay(2000);

            foreach (Form form in formList)
            {
                await this.CloseForm(form);
            }

            this.detectScreenBtn.Enabled = true;
        }

        // Merge images and store them in the windows registry.
        private void setWallpaperBtn_Click(object sender, EventArgs e)
        {
            // get file names
            string fileName1 = this.freeSpaceArea.Controls[0].Tag != null ? this.freeSpaceArea.Controls[0].Tag.ToString() : string.Empty;
            string fileName2 = this.freeSpaceArea.Controls[1].Tag != null ? this.freeSpaceArea.Controls[1].Tag.ToString() : string.Empty;


            if (!string.IsNullOrEmpty(fileName1)
                && !string.IsNullOrEmpty(fileName2) || mergeScreenBtn.Checked)
            {

                // merge images in bitmap


                // Note: there is no need to save bitmap and images cos it is stored as register key
                // without saving, setting background speed increases.

                var img1 = Image.FromFile(fileName1);
                var img2 = !string.IsNullOrEmpty(fileName2) ? Image.FromFile(fileName2) : null;

                Bitmap bm;

                if (this.mergeScreenBtn.Checked)
                {
                    Console.WriteLine("hey");
                    bm = StretchImageOverTwoDisplays(img1, Screen.AllScreens);
                    SaveBackground(bm, "0", "1");
                }
                else
                {
                    bm = MergeTwoImages(img1, img2, Screen.AllScreens);
                    SaveBackground(bm, "0", "1");
                }




            }

        }


        private void mergeScreenBtn_CheckedChanged(object sender, EventArgs e)
        {
            // remove displays and drow 1 common for both displays 
            // allow to click on it

            // in the case checkbox is unchecked it should display two displays


            if (mergeScreenBtn.Checked)
            {
                freeSpaceArea.Controls.Clear();

                //DrawDisplay();

                var displays = new Display().DrawDisplays(
                    true,
                    this.freeSpaceArea.Bounds.Width / 2,
                    this.freeSpaceArea.Bounds.Height / 2,
                    this.freeSpaceArea,
                    this.searchBtn);

                foreach (var display in displays)
                {
                    this.freeSpaceArea.Controls.Add(display);
                }

            }
            else
            {
                freeSpaceArea.Controls.Clear();

                var displays = new Display().DrawDisplays(
                    false,
                    this.freeSpaceArea.Bounds.Width / 2,
                    this.freeSpaceArea.Bounds.Height / 2,
                    this.freeSpaceArea,
                    this.searchBtn);

                foreach (var display in displays)
                {
                    this.freeSpaceArea.Controls.Add(display);
                }
                //DrawDisplays();
            }


        }


        #endregion
        // -------------------------------------------------------------------



        /// <summary>
        /// Hides search button and resolution labels.
        /// </summary>
        private void HideSearchAndResolutionControls()
        {
            this.searchBtn.Visible = false;

            var resolutionLabels = this.freeSpaceArea.Controls.OfType<Label>()
                .Where(c => c.Tag != null && c.Tag.ToString() == "resolution")
                .ToList();

            foreach (var lbl in resolutionLabels)
            {
                this.freeSpaceArea.Controls.Remove(lbl);
            }
        }

        /// <summary>
        /// Add existing screen resolutions to the resolutionsList 
        /// </summary>
        private void GetDisplaysResolutions()
        {
            foreach (var screen in Screen.AllScreens)
            {
                this.resolutionsList.Items.Add($"{screen.DeviceName.Replace(@"\.", "").Replace(@"\", "")}: {screen.Bounds.Width} x {screen.Bounds.Height}");
            }
        }



        private async Task CloseForm(Form form)
        {
            await Task.Delay(0);
            form.Close();
        }


        /// <summary>
        /// Combine two images into one and return it in form of bitmap.
        /// </summary>
        /// <param name="firstImage">Image 1.</param>
        /// <param name="secondImage">Image 2.</param>
        /// <param name="screens">Array of physical screens.</param>
        /// <returns>Bitmap.</returns>
        public Bitmap MergeTwoImages(Image firstImage, Image secondImage, Screen[] screens)
        {
            // TODO: merge image depends on wallpapers positions 

            // if wallpaper2 is on right on left use exisitng code on width and height
            // else new code requried

            // in the case of second part of code it requiers 4 statemnst checking which monitor is first 

            // TODO:
            // later addiotional check will be required, in the case 
            // when you first change wallpapers and then you will rearange wallpapers sides

            // first screen is a PRIMARY screen.
            var firstScreen = screens[0];
            var secondScreen = screens[1];

            int outputImageWidth = 0;
            int outputImageHeight = 0;
            Bitmap outputImage = null;

            if (this.wallpaper2Side == "left" || this.wallpaper2Side == "right")
            {
                outputImageWidth = firstImage.Width + secondImage.Width;
                outputImageHeight = firstImage.Height > secondImage.Height ? firstImage.Height : secondImage.Height;

                outputImage = new Bitmap(outputImageWidth, outputImageHeight, PixelFormat.Format32bppArgb);

                // TODO : replace 0 and new point() with actual y position 
                if (this.wallpaper2Side == "right")
                {
                    using (var graphics = Graphics.FromImage(outputImage))
                    {
                        graphics.Clear(Color.Transparent);
                        graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size), new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                        graphics.DrawImage(secondImage, new Rectangle(new Point(firstImage.Width, 0), secondImage.Size), new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
                    }
                }
                else if (this.wallpaper2Side == "left")
                {
                    using (var graphics = Graphics.FromImage(outputImage))
                    {
                        graphics.Clear(Color.Transparent);
                        graphics.DrawImage(secondImage, new Rectangle(new Point(), secondImage.Size), new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
                        graphics.DrawImage(firstImage, new Rectangle(new Point(secondImage.Width, 0), firstImage.Size), new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                    }
                }
            }
            else if (this.wallpaper2Side == "top" || this.wallpaper2Side == "bottom")
            {
                outputImageWidth = firstImage.Width > secondImage.Width ? firstImage.Width : secondImage.Width;
                outputImageHeight = firstImage.Height + secondImage.Height;

                outputImage = new Bitmap(outputImageWidth, outputImageHeight, PixelFormat.Format32bppArgb);

                if (this.wallpaper2Side == "top")
                {
                    using (var graphics = Graphics.FromImage(outputImage))
                    {
                        


                        graphics.Clear(Color.Transparent);
                        graphics.DrawImage(secondImage, new Rectangle(new Point(secondScreen.Bounds.X, 0), secondImage.Size ), new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
                        graphics.DrawImage(firstImage, new Rectangle(new Point(0, secondScreen.Bounds.Height), firstImage.Size), new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);

                        //graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size), new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                        //graphics.DrawImage(secondImage, new Rectangle(new Point(firstImage.Width, 0), secondImage.Size), new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
                    }
                }
                else if (this.wallpaper2Side == "bottom")
                {
                    using (var graphics = Graphics.FromImage(outputImage))
                    {
                        graphics.Clear(Color.Transparent);
                        // graphics.DrawImage(secondImage, new Rectangle(new Point(firstImage.Width, 0), secondImage.Size), new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
                        // graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size), new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                    }
                }
            }


            return outputImage;
        }

        private Bitmap StretchImageOverTwoDisplays(Image firstImage, Screen[] screens)
        {
            var firstScreen = screens[0];
            var secondScreen = screens[1];

            var width = firstScreen.Bounds.Width + secondScreen.Bounds.Width;
            var height = firstScreen.Bounds.Height > secondScreen.Bounds.Height ? firstScreen.Bounds.Height : secondScreen.Bounds.Height;
            //var height = 3390;

            var outputImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(outputImage))
            {
                graphics.Clear(Color.Transparent);
                //graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size), new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);

                graphics.DrawImage(firstImage, new Rectangle(0,0, width, height));
            }

                //var outputImageWidth = firstImage.Width + secondImage.Width;
                //var outputImageHeight = firstImage.Height > secondImage.Height ? firstImage.Height : secondImage.Height;

                //var outputImage = new Bitmap(outputImageWidth, outputImageHeight, PixelFormat.Format32bppArgb);

                //// TODO : replace 0 and new point() with actual y position 

                //    using (var graphics = Graphics.FromImage(outputImage))
                //    {
                //        graphics.Clear(Color.Transparent);
                //        graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size), new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                //        //graphics.DrawImage(secondImage, new Rectangle(new Point(firstImage.Width, 0), secondImage.Size), new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
                //    }







                return outputImage;
        }
       

        private void SaveBackground(Bitmap bitmap, string wallpaperStyle, string tileWallpaper)
        {
            // store temporary image in appdata...
            var img = (Image)bitmap;
            string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
            img.Save(tempPath, ImageFormat.Bmp);

            // wallpaper styles:
            // 0 - center
            // 2 - streched
            // 6 - fit
            // 10 - fill


            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            //key.SetValue(@"WallpaperStyle", 0.ToString());
            //key.SetValue(@"TileWallpaper", 1.ToString());
            key.SetValue(@"WallpaperStyle", wallpaperStyle);
            key.SetValue(@"TileWallpaper", tileWallpaper);
            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }






        

 









    }
}
