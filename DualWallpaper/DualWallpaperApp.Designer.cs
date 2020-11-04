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
            this.applyBtn = new System.Windows.Forms.Button();
            this.freeSpaceArea = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.resolutionsList = new System.Windows.Forms.ListBox();
            this.mergeBtn = new System.Windows.Forms.CheckBox();
            this.searchBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.versionLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // detectScreenBtn
            // 
            this.detectScreenBtn.BackColor = System.Drawing.Color.LightGray;
            this.detectScreenBtn.FlatAppearance.BorderSize = 0;
            this.detectScreenBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.detectScreenBtn.Font = new System.Drawing.Font("DM Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detectScreenBtn.Location = new System.Drawing.Point(637, 445);
            this.detectScreenBtn.Name = "detectScreenBtn";
            this.detectScreenBtn.Size = new System.Drawing.Size(113, 32);
            this.detectScreenBtn.TabIndex = 0;
            this.detectScreenBtn.Text = "I d e n t i t y";
            this.detectScreenBtn.UseVisualStyleBackColor = false;
            this.detectScreenBtn.Click += new System.EventHandler(this.detectScreenBtn_Click);
            // 
            // applyBtn
            // 
            this.applyBtn.BackColor = System.Drawing.Color.LightGray;
            this.applyBtn.FlatAppearance.BorderSize = 0;
            this.applyBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.applyBtn.Font = new System.Drawing.Font("DM Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.applyBtn.Location = new System.Drawing.Point(29, 445);
            this.applyBtn.Name = "applyBtn";
            this.applyBtn.Size = new System.Drawing.Size(112, 32);
            this.applyBtn.TabIndex = 5;
            this.applyBtn.Text = "A p p l y";
            this.applyBtn.UseVisualStyleBackColor = false;
            this.applyBtn.Visible = false;
            this.applyBtn.Click += new System.EventHandler(this.applyBtn_Click);
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
            this.label2.Font = new System.Drawing.Font("DM Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(24, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(305, 28);
            this.label2.TabIndex = 9;
            this.label2.Text = "S e t  Y o u r  W a l l p a p e r s";
            // 
            // label3
            // 
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("DM Sans", 9.749999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Location = new System.Drawing.Point(26, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(589, 40);
            this.label3.TabIndex = 12;
            this.label3.Text = "Single click to get know selected screen resolution and search wallpaper in your " +
    "default browser. Double click to pick your wallpaper for single screen.";
            // 
            // resolutionsList
            // 
            this.resolutionsList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.resolutionsList.Font = new System.Drawing.Font("DM Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.resolutionsList.FormattingEnabled = true;
            this.resolutionsList.ItemHeight = 21;
            this.resolutionsList.Location = new System.Drawing.Point(29, 550);
            this.resolutionsList.Name = "resolutionsList";
            this.resolutionsList.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.resolutionsList.Size = new System.Drawing.Size(300, 189);
            this.resolutionsList.TabIndex = 21;
            // 
            // mergeBtn
            // 
            this.mergeBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mergeBtn.Appearance = System.Windows.Forms.Appearance.Button;
            this.mergeBtn.BackColor = System.Drawing.Color.LightGray;
            this.mergeBtn.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mergeBtn.FlatAppearance.BorderSize = 0;
            this.mergeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mergeBtn.Font = new System.Drawing.Font("DM Sans", 12F);
            this.mergeBtn.Location = new System.Drawing.Point(512, 445);
            this.mergeBtn.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.mergeBtn.Name = "mergeBtn";
            this.mergeBtn.Size = new System.Drawing.Size(112, 32);
            this.mergeBtn.TabIndex = 22;
            this.mergeBtn.Text = "M e r g e";
            this.mergeBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.mergeBtn.UseVisualStyleBackColor = false;
            this.mergeBtn.CheckedChanged += new System.EventHandler(this.mergeScreenBtn_CheckedChanged);
            // 
            // searchBtn
            // 
            this.searchBtn.BackColor = System.Drawing.Color.LightGray;
            this.searchBtn.FlatAppearance.BorderSize = 0;
            this.searchBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.searchBtn.Font = new System.Drawing.Font("DM Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.searchBtn.Location = new System.Drawing.Point(387, 445);
            this.searchBtn.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(112, 32);
            this.searchBtn.TabIndex = 23;
            this.searchBtn.Text = "S e a r c h";
            this.searchBtn.UseVisualStyleBackColor = false;
            this.searchBtn.Visible = false;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.BackColor = System.Drawing.Color.LightGray;
            this.cancelBtn.FlatAppearance.BorderSize = 0;
            this.cancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cancelBtn.Font = new System.Drawing.Font("DM Sans", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cancelBtn.Location = new System.Drawing.Point(154, 445);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(112, 32);
            this.cancelBtn.TabIndex = 24;
            this.cancelBtn.Text = "C a n c e l";
            this.cancelBtn.UseVisualStyleBackColor = false;
            this.cancelBtn.Visible = false;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("DM Sans", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(24, 519);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(311, 28);
            this.label4.TabIndex = 25;
            this.label4.Text = "S c r e e n s  R e s o l u t i o n s";
            // 
            // versionLbl
            // 
            this.versionLbl.AutoSize = true;
            this.versionLbl.Font = new System.Drawing.Font("DM Sans", 8.249999F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.versionLbl.ForeColor = System.Drawing.Color.Black;
            this.versionLbl.Location = new System.Drawing.Point(26, 763);
            this.versionLbl.Name = "versionLbl";
            this.versionLbl.Size = new System.Drawing.Size(73, 14);
            this.versionLbl.TabIndex = 26;
            this.versionLbl.Text = "V e r.  1 . 0 . 0";
            // 
            // DualWallpaperApp
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(786, 786);
            this.Controls.Add(this.versionLbl);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.searchBtn);
            this.Controls.Add(this.mergeBtn);
            this.Controls.Add(this.resolutionsList);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.freeSpaceArea);
            this.Controls.Add(this.applyBtn);
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
        private System.Windows.Forms.Button applyBtn;
        private System.Windows.Forms.Panel freeSpaceArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox resolutionsList;
        private System.Windows.Forms.CheckBox mergeBtn;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label versionLbl;
    }
}

