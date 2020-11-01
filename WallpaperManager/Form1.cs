using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperManager
{
    public partial class Form1 : Form
    {
        // https://devblogs.microsoft.com/oldnewthing/?p=25003 

        private string wallpaperFileName1 = string.Empty;
        private string wallpaperFileName2 = string.Empty;


        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);




        /// <summary>
        /// On what side wallpaper2 currenlty is: "right", "left", "top", "bottom".
        /// </summary>
        private string wallpaper2Side = "right";

        //private readonly bool isDraggingWallpaper1 = false;
        //private bool isDraggingWallpaper2 = false;
        //private Point move;


        public Form1()
        {
            //m_aeroEnabled = true;

            this.InitializeComponent();

            //new DropShadow().ApplyShadows(this);


        }

        private void Form1_Load(object sender, EventArgs e)
        {


            this.DrawDisplays();

            this.GetDisplaysResolutions();
        }

        private void GetDisplaysResolutions()
        {

            foreach (var screen in Screen.AllScreens)
            {
                this.listBox1.Items.Add($"{screen.DeviceName.Replace(@"\.", "").Replace(@"\", "")}: {screen.Bounds.Width} x {screen.Bounds.Height}");
            }


        }


        // ------------------------------------------
        // class Display
        
        public void DrawDisplay()
        {
            var panelMiddlePointX = this.freeSpaceArea.Bounds.Width / 2;
            var panelMiddlePointY = this.freeSpaceArea.Bounds.Height / 2;

            Point realDisplayOneShiftLocation = new Point();

            int scale = 21;
            int margin = 1;

            Screen screen = null;
            if (Screen.AllScreens[0].Primary)
            {
                screen = Screen.AllScreens[0];
            }
            else
            {
                screen = Screen.AllScreens[1];
            }



            // actual screen scaled size
            var scaledScreenHeight = screen.Bounds.Size.Height / scale;
            var scaledScreenWidth = (screen.Bounds.Size.Width / scale);

            // center of actual screen
            var displayCenterX = scaledScreenWidth / 2;
            var displayCenterY = scaledScreenHeight / 2;


            var display = new PictureBox();
            display.Name = screen.DeviceName.Replace(@"\.", "").Replace(@"\", "").ToLower();
            display.Size = new Size(scaledScreenWidth, scaledScreenHeight);
            display.BackColor = Color.LightGray;
            display.BorderStyle = BorderStyle.FixedSingle;
            display.Paint += new PaintEventHandler((sender, e) =>
            {
                e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                string text = "1 | 2";
                Font font = new Font("Segoe UI", 40);

                SizeF textSize = e.Graphics.MeasureString(text, font);
                PointF locationToDraw = new PointF();
                locationToDraw.X = (display.Width / 2) - (textSize.Width / 2);
                locationToDraw.Y = (display.Height / 2) - (textSize.Height / 2);

                e.Graphics.DrawString(text, font, Brushes.White, locationToDraw);
            });


            realDisplayOneShiftLocation = new Point(panelMiddlePointX - displayCenterX, panelMiddlePointY - displayCenterY);

            display.Location = realDisplayOneShiftLocation;
            display.MouseDoubleClick += new MouseEventHandler(wallpaper1_DoubleClick);



            this.freeSpaceArea.Controls.Add(display);
        }


        private void DrawDisplays()
        {
            // panel area
            var panelMiddlePointX = this.freeSpaceArea.Bounds.Width / 2;
            var panelMiddlePointY = this.freeSpaceArea.Bounds.Height / 2;

            Point realDisplayOneShiftLocation = new Point();

            int scale = 21;
            int margin = 1;
            
             
            //int index = 1;
            foreach (var screen in Screen.AllScreens)
            {
                // actual screen scaled size
                var scaledScreenHeight = screen.Bounds.Size.Height / scale;
                var scaledScreenWidth = (screen.Bounds.Size.Width / scale);

                // center of actual screen
                var displayCenterX = scaledScreenWidth / 2;
                var displayCenterY = scaledScreenHeight / 2;

                var display = new PictureBox();
                display.Name = screen.DeviceName.Replace(@"\.", "").Replace(@"\", "").ToLower();
                display.Size = new Size(scaledScreenWidth, scaledScreenHeight);
                display.BackColor = Color.LightGray;
                display.BorderStyle = BorderStyle.FixedSingle;
                display.Paint += new PaintEventHandler(wallpaper_Label);

                if (screen.Primary)
                {
                    realDisplayOneShiftLocation = new Point(panelMiddlePointX - displayCenterX, panelMiddlePointY - displayCenterY);

                    display.Location = realDisplayOneShiftLocation;
                    display.MouseDoubleClick += new MouseEventHandler(wallpaper1_DoubleClick);
                    display.MouseClick += new MouseEventHandler((sender, e) => wallpaper_SingleClick(sender, e, Screen.AllScreens));
                }
                else
                {
                    int x = 0;
                    int y = 0;
                    if(screen.Bounds.X < 0)
                    {
                        // left side

                        x = realDisplayOneShiftLocation.X - (screen.Bounds.Width / scale) - margin;
                        y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / scale);
                    }
                    else if(screen.Bounds.X >=0 && screen.Bounds.Y < 0)
                    {
                        // top

                        y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / scale) - margin;
                        x = realDisplayOneShiftLocation.X + (screen.Bounds.X / scale);

                    }
                    else if(screen.Bounds.X >= 0 && screen.Bounds.Y >= Screen.AllScreens[0].Bounds.Height)
                    {
                        // bottom

                        y = realDisplayOneShiftLocation.Y + (Screen.AllScreens[0].Bounds.Height / scale) + margin;
                        x = realDisplayOneShiftLocation.X + (screen.Bounds.X / scale);
                    }
                    else
                    {
                        // right 

                        x = realDisplayOneShiftLocation.X + (Screen.AllScreens[0].Bounds.Width / scale) + margin;
                        y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y /scale);
                    }


                    display.Location = new Point(x, y);
                    display.MouseDoubleClick += new MouseEventHandler(wallpaper2_DoubleClick);
                    display.MouseClick += new MouseEventHandler((sender, e) => wallpaper_SingleClick(sender, e, Screen.AllScreens));
                }

                
                this.freeSpaceArea.Controls.Add(display);
                
            }

        }
        private string wallpaperResolution;
        private void wallpaper_SingleClick(object sender, MouseEventArgs e, Screen[] screens)
        {
            // change button visisbility to true
            // add button event
            // add screen refrences to button

            

            PictureBox pictureBox = (PictureBox)sender;

            // remove any labels first

            var dynamicLabels = this.freeSpaceArea.Controls.OfType<Label>().Where(c => c.Tag != null && c.Tag.ToString() == "resolution").ToList();

            foreach (var lbl in dynamicLabels)
            {
                this.freeSpaceArea.Controls.Remove(lbl);
            }


            // add label to each click picturebox with its resoultion, remove old label
            Label label = new Label();
            label.Font = new Font("Segoe UI", 14);
            label.Size = new Size(120, 20);
            label.Location = new Point(pictureBox.Bounds.Location.X, pictureBox.Bounds.Location.Y -50);
            label.Name = $"{pictureBox.Name}";
            label.Tag = "resolution";
            if (pictureBox.Name == "display1")
            {
                label.Text = $"{screens[0].Bounds.Width} x {screens[0].Bounds.Height}";
                wallpaperResolution = $"{screens[0].Bounds.Width} x {screens[0].Bounds.Height}";
            }
            else if(pictureBox.Name == "display2")
            {
                label.Text = $"{screens[1].Bounds.Width} x {screens[1].Bounds.Height}";
                wallpaperResolution = $"{screens[1].Bounds.Width} x {screens[1].Bounds.Height}";
            }
            freeSpaceArea.Controls.Add(label);

            button1.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(@"https://www.google.com/search?q=wallpaper+" + wallpaperResolution);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            this.button1.Visible = false;

            var dynamicLabels = this.freeSpaceArea.Controls.OfType<Label>().Where(c => c.Tag != null && c.Tag.ToString() == "resolution").ToList();

            foreach (var lbl in dynamicLabels)
            {
                this.freeSpaceArea.Controls.Remove(lbl);
            }
        }

        private void wallpaper_Label(object sender, PaintEventArgs e)
        {
            PictureBox display = (PictureBox)sender;

            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            string text = display.Name.Replace(@"display", "");
            Font font = new Font("Segoe UI", 40);

            SizeF textSize = e.Graphics.MeasureString(text, font);
            PointF locationToDraw = new PointF();
            locationToDraw.X = (display.Width / 2) - (textSize.Width / 2);
            locationToDraw.Y = (display.Height / 2) - (textSize.Height / 2);

            e.Graphics.DrawString(text, font, Brushes.White, locationToDraw);
        }

        private async Task<Form> CreateForm(Screen screen, int monitor)
        {

            // ----------FORM--------------
            var form = new Form
            {
                BackColor = Color.FromArgb(0, 102, 204),
                FormBorderStyle = FormBorderStyle.None,
                Text = $"form ",
                StartPosition = FormStartPosition.Manual,

                Bounds = new Rectangle(
                screen.Bounds.X + 100, screen.Bounds.Y + 100,
                screen.Bounds.Width, screen.Bounds.Height),
                Size = new Size(300, 300),
                Padding = new Padding(100),
                TopMost = true
            };

            await Task.Delay(50);

            // ---------LABEL--------------
            var label = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            label.Font = new Font("Serif", 60, FontStyle.Bold);
            label.Text = monitor.ToString();
            label.ForeColor = Color.White;

            form.Controls.Add(label);

            form.Show();

            return form;
        }

        private async Task CloseForm(Form form)
        {
            await Task.Delay(0);
            form.Close();
        }


        private async void detectScreenBtn_Click(object sender, EventArgs e)
        {
            this.detectScreenBtn.Enabled = false;

            int monitor = 0;
            var formList = new List<Form>();
            foreach (Screen screen in Screen.AllScreens)
            {
                monitor++;
                Form form = await this.CreateForm(screen, monitor);
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

  






        /// <summary>
        /// Pick image for 1st monitor.
        /// </summary>
        private void wallpaper1_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.wallpaperFileName1 = dialog.FileName;

                var img = Image.FromFile(this.wallpaperFileName1);
                pictureBox.Image = img;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                this.setWallpaperBtnEnabled();
            }
        }

        /// <summary>
        /// Pick image for 2nd monitor.
        /// </summary>
        private void wallpaper2_DoubleClick(object sender, EventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;

            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.wallpaperFileName2 = dialog.FileName;

                var img = Image.FromFile(this.wallpaperFileName2);
                pictureBox.Image = img;
                pictureBox.SizeMode = PictureBoxSizeMode.Zoom;

                this.setWallpaperBtnEnabled();
            }
        }

        /// <summary>
        /// Enable Set Wallpaper Button only when two wallpapers chosen.
        /// </summary>
        private void setWallpaperBtnEnabled()
        {
            if (!string.IsNullOrEmpty(this.wallpaperFileName1)
               && !string.IsNullOrEmpty(this.wallpaperFileName2) || checkBox1.Checked)
            {
                this.setWallpaperBtn.Enabled = true;
            }
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



            //outputImageWidth = firstImage.Width + secondImage.Width;
            //outputImageHeight = firstImage.Height > secondImage.Height ? firstImage.Height : secondImage.Height;

            //var outputImage = new Bitmap(outputImageWidth, outputImageHeight, PixelFormat.Format32bppArgb);
            //using (var graphics = Graphics.FromImage(outputImage))
            //{
            //    graphics.Clear(Color.Transparent);
            //    graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size), new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
            //    graphics.DrawImage(secondImage, new Rectangle(new Point(firstImage.Width, 0), secondImage.Size), new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
            //}
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

        // Merge images and store them in the windows registry.
        private void setWallpaperBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.wallpaperFileName1)
                && !string.IsNullOrEmpty(this.wallpaperFileName2) || checkBox1.Checked)
            {

                // merge images in bitmap


                // Note: there is no need to save bitmap and images cos it is stored as register key
                // without saving, setting background speed increases.

                var img1 = Image.FromFile(this.wallpaperFileName1);
                var img2 = !string.IsNullOrEmpty(this.wallpaperFileName2) ? Image.FromFile(this.wallpaperFileName2) : null;

                Bitmap bm;

                if (this.checkBox1.Checked)
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


                  
                //bm.Save("result.png", ImageFormat.Png);



                //System.Drawing.Image img = System.Drawing.Image.FromFile(@"F:\Projects\VStudio\WallpaperManager\WallpaperManager\bin\Debug\result.png");

               


            }

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



        // -----------------------------------------------------
        // Trick allowing for moving borderless form
        //
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();







        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // remove displays and drow 1 common for both displays 
            // allow to click on it

            // in the case checkbox is unchecked it should display two displays

            var x = freeSpaceArea.Controls.OfType<PictureBox>();

            if (checkBox1.Checked)
            {
                //foreach (Control item in freeSpaceArea.Controls.OfType<PictureBox>())
                //{
                //    freeSpaceArea.Controls.Remove(item);
                //    item.Dispose();
                //}
                freeSpaceArea.Controls.Clear();

                DrawDisplay();

            }
            else
            {
                freeSpaceArea.Controls.Clear();
                DrawDisplays();
            }

        }

 

        /// <summary>
        /// Close form.
        /// </summary>
        //private void closeBtn_Click(object sender, EventArgs e)
        //{
        //    this.Dispose();
        //    this.Close();
        //}

        // Explanation: 
        // I have removed function of drag and drop of the displays features to this program, because lack of 
        // knowledge and resources to find out how to implement these changes also in windows 10 desktops. 
        // This feature would be required to avoid switching between my solution and Windows 10 Display settings.


        //private void wallpaper2_MouseDown(object sender, MouseEventArgs e)
        //{
        //    var c = sender as Control;
        //    this.isDraggingWallpaper2 = true;
        //    this.move = e.Location;
        //}

        //private void wallpaper2_MouseMove(object sender, MouseEventArgs e)
        //{
        //    // get wallpaper1 width and height
        //    //var wallpaper1Height = this.wallpaper1.Bounds.Height;
        //    //var wallpaper1Width = this.wallpaper1.Bounds.Width;


        //    // raczej tego nie potrzebuje... (do usuniecia)
        //    //if (wallpaper2.Bounds.IntersectsWith(wallpaper1.Bounds)) 
        //    //{
        //    //    wallpaper2.BackColor = Color.Pink;           
        //    //}
        //    //else
        //    //{
        //    //    wallpaper2.BackColor = Color.LightGray;
        //    //}





        //    // move wallpaper 2 
        //    if (this.isDraggingWallpaper2 == true)
        //    {
        //        var c = sender as Control;

        //        this.wallpaper2.Left += e.X - this.move.X;
        //        this.wallpaper2.Top += e.Y - this.move.Y;




        //        // walpaper2 corners
        //        //var bottomLeftY = this.wallpaper2.Bounds.Bottom;
        //        //var bottomLeftX = this.wallpaper2.Bounds.X;
        //        //var bottomRightY =
        //        //var topLeft =
        //        //var topRight =




        //        //for (int i = 0; i < pictureBoxList.Count(); i++)
        //        //{
        //        //    if (c.Equals(pictureBoxList[i]))
        //        //    {
        //        //        pictureBoxList[i].Left += e.X - move.X;
        //        //        pictureBoxList[i].Top += e.Y - move.Y;
        //        //    }
        //        //}
        //    }
        //}

        //private readonly int margin = 3;
        //private void wallpaper2_MouseUp(object sender, MouseEventArgs e)
        //{
        //    this.isDraggingWallpaper2 = false;

        //    //Console.WriteLine("Wallpaper 1 Left: " + wallpaper1.Bounds.Left.ToString());
        //    //Console.WriteLine("Wallpaper 1 Right: " + wallpaper1.Bounds.Right.ToString());


        //    // ensure that there is no collision between wallpapers
        //    if (!this.wallpaper2.Bounds.IntersectsWith(this.wallpaper1.Bounds))
        //    {

        //        // the left side of wallpaper 1
        //        if (this.wallpaper2.Bounds.Right < this.wallpaper1.Bounds.Left)
        //        {
        //            this.wallpaper2.Left = this.wallpaper1.Bounds.Left - this.wallpaper2.Width - this.margin;
        //            this.wallpaper2Side = "left";
        //        }

        //        // the right side of wallpaper 1
        //        if (this.wallpaper2.Bounds.Left > this.wallpaper1.Bounds.Right)
        //        {
        //            this.wallpaper2.Left = this.wallpaper1.Bounds.Right + this.margin;
        //            this.wallpaper2Side = "right";
        //        }

        //        // the top side of wallpaper 1
        //        if (this.wallpaper2.Bounds.Bottom < this.wallpaper1.Bounds.Top)
        //        {
        //            this.wallpaper2.Top = this.wallpaper1.Bounds.Top - this.wallpaper2.Height - this.margin;
        //            this.wallpaper2Side = "top";
        //        }

        //        // the bottom side of wallpaper 1 
        //        if (this.wallpaper2.Bounds.Top > this.wallpaper1.Bounds.Bottom)
        //        {
        //            this.wallpaper2.Top = this.wallpaper1.Bounds.Bottom + this.margin;
        //            this.wallpaper2Side = "bottom";
        //        }

        //    }
        //    else
        //    {
        //        // the left inner side of wallpaper 1
        //        if (this.wallpaper2.Bounds.Left < (this.wallpaper1.Bounds.Left + (this.wallpaper1.Bounds.Width / 2)))
        //        {
        //            this.wallpaper2.Left = this.wallpaper1.Bounds.Left - this.wallpaper2.Width - this.margin;
        //            this.wallpaper2Side = "left";
        //        }

        //        // the right inner side of wallpaper 1
        //        if (this.wallpaper2.Bounds.Left > (this.wallpaper1.Bounds.Right - (this.wallpaper1.Bounds.Width / 2)))
        //        {
        //            this.wallpaper2.Left = this.wallpaper1.Bounds.Right + this.margin;
        //            this.wallpaper2Side = "right";
        //        }

        //        //  dokonczyc ...
        //        //// the top inner side of wallpaper 1
        //        //if (wallpaper2.Bounds.Top < (wallpaper1.Bounds.Top + (wallpaper1.Bounds.Height / 2)))
        //        //{
        //        //    wallpaper2.Top = wallpaper1.Bounds.Top - wallpaper2.Height - margin;
        //        //}
        //    }


        //}
















    }
}
