namespace WallpaperManager
{
    partial class Form1
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
            this.detectScreenBtn = new System.Windows.Forms.Button();
            this.setWallpaperBtn = new System.Windows.Forms.Button();
            this.freeSpaceArea = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
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
            this.setWallpaperBtn.Enabled = false;
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
            this.label1.Font = new System.Drawing.Font("Segoe UI", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(22, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(212, 40);
            this.label1.TabIndex = 8;
            this.label1.Text = "Dual Wallpaper";
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
            // listBox1
            // 
            this.listBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.listBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 21;
            this.listBox1.Location = new System.Drawing.Point(29, 539);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBox1.Size = new System.Drawing.Size(230, 63);
            this.listBox1.TabIndex = 21;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(507, 502);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(172, 17);
            this.checkBox1.TabIndex = 22;
            this.checkBox1.Text = "Set Wallpaper on both screens";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(786, 786);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.freeSpaceArea);
            this.Controls.Add(this.setWallpaperBtn);
            this.Controls.Add(this.detectScreenBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "DW";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
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
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

