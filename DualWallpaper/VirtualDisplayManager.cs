using DualWallpaper.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace DualWallpaper
{
    public class VirtualDisplayManager : IVirtualDisplayManager
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

                //if ((Screen.AllScreens[1].Bounds.X >= 0 && Screen.AllScreens[1].Bounds.Y < 0)
                //|| (Screen.AllScreens[1].Bounds.X >= 0 && Screen.AllScreens[1].Bounds.Y >= Screen.AllScreens[0].Bounds.Height))
                //{
                //    // top or bottom
                //    return 22;
                //}

                //return this.scale;

                return 15;

            }
            set => this.scale = value;
        }

        private Panel Panel { get; set; }
        private Button SearchBtn { get; set; }
        private Button ConfirmBtn { get; set; }
        private Button CancelBtn { get; set; }

        /// <summary>
        /// Space between drawn displays.
        /// </summary>
        private readonly int margin = 1;

        public VirtualDisplayManager(Panel panel, Button searchBtn, Button applyBtn, Button cancelBtn)
        {
            this.Panel = panel;
            this.SearchBtn = searchBtn;
            this.ConfirmBtn = applyBtn;
            this.CancelBtn = cancelBtn;
        }

        /// <summary>
        /// Returns single virtual display.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public PictureBox Show(int parentContainerMiddleWidth, int parentContainerMiddleHeight)
        {

            // TODO: if this is merged then it should show still two screens together in different positions, only the image is stretched over two displays
            // otherwise show only single monitor picked by user(?) and allow modify background only for selected display...






            // Draw Single Display -- Fuck, this logic should be for merged image (of 2 monitors, common picture)

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


            // ---------------------------------------------------
            // create virtual display
            //IVirtualDisplay virtualDisplay = new VirtualDisplay(scaledScreenWidth, scaledScreenHeight, deviceName, resolution);
            //PictureBox display = virtualDisplay.Draw();

            //// ---------------------------------------------------
            //// add additional features to virtual display
            //virtualDisplay.AddLabel(display, text);
            //virtualDisplay.AddSingleClick(display, this.Panel, this.SearchBtn);
            //virtualDisplay.AddDoubleClick(display, this.Panel, this.ConfirmBtn, this.CancelBtn);

            //// ---------------------------------------------------
            //// set virtual display location
            //int x = parentContainerMiddleWidth - displayCenterX;
            //int y = parentContainerMiddleHeight - displayCenterY;
            //display.Location = new Point(x, y);

            //return display;

            return null;
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


        /// <summary>
        /// Returns all virtual displays.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<PictureBox> ShowAll(int centerPointX, int centerPointY)
        {
            var pictureBoxes = new List<PictureBox>();
            var realDisplayOneShiftLocation = new Point();

            foreach (Screen screen in Screen.AllScreens.OrderByDescending(x => x.Primary == true))
            {

                // ---------------------------------------------------
                // create virtual display
                IVirtualDisplay virtualDisplay = new VirtualDisplay(screen, this.Scale);
                PictureBox display = virtualDisplay.Draw();

                // ---------------------------------------------------
                // add additional features to virtual display
                virtualDisplay.AddLabel(display);
                virtualDisplay.AddSingleClick(display, this.Panel, this.SearchBtn);
                virtualDisplay.AddDoubleClick(display, this.Panel, this.ConfirmBtn, this.CancelBtn);

               

                // --------------------------------------------------
                // Position virtual display on panel...
                if (screen.Primary)
                {
                    // set position for main screen first...
                    realDisplayOneShiftLocation = new Point(centerPointX - (display.Width / 2), centerPointY - (display.Height / 2));

                    display.Location = realDisplayOneShiftLocation;
                }
                else
                {
                    int x = 0;
                    int y = 0;

                    if (screen.Bounds.X < 0)
                    {
                        // left
                        x = realDisplayOneShiftLocation.X - (screen.Bounds.Width / this.Scale) - this.margin;
                        y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale);
                    }
                    else if (screen.Bounds.X >= 0 && screen.Bounds.Y < 0)
                    {
                        // top
                        x = realDisplayOneShiftLocation.X + (screen.Bounds.X / this.Scale);
                        y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale) - this.margin;
                    }
                    else if (screen.Bounds.X >= 0 && screen.Bounds.Y >= Screen.AllScreens[0].Bounds.Height)
                    {
                        // bottom
                        x = realDisplayOneShiftLocation.X + (screen.Bounds.X / this.Scale);
                        y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale) + this.margin;
                    }
                    else
                    {
                        // right 
                        x = realDisplayOneShiftLocation.X + (screen.Bounds.X / this.Scale);
                        y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale);
                    }

                    display.Location = new Point(x, y);
                }

                pictureBoxes.Add(display);
            }

            return pictureBoxes;
        }


    }
}
