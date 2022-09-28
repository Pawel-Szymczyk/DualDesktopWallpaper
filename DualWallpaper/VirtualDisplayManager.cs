using DualWallpaper.Enums;
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
        //private int scale = 14;

        //private int Scale
        //{
        //    get
        //    {
        //        // this is not fully working scale, this solution is not working in call case scenarios 
        //        // but for main scenarion this solution is good enough
        //        // TODO: dynamic scale (future)

        //        //if ((Screen.AllScreens[1].Bounds.X >= 0 && Screen.AllScreens[1].Bounds.Y < 0)
        //        //|| (Screen.AllScreens[1].Bounds.X >= 0 && Screen.AllScreens[1].Bounds.Y >= Screen.AllScreens[0].Bounds.Height))
        //        //{
        //        //    // top or bottom
        //        //    return 22;
        //        //}

        //        //return this.scale;

        //        return 15;

        //    }
        //    set => this.scale = value;
        //}

        private int Scale { get; set; }
        private Panel Panel { get; set; }
        private Button SearchBtn { get; set; }
        private Button ConfirmBtn { get; set; }
        private Button CancelBtn { get; set; }

        /// <summary>
        /// Space between drawn displays.
        /// </summary>
        private readonly int margin = 1;



        /// <summary>
        /// Default position of the 2md virtual display.
        /// </summary>
        public VirtualDisplayLayout SecondaryVirtualDisplayLayout
        {
            get
            {
                // assumming that primary screen will be always at the front of the user, starting from point(0,0)
                var primaryDisplay = Screen.PrimaryScreen;
                var secondaryDisplay = Screen.AllScreens.Where(x => x != primaryDisplay).FirstOrDefault();


                if(secondaryDisplay.Bounds.Left >= primaryDisplay.Bounds.Right)
                {
                    return VirtualDisplayLayout.Right;
                }
                else if(secondaryDisplay.Bounds.Right <= primaryDisplay.Bounds.Left)
                {
                    return VirtualDisplayLayout.Left;
                }
                else if(secondaryDisplay.Bounds.Bottom <= primaryDisplay.Bounds.Top)
                {
                    return VirtualDisplayLayout.Top;
                }
                else
                {
                    return VirtualDisplayLayout.Bottom;
                }
            }

        }

        public VirtualDisplayManager() 
        { 
            this.Scale = 1;
            this.Panel = new Panel();
            this.SearchBtn = new Button();
            this.ConfirmBtn = new Button();
            this.CancelBtn = new Button();
        }

        public VirtualDisplayManager(Panel panel, Button searchBtn, Button applyBtn, Button cancelBtn)
        {
            this.Scale = 17;
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


            this.SetTotalWidthAndHeightOfVirtualDisplays(SecondaryVirtualDisplayLayout);



            // Draw Single Display -- Fuck, this logic should be for merged image (of 2 monitors, common picture)

            // here the scale should be left default...

            //Screen screen = null;
            //string resolution = string.Empty;
            //if (Screen.AllScreens[0].Primary)
            //{
            //    screen = Screen.AllScreens[0];
            //    // this will be wrong but for now... it requires to know exavct screens positions to each other...
            //    int width = Screen.AllScreens[0].Bounds.Width + Screen.AllScreens[1].Bounds.Width;
            //    int height = Screen.AllScreens[1].Bounds.Height;
            //    resolution = $"{width} x {height}";

            //}
            //else
            //{
            //    screen = Screen.AllScreens[1];
            //}

            //// actual screen scaled size
            //int scaledScreenHeight = screen.Bounds.Size.Height / this.Scale;
            //int scaledScreenWidth = (screen.Bounds.Size.Width / this.Scale);

            //// center of actual screen
            //int displayCenterX = scaledScreenWidth / 2;
            //int displayCenterY = scaledScreenHeight / 2;

            //string text = "1 | 2";
            //string deviceName = "mergedDisplay";






            //foreach (Screen screen in Screen.AllScreens.OrderByDescending(x => x.Primary == true))
            //{
            //    if (screen.Primary)
            //    {
            //        // set position for main screen first...
            //        //realDisplayOneShiftLocation = new Point(centerPointX - (display.Width / 2), centerPointY - (display.Height / 2));

            //        //display.Location = realDisplayOneShiftLocation;

            //        width += screen.Bounds.Width;
            //    }
            //    else
            //    {
            //        //int x = 0;
            //        //int y = 0;

            //        if (screen.Bounds.X < 0)
            //        {
            //            // left
            //            //x = realDisplayOneShiftLocation.X - (screen.Bounds.Width / this.Scale) - this.margin;
            //            //y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale);

            //            width += screen.Bounds.Width;
            //        }
            //        else if (screen.Bounds.X >= 0 && screen.Bounds.Y < 0)
            //        {
            //            // top
            //            //x = realDisplayOneShiftLocation.X + (screen.Bounds.X / this.Scale);
            //            //y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale) - this.margin;

            //            width += screen.Bounds.Width;
            //        }
            //        else if (screen.Bounds.X >= 0 && screen.Bounds.Y >= Screen.AllScreens[0].Bounds.Height)
            //        {
            //            // bottom
            //            //x = realDisplayOneShiftLocation.X + (screen.Bounds.X / this.Scale);
            //            //y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale) + this.margin;
            //        }
            //        else
            //        {
            //            // right 
            //            //x = realDisplayOneShiftLocation.X + (screen.Bounds.X / this.Scale);
            //            //y = realDisplayOneShiftLocation.Y + (screen.Bounds.Y / this.Scale);


            //        }

            //        //display.Location = new Point(x, y);
            //    }
            //}



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



        public int TotalHeight { get; private set; }
        public int TotalWidth { get; private set; }


        private void SetTotalWidthAndHeightOfVirtualDisplays(VirtualDisplayLayout secondaryVirtualDisplayLayout)
        {
            int totalWidth = 0;
            int totalHeight = 0;
            
            // should be global?
            var primaryDisplay = Screen.PrimaryScreen;
            var secondaryDisplay = Screen.AllScreens.Where(x => x != primaryDisplay).FirstOrDefault();


            if (secondaryVirtualDisplayLayout.Equals(VirtualDisplayLayout.Left) 
                || secondaryVirtualDisplayLayout.Equals(VirtualDisplayLayout.Right))
            {
                totalWidth = Screen.AllScreens.Sum(x => x.Bounds.Width);

                // case 1 secondary display (y) is smaller or equal the primary display (y)
                // and its bottom is equal or greater than the primary screen
                if(secondaryDisplay.Bounds.Y <= primaryDisplay.Bounds.Y 
                    && secondaryDisplay.Bounds.Bottom >= primaryDisplay.Bounds.Bottom)
                {
                    totalHeight = secondaryDisplay.Bounds.Height;
                }

                // case 2 secondary display (y) is smaller or equal the primary display (y)
                // and its bottom is smaller than the primary screen 
                else if(secondaryDisplay.Bounds.Y <= primaryDisplay.Bounds.Y
                    && secondaryDisplay.Bounds.Bottom < primaryDisplay.Bounds.Bottom)
                {
                    totalHeight = secondaryDisplay.Bounds.Height + (primaryDisplay.Bounds.Bottom - secondaryDisplay.Bounds.Bottom);
                }

                // case 3 secondary display (y) is greater than the priamry display (y)
                // and its bottom is equal or greater than the primary screen
                else if(secondaryDisplay.Bounds.Y > primaryDisplay.Bounds.Y
                    && secondaryDisplay.Bounds.Bottom >= primaryDisplay.Bounds.Bottom)
                {
                    totalHeight = secondaryDisplay.Bounds.Height + (primaryDisplay.Bounds.Y + secondaryDisplay.Bounds.Y);
                }

                // case 4 secondary display (y) is greater than the priamry display (y)
                // and its bottom is smaller than the primary screen
                else if(secondaryDisplay.Bounds.Y > primaryDisplay.Bounds.Y
                    && secondaryDisplay.Bounds.Bottom < primaryDisplay.Bounds.Bottom)
                {
                    totalHeight = primaryDisplay.Bounds.Height;
                }

            }
            else if(secondaryVirtualDisplayLayout.Equals(VirtualDisplayLayout.Top)
                || secondaryVirtualDisplayLayout.Equals(VirtualDisplayLayout.Bottom))
            {
                totalHeight = Screen.AllScreens.Sum(x => x.Bounds.Height);

                // case 1 secondary display (x) is smaller equal the primary display (x)
                // and its right is equal or greater than the primary screen 
                if(secondaryDisplay.Bounds.X <= primaryDisplay.Bounds.X 
                    && secondaryDisplay.Bounds.Right >= primaryDisplay.Bounds.Right)
                {
                    totalWidth = secondaryDisplay.Bounds.Width;
                }

                // case 2 secondary display (x) is smaller equal the primary display (x)
                // and its right is smaller than the primary screen
                else if(secondaryDisplay.Bounds.X <= primaryDisplay.Bounds.X
                    && secondaryDisplay.Bounds.Right < primaryDisplay.Bounds.Right)
                {
                    totalWidth = secondaryDisplay.Bounds.Width + (primaryDisplay.Bounds.Width - secondaryDisplay.Bounds.Right);
                }

                // case 3 secondary display (x) is greater than the primary display (x)
                // and its right is equal or greater than the primary screen
                else if (secondaryDisplay.Bounds.X > primaryDisplay.Bounds.X
                    && secondaryDisplay.Bounds.Right >= primaryDisplay.Bounds.Right)
                {
                    totalWidth = secondaryDisplay.Bounds.Width + (primaryDisplay.Bounds.Width - secondaryDisplay.Bounds.X);
                }

                // case 4 secondary display (x) is greater than the primary display (x)
                // and its right is smaller than the primary screen
                else if (secondaryDisplay.Bounds.X > primaryDisplay.Bounds.X
                    && secondaryDisplay.Bounds.Right < primaryDisplay.Bounds.Right)
                {
                    totalWidth = primaryDisplay.Bounds.Width;
                }
            }
        }

    }
}


//switch (secondaryVirtualDisplayLayout)
//{
//    case VirtualDisplayLayout.Left:
//        break;

//    case VirtualDisplayLayout.Right:
//        break;

//    case VirtualDisplayLayout.Top:
//        break;

//    case VirtualDisplayLayout.Bottom:
//        break;

//    default:
//        break;
//}
