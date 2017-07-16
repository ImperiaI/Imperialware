namespace Imperialware
{
    partial class Splash_Screen
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Splash_Screen));
            this.Progress_Bar_Splash = new System.Windows.Forms.ProgressBar();
            this.Timer_Splash_Screen = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Progress_Bar_Splash
            // 
            this.Progress_Bar_Splash.Location = new System.Drawing.Point(130, 260);
            this.Progress_Bar_Splash.Name = "Progress_Bar_Splash";
            this.Progress_Bar_Splash.Size = new System.Drawing.Size(340, 12);
            this.Progress_Bar_Splash.TabIndex = 0;         
            // 
            // Timer_Splash_Screen
            // 
            this.Timer_Splash_Screen.Tick += new System.EventHandler(this.Timer_Splash_Screen_Tick);
            // 
            // Splash_Screen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(600, 300);
            this.Controls.Add(this.Progress_Bar_Splash);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Splash_Screen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Splash_Screen";
            this.Load += new System.EventHandler(this.Splash_Screen_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar Progress_Bar_Splash;
        private System.Windows.Forms.Timer Timer_Splash_Screen;
    }
}