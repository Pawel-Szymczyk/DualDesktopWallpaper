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

namespace WallpaperManager
{
    public static class BackgroundManager
    {

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
        /// Difault position of the 2md display.
        /// </summary>
        private static string defaultLayout = "right";
        private static string DefaultLayout 
        {
            get 
            {
                var screens = Screen.AllScreens;

                if (screens[1].Bounds.X < 0)
                {
                    return "left";
                }
                else if (screens[1].Bounds.X >= 0 && screens[1].Bounds.Y < 0)
                {
                    return "top";
                }
                else if (screens[1].Bounds.X >= 0 && screens[1].Bounds.Y >= Screen.AllScreens[0].Bounds.Height)
                {
                    return "bottom";
                }
                else
                {
                    return "right";
                }
            }
        }




        /// <summary>
        /// Combine two images into one and return it in form of bitmap.
        /// </summary>
        /// <param name="images">List of images.</param>
        /// <param name="screens">Array of physical screens.</param>
        /// <returns>Bitmap.</returns>
        private static Bitmap MergePictures(List<Image> images, Screen[] screens, string layout)
        {
            int outputImageWidth = 0;
            int outputImageHeight = 0;

            // -------------------------------------------------------------------------
            // Define total height and width of the picture, depends on the layout.
            //
            if (layout == "top" || layout == "bottom")
            {
                outputImageHeight = images.Sum(i => i.Height);
                
                if (screens[1].Bounds.X + screens[1].Bounds.Width > screens[0].Bounds.Width) 
                {
                    var difference = images[0].Width - screens[1].Bounds.X ;
                    outputImageWidth = images[0].Width + images[1].Width - difference;
                }
                else
                {
                    outputImageWidth = images[0].Width > images[1].Width ? images[0].Width : images[1].Width;
                }
            }
            else if (layout == "right" || layout == "left")
            {
                outputImageWidth = images.Sum(i => i.Width);

                // when second image is upper than primary, it has negative Y value.
                if (screens[1].Bounds.Top < 0)
                {
                    outputImageHeight = images[0].Height + images[1].Height - screens[1].Bounds.Bottom;
                }
                // when second image is lower than primary, it has positve Y value.
                else
                {
                    outputImageHeight = images[0].Height > images[1].Height ? images[0].Height + screens[1].Bounds.Top : images[1].Height + screens[1].Bounds.Top;
                }

                // special case, where top is negative but bottom is grater than primarys' height
                if (screens[1].Bounds.Top < 0 && screens[1].Bounds.Bottom > images[0].Height)
                {
                    outputImageHeight = images[0].Height > images[1].Height ? images[0].Height : images[1].Height;
                }
            }

            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, PixelFormat.Format32bppArgb);


            // -------------------------------------------------------------------------
            // Draw image based on the layout
            //
            using (var graphics = Graphics.FromImage(outputImage))
            {
                graphics.Clear(Color.Transparent);

                switch (layout)
                {
                    case "right":
                        if (screens[1].Bounds.Top < 0)
                        {
                            // when second image has nagative y value, then it should be tread as image 1 having starting point as (x, 0).
                            graphics.DrawImage(
                                images[1], 
                                new Rectangle(new Point(images[0].Width, 0), images[1].Size), 
                                new Rectangle(new Point(), images[1].Size), 
                                GraphicsUnit.Pixel);

                            graphics.DrawImage(
                                images[0], 
                                new Rectangle(new Point(0, images[1].Height - screens[1].Bounds.Bottom), images[0].Size), 
                                new Rectangle(new Point(), images[0].Size), 
                                GraphicsUnit.Pixel);
                        }
                        else 
                        {
                            // need to get max hight e,g. 1920 (H), height of monitor(h), equestion: (H-h)/2 = y

                            var y = (outputImageHeight - images[0].Height) / 2;

                            graphics.DrawImage(
                                images[0], 
                                new Rectangle(new Point(0,y), images[0].Size), 
                                new Rectangle(new Point(), images[0].Size), 
                                GraphicsUnit.Pixel);

                            graphics.DrawImage(
                                images[1],
                                new Rectangle(new Point(images[0].Width, screens[1].Bounds.Y), images[1].Size),
                                new Rectangle(new Point(), images[1].Size),
                                GraphicsUnit.Pixel);
                        }
                        break;

                    case "left":
                        if (screens[1].Bounds.Top < 0)
                        {
                            graphics.DrawImage(
                                images[1],
                                new Rectangle(new Point(), images[1].Size),
                                new Rectangle(new Point(), images[1].Size),
                                GraphicsUnit.Pixel);

                            graphics.DrawImage(
                                images[0],
                                new Rectangle(new Point(images[1].Width, images[1].Height - screens[1].Bounds.Bottom), images[0].Size),
                                new Rectangle(new Point(), images[0].Size),
                                GraphicsUnit.Pixel);
                        }
                        else
                        {
                            graphics.DrawImage(
                                images[1],
                                new Rectangle(new Point(0, screens[1].Bounds.Y), images[1].Size),
                                new Rectangle(new Point(), images[1].Size),
                                GraphicsUnit.Pixel);

                            graphics.DrawImage(
                                images[0],
                                new Rectangle(new Point(images[1].Width, 0), images[0].Size),
                                new Rectangle(new Point(), images[0].Size),
                                GraphicsUnit.Pixel);

                            //special case
                            // where image is in left bottom corner, lack of good solution at the moment
                        }
                        break;

                    case "top":
                        graphics.DrawImage(
                            images[1],
                            new Rectangle(new Point(screens[1].Bounds.X, 0), images[1].Size),
                            new Rectangle(new Point(), images[1].Size),
                            GraphicsUnit.Pixel);

                        graphics.DrawImage(
                                images[0],
                                new Rectangle(new Point(0, screens[1].Bounds.Height), images[0].Size),
                                new Rectangle(new Point(), images[0].Size),
                                GraphicsUnit.Pixel);
                        break;

                    case "bottom":
                        graphics.DrawImage(
                            images[0],
                            new Rectangle(new Point(), images[0].Size),
                            new Rectangle(new Point(), images[0].Size),
                            GraphicsUnit.Pixel);

                        graphics.DrawImage(
                           images[1],
                           new Rectangle(new Point(screens[1].Bounds.X, screens[0].Bounds.Height), images[1].Size),
                           new Rectangle(new Point(), images[1].Size),
                           GraphicsUnit.Pixel);
                        break;

                    default:
                        break;
                }
            }

            return outputImage;
        }


        private static Bitmap StretchBackground(Image firstImage, Screen[] screens)
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


        private static void SaveBackground(Bitmap bitmap, string wallpaperStyle, string tileWallpaper)
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


        /// <summary>
        /// Set Background image for all displays.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="checkBox"></param>
        public static void SetBackground(Panel panel, CheckBox checkBox)
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
                    bm = BackgroundManager.StretchBackground(img1, Screen.AllScreens);
                    BackgroundManager.SaveBackground(bm, "0", "1");
                }
                else
                {
                    bm = BackgroundManager.MergePictures(images, Screen.AllScreens, DefaultLayout);
                    BackgroundManager.SaveBackground(bm, "0", "1");
                }

                


            }
        }

        public static void CleanWallpapers(Panel panel)
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





    }
}
