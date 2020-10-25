using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace WallpaperManager
{
    public partial class Form1 : Form
    {
        // https://devblogs.microsoft.com/oldnewthing/?p=25003 



        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

     



        private bool isDragging = false;
        Point move;


        public Form1()
        {
            //m_aeroEnabled = true;

            InitializeComponent();

            new DropShadow().ApplyShadows(this);




        }

        Timer formCloseTimer = new Timer();

        


        private async Task<Form> CreateForm(Screen screen, int monitor)
        {

            // ----------FORM--------------
            Form form = new Form();
            form.BackColor = Color.FromArgb(0, 102, 204);
            form.FormBorderStyle = FormBorderStyle.None;
            form.Text = $"form ";
            form.StartPosition = FormStartPosition.Manual;

            form.Bounds = new Rectangle(
                screen.Bounds.X + 100, screen.Bounds.Y + 100,
                screen.Bounds.Width, screen.Bounds.Height);
            form.Size = new Size(300, 300);
            form.Padding = new Padding(100);
            form.TopMost = true;

            await Task.Delay(50);

            // ---------LABEL--------------
            Label label = new Label()
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
            List<Form> formList = new List<Form>();
            foreach (var screen in Screen.AllScreens)
            {
                monitor++;
                var form = await this.CreateForm(screen, monitor);
                formList.Add(form);
            }

            // wait 3 seconds before close forms.
            await Task.Delay(2000);

            foreach (var form in formList)
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







        string wallpaperFileName1 = string.Empty;
        string wallpaperFileName2 = string.Empty;


        /// <summary>
        /// Pick image for 1st monitor.
        /// </summary>
        private void wallpaper1_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                wallpaperFileName1 = dialog.FileName;

                Image img = Image.FromFile(wallpaperFileName1);
                this.wallpaper1.Image = img;
                this.wallpaper1.SizeMode = PictureBoxSizeMode.Zoom;

            }
        }

        /// <summary>
        /// Pick image for 2nd monitor.
        /// </summary>
        private void wallpaper2_DoubleClick(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                wallpaperFileName2 = dialog.FileName;

                Image img = Image.FromFile(wallpaperFileName2);
                this.wallpaper2.Image = img;
                this.wallpaper2.SizeMode = PictureBoxSizeMode.Zoom;

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
            
            int outputImageWidth = firstImage.Width + secondImage.Width;
            int outputImageHeight = firstImage.Height > secondImage.Height ? firstImage.Height : secondImage.Height;
            Bitmap outputImage = new Bitmap(outputImageWidth, outputImageHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(outputImage))
            {
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(firstImage, new Rectangle(new Point(), firstImage.Size), new Rectangle(new Point(), firstImage.Size), GraphicsUnit.Pixel);
                graphics.DrawImage(secondImage, new Rectangle(new Point(firstImage.Width, 0), secondImage.Size), new Rectangle(new Point(), secondImage.Size), GraphicsUnit.Pixel);
            }
            return outputImage;
        }

        // Merge images and store them in the windows registry.
        private void button2_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(this.wallpaperFileName1) 
                && !string.IsNullOrEmpty(this.wallpaperFileName2))
            {

                // merge images in bitmap

                //foreach (var screen in Screen.AllScreens)
                //{ 
                //}

                // Note: there is no need to save bitmap and images cos it is stored as register key
                // without saving, setting background speed increases.

                Image img1 = Image.FromFile(this.wallpaperFileName1);
                Image img2 = Image.FromFile(this.wallpaperFileName2);
                Bitmap bm = MergeTwoImages(img1, img2);
                //bm.Save("result.png", ImageFormat.Png);



                //System.Drawing.Image img = System.Drawing.Image.FromFile(@"F:\Projects\VStudio\WallpaperManager\WallpaperManager\bin\Debug\result.png");

                Image img = (Image)bm;
                string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
                //img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);



                RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
                key.SetValue(@"WallpaperStyle", 0.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
                //SystemParametersInfo(SetDeskWallpaper, 0, defaultBackgroundFile, UpdateIniFile | SendWinIniChange);
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
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void wallpaper2_MouseDown(object sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            isDragging = true;
            move = e.Location;
        }

        private void wallpaper2_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging == true)
            {
                Control c = sender as Control;

                this.wallpaper2.Left += e.X - move.X;
                this.wallpaper2.Top += e.Y - move.Y;

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

        private void wallpaper2_MouseUp(object sender, MouseEventArgs e)
        {
            isDragging = false;
        }

     

    }
}
