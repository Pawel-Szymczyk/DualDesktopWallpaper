namespace WallpaperManager
{
    partial class DualWallpaperApp
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DualWallpaperApp));
            this.detectScreenBtn = new System.Windows.Forms.Button();
            this.setWallpaperBtn = new System.Windows.Forms.Button();
            this.freeSpaceArea = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.resolutionsList = new System.Windows.Forms.ListBox();
            this.mergeScreenBtn = new System.Windows.Forms.CheckBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // detectScreenBtn
            // 
            this.detectScreenBtn.BackColor = System.Drawing.Color.LightGray;
            this.detectScreenBtn.FlatAppearance.BorderSize = 0;
            this.detectScreenBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.detectScreenBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detectScreenBtn.Location = new System.Drawing.Point(655, 445);
            this.detectScreenBtn.Name = "detectScreenBtn";
            this.detectScreenBtn.Size = new System.Drawing.Size(95, 32);
            this.detectScreenBtn.TabIndex = 0;
            this.detectScreenBtn.Text = "Identify";
            this.detectScreenBtn.UseVisualStyleBackColor = false;
            this.detectScreenBtn.Click += new System.EventHandler(this.detectScreenBtn_Click);
            // 
            // setWallpaperBtn
            // 
            this.setWallpaperBtn.BackColor = System.Drawing.Color.LightGray;
            this.setWallpaperBtn.FlatAppearance.BorderSize = 0;
            this.setWallpaperBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.setWallpaperBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.setWallpaperBtn.Location = new System.Drawing.Point(507, 445);
            this.setWallpaperBtn.Name = "setWallpaperBtn";
            this.setWallpaperBtn.Size = new System.Drawing.Size(141, 32);
            this.setWallpaperBtn.TabIndex = 5;
            this.setWallpaperBtn.Text = "Set Wallpaper";
            this.setWallpaperBtn.UseVisualStyleBackColor = false;
            this.setWallpaperBtn.Click += new System.EventHandler(this.setWallpaperBtn_Click);
            // 
            // freeSpaceArea
            // 
            this.freeSpaceArea.BackColor = System.Drawing.Color.WhiteSmoke;
            this.freeSpaceArea.Location = new System.Drawing.Point(29, 178);
            this.freeSpaceArea.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.freeSpaceArea.Name = "freeSpaceArea";
            this.freeSpaceArea.Size = new System.Drawing.Size(720, 260);
            this.freeSpaceArea.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("DM Sans", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(417, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(357, 38);
            this.label1.TabIndex = 8;
            this.label1.Text = "D U A L  W A L L P A P E R";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(24, 102);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(239, 25);
            this.label2.TabIndex = 9;
            this.label2.Text = "Rearrange your wallpapers";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(25, 137);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(589, 21);
            this.label3.TabIndex = 12;
            this.label3.Text = "Press and hold a wallpaper, then drag to rearrange it. Double click to pick wallp" +
    "aper.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(24, 502);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(197, 25);
            this.label8.TabIndex = 17;
            this.label8.Text = "Displays Resolutions:";
            // 
            // resolutionsList
            // 
            this.resolutionsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resolutionsList.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resolutionsList.FormattingEnabled = true;
            this.resolutionsList.ItemHeight = 21;
            this.resolutionsList.Location = new System.Drawing.Point(29, 539);
            this.resolutionsList.Name = "resolutionsList";
            this.resolutionsList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.resolutionsList.Size = new System.Drawing.Size(230, 63);
            this.resolutionsList.TabIndex = 21;
            // 
            // mergeScreenBtn
            // 
            this.mergeScreenBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.mergeScreenBtn.AutoSize = true;
            this.mergeScreenBtn.Location = new System.Drawing.Point(507, 502);
            this.mergeScreenBtn.Name = "mergeScreenBtn";
            this.mergeScreenBtn.Size = new System.Drawing.Size(163, 23);
            this.mergeScreenBtn.TabIndex = 22;
            this.mergeScreenBtn.Text = "Set Wallpaper on both screens";
            this.mergeScreenBtn.UseVisualStyleBackColor = true;
            this.mergeScreenBtn.CheckedChanged += new System.EventHandler(this.mergeScreenBtn_CheckedChanged);
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(507, 539);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(179, 23);
            this.searchBtn.TabIndex = 23;
            this.searchBtn.Text = "search for wallpaper";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Visible = false;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // DualWallpaperApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(786, 786);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.mergeScreenBtn);
            this.Controls.Add(this.resolutionsList);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.freeSpaceArea);
            this.Controls.Add(this.setWallpaperBtn);
            this.Controls.Add(this.detectScreenBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DualWallpaperApp";
            this.Load += new System.EventHandler(this.DualWallpaperApp_Load);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.DualWallpaperApp_MouseClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button detectScreenBtn;
        private System.Windows.Forms.Button setWallpaperBtn;
        private System.Windows.Forms.Panel freeSpaceArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ListBox resolutionsList;
        private System.Windows.Forms.CheckBox mergeScreenBtn;
        private System.Windows.Forms.Button searchBtn;
    }
}

