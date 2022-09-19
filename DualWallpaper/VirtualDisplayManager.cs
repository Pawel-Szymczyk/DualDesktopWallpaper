using DualWallpaper.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private Panel Panel { get; set; }
        private Button SearchBtn { get; set; }
        private Button ConfirmBtn { get; set; }
        private Button CancelBtn { get; set; }

        //private IVirtualDisplay VirtualDisplay;

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


            // ---------------------------------------------------
            // create virtual display
            IVirtualDisplay virtualDisplay = new VirtualDisplay(scaledScreenWidth, scaledScreenHeight, deviceName, resolution);
            PictureBox display = virtualDisplay.Draw();

            // ---------------------------------------------------
            // add additional features to virtual display
            virtualDisplay.AddLabel(display, text);
            virtualDisplay.AddSingleClick(display, this.Panel, this.SearchBtn);
            virtualDisplay.AddDoubleClick(display, this.Panel, this.ConfirmBtn, this.CancelBtn);

            // ---------------------------------------------------
            // set virtual display location
            int x = parentContainerMiddleWidth - displayCenterX;
            int y = parentContainerMiddleHeight - displayCenterY;
            display.Location = new Point(x, y);

            return display;
        }

        /// <summary>
        /// Returns all virtual displays.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public List<PictureBox> ShowAll()
        {
            throw new NotImplementedException();
        }
    }
}
