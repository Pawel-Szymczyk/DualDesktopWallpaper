using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace WallpaperManager
{
    public class DisplayManager
    {
        /// <summary>
        /// Represents real screen scale.
        /// </summary>
        private readonly int scale = 21;

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
        private PictureBox CreateDisplay(Screen screen, int width, int height, string text, Panel panel, Button searchBtn, Button applyBtn, Button cancelBtn)
        {
            var pictureBox = new PictureBox
            {
                Name = screen.DeviceName.Replace(@"\.", "").Replace(@"\", "").ToLower(),
                Size = new Size(width, height),
                BackColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle
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
        public List<PictureBox> DrawDisplays(bool drawSingleDiplay, int parentContainerMiddleWidth, int parentContainerMiddleHeight, Panel panel, Button searchBtn, Button applyBtn, Button cancelBtn)
        {
            var pictureBoxes = new List<PictureBox>();

            if (drawSingleDiplay)
            {
                // Draw Single Display

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
                int scaledScreenHeight = screen.Bounds.Size.Height / this.scale;
                int scaledScreenWidth = (screen.Bounds.Size.Width / this.scale);

                // center of actual screen
                int displayCenterX = scaledScreenWidth / 2;
                int displayCenterY = scaledScreenHeight / 2;

                string text = "1 | 2";
                PictureBox display = this.CreateDisplay(screen, scaledScreenWidth, scaledScreenHeight, text, panel, searchBtn, applyBtn, cancelBtn);

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
                    int scaledScreenHeight = screen.Bounds.Size.Height / this.scale;
                    int scaledScreenWidth = (screen.Bounds.Size.Width / this.scale);

                    // center of actual screen
                    int displayCenterX = scaledScreenWidth / 2;
                    int displayCenterY = scaledScreenHeight / 2;

                    string text = screen.DeviceName.Replace(@"\.", "").Replace(@"\", "").ToLower().Replace(@"display", "");
                    PictureBox display = this.CreateDisplay(screen, scaledScreenWidth, scaledScreenHeight, text, panel, searchBtn, applyBtn, cancelBtn);


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

                            x = realDisplayOneShiftLocation.X - (screen.Bounds.Width / this.scale) - this.margin;
                            y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.scale);
                        }
                        else if (screen.Bounds.X >= 0 && screen.Bounds.Y < 0)
                        {
                            // top

                            y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.scale) - this.margin;
                            x = realDisplayOneShiftLocation.X + (screen.Bounds.X / this.scale);

                        }
                        else if (screen.Bounds.X >= 0 && screen.Bounds.Y >= Screen.AllScreens[0].Bounds.Height)
                        {
                            // bottom

                            y = realDisplayOneShiftLocation.Y + (Screen.AllScreens[0].Bounds.Height / this.scale) + this.margin;
                            x = realDisplayOneShiftLocation.X + (screen.Bounds.X / this.scale);
                        }
                        else
                        {
                            // right 

                            x = realDisplayOneShiftLocation.X + (Screen.AllScreens[0].Bounds.Width / this.scale) + this.margin;
                            y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.scale);
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
