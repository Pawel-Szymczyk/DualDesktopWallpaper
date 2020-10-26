using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperManager
{
    public partial class Form1 : Form
    {
        // https://devblogs.microsoft.com/oldnewthing/?p=25003 



        private const int SPI_SETDESKWALLPAPER = 20;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);





        private readonly bool isDraggingWallpaper1 = false;
        private bool isDraggingWallpaper2 = false;
        private Point move;


        public Form1()
        {
            //m_aeroEnabled = true;

            this.InitializeComponent();

            new DropShadow().ApplyShadows(this);




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



        //listBox1.Items.Add("Device Name: " + screen.DeviceName);
        //listBox1.Items.Add("Bounds: " +
        //    screen.Bounds.ToString());
        //listBox1.Items.Add("Type: " +
        //    screen.GetType().ToString());
        //listBox1.Items.Add("Working Area: " +
        //    screen.WorkingArea.ToString());
        //listBox1.Items.Add("Primary Screen: " +
        //    screen.Primary.ToString());







        private string wallpaperFileName1 = string.Empty;
        private string wallpaperFileName2 = string.Empty;


        /// <summary>
        /// Pick image for 1st monitor.
        /// </summary>
        private void wallpaper1_DoubleClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.wallpaperFileName1 = dialog.FileName;

                var img = Image.FromFile(this.wallpaperFileName1);
                this.wallpaper1.Image = img;
                this.wallpaper1.SizeMode = PictureBoxSizeMode.Zoom;

                this.setWallpaperBtnEnabled();
            }
        }

        /// <summary>
        /// Pick image for 2nd monitor.
        /// </summary>
        private void wallpaper2_DoubleClick(object sender, EventArgs e)
        {
            var dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.wallpaperFileName2 = dialog.FileName;

                var img = Image.FromFile(this.wallpaperFileName2);
                this.wallpaper2.Image = img;
                this.wallpaper2.SizeMode = PictureBoxSizeMode.Zoom;

                this.setWallpaperBtnEnabled();
            }
        }

        /// <summary>
        /// Enable Set Wallpaper Button only when two wallpapers chosen.
        /// </summary>
        private void setWallpaperBtnEnabled()
        {
            if (!string.IsNullOrEmpty(this.wallpaperFileName1)
               && !string.IsNullOrEmpty(this.wallpaperFileName2))
            {
                this.setWallpaperBtn.Enabled = true;
            }
        }

        /// <summary>
        /// Combine two images into one and return it in form of bitmap.
        /// </summary>
        /// <param name="firstImage">Image 1.</param>
        /// <param name="secondImage">Image 2.</param>
        /// <returns>Bitmap.</returns>
        public static Bitmap MergeTwoImages(Image firstImage, Image secondImage)
        {
            // TODO: merge image depends on wallpapers positions 



            int outputImageWidth = firstImage.Width + secondImage.Width;
            int outputImageHeight = firstImage.Height > secondImage.Height ? firstImage.Height : secondImage.Height;
            var outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (var graphics = Graphics.FromImage(outputImage))
            {
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size), new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                graphics.DrawImage(secondImage, new Rectangle(new Point(firstImage.Width, 0), secondImage.Size), new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
            }
            return outputImage;
        }

        // Merge images and store them in the windows registry.
        private void setWallpaperBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.wallpaperFileName1)
                && !string.IsNullOrEmpty(this.wallpaperFileName2))
            {

                // merge images in bitmap

                //foreach (var screen in Screen.AllScreens)
                //{ 
                //}

                // Note: there is no need to save bitmap and images cos it is stored as register key
                // without saving, setting background speed increases.

                var img1 = Image.FromFile(this.wallpaperFileName1);
                var img2 = Image.FromFile(this.wallpaperFileName2);
                Bitmap bm = MergeTwoImages(img1, img2);
                //bm.Save("result.png", ImageFormat.Png);



                //System.Drawing.Image img = System.Drawing.Image.FromFile(@"F:\Projects\VStudio\WallpaperManager\WallpaperManager\bin\Debug\result.png");

                // store temporary image in appdata...
                var img = (Image)bm;
                string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
                img.Save(tempPath, ImageFormat.Bmp);



                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                key.SetValue(@"WallpaperStyle", 0.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
                SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);


            }

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



        /// <summary>
        /// Close form.
        /// </summary>
        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }








        private void wallpaper2_MouseDown(object sender, MouseEventArgs e)
        {
            var c = sender as Control;
            this.isDraggingWallpaper2 = true;
            this.move = e.Location;
        }

        private void wallpaper2_MouseMove(object sender, MouseEventArgs e)
        {
            // get wallpaper1 width and height
            //var wallpaper1Height = this.wallpaper1.Bounds.Height;
            //var wallpaper1Width = this.wallpaper1.Bounds.Width;


            // raczej tego nie potrzebuje... (do usuniecia)
            //if (wallpaper2.Bounds.IntersectsWith(wallpaper1.Bounds)) 
            //{
            //    wallpaper2.BackColor = Color.Pink;           
            //}
            //else
            //{
            //    wallpaper2.BackColor = Color.LightGray;
            //}





            // move wallpaper 2 
            if (this.isDraggingWallpaper2 == true)
            {
                var c = sender as Control;

                this.wallpaper2.Left += e.X - this.move.X;
                this.wallpaper2.Top += e.Y - this.move.Y;




                // walpaper2 corners
                //var bottomLeftY = this.wallpaper2.Bounds.Bottom;
                //var bottomLeftX = this.wallpaper2.Bounds.X;
                //var bottomRightY =
                //var topLeft =
                //var topRight =




                //for (int i = 0; i < pictureBoxList.Count(); i++)
                //{
                //    if (c.Equals(pictureBoxList[i]))
                //    {
                //        pictureBoxList[i].Left += e.X - move.X;
                //        pictureBoxList[i].Top += e.Y - move.Y;
                //    }
                //}
            }
        }

        private readonly int margin = 3;
        private void wallpaper2_MouseUp(object sender, MouseEventArgs e)
        {
            this.isDraggingWallpaper2 = false;

            //Console.WriteLine("Wallpaper 1 Left: " + wallpaper1.Bounds.Left.ToString());
            //Console.WriteLine("Wallpaper 1 Right: " + wallpaper1.Bounds.Right.ToString());


            // ensure that there is no collision between wallpapers
            if (!this.wallpaper2.Bounds.IntersectsWith(this.wallpaper1.Bounds))
            {

                // the left side of wallpaper 1
                if (this.wallpaper2.Bounds.Right < this.wallpaper1.Bounds.Left)
                {
                    this.wallpaper2.Left = this.wallpaper1.Bounds.Left - this.wallpaper2.Width - this.margin;
                }

                // the right side of wallpaper 1
                if (this.wallpaper2.Bounds.Left > this.wallpaper1.Bounds.Right)
                {
                    this.wallpaper2.Left = this.wallpaper1.Bounds.Right + this.margin;
                }

                // the top side of wallpaper 1
                if (this.wallpaper2.Bounds.Bottom < this.wallpaper1.Bounds.Top)
                {
                    this.wallpaper2.Top = this.wallpaper1.Bounds.Top - this.wallpaper2.Height - this.margin;
                }

                // the bottom side of wallpaper 1 
                if (this.wallpaper2.Bounds.Top > this.wallpaper1.Bounds.Bottom)
                {
                    this.wallpaper2.Top = this.wallpaper1.Bounds.Bottom + this.margin;
                }

            }
            else
            {
                // the left inner side of wallpaper 1
                if (this.wallpaper2.Bounds.Left < (this.wallpaper1.Bounds.Left + (this.wallpaper1.Bounds.Width / 2)))
                {
                    this.wallpaper2.Left = this.wallpaper1.Bounds.Left - this.wallpaper2.Width - this.margin;
                }

                // the right inner side of wallpaper 1
                if (this.wallpaper2.Bounds.Left > (this.wallpaper1.Bounds.Right - (this.wallpaper1.Bounds.Width / 2)))
                {
                    this.wallpaper2.Left = this.wallpaper1.Bounds.Right + this.margin;
                }

                //  dokonczyc ...
                //// the top inner side of wallpaper 1
                //if (wallpaper2.Bounds.Top < (wallpaper1.Bounds.Top + (wallpaper1.Bounds.Height / 2)))
                //{
                //    wallpaper2.Top = wallpaper1.Bounds.Top - wallpaper2.Height - margin;
                //}
            }


        }
















    }
}
