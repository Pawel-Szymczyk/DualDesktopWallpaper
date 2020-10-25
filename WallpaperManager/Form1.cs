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


        public Form1()
        {
            InitializeComponent();



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

        void timer_Tick(object sender, EventArgs e)
        {
            //formCloseTimer.Stop();
            //formCloseTimer.Tick -= new EventHandler(timer_Tick);
            //this.Close();


            //int monitor = 0;

            //List<Form> forms = new List<Form>();


            //// For each screen, add the screen properties to a list box.
            //foreach (var screen in System.Windows.Forms.Screen.AllScreens)
            //{
            //    //listBox1.Items.Add("Device Name: " + screen.DeviceName);
            //    //listBox1.Items.Add("Bounds: " +
            //    //    screen.Bounds.ToString());
            //    //listBox1.Items.Add("Type: " +
            //    //    screen.GetType().ToString());
            //    //listBox1.Items.Add("Working Area: " +
            //    //    screen.WorkingArea.ToString());
            //    //listBox1.Items.Add("Primary Screen: " +
            //    //    screen.Primary.ToString());
            //    monitor++;


            //    Form form = new Form();
            //    form.BackColor = Color.FromArgb(0, 102, 204);
            //    form.FormBorderStyle = FormBorderStyle.None;
            //    form.Text = $"form {monitor}";
            //    form.StartPosition = FormStartPosition.Manual;


            //    form.Bounds = new Rectangle(
            //        screen.Bounds.X + 100, screen.Bounds.Y + 100, 
            //        screen.Bounds.Width, screen.Bounds.Height);
            //    form.Size = new Size(300, 300);
            //    form.Padding = new Padding(100);
            //    form.TopMost = true;

            //    forms.Add(form);
            //    form.Show();


            //    Label label = new Label()
            //    {
            //        AutoSize = false,
            //        TextAlign = ContentAlignment.MiddleCenter,
            //        Dock = DockStyle.Fill
            //    };
            //    label.Font = new Font("Serif", 60, FontStyle.Bold);
            //    label.Text = monitor.ToString();
            //    label.ForeColor = Color.White;


            //    form.Controls.Add(label);


            //}


            //formCloseTimer.Stop();
            ////formCloseTimer.Tick -= new EventHandler(timer_Tick);
            //foreach(var form in forms)
            //{
            //    form.Close();
            //}
        }


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



        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                wallpaperFileName1 = dialog.FileName;

                Image img = Image.FromFile(wallpaperFileName1);
                this.pictureBox1.Image = img;
                this.pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                wallpaperFileName2 = dialog.FileName;

                Image img = Image.FromFile(wallpaperFileName2);
                this.pictureBox2.Image = img;
                this.pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            }
        }


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


    }
}
