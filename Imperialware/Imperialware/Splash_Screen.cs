using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using Microsoft.Win32;
using System.Text.RegularExpressions;



namespace Imperialware
{
    public partial class Splash_Screen : Form
    {
        public Splash_Screen()
        {
            InitializeComponent();
           
        }

        string Splash_Program_Dir = "";
        string Old_Program_Dir = "";  
        string Setting = "";

    
        // if (MainWindow.Loadscreen_Speed != null){}
        public static int Loading_Bar_Speed = 2;


        private void Splash_Screen_Load(object sender, EventArgs e)
        {                            
            try
            {   // Fetching Program Directories setting from Registry
                Splash_Program_Dir = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware",
                                        "Program_Directory", "").ToString();
            } catch { }

            try
            {   // Fetching Program Directories setting from Registry
                Old_Program_Dir = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware",
                                        "Old_Program_Directory", "").ToString();
            } catch { }


            try
            {
                // Disabling Splash screen for Upgrade or Uninstall process
                if (Old_Program_Dir != "Delete_It" & Old_Program_Dir != "Upgrade")                 
                {
                    // Loading and assigning Variables from Settings.txt  
                    Setting = Splash_Program_Dir + "Settings_" + Environment.UserName + ".txt";

                    string Splash_Image = Load_Setting(Setting, "Selected_Theme") + "Splash_Screen.jpg";

                    if (File.Exists(Splash_Image)) { this.BackgroundImage = new Bitmap(Splash_Image); }
                    else { this.BackgroundImage = new Bitmap(Splash_Program_Dir + @"Themes\Default\Splash_Screen.jpg"); }

                    this.Size = new Size(BackgroundImage.Size.Width, BackgroundImage.Size.Height);
                }

                // ====== For different Window Scalings ======
                int DPI_X_Size = 0;

                // Getting Font scale setting of user
                using (Graphics gfx = this.CreateGraphics())
                {
                    DPI_X_Size = (int)gfx.DpiX;
                }

                // Depending on Windows Font scale setting of user, we adjust the progress bar:                
                if (DPI_X_Size == 120) { Progress_Bar_Splash.Location = new Point(this.Size.Width / 8, (this.Size.Height / 8) * 7); }
                else { Progress_Bar_Splash.Location = new Point(this.Size.Width / 9 * 2, (this.Size.Height / 8) * 7); } 
            }
            catch 
            {
                // this.BackColor = Color.FromArgb(250, 64, 64, 64);
                Loading_Bar_Speed = 1;
            }


            this.Opacity = 0.89;

            // This timer is used to raise the progress bar of loading
            Timer_Splash_Screen.Start();
        }


        // Increasing loading process - progress bar
        private void Timer_Splash_Screen_Tick(object sender, EventArgs e)
        {
            Progress_Bar_Splash.Increment(Loading_Bar_Speed);
            if (Progress_Bar_Splash.Value == 100)
            {
                Timer_Splash_Screen.Stop();               
            }
        }



        // ================================= Functions =====================================

        string Load_Setting(string Text_File, string Variable)
        {
            string Result = "";

            // This enables the user to assign files to certain values
            if (Text_File == "0" | Text_File == "1" | Text_File == "2") { Text_File = Setting; }
  

            try
            {
                foreach (string Line in File.ReadLines(Text_File))
                {
                    // If contains our Variable and if the line doesent start with //, # or :: (Comments)
                    if (Line.Contains(Variable) & !Regex.IsMatch(Line, "^//.*?") & !Regex.IsMatch(Line, "^#.*?") & !Regex.IsMatch(Line, "^::.*?"))
                    // if (Regex.IsMatch(Line, ".*?" + Variable + ".*?") 
                    {   // Then we split the line into a string array, at positon of the = sign.
                        string[] Value = Line.Split('=');
                        // Value[0] is the Variable itself, Value[1] is its Value! We try it because sometimes the value is empty, to prevent it from loading a non existing table
                        try
                        {   // If the Variable is THE DESIRED ONE and not just with similar name
                            if (Regex.Replace(Value[0], " ", "").Equals(Variable))
                            {   // If the value has "" it is protected from this interpreter and the _ emptyspaces wont be removed
                                if (Value[1].Contains(@""""))
                                {
                                    Result = Regex.Replace(Value[1], @"""", "");
                                    Result = Result.Substring(1, Result.Length - 1);
                                }
                                else { Result = Regex.Replace(Value[1], " ", ""); }
                            }
                        }
                        catch { }

                        // Adding the removed emptyspace back for program directory or it would break many pathsv
                        if (Result.Contains("ProgramFiles(x86)")) { Result = Regex.Replace(Result, "ProgramFiles(x86)", "Program Files (x86)"); }
                        else if (Result.Contains("ProgramFiles")) { Result = Regex.Replace(Result, "ProgramFiles", "Program Files"); }
                    }
                }
            } catch { }

            // Getting Rid of the Emptyspace, exchange this with "Result" to deactivate
            return Result;
        }


        // ================================= End of File =====================================


    }
}
