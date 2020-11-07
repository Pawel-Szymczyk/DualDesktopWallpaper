using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WallpaperManager
{
    /// <summary>
    /// Responsible for drawing displays in the application
    /// </summary>
    public class DisplayManager
    {
        /// <summary>
        /// Represents real screen scale.
        /// </summary>
        private int scale = 14;

        private int Scale
        {
            get
            {
                // this is not fully working scale, this solution is not working in call case scenarios 
                // but for main scenarion this solution is good enough
                // TODO: dynamic scale (future)

                if ((Screen.AllScreens[1].Bounds.X >= 0 && Screen.AllScreens[1].Bounds.Y < 0)
                || (Screen.AllScreens[1].Bounds.X >= 0 && Screen.AllScreens[1].Bounds.Y >= Screen.AllScreens[0].Bounds.Height))
                {
                    // top or bottom
                    return 22;
                }

                return this.scale;

            }
            set => this.scale = value;
        }

        /// <summary>
        /// Space between drawn displays.
        /// </summary>
        private readonly int margin = 1;


        /// <summary>
        /// Create PictureBox display.
        /// </summary>
        /// <param name="screen">Screen context.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <returns>PictureBox.</returns>
        private PictureBox DrawDisplay(Screen screen, Panel panel, Button searchBtn, Button applyBtn, Button cancelBtn, int width, int height, string text, string deviceName, string resolution)
        {
            var pictureBox = new PictureBox
            {
                Name = deviceName,
                Size = new Size(width, height),
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                Text = resolution
            };
            pictureBox.Paint += new PaintEventHandler((sender, e) => DisplayEventHandler.draw_Label(sender, e, text));
            pictureBox.MouseClick += new MouseEventHandler((sender, e) => DisplayEventHandler.display_SingleClick(sender, e, Screen.AllScreens, panel, searchBtn));
            pictureBox.MouseDoubleClick += new MouseEventHandler((sender, e) => DisplayEventHandler.display_DoubleClick(sender, e, panel, applyBtn, cancelBtn));

            return pictureBox;
        }




        /// <summary>
        /// Draw number of displays in the parent panel, mimic screen arrangemnt set up in the Windows display settings.
        /// </summary>
        /// <param name="drawSingleDiplay">
        ///     True if user want to draw single screen (having stretched image over two monitors).
        ///     False if user want to have all available screens (having multiple wallpapers).
        /// </param>
        /// <param name="parentContainerMiddleWidth">Middle of parent panel - width.</param>
        /// <param name="parentContainerMiddleHeight">Middle of parent panel - height.</param>
        /// <param name="panel">Parent panel.</param>
        /// <param name="searchBtn">Search button.</param>
        /// <returns>List of ready to display picture boxes.</returns>
        public List<PictureBox> ShowDisplays(bool drawSingleDiplay, int parentContainerMiddleWidth, int parentContainerMiddleHeight, Panel panel, Button searchBtn, Button applyBtn, Button cancelBtn)
        {
            var pictureBoxes = new List<PictureBox>();

            if (drawSingleDiplay)
            {
                // Draw Single Display

                // here the scale should be left default...

                Screen screen = null;
                string resolution = string.Empty;
                if (Screen.AllScreens[0].Primary)
                {
                    screen = Screen.AllScreens[0];
                    // this will be wrong but for now... it requires to know exavct screens positions to each other...
                    int width = Screen.AllScreens[0].Bounds.Width + Screen.AllScreens[1].Bounds.Width;
                    int height = Screen.AllScreens[1].Bounds.Height;
                    resolution = $"{width} x {height}";

                }
                else
                {
                    screen = Screen.AllScreens[1];
                }

                // actual screen scaled size
                int scaledScreenHeight = screen.Bounds.Size.Height / this.Scale;
                int scaledScreenWidth = (screen.Bounds.Size.Width / this.Scale);

                // center of actual screen
                int displayCenterX = scaledScreenWidth / 2;
                int displayCenterY = scaledScreenHeight / 2;

                string text = "1 | 2";
                string deviceName = "mergedDisplay";

                PictureBox display = this.DrawDisplay(screen, panel, searchBtn, applyBtn, cancelBtn, scaledScreenWidth, scaledScreenHeight, text, deviceName, resolution);

                display.Location = new Point(parentContainerMiddleWidth - displayCenterX, parentContainerMiddleHeight - displayCenterY);
                pictureBoxes.Add(display);
            }
            else
            {
                // Draw Multiple Displays

                var realDisplayOneShiftLocation = new Point();

                foreach (Screen screen in Screen.AllScreens)
                {


                    // actual screen scaled size
                    int scaledScreenHeight = screen.Bounds.Size.Height / this.Scale;
                    int scaledScreenWidth = (screen.Bounds.Size.Width / this.Scale);

                    // center of actual screen
                    int displayCenterX = scaledScreenWidth / 2;
                    int displayCenterY = scaledScreenHeight / 2;

                    string text = screen.DeviceName.Replace(@"\.", "").Replace(@"\", "").ToLower().Replace(@"display", "");
                    string deviceName = screen.DeviceName.Replace(@"\.", "").Replace(@"\", "").ToLower();

                    string resolution = $"{screen.Bounds.Width} x {screen.Bounds.Height}"; ;



                    PictureBox display = this.DrawDisplay(screen, panel, searchBtn, applyBtn, cancelBtn, scaledScreenWidth, scaledScreenHeight, text, deviceName, resolution);


                    if (screen.Primary)
                    {
                        realDisplayOneShiftLocation = new Point(parentContainerMiddleWidth - displayCenterX, parentContainerMiddleHeight - displayCenterY);

                        display.Location = realDisplayOneShiftLocation;

                    }
                    else
                    {
                        int x = 0;
                        int y = 0;
                        if (screen.Bounds.X < 0)
                        {
                            // left side
                            x = realDisplayOneShiftLocation.X - (screen.Bounds.Width / this.Scale) - this.margin;
                            y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale);
                        }
                        else if (screen.Bounds.X >= 0 && screen.Bounds.Y < 0)
                        {
                            // top
                            y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale) - this.margin;
                            x = realDisplayOneShiftLocation.X + (screen.Bounds.X / this.Scale);

                        }
                        else if (screen.Bounds.X >= 0 && screen.Bounds.Y >= Screen.AllScreens[0].Bounds.Height)
                        {
                            // bottom
                            y = realDisplayOneShiftLocation.Y + (Screen.AllScreens[0].Bounds.Height / this.Scale) + this.margin;
                            x = realDisplayOneShiftLocation.X + (screen.Bounds.X / this.Scale);
                        }
                        else
                        {
                            // right 
                            x = realDisplayOneShiftLocation.X + (Screen.AllScreens[0].Bounds.Width / this.Scale) + this.margin;
                            y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale);
                        }

                        display.Location = new Point(x, y);
                    }

                    pictureBoxes.Add(display);
                }

            }
            return pictureBoxes;
        }


    }
}
