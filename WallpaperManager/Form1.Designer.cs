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
            this.wallpaper1 = new System.Windows.Forms.PictureBox();
            this.wallpaper2 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.closeBtn = new System.Windows.Forms.Button();
            this.freeSpaceArea = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.wallpaper1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wallpaper2)).BeginInit();
            this.freeSpaceArea.SuspendLayout();
            this.SuspendLayout();
            // 
            // detectScreenBtn
            // 
            this.detectScreenBtn.BackColor = System.Drawing.Color.LightGray;
            this.detectScreenBtn.FlatAppearance.BorderSize = 0;
            this.detectScreenBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.detectScreenBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.detectScreenBtn.Location = new System.Drawing.Point(662, 433);
            this.detectScreenBtn.Name = "detectScreenBtn";
            this.detectScreenBtn.Size = new System.Drawing.Size(95, 32);
            this.detectScreenBtn.TabIndex = 0;
            this.detectScreenBtn.Text = "Identify";
            this.detectScreenBtn.UseVisualStyleBackColor = false;
            this.detectScreenBtn.Click += new System.EventHandler(this.detectScreenBtn_Click);
            // 
            // wallpaper1
            // 
            this.wallpaper1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(204)))), ((int)(((byte)(65)))), ((int)(((byte)(41)))));
            this.wallpaper1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wallpaper1.Location = new System.Drawing.Point(236, 83);
            this.wallpaper1.Name = "wallpaper1";
            this.wallpaper1.Size = new System.Drawing.Size(220, 80);
            this.wallpaper1.TabIndex = 3;
            this.wallpaper1.TabStop = false;
            this.wallpaper1.DoubleClick += new System.EventHandler(this.wallpaper1_DoubleClick);
            // 
            // wallpaper2
            // 
            this.wallpaper2.BackColor = System.Drawing.Color.LightGray;
            this.wallpaper2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.wallpaper2.Location = new System.Drawing.Point(462, 83);
            this.wallpaper2.Name = "wallpaper2";
            this.wallpaper2.Size = new System.Drawing.Size(70, 126);
            this.wallpaper2.TabIndex = 4;
            this.wallpaper2.TabStop = false;
            this.wallpaper2.DoubleClick += new System.EventHandler(this.wallpaper2_DoubleClick);
            this.wallpaper2.MouseDown += new System.Windows.Forms.MouseEventHandler(this.wallpaper2_MouseDown);
            this.wallpaper2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.wallpaper2_MouseMove);
            this.wallpaper2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.wallpaper2_MouseUp);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(331, 610);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(323, 76);
            this.button2.TabIndex = 5;
            this.button2.Text = "Set Wallpaper";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // closeBtn
            // 
            this.closeBtn.FlatAppearance.BorderSize = 0;
            this.closeBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.closeBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(17)))), ((int)(((byte)(35)))));
            this.closeBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeBtn.Font = new System.Drawing.Font("Segoe UI Light", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeBtn.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.closeBtn.Location = new System.Drawing.Point(739, -1);
            this.closeBtn.Margin = new System.Windows.Forms.Padding(0);
            this.closeBtn.Name = "closeBtn";
            this.closeBtn.Size = new System.Drawing.Size(48, 34);
            this.closeBtn.TabIndex = 6;
            this.closeBtn.Text = "x";
            this.closeBtn.UseVisualStyleBackColor = true;
            this.closeBtn.Click += new System.EventHandler(this.closeBtn_Click);
            // 
            // freeSpaceArea
            // 
            this.freeSpaceArea.AllowDrop = true;
            this.freeSpaceArea.BackColor = System.Drawing.Color.WhiteSmoke;
            this.freeSpaceArea.Controls.Add(this.wallpaper1);
            this.freeSpaceArea.Controls.Add(this.wallpaper2);
            this.freeSpaceArea.Location = new System.Drawing.Point(29, 178);
            this.freeSpaceArea.Margin = new System.Windows.Forms.Padding(20, 3, 20, 3);
            this.freeSpaceArea.Name = "freeSpaceArea";
            this.freeSpaceArea.Size = new System.Drawing.Size(728, 249);
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
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(786, 786);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.freeSpaceArea);
            this.Controls.Add(this.closeBtn);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.detectScreenBtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.wallpaper1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wallpaper2)).EndInit();
            this.freeSpaceArea.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button detectScreenBtn;
        private System.Windows.Forms.PictureBox wallpaper1;
        private System.Windows.Forms.PictureBox wallpaper2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button closeBtn;
        private System.Windows.Forms.Panel freeSpaceArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

