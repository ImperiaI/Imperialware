namespace Imperialware.Resources
{
    public partial class Caution_Window
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
            this.Text_Box_Caution_Window = new System.Windows.Forms.RichTextBox();
            this.Button_Caution_Box_2 = new System.Windows.Forms.Button();
            this.Button_Caution_Box_1 = new System.Windows.Forms.Button();
            this.Button_Caution_Box_3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Text_Box_Caution_Window
            // 
            this.Text_Box_Caution_Window.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Text_Box_Caution_Window.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Text_Box_Caution_Window.Font = new System.Drawing.Font("Georgia", 15F);
            this.Text_Box_Caution_Window.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.Text_Box_Caution_Window.Location = new System.Drawing.Point(-5, -5);
            this.Text_Box_Caution_Window.Margin = new System.Windows.Forms.Padding(4);
            this.Text_Box_Caution_Window.Name = "Text_Box_Caution_Window";
            this.Text_Box_Caution_Window.ReadOnly = true;
            this.Text_Box_Caution_Window.Size = new System.Drawing.Size(510, 320);
            this.Text_Box_Caution_Window.TabIndex = 0;
            this.Text_Box_Caution_Window.Text = "";
            this.Text_Box_Caution_Window.WordWrap = false;
            // 
            // Button_Caution_Box_2
            // 
            this.Button_Caution_Box_2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button_Caution_Box_2.Font = new System.Drawing.Font("Georgia", 14F);
            this.Button_Caution_Box_2.Location = new System.Drawing.Point(400, 256);
            this.Button_Caution_Box_2.Name = "Button_Caution_Box_2";
            this.Button_Caution_Box_2.Size = new System.Drawing.Size(126, 40);
            this.Button_Caution_Box_2.TabIndex = 96;
            this.Button_Caution_Box_2.Text = "No";
            this.Button_Caution_Box_2.UseVisualStyleBackColor = true;
            this.Button_Caution_Box_2.Visible = false;
            this.Button_Caution_Box_2.Click += new System.EventHandler(this.Button_Caution_Box_2_Click);
            // 
            // Button_Caution_Box_1
            // 
            this.Button_Caution_Box_1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button_Caution_Box_1.Font = new System.Drawing.Font("Georgia", 14F);
            this.Button_Caution_Box_1.Location = new System.Drawing.Point(112, 256);
            this.Button_Caution_Box_1.Name = "Button_Caution_Box_1";
            this.Button_Caution_Box_1.Size = new System.Drawing.Size(126, 40);
            this.Button_Caution_Box_1.TabIndex = 97;
            this.Button_Caution_Box_1.Text = "Yes";
            this.Button_Caution_Box_1.UseVisualStyleBackColor = true;
            this.Button_Caution_Box_1.Visible = false;
            this.Button_Caution_Box_1.Click += new System.EventHandler(this.Button_Caution_Box_1_Click);
            // 
            // Button_Caution_Box_3
            // 
            this.Button_Caution_Box_3.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.Button_Caution_Box_3.Font = new System.Drawing.Font("Georgia", 14F);
            this.Button_Caution_Box_3.Location = new System.Drawing.Point(256, 256);
            this.Button_Caution_Box_3.Name = "Button_Caution_Box_3";
            this.Button_Caution_Box_3.Size = new System.Drawing.Size(126, 40);
            this.Button_Caution_Box_3.TabIndex = 98;
            this.Button_Caution_Box_3.Text = "Maybe";
            this.Button_Caution_Box_3.UseVisualStyleBackColor = true;
            this.Button_Caution_Box_3.Visible = false;
            this.Button_Caution_Box_3.Click += new System.EventHandler(this.Button_Caution_Box_3_Click);
            // 
            // Caution_Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 312);
            this.Controls.Add(this.Button_Caution_Box_3);
            this.Controls.Add(this.Button_Caution_Box_1);
            this.Controls.Add(this.Button_Caution_Box_2);
            this.Controls.Add(this.Text_Box_Caution_Window);
            this.Font = new System.Drawing.Font("Georgia", 10F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(516, 150);
            this.Name = "Caution_Window";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Caution";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Caution_Window_Load);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.RichTextBox Text_Box_Caution_Window;
        public System.Windows.Forms.Button Button_Caution_Box_2;
        public System.Windows.Forms.Button Button_Caution_Box_1;
        public System.Windows.Forms.Button Button_Caution_Box_3;

    }
}