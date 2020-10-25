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

        //const int SetDeskWallpaper = 20;
        //const int UpdateIniFile = 0x01;
        //const int SendWinIniChange = 0x02;
        //[DllImport("user32.dll", CharSet = CharSet.Auto)]
        //static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);




        //Point primaryMonitorPoint = new Point(0, 0);
        const string defaultBackgroundFile = @"C:\ProgramData\Pics\x.jpg";
        //Dictionary<string, Image> images = new Dictionary<string, Image>();




        //List<string> screenos = new List<string>();
        //int index = 0;

        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        public enum Style : int
        {
            Tiled,
            Centered,
            Stretched
        }

        //public static void Set()
        //{

        //    //using (var virtualScreenBitmap = new Bitmap((int)System.Windows.Forms.SystemInformation.VirtualScreen.Width, (int)System.Windows.Forms.SystemInformation.VirtualScreen.Height))
        //    //{
        //    //    using (var virtualScreenGraphic = Graphics.FromImage(virtualScreenBitmap))
        //    //    {

        //    //        //System.IO.Stream s = new System.Net.WebClient().OpenRead(uri.ToString());

        //    //        //System.Drawing.Image img = System.Drawing.Image.FromStream(s);
        //    //        //string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
        //    //        //img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);




        //    //        //gets the image which we want to place in virtualScreenGraphic
        //    //        //var image2 = images["all"];

        //    //        //sets the position and size where the images will go
        //    //        Rectangle rectangle2 = new Rectangle(0, 0, virtualScreenBitmap.Width, virtualScreenBitmap.Height);

        //    //        // fill with the desired image... centered                            
        //    //        //if (image2 != null)
        //    //        //    DrawImageCentered(virtualScreenGraphic, image2, rectangle2);

        //    //        //draws the picture at the right place in virtualScreenGraphic
        //    //        virtualScreenGraphic.DrawImage(virtualScreenBitmap, rectangle2);

        //    //        string fileName = Path.GetFileNameWithoutExtension(defaultBackgroundFile);
        //    //        virtualScreenBitmap.Save(fileName, ImageFormat.Jpeg);




        //    //    }
        //    //}

        //    //RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
        //    //if (style == Style.Stretched)
        //    //{
        //    //    key.SetValue(@"WallpaperStyle", 2.ToString());
        //    //    key.SetValue(@"TileWallpaper", 0.ToString());
        //    //}

        //    //if (style == Style.Tiled)
        //    //{
        //    //    key.SetValue(@"WallpaperStyle", 0.ToString());
        //    //    key.SetValue(@"TileWallpaper", 1.ToString());
        //    //}
        //    //if (style == Style.Centered)
        //    //{
        //    //    key.SetValue(@"WallpaperStyle", 0.ToString());
        //    //    key.SetValue(@"TileWallpaper", 0.ToString());
        //    //}


        //    System.Drawing.Image img = System.Drawing.Image.FromFile(defaultBackgroundFile);
        //    string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");
        //    img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);



        //    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
        //    key.SetValue(@"WallpaperStyle", 0.ToString());
        //    key.SetValue(@"TileWallpaper", 1.ToString());
        //    //SystemParametersInfo(SetDeskWallpaper, 0, defaultBackgroundFile, UpdateIniFile | SendWinIniChange);
        //    SystemParametersInfo(SPI_SETDESKWALLPAPER,0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);

        //}




        //        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        //        private static extern IntPtr CreateRoundRectRgn
        //(
        //    int nLeftRect, // x-coordinate of upper-left corner
        //    int nTopRect, // y-coordinate of upper-left corner
        //    int nRightRect, // x-coordinate of lower-right corner
        //    int nBottomRect, // y-coordinate of lower-right corner
        //    int nWidthEllipse, // height of ellipse
        //    int nHeightEllipse // width of ellipse
        // );

        //        [DllImport("dwmapi.dll")]
        //        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);

        //        [DllImport("dwmapi.dll")]
        //        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        //        [DllImport("dwmapi.dll")]
        //        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);

        //        private bool m_aeroEnabled;                     // variables for box shadow
        //        private const int CS_DROPSHADOW = 0x00020000;
        //        private const int WM_NCPAINT = 0x0085;
        //        private const int WM_ACTIVATEAPP = 0x001C;

        //        public struct MARGINS                           // struct for box shadow
        //        {
        //            public int leftWidth;
        //            public int rightWidth;
        //            public int topHeight;
        //            public int bottomHeight;
        //        }

        //        private const int WM_NCHITTEST = 0x84;          // variables for dragging the form
        //        private const int HTCLIENT = 0x1;
        //        private const int HTCAPTION = 0x2;

        //        protected override CreateParams CreateParams
        //        {
        //            get
        //            {
        //                m_aeroEnabled = CheckAeroEnabled();

        //                CreateParams cp = base.CreateParams;
        //                if (!m_aeroEnabled)
        //                    cp.ClassStyle |= CS_DROPSHADOW;

        //                return cp;
        //            }
        //        }

        //        private bool CheckAeroEnabled()
        //        {
        //            if (Environment.OSVersion.Version.Major >= 6)
        //            {
        //                int enabled = 0;
        //                DwmIsCompositionEnabled(ref enabled);
        //                return (enabled == 1) ? true : false;
        //            }
        //            return false;
        //        }

        //        protected override void WndProc(ref Message m)
        //        {
        //            switch (m.Msg)
        //            {
        //                case WM_NCPAINT:                        // box shadow
        //                    if (m_aeroEnabled)
        //                    {
        //                        var v = 2;
        //                        DwmSetWindowAttribute(this.Handle, 2, ref v, 4);
        //                        MARGINS margins = new MARGINS()
        //                        {
        //                            bottomHeight = 1,
        //                            leftWidth = 1,
        //                            rightWidth = 1,
        //                            topHeight = 1
        //                        };
        //                        DwmExtendFrameIntoClientArea(this.Handle, ref margins);

        //                    }
        //                    break;
        //                default:
        //                    break;
        //            }
        //            base.WndProc(ref m);

        //            if (m.Msg == WM_NCHITTEST && (int)m.Result == HTCLIENT)     // drag the form
        //                m.Result = (IntPtr)HTCAPTION;

        //        }




        //private const int CS_DropShadow = 0x00020000;
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ClassStyle |= CS_DropShadow;
        //        return cp;
        //    }
        //}


        public Form1()
        {
            //m_aeroEnabled = true;

            InitializeComponent();

            new DropShadow().ApplyShadows(this);

            ////figure out where the main monitor is in relation to the virtualScreenBitmap
            //foreach (Screen scr in Screen.AllScreens)
            //{
            //    images.Add(scr.DeviceName, null);
            //    screenos.Add(scr.DeviceName);
            //    if (scr.Bounds.Left < primaryMonitorPoint.X)
            //        primaryMonitorPoint.X = scr.Bounds.Left;
            //    if (scr.Bounds.Top < primaryMonitorPoint.Y)
            //        primaryMonitorPoint.Y = scr.Bounds.Top;
            //}
            //primaryMonitorPoint.X *= -1;
            //primaryMonitorPoint.Y *= -1;

            ////Image for multiple screens
            //images.Add("all", null);

            ////set Images in Dictionary in case there are previous Images
            //if (File.Exists(defaultBackgroundFile))
            //{
            //    using (var old = new Bitmap(defaultBackgroundFile))
            //    {
            //        foreach (Screen scr in Screen.AllScreens)
            //        {
            //            Rectangle rectangle = new Rectangle(primaryMonitorPoint.X + scr.Bounds.Left, primaryMonitorPoint.Y + scr.Bounds.Top, scr.Bounds.Width, scr.Bounds.Height);
            //            if (old.Width >= (rectangle.X + rectangle.Width) &&
            //                old.Height >= (rectangle.Y + rectangle.Height))
            //                images[scr.DeviceName] = (Bitmap)old.Clone(rectangle, old.PixelFormat);
            //        }
            //    }
            //}


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
            await Task.Delay(3000);

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








        //public void setAlternatingWalls(string file)
        //{
        //    images[screenos[index]] = Image.FromFile(file);
        //    index++;
        //    if (index == screenos.Count)
        //        index = 0;

        //    CreateBackgroundImage(Method.multiple);
        //    GC.Collect();
        //}

        ////public void setWallforScreen(Screen screen, string file)
        ////{
        ////    images[screen.DeviceName] = Image.FromFile(file);
        ////    CreateBackgroundImage(Method.multiple);
        ////    GC.Collect();
        ////}
        //public void setWallforScreen(string screenName, string file)
        //{
        //    images[screenName] = Image.FromFile(file);
        //    CreateBackgroundImage(Method.multiple);
        //    GC.Collect();
        //}

        //public void setWallforAllScreen(string file)
        //{
        //    images["all"] = Image.FromFile(file);
        //    CreateBackgroundImage(Method.single);
        //    GC.Collect();
        //}


        //private enum Method
        //{
        //    multiple,
        //    single
        //}
        //private void CreateBackgroundImage(Method method)
        //{

        //    using (var virtualScreenBitmap = new Bitmap((int)System.Windows.Forms.SystemInformation.VirtualScreen.Width, (int)System.Windows.Forms.SystemInformation.VirtualScreen.Height))
        //    {
        //        using (var virtualScreenGraphic = Graphics.FromImage(virtualScreenBitmap))
        //        {

        //            switch (method)
        //            {
        //                // alternated Screen Images
        //                case Method.multiple:
        //                    foreach (var screen in System.Windows.Forms.Screen.AllScreens)
        //                    {
        //                        // gets the image which we want to place in virtualScreenGraphic
        //                        var image = (images.ContainsKey(screen.DeviceName)) ? images[screen.DeviceName] : null;

        //                        //sets the position and size where the images will go
        //                        Rectangle rectangle = new Rectangle(primaryMonitorPoint.X + screen.Bounds.Left, primaryMonitorPoint.Y + screen.Bounds.Top, screen.Bounds.Width, screen.Bounds.Height);

        //                        // produce a image for the screen and fill it with the desired image... centered
        //                        var monitorBitmap = new Bitmap(rectangle.Width, rectangle.Height);
        //                        if (image != null)
        //                            DrawImageCentered(Graphics.FromImage(monitorBitmap), image, rectangle);

        //                        //draws the picture at the right place in virtualScreenGraphic
        //                        virtualScreenGraphic.DrawImage(monitorBitmap, rectangle);
        //                    }
        //                    break;

        //                //Single screen Image
        //                case Method.single:
        //                    // gets the image which we want to place in virtualScreenGraphic
        //                    var image2 = images["all"];

        //                    //sets the position and size where the images will go
        //                    Rectangle rectangle2 = new Rectangle(0, 0, virtualScreenBitmap.Width, virtualScreenBitmap.Height);

        //                    // fill with the desired image... centered                            
        //                    if (image2 != null)
        //                        DrawImageCentered(virtualScreenGraphic, image2, rectangle2);

        //                    //draws the picture at the right place in virtualScreenGraphic
        //                    virtualScreenGraphic.DrawImage(virtualScreenBitmap, rectangle2);
        //                    break;
        //            }

        //            string fileName = Path.GetFileNameWithoutExtension(defaultBackgroundFile);
        //            virtualScreenBitmap.Save(fileName, ImageFormat.Jpeg);
        //        }
        //    }

        //    RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
        //    key.SetValue(@"WallpaperStyle", 0.ToString());
        //    key.SetValue(@"TileWallpaper", 1.ToString());
        //    SystemParametersInfo(SetDeskWallpaper, 0, defaultBackgroundFile, UpdateIniFile | SendWinIniChange);
        //}


        //private void DrawImageCentered(Graphics g, Image img, Rectangle monitorRect)
        //{
        //    double ratiodev = (1.0 * monitorRect.Width / monitorRect.Height) - (1.0 * img.Width / img.Height);
        //    if (((1.0 * monitorRect.Width / monitorRect.Height > 1) && ratiodev > -0.25 && ratiodev < 0.25))
        //    {
        //        img = getsnappedIMG(img, monitorRect);
        //    }


        //    float heightRatio = (float)monitorRect.Height / (float)img.Height;
        //    float widthRatio = (float)monitorRect.Width / (float)img.Width;
        //    int height = monitorRect.Height;
        //    int width = monitorRect.Width;
        //    int x = 0;
        //    int y = 0;

        //    if (heightRatio < widthRatio)
        //    {
        //        width = (int)((float)img.Width * heightRatio);
        //        height = (int)((float)img.Height * heightRatio);
        //        x = (int)((float)(monitorRect.Width - width) / 2f);
        //    }
        //    else
        //    {
        //        width = (int)((float)img.Width * widthRatio);
        //        height = (int)((float)img.Height * widthRatio);
        //        y = (int)((float)(monitorRect.Height - height) / 2f);
        //    }
        //    Rectangle rect = new Rectangle(x, y, width, height);
        //    g.DrawImage(img, rect);
        //}

        //private Image getsnappedIMG(Image img, Rectangle monitorRect)
        //{
        //    double ratiodev = (1.0 * monitorRect.Width / monitorRect.Height) - (1.0 * img.Width / img.Height);
        //    int height = img.Height;
        //    int width = img.Width;

        //    Rectangle rect;
        //    if (ratiodev < 0)
        //    {
        //        rect = new Rectangle(0, 0, (int)((1.0 * monitorRect.Width / monitorRect.Height) * height), height);
        //        rect.X = (width - rect.Width) / 2;
        //    }
        //    else
        //    {
        //        rect = new Rectangle(0, 0, width, (int)(1.0 * width / (1.0 * monitorRect.Width / monitorRect.Height)));
        //        rect.Y = (height - rect.Height) / 2;
        //    }


        //    var img2 = (Bitmap)img;
        //    return (Bitmap)img2.Clone(rect, img.PixelFormat);

        //}



        string wallpaperFileName1 = string.Empty;
        string wallpaperFileName2 = string.Empty;


        /// <summary>
        /// Pick image for 1st monitor.
        /// </summary>
        private void wallpaper1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                wallpaperFileName1 = dialog.FileName;

                Image img = Image.FromFile(wallpaperFileName1);
                this.wallpaper1.Image = img;
                this.wallpaper1.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        /// <summary>
        /// Pick image for 2nd monitor.
        /// </summary>
        private void wallpaper2_Click(object sender, EventArgs e)
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






        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    e.Graphics.DrawRectangle(Pens.Black, this.Bounds);
        //}
    }
}
