using DualWallpaper.Enums;
using DualWallpaper.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DualWallpaper
{
    public class BackgroundManager : IBackgroundManager
    {
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
        /// Secondary virtual display position.
        /// </summary>
        private VirtualDisplayLayout VirtualDisplayLayout { get; set; }

        /// <summary>
        /// Total Height of multiple displays.
        /// </summary>
        private int TotalHeight { get; set; }

        /// <summary>
        /// Total Width of multiple displays.
        /// </summary>
        private int TotalWidth { get; set; }

        public BackgroundManager()
        {
            this.VirtualDisplayLayout = VirtualDisplayLayout.None;
        }

        public BackgroundManager(VirtualDisplayLayout secondaryVirtualDisplayLayout)
        {
            this.VirtualDisplayLayout = secondaryVirtualDisplayLayout;
        }

        public BackgroundManager(VirtualDisplayLayout secondaryVirtualDisplayLayout, int totalHeight, int totalWidth)
        {
            this.VirtualDisplayLayout = secondaryVirtualDisplayLayout;
            this.TotalHeight = totalHeight;
            this.TotalWidth = totalWidth;
        }


        /// <summary>
        /// Combine two images into one and return it in form of bitmap.
        /// </summary>
        /// <param name="images">List of images.</param>
        /// <param name="screens">Array of physical screens.</param>
        /// <returns>Bitmap.</returns>
        //private Bitmap MergePictures(List<Image> images, Screen[] screens, VirtualDisplayLayout virtualDisplayLayout)
        private Bitmap MergePictures(List<Image> images, Screen[] screens)
        {


            // -------------------------------------------------------------------------
            // #1. Create a new Bitmap with total width, total height...
            //
            //Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, PixelFormat.Format32bppArgb);
            Bitmap outputImage = new Bitmap(this.TotalWidth, this.TotalHeight, PixelFormat.Format32bppArgb);


            var primaryDisplay = Screen.PrimaryScreen;
            var secondaryDisplay = Screen.AllScreens.Where(x => x != primaryDisplay).FirstOrDefault();

           

            // -------------------------------------------------------------------------
            // #2. Draw image based on the layout
            //
            using (var graphics = Graphics.FromImage(outputImage))
            {
                graphics.Clear(Color.Transparent);



                switch (this.VirtualDisplayLayout)
                {
                    case VirtualDisplayLayout.Left:

                        // case 1 secondary display (y) is smaller or equal the primary display (y)
                        // and its bottom is equal or greater than the primary screen
                        if (secondaryDisplay.Bounds.Y <= primaryDisplay.Bounds.Y
                            && secondaryDisplay.Bounds.Bottom >= primaryDisplay.Bounds.Bottom)
                        {
                            // draw secondary display image firts...
                            graphics.DrawImage(
                               images[1],
                               new Rectangle(new Point(), images[1].Size),
                               new Rectangle(new Point(), images[1].Size),
                               GraphicsUnit.Pixel);

                            // ...draw primary display image
                            //int image0x = images[1].Width;
                            //int image0y = images[1].Height - primaryDisplay.Bounds.Bottom;
                            int image0x = primaryDisplay.Bounds.X + secondaryDisplay.Bounds.Width;
                            int image0y = images[1].Height - primaryDisplay.Bounds.Bottom;
                            graphics.DrawImage(
                                images[0],
                                new Rectangle(new Point(image0x, image0y), images[0].Size),
                                new Rectangle(new Point(), images[0].Size),
                                GraphicsUnit.Pixel);

                           
                        }









                        //if (primaryDisplay.Bounds.Top < 0)
                        //{
                        //    graphics.DrawImage(
                        //        images[1],
                        //        new Rectangle(new Point(), images[1].Size),
                        //        new Rectangle(new Point(), images[1].Size),
                        //        GraphicsUnit.Pixel);

                        //    graphics.DrawImage(
                        //        images[0],
                        //        new Rectangle(new Point(images[1].Width, images[1].Height - primaryDisplay.Bounds.Bottom), images[0].Size),
                        //        new Rectangle(new Point(), images[0].Size),
                        //        GraphicsUnit.Pixel);
                        //}
                        //else
                        //{
                        //    graphics.DrawImage(
                        //        images[1],
                        //        new Rectangle(new Point(0, primaryDisplay.Bounds.Y), images[1].Size),
                        //        new Rectangle(new Point(), images[1].Size),
                        //        GraphicsUnit.Pixel);

                        //    graphics.DrawImage(
                        //        images[0],
                        //        new Rectangle(new Point(images[1].Width, 0), images[0].Size),
                        //        new Rectangle(new Point(), images[0].Size),
                        //        GraphicsUnit.Pixel);

                        //    //special case
                        //    // where image is in left bottom corner, lack of good solution at the moment
                        //}

                        break;








                    case VirtualDisplayLayout.Right:

                        if (primaryDisplay.Bounds.Top < 0)
                        {
                            // when second image has nagative y value, then it should be tread as image 1 having starting point as (x, 0).
                            graphics.DrawImage(
                                images[1],
                                new Rectangle(new Point(images[0].Width, 0), images[1].Size),
                                new Rectangle(new Point(), images[1].Size),
                                GraphicsUnit.Pixel);

                            graphics.DrawImage(
                                images[0],
                                new Rectangle(new Point(0, images[1].Height - primaryDisplay.Bounds.Bottom), images[0].Size),
                                new Rectangle(new Point(), images[0].Size),
                                GraphicsUnit.Pixel);
                        }
                        else
                        {

                            // primary screen is always with coordinates (0,0), so the screen we can take the height difference is one of the highier screens.
                            // grab that hight, ensure it's a positive number, as use as looking for Y.
                            // take higher picture as reference 
                            var higherScreen = screens.OrderByDescending(item => item.Bounds.Height).First();
                            var y = Math.Abs(higherScreen.Bounds.Y);

                            graphics.DrawImage(
                                images[0],
                                new Rectangle(new Point(0, y), images[0].Size),
                                new Rectangle(new Point(), images[0].Size),
                                GraphicsUnit.Pixel);

                            graphics.DrawImage(
                                images[1],
                                new Rectangle(new Point(images[0].Width, primaryDisplay.Bounds.Y), images[1].Size),
                                new Rectangle(new Point(), images[1].Size),
                                GraphicsUnit.Pixel);
                        }

                        break;

                    case VirtualDisplayLayout.Top:

                        graphics.DrawImage(
                           images[1],
                           new Rectangle(new Point(primaryDisplay.Bounds.X, 0), images[1].Size),
                           new Rectangle(new Point(), images[1].Size),
                           GraphicsUnit.Pixel);

                        graphics.DrawImage(
                                images[0],
                                new Rectangle(new Point(0, primaryDisplay.Bounds.Height), images[0].Size),
                                new Rectangle(new Point(), images[0].Size),
                                GraphicsUnit.Pixel);

                        break;

                    case VirtualDisplayLayout.Bottom:

                        graphics.DrawImage(
                            images[0],
                            new Rectangle(new Point(), images[0].Size),
                            new Rectangle(new Point(), images[0].Size),
                            GraphicsUnit.Pixel);

                        graphics.DrawImage(
                           images[1],
                           new Rectangle(new Point(primaryDisplay.Bounds.X, screens[0].Bounds.Height), images[1].Size),
                           new Rectangle(new Point(), images[1].Size),
                           GraphicsUnit.Pixel);

                        break;

                    case VirtualDisplayLayout.None:
                        break;
                    default:
                        break;
                }


            }

            return outputImage;
        }


        private Bitmap StretchBackground(Image firstImage, Screen[] screens)
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

                graphics.DrawImage(firstImage, new Rectangle(0, 0, width, height));
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


        





        /// <summary>
        /// Set Background image for all displays.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="checkBox"></param>
        public void SaveBackground(Panel panel, CheckBox checkBox)
        {
            // get file names
            string fileName1 = panel.Controls[0].Tag != null ? panel.Controls[0].Tag.ToString() : string.Empty;
            string fileName2 = panel.Controls[1].Tag != null ? panel.Controls[1].Tag.ToString() : string.Empty;


            if (!string.IsNullOrEmpty(fileName1)
                && !string.IsNullOrEmpty(fileName2) || checkBox.Checked)
            {

                // merge images in bitmap


                // Note: there is no need to save bitmap and images cos it is stored as register key
                // without saving, setting background speed increases.

                var img1 = Image.FromFile(fileName1);
                Image img2 = !string.IsNullOrEmpty(fileName2) ? Image.FromFile(fileName2) : null;

                List<Image> images = new List<Image>() { img1, img2 };

                Bitmap bm;

                if (checkBox.Checked)
                {
                    bm = StretchBackground(img1, Screen.AllScreens);
                    string tempPath = SaveBackgroundToFile(bm);
                    SaveBackgroundToSystemRegistry(tempPath, "0", "1");
                }
                else
                {
                    //bm = MergePictures(images, Screen.AllScreens, this.VirtualDisplayLayout);
                    bm = MergePictures(images, Screen.AllScreens);
                    string tempPath = SaveBackgroundToFile(bm);
                    SaveBackgroundToSystemRegistry(tempPath, "0", "1");
                }




            }
        }

        public void CleanWallpapers(Panel panel)
        {
            foreach (var u in panel.Controls)
            {
                if (u is PictureBox)
                {
                    var obj = (PictureBox)u;
                    obj.Image = null;
                }
            }
        }

        private string SaveBackgroundToFile(Bitmap bitmap)
        {
            // store temporary image in appdata...
            var img = (Image)bitmap;
            string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
            img.Save(tempPath, ImageFormat.Bmp);

            return tempPath;
        }

        private void SaveBackgroundToSystemRegistry(string tempPath, string wallpaperStyle, string tileWallpaper)
        {
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
