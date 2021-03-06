﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WallpaperManager
{
    public static class DisplayIdentity
    {
        public static async Task<Form> DisplayIdentityBox(Screen screen, int monitor)
        {

            // ----------FORM--------------
            var identityBox = new Form
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
            var identityLabel = new Label()
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            identityLabel.Font = new Font("Serif", 60, FontStyle.Bold);
            identityLabel.Text = monitor.ToString();
            identityLabel.ForeColor = Color.White;

            identityBox.Controls.Add(identityLabel);

            identityBox.Show();

            return identityBox;
        }


        public static async Task DetectIdentity(Button button)
        {
            button.Enabled = false;

            int monitor = 0;
            var formList = new List<Form>();
            foreach (Screen screen in Screen.AllScreens)
            {
                monitor++;
                Form form = await DisplayIdentity.DisplayIdentityBox(screen, monitor);
                formList.Add(form);
            }

            // wait 3 seconds before close forms.
            await Task.Delay(2000);

            foreach (Form form in formList)
            {
                await Task.Delay(0);
                form.Close();
            }

            button.Enabled = true;
        }


    }
}
