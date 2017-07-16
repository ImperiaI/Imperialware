
using System;
using System.Collections.Generic;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Imperialware.Resources;

using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System.Threading;





namespace Imperialware
{
    public partial class MainWindow
    {


        //=====================//

        void Renaming(string Path_and_File, string New_File_Name)
        {   
            try
            {   // get the file attributes for file or directory
                FileAttributes The_Attribute = File.GetAttributes(Path_and_File);

                //detect whether its a directory or file
                if ((The_Attribute & FileAttributes.Directory) == FileAttributes.Directory)
                {   
                    try
                    {   // Getting the Path only for the right parameter and appending the new name to it
                        Directory.Move(Path_and_File, Path.GetDirectoryName(Path_and_File) + @"\" + New_File_Name);
                    }
                    catch { }
                }
                else
                {
                    try
                    { 
                        File.Move(Path_and_File, Path.GetDirectoryName(Path_and_File) + @"\" + New_File_Name);
                    }
                    catch { }
                }
            }
            catch { }
        }
        //=====================//

        void Moving(string Path_and_File, string New_Path)
        {   // This little function just saves me annoying syntax typing so I can just write destination path

            if (Debug_Mode == "false")
            { 
                try
                {   // get the file attributes for file or directory
                    FileAttributes The_Attribute = File.GetAttributes(Path_and_File);

                    if (!Directory.Exists(New_Path)) { Directory.CreateDirectory(New_Path); }

                    //detect whether its a directory or file
                    if ((The_Attribute & FileAttributes.Directory) == FileAttributes.Directory)
                    { 
                        try { System.IO.Directory.Move(Path_and_File, New_Path + @"\" + Path.GetFileName(Path_and_File)); }  catch { }
                    }
                    else
                    {
                        try { System.IO.File.Move(Path_and_File, New_Path + @"\" + Path.GetFileName(Path_and_File)); } catch { }
                    }
                } catch { }
            }

            else if (Debug_Mode == "true")
            {
                // get the file attributes for file or directory
                FileAttributes The_Attribute = File.GetAttributes(Path_and_File);

                if (!Directory.Exists(New_Path)) { Directory.CreateDirectory(New_Path); }

                //detect whether its a directory or file
                if ((The_Attribute & FileAttributes.Directory) == FileAttributes.Directory)
                { System.IO.Directory.Move(Path_and_File, New_Path + @"\" + Path.GetFileName(Path_and_File)); } 
               
                else { System.IO.File.Move(Path_and_File, New_Path + @"\" + Path.GetFileName(Path_and_File)); } 
            }                  
        }

        //========= Thanks to jaysponsored form Stackoverfl0w ==========//
        // I almost can't believe Microsoft doesent provide a proper Copy function for Directories !! 
        void Copy_Now(string Source_Directory, string Destination_Directory) 
        {
            if (Debug_Mode == "false")
            {
                // substring is to remove Destination_Directory absolute path (E:\).
                try
                {   // Create subdirectory structure in destination    
                    foreach (string dir in Directory.GetDirectories(Source_Directory, "*", System.IO.SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(Destination_Directory + dir.Substring(Source_Directory.Length));
                        // Example:
                        //     > C:\sources (and not C:\E:\sources)
                    }
                } catch { }

                try 
                {   foreach (string file_name in Directory.GetFiles(Source_Directory, "*.*", System.IO.SearchOption.AllDirectories))
                    {
                        File.Copy(file_name, Destination_Directory + file_name.Substring(Source_Directory.Length), true);
                    }
                } catch { }
            }

            else if (Debug_Mode == "true")
            {
                // Create subdirectory structure in destination    
                foreach (string dir in Directory.GetDirectories(Source_Directory, "*", System.IO.SearchOption.AllDirectories))
                {
                    Directory.CreateDirectory(Destination_Directory + dir.Substring(Source_Directory.Length));
                    // Example:
                    //     > C:\sources (and not C:\E:\sources)
                }

                foreach (string file_name in Directory.GetFiles(Source_Directory, "*.*", System.IO.SearchOption.AllDirectories))
                {
                    File.Copy(file_name, Destination_Directory + file_name.Substring(Source_Directory.Length), true);
                }
            }

        }
        //=====================//
        // If not exist add from source path
        void Verify_Copy(string Source_Path_and_File, string New_Path_and_File)
        {
            if (!Directory.Exists(Path.GetDirectoryName(Source_Path_and_File)))
            { Imperial_Console(600, 100, Add_Line + Path.GetDirectoryName(Source_Path_and_File)); Directory.CreateDirectory(Path.GetDirectoryName(Source_Path_and_File)); }

            if (Debug_Mode == "false")
            {   try 
                {   if (!File.Exists(New_Path_and_File))
                    { File.Copy(Source_Path_and_File, New_Path_and_File, true); }
                } catch { }
            }           
            else if (Debug_Mode == "true")
            { 
                if (!File.Exists(New_Path_and_File))
                { File.Copy(Source_Path_and_File, New_Path_and_File, true); }
            }
        }


        
        // If exists in source path copy and OVERWRITE that to target
        void Overwrite_Copy(string Source_Path_and_File, string New_Path_and_File)
        {   
            if (Debug_Mode == "false")
            {   try
                {   if (File.Exists(Source_Path_and_File))
                    { File.Copy(Source_Path_and_File, New_Path_and_File, true); }
                } catch { }
            }
            else if (Debug_Mode == "true")
            {
                if (File.Exists(Source_Path_and_File))
                { File.Copy(Source_Path_and_File, New_Path_and_File, true); }
            }
        }
        //=====================//
        void Deleting(string Data)
        {   int Error_Count = 0;
           
            if (!Directory.Exists(Data)) { Error_Count = Error_Count + 10; } 
            else 
            {   try { Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(Data, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin); }
                catch { Error_Count++; }
            }

            if (!File.Exists(Data)) { Error_Count = Error_Count + 10; }
            else
            {   try { Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(Data, Microsoft.VisualBasic.FileIO.UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin); }
                catch { Error_Count++; }
            }

            // If both methods failed that probably means no Visual Basic is installed.
            if (Error_Count != 0 & Error_Count < 3) { Imperial_Console(600, 100, "Error: You seem to be missing Net Framework 4.0 and Visual Basic.FileIO on your System."); }
            else if (Debug_Mode == "true" & Error_Count > 19) { Imperial_Console(600, 100, "   Could not find the selected Object for deletion."); }

            Error_Count = 0;
        }




        //========== DON'T use emptyspace signs for this function, except in "quote marks"! ===========//
        string Load_Setting(string Text_File, string Variable)
        {
            string Result = "";

            // This enables the user to assign files to certain values
            if (Text_File == "0" | Text_File == "1" | Text_File == "2") { Text_File = Setting; }
            else if (Text_File == "4") { Text_File = Maximum_Values; }


            /* --> Wont work because if the iEnumerable Boot_Settings is innitiated as global variable it causes a open process to freeze the application!
            IEnumerable<string> The_File;
            // Performance related: When boot loading we use a preset file, otherwise we load it new each time the function is called.
            if (User_Input == false) { The_File = Boot_Settings; }
            else { The_File = File.ReadLines(Text_File);} 
            */


            try
            {   foreach (string Line in File.ReadLines(Text_File))
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
                                if (Value[1].Contains(@"""")) { 
                                    Result = Regex.Replace(Value[1], @"""", "");
                                    Result = Result.Substring(1, Result.Length - 1);
                                }
                                else {Result = Regex.Replace(Value[1], " ", "");}
                            }
                        } catch { }

                        // Adding the removed emptyspace back for program directory or it would break many pathsv
                        if (Result.Contains("ProgramFiles(x86)")) { Result = Regex.Replace(Result, "ProgramFiles(x86)", "Program Files (x86)"); }
                        else if (Result.Contains("ProgramFiles")) { Result = Regex.Replace(Result, "ProgramFiles", "Program Files"); }
                    }
                }
            }
            
            catch 
            {   if (Debug_Mode == "true")
                {
                    if (User_Input & !Variable.Contains("Max")) { Imperial_Console(600, 100, "Sorry, I was not able to load the value of the variable " + Variable); }
                   
                    // Collecting all variables that failed to load in that list to display it whether after the startup function or (if Debug Mode) right now.                  
                    else { Error_List.Add(Variable); }                 
                }             
            }

            // Getting Rid of the Emptyspace, exchange this with "Result" to deactivate
            return Result;
 
        }


        //=====================//

        void Load_All_Settings(string Text_File, string[] Load_Settings)
        {
          foreach (string Entry in Load_Settings)
            {
                
                try 
                {   // Get the variable from the string array "Load_Settings" using FieldInfo.
                    System.Reflection.FieldInfo Window_Info = typeof(Imperialware.MainWindow).GetField(Entry);

                    // Set static field (variable) to this value.
                    Window_Info.SetValue(null, Load_Setting(Text_File, Entry));
                }
                
              
                catch {Error_List.Add(Entry); Temporal_A = "true";}               
            }

            if (Debug_Mode == "true" & Temporal_A == "true")
            {   Temporal_A = "false";
                string Error_Text = "";
                int Console_Height = 200;
                if (Error_List.Count > 5) { Console_Height = 600; } 
              
                foreach (string Error in Error_List) { Error_Text += "    " + Error + Add_Line; }
                Imperial_Console(600, Console_Height, Add_Line + "    Error: Could not load " + Add_Line + Error_Text);
            }
        }
        //=====================//

        void Save_Setting(string Text_File, string Variable, string New_Value)
        {
            bool Quote = false;


            // "2" and "4" is used for int type values, then we won't need to try this
            if (Text_File != "2" & Text_File != "4" & Text_File != Maximum_Values
                & Text_File != Maximum_Values_Fighter & Text_File != Maximum_Values_Bomber & Text_File != Maximum_Values_Corvette & Text_File != Maximum_Values_Frigate & Text_File != Maximum_Values_Capital
                & Text_File != Maximum_Values_Infantry & Text_File != Maximum_Values_Vehicle & Text_File != Maximum_Values_Hero & Text_File != Maximum_Values_Structure)

            {   // This sets our variable in the memory to the new value
                try
                {   // Get our variable 
                    System.Reflection.FieldInfo Window_Info = typeof(Imperialware.MainWindow).GetField(Variable);

                    // Set variable to new value.
                    Window_Info.SetValue(Variable, New_Value);
                }
                catch
                {
                    try
                    {
                        System.Reflection.FieldInfo Window_Info = typeof(Imperialware.MainWindow).GetField(Variable);
                        Window_Info.SetValue(null, New_Value.ToString());
                    }
                    catch
                    {
                        if (Debug_Mode == "true")
                        {
                                Imperial_Console(700, 150, Add_Line + "    Error: Could not convert the new Value for " + @"""" + Variable + @"""" 
                                                     + Add_Line + "    to String in order to store it.");
                        }
                    }
                }
            }

            // This enables the user to assign files to certain values
            // 0 = In Quotemarks, 1 = Normal with variable setup, 2 = No variable setup ( use for int values)          
            if (Text_File == "0") { Text_File = Setting; Quote = true; }
            else if (Text_File == "1" | Text_File == "2") { Text_File = Setting; }
            else if (Text_File == "4") { Text_File = Maximum_Values; }
           

            // Reading File      
            string The_Text = File.ReadAllText(Text_File);

            // If the New Variable is not the old one
            if (Load_Setting(Text_File, Variable) != New_Value)
            {        
                string Old_Entry = "";

                foreach (string Line in File.ReadLines(Text_File))
                {   // If contains our Variable and if the line doesent start with //, # or :: (Comments)
                    if (Line.Contains(Variable) & !Regex.IsMatch(Line, "^//.*?") & !Regex.IsMatch(Line, "^#.*?") & !Regex.IsMatch(Line, "^::.*?"))
                    {
                        // First I split Variable = Result into a array
                        string[] Value = Line.Split('=');
                        // Then we use Regex to get rid of the _Emptyspaces and make sure we got the right Variable, if so we got our Old_Entry selected.
                        try { if (Regex.Replace(Value[0], " ", "").Equals(Variable)) { Old_Entry = Line; } }
                        catch { }
                    }
                }


                // If the parameter 0 was choosen that means the new value needs to be a quoted path!
                if (Quote)
                {   // Loading the specified Variable and Replacing the Old value it returns with a new one (if duplicates exist it replaces the last one)
                    try { The_Text = The_Text.Replace(Old_Entry, Variable + " = " + @"""" + New_Value + @""""); } catch { }             
                }
                else
                {   // Otherwise we store without quote marks
                    try { The_Text = The_Text.Replace(Old_Entry, Variable + " = " + New_Value); } catch { }
                }


                // Saving Changes
                File.WriteAllText(Text_File, The_Text);
            }
        }
 
       //=====================//

       // Window Size expects 2 int parameters like 700, 200
       void Imperial_Console(int Window_Size_X, int Window_Size_Y, string Text)
       {   // Innitiating new Form
           Caution_Window Display = new Caution_Window();
           Display.Opacity = Transparency;
           Display.Size = new Size(Window_Size_X, Window_Size_Y);

           // Using Theme colors for Text and Background
           Display.Text_Box_Caution_Window.BackColor = Color_05;
           Display.Text_Box_Caution_Window.ForeColor = Color_03;

           Display.Text_Box_Caution_Window.Text = Text; 

           Display.Show();

       }
       //=====================//


       void Imperial_Dialogue(int Window_Size_X, int Window_Size_Y, string Button_A_Text, string Button_B_Text, string Button_C_Text, string Text) 
       {
         //========== Displaying Error Messages to User   
            // Innitiating new Form
            Caution_Window Display = new Caution_Window();
            Display.Opacity = Transparency;
            Display.Size = new Size(Window_Size_X, Window_Size_Y);

            // Using Theme colors for Text and Background
            Display.Text_Box_Caution_Window.BackColor = Color_05;
            Display.Text_Box_Caution_Window.ForeColor = Color_03;


            if (Button_A_Text != "false" & Button_C_Text == "false")
            {   Display.Button_Caution_Box_1.Visible = true;
                Display.Button_Caution_Box_1.Text = Button_A_Text;
                Display.Button_Caution_Box_1.Location = new Point(120, 86);
            }


            if (Button_B_Text != "false" & Button_C_Text == "false")
            {   Display.Button_Caution_Box_2.Visible = true;
                Display.Button_Caution_Box_2.Text = Button_B_Text;
                Display.Button_Caution_Box_2.Location = new Point(280, 86);
            }

            else if (Button_C_Text != "false")
            {
                // The first 2 buttons moves aside to free space for this one
                Display.Button_Caution_Box_1.Visible = true;
                Display.Button_Caution_Box_1.Text = Button_A_Text;
                Display.Button_Caution_Box_1.Location = new Point(60, 86);
             
                Display.Button_Caution_Box_2.Visible = true;
                Display.Button_Caution_Box_2.Text = Button_B_Text;
                Display.Button_Caution_Box_2.Location = new Point(380, 86);

                Display.Button_Caution_Box_3.Visible = true;
                Display.Button_Caution_Box_3.Text = Button_C_Text;
                Display.Button_Caution_Box_3.Location = new Point(220, 86);
            }

            Display.Text_Box_Caution_Window.Text = Text;


            Display.ShowDialog(this);

       }
 
       //=====================//

       void Set_Maximal_Value_Directories()
       {
           Temporal_B = Mod_Name;

           // This line just makes sure we use the same info directory for different version of the Stargate TPC Mod
           if (Temporal_B == "StargateAdmin" | Temporal_B == "StargateBeta" | Temporal_B == "StargateOpenBeta") { Temporal_B = "Stargate"; }

           // Distinguishing between EAW and FOC version of Stargate
           if (Game_Path == Game_Path_EAW & Temporal_B == "Stargate") { Temporal_B = "Stargate_Empire_at_War"; }
         

          
           Maximum_Values_Fighter = Program_Directory + Mods_Directory + Temporal_B + @"\Maximal_Values\Fighter.txt";
           Maximum_Values_Bomber = Program_Directory + Mods_Directory + Temporal_B + @"\Maximal_Values\Bomber.txt";
           Maximum_Values_Corvette = Program_Directory + Mods_Directory + Temporal_B + @"\Maximal_Values\Corvette.txt";
           Maximum_Values_Frigate = Program_Directory + Mods_Directory + Temporal_B + @"\Maximal_Values\Frigate.txt";
           Maximum_Values_Capital = Program_Directory + Mods_Directory + Temporal_B + @"\Maximal_Values\Capital.txt";

           Maximum_Values_Infantry = Program_Directory + Mods_Directory + Temporal_B + @"\Maximal_Values\Infantry.txt";
           Maximum_Values_Vehicle = Program_Directory + Mods_Directory + Temporal_B + @"\Maximal_Values\Vehicle.txt";
           Maximum_Values_Air = Program_Directory + Mods_Directory + Temporal_B + @"\Maximal_Values\Air.txt";
           Maximum_Values_Hero = Program_Directory + Mods_Directory + Temporal_B + @"\Maximal_Values\Hero.txt";
           Maximum_Values_Structure = Program_Directory + Mods_Directory + Temporal_B + @"\Maximal_Values\Structure.txt";
       }


       void Load_Maximal_Values(string Text_File)
       {
           // Loading Int type Variables from settings file 
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Model_Scale"), out Maximum_Model_Scale);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Model_Height"), out Maximum_Model_Height);

           Int32.TryParse(Load_Setting(Text_File, "Maximum_Select_Box_Scale"), out Maximum_Select_Box_Scale);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Health_Bar_Height"), out Maximum_Health_Bar_Height);

           // Power Values
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Credits"), out Maximum_Credits);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Hull"), out Maximum_Hull);

           Int32.TryParse(Load_Setting(Text_File, "Maximum_Shield"), out Maximum_Shield);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Shield_Rate"), out Maximum_Shield_Rate);

           Int32.TryParse(Load_Setting(Text_File, "Maximum_Energy"), out Maximum_Energy);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Energy_Rate"), out Maximum_Energy_Rate);

           Int32.TryParse(Load_Setting(Text_File, "Maximum_Speed"), out Maximum_Speed);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_AI_Combat"), out Maximum_AI_Combat);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Projectile"), out Maximum_Projectile);
    

           // Requirement Values
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Build_Cost"), out Maximum_Build_Cost);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Skirmish_Cost"), out Maximum_Skirmish_Cost);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Build_Time"), out Maximum_Build_Time);

           Int32.TryParse(Load_Setting(Text_File, "Maximum_Tech_Level"), out Maximum_Tech_Level);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Star_Base_LV"), out Maximum_Star_Base_LV);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Ground_Base_LV"), out Maximum_Ground_Base_LV);

           Int32.TryParse(Load_Setting(Text_File, "Maximum_Timeline"), out Maximum_Timeline);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Slice_Cost"), out Maximum_Slice_Cost);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Build_Limit"), out Maximum_Build_Limit);
           Int32.TryParse(Load_Setting(Text_File, "Maximum_Lifetime_Limit"), out Maximum_Lifetime_Limit);


           Int32.TryParse(Load_Setting(Text_File, "Costum_Tag_4_Max_Value"), out Costum_Tag_4_Max_Value);
           Int32.TryParse(Load_Setting(Text_File, "Costum_Tag_5_Max_Value"), out Costum_Tag_5_Max_Value);
           Int32.TryParse(Load_Setting(Text_File, "Costum_Tag_6_Max_Value"), out Costum_Tag_6_Max_Value);


           // Setting Maximal Values to their Textboxes
           Text_Box_Max_Model_Scale.Text = Maximum_Model_Scale.ToString();
           Text_Box_Max_Model_Height.Text = Maximum_Model_Height.ToString();
           Text_Box_Max_Select_Box_Scale.Text = Maximum_Select_Box_Scale.ToString();
           Text_Box_Max_Health_Bar_Height.Text = Maximum_Health_Bar_Height.ToString();
          
           Text_Box_Max_Credits.Text = Maximum_Credits.ToString();
           Text_Box_Max_Hull.Text = Maximum_Hull.ToString();
           Text_Box_Max_Shield.Text = Maximum_Shield.ToString();
           Text_Box_Max_Shield_Rate.Text = Maximum_Shield_Rate.ToString();

           Text_Box_Max_Energy.Text = Maximum_Energy.ToString();
           Text_Box_Max_Energy_Rate.Text = Maximum_Energy_Rate.ToString();
           Text_Box_Max_Speed.Text = Maximum_Speed.ToString();
           Text_Box_Max_AI_Combat.Text = Maximum_AI_Combat.ToString();
           Text_Box_Max_Projectile.Text = Maximum_Projectile.ToString();   

           Text_Box_Max_Build_Cost.Text = Maximum_Build_Cost.ToString();
           Text_Box_Max_Skirmish_Cost.Text = Maximum_Skirmish_Cost.ToString();
           Text_Box_Max_Build_Time.Text = Maximum_Build_Time.ToString();
           Text_Box_Max_Tech_Level.Text = Maximum_Tech_Level.ToString();
           Text_Box_Max_Star_Base_LV.Text = Maximum_Star_Base_LV.ToString();
           Text_Box_Max_Ground_Base_LV.Text = Maximum_Ground_Base_LV.ToString();
           Text_Box_Max_Timeline.Text = Maximum_Timeline.ToString();
           Text_Box_Max_Slice_Cost.Text = Maximum_Slice_Cost.ToString();
           Text_Box_Max_Build_Limit.Text = Maximum_Build_Limit.ToString();
           Text_Box_Max_Lifetime_Limit.Text = Maximum_Lifetime_Limit.ToString();

           Text_Box_Max_Costum_4.Text = Costum_Tag_4_Max_Value.ToString();
           Text_Box_Max_Costum_5.Text = Costum_Tag_5_Max_Value.ToString();
           Text_Box_Max_Costum_6.Text = Costum_Tag_6_Max_Value.ToString();


           // Report function for all missloaded Variables
           if (Loading_Completed & Debug_Mode == "true" & Error_List.Count > 0)
           {
               Temporal_A = "";
               Temporal_C = 200;
               if (Error_List.Count > 5) { Temporal_C = 600; }

               foreach (string Error in Error_List) { Temporal_A += "    " + Error + Add_Line; }
               Imperial_Console(600, Temporal_C, Add_Line + "    Error: Could not load " + Add_Line + Temporal_A);

               Error_List.Clear();
           }

       }


       // Usage: Set_Maximal_Values((TextBox)sender, "Maximum_Hull", Maximum_Hull);
       void Set_Maximal_Values(TextBox Text_Box, string Variable_Name, int Variable_Value)
       {   // Making sure this doesent bug at startup to permanently retry and loop open the error Message !!
           if (User_Input == true)
           {
               // Need to make sure the Mod dir exists, otherwise stackoverflow!
               if (Mod_Name != "" & Mod_Name != "Inactive" & Mod_Name != "false" & Directory.Exists(Game_Path + Mods_Directory + Mod_Name))
               {   // We forward the Name of our Element as String the function, at the same time we use that function to check whether a valid entry was typed.                  
                   if (Edit_Max_Value_Text(Text_Box.Name.ToString()) == true) 
                   { 
                   // Converting the text string to int
                   Int32.TryParse(Text_Box.Text, out Variable_Value);

                   if (Maximal_Value_Class == "1") { Save_Setting(Maximum_Values_Fighter, Variable_Name, Variable_Value.ToString()); }
                   else if (Maximal_Value_Class == "2") { Save_Setting(Maximum_Values_Bomber, Variable_Name, Variable_Value.ToString()); }
                   else if (Maximal_Value_Class == "3") { Save_Setting(Maximum_Values_Corvette, Variable_Name, Variable_Value.ToString()); }
                   else if (Maximal_Value_Class == "4") { Save_Setting(Maximum_Values_Frigate, Variable_Name, Variable_Value.ToString()); }
                   else if (Maximal_Value_Class == "5") { Save_Setting(Maximum_Values_Capital, Variable_Name, Variable_Value.ToString()); }

                   else if (Maximal_Value_Class == "6") { Save_Setting(Maximum_Values_Infantry, Variable_Name, Variable_Value.ToString()); }
                   else if (Maximal_Value_Class == "7") { Save_Setting(Maximum_Values_Vehicle, Variable_Name, Variable_Value.ToString()); }
                   else if (Maximal_Value_Class == "8") { Save_Setting(Maximum_Values_Hero, Variable_Name, Variable_Value.ToString()); }
                   else if (Maximal_Value_Class == "9") { Save_Setting(Maximum_Values_Structure, Variable_Name, Variable_Value.ToString()); }
                   }
               }
               // Otherwise we restore the original Value in the Text Box -- This else line could cause a Stack Overflow Exception!
               else { Text_Box.Text = Variable_Value.ToString(); }
           }
       }

       //=====================//
       void Set_Language()
       {   // Setting combobox field to the language
            Combo_Box_Language.Text = Game_Language;
       
            // Setting flag image, the images are from: http://www.iconarchive.com/show/flag-icons-by-gosquared.8.html
            Image Language_Flag = new Bitmap(Program_Directory + @"Images\Languages\Flag_" + Game_Language + ".jpg");
            Picture_Box_Languages.BackgroundImage = Language_Flag;
            

            if (Game_Language == "English")
            {
                
            }
            else if (Game_Language == "German")
            {
                
            }
        }

       //============ Set Checkbox ===========//
       // This wont work if exectuted from a Checkbox because then it causes a Stack Overflow.
        void Toggle_Checkbox(CheckBox Check_Box, string Text_File, string Variable_Name)
       {   // User_Input prevents the loading function from unintentionally toggeling the value
           if (User_Input == true)
           {
               // If the variable is already true, we are going to toggle it to false
               if (Check_Box.Checked)
               {   // Switching Name Checkbox Color in order to highlight the selected Checkbox, and unchecking it
                   // Check_Box.ForeColor = Color.FromArgb(200, 40, 130, 240);
                   Check_Box.ForeColor = Color_02;
                   Check_Box.Checked = !Check_Box.Checked;

                   Save_Setting(Text_File, Variable_Name, "false");

               } // Otherwise it toggles to be true

               else
               {
                   Check_Box.ForeColor = Color_03;
                   Check_Box.Checked = !Check_Box.Checked;

                   Save_Setting(Text_File, Variable_Name, "true");
               }
           }
       }


        //========= Auto Set Checkbox (without causeing Stackoverfl0w) ========//
        void Auto_Toggle_Checkbox(CheckBox Check_Box, string Text_File, string Variable_Name)
        {   // User_Input prevents the loading function from unintentionally toggeling the value
            if (User_Input == true)
            {
                // If the variable is already true, we are going to toggle it to false
                if (!Check_Box.Checked)
                {   // Switching Name Checkbox Color in order to highlight the selected Checkbox, and unchecking it
                    Check_Box.ForeColor = Color_02;
                    Save_Setting(Text_File, Variable_Name, "false");

                } // Otherwise it toggles to be true

                else
                {   Check_Box.ForeColor = Color_03;     
                    Save_Setting(Text_File, Variable_Name, "true");
                }
            }
        }

        // You can also use "(CheckBox)sender" as general variable to call this function:
        // Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Evade_Language");


       //============ Set Radiobutton ===========// temp
       void Toggle_Radio_Button(RadioButton Radio_Button, string Text_File, string Variable_Name, string Variable_Value)
       {   // User_Input prevents the loading function from unintentionally toggeling the value
           if (User_Input == true)
           {

               // If the variable is already true, we are going to toggle it to false
               if (Variable_Value == "true")
               {   // Switching Name Checkbox Color in order to highlight the selected Checkbox, and unchecking it
                   Radio_Button.ForeColor = Color_02;
                   Radio_Button.Select();

                   Variable_Value = "false";
                   Save_Setting(Text_File, Variable_Name, "false");

               } // Otherwise it toggles to be true
               else if (Variable_Value == "false")
               {
                   Radio_Button.ForeColor = Color_03;
                   Radio_Button.Select();

                   Variable_Value = "true";
                   Save_Setting(Text_File, Variable_Name, Variable_Value);
               }
           }
       }



        //============ Moving to Texture Directory (Push) ===========//

        void Switch_Loading_Screens()
        {

            // These are needed to cycle the ingame screen images
            Image_Cycle = Load_Setting(Setting, "Image_Cycle");
            Moved_Images = Load_Setting(Setting, "Moved_Images");


            // If a Cycle Images directory was found in .\Art\Textures of this mod
            if (Directory.Exists(Art_Directory + @"Textures\Cycle_Images") & Moved_Images != "false" & Moved_Images != "")
            {
                // Extracting all stored image data from string format
                string[] All_Moved_Images = Moved_Images.Split(',');

                try
                {
                    foreach (string Image in All_Moved_Images)
                    {   // Moving the Images from the last usage back into their original directory we stored
                        File.Move(Art_Directory + @"Textures\" + Image, Art_Directory + @"Textures\Cycle_Images\" + Image_Cycle + @"\" + Image);
                    }
                } catch { }
            }


            //=========== Drawing from Cycle Directory (Pull) =======//
       

            // Innitiating int list and adding just 0 to get rid of the 0 slot, so we can later start at 1
            List<string> Found_Numbers = new List<string>();
            Found_Numbers.Add("0");


            // This for loop iterates up to 20 until all directories were found
            for (int i = 1; i <= 20; ++i)
            {

                // We need to concider the 0 from the first 9 digits, we use this state to test the value.
                if (i < 10)
                {
                    // If any numbered directory was found in there, we add it into our list variable
                    if (Directory.Exists(Art_Directory + @"Textures\Cycle_Images\0" + i)) { Found_Numbers.Add("0" + i); }
                }
                else
                {
                    if (Directory.Exists(Art_Directory + @"Textures\Cycle_Images\" + i)) { Found_Numbers.Add(i.ToString()); }
                }
            }


            // Generating random Variable
            Random Randomizer = new Random();
            // Generating random Number from this range
            int Random = Randomizer.Next(1, Found_Numbers.Count());

            if (Image_Cycle != "false" & Image_Cycle != "" & Random.ToString() == Image_Cycle)
            // If the same value as before was chosen we repeat again and hope to get a new one
            { Random = Randomizer.Next(1, Found_Numbers.Count()); }


     
            //Fetching all files inside of that folder
            string[] Images = System.IO.Directory.GetFiles(Art_Directory + @"Textures\Cycle_Images\" + Found_Numbers[Random]);
            

            // Resetting Variable for next usage
            Moved_Images = "";

            try
            {
                foreach (string Image in Images)
                {   // We save all images as longer string seperated by the , sign
                    Moved_Images += Path.GetFileName(Image) + ",";
                    System.IO.File.Move(Image, Art_Directory + @"Textures\" + Path.GetFileName(Image)); 
                }
            } catch { }


            // Storing the number of cycle directory so we can move all files back during next usage!
            Save_Setting(Setting, "Image_Cycle", Found_Numbers[Random]);
            Save_Setting(Setting, "Moved_Images", Moved_Images);

            Found_Numbers.Clear();

        }                    
        //=====================//

        string Get_Game_Path(string Game)
        {
            RegistryKey The_Key;

            if (Game == "EAW")
            {
                try
                {
                    The_Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LucasArts\Star Wars Empire at War\1.0");
                    if (The_Key == null) { The_Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\LucasArts\Star Wars Empire at War\1.0"); }

                    Temporal_A = The_Key.GetValue("ExePath").ToString();
                    string The_Value = Temporal_A.Remove(Temporal_A.Length - 9);

                    return The_Value; 

                } catch { Imperial_Console(700, 100, Add_Line + "    Error: could not extract Star Wars - EAW game path from Registry."
                                                  + Add_Line + @"    You have to search .\LucasArts\Star Wars Empire at War\GameData\ "
                                                  + Add_Line + @"    and to insert it manually in Settings\Game Path: EAW.");
                }
            }
           
            else if (Game == "FOC")
            {
                try
                {
                    The_Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\LucasArts\Star Wars Empire at War Forces of Corruption\1.0");
                    if (The_Key == null) { The_Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Wow6432Node\LucasArts\Star Wars Empire at War Forces of Corruption\1.0"); }

                    
                    return The_Key.GetValue("InstallPath").ToString() + @"\"; 
                }
                catch
                {
                    Imperial_Console(750, 100, Add_Line + "    Error: could not extract Star Wars - FOC game path from Registry."
                                            + Add_Line + @"    You have to search .\LucasArts\Star Wars Empire at War Forces of Corruption\ "
                                            + Add_Line + @"    and to insert it manually in Settings\Game Path: FOC.");
                }
            }

            return null;
        }

        //=====================//
        Color Load_Color(string Setting_File, string Variable)
        {
            List<int> Brush = new List<int>();
            string[] Value = Load_Setting(Setting_File, Variable).Split(',');


            foreach (string Entry in Value)
            {   int Number;
                Int32.TryParse(Entry, out Number);
                Brush.Add(Number);
            }

            // Brush 3 in front because I switched position of the A value 
            Color The_Color = Color.FromArgb(Brush[3], Brush[0], Brush[1], Brush[2]);

            return The_Color;
        }
        //=====================//    
        // To use RGB Values just write Set_Background_Color(Color.Transparent, 0, 0, 0, 190);
        // To save a color do Save_Setting("2", "Color_01", "200, 200, 200, 200");

        // Red = 240, 50, 20
        // Light Blue = 90, 180, 230
        // Dark Blue = 40, 130, 240
        // Green = 10, 245, 155  and 2, 253, 24
        // Nice Orange/Gold = 7, 0, 50
        // int[] Background_Color = { 40, 130, 240, 220 };



        void Set_Color(Color Chosen_Color, int Color_Index)
        {   
            // if (Background_Color == Color.Transparent) { Background_Color = Color.FromArgb(Brightness, Red, Blue, Green); }


            // Now depending on which color the User chose we set certain controls connected to them
            if (Color_Index == 01)
            { 
                // Defining arrays of dynamic variables for the white/black/grey Lable objects
                // Color_01: Maincolor
                dynamic[] The_Controls = 
                {
                    Label_Mod_Info,
                    Label_Faction, Label_Planet_1, Label_Planet_2, Label_Units,
                    Label_Credits, Label_Unit_Name, Label_Unit_Type,
                    Label_Is_Variant, Check_Box_Is_Variant,
                       
                    // Art Settings
                    Label_Model_Name, Label_Icon_Name, Label_Radar_Icon_Size, Label_Radar_Icon, 
                    Label_Text_Id, Label_Encyclopedia_Text, Label_Unit_Class, 
                   
                    Label_Scale_Factor, Label_Model_Height, Label_Select_Box_Scale,
                    Label_Select_Box_Height, Label_Health_Bar_Height, Check_Box_Health_Bar_Size,
                
                    // Power Values                  
                    Label_Class, Label_Hull, Label_Shield,
                    Label_Energy, Label_Speed, Label_Population,
                    Label_AI_Comabat, Label_Projectile, Label_Current_Balancing,

                    // Unit Abilities
                    Label_Ability_Type, Label_Auto_Fire, Label_Activated_GUI_Ability, 
                    Label_Expiration_Seconds, Label_Recharge_Seconds, 
                    Label_Alternate_Name, Label_Alternate_Description, Label_Alternate_Icon,
                    Label_SFX_Ability_Activated, Label_SFX_Ability_Deactivated, Lable_Lua_Script,
                    Label_Mod_Multiplier, Label_Additional_Abilities, 

                    // Unit Properties
                    Label_Is_Team, Label_Team_Name, Label_Team_Type, 
                    Label_Team_Is_Variant, Label_Shuttle_Type, 
                    Label_Team_Members, Check_Box_Team_Is_Variant,

                    Label_Is_Hero, Label_Show_Head, Label_God_Mode, Label_Use_Particle,
                    Label_Starting_Spawned_Units, Label_Reserve_Spawned_Units, Label_Death_Clone, Label_Death_Clone_Model,
                    Label_Inactive_Behavoir, Label_Active_Behavoir, 
                    Check_Box_Has_Hyperspace,
 
                    // Build Requirements
                    Label_Inactive_Affiliations, Label_Active_Affiliations, Check_Box_Add_To_Base,  
                    Label_Enable_All, Label_Build_Tab_Space, Label_Build_Cost, Label_Skirmish_Cost, 
                    Label_Build_Time, Label_Tech_Level, Label_Star_Base_LV, Label_Ground_Base_LV, 
                    Label_Innitially_Locked, Label_Required_Timeline, 
                   


                    // Game Tab
                    Lable_Language, Label_EAW_Path,              
                    Label_FOC_Path, Label_Savegame_Path, Label_Vanilla_Command_Bar,
                    Label_Dashboard_Mod, Label_Dashboard_Source_Faction,
                    Label_Dashboard_Target_Faction, Label_Mod_Declaration,

                    // Settings Tab
                    Lable_Transperency, Label_Version, Label_Special_Button_Color, Label_Imperialware_Themes,
                    Label_Folder_Path, 
                    
                    Label_Maximum_Model_Scale, Label_Maximum_Model_Height, 
                    Label_Maximum_Select_Box_Scale, Label_Maximum_Health_Bar_Height, 
                    Label_Maximum_Credits, Label_Maximum_Hull, 
                    Label_Maximum_Shield, Label_Maximum_Shield_Rate, Label_Maximum_Energy, 
                    Label_Maximum_Energy_Rate, Label_Maximum_Speed, Label_Maximum_AI_Combat, Label_Maximum_Projectile,

                    Label_Maximum_Build_Cost, Label_Maximum_Skirmish_Cost, Label_Maximum_Slice_Cost,
                    Label_Maximum_Build_Time, Label_Maximum_Tech_Level, Label_Maximum_Timeline,
                    Label_Maximum_Star_Base_LV, Label_Maximum_Ground_Base_LV, 
                    Label_Maximum_Build_Limit, Label_Maximum_Lifetime_Limit, 

                    Label_Maximum_Costum_4, Label_Maximum_Costum_5, Label_Maximum_Costum_6,
                };

                // Until the iteration cycle reached the maximum number of Items in this list
                for (int cycle = 0; cycle < The_Controls.Length; cycle++)
                {
                    // We take the array item with the current cycle numer, and change its fore color to the Layout color!
                    The_Controls[cycle].ForeColor = Chosen_Color;
                }
            }

            /*
            else if (Color_Index == 02)
            {   // Color_03: Selected              
                dynamic[] The_Controls = { Label_Amount };
            }
            */

            else if (Color_Index == 03)
            {   // Color_03: Selected              
                dynamic[] The_Controls = 
                {                
                    // Launcher Tab
                    Label_Addon_Name, Label_Mod_Name, Label_Game_Name,

                    // Manage Tab
                    Label_Cheating, Label_Start_Planet, Label_Target_Planet, 
                    Label_Planet_1_Name, Label_Planet_2_Name, Text_Box_Search_Bar, 
                    Label_Galaxy_File, Label_Credit_Sign, Label_Xml_Name, 
                    Label_Model_Alo, Label_Icon_Tga, Label_Radar_Icon_Tga, Label_Dat_Editor,
                    Label_String_Editor, Label_Art_Settings, Label_Power_Values, Label_Balancing_Info, 
                    Label_Abilities, Label_Expiration_Sec, Label_Recharge_Sec, Label_Ability_Icon, Check_Box_Use_In_Team,
                    Label_Unit_Properties, Label_Start_Units_Amount, Label_Reserve_Units_Amount, Label_Costum_Tags,
                    Label_Build_Requirements, Check_Box_Add_To_Skirmish, Check_Box_Add_To_Campaign, Label_Slice_Credits,
                    Label_Build_Credits, Label_Skirmish_Credits, Label_Seconds,
               
                    // Settings Tab
                    Label_Mod_Name_2, Label_Galactic_Alo, Label_Tactical_Alo, 
                    Label_EAW_Savegame, Label_FOC_Savegame, Label_Imperialware_Version, 
                    Label_Xml_Editor_Settings, Label_UI_Settings, Label_Maximal_Values,
                };

                // Until the iteration cycle reached the maximum number of Items in this list
                for (int cycle = 0; cycle < The_Controls.Length; cycle++)
                {
                    // We take the array item with the current cycle numer, and change its fore color to the Layout color!
                    The_Controls[cycle].ForeColor = Chosen_Color;
                }
            }


            else if (Color_Index == 04)
            {  // Color_04: Buttons
                dynamic[] The_Controls = 
                {
                    Button_Debug_Mode,

                    // Mods Tab
                    Button_Open_Map_Editor, Button_Set_Mod, Button_Start_Mod, 

                    // Apps Tab
                    Text_Box_Object_Searchbar, Button_Download_App, Button_Add_App, Button_Remove_App, 
                    Button_Select_Mods, Button_Select_Addons, Button_Start_App, 
                    Button_Load_Addon, Button_Unload_Addon, 

                    // Manage Tab              
                    Button_Expand_A, Button_Expand_B, Button_Expand_C,
                    Button_Expand_D, Button_Expand_E, Button_Expand_F, 
                    Button_Expand_G, Button_Expand_H, Button_Expand_I,
                    Button_Expand_J, Button_Expand_K,

                    Button_Collaps_A, Button_Collaps_B, Button_Collaps_C,
                    Button_Collaps_D, Button_Collaps_F, Button_Collaps_G,
                    Button_Collaps_I, Button_Collaps_J,

                    Combo_Box_Faction, Button_Destroy_Planet, Button_Select_Planet,                 
                    Button_Teleport, Combo_Box_Filter_Type,
                    Button_Add_Selected, Button_Remove_Selection, Button_Spawn,

                    Button_Edit, Button_Edit_Xml, Button_Give_Credits,
                    Text_Box_Name, Combo_Box_Type, Text_Box_Name,
                    Text_Box_Is_Variant, Text_Box_Credits, 

                    Button_Affiliation_to_Active, Button_Affiliation_Exchange, Button_Affiliation_to_Inactive, 
                    List_Box_Inactive_Affiliations, List_Box_Active_Affiliations, List_View_Requirements,  
                    Text_Box_Build_Cost, Text_Box_Skirmish_Cost, Text_Box_Build_Time,
                    Text_Box_Tech_Level, Text_Box_Star_Base_LV, Text_Box_Ground_Base_LV,
                    Text_Box_Required_Timeline, Text_Box_Slice_Cost, Text_Box_Current_Limit,
                    Text_Box_Lifetime_Limit, 
                    

                    // Xml Editing Values
                    Button_Open_Xml, Button_Create_New_Xml, Button_Search_Model,
                    Button_Default_Radar_Icon, Text_Box_Model_Name, Text_Box_Icon_Name,
                    Text_Box_Radar_Icon, Text_Box_Radar_Size, Text_Box_Text_Id,
                    Text_Box_Unit_Class, Text_Box_Encyclopedia_Text,                     
                    Text_Box_Scale_Factor, Text_Box_Model_Height, 
                    Text_Box_Health_Bar_Height, Text_Box_Select_Box_Scale, Text_Box_Select_Box_Height,
                    
                    Combo_Box_Health_Bar_Size, Combo_Box_Class, 
                    Text_Box_Hull, Text_Box_Shield, Text_Box_Shield_Rate, 
                    Text_Box_Energy, Text_Box_Energy_Rate,                    
                    Text_Box_Speed, Text_Box_Rate_Of_Turn, Text_Box_Bank_Turn_Angle,
                    Text_Box_AI_Combat, Text_Box_Population,                    
                    Text_Box_Projectile, 

                    // Unit Abilities
                    Combo_Box_Ability_Type, Combo_Box_Activated_GUI_Ability, Combo_Box_Mod_Multiplier, Combo_Box_Additional_Abilities,
                    Text_Box_Expiration_Seconds, Text_Box_Recharge_Seconds, 
                    Text_Box_Alternate_Name, Text_Box_Alternate_Description, Text_Box_Ability_Icon, 
                    Text_Box_SFX_Ability_Activated, Text_Box_SFX_Ability_Deactivated, Text_Box_Lua_Script,
                    Text_Box_Mod_Multiplier, Text_Box_Additional_Abilities, 
                    
                    // Unit Properties
                    Text_Box_Name, Text_Box_Team_Is_Variant, Text_Box_Shuttle_Type,
                    Text_Box_Team_Amount, Text_Box_Team_Offsets, Text_Box_Team_Members,
                    Combo_Box_Team_Type, 

                    Text_Box_Hyperspace_Speed, Text_Box_Starting_Unit_Name, Text_Box_Spawned_Unit,
                    Text_Box_Reserve_Unit_Name, Text_Box_Reserve_Unit, Text_Box_Death_Clone, Text_Box_Death_Clone_Model,
                    List_Box_Inactive_Behavoir, List_Box_Active_Behavoir,

                    // Build Requirements
                    Button_Move_to_Active, Button_Behavoir_Exchange, Button_Move_to_Inactive, 

                    Button_Save_Names, Button_Reset_Tag_Names, 
                    Text_Box_Costum_Tag_1, Text_Box_Costum_Tag_2, Text_Box_Costum_Tag_3,
                    Text_Box_Costum_Tag_4, Text_Box_Costum_Tag_5, Text_Box_Costum_Tag_6,
                    Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As,

                    // Edit Xml Tab
                    Text_Box_Xml_Search_Bar, Button_Load_Xml, Button_Add_Xml_Tag,
                    Button_Remove_Xml_Tag, Button_Remove_Xml_Tag, Button_Save_Xml_As,
                    Button_Undo_Change, Button_Redo_Change, Button_Save_Xml, 

                    // Game Tab
                    Combo_Box_Language, 
                    Text_Box_EAW_Path, Button_Open_EAW_Path, Button_Reset_EAW_Path, 
                    Text_Box_FOC_Path, Button_Open_FOC_Path, Button_Reset_FOC_Path,
                    Text_Box_Savegame_Path, Button_Open_Savegame_Path, Button_Reset_Savegame_Path,
                    Button_Commandbar_Color_Blue, Button_Reset_Vanilla_Commandbar, 
                    Combo_Box_Dashboard_Mod, Combo_Box_Target_Faction, Button_Restore_Dashboards,
                    Button_Apply_Dashboard,

                    // Settings Tab
                    Button_Add_Scepter_Sway, Text_Box_Color_Selection, 
                    Button_Color_1, Button_Color_2, Button_Color_3,
                    Button_Color_4, Button_Color_5, Button_Color_6,
                    Button_Choose_Theme, Button_Set_Background, Button_Cycle_Background,
                    Text_Box_Folder_Path, Button_Set_Folder_Path, 
                    Button_Open_Folder_Path, Button_Reset_Folder_Path,

                    // Maximal Values
                    Text_Box_Max_Model_Scale, Text_Box_Max_Model_Height, 
                    Text_Box_Max_Select_Box_Scale, Text_Box_Max_Health_Bar_Height,

                    Button_Restore_Default_Values, Text_Box_Max_Credits, Text_Box_Max_Hull,
                    Text_Box_Max_Shield, Text_Box_Max_Shield_Rate, Text_Box_Max_Energy,
                    Text_Box_Max_Energy_Rate, Text_Box_Max_Speed, Text_Box_Max_AI_Combat, Text_Box_Max_Projectile,

                    Text_Box_Max_Build_Cost, Text_Box_Max_Skirmish_Cost, Text_Box_Max_Slice_Cost,
                    Text_Box_Max_Build_Time, Text_Box_Max_Tech_Level, Text_Box_Max_Timeline, 
                    Text_Box_Max_Star_Base_LV, Text_Box_Max_Ground_Base_LV, 
                    Text_Box_Max_Build_Limit, Text_Box_Max_Lifetime_Limit,

                    Text_Box_Max_Costum_4, Text_Box_Max_Costum_5, Text_Box_Max_Costum_6,
                    Button_Reset_All_Settings, Button_Withdraw_Files, Button_Uninstall_Imperialware  
                };


                // Until the iteration cycle reached the maximum number of Items in this list
                for (int cycle = 0; cycle < The_Controls.Length; cycle++)
                {
                    // We take the array item with the current cycle numer, and change its fore color to the Layout color!
                    The_Controls[cycle].ForeColor = Chosen_Color;
                }
            }

            else if (Color_Index == 05)
            {
                // Changing Color of the Background Window
                // Graphics The_Graphic = this.CreateGraphics();
                // The_Graphic.FillRectangle(new SolidBrush(Chosen_Color), this.ClientRectangle);
                this.BackColor = Chosen_Color;


                Brush Background_Brush = new System.Drawing.SolidBrush(Chosen_Color);
                // Setting Background Color of selected Tabs
                // Tab_Control_01.SelectedTab.BackColor = Chosen_Color;
                // Tab_Control_01.TabPages[1].BackColor = Chosen_Color;
                // Tab_Control_01.TabPages[2].BackColor = Chosen_Color;

                
                // Color_05: User Interface Background of all tab Pages
                dynamic[] The_Controls = 
                {
                    Tab_Control_01.TabPages[1], Tab_Control_01.TabPages[2], Tab_Control_01.TabPages[3], 
                    Tab_Control_01.TabPages[4], Tab_Control_01.TabPages[5], Tab_Control_01.TabPages[6],
                    Track_Bar_Mod_Image_Size, Track_Bar_Xml_Values,
                };

                // Until the iteration cycle reached the maximum number of Items in this list
                for (int cycle = 0; cycle < The_Controls.Length; cycle++)
                {
                    // We take the array item with the current cycle numer, and change its fore color to the Layout color!
                    The_Controls[cycle].BackColor = Chosen_Color;
                }
            }

            else if (Color_Index == 06)
            {   // Color_06: Costum Xml Tags
                dynamic[] The_Controls = 
                {
                    Text_Box_Tag_1_Name, Text_Box_Tag_2_Name, Text_Box_Tag_3_Name,
                    Text_Box_Tag_4_Name, Text_Box_Tag_5_Name, Text_Box_Tag_6_Name,
                };
            

                // Adjusting the Backcolor according to the Parent object of these Textboxes
                Text_Box_Tag_1_Name.BackColor = Group_Box_Costum_Tags.BackColor;
                Text_Box_Tag_2_Name.BackColor = Group_Box_Costum_Tags.BackColor;
                Text_Box_Tag_3_Name.BackColor = Group_Box_Costum_Tags.BackColor;
                Text_Box_Tag_4_Name.BackColor = Group_Box_Costum_Tags.BackColor;
                Text_Box_Tag_5_Name.BackColor = Group_Box_Costum_Tags.BackColor;
                Text_Box_Tag_6_Name.BackColor = Group_Box_Costum_Tags.BackColor;

                // Until the iteration cycle reached the maximum number of Items in this list
                for (int cycle = 0; cycle < The_Controls.Length; cycle++)
                {
                    // We take the array item with the current cycle numer, and change its fore color to the Layout color!
                    The_Controls[cycle].ForeColor = Chosen_Color;
                }
            }
                       
        }



        // This covers all UNchecked Radio Buttons and Checkboxes !
        void Set_Checkbox_Color(Color Background_Color, bool Is_Checked)
        {
            // Defining a array of dynamic variables for the white/black/grey Lable objects
            dynamic[] Colored_Lables = 
            {
                // Mods Tab
                Button_Select_EAW, Button_Select_FOC, Check_Box_No_Mod,            

                // Manage Tab
                Radio_Button_Planet_1, Radio_Button_Planet_2, Radio_Button_Planet_3, Radio_Button_Planet_4,               
                Check_Box_Is_Variant, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, // Label_Amount, 
                Check_Box_Slice_Cost, Check_Box_Current_Limit, Check_Box_Lifetime_Limit,

                // Game Tab
                Check_Box_Use_Language, Check_Box_Evade_Language, Check_Box_Windowed_Mode, Check_Box_Mod_Savegame_Directory,

                // Settings Tab
                Check_Box_Copy_Editor_Art, Check_Box_Xml_Checkmate, Check_Box_Set_Theme,
                Check_Box_Close_After_Launch, Check_Box_Debug_Mode, Check_Box_Copy_Editor_Art,
                Check_Box_Xml_Checkmate, Check_Box_Load_Rescent, Check_Box_Load_Issues, Check_Box_Load_Tag_Issues, 
                Check_Box_Add_Core_Code, Check_Box_Set_Theme, Check_Box_Set_Color, Check_Box_Cycle_Mod_Image, 
                Check_Box_Show_Mod_Button, Check_Box_Show_Addon_Button,
            };

            // Until the iteration cycle reached the maximum number of Items in this list
            for (int cycle = 0; cycle < Colored_Lables.Length; cycle++)
            {
                // We take the array item with the current cycle numer, and change its fore color to the Layout color!

                if (Is_Checked) 
                {   
                    if (Colored_Lables[cycle].Checked) { Colored_Lables[cycle].ForeColor = Background_Color; }
                }              
                else 
                {   
                    if (!Colored_Lables[cycle].Checked) { Colored_Lables[cycle].ForeColor = Background_Color; }
                }

            }
        }

   

        void Make_Backgrounds_Transparent()
        {
            dynamic[] Colored_Lables = 
            {
                // Color 01
                Label_Faction, Label_Planet_1, Label_Planet_2, Label_Units,
                Label_Credits, Label_Unit_Name, Label_Unit_Type,
                Label_Is_Variant, 
                       
                // Art Settings
                Label_Model_Name, Label_Icon_Name, Label_Radar_Icon_Size, Label_Radar_Icon, 
                Label_Text_Id, Label_Encyclopedia_Text, Label_Unit_Class, 

                Label_Scale_Factor, Label_Model_Height, Label_Select_Box_Scale,
                Label_Select_Box_Height, Label_Health_Bar_Height, Check_Box_Health_Bar_Size,                
                Button_Operator_Model_Height, Button_Operator_Select_Box_Height,
                
                // Power Values
                Label_Class, Label_Hull, Label_Shield,
                Label_Energy, Label_Speed, Button_Operator_Population, 
                Label_Population, Label_AI_Comabat, Label_Projectile,
                Label_Current_Balancing,

                // Unit Abilities
                Label_Ability_Type, Label_Auto_Fire, Label_Activated_GUI_Ability, 
                Label_Expiration_Seconds, Label_Recharge_Seconds, 
                Label_Alternate_Name, Label_Alternate_Description, Label_Alternate_Icon,
                Label_SFX_Ability_Activated, Label_SFX_Ability_Deactivated, Lable_Lua_Script,
                Label_Mod_Multiplier, Label_Additional_Abilities,
                Switch_Button_Auto_Fire, Button_Add_Ability,
                 
                // Unit Properties
                Label_Is_Team, Label_Team_Name, Label_Team_Type, 
                Label_Team_Is_Variant, Label_Shuttle_Type, 
                Label_Team_Members, Check_Box_Team_Is_Variant,

                Label_Is_Hero, Label_Show_Head, Label_God_Mode, Label_Use_Particle,
                Label_Starting_Spawned_Units, Label_Reserve_Spawned_Units, Label_Death_Clone, Label_Death_Clone_Model,
                Label_Inactive_Behavoir, Label_Active_Behavoir, 
                Check_Box_Has_Hyperspace,

                Switch_Button_Is_Team, Switch_Button_Is_Hero, Switch_Button_Show_Head, Switch_Button_God_Mode, Switch_Button_Use_Particle,

                // Build Requirements
                Label_Inactive_Affiliations, Label_Active_Affiliations, Check_Box_Add_To_Base,  
                Label_Enable_All, Label_Build_Tab_Space, Label_Build_Cost, Label_Skirmish_Cost, 
                Label_Build_Time, Label_Tech_Level, Label_Star_Base_LV, Label_Ground_Base_LV, 

                Switch_Button_Enable_All, Switch_Button_Build_Tab, Switch_Button_Innitially_Locked, 
                Button_Required_Planets, Button_Required_Structures,
                Label_Innitially_Locked, Label_Required_Timeline, Check_Box_Slice_Cost, 
                Check_Box_Current_Limit, Check_Box_Lifetime_Limit,



                // Game Tab
                Lable_Language, Label_EAW_Path,              
                Label_FOC_Path, Label_Savegame_Path, Label_Vanilla_Command_Bar,
                Label_Dashboard_Mod, Label_Dashboard_Source_Faction,
                Label_Dashboard_Target_Faction, Label_Mod_Declaration,

                // Settings Tab
                Lable_Transperency, Label_Version, Label_Special_Button_Color, Label_Imperialware_Themes,
                Label_Folder_Path, 

                Label_Maximum_Model_Scale, Label_Maximum_Model_Height, 
                Label_Maximum_Select_Box_Scale, Label_Maximum_Health_Bar_Height, 
                Label_Maximum_Credits, Label_Maximum_Hull, 
                Label_Maximum_Shield, Label_Maximum_Shield_Rate, Label_Maximum_Energy, 
                Label_Maximum_Energy_Rate, Label_Maximum_Speed, Label_Maximum_AI_Combat, Label_Maximum_Projectile,
                            
                Label_Maximum_Build_Cost, Label_Maximum_Skirmish_Cost, Label_Maximum_Slice_Cost,
                Label_Maximum_Build_Time, Label_Maximum_Tech_Level, Label_Maximum_Timeline,
                Label_Maximum_Star_Base_LV, Label_Maximum_Ground_Base_LV, 
                Label_Maximum_Build_Limit, Label_Maximum_Lifetime_Limit, 
                Label_Maximum_Costum_4, Label_Maximum_Costum_5, Label_Maximum_Costum_6,  

                // Color 03, Launcher Tab
                Label_Addon_Name, Label_Mod_Name, Label_Game_Name,

                // Manage Tab
                Mini_Button_Save, Mini_Button_Save_As, Mini_Button_Copy_Instance, Mini_Button_Copy_Unit, Mini_Button_Copy_Team, Label_Cheating, Label_Start_Planet, Label_Target_Planet, 
                Label_Galaxy_File, Label_Credit_Sign, Label_Xml_Name, Button_Open_Variant, Button_Open_Team_Variant,
                Label_Model_Alo, Label_Icon_Tga, Label_Radar_Icon_Tga, Label_Dat_Editor,
                Label_String_Editor, Label_Art_Settings, Label_Power_Values, Label_Balancing_Info,
                Label_Abilities, Label_Expiration_Sec, Label_Recharge_Sec, Label_Ability_Icon, Check_Box_Use_In_Team,
                Label_Unit_Properties, Label_Start_Units_Amount, Label_Reserve_Units_Amount, Label_Costum_Tags,
                Label_Build_Requirements, Check_Box_Add_To_Skirmish, Check_Box_Add_To_Campaign, Label_Slice_Credits,
                Label_Build_Credits, Label_Skirmish_Credits, Label_Seconds,
                
                // Settings Tab
                Label_Mod_Name_2, Label_Galactic_Alo, Label_Tactical_Alo, 
                Label_EAW_Savegame, Label_FOC_Savegame, Label_Imperialware_Version, 
                Label_Xml_Editor_Settings, Label_UI_Settings, Label_Maximal_Values,



                // Color 04
                Label_Planet_1_Name, Label_Planet_2_Name,      


                // Segment Buttons
                Button_Refresh_Galaxy, Button_Refresh_Planets, 
                Button_Teleport_Planets, Button_Teleport_Both, Button_Teleport_Units,
                Button_Use_Game_Language, Button_Use_Mod_Language, 
                Button_EAW_Command_Bar, Button_FOC_Command_Bar, 
                Button_Primary_Ability, Button_Secondary_Ability, 
                Button_Color_Switch_1, Button_Color_Switch_7,
                Button_Class_1, Button_Class_5, Button_Class_6, Button_Class_10,

                 
                // Other things
                Mod_Picture, Picture_Box_Dashboard_Preview, Picture_Box_Color_Picker, 
                Button_Launch_Mod, Button_Mod_Only, Button_Launch_Addon, 

            };

            // Applying translucent Background color to all Text Boxes, this is important because Background color changes with themes
            foreach (var Item in Colored_Lables) { try { Item.BackColor = Color.Transparent; } catch { } }

        }


        void Adjust_Text_Fonts(int Font_Scale_Value)
        {          
            dynamic[] Text_Items = 
            {
                // Color 01
                Label_Faction, Label_Planet_1, Label_Planet_2, Label_Units,
                Label_Credits, Label_Unit_Name, Label_Unit_Type,
                Label_Is_Variant, 
                       
                // Art Settings
                Label_Model_Name, Label_Icon_Name, Label_Radar_Icon_Size, Label_Radar_Icon, 
                Label_Text_Id, Label_Encyclopedia_Text, Label_Unit_Class, 

                Label_Scale_Factor, Label_Model_Height, Label_Select_Box_Scale,
                Label_Select_Box_Height, Label_Health_Bar_Height, Check_Box_Health_Bar_Size,
                
                // Power Values
                Label_Class, Label_Hull, Label_Shield,
                Label_Energy, Label_Speed, Label_Population,
                Label_AI_Comabat, Label_Projectile, Label_Current_Balancing,

                // Unit Abilities
                Label_Ability_Type, Label_Auto_Fire, Label_Activated_GUI_Ability, 
                Label_Expiration_Seconds, Label_Recharge_Seconds, 
                Label_Alternate_Name, Label_Alternate_Description, Label_Alternate_Icon,
                Label_SFX_Ability_Activated, Label_SFX_Ability_Deactivated, Lable_Lua_Script,
                Label_Mod_Multiplier, Label_Additional_Abilities,

                // Unit Properties
                Label_Is_Team, Label_Team_Name, Label_Team_Type, 
                Label_Team_Is_Variant, Label_Shuttle_Type, 
                Label_Team_Members, Check_Box_Team_Is_Variant,

                Label_Is_Hero, Label_Show_Head, Label_God_Mode, Label_Use_Particle,
                Label_Starting_Spawned_Units, Label_Reserve_Spawned_Units, Label_Death_Clone, Label_Death_Clone_Model,
                Label_Inactive_Behavoir, Label_Active_Behavoir, 
                Check_Box_Has_Hyperspace,

                // Build Requirements
                Label_Inactive_Affiliations, Label_Active_Affiliations, Check_Box_Add_To_Base,  
                Label_Enable_All, Label_Build_Tab_Space, Label_Build_Cost, Label_Skirmish_Cost, 
                Label_Build_Time, Label_Tech_Level, Label_Star_Base_LV, Label_Ground_Base_LV,

                Label_Innitially_Locked, Label_Required_Timeline, Check_Box_Slice_Cost, 
                Check_Box_Current_Limit, Check_Box_Lifetime_Limit,


                // Game Tab
                Lable_Language, Label_EAW_Path,              
                Label_FOC_Path, Label_Savegame_Path, Label_Vanilla_Command_Bar,
                Label_Dashboard_Mod, Label_Dashboard_Source_Faction,
                Label_Dashboard_Target_Faction, Label_Mod_Declaration,

                // Settings Tab
                Lable_Transperency, Label_Version, Label_Special_Button_Color, Label_Imperialware_Themes,
                Label_Folder_Path, 
                
                Label_Maximum_Model_Scale, Label_Maximum_Model_Height, 
                Label_Maximum_Select_Box_Scale, Label_Maximum_Health_Bar_Height, 
                Label_Maximum_Credits, Label_Maximum_Hull, 
                Label_Maximum_Shield, Label_Maximum_Shield_Rate, Label_Maximum_Energy, 
                Label_Maximum_Energy_Rate, Label_Maximum_Speed, Label_Maximum_AI_Combat, Label_Maximum_Projectile,

                Label_Maximum_Build_Cost, Label_Maximum_Skirmish_Cost, Label_Maximum_Slice_Cost,
                Label_Maximum_Build_Time, Label_Maximum_Tech_Level, Label_Maximum_Timeline,
                Label_Maximum_Star_Base_LV, Label_Maximum_Ground_Base_LV, 
                Label_Maximum_Build_Limit, Label_Maximum_Lifetime_Limit, 

                Label_Maximum_Costum_4, Label_Maximum_Costum_5, Label_Maximum_Costum_6,


                // Color 03, Launcher Tab
                Label_Addon_Name, Label_Mod_Name, Label_Game_Name,

                // Manage Tab
                Label_Cheating, Label_Start_Planet, Label_Target_Planet, 
                Label_Galaxy_File, Label_Credit_Sign, Label_Xml_Name, 
                Label_Model_Alo, Label_Icon_Tga, Label_Radar_Icon_Tga, Label_Dat_Editor,
                Label_String_Editor, Label_Art_Settings, Label_Power_Values, Label_Balancing_Info,
               
                Label_Abilities, Label_Expiration_Sec, Label_Recharge_Sec, Label_Ability_Icon, Check_Box_Use_In_Team,
                Label_Unit_Properties, Label_Start_Units_Amount, Label_Reserve_Units_Amount, Label_Costum_Tags,
                Label_Build_Requirements, Check_Box_Add_To_Skirmish, Check_Box_Add_To_Campaign, Label_Slice_Credits,
                Label_Build_Credits, Label_Skirmish_Credits, Label_Seconds,
                
                // Settings Tab
                Label_Mod_Name_2, Label_Galactic_Alo, Label_Tactical_Alo, 
                Label_EAW_Savegame, Label_FOC_Savegame, Label_Imperialware_Version, 
                Label_Xml_Editor_Settings, Label_UI_Settings, Label_Maximal_Values,



                // Color 04
                Label_Planet_1_Name, Label_Planet_2_Name,      


                // Segment Buttons
                Button_Refresh_Galaxy, Button_Refresh_Planets, 
                Button_Teleport_Planets, Button_Teleport_Both, Button_Teleport_Units,
                Button_Use_Game_Language, Button_Use_Mod_Language, 
                Button_EAW_Command_Bar, Button_FOC_Command_Bar,
                
                Button_Primary_Ability, Button_Secondary_Ability, 

                Button_Color_Switch_1, Button_Color_Switch_2, Button_Color_Switch_3, 
                Button_Color_Switch_4, Button_Color_Switch_5, Button_Color_Switch_6, Button_Color_Switch_7, 

                Button_Class_1,Button_Class_2, Button_Class_3, Button_Class_4, Button_Class_5, 
                Button_Class_6, Button_Class_7, Button_Class_8, Button_Class_9, Button_Class_10,
            

                // Other things
                Mod_Picture, Picture_Box_Dashboard_Preview, Picture_Box_Color_Picker, 
                Button_Launch_Mod, Button_Mod_Only, Button_Launch_Addon,

               
                //======= CHECK BOXES =======
                // Mods Tab
                Button_Select_EAW, Button_Select_FOC, Check_Box_No_Mod,            

                // Manage Tab
                Radio_Button_Planet_1, Radio_Button_Planet_2, Radio_Button_Planet_3, Radio_Button_Planet_4, 
                Label_Amount, Check_Box_Is_Variant, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data,

                // Game Tab
                Check_Box_Use_Language, Check_Box_Evade_Language, Check_Box_Windowed_Mode, Check_Box_Mod_Savegame_Directory,

                // Settings Tab
                Check_Box_Copy_Editor_Art, Check_Box_Xml_Checkmate, Check_Box_Set_Theme,
                Check_Box_Close_After_Launch, Check_Box_Debug_Mode, Check_Box_Copy_Editor_Art,
                Check_Box_Xml_Checkmate, Check_Box_Load_Rescent, Check_Box_Load_Issues,
                Check_Box_Load_Tag_Issues, Check_Box_Add_Core_Code, Check_Box_Set_Theme, Check_Box_Set_Color, Check_Box_Cycle_Mod_Image, 
                Check_Box_Show_Mod_Button, Check_Box_Show_Addon_Button,   
          

                //======= BUTTON TEXTS =======

                Button_Debug_Mode,

                // Mods Tab
                Button_Open_Map_Editor, Button_Set_Mod, Button_Start_Mod, 

                // Apps Tab
                Text_Box_Object_Searchbar, Button_Download_App, Button_Add_App, Button_Remove_App, 
                Button_Select_Mods, Button_Select_Addons, Button_Start_App, 
                Button_Load_Addon, Button_Unload_Addon, 

                // Manage Tab              
                Button_Expand_A, Button_Expand_B, Button_Expand_C,
                Button_Expand_D, Button_Expand_E, Button_Expand_F,
                Button_Expand_G, Button_Expand_H, Button_Expand_I,
                Button_Expand_J, Button_Expand_K,
               
                Button_Collaps_A, Button_Collaps_B, Button_Collaps_C,
                Button_Collaps_D, Button_Collaps_F, Button_Collaps_G,
                Button_Collaps_I, Button_Collaps_J,

                Combo_Box_Faction, Button_Destroy_Planet, Button_Select_Planet,                 
                Button_Teleport, Text_Box_Search_Bar, Combo_Box_Filter_Type,
                Button_Add_Selected, Button_Remove_Selection, Button_Spawn,

                Button_Edit, Button_Edit_Xml, Button_Give_Credits,
                Text_Box_Name, Combo_Box_Type, Text_Box_Name,
                Text_Box_Is_Variant, Text_Box_Credits,  

                Button_Affiliation_to_Active, Button_Affiliation_Exchange, Button_Affiliation_to_Inactive, 
                List_Box_Inactive_Affiliations, List_Box_Active_Affiliations, List_View_Requirements,  
                Text_Box_Build_Cost, Text_Box_Skirmish_Cost, Text_Box_Build_Time,
                Text_Box_Tech_Level, Text_Box_Star_Base_LV, Text_Box_Ground_Base_LV,
                Text_Box_Required_Timeline, Text_Box_Slice_Cost, Text_Box_Current_Limit,
                Text_Box_Lifetime_Limit, 

                // Xml Editing Values
                Button_Open_Xml, Button_Create_New_Xml, Button_Search_Model,
                Button_Default_Radar_Icon, Text_Box_Model_Name, Text_Box_Icon_Name,
                Text_Box_Radar_Icon, Text_Box_Radar_Size, Text_Box_Text_Id,
                Text_Box_Unit_Class, Text_Box_Encyclopedia_Text,                
                Text_Box_Scale_Factor, Text_Box_Model_Height, 
                Text_Box_Health_Bar_Height, Text_Box_Select_Box_Scale, Text_Box_Select_Box_Height,
                    
                Combo_Box_Health_Bar_Size, Combo_Box_Class, 
                Text_Box_Hull, Text_Box_Shield, Text_Box_Shield_Rate, 
                Text_Box_Energy, Text_Box_Energy_Rate, 
                Text_Box_Speed, Text_Box_Rate_Of_Turn, Text_Box_Bank_Turn_Angle,
                Text_Box_AI_Combat, Text_Box_Population,    
                Text_Box_Projectile, 

                // Unit Abilities
                Combo_Box_Ability_Type, Combo_Box_Activated_GUI_Ability, Combo_Box_Mod_Multiplier, Combo_Box_Additional_Abilities,
                Text_Box_Expiration_Seconds, Text_Box_Recharge_Seconds, 
                Text_Box_Alternate_Name, Text_Box_Alternate_Description, Text_Box_Ability_Icon, 
                Text_Box_SFX_Ability_Activated, Text_Box_SFX_Ability_Deactivated, Text_Box_Lua_Script,
                Text_Box_Mod_Multiplier, Text_Box_Additional_Abilities, 
                            
                // Unit Properties
                Text_Box_Name, Text_Box_Team_Is_Variant, Text_Box_Shuttle_Type,
                Text_Box_Team_Amount, Text_Box_Team_Offsets, Text_Box_Team_Members,
                Combo_Box_Team_Type, 

                Text_Box_Hyperspace_Speed, Text_Box_Starting_Unit_Name, Text_Box_Spawned_Unit,
                Text_Box_Reserve_Unit_Name, Text_Box_Reserve_Unit, Text_Box_Death_Clone, Text_Box_Death_Clone_Model,
                List_Box_Inactive_Behavoir, List_Box_Active_Behavoir,
                Button_Move_to_Active, Button_Behavoir_Exchange, Button_Move_to_Inactive, 

                // Build Requirements
                Button_Save_Names, Button_Reset_Tag_Names, 
                Text_Box_Costum_Tag_1, Text_Box_Costum_Tag_2, Text_Box_Costum_Tag_3,
                Text_Box_Costum_Tag_4, Text_Box_Costum_Tag_5, Text_Box_Costum_Tag_6,
                Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As,

                // Edit Xml Tab
                Text_Box_Xml_Search_Bar, Button_Load_Xml, Button_Add_Xml_Tag,
                Button_Remove_Xml_Tag, Button_Remove_Xml_Tag, Button_Save_Xml_As,
                Button_Undo_Change, Button_Redo_Change, Button_Save_Xml,

                // Game Tab
                Combo_Box_Language, 
                Text_Box_EAW_Path, Button_Open_EAW_Path, Button_Reset_EAW_Path, 
                Text_Box_FOC_Path, Button_Open_FOC_Path, Button_Reset_FOC_Path,
                Text_Box_Savegame_Path, Button_Open_Savegame_Path, Button_Reset_Savegame_Path,
                Button_Commandbar_Color_Blue, Button_Reset_Vanilla_Commandbar, 
                Combo_Box_Dashboard_Mod, Combo_Box_Target_Faction, Button_Restore_Dashboards,
                Button_Apply_Dashboard,

                // Settings Tab
                Button_Add_Scepter_Sway, Text_Box_Color_Selection, 
                Button_Color_1, Button_Color_2, Button_Color_3,
                Button_Color_4, Button_Color_5, Button_Color_6,
                Button_Choose_Theme, Button_Set_Background, Button_Cycle_Background,
                Text_Box_Folder_Path, Button_Set_Folder_Path, 
                Button_Open_Folder_Path, Button_Reset_Folder_Path,

                // Maximal Values
                Text_Box_Max_Model_Scale, Text_Box_Max_Model_Height, 
                Text_Box_Max_Select_Box_Scale, Text_Box_Max_Health_Bar_Height,
                Button_Restore_Default_Values, Text_Box_Max_Credits, Text_Box_Max_Hull,             
                Text_Box_Max_Shield, Text_Box_Max_Shield_Rate, Text_Box_Max_Energy,
                Text_Box_Max_Energy_Rate, Text_Box_Max_Speed, Text_Box_Max_AI_Combat, Text_Box_Max_Projectile,            

                Text_Box_Max_Build_Cost, Text_Box_Max_Skirmish_Cost, Text_Box_Max_Slice_Cost,
                Text_Box_Max_Build_Time, Text_Box_Max_Tech_Level, Text_Box_Max_Timeline, 
                Text_Box_Max_Star_Base_LV, Text_Box_Max_Ground_Base_LV, 
                Text_Box_Max_Build_Limit, Text_Box_Max_Lifetime_Limit, 

                Text_Box_Max_Costum_4, Text_Box_Max_Costum_5, Text_Box_Max_Costum_6,
                Button_Reset_All_Settings, Button_Withdraw_Files, Button_Uninstall_Imperialware,


                //======= RTF COSTUM XML TAGS =======
                Text_Box_Tag_1_Name, Text_Box_Tag_2_Name, Text_Box_Tag_3_Name,
                Text_Box_Tag_4_Name, Text_Box_Tag_5_Name, Text_Box_Tag_6_Name,
            };
          
            foreach (var Item in Text_Items) 
            {
                // We devide the old font size by Font_Scale_Value (which is 120) and multiply with 100 to get back at 100% as usual
                Item.Font = new Font("Georgia", (Item.Font.Size / Font_Scale_Value) * 100, FontStyle.Regular);
            }  

        }

        //=====================//

        void Change_Button_Color()
        {
            dynamic[] All_Buttons = 
            {
                Button_Debug_Mode,

                // Mods Tab
                Button_Open_Map_Editor, Button_Set_Mod, Button_Start_Mod, 

                // Apps Tab
                Button_Download_App, Button_Add_App, Button_Remove_App, 
                Button_Select_Mods, Button_Select_Addons, Button_Start_App, 
                Button_Load_Addon, Button_Unload_Addon, 

                // Manage Tab              
                Button_Expand_A, Button_Expand_B, Button_Expand_C,
                Button_Expand_D, Button_Expand_E, Button_Expand_F,
                Button_Expand_G, Button_Expand_H, Button_Expand_I,
                Button_Expand_J, Button_Expand_K,

                Button_Collaps_A, Button_Collaps_B, Button_Collaps_C,
                Button_Collaps_D, Button_Collaps_F, Button_Collaps_G,
                Button_Collaps_I, Button_Collaps_J,

                Button_Destroy_Planet, Button_Select_Planet,                 
                Button_Teleport, Button_Add_Selected, Button_Remove_Selection, Button_Spawn,

                Button_Edit, Button_Edit_Xml, Button_Give_Credits,
                Button_Affiliation_to_Active, Button_Affiliation_Exchange, Button_Affiliation_to_Inactive, 
                                             

                // Xml Editing Values
                Button_Open_Xml, Button_Create_New_Xml, Button_Search_Model,
                Button_Default_Radar_Icon,                  
                Button_Move_to_Active, Button_Behavoir_Exchange, Button_Move_to_Inactive, 

                Button_Save_Names, Button_Reset_Tag_Names,                 
                Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As,

                // Edit Xml Tab
                Button_Load_Xml, Button_Add_Xml_Tag, Button_Toggle_Value,
                Button_Remove_Xml_Tag, Button_Remove_Xml_Tag, Button_Save_Xml_As,
                Button_Save_Xml, 

                // Game Tab
                Button_Open_EAW_Path, Button_Reset_EAW_Path, 
                Button_Open_FOC_Path, Button_Reset_FOC_Path,
                Button_Open_Savegame_Path, Button_Reset_Savegame_Path,
                Button_Commandbar_Color_Blue, Button_Reset_Vanilla_Commandbar, 
                Button_Restore_Dashboards, Button_Apply_Dashboard,

                // Settings Tab
                Button_Add_Scepter_Sway,
                Button_Color_1, Button_Color_2, Button_Color_3,
                Button_Color_4, Button_Color_5, Button_Color_6,
                Button_Choose_Theme, Button_Set_Background, Button_Cycle_Background,
                Button_Set_Folder_Path, Button_Open_Folder_Path, Button_Reset_Folder_Path,
                              
                // Maximal Values
                Button_Restore_Default_Values, Button_Reset_All_Settings, Button_Withdraw_Files, Button_Uninstall_Imperialware
            };


            foreach (var Button in All_Buttons)
            {   // Setting a translucent (gray and gradinent) button image over the Background Color                                                      
               
                if (File.Exists(Selected_Theme + @"Buttons\Button.png")) { Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button.png", Button.Size.Width, Button.Size.Height); }
                else { Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button.png", Button.Size.Width, Button.Size.Height); }

                if (Button != Button_Reset_All_Settings & Button != Button_Withdraw_Files & Button != Button_Uninstall_Imperialware) { Button.BackColor = Color_02; }
                Button.ForeColor = Color_04;

                // Registering a hover and leave event for each button, they exchange button Images in order to highlight/unhilight them! 
                Button.MouseHover += new EventHandler(Highlight_Button);
                Button.MouseLeave += new EventHandler(Unhighlight_Button);
            }



            //========  Dealing with special Buttons
            dynamic[] Search_Buttons = 
            {   // Search Buttons need a own button
                Button_Search_Object, Button_Search_Unit, Button_Search_Ability, Button_Xml_Search
            };
       
            foreach (var Button in Search_Buttons)
            {               
                if (File.Exists(Selected_Theme + @"Buttons\Button_Search.png")) { Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_Search.png", Button.Size.Width - 3, Button.Size.Height - 3); }
                else { Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_Search.png", Button.Size.Width -3, Button.Size.Width -3); }

                Button.BackColor = Color_02;
                Button.ForeColor = Color_04;
                Button.MouseHover += new EventHandler(Highlight_Button);
                Button.MouseLeave += new EventHandler(Unhighlight_Button);
            }


            if (File.Exists(Selected_Theme + @"Buttons\Button_Undo.png")) { Button_Undo_Change.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_Undo.png", Button_Undo_Change.Size.Width - 4, Button_Undo_Change.Size.Height - 4); }
            else { Button_Undo_Change.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_Undo.png", Button_Undo_Change.Size.Width - 4, Button_Undo_Change.Size.Height - 4); }

            Button_Undo_Change.BackColor = Color_02;
            Button_Undo_Change.ForeColor = Color_04;
            Button_Undo_Change.MouseHover += new EventHandler(Highlight_Button);
            Button_Undo_Change.MouseLeave += new EventHandler(Unhighlight_Button);


            if (File.Exists(Selected_Theme + @"Buttons\Button_Redo.png")) { Button_Redo_Change.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_Redo.png", Button_Redo_Change.Size.Width - 4, Button_Undo_Change.Size.Height - 4); }
            else { Button_Redo_Change.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_Redo.png", Button_Redo_Change.Size.Width - 4, Button_Redo_Change.Size.Height - 4); }

            Button_Redo_Change.BackColor = Color_02;
            Button_Redo_Change.ForeColor = Color_04;
            Button_Redo_Change.MouseHover += new EventHandler(Highlight_Button);
            Button_Redo_Change.MouseLeave += new EventHandler(Unhighlight_Button);


            if (File.Exists(Selected_Theme + @"Buttons\Button_Arrow.png")) { Button_Open_Variant.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_Arrow.png", Button_Open_Variant.Size.Width, Button_Open_Variant.Size.Height); }
            else { Button_Open_Variant.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_Arrow.png", Button_Open_Variant.Size.Width, Button_Open_Variant.Size.Height); }


            Mini_Button_Copy_Instance_MouseLeave(null, null);
            Mini_Button_Copy_Unit_MouseLeave(null, null);
            Mini_Button_Copy_Team_MouseLeave(null, null);
            Mini_Button_Save_MouseLeave(null, null);
            Mini_Button_Save_As_MouseLeave(null, null);         
        }
    
        //=====================//

        // Requires using System.Windows.Forms;
        /* <summary>
        (In/De)creases a height of the «control» and the window «form», and moves accordingly
        down or up an elements in the «move_list». To decrease size you have to give a negative
        argument in the «the_sz».
        Usually used to collapse (or expand) an elements of a form, and to move the controls of
        the «move_list» down to fill an appeared gap.
        </summary>
        <param name="control">control to collapse/expand</param>
        <param name="form">form which would be resized accordingly after size of a control
        changed (give «null» if you don't want to)</param>
        <param name="move_list">A list of a controls that should also be moved up or down to
        «the_sz» size (e.g. to fill a gap after the «control» collapsed)</param>
        <param name="the_sz">size to change the control, form, and the «move_list»</param>
        */


        // Expander Box Function
        public static void ToggleControlY(Control control, Form Expander_Object, List<Control> Move_List, int The_Size)
        {
            //→ Change Size of control
            control.Height += The_Size;
            //→ Change sz of Wind
            if (Expander_Object != null)
                Expander_Object.Height += The_Size;
            //*** We leaved a gap(or intersected with another controls) now!
            //→ So, move up/down a list of a controls                  

            try
            {   foreach (Control Item in Move_List)
                {   Point Location = Item.Location;

                    Location.Y += The_Size;
                    Item.Location = Location;
                }            
           } catch { }

        } 

        //=====================//


        public static Image Resize_Image(string Image_Path, string Image_Name, int new_width, int new_height)
        {   try 
            {   Bitmap image = new Bitmap(Image_Path + Image_Name);

                Bitmap new_image = new Bitmap(new_width, new_height);
                Graphics Picture = Graphics.FromImage((Image)new_image);
                Picture.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                Picture.DrawImage(image, 0, 0, new_width, new_height);
                return new_image;
            } catch { }
            return null;
        }

        
        public static Image Text_Image(string Image_Path, string Image_Name, int new_width, int new_height, int X_Position, int Y_Position, int Font_Size, Color Used_Color, string The_Text)
        {   try 
            {   Bitmap image = new Bitmap(Image_Path + Image_Name);

                
                Bitmap new_image = new Bitmap(new_width, new_height);
                Graphics Picture = Graphics.FromImage((Image)new_image);
                Picture.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;
                Picture.DrawImage(image, 0, 0, new_width, new_height);

                Brush myBrush = new System.Drawing.SolidBrush(Used_Color);
               
                // This part draws a text over the picture
                using (Font myFont = new Font("Georgia", Font_Size))
                {
                    Picture.DrawString(The_Text, myFont, myBrush, new Point(X_Position, Y_Position));
                }

                return new_image;
            } catch { }
            return null;
        }


        // This only processes a new aspect ratio
        public static Size Get_Aspect_Ratio(Size Source, int Max_Width, int Max_Height)
        {
            decimal rnd = Math.Min(Max_Width / (decimal)Source.Width, Max_Height / (decimal)Source.Height);
            return new Size((int)Math.Round(Source.Width * rnd), (int)Math.Round(Source.Height * rnd));
        }

       

        void Adjust_Wallpaper (string Image_Path)
        {         
            // Turning the selected path into a Bitmap, then getting its sizes
            Bitmap Selected_Image = new Bitmap(Image_Path);
            Size Source = Selected_Image.Size;
            // -20 and -70 to compensate for program borders
            int Max_Width = this.Size.Width - 20;
            int Max_Height = this.Size.Height - 70;

            // Processing a new size according to the window size, in oder to adjust the background image to it 
            decimal rnd = Math.Max(Max_Width / (decimal)Source.Width, Max_Height / (decimal)Source.Height);
            Size New_Ratio = new Size((int)Math.Round(Source.Width * rnd), (int)Math.Round(Source.Height * rnd));

            // Setting Image size
            Background_Wallpaper.Size = New_Ratio;
            // Resizing the image to the new size
            Background_Wallpaper.Image = Resize_Image(Path.GetDirectoryName(Image_Path) + @"\", Path.GetFileName(Image_Path), New_Ratio.Width, New_Ratio.Height);               
        }

        //===========================//
        void Refresh_Theme_List()
        {
            List_View_Theme_Selection.Items.Clear();

            // If the Mods directory was found 
            if (Directory.Exists(Program_Directory + "Themes"))
            {
                // We put all found directories inside of our target folder into a string table
                string[] File_Paths = Directory.GetDirectories(Program_Directory + "Themes");
                int FileCount = File_Paths.Count();

                // Cycling up from 0 to the total count of folders found above in this directory;
                for (int Cycle_Count = 0; Cycle_Count < FileCount; Cycle_Count = Cycle_Count + 1)
                {
                    // Getting the Name only from all folder paths, Cycle_Count increases by 1 in each cycle 
                    string List_Value = Path.GetFileName(File_Paths[Cycle_Count]);


                    // Inserting that folder name into the List View box                    
                    var Item = List_View_Theme_Selection.Items.Add(List_Value); ;

                    Item.ForeColor = Color.Black;

                    // Every second item should have this value in order to create a checkmate pattern with good contrast
                    if (Checkmate_Color == "true" & Item.Index % 2 == 0)
                    {
                        Item.ForeColor = Color_03;
                        Item.BackColor = Color_07;
                    }

                }
            }

            else // Probably the path is wrong
            {
                List_View_Theme_Selection.Items.Add(Program_Directory + "Themes");
                List_View_Theme_Selection.Items.Add("Seems to not exist.");
            }
        }


        void Change_Theme(string Selected_Theme, bool Save_Background)
        {
            try
            {   // We are going to set that file as our background Image
                Save_Setting("2", "Background_Image_Path", @"""" + Selected_Theme + @"""");

                if (Save_Background) { Save_Setting("2", "Selected_Theme", @"""" + Selected_Theme + @""""); }
             
                Save_Setting(Setting, "Background_Image_A", "Background.jpg");

                Save_Setting(Setting, "Background_Image_B", "Background_Small.jpg");
                                              
                // Background_Wallpaper.Image = Image.FromFile(Selected_Theme + @"\Background.jpg");
                Adjust_Wallpaper(Selected_Theme + @"Background.jpg");

                Background_Image_Path = Selected_Theme;

                if (File.Exists(Selected_Theme + @"Buttons\Color_Picker.png")) { Picture_Box_Color_Picker.Image = Resize_Image(Selected_Theme + @"Buttons\", "Color_Picker.png", 320, 320); }
                else { Picture_Box_Color_Picker.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Color_Picker.png", 320, 320); }



                string Setting_File = Selected_Theme + "Colors.txt"; 

                if (File.Exists(Setting_File) & Auto_Color == "true")
                {   // Loading Colors
                    try { Color_01 = Load_Color(Setting_File, "Color_01"); } catch { Color_01 = Color.FromArgb(135, 206, 250, 255); } // Main Color
                    try { Color_02 = Load_Color(Setting_File, "Color_02"); } catch { Color_02 = Color.FromArgb(40, 130, 240, 220); } // Unselect

                    try { Color_03 = Load_Color(Setting_File, "Color_03"); } catch { Color_03 = Color.FromArgb(200, 200, 200, 200); } // Select
                    try { Color_04 = Load_Color(Setting_File, "Color_04"); } catch { Color_04 = Color.FromArgb(0, 0, 0, 255); } // Button Color

                    try { Color_05 = Load_Color(Setting_File, "Color_05"); } catch { Color_05 = Color.FromArgb(64, 64, 64, 240); } // UI Color
                    try { Color_06 = Load_Color(Setting_File, "Color_06"); } catch { Color_06 = Color.FromArgb(147, 112, 219, 255); } // Costum Tag Color
                    try { Color_07 = Load_Color(Setting_File, "Color_07"); } catch { Color_07 = Color.FromArgb(120, 120, 120, 255); } // Checker Color 


                    // Setting Colors: 
                    try { Set_Color(Color_01, 01); } catch { }
                    try { Set_Color(Color_02, 02); } catch { }

                    // The Checkboxes have their own function because of the checked/unchecked state
                    try { Set_Checkbox_Color(Color_02, false); } catch { }                  
                    try { Set_Checkbox_Color(Color_03, true); } catch { }

                    try { Set_Color(Color_03, 03); } catch { }
                    try { Set_Color(Color_04, 04); } catch { }

                    try { Set_Color(Color_05, 05); } catch { }
                    try { Set_Color(Color_06, 06); } catch { }


                    // Saving Colors: 
                    string Current_Color = Color_01.R.ToString() + ", " + Color_01.G.ToString() + ", " + Color_01.B.ToString() + ", " + Color_01.A.ToString();
                    Save_Setting("2", "Color_01", Current_Color);

                    Current_Color = Color_02.R.ToString() + ", " + Color_02.G.ToString() + ", " + Color_02.B.ToString() + ", " + Color_02.A.ToString();
                    Save_Setting("2", "Color_02", Current_Color);

                    Current_Color = Color_03.R.ToString() + ", " + Color_03.G.ToString() + ", " + Color_03.B.ToString() + ", " + Color_03.A.ToString();
                    Save_Setting("2", "Color_03", Current_Color);

                    Current_Color = Color_04.R.ToString() + ", " + Color_04.G.ToString() + ", " + Color_04.B.ToString() + ", " + Color_04.A.ToString();
                    Save_Setting("2", "Color_04", Current_Color);

                    Current_Color = Color_05.R.ToString() + ", " + Color_05.G.ToString() + ", " + Color_05.B.ToString() + ", " + Color_05.A.ToString();
                    Save_Setting("2", "Color_05", Current_Color);

                    Current_Color = Color_06.R.ToString() + ", " + Color_06.G.ToString() + ", " + Color_06.B.ToString() + ", " + Color_06.A.ToString();
                    Save_Setting("2", "Color_06", Current_Color);

                    Current_Color = Color_07.R.ToString() + ", " + Color_07.G.ToString() + ", " + Color_07.B.ToString() + ", " + Color_07.A.ToString();
                    Save_Setting("2", "Color_07", Current_Color);


                  

                    // Saving "Button_Color" after loading it from "Setting_File" inside of the selected Theme
                    try { Save_Setting(Setting, "Button_Color", Load_Setting(Setting_File, "Button_Color")); } catch {}
                    
                    // If not specified we try to get the Buttons inside of the current Theme
                    if (Button_Color == "") { Save_Setting(Setting, "Button_Color", List_View_Theme_Selection.SelectedItems[0].Text); }
                    Set_All_Switch_Buttons();

                    // Adjusting the new colors for the segment buttons
                    Set_Color_Segment_Buttons();
                    Set_All_Segment_Buttons();

                }

                Change_Button_Color();

                // Adjusting the new colors for launch buttons
                Set_Launch_Button(Button_Launch_Mod, "Button_Launch.png", "Button_Launch.png", 34, "Launch", 158);
                Button_Launch_Mod.Parent = Background_Wallpaper;

                Set_Launch_Button(Button_Mod_Only, "Button_Mod.png", "Button_Launch.png", 20, "Mod Only", 100);
                Button_Mod_Only.Parent = Background_Wallpaper;

                Set_Launch_Button(Button_Launch_Addon, "Button_Addon.png", "Button_Launch.png", 8, "Addon + Mod", 100);
                Button_Launch_Addon.Parent = Background_Wallpaper;

            }
            catch
            {
                if (Debug_Mode == "true")
                {Imperial_Console(600, 100, "     Could not find the selected Theme in" + Add_Line + "     " + Program_Directory + @"Themes\" + Add_Line + "     please try it again.");}
            }
        
            // Showing the right tab, so the user gets the connection
            Tab_Control_01.SelectedIndex = 0;

        }

        //===========================//

        void Set_Launch_Button(PictureBox The_Button, string Selected_Button, string Alternative_Button, int Text_Distance, string Button_Name, int Button_Size)
        {             
            // This button has different size so it needs other treatment
            if (The_Button == Button_Launch_Mod)
            {              
                if (File.Exists(Selected_Theme + @"Buttons\" + Selected_Button))
                {
                    The_Button.Image = Text_Image(Selected_Theme + @"Buttons\", Selected_Button, Button_Size, Button_Size, Text_Distance, 60, 18, Color_03, Button_Name);
                }  // Otherwise we esacpe to Imperialware's default one
                else { The_Button.Image = Text_Image(Program_Directory + @"Themes\Default\Buttons\", Selected_Button, Button_Size, Button_Size, Text_Distance, 60, 18, Color_03, Button_Name); }
                return;
            }


            // This enables us to use a single source image if the other 2 are missing (because optioinal)
            if (File.Exists(Selected_Theme + @"Buttons\" + Selected_Button)) { The_Button.Image = Text_Image(Selected_Theme + @"Buttons\", Selected_Button, Button_Size, Button_Size, Text_Distance, 40, 10, Color_03, Button_Name); }
            else
            {
                if (File.Exists(Selected_Theme + @"Buttons\" + Alternative_Button))
                {// Otherwise we can alternatively use the Launch button for all 3 buttons
                    The_Button.Image = Text_Image(Selected_Theme + @"Buttons\", Alternative_Button, 100, 100, Text_Distance, 40, 10, Color_03, Button_Name);
                }
                else
                {// If the selected theme has no button at all we will use the standard ones from Imperialware's black theme            
                    The_Button.Image = Text_Image(Program_Directory + @"Themes\Default\Buttons\", Selected_Button, Button_Size, Button_Size, Text_Distance, 40, 10, Color_03, Button_Name);
                }               
            }                 
        }


        //===========================//
        // Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, true);

        void Toggle_Button(PictureBox The_Button, string Active_Button, string Inactive_Button, int Scale_Treshold, bool Is_On)
        {
            string Source_Directory = @"C:\Program Files\Imperialware\Themes\" + Button_Color + @"\";

            if (Is_On)
            {
                if (File.Exists(Source_Directory + @"Buttons\" + Active_Button + ".png"))
                { The_Button.BackgroundImage = Resize_Image(Source_Directory + @"Buttons\", Active_Button + ".png", The_Button.Size.Width + Scale_Treshold, The_Button.Size.Height + Scale_Treshold); }
                else try { The_Button.BackgroundImage = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", Active_Button + ".png", The_Button.Size.Width + Scale_Treshold, The_Button.Size.Height + Scale_Treshold); } catch { }
            }
            else
            {
                if (File.Exists(Source_Directory + @"Buttons\" + Inactive_Button + ".png"))
                { The_Button.BackgroundImage = Resize_Image(Source_Directory + @"Buttons\", Inactive_Button + ".png", The_Button.Size.Width + Scale_Treshold, The_Button.Size.Height + Scale_Treshold); }
                else try { The_Button.BackgroundImage = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", Inactive_Button + ".png", The_Button.Size.Width + Scale_Treshold, The_Button.Size.Height + Scale_Treshold); } catch { }
            }
        }          
        //===========================//

        void Set_All_Switch_Buttons()
        {
            if (Toggle_Enable_All) { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, true); }
            else { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false); }

            if (Toggle_Build_Tab) { Toggle_Button(Switch_Button_Build_Tab, "Button_On", "Button_Off", 0, true); }
            else { Toggle_Button(Switch_Button_Build_Tab, "Button_On", "Button_Off", 0, false); }

            if (Toggle_Innitially_Locked) { Toggle_Button(Switch_Button_Innitially_Locked, "Button_On", "Button_Off", 0, true); }
            else { Toggle_Button(Switch_Button_Innitially_Locked, "Button_On", "Button_Off", 0, false); }

            if (Is_Team) { Toggle_Button(Switch_Button_Is_Team, "Button_On", "Button_Off", 0, true); }
            else { Toggle_Button(Switch_Button_Is_Team, "Button_On", "Button_Off", 0, false); }

            if (Toggle_Is_Hero) { Toggle_Button(Switch_Button_Is_Hero, "Button_On", "Button_Off", 0, true); }
            else { Toggle_Button(Switch_Button_Is_Hero, "Button_On", "Button_Off", 0, false); }

            if (Toggle_Show_Head) { Toggle_Button(Switch_Button_Show_Head, "Button_On", "Button_Off", 0, true); }
            else { Toggle_Button(Switch_Button_Show_Head, "Button_On", "Button_Off", 0, false); }

            if (Toggle_God_Mode) { Toggle_Button(Switch_Button_God_Mode, "Button_On", "Button_Off", 0, true); }
            else { Toggle_Button(Switch_Button_God_Mode, "Button_On", "Button_Off", 0, false); }         

            if (Toggle_Use_Particle) { Toggle_Button(Switch_Button_Use_Particle, "Button_On", "Button_Off", 0, true); }
            else { Toggle_Button(Switch_Button_Use_Particle, "Button_On", "Button_Off", 0, false); }

            if (Toggle_Auto_Fire) { Toggle_Button(Switch_Button_Auto_Fire, "Button_On", "Button_Off", 0, true); }
            else { Toggle_Button(Switch_Button_Auto_Fire, "Button_On", "Button_Off", 0, false); }        
        }          
        //===========================//

        
        void Render_Segment_Button(PictureBox The_Button, string Button_Type, bool Is_Highlighted, int New_Width, int New_Height, int X_Position, int Y_Position, int Font_Size, Color Selected_Color, string The_Text)
        {
            string Button_Mode = "";
            string Source_Directory = @"C:\Program Files\Imperialware\Themes\" + Button_Color + @"\";

            try 
            {   // Need the Button Mode to decide whether to take Highlighted or normal version
                if (Is_Highlighted) { Button_Mode = "_Highlighted.png"; } else { Button_Mode = ".png"; }

                if (File.Exists(Source_Directory + @"Buttons\Segment_Button_" + Button_Type + Button_Mode))
                { The_Button.Image = Text_Image(Source_Directory + @"Buttons\", "Segment_Button_" + Button_Type + Button_Mode, New_Width, New_Height, X_Position, Y_Position, Font_Size, Selected_Color, The_Text); }
                // Otherwise we set the one from the default Theme
                else { The_Button.Image = Text_Image(Program_Directory + @"Themes\Default\Buttons\", "Segment_Button_" + Button_Type + Button_Mode, New_Width, New_Height, X_Position, Y_Position, Font_Size, Selected_Color, The_Text); }               
            }
            catch
            {
                if (Is_Highlighted) { Button_Mode = "_Highlighted.jpg"; } else { Button_Mode = ".jpg"; }

                if (File.Exists(Source_Directory + @"Buttons\Segment_Button_" + Button_Type + Button_Mode))
                { The_Button.Image = Text_Image(Source_Directory + @"Buttons\", "Segment_Button_" + Button_Type + Button_Mode, New_Width, New_Height, X_Position, Y_Position, Font_Size, Selected_Color, The_Text); }
                // Otherwise we set the one from the default Theme
                else { The_Button.Image = Text_Image(Program_Directory + @"Themes\Default\Buttons\", "Segment_Button_" + Button_Type + Button_Mode, New_Width, New_Height, X_Position, Y_Position, Font_Size, Selected_Color, The_Text); }                        
            }
        }

        //===========================//
        void Set_All_Segment_Buttons()
        {
            // NOTE: The Function Make_Backgrounds_Translucent() setts all background button Colors to Color.Transparent

            // Setting "Planet_Switch" Segment Button
            if (Planet_Switch == "Galaxy")
            {
                Render_Segment_Button(Button_Refresh_Galaxy, "Left", true, 82, 34, 17, 8, 10, Color_03, "Galaxy");
                Render_Segment_Button(Button_Refresh_Planets, "Right", false, 82, 34, 17, 8, 10, Color_04, "Planets");
            }
            else if (Planet_Switch != "Galaxy")
            {
                Render_Segment_Button(Button_Refresh_Galaxy, "Left", false, 82, 34, 17, 8, 10, Color_04, "Galaxy");
                Render_Segment_Button(Button_Refresh_Planets, "Right", true, 82, 34, 17, 8, 10, Color_03, "Planets");
            }


            // Setting "Teleportation_Mode" Segment Button  
            if (Teleportation_Mode == "Planets")
            {   // Choosing Teleport Method
                Teleport_from_Planet = "Teleport_from_Planet = true";

                Render_Segment_Button(Button_Teleport_Planets, "Left", true, 82, 34, 12, 6, 13, Color_03, "Planets");
                Render_Segment_Button(Button_Teleport_Both, "Middle", false, 82, 34, 20, 6, 13, Color_04, "Both");
                Render_Segment_Button(Button_Teleport_Units, "Right", false, 82, 34, 15, 6, 13, Color_04, "Units");
            }
            else if (Teleportation_Mode == "Planets_and_Units")
            {
                Teleport_Units_on_Planet = "Teleport_Both = true";

                Render_Segment_Button(Button_Teleport_Planets, "Left", false, 82, 34, 12, 6, 13, Color_04, "Planets");
                Render_Segment_Button(Button_Teleport_Both, "Middle", true, 82, 34, 20, 6, 13, Color_03, "Both");
                Render_Segment_Button(Button_Teleport_Units, "Right", false, 82, 34, 15, 6, 13, Color_04, "Units");
            }
            else if (Teleportation_Mode == "Units")
            {
                Teleport_Units = "Teleport_Units = true";

                Render_Segment_Button(Button_Teleport_Planets, "Left", false, 82, 34, 12, 6, 13, Color_04, "Planets");
                Render_Segment_Button(Button_Teleport_Both, "Middle", false, 82, 34, 20, 6, 13, Color_04, "Both");
                Render_Segment_Button(Button_Teleport_Units, "Right", true, 82, 34, 15, 6, 13, Color_03, "Units");
            }



            // Setting "Mod Language" Segment Button
            if (Language_Mode == "Game")
            {
                Render_Segment_Button(Button_Use_Game_Language, "Left", true, 85, 36, 6, 8, 10, Color_03, "From Game");
                Render_Segment_Button(Button_Use_Mod_Language, "Right", false, 85, 36, 6, 8, 10, Color_04, "From Mod");
            }
            else if (Language_Mode == "Mod")
            {
                Render_Segment_Button(Button_Use_Game_Language, "Left", false, 85, 36, 6, 8, 10, Color_04, "From Game");
                Render_Segment_Button(Button_Use_Mod_Language, "Right", true, 85, 36, 6, 8, 10, Color_03, "From Mod");
            }
            else if (Language_Mode == "false")
            {
                Render_Segment_Button(Button_Use_Game_Language, "Left", false, 85, 36, 6, 8, 10, Color_04, "From Game");
                Render_Segment_Button(Button_Use_Mod_Language, "Right", false, 85, 36, 6, 8, 10, Color_04, "From Mod");
            }


            // Setting "Vanilla Command Bar" Segment Button: This one always starts as EAW so we dont care to store it in settings
            Render_Segment_Button(Button_EAW_Command_Bar, "Left", true, 85, 36, 22, 8, 12, Color_03, "EAW");
            Render_Segment_Button(Button_FOC_Command_Bar, "Right", false, 85, 36, 22, 8, 12, Color_04, "FOC");


            // Calling from function because this code is used multiple times
            Set_Class_Button();


            // As Selected_Ability and Switch_Required_Object innitiate with the value 1 on startup, so the first one will fire        
            if (Selected_Ability == 1) { Render_Segment_Button(Button_Primary_Ability, "Left", true, 85, 36, 11, 8, 12, Color_03, "Primary"); }
            else { Render_Segment_Button(Button_Primary_Ability, "Left", false, 85, 36, 18, 9, 10, Color_04, "Primary"); }

            if (Selected_Ability == 2) { Render_Segment_Button(Button_Secondary_Ability, "Right", true, 85, 36, 2, 8, 12, Color_03, "Secondary"); }
            else { Render_Segment_Button(Button_Secondary_Ability, "Right", false, 85, 36, 6, 9, 10, Color_04, "Secondary"); }


            if (Switch_Required_Object == 1) { Render_Segment_Button(Button_Required_Planets, "Left", true, 85, 36, 13, 8, 12, Color_03, "Planets"); }
            else { Render_Segment_Button(Button_Required_Planets, "Left", false, 85, 36, 20, 9, 10, Color_04, "Planets"); }

            if (Switch_Required_Object == 2) { Render_Segment_Button(Button_Required_Structures, "Right", true, 85, 36, 1, 8, 12, Color_03, "Structures"); }
            else { Render_Segment_Button(Button_Required_Structures, "Right", false, 85, 36, 9, 8, 10, Color_04, "Structures"); }
           
        }


        //===========================//
        void Set_Class_Button()
        {
            if (Maximal_Value_Class == "1") { Render_Segment_Button(Button_Class_1, "Left", true, 82, 34, 11, 6, 13, Color_03, "Fighter"); }
            else { Render_Segment_Button(Button_Class_1, "Left", false, 82, 34, 11, 6, 13, Color_04, "Fighter"); }

            if (Maximal_Value_Class == "2") { Render_Segment_Button(Button_Class_2, "Middle", true, 82, 34, 9, 6, 13, Color_03, "Bomber"); }
            else { Render_Segment_Button(Button_Class_2, "Middle", false, 82, 34, 9, 6, 13, Color_04, "Bomber"); }

            if (Maximal_Value_Class == "3") { Render_Segment_Button(Button_Class_3, "Middle", true, 82, 34, 8, 6, 13, Color_03, "Corvette"); }
            else { Render_Segment_Button(Button_Class_3, "Middle", false, 82, 34, 8, 6, 13, Color_04, "Corvette"); }

            if (Maximal_Value_Class == "4") { Render_Segment_Button(Button_Class_4, "Middle", true, 82, 34, 12, 6, 13, Color_03, "Frigate"); }
            else { Render_Segment_Button(Button_Class_4, "Middle", false, 82, 34, 12, 6, 13, Color_04, "Frigate"); }

            if (Maximal_Value_Class == "5") { Render_Segment_Button(Button_Class_5, "Right", true, 82, 34, 10, 6, 13, Color_03, "Capital"); }
            else { Render_Segment_Button(Button_Class_5, "Right", false, 82, 34, 10, 6, 13, Color_04, "Capital"); }

            if (Maximal_Value_Class == "6") { Render_Segment_Button(Button_Class_6, "Left", true, 82, 34, 8, 6, 13, Color_03, "Infantry"); }
            else { Render_Segment_Button(Button_Class_6, "Left", false, 82, 34, 8, 6, 13, Color_04, "Infantry"); }

            if (Maximal_Value_Class == "7") { Render_Segment_Button(Button_Class_7, "Middle", true, 82, 34, 11, 6, 13, Color_03, "Vehicle"); }
            else { Render_Segment_Button(Button_Class_7, "Middle", false, 82, 34, 11, 6, 13, Color_04, "Vehicle"); }

            if (Maximal_Value_Class == "8") { Render_Segment_Button(Button_Class_8, "Middle", true, 82, 34, 27, 6, 13, Color_03, "Air"); }
            else { Render_Segment_Button(Button_Class_8, "Middle", false, 82, 34, 27, 6, 13, Color_04, "Air"); }

            if (Maximal_Value_Class == "9") { Render_Segment_Button(Button_Class_9, "Middle", true, 82, 34, 20, 6, 13, Color_03, "Hero"); }
            else { Render_Segment_Button(Button_Class_9, "Middle", false, 82, 34, 20, 6, 13, Color_04, "Hero"); }

            if (Maximal_Value_Class == "10") { Render_Segment_Button(Button_Class_10, "Right", true, 82, 34, 1, 6, 13, Color_03, "Structure"); }
            else { Render_Segment_Button(Button_Class_10, "Right", false, 82, 34, 1, 6, 13, Color_04, "Structure"); }

        }

        //===========================//
        void Set_Color_Segment_Buttons()
        {           
            if (Button_Color == "Red") 
            { Render_Segment_Button(Button_Color_Switch_1, "Left", true, 60, 34, 14, 6, 13, Color.White, "Red"); }
            else { Render_Segment_Button(Button_Color_Switch_1, "Left", false, 60, 34, 14, 6, 13, Color.Black, "Red"); }
              
            if (Button_Color == "Ice")
            { Render_Segment_Button(Button_Color_Switch_2, "Middle", true, 60, 34, 11, 6, 13, Color.White, "Blue"); }
            else { Render_Segment_Button(Button_Color_Switch_2, "Middle", false, 60, 34, 11, 6, 13, Color.Black, "Blue"); }
                
            if (Button_Color == "Green_Matrix")
            { Render_Segment_Button(Button_Color_Switch_3, "Middle", true, 60, 34, 6, 6, 13, Color.White, "Green"); }
            else { Render_Segment_Button(Button_Color_Switch_3, "Middle", false, 60, 34, 6, 6, 13, Color.Black, "Green"); }
                
            if (Button_Color == "Gold")
            { Render_Segment_Button(Button_Color_Switch_4, "Middle", true, 60, 34, 3, 6, 13, Color.White, "Yellow"); }
            else { Render_Segment_Button(Button_Color_Switch_4, "Middle", false, 60, 34, 3, 6, 13, Color.Black, "Yellow"); }
                
            if (Button_Color == "Pink")
            { Render_Segment_Button(Button_Color_Switch_5, "Middle", true, 60, 34, 10, 6, 13, Color.White, "Pink"); }
            else { Render_Segment_Button(Button_Color_Switch_5, "Middle", false, 60, 34, 10, 6, 13, Color.Black, "Pink"); }
                
            if (Button_Color == "Stargate")
            { Render_Segment_Button(Button_Color_Switch_6, "Middle", true, 60, 34, 9, 6, 13, Color.White, "Cyan"); }
            else { Render_Segment_Button(Button_Color_Switch_6, "Middle", false, 60, 34, 9, 6, 13, Color.Black, "Cyan"); }
                
            if (Button_Color == "Metal")
            { Render_Segment_Button(Button_Color_Switch_7, "Right", true, 60, 34, 6, 6, 13, Color.White, "Silver"); }
            else { Render_Segment_Button(Button_Color_Switch_7, "Right", false, 60, 34, 6, 6, 13, Color.Black, "Silver"); }
        }

        //===========================//
        List<string> Get_Mod_Info_Directories()
        {   // Innitiating string list 
            List<string> Found_Objects = new List<string>();


            // Refreshing Values
            Selected_Object = Load_Setting(Setting, "Selected_Object");
            Info_Directory_Path = Program_Directory + Selected_Object + @"\";



            // If a data directory of this Mod/Addon is found in Imperialwares Directory
            if (Directory.Exists(Info_Directory_Path))
            {
                // We put all found directories inside of our target folder into a string table
                string[] File_Paths = Directory.GetDirectories(Info_Directory_Path);
                int Folder_Count = File_Paths.Count();


                // Cycling up from 0 to the total count of folders found above in this directory;
                for (int Cycle_Count = 0; Cycle_Count < Folder_Count; Cycle_Count = Cycle_Count + 1)
                {
                    // Getting the Name only from all folder paths, Cycle_Count increases by 1 in each cycle 
                    string File_Name = Path.GetFileName(File_Paths[Cycle_Count]);
                    // And inserting that folder name into the List
                    Found_Objects.Add(File_Name);
                }
            }

            return Found_Objects;
        }


        //=====================//
   
        void Refresh_Apps()
        {
            // Getting rid of the whole site with old row style
            Table_Layout_Panel_Appstore.Controls.Clear();

            string Object_Path = "";
            string Image_Name = "01.png";
            string Image_Text = "";
            int Cycle_Count = 0;


            foreach (string Item in Get_Mod_Info_Directories())
            {
                // Ignoring the Template Directory
                if (Item != "Mod_Template") 
                {


                    // Exception for some mods with own images, there it will search their Addon directory
                    string Exception_List = "Wrackage_and_Bodies_Stay_Mod";
                    if (Exception_List.Contains(Item) & Selected_Object == "Addons") { Object_Path = Info_Directory_Path + Item + @"\Images\"; }

                   

                    else if (Selected_Object == "Addons")
                    {
                        // Gathering Mod and Game Information of the selected Addon    
                        string Info_File = Program_Directory + @"Addons\" + Item + @"\Info.txt";                       
                        string Host_Mod = Load_Setting(Info_File, "Mod");

                        // There are currently no Addons for TPC, so we choose Stargate - EAW
                        if (Host_Mod == "Stargate") { Host_Mod = "Stargate_Empire_at_War"; }

                        // Setting image path according to the mod inside of the Info.txt file
                        Object_Path = Program_Directory + @"Mods\" + Host_Mod + @"\Images\";
                    }
                    else { Object_Path = Info_Directory_Path + Item + @"\Images\"; }
                             


                    Image_Name = "01.png";
                    // If no .jpg was found we switch to .png
                    if (!File.Exists(Object_Path + Image_Name)) { Image_Name = "01.jpg"; }

                    // Otherwise we chose a black background image with directory name in white text
                    if (!File.Exists(Object_Path + Image_Name))
                    {   Object_Path = Program_Directory + @"Images\";
                        Image_Name = "Plain_Mod_Image.jpg";
                        Image_Text = Item;
                    }

                    // Showing name of the Stargate Addons
                    //if (Item.Contains("Stargate")) { Image_Text = Item; }



                    // Add a new Control button with the following specifications
                    var New_Button = new Button()
                    {
                        Name = Item,
                        Text = Image_Text,
                        ForeColor = Color.White,
                        // BackColor = Color_02,
                        Width = 120,
                        Height = 120,
                        Image = Resize_Image(Object_Path, Image_Name, 120, 120),
                    };


                    New_Button.Click += new EventHandler(App_Image_Click);

                    // The last 2 values specify button position in column and row, we need to place new buttons at the very end after the first row was filled with 4 buttons.
                    if (Cycle_Count < 5) { Table_Layout_Panel_Appstore.Controls.Add(New_Button, 0, 0); }
                    else { Table_Layout_Panel_Appstore.Controls.Add(New_Button, Table_Layout_Panel_Appstore.ColumnCount, 0); }


                    Image_Text = "";
                    Cycle_Count++;
                }
            }

        }

        

        //=====================//
        void Remove_Apps()
        {  
           Table_Layout_Panel_Appstore.Controls.Clear();
           Table_Layout_Panel_Appstore.ColumnCount = 4;
        }

        //=====================//
        void Load_Last_Galaxy()
        { 
         try {// Turning Selection button to unpressable state
                Picture_Box_Select_Planet.Visible = true;
                Button_Select_Planet.Visible = false;              

                // Removing all listed items from the List Box in order to refresh
                List_Box_Galaxy.Items.Clear();

                // Splitting all entries from this long string
                string[] bits = Selected_Galaxy.Split(',');

                foreach (string bit in bits)
                {   // And listing it in the Galaxy List Box
                    List_Box_Galaxy.Items.Add(bit);
                }             
         }
         catch { Imperial_Console(600, 100, Add_Line + "    There is no default mod set, please choose a Mod in the Mods tab."); }
        }


        //=====================//
        void Load_Last_Planets()
        {
            try
            {// Turning Selection button back to pressable state
                Picture_Box_Select_Planet.Visible = false;
                Button_Select_Planet.Visible = true;

                // Removing all listed items from the List Box in order to refresh
                List_Box_Galaxy.Items.Clear();

                // Splitting all entries from this long string (loaded from .txt file at the top of "Main_Window.cs"
                string[] Planets = Found_Planets.Split(',');

                foreach (string Planet in Planets)
                {   // And listing it in the Galaxy List Box
                    List_Box_Galaxy.Items.Add(Planet);
                }

                if (Label_Galaxy_File.Text == "Galactic Conquest") { Label_Galaxy_File.Text = Planet_Switch; }
            }
            catch { Imperial_Console(600, 100, Add_Line + "    There is no default mod set, please choose a Mod in the Mods tab."); }
        }

        //=====================//
        void Load_Recent_Units()
        {   try
            {   // Removing all listed items from the List Box in order to refresh
                List_Box_All_Instances.Items.Clear();

                // Splitting all entries from this long string
                string[] Units = Selected_Units.Split(',');

                foreach (string Unit in Units)
                {   if (Unit != "")
                    {   // And listing it in the Galaxy List Box
                        List_Box_All_Instances.Items.Add(Unit);
                    }
                }
            } catch {}
        }

        //=====================//


        public List<string> Get_Xmls()
        {
            // Removing all listed items from the listbox in order to refresh
            List_Box_Xmls.Items.Clear();

            List<string> All_Xmls = new List<string>();
          

            // Trying it because if the player still has no mod setted it would cause an exception:
            try 
            {
                // If the Xml directory was found 
                if (Directory.Exists(Xml_Directory))
                {             
                    // We are going to store the found xmls including their path in this list, so the program can access it for other purposes
                    foreach (string The_File in Directory.GetFiles(Xml_Directory))
                    { if (The_File.EndsWith(".XML") | The_File.EndsWith(".xml")) { All_Xmls.Add(The_File); } }             
            
                    int File_Count = All_Xmls.Count();

                     // Cycling up from 0 to the total count of files found above in this directory;
                    for (int Cycle_Count = 0; Cycle_Count < File_Count; Cycle_Count = Cycle_Count + 1)
                    {
                        // Getting the Name only of all files, Cycle_Count increases by 1 in each cycle 
                        string List_Value = Path.GetFileName(All_Xmls[Cycle_Count]);
                        // And inserting that file name into the Listbox
                        List_Box_Xmls.Items.Add(List_Value);
                    }
                }
                else
                { Imperial_Console(620, 100, Add_Line + "    No Mod directory found, please set a Mod in the Mods tab."
                                           + Add_Line + "    Or the wrong Game checkbox is selected, then you could"
                                           + Add_Line + "    try selecting the other Game.");
                }
            } catch {  }
                                  

            return All_Xmls;
        }
        //=====================//

        void Check_Cheat_Dummy()
        {
            if (!Directory.Exists(Xml_Directory + @"Core")) { Directory.CreateDirectory(Xml_Directory + @"Core"); }


            string Cheating_Dummy = Xml_Directory + @"Core\Cheating_Dummy.xml";

            // If no Cheating Dummy file exists, we copy it and make sure it is registered in the GameObjectFiles.xml
            if (!File.Exists(Cheating_Dummy))
            {
                // File.Copy("Cheating_Dummy.xml", Cheating_Dummy, true);
               
                byte[] Dummy_Resource = Imperialware.Properties.Resources.Cheating_Dummy;
                File.WriteAllBytes(Cheating_Dummy, Dummy_Resource);


                // Checking if the tag "File" exists in the specified GOF Xml File above with the Value "Core\Cheating_Dummy"
                Save_Tag_Into_Xml(Xml_Directory + "GameObjectFiles.xml", "Root", "File", @"Core\Cheating_Dummy.xml", false);




                // ======= Making sure all factions can build the Cheating dummy, using the load factions code =======
                string Playable_Factions = "";         


                // Getting all Factions from the Combo box and adding them to the File
                foreach (string Item in All_Playable_Factions)
                { if (Item != "Load Factions" & Item != "") { Playable_Factions += Item + ", "; } }

                
                try
                {   // Reading File      
                    string The_Text = File.ReadAllText(Cheating_Dummy);

                    // We replace anything in all Affiliation tags with the current Playable_Factions
                    The_Text = Regex.Replace(The_Text, @"<Affiliation>.*?</Affiliation>", "<Affiliation>" + Playable_Factions + "</Affiliation>");

                    // Saving Changes
                    File.WriteAllText(Cheating_Dummy, The_Text);
              
                } catch { }


                if (Allowed_Patching == "true")
                {
                    Make_Buildable_In_Base("false", "StarBase", "Cheating_Dummy_Space", "Cheating_Dummy_Space", false);
                    Make_Buildable_In_Base("false", "SpecialStructure", "Cheating_Dummy_Land_GC", "Cheating_Dummy_Land_SK", false);
                }
            }
                      
        }
        

        // ============== PATCHING all Ground Headquarter Buildings and Starbases with the Dummy ===============
        // Make_Buildable_In_Base("StarBase", "Unit_Name_Campaign", "Unit_Name_Skirmish", false);
        void Make_Buildable_In_Base(string Required_Faction, string Unit_Type, string Patch_Unit_GC, string Patch_Unit_SK, bool Show_Loading_Screen)
        {          
            Splash_Screen splashForm = null;

            if (Show_Loading_Screen) 
            {                
                Thread splashThread = new Thread(new ThreadStart(
                   delegate
                   {
                       splashForm = new Splash_Screen();
                       Application.Run(splashForm);
                   }
                 ));

                splashThread.SetApartmentState(ApartmentState.STA);
                splashThread.Start();
            }
               
  

            foreach (var Xml in Get_Xmls())
            {   // To prevent the program from crashing because of xml errors:
                 try {  
                    // ===================== Opening Xml File =====================
                    XDocument Xml_File = XDocument.Load(Xml, LoadOptions.PreserveWhitespace);


                    // Defining a XElement List
                    IEnumerable<XElement> Xml_Content =

                    // Selecting all Tags with this string Value:
                    from All_Tags in Xml_File.Descendants(Unit_Type)
                    // Selecting all non empty tags (null because we need all selected)
                    where (string)All_Tags.Attribute("Name") != null
                    select All_Tags;



                    string Current_Value = "";

                    // =================== Editing Xml Instance ===================
                    foreach (XElement Instance in Xml_Content)
                    {
                        // Setting any variable, just to find out if that tag is not inside the current Instance. If not inside the catch breaks the loop and continues with the next instance
                        if (Unit_Type == "SpecialStructure") try { Current_Value = Instance.Descendants("HQ_Win_Condition_Relevant").First().Value; } catch { continue; }

                        // Escaping Instance patching if any Required_Faction was specified and this Instance doesen't have it. 
                        if (Required_Faction != "false" & !Instance.Descendants("Affiliation").Any()) { continue; }
                        // Causes Exception: else if (Required_Faction != "false" & !Instance.Descendants("Affiliation").First().Value.Contains(Required_Faction)) { continue; }
                            

                        
                        // string Instance_Name = (string)Instance.FirstAttribute.Value;


                        foreach (string Faction in All_Playable_Factions)
                        {
                            if (Required_Faction != "false" & Faction != Required_Faction) { continue; }

                            if (Patch_Unit_GC != "false")
                            {   try
                                {   if (Instance.Descendants("Tactical_Buildable_Objects_Campaign").Any())
                                    {   Current_Value = Instance.Descendants("Tactical_Buildable_Objects_Campaign").First().Value;

                                        if (Current_Value != "" & Current_Value.Contains(Faction) & !Regex.IsMatch(Current_Value, Faction + ".*?" + Patch_Unit_GC) & !Regex.IsMatch(Current_Value, Faction + "," + ".        " + Patch_Unit_GC, RegexOptions.Singleline))
                                        { Instance.Descendants("Tactical_Buildable_Objects_Campaign").First().Value = Regex.Replace(Current_Value, Faction + ",", Faction + "," + Add_Line + "        " + Patch_Unit_GC + ","); }
                                    }
                                    else
                                    {   if (Faction != "" & Instance.Descendants("Affiliation").First().Value.Contains(Faction) & Unit_Type != "StarBase")
                                        {   Instance.Add("    ");
                                            Instance.Add(new XElement("Tactical_Buildable_Objects_Campaign", Add_Line + "      " + Faction + "," + Add_Line + "        " + Patch_Unit_GC + "," + Add_Line + "    "));
                                            Instance.Add(Add_Line);
                                        }
                                    }
                                } catch { }
                            }


                            if (Patch_Unit_SK != "false")
                            {   try
                                {   if (Instance.Descendants("Tactical_Buildable_Objects_Multiplayer").Any())
                                    {   Current_Value = Instance.Descendants("Tactical_Buildable_Objects_Multiplayer").First().Value;

                                        if (Current_Value != "" & Current_Value.Contains(Faction) & !Regex.IsMatch(Current_Value, Faction + ".*?" + Patch_Unit_SK) & !Regex.IsMatch(Current_Value, Faction + "," + ".        " + Patch_Unit_SK, RegexOptions.Singleline))
                                        { Instance.Descendants("Tactical_Buildable_Objects_Multiplayer").First().Value = Regex.Replace(Current_Value, Faction + ",", Faction + "," + Add_Line + "        " + Patch_Unit_SK + ","); }
                                    }
                                    else
                                    {   if (Faction != "" & Instance.Descendants("Affiliation").First().Value.Contains(Faction) & Unit_Type != "StarBase")
                                        {   Instance.Add("    ");
                                            Instance.Add(new XElement("Tactical_Buildable_Objects_Multiplayer", Add_Line + "      " + Faction + "," + Add_Line + "        " + Patch_Unit_SK + "," + Add_Line + "    "));
                                            Instance.Add(Add_Line);
                                        }
                                    }
                                } catch { }
                            }                                                            
                        }                     
                    }

                    Xml_File.Save(Xml);
                    // ===================== Closing Xml File =====================

                } catch { /*Imperial_Console(600, 100, "Failed to load " + Path.GetFileName(Xml) + Add_Line + "Or a Unit inside of it.");*/ }
            }

            if (Show_Loading_Screen) 
            {   // Closing the loading screen again once we're done
                splashForm.Invoke(new Action(splashForm.Close));
                splashForm.Dispose();
                splashForm = null;
            }
        }


        //=========== UI to Xml Tag Functions ==========//
        void Remove_God_Mode()
        {   
            try
            {
                Temporal_A = Load_Setting(Setting, "GMU");
                if (!Regex.IsMatch(Temporal_A, "[^A-Za-z]")) { return; } // Prevents crashes
          
                string[] God_Mode_Unit = Temporal_A.Split(',');
                Temporal_B = Regex.Replace(God_Mode_Unit[1], " ", "");

                if (Xml_Utility(God_Mode_Unit[0], Temporal_B, "Shield_Refresh_Rate") == "100000") { 
  
                    string Unit_Class = Xml_Utility(God_Mode_Unit[0], Temporal_B, "CategoryMask");
                    string Text_File = "";

                    switch (Unit_Class)
                    {
                        case "Fighter":
                            Text_File = Maximum_Values_Fighter;
                            break;
                        case "Bomber":
                            Text_File = Maximum_Values_Bomber;
                            break;

                        case "Corvette":
                            Text_File = Maximum_Values_Corvette;
                            break;
                        case "Frigate":
                            Text_File = Maximum_Values_Frigate;
                            break;
                        case "Capital":
                            Text_File = Maximum_Values_Capital;
                            break;

                        case "Infantry":
                            Text_File = Maximum_Values_Infantry;
                            break;
                        case "Vehicle":
                            Text_File = Maximum_Values_Vehicle;
                            break;
                        case "Air":
                            Text_File = Maximum_Values_Air;
                            break;
                        case "Hero":
                            Text_File = Maximum_Values_Hero;
                            break;
                        case "Structure":
                            Text_File = Maximum_Values_Structure;
                            break;
                    }

                    Int32.TryParse(Load_Setting(Text_File, "Maximum_Shield_Rate"), out Temporal_C);
                    // This variable strengh gives 75%, depening on class of selected unit
                    Temporal_A = ((Temporal_C / 4) * 3).ToString();           

                    // Writing the new value we just processed into the Tag
                    Verify_Edit_Xml_Tag(God_Mode_Unit[0], Temporal_B, "Shield_Refresh_Rate", Temporal_A);             
                }
            }  catch { }
        }
                      
        //=========== UI to Xml Tag Functions ==========//

        // Scroll_Xml_Value(Track_Bar, Progress_Bar, Text_Box, Maximal_Value);
        void Scroll_Xml_Value(TrackBar Track_Bar, ProgressBar Progress_Bar, TextBox Text_Box, int Maximal_Value, int Number)
        {
            // Fighters and Ground Units have a smaller maximum value, so we adjust the divisor number for such cases
            if (Maximal_Value < 999 & Number == 100) { Number = 10; }


            Progress_Bar.Maximum = Maximal_Value / Number;
            Track_Bar.Maximum = Progress_Bar.Maximum;

            Progress_Bar.Value = Track_Bar.Value;
            // Setting Value of the Text Box, according to the Maximal Value 
            Text_Box.Text = (Track_Bar.Value * Number).ToString();

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        void Scroll_Xml_Decimal(TrackBar Track_Bar, ProgressBar Progress_Bar, TextBox Text_Box, int Maximal_Value)
        {   // Processing Decimal Value 
            // System.Globalization.CultureInfo.GetCultureInfo("de-DE");

            Scrolling = true; 

            // Need to make sure the . is not turned into , for the european (german) languages: System.Globalization.CultureInfo.InvariantCulture.NumberFormat
            Text_Box.Text = ((decimal)Track_Bar.Value / 10).ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            if (!Text_Box.Text.Contains(".") & Text_Box.Text != "0" & Text_Box.Text != "") { Text_Box.Text = Text_Box.Text + ".0"; }

            Track_Bar.Maximum = Maximal_Value * 10;
            Progress_Bar.Maximum = Maximal_Value * 10;
            Progress_Bar.Value = (Track_Bar.Value);

            Scrolling = false; 
        }


        // Text_Box_Text_Changed(Track_Bar, Progress_Bar, Maximal_Value, Typed_Value, Number)
        void Text_Box_Text_Changed(TrackBar Track_Bar, ProgressBar Progress_Bar, int Maximal_Value, int Typed_Value, int Number)
        {
            if (Scrolling) { return; }

            // Fighters and Ground Units have a smaller maximum value, so we adjust the divisor number for such cases
            if (Maximal_Value < 999 & Number == 100) { Number = 10; }

            // If the value is higher then Maximum we make sure the Bars are full
            if (Typed_Value > Maximal_Value)
            {
                Progress_Bar.Value = Progress_Bar.Maximum;
                Track_Bar.Value = Track_Bar.Maximum;
            }
            else
            {   try
                {   // Setting amount in the Trackbar according to the Maximal Value 
                    Track_Bar.Maximum = Maximal_Value / Number;
                    // Then we need to set the Track Bar according to the Text box / 100      
                    Track_Bar.Value = Typed_Value / Number;


                    // Adjusting the Maximum Value of the Progress Bar and multiplicating x 100 to get the percentage
                    Progress_Bar.Maximum = Maximal_Value;
                    Progress_Bar.Value = Typed_Value;
                } catch { }
            }
           
        }

        //=========== Text to Xml Tag Functions for saving ==========//
        //if (!Is_Team) { Verify_Tag_with_Extension(Unit_Type, "Icon_Name", Text_Box_Icon_Name, ".tga")}


        void Verify_Tag_with_Extension(IEnumerable<XElement> The_Unit, string Tag_Name, Control Text_Box, string Suffix)
        {   try // Save_Xml_File.Element(Root_Tag).Element(Unit_Type)
            {
                Temporal_A = Text_Box.Text;
                if (The_Unit == null) { The_Unit = Instance; }


                if (The_Unit.Descendants(Tag_Name).Any())
                {   // If tag is not existing we create it below
                    if (Text_Box.Text != "" & Text_Box.Text != The_Unit.Descendants(Tag_Name).First().Value) 
                    {                       
                        if (Regex.IsMatch(Temporal_A, "(?i).*?" + Suffix + Suffix))
                        {    // We remove all .tga suffixes if there are more then one
                            Temporal_B = Regex.Replace(Temporal_A, "(?i)" + Suffix, "");

                            // And add a fresh suffix                  
                            The_Unit.Descendants(Tag_Name).First().Value = Temporal_B + Suffix;
                        }

                        else if (Regex.IsMatch(Temporal_A, "(?i).*?" + Suffix)) // If ends with suffix 
                        {   // Thats fine to save
                            The_Unit.Descendants(Tag_Name).First().Value = Temporal_A;
                        }
                        else // Otherwise we asume there is no suffix, so we add .tga
                        {
                            Temporal_B = Regex.Replace(Temporal_A, Temporal_A, Temporal_A + Suffix);
                            The_Unit.Descendants(Tag_Name).First().Value = Temporal_B;
                        }
                    }
                }
                else if (Text_Box.Text != "")
                {                    
                    if (Regex.IsMatch(Temporal_A, "(?i).*?" + Suffix + Suffix))
                    {   Temporal_B = Regex.Replace(Temporal_A, "(?i)" + Suffix, "");
                        The_Unit.First().Add("\t\t", new XElement(Tag_Name, Temporal_B + Suffix), Add_Line);
                    }
                    else if (Regex.IsMatch(Temporal_A, "(?i).*?" + Suffix)) // If ends with suffix 
                    {   // Thats fine to save                        
                        The_Unit.First().Add("\t\t", new XElement(Tag_Name, Temporal_A), Add_Line);
                    }
                    else 
                    {   Temporal_B = Regex.Replace(Temporal_A, Temporal_A, Temporal_A + Suffix);
                        The_Unit.First().Add("\t\t", new XElement(Tag_Name, Temporal_B), Add_Line);
                    }                              
                }             
            } catch { }
        }      


        //=====================//

        string Verify_Ability_Icon(string Ability_Icon)
        {
            if (Ability_Icon != null)
            {   // Verifying Ability icon suffix
                if (!Ability_Icon.Contains(".tga")) { Ability_Icon = Ability_Icon + ".tga"; }
                
                else if (Regex.IsMatch(Ability_Icon, "(?i).*?.tga.tga"))
                {    // We remove all .tga suffixes if there are more then one
                    Temporal_A = Regex.Replace(Ability_Icon, "(?i).tga", "");

                    // And add a fresh suffix                  
                    Ability_Icon = Temporal_A + ".tga";
                }
            }        

            return Ability_Icon;
        } 

        //=====================//

        //if (!Is_Team) { Add_Tag_with_Extension(Unit_Type, "Icon_Name", Text_Box_Icon_Name, ".tga")}

        void Add_Tag_with_Extension(string Unit_Type, string Tag_Name, Control Text_Box, string Suffix)
        {
            if (Text_Box.Text != "")
            {   Temporal_A = Text_Box.Text;

                if (Regex.IsMatch(Temporal_A, "(?i).*?" + Suffix + Suffix))
                {    // We remove all suffixes if there are more then one
                    Temporal_B = Regex.Replace(Temporal_A, "(?i)" + Suffix, "");

                    // And add a fresh suffix               
                    Save_Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Tag_Name, Temporal_B + Suffix));
                }

                else if (Regex.IsMatch(Temporal_A, "(?i).*?" + Suffix)) // If ends with .suffix 
                {   // Thats fine to save
                    Save_Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Tag_Name, Temporal_A));
                }
                else // Otherwise we asume there is no suffix, so we add .suffix
                {
                    Temporal_B = Regex.Replace(Temporal_A, Temporal_A, Temporal_A + Suffix);
                    Save_Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Tag_Name, Temporal_B));
                }
            }

        }
                


        void Add_Tag(Control Text_Box, XDocument Xml_File, string Root_Tag, string Tag_Name)
        {
            if (Text_Box.Text != "")
            { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Tag_Name, Text_Box.Text)); }
        }

        void Add_Tag_To_Team(string Tag_Name, Control Text_Box)
        { 
            if (Text_Box.Text != "")
            { Save_Xml_File.Element(Root_Tag).Element(Team_Type).Add(new XElement(Tag_Name, Text_Box.Text)); }
        }

        void Add_Text_To_Team(string Tag_Name, string Tag_Value)
        {   if (Tag_Value != "")
            { Save_Xml_File.Element(Root_Tag).Element(Team_Type).Add(new XElement(Tag_Name, Tag_Value)); }
        }

        // Validate_Save_Tag(Text_Box, Xml_File, Tag_Name);
        void Validate_Save_Tag(Control Text_Box, string Tag_Name)
        {
            if (Text_Box.Text != "") // If that tag exists
            {   try // We try to update that tag 
                {
                    if (Instance.Descendants(Tag_Name).Any())
                    {   if (Text_Box.Text != Instance.Descendants(Tag_Name).First().Value)
                        { Instance.Descendants(Tag_Name).First().Value = Text_Box.Text; }                   
                    }
                    else
                    {   Instance.First().Add("\t\t");
                        Instance.First().Add(new XElement(Tag_Name, Text_Box.Text), Add_Line);
                    }
                } catch { }
            }      
        }

        void Validate_Save_Unit_Tag(IEnumerable<XElement> The_Unit, string Tag_Name, string Tag_Value)
        {
            if (Tag_Value != "")
            {   try 
                {   if (The_Unit.Descendants(Tag_Name).Any())
                    {   if (Tag_Value != The_Unit.Descendants(Tag_Name).First().Value)
                        { The_Unit.Descendants(Tag_Name).First().Value = Tag_Value; }
                    }
                    else
                    {   The_Unit.First().Add("\t\t");
                        The_Unit.First().Add(new XElement(Tag_Name, Tag_Value), Add_Line);
                    }
                } catch { }
            }
        }  


        void Validate_Save_Text(string Tag_Name, string Tag_Value)
        {   if (Tag_Value != "") 
            {   try // We try to update that tag 
                {   if (Instance.Descendants(Tag_Name).Any())
                    {   if (Tag_Value != Instance.Descendants(Tag_Name).First().Value)
                        { Instance.Descendants(Tag_Name).First().Value = Tag_Value; }                    
                    }
                    else 
                    { Instance.First().Add("\t\t", new XElement(Tag_Name, Tag_Value), Add_Line); }
                } catch { }
            }      
        }

        //=====================// 
        // Validate_Save_Ability_Code(Ability_Type, Tag_Name, Tag_Value);

        // You can also use this to clear all tags from the Text_Box_Mod_Multiplier.Text which runns through Ability_1_Mod_Multipliers and Ability_2_Mod_Multipliers:
        // Validate_Save_Ability_Code(Ability_1_Type, null, "Clear");

        // Or clear the whole Ability
        // Validate_Save_Ability_Code(Ability_1_Type, null, "Clear_All");

        void Validate_Save_Ability_Code(string Selected_Ability, string Tag_Name, string Tag_Value)
        {            
            // Makig sure Abilities Data exists
            if (!Instance.Descendants("Unit_Abilities_Data").Any())
            {   Instance.First().Add("\t\t", new XComment(" ============ Abilities ============ "), 
                Add_Line, "\t\t", new XElement("Unit_Abilities_Data", new XAttribute("SubObjectList", "Yes")), Add_Line + "\t");
            }

            // If no such Ability exists and while we have less then 2 abilities, we create one 
            if (Instance.Descendants("Unit_Abilities_Data").Descendants("Unit_Ability").Count() < 2 & Selected_Ability != "" & Selected_Ability != " NONE" &
                !Instance.Descendants("Unit_Abilities_Data").Descendants("Unit_Ability").Descendants("Type").Any(x => x.Value == Selected_Ability))
            {
                // if (Selected_Ability == Ability_2_Type) // Adding this Comment only to second ability
                // { Instance.Descendants("Unit_Abilities_Data").First().Add(Add_Line, "\t\t\t", new XComment(" ========= Secondary Ability ========= ")); } 

                Instance.Descendants("Unit_Abilities_Data").First().Add(                               
                Add_Line, "\t\t\t", new XElement("Unit_Ability",
                Add_Line, "\t\t\t\t", new XElement("Type", Selected_Ability), Add_Line), Add_Line + "\t\t");
                Temporal_B = "true";
            }

           

            Temporal_C = 0;
     
            if (Tag_Value != "")
            {   // We search all "Unit_Ability" subtags
                foreach (var Tag in Instance.Descendants("Unit_Abilities_Data").Descendants("Unit_Ability"))               
                {   try 
                    {   Temporal_C++; // Increasing count for each Ability
                        
                                          
                        if (Regex.IsMatch(Tag.Descendants("Type").First().Value, "(?i)" + Selected_Ability))
                        {

                            if (Tag_Value == "Clear") // This function clears all added tags, except for the "Keep_Tags"
                            {   string Keep_Tags = "Type, Supports_Autofire, GUI_Activated_Ability_Name, Expiration_Seconds, Recharge_Seconds, Alternate_Name_Text, Alternate_Description_Text, Alternate_Icon_Name, SFXEvent_GUI_Unit_Ability_Activated, SFXEvent_GUI_Unit_Ability_Deactivated";
                              
                                // Needs to be a for loop, foreach and i++ would not work
                                for (int i = Tag.Descendants().Count() - 1; i > 0; i--)
                                {
                                    var Subtag = Tag.Descendants().ElementAt(i);
                         
                                    // Clearing all non standard tags
                                    if (!Keep_Tags.Contains(Subtag.Name.ToString())) { Subtag.Remove(); }                        
                                }
                                return;
                            }
                                                         
                                                                                                                                                                                                           
                            // Appending these special tags directly to the Selected_Ability after Clearing all tags above
                            if (Tag_Name == "Unit_Ability" & Tag_Value != "") 
                            { Tag.Add("\t\t\t\t", new XComment("here" + Tag_Value + "here"), Add_Line); return; }


                            if (Tag_Value != null & Tag.Descendants(Tag_Name).Any()) // If exists we will adjust the value
                            {
                                if (Tag.Descendants(Tag_Name).First().Value != Tag_Value)
                                { Tag.Descendants(Tag_Name).First().Value = Tag_Value; }
                            }
                            else if (Tag_Value != null & Tag_Value != "") // Otherwise we create this tag 
                            { Tag.Add("\t\t\t\t", new XElement(Tag_Name, Tag_Value), Add_Line); }
                        }

                        // Changing Ability Name
                        else if (Tag_Name == "Type" & Tag.Descendants(Tag_Name).Any()) 
                        {
                            if (Temporal_C == 1 & Selected_Ability == Ability_1_Type) // Then we know its Ability at Slot 1
                            {   // This triggers only in the first loop
                                if (Tag.Descendants(Tag_Name).First().Value != Tag_Value)
                                { Tag.Descendants(Tag_Name).First().Value = Tag_Value; }
                            }

                            else if (Temporal_C == 2 & Selected_Ability == Ability_2_Type) // Then we know its Ability at Slot 2                              
                            {   // This triggers only in the second loop while Temporal_C == 2 & Selected_Ability == Ability_2_Type
                                if (Tag.Descendants(Tag_Name).First().Value != Tag_Value)
                                { Tag.Descendants(Tag_Name).First().Value = Tag_Value; }
                            }
                        }
                       

                    } catch { }
                }      
            }        
        }

        //=====================// 

        void Remove_Abilities()           
        {
            Temporal_B = "Squadron, GroundCompany, HeroCompany";

            if (Ability_1_Remove != "")
            {
                Instance.Descendants("Unit_Abilities_Data").Descendants("Unit_Ability").Where(x => x.Descendants("Type").First().Value == Ability_1_Remove).Remove();
                // For Team units wait with deletion of the ability for Ability_1_Remove until we passed the normal unit and reached the Squadron/Team 
                if (Is_Team & Instance.Any()) { if (Temporal_B.Contains(Instance.First().Name.ToString())) Ability_1_Remove = ""; }
                else { Ability_1_Remove = ""; } 
            }

            if (Ability_2_Remove != "")
            {
                Instance.Descendants("Unit_Abilities_Data").Descendants("Unit_Ability").Where(x => x.Descendants("Type").First().Value == Ability_2_Remove).Remove();
                if (Is_Team & Instance.Any()) { if (Temporal_B.Contains(Instance.First().Name.ToString())) Ability_2_Remove = ""; }
                else { Ability_2_Remove = ""; } 
            }

            Temporal_B = "";
        }

        //=====================// 

        void Overwrite_Save_Tag(Control Text_Box, string Tag_Name)
        {   try 
            {   if (Instance.Descendants(Tag_Name).Any())
                {   if (Text_Box.Text != Instance.Descendants(Tag_Name).First().Value)
                    { Instance.Descendants(Tag_Name).First().Value = Text_Box.Text; }
                }
                else if (Text_Box.Text != "")
                {   Instance.First().Add("\t\t"); 
                    Instance.First().Add(new XElement(Tag_Name, Text_Box.Text), Add_Line); 
                }
            } catch { }                 
        }

        void Overwrite_Save_Text(string Tag_Name, string Tag_Value)
        {   try
            {   if (Instance.Descendants(Tag_Name).Any())
                {
                    if (Tag_Value != Instance.Descendants(Tag_Name).First().Value)
                    { Instance.Descendants(Tag_Name).First().Value = Tag_Value; }
                }
                else if (Tag_Value != "")
                {
                    Instance.First().Add("\t\t");
                    Instance.First().Add(new XElement(Tag_Name, Tag_Value), Add_Line);
                }
            } catch { }
        }
         
        // Validate_Save_Costum_Tag(Text_Box_Name, Text_Box);
        void Validate_Save_Costum_Tag(RichTextBox Text_Box_Name, TextBox Text_Box)
        {   try
            {   // Removing <> signs from name, if the User keeps the brackets
                string Costum_Tag_Name_Value = Regex.Replace(Text_Box_Name.Text, "[</>]", "");

                if (Instance.Descendants(Costum_Tag_Name_Value).Any())
                {
                    if (Text_Box.Text != "" & Text_Box_Name.Text != "<Tag_1_Name>" & Text_Box.Text != Instance.Descendants(Costum_Tag_Name_Value).First().Value)
                    { Instance.Descendants(Costum_Tag_Name_Value).First().Value = Text_Box.Text; }
                }
                else if (Text_Box.Text != "" & Text_Box_Name.Text != "<Tag_1_Name>")
                { Instance.First().Add("\t\t"); Instance.First().Add(new XElement(Costum_Tag_Name_Value, Text_Box.Text), Add_Line); }
             
            } catch { }
        }

        void Save_Switch(string Tag_Name, string Bool_Type, bool Switch)
        {
            string Is_True = "Yes";
            string Is_False = "No";

            if (Bool_Type == "1")
            {
                Is_True = "True";
                Is_False = "False";
            }

            try
            {   if (Instance.Descendants(Tag_Name).Any())
                {   // Aligning Tag value to Toggle
                    if (Switch == true & Is_True != Instance.Descendants(Tag_Name).First().Value) { Instance.Descendants(Tag_Name).First().Value = Is_True; }
                    else if (Switch == false & Is_False != Instance.Descendants(Tag_Name).First().Value) { Instance.Descendants(Tag_Name).First().Value = Is_False; }
                }
                else
                {   if (Switch == true) { Instance.First().Add("\t\t"); Instance.First().Add(new XElement(Tag_Name, Is_True), Add_Line); }
                    else if (Switch == false) { Instance.First().Add("\t\t"); Instance.First().Add(new XElement(Tag_Name, Is_False), Add_Line); }
                }
            } catch { }
        }

        // Position of these 2 parameters (left or right side) decides in which direction they move!
        // Switch_Value_Activity(List_Box_Inactive, List_Box_Active);
        void Switch_Value_Activity(ListBox List_Box_Inactive, ListBox List_Box_Active)
        {
            string Current_Selection = List_Box_Inactive.GetItemText(List_Box_Inactive.SelectedItem).ToString();
            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }

            for (int i = List_Box_Inactive.Items.Count - 1; i >= 0; --i)
            {
                string Item = List_Box_Inactive.Items[i].ToString();

                if (Item == Current_Selection)
                {   // Transfer; removing selection from one Listbox and adding to the other
                    List_Box_Active.Items.Add(Item);
                    List_Box_Inactive.Items.Remove(Item);

                    // Selecting the newest Entry in the other Listbox, which is the one we just added
                    List_Box_Active.SetSelected(List_Box_Active.Items.Count - 1, true);
                }
            }

            // Then selecting the highest one in this Listbox
            try { List_Box_Inactive.SelectedItem = List_Box_Inactive.Items[0]; } catch { }
        }


        //=====================// 
        void Exchange_Table_Values(ListBox List_Box_A, ListBox List_Box_B)
        {
            Temporal_A = "";

            // Getting all Items of first list into the string
            for (int i = List_Box_A.Items.Count - 1; i >= 0; --i)
            {
                var Item = List_Box_A.Items[i].ToString();

                if (Item != "")
                {
                    Temporal_A += Item + ",";
                }
            }

            List_Box_A.Items.Clear();


            // Then transfering all items to the other list          
            for (int i = List_Box_B.Items.Count - 1; i >= 0; --i)
            { List_Box_A.Items.Add(List_Box_B.Items[i].ToString()); }

            List_Box_B.Items.Clear();

            foreach (string Entry in Temporal_A.Split(','))
            { List_Box_B.Items.Add(Entry); }
        }
   
        //=====================// 

        // This function must NOT be used inside any process of XML editing, or the Xml_File loads as XDocument then it saves the change, 
        // But the (older) XDocument you loaded before is still inside of the memory and overwrites the (edited) Validated XDocument save again! 
        // Use the function below to save from inside a editing/saving process.

        // If Allow_Duplicates is true that will add a new Tag with the name "Tag_Name" each time the code is executed, if false the "Tag_Content" 
        // in the existing tag will be updated.

        void Save_Tag_Into_Xml(string File_Name, string Required_Instance, string Tag_Name, string Tag_Content, bool Allow_Duplicates)
        {
            try // We need to try in order to prevent xml errors
            {               
                XDocument Xml_File = XDocument.Load(File_Name, LoadOptions.PreserveWhitespace);

               
                if (Required_Instance == "Root") // This saves automatically to root tag
                {
                    foreach (XElement Tag in Xml_File.Root.Descendants())
                    { if (!Allow_Duplicates & Tag.Value == Tag_Content) { return; } }
                    
                    // If it passed the duplicate check for ALL Descendant tags of the "Tag_Name" Variable, we continue
                    Xml_File.Root.Add("\t");
                    Xml_File.Root.Add(new XElement(Tag_Name, Tag_Content), Add_Line);
                    Xml_File.Save(File_Name); return;
                }


                var Current_Instance =
                   from All_Tags in Xml_File.Root.Descendants()
                   // Selecting all non empty tags that have the Attribute "Name", null because we need all selected.
                   where (string)All_Tags.Attribute("Name") == Required_Instance
                   select All_Tags;

               
                // =================== Checking Xml Instance ===================
                foreach (XElement Instance in Current_Instance)
                {
                    // If any duplicate tag was found but is not allowed
                    if (!Allow_Duplicates & Instance.Descendants(Tag_Name).Any())
                    {    try // We try to update that tag to the new Tag_Content
                         {  if (Tag_Content != "" & Tag_Content != Instance.Descendants(Tag_Name).First().Value)
                            { Instance.Descendants(Tag_Name).First().Value = Tag_Content; }
                         } catch { }                  
                    }
                    else
                    {   // This special tag is supposed to have the position at the very top
                        if (Tag_Name == "Variant_Of_Existing_Type")
                        { Instance.AddFirst(Add_Line, "\t\t", new XElement(Tag_Name, Tag_Content)); }
                       
                        else  // Adding new Element to the Root tag of this Xml File
                        { Instance.AddFirst("\t\t"); Instance.Add(new XElement(Tag_Name, Tag_Content), Add_Line); }
                    }
                }
                
                // Saving XML File
                Xml_File.Save(File_Name);
                

            } catch { if (Debug_Mode == "true") { Imperial_Console(600, 100, Add_Line + "Failed to save the <" + Tag_Name + "> tag."); } }
        }
        //=====================//

        // Use this function to save from inside any editing/saving process.

        void Validate_Tag_In_Xml(XDocument Xml_File, string Required_Instance, string Tag_Name, string Tag_Content, bool Allow_Duplicates)
        {
            try // We need to try in order to prevent xml errors
            {              
                if (Required_Instance == "Root") // This saves automatically to root tag
                {
                    foreach (XElement Tag in Xml_File.Root.Descendants())
                    { if (!Allow_Duplicates & Tag.Value == Tag_Content) { return; } }

                    // If it passed the duplicate check for ALL Descendant tags of the "Tag_Name" Variable, we continue
                    Xml_File.Root.Add("\t");
                    Xml_File.Root.Add(new XElement(Tag_Name, Tag_Content), Add_Line);
                    return;
                }


                var Current_Instance =
                   from All_Tags in Xml_File.Root.Descendants()
                   // Selecting all non empty tags that have the Attribute "Name", null because we need all selected.
                   where (string)All_Tags.Attribute("Name") == Required_Instance
                   select All_Tags;


                // =================== Checking Xml Instance ===================
                foreach (XElement Instance in Current_Instance)
                {
                    // If any duplicate tag was found but is not allowed
                    if (!Allow_Duplicates & Instance.Descendants(Tag_Name).Any())
                    {
                        try // We try to update that tag to the new Tag_Content
                        {   if (Tag_Content != "" & Tag_Content != Instance.Descendants(Tag_Name).First().Value)
                            { Instance.Descendants(Tag_Name).First().Value = Tag_Content; }
                        } catch { }                          
                    }
                    else
                    {   // This special tag is supposed to have the position at the very top
                        if (Tag_Name == "Variant_Of_Existing_Type")
                        { Instance.AddFirst(Add_Line, "\t\t", new XElement(Tag_Name, Tag_Content)); }

                        else  // Adding new Element to the Root tag of this Xml File
                        { Instance.Add("\t\t"); Instance.Add(new XElement(Tag_Name, Tag_Content), Add_Line); }
                    }
                }     
            } catch { if (Debug_Mode == "true") { Imperial_Console(600, 100, Add_Line + "Failed to save the <" + Tag_Name + "> tag."); } }
        }

        //=====================//
     
        void Validate_Xml_Template(string Xml_File)
        {   try
            {
                if (!File.Exists(Xml_Directory + @"Core\" + Xml_File))
                {   // If our template files don't exist we are going to copy it into the Xml dir of the chosen Mod
                    File.Copy(Program_Directory + @"Xml_Core\" + Xml_File, Xml_Directory + @"Core\" + Xml_File, true);

                    // And add the tag "File" to GOF and HDF .xml with the Name of that Template.xml.
                    Save_Tag_Into_Xml(Xml_Directory + "GameObjectFiles.xml", "Root", "File", @"Core\" + Xml_File, false);
                    Save_Tag_Into_Xml(Xml_Directory + "HardpointDataFiles.xml", "Root", "File", @"Core\" + Xml_File, false);
                }
            }
            catch { }
        }
        //=====================//


        void Apply_Cheats() 
        {

            // If the Directory path for Lua Scripts don't exist, we create one
            if (!Directory.Exists(Data_Directory + @"Scripts\GameObject"))
            { Directory.CreateDirectory(Data_Directory + @"Scripts\GameObject"); }


            string Lua_Script = Data_Directory + @"Scripts\GameObject\Story_Cheating.lua";

            // Overwriting Lua Script with its template in the .exe, in order to Edit it
            // File.Copy("Story_Cheating.lua", Lua_Script, true);

            byte[] Script_Resource = Imperialware.Properties.Resources.Story_Cheating;
            File.WriteAllBytes(Lua_Script, Script_Resource);
            
                         
            
            // We are going to replace the Unit Variable in our Cheating Lua Script.
            string Spawnlist_Text = "";           
            foreach (var item in List_Box_All_Spawns.Items)
            {
                Spawnlist_Text += "\"" + item.ToString() + "\"" + ", ";
            }
         


            // Reading file                 
            string The_Text = File.ReadAllText(Lua_Script);

            // string Selected_Faction_Without_Whitespace = System.Text.RegularExpressions.Regex.Replace(Selected_Faction, "[\n\r\t]", " ");
               
            // This replaces these 3 keywords and overwrites the whole file:
            The_Text = The_Text.Replace("\"Faction_Name\"", "\"" + Selected_Faction + "\"");

            // If no Planet Variable was specified, we spawn on the planet where the Cheating dummy was built
            if (Selected_Planet == "Build_Planet")
            {   The_Text = The_Text.Replace("\"Planet_Name_01\"", "\"" + "" + "\"");
                The_Text = The_Text.Replace("Build_Planet = false", "Build_Planet = true"); }           
            else // Otherwise we use the assigned Planet Variable for Target Planet
            {The_Text = The_Text.Replace("\"Planet_Name_01\"", "\"" + Target_Planet + "\"");}
           
            The_Text = The_Text.Replace("\"Planet_Name_02\"", "\"" + Start_Planet + "\"");              
            The_Text = The_Text.Replace("\"Selected_Units\"", Spawnlist_Text);

            // If Teleport, Destroy or Spawn were activated we will insert the Value "true" to enable them in the script
            The_Text = The_Text.Replace("Spawn = false", Activate_Spawn);
            The_Text = The_Text.Replace("Teleport_Units = false", Teleport_Units);
            The_Text = The_Text.Replace("Teleport_from_Planet = false", Teleport_from_Planet);
            The_Text = The_Text.Replace("Teleport_Both = false", Teleport_Units_on_Planet);
            The_Text = The_Text.Replace("Destroy_Target_Planet = false", Destroy_Target_Planet);

                           
            // Giving Player Money if he setted any value
            if (Player_Credits != 0) { The_Text = The_Text.Replace("\"Player_Credits\"", Player_Credits.ToString()); }
            else The_Text = The_Text.Replace("\"Player_Credits\"", "0" );


            File.WriteAllText(Lua_Script, The_Text);
           

            // Resetting for next usage
            Activate_Spawn = "Spawn = false";
            Teleport_Units = "Teleport_Units = false";
            Teleport_from_Planet = "Teleport_from_Planet = false";
            Teleport_Units_on_Planet = "Teleport_Both = false";
            Destroy_Target_Planet = "Destroy_Target_Planet = false";           
           
            Player_Credits = 0;
            Track_Bar_Credits.Value = 0;
            Progress_Bar_Credits.Value = 0;
            Text_Box_Credits.Text = "";
                               
        }

        //=====================//
        
        // Xml_Utility(Selected_Xml, null, null);     = Returns a list of all found Instances in this Xml
        // Xml_Utility(Selected_Xml, "Unit_Name", null);     = Returns the full Unit 
        // Xml_Utility(Selected_Xml, "Unit_Name", "Tag_Name");     = Returns value of that Tag

        // Xml_Utility(null, "Unit_Name", null);     = Searches all Xml files and returns the full Unit if found (costs more performance)
        // Xml_Utility(null, "Unit_Name", "Tag_Name");      = Searches all Xml files and returns the Value of Tag_Name


        string Xml_Utility(string Xml_Name, string Required_Instance, string Required_Tag_Value)
        {
            if (Xml_Name == null)
            {
                foreach (var Xml in Get_Xmls())
                {
                    try // Preventing the program from crashing because of xml errors
                    {
                        // ===================== Opening Xml File =====================
                        XDocument Xml_File = XDocument.Load(Xml, LoadOptions.PreserveWhitespace);
                   
                        var Instances =
                            from All_Tags in Xml_File.Root.Descendants()
                            // Selecting all non empty tags that have the Attribute "Name", null because we need all selected.
                            where (string)All_Tags.Attribute("Name") == Required_Instance
                            select All_Tags;


                        // =================== Checking Xml Instance ===================
                        foreach (XElement Instance in Instances)
                        {
                            if (Required_Instance != null & Required_Tag_Value != null & Required_Tag_Value != "")
                            { if (Instance.Descendants(Required_Tag_Value).Any()) try { return Instance.Descendants(Required_Tag_Value).First().Value; } catch { } }                          
                        
                            else if (Required_Instance == null) { return null; } 

                            // If neither Tag Value nor Unit type was specified (nothing but the Instance Name) we simply return the whole instance
                            else if (Required_Tag_Value == null) { return Instance.ToString(); }  
                        }
                    }
                    catch { /* Imperial_Console(600, 100, "Failed to load " + Path.GetFileName(Xml) + Add_Line + "Or a Unit inside of it.");*/ }
                }
            }

            else if (Xml_Name != null)
            {   try
                {   
                    if (Required_Instance != null)
                    {   // ===================== Opening Xml File =====================
                        XDocument Xml_File = XDocument.Load(Xml_Name, LoadOptions.PreserveWhitespace);

                        var Instances =
                           from All_Tags in Xml_File.Root.Descendants()
                           // Selecting all non empty tags that have the Attribute "Name", null because we need all selected.
                           where (string)All_Tags.Attribute("Name") == Required_Instance
                           select All_Tags;


                        // =================== Checking Xml Instance ===================
                        foreach (XElement Instance in Instances)
                        {
                           if (Required_Instance != null & Required_Tag_Value != null & Required_Tag_Value != "")
                           { try { return Instance.Descendants(Required_Tag_Value).First().Value; } catch { } }

                           else if (Required_Instance == null) { return null; }

                           // If neither Tag Value nor Unit type was specified (nothing but the Instance Name) we simply return the whole instance
                           else if (Required_Tag_Value == null) { return Instance.ToString(); }  
                        }
                    }
                    else
                    {
                        // ===================== Opening Xml File =====================
                        XDocument Xml_File = XDocument.Load(Xml_Name, LoadOptions.PreserveWhitespace);


                        IEnumerable<XElement> Xml_Content =                       
                        from All_Tags in Xml_File.Root.Descendants()
                        // Selecting all non empty tags that have the Attribute "Name", null because we need all selected.
                        where (string)All_Tags.Attribute("Name") != null
                        select All_Tags;


                        string All_Instances = "";
                        Temporal_C = 0;

                        // =================== Checking Xml Instance ===================
                        foreach (XElement Instance in Xml_Content)
                        {
                            Temporal_C++;

                            // Adding Attribute Name, the last one gets no ", " attached
                            if (Temporal_C == Xml_Content.Count()) { All_Instances += (string)Instance.FirstAttribute.Value; } 
                            else { All_Instances += (string)Instance.FirstAttribute.Value + ", "; }                                                   
                        }



                        return All_Instances;                      
                    }
               
                }
                catch { /* Imperial_Console(600, 100, "Failed to load " + Path.GetFileName(Xml) + Add_Line + "Or a Unit inside of it.");*/ }
            }
            return null;
        }


        //=====================//
             
        // Returns Required_Tag_Value
        // Ability_Utility(Xml_Name, Required_Instance, Required_Ability, Required_Tag_Value):
        // Ability_Utility(null, Required_Instance, Required_Ability, Required_Tag_Value):

        // Returns all values of Mod_Multiplayers inside of a string seperated by , signs. We split it later
        // Ability_Utility(Xml_Name, Required_Instance, Required_Ability, "Mod_Multiplier"):

        // Returns the full Required_Ability code
        // Ability_Utility(Xml_Name, Required_Instance, Required_Ability, null):
        // Ability_Utility(null, Required_Instance, Required_Ability, null):

        // Returns all found "Ability" tags in this unit and seperates them by , which you can split then:
        // Ability_Utility(Xml_Name, Required_Instance, null, null):
        // Ability_Utility(null, Required_Instance, null, null):

        // Returns Name of all Instances with the required ability in this Xml or in the whole mod if Xml_Name == null
        // Ability_Utility(Xml_Name, null, "Stealth", null));
        // Ability_Utility(null, null, "Stealth", null));


        string Ability_Utility(string Xml_Name, string Required_Instance, string Required_Ability, string Required_Tag_Value)
        {   string Result = "";

            if (Xml_Name != null)
            { Result = Get_Ability_Code(Xml_Name, Required_Instance, Required_Ability, Required_Tag_Value); }
           
            else if (Xml_Name == null)
            {   foreach (var Xml in Get_Xmls())
                { Result += Get_Ability_Code(Xml, Required_Instance, Required_Ability, Required_Tag_Value); }
            }

            return Result;
        }


        string Get_Ability_Code(string Xml_Name, string Required_Instance, string Required_Ability, string Required_Tag_Value)
        {                    
            Temporal_A = "";
            Temporal_B = "";
            string Mod_Multipliers = "";

            try 
            {   // ===================== Opening Xml File =====================
                XDocument Xml_File = XDocument.Load(Xml_Name, LoadOptions.PreserveWhitespace);

                var Instances =
                    from All_Tags in Xml_File.Root.Descendants()
                    // Selecting all non empty tags that have the Attribute "Name", null because we need all selected.
                    where  (string)All_Tags.Attribute("Name") != null
                    & All_Tags.Descendants("Unit_Abilities_Data").Descendants("Unit_Ability").Descendants("Type").Any()
                    select All_Tags;


                // =================== Checking Xml Instance ===================
                foreach (XElement Instance in Instances)
                {
                    Temporal_A = (string)Instance.FirstAttribute.Value;


                    if (Required_Tag_Value == "Abilities" & Instance.Descendants("Abilities").Any())
                    { return Instance.Descendants("Abilities").First().ToString(); }

                    if (Required_Instance == null)
                    {
                        if (Regex.IsMatch(Instance.Descendants("Unit_Abilities_Data").Descendants("Unit_Ability").Descendants("Type").First().Value, "(?i)" + Required_Ability))
                        {                         
                            // Adding Attribute Name to list of units that own this ability
                            Temporal_B += Temporal_A + ",";
                        }                     
                    }

                    else if (Required_Instance == Temporal_A)
                    {   // We search all "Unit_Ability" subtags
                        foreach (var Tag in Instance.Descendants("Unit_Abilities_Data").Descendants("Unit_Ability"))
                        {
                            // if (Tag.Descendants("Type").First().Value == Required_Ability) Using Regex instead to be not case sensitive
                            if (Regex.IsMatch(Tag.Descendants("Type").First().Value, "(?i)" + Required_Ability))
                            {   // If neither Instance nor Unit type was specified (nothing but the Instance Name) we simply return all abilities
                                if (Required_Ability != null & Required_Tag_Value == null)
                                { try { return Tag.ToString(); } catch {} }

                                else if (Required_Tag_Value != null & Required_Tag_Value != "Mod_Multiplier") 
                                { try { return Tag.Descendants(Required_Tag_Value).First().Value; } catch { } }


                                if (Required_Tag_Value == "Mod_Multiplier" & Tag.Descendants("Mod_Multiplier").Any())
                                {
                                    foreach (var Entry in Tag.Descendants("Mod_Multiplier"))
                                    { Mod_Multipliers += Regex.Replace(Entry.ToString(), @"\t", " ") + ";"; }                                   
                                }                                
                            }
                           
                            if (Required_Ability == null) { Temporal_B += Tag.Descendants("Type").First().Value + ","; }
                        }

                        if (Required_Tag_Value == "Mod_Multiplier") { return Mod_Multipliers; }
                        
                        // Otherwise we return the name of all found abilities 
                        if (Required_Ability == null) { return Temporal_B; }
                    }
                                                                                                                                                                                                                                                          
                 }
            } catch {} 
            return Temporal_B;
        }

        //=====================//

        // This function is called for both, the Unit itself and its Team/Squadron, 
        // Where "Instance" means the current unit which changes from Unit to target at Squadron/Team
        void Save_Abilities() 
        {

            Ability_1_Icon = Verify_Ability_Icon(Ability_1_Icon);
            Ability_2_Icon = Verify_Ability_Icon(Ability_2_Icon);

            // string Instance_Text = Instance.First().ToString();

            if (Ability_1_Mod_Multipliers != null)
            { Temporal_A = Regex.Replace(Ability_1_Mod_Multipliers, ";", "\n"); }
            else { Temporal_A = Ability_1_Mod_Multipliers; }



            // Clearing abilities 
            Remove_Abilities(); 

        
            // Ability_1_Type is set by Combo_Box_Ability_Type when the user selects anything
            if (Ability_1_Type != null & Ability_1_Type != "" & Ability_1_Type != " NONE")
            {
                if (Ability_1_Remove == null | Ability_1_Remove == "")
                {
                    // Clearing all tags that are not the ones below
                    Validate_Save_Ability_Code(Ability_1_Type, null, "Clear");

                    Validate_Save_Ability_Code(Ability_1_Type, "Type", Ability_1_Type);
                    Validate_Save_Ability_Code(Ability_1_Type, "Supports_Autofire", Ability_1_Toggle_Auto_Fire.ToString());
                    Validate_Save_Ability_Code(Ability_1_Type, "GUI_Activated_Ability_Name", Ability_1_Activated_GUI);
                    Validate_Save_Ability_Code(Ability_1_Type, "Expiration_Seconds", Ability_1_Expiration_Time);
                    Validate_Save_Ability_Code(Ability_1_Type, "Recharge_Seconds", Ability_1_Recharge_Time);

                    Validate_Save_Ability_Code(Ability_1_Type, "Alternate_Name_Text", Ability_1_Name);
                    Validate_Save_Ability_Code(Ability_1_Type, "Alternate_Description_Text", Ability_1_Description);
                    Validate_Save_Ability_Code(Ability_1_Type, "Alternate_Icon_Name", Ability_1_Icon);

                    Validate_Save_Ability_Code(Ability_1_Type, "SFXEvent_GUI_Unit_Ability_Activated", Ability_1_Activated_SFX);
                    Validate_Save_Ability_Code(Ability_1_Type, "SFXEvent_GUI_Unit_Ability_Deactivated", Ability_1_Deactivated_SFX);


                    if (Temporal_A != null)
                    {                        
                        // We use a XComment that is marked with "here" that we remove later in order to preserve < and > signs, also we add "\t\t\t\t" to each line
                        Validate_Save_Ability_Code(Ability_1_Type, "Unit_Ability", Regex.Replace(Temporal_A, "\n", "\n\t\t\t\t"));
                    }

                }               
            }        


            if (Ability_2_Mod_Multipliers != null)
            { Temporal_A = Regex.Replace(Ability_2_Mod_Multipliers, ";", "\n"); }
            else { Temporal_A = Ability_2_Mod_Multipliers; }


            if (Ability_2_Type != null & Ability_2_Type != "" & Ability_2_Type != " NONE")
            {
                if (Ability_2_Remove == null | Ability_2_Remove == "")
                {
                    Validate_Save_Ability_Code(Ability_2_Type, null, "Clear");

                    Validate_Save_Ability_Code(Ability_2_Type, "Type", Ability_2_Type);
                    Validate_Save_Ability_Code(Ability_2_Type, "Supports_Autofire", Ability_2_Toggle_Auto_Fire.ToString());
                    Validate_Save_Ability_Code(Ability_2_Type, "GUI_Activated_Ability_Name", Ability_2_Activated_GUI);
                    Validate_Save_Ability_Code(Ability_2_Type, "Expiration_Seconds", Ability_2_Expiration_Time);
                    Validate_Save_Ability_Code(Ability_2_Type, "Recharge_Seconds", Ability_2_Recharge_Time);

                    Validate_Save_Ability_Code(Ability_2_Type, "Alternate_Name_Text", Ability_2_Name);
                    Validate_Save_Ability_Code(Ability_2_Type, "Alternate_Description_Text", Ability_2_Description);
                    Validate_Save_Ability_Code(Ability_2_Type, "Alternate_Icon_Name", Ability_2_Icon);

                    Validate_Save_Ability_Code(Ability_2_Type, "SFXEvent_GUI_Unit_Ability_Activated", Ability_2_Activated_SFX);
                    Validate_Save_Ability_Code(Ability_2_Type, "SFXEvent_GUI_Unit_Ability_Deactivated", Ability_2_Deactivated_SFX);

                    if (Temporal_A != null) { Validate_Save_Ability_Code(Ability_2_Type, "Unit_Ability", Regex.Replace(Temporal_A, "\n", "\n\t\t\t\t")); }              
                }
            }



            // Making sure it ends with only 1 "</Abilities>"  
            if (!Is_Team & !Text_Box_Additional_Abilities.Text.EndsWith("</Abilities>") & Text_Box_Additional_Abilities.Text != "")
            {
                Text_Box_Additional_Abilities.Focus();

                Imperial_Dialogue(500, 160, "Save", "Cancel", "false", Add_Line + "    Warning: The text inside of the Passive Abilities"
                                                             + Add_Line + @"    Box does not end with ""</Abilities>"".");

                if (Caution_Window.Passed_Value_A.Text_Data == "false") { return; }
            }
            else if (!Is_Team & Text_Box_Additional_Abilities.Text.EndsWith("</Abilities></Abilities>"))
            { Text_Box_Additional_Abilities.Text = Text_Box_Additional_Abilities.Text.Replace("</Abilities></Abilities>", "</Abilities>"); }



            // Removing Tag Variable (was needed before in order to append Tags to it)
            if (Text_Box_Additional_Abilities.Text != "")
            {
                Temporal_B = Regex.Replace(Text_Box_Additional_Abilities.Text, @"<Abilities SubObjectList=""Yes"">", "");
                Temporal_A = Regex.Replace(Temporal_B, @"</Abilities>", "");
            }
            else { Temporal_A = ""; }


            // This wont run if the Checkbox for teams is unchecked
            Temporal_B = "Squadron, GroundCompany, HeroCompany";

            try
            {  if (Temporal_B.Contains(Instance.First().Name.ToString()) // Also if Text_Box_Additional_Abilities.Text contains nothing we can skip the returning
                & Text_Box_Additional_Abilities.Text != "" & Check_Box_Use_In_Team.Checked == false) { return; }
            } catch {}

            if (Instance.Descendants("Abilities").Any()) // If exists we will adjust the value
            {                
                // Need to delete and rebuild the whole Abilities tag each time because Text_Box_Additional_Abilities mitght has changed
                Instance.Descendants("Abilities").First().Value = "";
                Instance.Descendants("Abilities").First().Add(new XComment("here" + Temporal_A + "here"));
            }

            else if (Text_Box_Additional_Abilities.Text != null & Text_Box_Additional_Abilities.Text != "")
            {   Instance.First().Element("Unit_Abilities_Data").AddAfterSelf(Add_Line,
                    "\t\t", new XComment(" ======== Additional Abilities ========= "), Add_Line,
                    "\t\t", new XComment("here" + Text_Box_Additional_Abilities.Text + "here"), Add_Line,
                    "\t\t", new XComment(" ======================================= "), Add_Line
                ); // End of Abilities                                     
            }


            // Resetting Icon values
            Ability_1_Icon = Ability_1_Icon.Replace(".tga", "");
            Ability_2_Icon = Ability_2_Icon.Replace(".tga", "");
             
        }

        //=====================//
        void Verify_Edit_Xml_Tag(string Xml_Name, string Selected_Unit, string Tag_Name, string New_Tag_Value)
        {         
            try
            {   //===== Loading .Xml File =====
                XDocument Xml_File = XDocument.Load(Xml_Name, LoadOptions.PreserveWhitespace);
          
                           
                IEnumerable<XElement> Selected_Instance =
                // Selecting all child Tags from tags that have the Name "SpecialStructure"   
                from All_Tags in Xml_File.Root.Descendants()
                // Selecting all non empty tags that have the Attribute "Name", null because we need all selected.
                where (string)All_Tags.Attribute("Name") == Selected_Unit
                select All_Tags;
 
                if (Selected_Instance.Descendants(Tag_Name).First().Value != New_Tag_Value)
                { Selected_Instance.Descendants(Tag_Name).First().Value = New_Tag_Value; } 
                

                //====== Saving Changes ======

                Xml_File.Save(Xml_Name);
            } catch { }
        }
       
        //=====================//

        List<string> Load_Xml_Unit(string Xml_Name, string Current_Unit)
        {
            try 
            {   //======== Loading .Xml File ==========     
                XElement Xml_File = XElement.Load(Xml_Name);


                // Getting the current Unit and selecting all its Tags
                var Tags = Xml_File.Elements(Unit_Type)
                    .Where(Item => Item.Attribute("Name").Value == Current_Unit)
                    .SelectMany(Entry => Entry.Descendants()).ToList();



                // Removing all listed items from the List Box 
                List_View_Selected_Tag.Items.Clear();

                foreach (var Tag in Tags)
                {
                    // Inserting the the Names in tags and the Value between them
                    List_View_Selected_Tag.Items.Add("<" + Tag.Name + ">" + Tag.Value + "</" + Tag.Name + ">");

                    // Adding it to a List
                    Selected_Instance_Tags.Add(Tag.ToString());
                }
            } 
            catch { Imperial_Console(700, 150, "Could not find " + Add_Line + Xml_Name); }

            return Selected_Instance_Tags;   
        }


        //=====================//

        List<string> Load_Xml_File2(string Xml_Name)
        {

            int Xml_Entries = 0;

            try
            {
                //======== Loading .Xml File ==========     
                XElement Xml_File = XElement.Load(Xml_Name);

                Xml_Entries = 1;

                // Getting the current Unit and selecting all its Tags
                var Tags = Xml_File.Elements()
                .SelectMany(Entry => Entry.Descendants()).ToList();


                // Removing all listed items from the List Box 
                List_View_Selected_Tag.Items.Clear();

              

                foreach (var Tag in Tags)
                {
                    
                    // Inserting the the Names in tags and the Value between them               
                    var Item = List_View_Selected_Tag.Items.Add("<" + Tag.Name + ">" + Tag.Value + "</" + Tag.Name + ">");
                                      
                   
                    Item.ForeColor = Color.Black;

                    // Every second item should have this value in order to create a checkmate pattern with good contrast
                    if (Checkmate_Color == "true" & Item.Index % 2 == 0)
                    {   Item.ForeColor = Color_03;
                        Item.BackColor = Color_07;
                    }

                    if (Tag.Value == "Yes" | Tag.Value == "No" | Tag.Value == "yes" | Tag.Value == "no" | Tag.Value == "True" | Tag.Value == "False" | Tag.Value == "true" | Tag.Value == "false")
                    { Item.BackColor = Color_02; }


                    // Adding it to a List
                    Selected_Instance_Tags.Add(Tag.ToString());
                }
            } 
            
            catch 
            {   Size Window_Size = new Size(516, 200);

                // Using the Costum Massage Box
                Show_Message_Box_One_Button(Window_Size, Xml_Name, null);                      
            }

            return Selected_Instance_Tags;
        }



        //=====================//

        List<string> Load_Xml_File(string Xml_Name)
        {   // Removing all listed items from the List View 
            List_View_Selected_Tag.Items.Clear();

            
            try
            {
                foreach (string Line in File.ReadLines(Xml_Name))
                {
                    
                    string New_Line = Line;

                    // We remove all (t)abulator keys and emptyspace, except for comments with <!-- --> and Attributes
                    if (!Line.Contains(@"<!--") & !Line.Contains(@"-->") & !Line.Contains(@"Name="""))
                    {   
                        // Removing all Tab key values
                        string Untabbed = Regex.Replace(Line, @"\t", "");
                        // Removing all Emptyspace values
                        New_Line = Regex.Replace(Untabbed, @" ", "");
                      
                    }                   
                    else if (Line.Contains(@"Name="""))
                    {
                        string Untabbed = Regex.Replace(Line, @"\t", "");
                        New_Line = Regex.Replace(Untabbed, @".*?<", "<");
                    }
                  


                    // Drawing all of them in our List View                        
                    var Item = List_View_Selected_Tag.Items.Add(New_Line);


                    Item.ForeColor = Color.Black;

                    // Every second item should have this value in order to create a checkmate pattern with good contrast
                    if (Checkmate_Color == "true" & Item.Index % 2 == 0)
                    {
                        Item.ForeColor = Color_03;
                        Item.BackColor = Color_07;
                    }
                              

                    // After drawing it, we seperate the Xml tags from their values:
                    // Removing all <XML_Tags> to get the value only
                    string The_Value = Regex.Replace(New_Line, @"<.*?>", "");
                    
                    // Highlighting boolean type values, disabled because triggers to highlight wrong tags
                    if (The_Value == "Yes" | The_Value == "No" | The_Value == "yes" | The_Value == "no") { Item.ForeColor = Color_02; }
                    else if (The_Value == "True" | The_Value == "False" | The_Value == "true" | The_Value == "false") { Item.ForeColor = Color_02; }
                    
                    // Marking commented lines in green
                    else if (Line.Contains("<!--") | Line.Contains("-->")) { Item.ForeColor = Color.Green; }

        
                    /*
                    // If the current line doesen't contain any tags I originally wanted to assign the value in a single line to the tag above, didnt worked
                    if (!Regex.IsMatch(Line, "<.*?>") & !Line.Contains("\t") & Line !=  "")
                    {   // We get the count of the newest entry
                        
                        // var Last_Item = Selected_Instance_Tags.ElementAt(Selected_Instance_Tags.Count());
                        // And add our line to the entry above, in orer to align all values in the same line by eachother.
                        // Last_Item += Line; 
                    } 
                    */

                    // Adding tag + value of this line to a List
                    Selected_Instance_Tags.Add(New_Line);
                }
            }

            catch
            {             
                Size Window_Size = new Size(516, 200);

                // Using the Costum Massage Box
                Show_Message_Box_One_Button(Window_Size, Xml_Name, null);
            }


            return Selected_Instance_Tags;

        }
        //=====================//

        string Load_Instance_Tag(string Xml_Name, string Current_Unit, string Desired_Tag)
        {   
            // Preparing Return Value
            string Current_Selection = "";
            
            int Xml_Entries = 0;


            try
            {
                //======== Loading .Xml File ==========     
                XElement Selected_Xml_File = XElement.Load(Xml_Name);

                // If we made it so far this means the xml was found, otherwise the 
                Xml_Entries = 1;

                // Defining the Variable we are going to use below
                IEnumerable<XElement> Entries;
             
           
                /* // temp
                if (Desired_Tag == "Unit_Name")
                {   // No idea why, need to get the name again or Linq wont allow me to modify the damn tag -.- otherwise it says "Sequence contains more than one matching element"
                    Entries = from All_Tags in Selected_Xml_File.Descendants(Unit_Type)               
                              where (string)All_Tags.Attribute("Name").Value == Current_Unit
                              select All_Tags;

                    foreach (XElement Tags in Entries)
                    {
                        // Putting these Element of the Tags into a string Variable
                        Current_Selection = (string)Tags.FirstAttribute.Value;
                    }      
                }*/

                // Projectile is a special one because it is outside of the Xml Unit
                if (Desired_Tag == "Projectile_Damage") 
                { 
                     Entries = Selected_Xml_File.Elements("Projectile")
                        .Select(Tag => Tag.Descendants(Desired_Tag).First()).ToList();
                 
                } else {
                // Selecting the right Unit and choosing the desired tag from it
                     Entries = Selected_Xml_File.Elements(Unit_Type)
                    .Where(item => item.Attribute("Name").Value == Current_Unit)
                    .Select(Tag => Tag.Descendants(Desired_Tag).First()).ToList();                  
                }


                foreach (var Entry in Entries)
                {
                    if (Entry.Name == Desired_Tag)
                    {
                        Current_Selection = Entry.Value;
                    }
                }
              
            }
            catch
            {
                if (Xml_Entries == 0)
                {   // Disabled because this spamms 20 error boxes -.-
                    // Xml_Exception_Files.Add(Xml_Name);
                    // Imperial_Console(600, 100, Add_Line + "    Could not find the Xml File, please press Refresh and try again.");                   
                }

                // This returns that no Variant Tag was found for this Unit
                else if (Desired_Tag == "Variant_Of_Existing_Type") { Current_Selection = "Is_No_Variant"; }
                // Otherwise we could not find the Tag and adding that tag into this List to forward it to the User
                else { Misloaded_Tags.Add(Desired_Tag); }
            }

            // Returning our Result Values
            return Current_Selection;  
        }

        //=====================//
        string Load_Operator_Tag(string Tag_Content, EventHandler Operator_Button_Event, bool Operator_Switch)
        {
            string Result = "";

            if (Tag_Content.Contains("-")) 
            {
                Result = Tag_Content.Replace("-", ""); // If the + - button is at + (true) we turn it to -
                if (Operator_Switch) { Operator_Button_Event(null, null); }
            }
            else // Otherwise it is a + value
            {   // And we need to make sure it toggles to +
                if (Operator_Switch == false) { Operator_Button_Event(null, null); }
                Result = Tag_Content;
            } 
      
            return Result;
        }

        //=====================//

        List<string> Load_Unit_Abilities(string Xml_Name)
        {

            try
            {
                //======== Loading .Xml File ==========     
                XElement Xml_File = XElement.Load(Xml_Name);


                // Getting the current Unit and selecting all its Tags
                var Tags = Xml_File.Elements()
                    .SelectMany(Entry => Entry.Elements().Descendants()).ToList();


                // Removing all listed items from the List Box 
                List_View_Selected_Tag.Items.Clear();

                foreach (var Tag in Tags)
                {

                    // Inserting the the Names in tags and the Value between them
                    List_View_Selected_Tag.Items.Add("<" + Tag.Name + ">" + Tag.Value + "</" + Tag.Name + ">");

                    // Adding it to a List
                    Selected_Instance_Tags.Add(Tag.ToString());
                }
            }
            catch { MessageBox.Show("The searched Tag was not found."); }

            return Selected_Instance_Tags;
        }
        //=====================//


        bool Edit_Max_Value_Text(string Text_Box) 
        {
            // Innitiating UI Textbox Element
            Control Text_Box_Element = this.Controls.Find(Text_Box, true).Single();

            
            // Making sure the User doesen't type any Letters // Here often StackOverfl0w happens because of missing Maximal_Values.txt files!!
            if (Regex.IsMatch(Text_Box_Element.Text, "[^0-9.,]"))
            {
                Imperial_Console(500, 100, Add_Line + "Please enter only numbers.");
                // Removing the wrong Text
                Text_Box_Element.Text = "";
            }

            int Typed_Value;
            Int32.TryParse(Text_Box_Element.Text, out Typed_Value);


            // Making sure the User doesen't type too low or too high values
            if (Typed_Value > 100000)
            {
                Imperial_Console(600, 100, Add_Line + "    Please enter a smaller value. Maximum = 100.000");
                Text_Box_Element.Text = "";
            }
            else if (Typed_Value < 0.9)
            {
                Text_Box_Element.Text = "";
            }

           
            //Depending on whether the user typed anything value we return true/false
            if (Text_Box_Element.Text != "") {return true;}
            else {return false;}
            
        }
        //=====================//


        int Check_Numeral_Text_Box(Control Text_Box, int Maximum_Value, bool Is_Decimal) 
        {
            // Innitiating UI Textbox Elements
            // Control Text_Box_Element = this.Controls.Find(Text_Box, true).Single();
           
 
            // Making sure the User doesen't type any Letters
            if (System.Text.RegularExpressions.Regex.IsMatch(Text_Box.Text, "[^0-9.,]"))
            {
                if (User_Input)
                {   Imperial_Console(500, 100, Add_Line + "Please enter only numbers.");
                    // Removing the wrong Text
                    Text_Box.Text = "";              
                }                          

                // Exclusively for the Add Money Box 
                if (Maximum_Value == Maximum_Credits) Player_Credits = 0;
            }


            int Typed_Value;
            Int32.TryParse(Text_Box.Text, out Typed_Value);


            // Making sure the User doesen't type too low or too high values
            if (Typed_Value > Maximum_Value)
            {
                if (User_Input)
                {
                    Text_Box.Text = "";
                    Imperial_Console(600, 100, Add_Line + "    Please enter a smaller value or change the Max Settings."
                                                           + Add_Line + "    Maximum = " + Maximum_Value); 
                }
                // If triggered by the Loading Function, we add it to a list that notifies the User
                // else { Error_List.Add(Text_Box.Name.ToString()); }                
            }
            else if (User_Input & !Is_Decimal & Typed_Value < 0.9)
            {
                Text_Box.Text = "";

                //Hiding the Money sign
                if (Maximum_Value == Maximum_Credits) Label_Credit_Sign.Text = "";
            }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }

            return Typed_Value;
        }


        
        //=====================//
        decimal Set_From_Float_Text_Box(Control Text_Box, TrackBar Track_Bar, ProgressBar Progress_Bar, int Maximum_Value, int Number, bool Check_Maximum) 
        {
            decimal Typed_Value = 0;
            int Selected_Value = 0;
              
            // Making sure the User doesen't type any Letters
            if (Regex.IsMatch(Text_Box.Text, "[^0-9.,]") & User_Input)
            {
                Imperial_Console(500, 100, Add_Line + "Please enter only numbers.");
                // Removing the wrong Text
                Text_Box.Text = "";
                return Typed_Value;
            }

         
            decimal.TryParse(Text_Box.Text, out Typed_Value);          
            Selected_Value = (int)(Typed_Value * 10);

            /*
            // If contains a decimal sign but not on the last place 
            if (Text_Box.Text.Contains(".") & !Regex.IsMatch(Text_Box.Text, ".*?." + "?")) { Selected_Value = (int)(Typed_Value * Number); }
            else { Selected_Value = (int) Typed_Value; }
             */


            if (Typed_Value > (Maximum_Value * 10) & Maximum_Value != 0) 
            {   // Setting both bars to maximum
                Progress_Bar.Value = Progress_Bar.Maximum;
                Track_Bar.Value = Track_Bar.Maximum;

                if (User_Input & Check_Maximum) 
                {   Text_Box.Text = "";
                    Imperial_Console(700, 100, Add_Line + @"    Please enter a smaller ""decimal"" value or change the Max Settings."  
                                                           + Add_Line + "    Maximum = " + Maximum_Value);
                }
            }
            else
            {  try
                {
                    Progress_Bar.Maximum = Maximum_Value * Number;
                    Track_Bar.Maximum = Maximum_Value * Number;

                    Track_Bar.Value = Selected_Value;                
                    Progress_Bar.Value = Selected_Value;
                } catch { }
            } 

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
            return Typed_Value;
        }

        //=====================//
        void Set_From_Decimal_Text_Box(Control Text_Box, TrackBar Track_Bar, ProgressBar Progress_Bar, int Maximum_Value, int Number, bool Check_Maximum)
        {
            decimal Typed_Value = 0;

            // Making sure the User doesen't type any Letters
            if (System.Text.RegularExpressions.Regex.IsMatch(Text_Box.Text, "[^0-9.,]") & User_Input)
            {
                Imperial_Console(500, 100, Add_Line + "Please enter only numbers.");
                // Removing the wrong Text
                Text_Box.Text = "";
                return;
            }


            decimal.TryParse(Text_Box.Text, out Typed_Value);

            int Selected_Value = (int)Typed_Value;


            if (Typed_Value > Maximum_Value & Maximum_Value != 0 & User_Input)
            {   // Setting both bars to maximum
                Progress_Bar.Value = Progress_Bar.Maximum;
                Track_Bar.Value = Track_Bar.Maximum;

                if (User_Input & Check_Maximum)
                {
                    Text_Box.Text = "";
                    Imperial_Console(700, 100, Add_Line + @"    Please enter a smaller ""decimal"" value or change the Max Settings."
                                                        + Add_Line + "    Maximum = " + Maximum_Value);
                }
            }
            else
            {   try
                {   Track_Bar.Maximum = Maximum_Value / Number;                   
                    Track_Bar.Value = Selected_Value / Number;

                    Progress_Bar.Maximum = Maximum_Value;          
                    Progress_Bar.Value = Selected_Value;                 
                } catch { }
            }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }          
        }
                   
        //=====================//

        // This auto sets a value for a similar tag to the first one selected by the UI
        void Process_Tag_Value(ProgressBar Progress_Bar_Percent, TextBox Target_Text_Box, int Maximal_Value_A, int Maximal_Value_B)
        {
            // If the percentage of the progress box is bigger then 80%, we autoselect the full shield refresh rate strengh
            if (Progress_Bar_Percent.Value > (Maximal_Value_A / 100) * 80) { Target_Text_Box.Text = Maximal_Value_B.ToString(); }
            // If between 60 and 80%
            else if (Progress_Bar_Percent.Value > (Maximal_Value_A / 100) * 60 & Progress_Bar_Percent.Value < (Maximal_Value_A / 100) * 80) { Target_Text_Box.Text = ((Maximal_Value_B / 5) * 4).ToString(); }
            else if (Progress_Bar_Percent.Value > (Maximal_Value_A / 100) * 40 & Progress_Bar_Percent.Value < (Maximal_Value_A / 100) * 60) { Target_Text_Box.Text = ((Maximal_Value_B / 5) * 3).ToString(); }
            else if (Progress_Bar_Percent.Value > (Maximal_Value_A / 100) * 20 & Progress_Bar_Percent.Value < (Maximal_Value_A / 100) * 40) { Target_Text_Box.Text = ((Maximal_Value_B / 5) * 2).ToString(); }
            else if (Progress_Bar_Percent.Value > 0 & Progress_Bar_Percent.Value < (Maximal_Value_A / 100) * 20) { Target_Text_Box.Text = ((Maximal_Value_B / 5)).ToString(); }
            else if (Progress_Bar_Percent.Value == 0) { Target_Text_Box.Text = ""; }
        }


        //=====================//

        // This provides balancing information for the user in the UI
        void Process_Balancing_Percentage()
        {   try 
            {   int Hull_Value = 100;
                if (Progress_Bar_Hull.Value != Progress_Bar_Hull.Maximum & Progress_Bar_Hull.Value != 0) { Hull_Value = (Progress_Bar_Hull.Value / (Maximum_Hull / 100)); }

                int Shield_Value = 100;
                if (Progress_Bar_Shield.Value != Progress_Bar_Shield.Maximum & Progress_Bar_Shield.Value != 0) { Shield_Value = (Progress_Bar_Shield.Value / (Maximum_Shield / 100)); }

                int Speed_Value = 100; // * 10 and * 100 to get away from decimal values after division, because result needs to be int, then / 100 to get the percentage
                if (Progress_Bar_Speed.Value != Progress_Bar_Speed.Maximum & Progress_Bar_Speed.Value != 0) { Speed_Value = ((Progress_Bar_Speed.Value * 10) / ((Maximum_Speed * 100) / 100)); }

                int AI_Combat_Value = 100;
                if (Progress_Bar_AI_Combat.Value != Progress_Bar_AI_Combat.Maximum & Progress_Bar_AI_Combat.Value != 0) { AI_Combat_Value = (Progress_Bar_AI_Combat.Value / (Maximum_AI_Combat / 100)); }

                int Population_Value = 100; // * 100 to get rid of decimal value and / 20 to get the percentage
                if (Progress_Bar_Population.Value != Progress_Bar_Population.Maximum & Progress_Bar_Population.Value != 0) { Population_Value = ((Progress_Bar_Population.Value * 100) / 20); }
                // If in minus mode we count that amount of balancing strengh away, otherwise its normal addition
                if (Toggle_Operator_Population == false) { Population_Value = -Population_Value; }

                int Projectile_Value = 200; // Counts * 2 because Projectile has a huge impact on balancing!
                if (Progress_Bar_Projectile.Value != Progress_Bar_Projectile.Maximum & Progress_Bar_Projectile.Value != 0) { Projectile_Value = ((Progress_Bar_Projectile.Value * 100) / Maximum_Projectile) * 2; }


                // /7 because they are 6 percentage fields (600%) that needs to be scaled down to 100% maximum, and Projectile_Value counts 200% (thus 700) because it is very important
                Label_Current_Balancing.Text = ((Hull_Value + Shield_Value + Speed_Value + AI_Combat_Value + Population_Value + Projectile_Value) / 7).ToString() + "%";
            } catch { }
        }

        //=====================//


        // Searches all .xmls for spawnable units, it needs most performances of all functions in this application.
        void Refresh_Units()
        {
            Application.UseWaitCursor = true;
            Application.DoEvents();

            // Depending on the selection of the Unit Type Filter Combo Box, the Function will search all xmls for these kind of units:
            // string Selected_Type = Combo_Box_Filter_Type.Text;


            if (User_Input & Combo_Box_Filter_Type.Text == "")
            {
                Size Window_Size = new Size(516, 200);

                string Message_Text = "The Combo Box for Unit Type Filter is empty, \n   please select anything.";

                // Using the Costum Massage Box and exiting the function
                Show_Message_Box_One_Button(Window_Size, null, Message_Text);

                Application.UseWaitCursor = false;
                Application.DoEvents();

                return;
            }

            

            // Storing Settings for next Usage --> Disabled due to interferences with Save Button
            // if (Combo_Box_Filter_Type.Text != "") { Save_Setting(Setting, "Unit_Type", Selected_Type); }
 

            // Clearing Variable from last usge
            Save_Setting(Setting, "Selected_Units", "");


            //===============  Adding Items to the lower list:

            // Removing all listed items from the listbox in order to refresh
            List_Box_All_Instances.Items.Clear();


            foreach (var Xml in Get_Xmls())
            {
                // To prevent the program from crashing because of xml errors:          
                try
                {
                    // Loading the Xml File PATHS:
                    XElement Xml_File = XElement.Load(Xml);


                    // Defining a XElement List
                    IEnumerable<XElement> List_01 =

                    // Selecting all Tags with this string Value:
                    from All_Tags in Xml_File.Descendants(Combo_Box_Filter_Type.Text)
                    // Selecting all non empty tags (null because we need all selected)
                    where (string)All_Tags.Attribute("Name") != null
                    select All_Tags;

                    foreach (XElement Tags in List_01)
                    {
                        // Putting these Element of the Tags into a string Variable
                        string Selected_Tag = (string)Tags.FirstAttribute.Value;

                        if (!Selected_Tag.Contains("Death_Clone") & Selected_Tag != "") // We filter all Death clone instances out
                        {   // And listing it in its Listbox
                            List_Box_All_Instances.Items.Add(Selected_Tag);

                            // Setting Unit Cache
                            Selected_Units += Selected_Tag.ToString() + ",";
                        }                     
                    }
                    Save_Setting(Setting, "Selected_Units", Selected_Units);
                }

                catch
                {   // Adding Filename to List, later we show it to the User               
                    Xml_Exception_Files.Add(Path.GetFileName(Xml));
                }
            }

            if (Show_Load_Issues == "true" & User_Input)
            {
                Size Window_Size_01 = new Size(700, 400);
                // Using the Costum Massage Box
                Show_Message_Box_Xml_Fail(Window_Size_01); 
            }

            Xml_Exception_Files.Clear();

            Application.UseWaitCursor = false;
            Application.DoEvents();
        }
        
        //=====================//

        void Refresh_Selected_Xml()
        {         
            // Clearing Variable from last usge
            Save_Setting(Setting, "Selected_Units", "");


            // To prevent the program from crashing because of xml errors:          
            try
            {
                // Loading the Xml File PATHS:
                XElement Xml_File = XElement.Load(Selected_Xml);


                // Defining a XElement List
                IEnumerable<XElement> Content =

                // Selecting all Tags with this string Value:
                from All_Tags in Xml_File.Descendants(Combo_Box_Filter_Type.Text)
                // Selecting all non empty tags (null because we need all selected)
                where (string)All_Tags.Attribute("Name") != null
                select All_Tags;

                foreach (XElement Tags in Content)
                {
                    // Putting these Element of the Tags into a string Variable
                    string Selected_Tag = (string)Tags.FirstAttribute.Value;

                    // We filter all Death clone instances out
                    if (!Selected_Tag.Contains("Death_Clone") & Selected_Tag != "" & !List_Box_All_Instances.Items.Contains(Selected_Tag)) 
                    {   // And listing it in its Listbox
                        List_Box_All_Instances.Items.Add(Selected_Tag);                       
                    }
                }          
            }

            catch
            {   if (Show_Load_Issues == "true" & User_Input)
                {
                    Imperial_Console(600, 100, Add_Line + "Failed to load " + Path.GetFileName(Selected_Xml));
                }       
            }

        }
           
        //=====================//
      
        public string[] Load_Behavoir_Pool()
        {
            string[] Behavoir_Pool = new string[] {};

            // Depending on the selection of the Unit Type Filter Combo Box, the Function will search all xmls for these kind of units:
            if (Combo_Box_Type.Text == "SpaceUnit" | Combo_Box_Type.Text == "UniqueUnit" | Combo_Box_Type.Text == "Squadron" | Combo_Box_Type.Text == "HeroCompany" | Combo_Box_Type.Text == "TransportUnit")
            {   // Defining Behavoir Pool, according to unit type
                Behavoir_Pool = new string[] { "ABILITY_COUNTDOWN", "SPAWN_SQUADRON", "SIMPLE_SPACE_LOCOMOTOR", "POWERED", "SHIELDED", "TARGETING", "HIDE_WHEN_FOGGED", "REVEAL", "UNIT_AI", "ASTEROID_FIELD_DAMAGE", "DAMAGE_TRACKING", "ION_STUN_EFFECT", "NEBULA", "SELF_DESTRUCT", "VEHICLE_THIEF"};
                Behavoir_Tag = "SpaceBehavior";
            }

            // For the Land Units
            else if (Combo_Box_Type.Text == "HeroUnit" | Combo_Box_Type.Text == "GroundInfantry" | Combo_Box_Type.Text == "GroundVehicle" | Combo_Box_Type.Text == "GroundCompany")
            {
                Behavoir_Pool = new string[] { "WALK_LOCOMOTOR", "LAND_TEAM_INFANTRY_LOCOMOTOR", "LAND_TEAM_CONTAINER_LOCOMOTOR", "FLYING_LOCOMOTOR", "IDLE", "TARGETING", "TEAM_TARGETING", "WEAPON", "POWERED", "SHIELDED", "ABILITY_COUNTDOWN", "AFFECTED_BY_SHIELD", 
                        "TREAD_SCROLL", "SPAWN_SQUADRON", "REVEAL", "HIDE_WHEN_FOGGED", "SQUASH", "SURFACE_FX", "UNIT_AI", "TELEKINESIS_TARGET", "STUNNABLE", "WIND_DISTURBANCE", "DAMAGE_TRACKING", "SELF_DESTRUCT", "TURRET", "GARRISON_UNIT", "GARRISON_HOVER", "DEPLOY_TROOPERS", "VEHICLE_THIEF" };
                Behavoir_Tag = "LandBehavior";
            }

            // For Space Buildings
            if (Combo_Box_Type.Text == "StarBase" | Combo_Box_Type.Text == "SpaceBuildable" | Combo_Box_Type.Text == "SpecialStructure" | Combo_Box_Type.Text == "Squadron")
            {
                Behavoir_Pool = new string[] { "SPACE_OBSTACLE", "ABILITY_COUNTDOWN", "SELECTABLE", "POWERED", "SHIELDED", "TURRET", "TARGETING", "WEAPON", "DAMAGE_TRACKING", "SPAWN_SQUADRON", "HIDE_WHEN_FOGGED", 
                                               "REVEAL", "UNIT_AI", "ION_STUN_EFFECT", "STUNNABLE", "SPECIAL_WEAPON", "SELF_DESTRUCT"};
                Behavoir_Tag = "SpaceBehavior";
            }

            // For Ground Buildings
            if (Combo_Box_Type.Text == "TechBuilding" | Combo_Box_Type.Text == "GroundBase" | Combo_Box_Type.Text == "GroundStructure" | Combo_Box_Type.Text == "GroundBuildable")
            {
                Behavoir_Pool = new string[] { "LAND_OBSTACLE", "DUMMY_LAND_BASE_LEVEL_COMPONENT", "EARTHQUAKE_TARGET", "TELEKINESIS_TARGET", "SURFACE_FX", "WIND_DISTURBANCE", "TERRAIN_TEXTURE_MODIFICATION", "TURRET", 
                                               "TARGETING", "WEAPON", "LOBBING_SUPERWEAPON", "AFFECTED_BY_SHIELD", "TREAD_SCROLL", "SELF_DESTRUCT"};
                Behavoir_Tag = "LandBehavior";
            }
          
            else if (Combo_Box_Type.Text == "Projectile")
            {
                Behavoir_Pool = new string[] { "PROJECTILE", "HIDE_WHEN_FOGGED" };
                Behavoir_Tag = "Behavior";
            }
        
            return Behavoir_Pool;
        }
        //=====================//


        void Load_Unit_To_UI(string Selected_Xml, string Unit)
        {            
            // Making sure this only fires if any rescent Unit was edited
            if (Selected_Xml != "" & Unit != "")
            {
                if (Application.UseWaitCursor == false)
                {   Application.UseWaitCursor = true;
                    Application.DoEvents();
                }

                // Making sure the Textbox knows it was not the user who clicked the checkbox, so it doesent have to add the Variant tag
                User_Input = false;
                Loading_To_UI = true;

                // Needs to run under "User_Input = false" because otherwise it causes the "Save" function to have no Selected_XML for saving and finally fail. 
                if (Loading_Completed) { Button_Create_New_Xml_Click(null, null); }


                          
                // Passing the Unit Name and Type values from the instance above into their UI Boxes:
                Text_Box_Name.Text = Unit;
                // Loading Type according to the Search Settings, used to be disabled because that prevents Combo_Box_Type from acting on its own
                Combo_Box_Type.Text = Unit_Type;

                // This loading part needs to be over the other tags, or its code will overwrite loaded tags like Layer_Z_Adjust and other class relevant things
                Combo_Box_Class.Text = Load_Instance_Tag(Selected_Xml, Unit, "Ship_Class"); 
                              
                Temporal_A = Combo_Box_Class.Text;

                if (Temporal_A == "") // If doesen't have that tag to load we try alternatives
                {   Combo_Box_Class.Text = Load_Instance_Tag(Selected_Xml, Unit, "Space_Layer");
                    Temporal_A = Combo_Box_Class.Text;
                }

               
                //======================== Loading the Squadron/Team =========================


                if (Temporal_A.Contains("Fighter") | Temporal_A.Contains("Bomber") | Unit_Mode == "Ground")  
                {
                    // Assigning the type of Team or Squadron we are going to search for
                    if (Temporal_A.Contains("Fighter") | Temporal_A.Contains("Bomber")) { Team_Type = "Squadron"; Team_Units = "Squadron_Units"; }
                    else if (Temporal_A.Contains("HeroUnit") | Temporal_A.Contains("SpaceHero") | Temporal_A.Contains("LandHero")) { Team_Type = "HeroCompany"; Team_Units = "Company_Units"; }
                    else if (Unit_Mode == "Ground") { Team_Type = "GroundCompany"; Team_Units = "Company_Units"; }

                    // Clearing List
                    Error_List.Clear();
               

                    int Highest_Rated = 0;
                    string Team_Xml_File = "";
                    bool List_Contains_This = false;
         

                    // Getting the currently selected Item
                    if (List_View_Teams.SelectedItems.Count > 0)
                    { Temporal_A = List_View_Teams.SelectedItems[0].Text; }


                    foreach (var Xml in Get_Xmls())
                    {   try // Preventing the program from crashing because of xml errors
                        {
                            // ===================== Opening Xml File =====================
                            XDocument Xml_File = XDocument.Load(Xml, LoadOptions.PreserveWhitespace);

                            var Team_Objects =
                                from All_Tags in Xml_File.Descendants(Team_Type)
                                // Selecting all Instances that have any Team_Units inside
                               
                                //  where All_Tags.Descendants(Team_Units).Any() 
                                // where All_Tags.Element(Team_Units).Value.Contains(Unit)                               
                                select All_Tags;


                            // =================== Checking Xml Instance ===================
                            foreach (XElement Team_Instance in Team_Objects)
                            {   // If our Unit is part of any Team/Squadron Team
                                if (Team_Instance.Elements(Team_Units).Any())
                                {
                                    if (Team_Instance.Element(Team_Units).Value.Contains(Unit))
                                    {
                                        var Item = List_View_Teams.Items.Add((string)Team_Instance.Attribute("Name"));


                                        // Adding all Teams of which this team is a Variant
                                        if (Team_Instance.Elements("Variant_Of_Existing_Type").Any())
                                        {   Temporal_B = Team_Instance.Element("Variant_Of_Existing_Type").Value;

                                            List_Contains_This = false;

                                            foreach (string Entry in List_View_Teams.Items)
                                            { if (Entry == Temporal_B) { List_Contains_This = true; break; }; }


                                            //  If the value is not empty and if it is not already in that list
                                            if (Temporal_B != null & Temporal_B != "" & !List_Contains_This) // !List_View_Teams.Text.Contains(Temporal_B))
                                            {
                                                List_View_Teams.Items.Add(Team_Instance.Element("Variant_Of_Existing_Type").Value);
                                            }
                                        }

                                        Item.ForeColor = Color.Black;

                                        // Every second item should have this value in order to create a checkmate pattern with good contrast
                                        if (Checkmate_Color == "true" & Item.Index % 2 == 0 & Temporal_A != (string)Team_Instance.Attribute("Name"))
                                        {
                                            Item.ForeColor = Color_03;
                                            Item.BackColor = Color_07;
                                        }


                                        // And if this one has the most Descendant tags, that means it is probably the one with full code and no variant_of_existing_type.                         
                                        if (Team_Instance.Descendants().Count() > Highest_Rated)
                                        {
                                            Highest_Rated = Team_Instance.Descendants().Count();
                                            Team_Xml_File = Xml;
                                            Text_Box_Team_Name.Text = (string)Team_Instance.Attribute("Name");
                                        }

                                    }                              
                                }
                               
                                else if (List_View_Teams.Items.Count > 0)
                                {                                                                             
                                    // For each object we already found above we do another query, and if it is a variant of them it will be added to our squadron list
                                    for (int i = List_View_Teams.Items.Count - 1; i >= 0; --i)
                                    {                                       
                                        Temporal_A = List_View_Teams.Items[i].Text;
                                        List_Contains_This = false;

                                        foreach (string Entry in List_View_Teams.Items)
                                        { if (Entry == Temporal_A) { List_Contains_This = true; break; }; }


                                        // & !List_View_Teams.Text.Contains(Temporal_A))
                                        if (Team_Instance.Element("Variant_Of_Existing_Type").Value == Temporal_A & !List_Contains_This)
                                        { List_View_Teams.Items.Add((string)Team_Instance.Attribute("Name")); Imperial_Console(600, 100, Add_Line + Temporal_B); }
                                    }                                                                                                                                                                                       
                                }
                            
                            }
                        } catch { Error_List.Add(Path.GetFileName(Xml)); }                   
                    }
                 

                    // Telling the user about crashed .XMLs that may contain syntax errors
                    if (Loading_Completed & Show_Load_Issues == "true" & Error_List.Count() > 0) 
                    {
                        Temporal_A = "    Failed to load the following Xml files:" + Add_Line;
                        foreach (string Entry in Error_List)
                        {
                            Temporal_A += "    " + Entry + Add_Line;
                        }
                        Imperial_Console(700, 340, Temporal_A); 
                    }
                                 
                    // We load just the team with most Descendnant tag entries, if there is only one thats the one we use.
                    Load_Team_To_UI(Team_Xml_File, Text_Box_Team_Name.Text);

                    // At the end of Load_Team_To_UI() it is set to true, but we need it to be false in order to accept ALL values
                    User_Input = false; 

                    // Loading must have completed, or it would interfere with the correct UI startup!
                    if (Loading_Completed & Text_Box_Team_Name.Text != "" & !Is_Team) { if (Size_Expander_I == "false") { Button_Expand_I_Click(null, null); } Switch_Button_Is_Team_Click(null, null); } //Enabling Team Subtab
                    else if (!Loading_Completed & Text_Box_Team_Name.Text != "" & !Is_Team) { Launch_Team = true; } // Telling the MainWindow_Shown event to open team tab instead
                                    

                    // Clearing List
                    Error_List.Clear();               
                }



                //========================= Loading the actual Unit =========================     

                //============== Setting Variant_Of_Existing_Type ==============  

                string Variant_Value = Load_Instance_Tag(Selected_Xml, Unit, "Variant_Of_Existing_Type");


                if (Variant_Value == "Is_No_Variant")
                {
                    if (Check_Box_Is_Variant.Checked)
                    {
                        // Making sure the Checkbox is unchecked
                        Check_Box_Is_Variant.Checked = !Check_Box_Is_Variant.Checked;           
                        // Clearing Textbox
                        Text_Box_Is_Variant.Text = "";
                        Button_Open_Variant.Visible = false;
                    }
                }
                else if (Variant_Value != "" && Variant_Value != "Is_No_Variant")
                {
                    if (!Check_Box_Is_Variant.Checked)
                    {    // Checking the "Is Variant" Checkbox in order to enable the Textfield
                        Check_Box_Is_Variant.Checked = !Check_Box_Is_Variant.Checked;                 
                        Button_Open_Variant.Visible = true;
                    }

                    // Inserting the value for Parent Xml Instance into the UI Field
                    Text_Box_Is_Variant.Text = Variant_Value;
                }

                
                //========================= Loading Art Settings =========================  
                Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Space_Model_Name");
                // Using Regex to remove the suffix, the (?i) enables case insensitive search
                Temporal_B = Regex.Replace(Temporal_A, "(?i).alo", "");
                Text_Box_Model_Name.Text = Temporal_B;
                

                if (Text_Box_Icon_Name.Text == "") 
                { 
                    Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Icon_Name");
                    Temporal_B = Regex.Replace(Temporal_A, "(?i).tga", "");
                    Text_Box_Icon_Name.Text = Temporal_B;
                }
           
                if (Text_Box_Radar_Icon.Text == "") 
                { 
                    Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Radar_Icon_Name");
                    Temporal_B = Regex.Replace(Temporal_A, "(?i).tga", "");
                    Text_Box_Radar_Icon.Text = Temporal_B;
                }

                if (Text_Box_Radar_Size.Text == "") { Text_Box_Radar_Size.Text = Load_Instance_Tag(Selected_Xml, Unit, "Radar_Icon_Size"); }
                // TODO: <Is_Visible_On_Radar>


                if (Text_Box_Text_Id.Text == "") { Text_Box_Text_Id.Text = Load_Instance_Tag(Selected_Xml, Unit, "Text_ID"); }
                if (Text_Box_Encyclopedia_Text.Text == "") { Text_Box_Encyclopedia_Text.Text = Load_Instance_Tag(Selected_Xml, Unit, "Encyclopedia_Text"); }
                if (Text_Box_Unit_Class.Text == "") { Text_Box_Unit_Class.Text = Load_Instance_Tag(Selected_Xml, Unit, "Encyclopedia_Unit_Class"); }


                // Scrollable numeral values
                Text_Box_Scale_Factor.Text = Load_Instance_Tag(Selected_Xml, Unit, "Scale_Factor");

                Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Layer_Z_Adjust");
                Text_Box_Model_Height.Text = Load_Operator_Tag(Temporal_A, Button_Operator_Model_Height_Click, Toggle_Operator_Model_Height);

          
                Temporal_B = Load_Instance_Tag(Selected_Xml, Unit, "GUI_Hide_Health_Bar");
                // If Health + Shield Bar is deactivated we uncheck that checkbox 
                if (Temporal_B == "Yes" | Temporal_B == "YES" | Temporal_B == "yes" | Temporal_B == "yeS")
                { if (Check_Box_Health_Bar_Size.Checked) { Check_Box_Health_Bar_Size.Checked = false; } }

                else
                {   // Otherwise we make sure this tag is activated
                    if (Check_Box_Health_Bar_Size.Checked == false) { Check_Box_Health_Bar_Size.Checked = true; }
                 
                    // Then we can also proceed to import further details
                    Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "GUI_Bracket_Size");
                    if (Temporal_A == "0") { Combo_Box_Health_Bar_Size.Text = "Small"; }         
                    else if (Temporal_A == "2") { Combo_Box_Health_Bar_Size.Text = "Large"; }
                    else { Combo_Box_Health_Bar_Size.Text = "Medium"; }          
                }

                Text_Box_Health_Bar_Height.Text = Load_Instance_Tag(Selected_Xml, Unit, "GUI_Bounds_Scale");
                Text_Box_Select_Box_Scale.Text = Load_Instance_Tag(Selected_Xml, Unit, "Select_Box_Scale");

                Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Select_Box_Z_Adjust");
                Text_Box_Select_Box_Height.Text = Load_Operator_Tag(Temporal_A, Button_Operator_Select_Box_Height_Click, Toggle_Select_Box_Height);

               
                //========================= Loading Power Values =========================                   
                Text_Box_Hull.Text = Load_Instance_Tag(Selected_Xml, Unit, "Tactical_Health");

                Text_Box_Shield.Text = Load_Instance_Tag(Selected_Xml, Unit, "Shield_Points");

                // Checking whether God Mode is active
                Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Shield_Refresh_Rate");
                if (Temporal_A == "100000") 
                {   Toggle_Button(Switch_Button_God_Mode, "Button_On", "Button_Off", 0, true); Toggle_God_Mode = true; 
                    Text_Box_Shield_Rate.Text = "God";              
                } 
                else
                {   Toggle_Button(Switch_Button_God_Mode, "Button_On", "Button_Off", 0, false); Toggle_God_Mode = false; 
                    Text_Box_Shield_Rate.Text = Temporal_A; 
                }
              

                Text_Box_Energy.Text = Load_Instance_Tag(Selected_Xml, Unit, "Energy_Capacity");
                Text_Box_Energy_Rate.Text = Load_Instance_Tag(Selected_Xml, Unit, "Energy_Refresh_Rate");

                Text_Box_Speed.Text = Load_Instance_Tag(Selected_Xml, Unit, "Max_Speed");
                Text_Box_Rate_Of_Turn.Text = Load_Instance_Tag(Selected_Xml, Unit, "Max_Rate_Of_Turn");
                Text_Box_Bank_Turn_Angle.Text = Load_Instance_Tag(Selected_Xml, Unit, "Bank_Turn_Angle");

                if (Text_Box_AI_Combat.Text == "") { Text_Box_AI_Combat.Text = Load_Instance_Tag(Selected_Xml, Unit, "AI_Combat_Power"); }

                if (Text_Box_Population.Text == "") 
                {   // Processing population value, according to these two tags:
                    Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Population_Value");
                    Temporal_B = Load_Instance_Tag(Selected_Xml, Unit, "Additional_Population_Capacity");

                    if (Temporal_B != "") // If existing we parse it to int 
                    {   Int32.TryParse(Temporal_A, out Temporal_C); 
                        Int32.TryParse(Temporal_B, out Temporal_D);

                        // If Additional Population Capacity is bigger then Population Capacity we take Additional Population Capacity instead  
                        if (Temporal_C < Temporal_D) 
                        {
                            Temporal_A = (Temporal_D - Temporal_C).ToString();
                            // Making sure Operator is set to +
                            if (Toggle_Operator_Population == false) { Button_Operator_Population_Click(null, null); }
                        }
                    }
                    else
                    {   // Otherwise there is no "Additional_Population_Capacity", making sure Operator is set to - 
                        if (Toggle_Operator_Population) { Button_Operator_Population_Click(null, null); }                  
                    }
                    Text_Box_Population.Text = Temporal_A;
                }

          
                if (Text_Box_Projectile.Text == "") 
                {   
                    Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Projectile_Damage");

                    if (Temporal_A != null & Temporal_A.Contains(".")) // Then it must be decimal
                    {
                        decimal Decimal_Text;
                        decimal.TryParse(Temporal_A, out Decimal_Text);
                        int Integer_Text = (int)Decimal_Text;
                        Text_Box_Projectile.Text = Integer_Text.ToString();
                    }
                    else if (Temporal_A != null ) { Text_Box_Projectile.Text = Temporal_A; }
                }

                Process_Balancing_Percentage();


                //========================== Loading Abilities =========================== 

                // Getting a list of all abilities of this unit:
                Temporal_E = Ability_Utility(Selected_Xml, Unit, null, null).Split(',');


                try 
                {   // Need to set these variables instead of the usual Textboxes, they get then loaded by the Ability buttons at the top
                    if (Temporal_E[0] != null)
                    {
                        Ability_1_Type = Temporal_E[0];
                        Ability_1_Activated_GUI = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "GUI_Activated_Ability_Name");

                        Temporal_A = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "Supports_Autofire");
                        // If this toggle is disabled and the Xml value is True we (indirectly) set that to true as well
                        if (Regex.IsMatch(Temporal_A, "(?i).*?" + "True")) { Ability_1_Toggle_Auto_Fire = true; }
                        else if (Regex.IsMatch(Temporal_A, "(?i).*?" + "False")) { Ability_1_Toggle_Auto_Fire = false; }


                        Temporal_A = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "Expiration_Seconds");
                        if (Temporal_A.Contains(".0")) { Temporal_A = Temporal_A.Replace(".0", ""); }
                        Ability_1_Expiration_Time = Temporal_A;

                        Temporal_B = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "Recharge_Seconds");
                        if (Temporal_B.Contains(".0")) { Temporal_B = Temporal_B.Replace(".0", ""); }
                        Ability_1_Recharge_Time = Temporal_B;


                        Ability_1_Name = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "Alternate_Name_Text");
                        Ability_1_Description = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "Alternate_Description_Text");

                        Temporal_A = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "Alternate_Icon_Name");
                        if (Regex.IsMatch(Temporal_A, "(?i).*?" + ".tga")) { Temporal_A = Temporal_A.Remove(Temporal_A.Length - 4); }
                        Ability_1_Icon = Temporal_A;

                        Ability_1_Activated_SFX = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "SFXEvent_GUI_Unit_Ability_Activated");
                        Ability_1_Deactivated_SFX = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "SFXEvent_GUI_Unit_Ability_Deactivated");

                        Ability_1_Mod_Multipliers = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "Mod_Multiplier");

                        Text_Box_Additional_Abilities.Text = Ability_Utility(Selected_Xml, Unit, Ability_1_Type, "Abilities"); 

                        // Pusing that button to display results of first ability
                        Button_Primary_Ability_Click(null, null);
                       
                    }              
                } catch { }


                try 
                {   if (Temporal_E[1] != null)
                    {
                        Ability_2_Type = Temporal_E[1];
                        Ability_2_Activated_GUI = Ability_Utility(Selected_Xml, Unit, Ability_2_Type, "GUI_Activated_Ability_Name");

                        Temporal_A = Ability_Utility(Selected_Xml, Unit, Ability_2_Type, "Supports_Autofire");
                        if (Regex.IsMatch(Temporal_A, "(?i).*?" + "True")) { Ability_2_Toggle_Auto_Fire = true; }
                        else if (Regex.IsMatch(Temporal_A, "(?i).*?" + "False")) { Ability_2_Toggle_Auto_Fire = false; }

                        Temporal_A = Ability_Utility(Selected_Xml, Unit, Ability_2_Type, "Expiration_Seconds");
                        if (Temporal_A.Contains(".0")) { Temporal_A = Temporal_A.Replace(".0", ""); }
                        Ability_2_Expiration_Time = Temporal_A;

                        Temporal_B = Ability_Utility(Selected_Xml, Unit, Ability_2_Type, "Recharge_Seconds");
                        if (Temporal_B.Contains(".0")) { Temporal_B = Temporal_B.Replace(".0", ""); }
                        Ability_2_Recharge_Time = Temporal_B;

                        Ability_2_Name = Ability_Utility(Selected_Xml, Unit, Ability_2_Type, "Alternate_Name_Text");
                        Ability_2_Description = Ability_Utility(Selected_Xml, Unit, Ability_2_Type, "Alternate_Description_Text");

                        Temporal_A = Ability_Utility(Selected_Xml, Unit, Ability_2_Type, "Alternate_Icon_Name");
                        if (Regex.IsMatch(Temporal_A, "(?i).*?" + ".tga")) { Temporal_A = Temporal_A.Remove(Temporal_A.Length - 4); }
                        Ability_2_Icon = Temporal_A;

                        Ability_2_Activated_SFX = Ability_Utility(Selected_Xml, Unit, Ability_2_Type, "SFXEvent_GUI_Unit_Ability_Activated");
                        Ability_2_Deactivated_SFX = Ability_Utility(Selected_Xml, Unit, Ability_2_Type, "SFXEvent_GUI_Unit_Ability_Deactivated");

                        Ability_2_Mod_Multipliers = Ability_Utility(Selected_Xml, Unit, Ability_2_Type, "Mod_Multiplier");
                    }                
                }  catch { }


                // Disabling User Input for Hyperspace Ability
                if (Text_Box_Mod_Multiplier.Text.Contains("<Spawned_Object_Type>Hyperspace_Jump_Target</Spawned_Object_Type>"))
                {   Track_Bar_Expiration_Seconds.Enabled = false;
                    Text_Box_Expiration_Seconds.Enabled = false;
                    Track_Bar_Recharge_Seconds.Enabled = false;
                    Text_Box_Recharge_Seconds.Enabled = false;
                }

                //======================= Loading Unit Properties ======================== 

                // Team is loaded by the Load_Team_To_UI() function 
               
                if (!Is_Team) // If is a Team these tags will be dealed by the Team/Squadron instance, hopefully
                {
                    Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Is_Named_Hero");
                    if (Temporal_A == "Yes") { Toggle_Button(Switch_Button_Is_Hero, "Button_On", "Button_Off", 0, true); Toggle_Is_Hero = true; }
                    else if (Temporal_A == "No") { Toggle_Button(Switch_Button_Is_Hero, "Button_On", "Button_Off", 0, false); Toggle_Is_Hero = false; }

                    Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Show_Hero_Head");
                    if (Temporal_A == "Yes") { Toggle_Button(Switch_Button_Show_Head, "Button_On", "Button_Off", 0, true); Toggle_Show_Head = true; }
                    else if (Temporal_A == "No") { Toggle_Button(Switch_Button_Show_Head, "Button_On", "Button_Off", 0, false); Toggle_Show_Head = false; }

                    if (Load_Instance_Tag(Selected_Xml, Unit, "Hyperspace") == "Yes") 
                    {   Check_Box_Has_Hyperspace.Checked = true;
                        Text_Box_Hyperspace_Speed.Text = Load_Instance_Tag(Selected_Xml, Unit, "Hyperspace_Speed");
                    }
                    else { Check_Box_Has_Hyperspace.Checked = false; }
                }


                Text_Box_Lua_Script.Text = Load_Instance_Tag(Selected_Xml, Unit, "Lua_Script");
                if (Text_Box_Lua_Script.Text == "ObjectScript_Hyper_Space") { Toggle_Button(Switch_Button_Use_Particle, "Button_On", "Button_Off", 0, true); Toggle_Use_Particle = true; }
                else { Toggle_Button(Switch_Button_Use_Particle, "Button_On", "Button_Off", 0, false); Toggle_Use_Particle = false; }


                if (Text_Box_Spawned_Unit.Text == "")
                {
                    Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Starting_Spawned_Units_Tech_0");
                    if (Temporal_A != null & Temporal_A != "")
                    {   try
                        {   Temporal_E = Temporal_A.Split(',');
                            Text_Box_Starting_Unit_Name.Text = Temporal_E[0];
                            Text_Box_Spawned_Unit.Text = Temporal_E[1];
                        } catch { }
                    } 
                }

                if (Text_Box_Reserve_Unit.Text == "")
                {
                    Temporal_B = Load_Instance_Tag(Selected_Xml, Unit, "Reserve_Spawned_Units_Tech_0");
                    if (Temporal_B != null & Temporal_B != "")
                    {   try
                        {   Temporal_E = Temporal_B.Split(',');
                            Text_Box_Reserve_Unit_Name.Text = Temporal_E[0];
                            Text_Box_Reserve_Unit.Text = Temporal_E[1];
                        } catch { }
                    }
                }

                Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Death_Clone");
                if (Temporal_A != "")
                {
                    Text_Box_Death_Clone.Text = Temporal_A.Replace("Damage_Normal, ", "");
                    Text_Box_Death_Clone.Text = Text_Box_Death_Clone.Text.Replace("Damage_Force_Whirlwind, ", "");

                    Temporal_B = Load_Instance_Tag(Selected_Xml, Text_Box_Death_Clone.Text, "Space_Model_Name");
                    // If Death Clone can't be found in the File we try to find it in any other file using the Utility
                    if (Temporal_B == null | Temporal_B == "") { Temporal_B = Xml_Utility(null, Text_Box_Death_Clone.Text, "Space_Model_Name"); }

                    try { Text_Box_Death_Clone_Model.Text = Regex.Replace(Temporal_B, "(?i).alo", ""); }
                    catch { } // Getting rid of suffix
                } 
                

                // Clearing both Listboxes and loading 
                List_Box_Inactive_Behavoir.Items.Clear();
                List_Box_Active_Behavoir.Items.Clear();


                string[] Unit_Behavoir = Load_Behavoir_Pool();

                // Adding to List Box (need to remove the matching ones later
                foreach (var Entry in Unit_Behavoir)
                { List_Box_Inactive_Behavoir.Items.Add(Entry); }




                //============ Getting the Space Bahavoir Tag and putting its value into a List Box Element.
                string Active_Behavoir = Load_Instance_Tag(Selected_Xml, Unit, Behavoir_Tag);


                string[] bits = Active_Behavoir.Split(',');

                foreach (string bit in bits)
                {
                    // Using Regex to get all Values into a List Box
                    string Remove_Emptyspace = Regex.Replace(bit, "[\n\r\t ]", "");

                    // To make sure we won't add any empty string
                    if (Remove_Emptyspace != "") List_Box_Active_Behavoir.Items.Add(Remove_Emptyspace);
                }



                //=========== Removing matching entries in the Pool of Inactive Behavoir 
                foreach (string Entry in Unit_Behavoir)
                {
                    // i = Amount of Entries -1 because the List starts at -1; until I reached 0; count -1 at each iteration
                    for (int i = List_Box_Active_Behavoir.Items.Count - 1; i >= 0; --i)
                    {
                        string Behavoir = List_Box_Active_Behavoir.Items[i].ToString();

                        // Removing the currently selected Tag from list box
                        if (Behavoir == Entry)
                        {   // Removing matched Items                                                                     
                            List_Box_Inactive_Behavoir.Items.Remove(Entry);
                        }
                    }
                }



                //====================== Loading Build Requirements ======================                 

                //========= Getting the Affiliation Tag and putting its value into a List Box Element.
                if (!Is_Team)
                {
                    List_Box_Inactive_Affiliations.Items.Clear();
                    List_Box_Active_Affiliations.Items.Clear();

              
                    Temporal_A = Xml_Utility(Selected_Xml, Unit, "Affiliation");

                    if (Temporal_A != null & Temporal_A != "")
                    {
                        Temporal_E = Temporal_A.Split(',');


                        foreach (string Entry in Temporal_E)
                        {
                            // Using Regex to get all Values into a List Box
                            Temporal_B = Regex.Replace(Entry, "[\n\r\t ]", "");

                            // To make sure we won't add any empty string
                            if (Temporal_B != "") List_Box_Active_Affiliations.Items.Add(Temporal_B);
                        }
                    }



                    //=========== Removing matching entries in the Pool of Unused Affiliations

                    string[] The_Factions = All_Factions.Split(',');

                    foreach (string Entry in The_Factions)
                    {
                        // First we add all these entries, and later if matched they get removed
                        if (Entry != "") { List_Box_Inactive_Affiliations.Items.Add(Entry); }

                        // i = Amount of Entries -1 because the List starts at -1; until I reached 0; count -1 at each iteration
                        for (int i = List_Box_Active_Affiliations.Items.Count - 1; i >= 0; --i)
                        {
                            string Affiliation = List_Box_Active_Affiliations.Items[i].ToString();

                            // Removing the currently selected Tag from list box
                            if (Affiliation == Entry)
                            {   // Removing matched Items                                                                     
                                List_Box_Inactive_Affiliations.Items.Remove(Entry);
                            }
                        }
                    }         
                

                    Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Build_Tab_Space_Units");

                    if (Temporal_A == "Yes") { Toggle_Build_Tab = true; Toggle_Button(Switch_Button_Build_Tab, "Button_On", "Button_Off", 0, true); }
                    else if (Temporal_A == "No") { Toggle_Build_Tab = false; Toggle_Button(Switch_Button_Build_Tab, "Button_On", "Button_Off", 0, false); }
                }

                // If all of these boxes are empty, otherwise it is probably already loaded from the Team/Squadron instance
                if (Text_Box_Build_Cost.Text == "") { Text_Box_Build_Cost.Text = Load_Instance_Tag(Selected_Xml, Unit, "Build_Cost_Credits"); }

                if (Text_Box_Skirmish_Cost.Text == "") { Text_Box_Skirmish_Cost.Text = Load_Instance_Tag(Selected_Xml, Unit, "Tactical_Build_Cost_Multiplayer"); }

                if (Text_Box_Build_Time.Text == "") { Text_Box_Build_Time.Text = Load_Instance_Tag(Selected_Xml, Unit, "Build_Time_Seconds"); }
                if (Text_Box_Tech_Level.Text == "") { Text_Box_Tech_Level.Text = Load_Instance_Tag(Selected_Xml, Unit, "Tech_Level"); }

                if (Text_Box_Star_Base_LV.Text == "") { Text_Box_Star_Base_LV.Text = Load_Instance_Tag(Selected_Xml, Unit, "Required_Star_Base_Level"); }
                if (Text_Box_Ground_Base_LV.Text == "") { Text_Box_Ground_Base_LV.Text = Load_Instance_Tag(Selected_Xml, Unit, "Required_Ground_Base_Level"); }

                if (Text_Box_Required_Timeline.Text == "") { Text_Box_Required_Timeline.Text = Load_Instance_Tag(Selected_Xml, Unit, "Required_Timeline"); }
                
                if (!Is_Team)
                {   Temporal_A = Load_Instance_Tag(Selected_Xml, Unit, "Build_Initially_Locked");
                    if (Temporal_A == "Yes") { Toggle_Innitially_Locked = true; Toggle_Button(Switch_Button_Innitially_Locked, "Button_On", "Button_Off", 0, true); }
                    else if (Temporal_A == "No") { Toggle_Innitially_Locked = false; Toggle_Button(Switch_Button_Innitially_Locked, "Button_On", "Button_Off", 0, false); }
                }

                if (Text_Box_Slice_Cost.Text == "") // If beginns empty no Team is probably loaded
                {   Text_Box_Slice_Cost.Text = Load_Instance_Tag(Selected_Xml, Unit, "Slice_Cost_Credits");
                    if (Text_Box_Slice_Cost.Text != "") { Check_Box_Slice_Cost.Checked = true; } // If textbox is meanwhile filled that means we can check it.
                }

                if (Text_Box_Current_Limit.Text == "") 
                {   Text_Box_Current_Limit.Text = Load_Instance_Tag(Selected_Xml, Unit, "Build_Limit_Current_Per_Player");
                    if (Text_Box_Current_Limit.Text != "") { Check_Box_Current_Limit.Checked = true; }
                }

                if (Text_Box_Lifetime_Limit.Text == "")
                {   Text_Box_Lifetime_Limit.Text = Load_Instance_Tag(Selected_Xml, Unit, "Build_Limit_Lifetime_Per_Player");
                    if (Text_Box_Lifetime_Limit.Text != "") { Check_Box_Lifetime_Limit.Checked = true; }
                }

                //======================================================================= 
                      
         
                User_Input = true;
                // Need this so the Button_Primary_Ability_Click() and Button_Secondary_Ability_Click() know when they are to leave User Input to false
                Loading_To_UI = false;

                if (Application.UseWaitCursor == true)
                {   Application.UseWaitCursor = false;
                    Application.DoEvents();
                }

                // Showing the Name of the Current Xml over the Editor Settings
                try { Label_Xml_Name.Text = Path.GetFileName(Selected_Xml); } catch { }

                // Moving the scrollbar down to focus on the editor UI
                Label_Power_Values.Focus();

       

                //================== Preparing "Out of Range" Error Messages for the User ==================
                string Error_Text = "";

                if (Error_List.Count > 0 & Show_Tag_Issues == "true") 
                {
                    foreach (string Error in Error_List) 
                    {  
                       if (Error == "Text_Box_Name") { Error_Text += "   <Unit_Name>" + "\n";}
                       else if (Error == "Combo_Box_Type") { Error_Text += "   " + Combo_Box_Type.Text + "  (Unit Type) \n"; }
                       else if (Error == "Text_Box_Is_Variant") { Error_Text += "   <Variant_Of_Existing_Type>" + "\n"; }

                       // Art Settings
                       else if (Error == "Text_Box_Model_Name") { Error_Text += "   <Space_Model_Name>" + "\n"; }
                       else if (Error == "Text_Box_Icon_Name") { Error_Text += "   <Icon_Name>" + "\n"; }
                       else if (Error == "Text_Box_Radar_Icon") { Error_Text += "   <Radar_Icon_Name>" + "\n"; }
                       else if (Error == "Text_Box_Radar_Size") { Error_Text += "   <Radar_Icon_Size>" + "\n"; }
                       else if (Error == "Text_Box_Text_Id") { Error_Text += "   <Text_ID>" + "\n"; }
                       else if (Error == "Text_Box_Unit_Class") { Error_Text += "   <Encyclopedia_Unit_Class>" + "\n"; }
                       else if (Error == "Text_Box_Encyclopedia_Text") { Error_Text += "   <Encyclopedia_Text>" + "\n"; }
                    
                       // Power Values
                       else if (Error == "Combo_Box_Class") { Error_Text += "   <Ship_Class>, " + "<Space_Layer>, " + "<CategoryMask>" + "  (Class) \n"; }
                       else if (Error == "Text_Box_Hull") { Error_Text += "   <Tactical_Health>" + "  (Hull) \n"; }
                       else if (Error == "Text_Box_Shield") { Error_Text += "   <Shield_Points>" + "  (Shield) \n"; }
                       else if (Error == "Text_Box_Shield_Rate") { Error_Text += "   <Shield_Refresh_Rate>" + "\n"; }
                       else if (Error == "Text_Box_Energy") { Error_Text += "   <Energy_Capacity>" + "  (Energy) \n"; }
                       else if (Error == "Text_Box_Energy_Rate") { Error_Text += "   <Energy_Refresh_Rate>" + "\n"; }
                       else if (Error == "Text_Box_AI_Combat") { Error_Text += "   <AI_Combat_Power>" + "  (AI Combat) \n"; }
                       else if (Error == "Text_Box_Projectile") { Error_Text += "   <Projectile_Damage>" + "\n "; }


                       // Costum Tags                Text_Box_Tag_Name.Text because this is how the Costum Tags are named
                       else if (Error == "Text_Box_Costum_Tag_1") { Error_Text += "   " + Text_Box_Tag_1_Name.Text + "\n"; }
                       else if (Error == "Text_Box_Costum_Tag_2") { Error_Text += "   " + Text_Box_Tag_1_Name.Text + "\n"; }
                       else if (Error == "Text_Box_Costum_Tag_3") { Error_Text += "   " + Text_Box_Tag_1_Name.Text + "\n"; }
                       else if (Error == "Text_Box_Costum_Tag_4") { Error_Text += "   " + Text_Box_Tag_1_Name.Text + "\n"; }
                       else if (Error == "Text_Box_Costum_Tag_5") { Error_Text += "   " + Text_Box_Tag_1_Name.Text + "\n"; }
                       else if (Error == "Text_Box_Costum_Tag_6") { Error_Text += "   " + Text_Box_Tag_1_Name.Text + "\n"; }

                       else { Error_Text += Error + ", "; }
                    }                
                }             
                                               
                //================== Preparing "Tag not Found" Error Messages for the User ==================
              
                string Misload_Text = "";

                if (Misloaded_Tags.Count > 0)
                {
                    foreach (string Failed_Tag in Misloaded_Tags)
                    {
                        Misload_Text += "   <" + Failed_Tag + "> \n";
                    }                                                   
                }


                //================== Displaying Error Message
                if (Error_List.Count > 0 & Show_Tag_Issues == "true" & Loading_Completed | Misloaded_Tags.Count > 0 & Show_Tag_Issues == "true" & Loading_Completed) 
                {
                    // Innitiating new Form
                    Caution_Window Display = new Caution_Window();
                    Display.Opacity = Transparency;
                    Display.Size = new Size(700, 400);

                    // Using Theme colors for Text and Background
                    Display.Text_Box_Caution_Window.BackColor = Color_05;
                    Display.Text_Box_Caution_Window.ForeColor = Color_03;

                    // If Error_List contains anything we show the first Message
                    if (Error_List.Count > 0)
                    { Display.Text_Box_Caution_Window.Text = "   (You can disable this message using \"Show Tag Issues\" in Settings.)\n   The Following Values are out of range, you can still lower them or increase \n   their Max settings and reload:" + "\n \n" + Error_Text; }
                    
                    // Then we add the second Message to the Message Box
                    if (Misloaded_Tags.Count > 0) { Display.Text_Box_Caution_Window.Text += "\n\n   Failed to load the Following Values (probably not found or is Variant): \n\n" + Misload_Text; }


                    Display.Show(); 
                }


                // Clearing Lists
                Error_List.Clear();
                Misloaded_Tags.Clear(); 
            }   
        }

        // ================== Team Subbox ==================//

        // Xml_File value can be null, then this function will search the Xml directory for the required Team/Squadron
        //Load_Team_To_UI(Team_Xml_File, Team_Name);
        void Load_Team_To_UI(string Team_Xml_File, string Team_Name)
        {
            User_Input = false;
            //Clear_Team_Values();

            Text_Box_Team_Name.Text = Team_Name;
            Combo_Box_Team_Type.Text = Team_Type;

            Selected_Team = Team_Name;


            string All_Team_Members = "";


            // If no source .xml was specified we try the selected Xml, which is a global variable that already updated above
            if (Team_Xml_File != null) { All_Team_Members = Xml_Utility(Team_Xml_File, Team_Name, Team_Units); }
            else
            {
                Team_Xml_File = Selected_Xml;
                All_Team_Members = Xml_Utility(Team_Xml_File, Team_Name, Team_Units);


                // If still not found we go search for it
                if (All_Team_Members == null | All_Team_Members == "")
                {
                    foreach (var Xml in Get_Xmls())
                    {
                        try
                        {   // ===================== Opening Xml File =====================
                            XDocument Xml_File = XDocument.Load(Xml, LoadOptions.PreserveWhitespace);

                            var Instances =
                                from All_Tags in Xml_File.Root.Descendants()
                                // Selecting all non empty tags that have the Attribute "Name", null because we need all selected.
                                where (string)All_Tags.Attribute("Name") == Team_Name
                                select All_Tags;


                            // =================== Checking Xml Instance ===================
                            foreach (XElement Instance in Instances)
                            {
                                // Getting the Xml of this Team
                                Team_Xml_File = Xml;

                                // Getting tag value of the Team members (just because we are already in that XElement)
                                if (Team_Units != null & Team_Units != "")
                                { try { All_Team_Members = Instance.Descendants(Team_Units).First().Value; } catch { } }

                                continue;
                            }
                        }
                        catch { }
                    }
                }
            }



            if (All_Team_Members != null)
            {
                Temporal_A = Regex.Replace(All_Team_Members, " ", "");
                Temporal_B = Regex.Replace(Temporal_A, @"\t", "");
                string[] Team_Members = Temporal_B.Split(',');


                Temporal_A = "";

                foreach (string Team_Member in Team_Members)
                { Temporal_A += Team_Member + ", " + Add_Line; }

                Text_Box_Team_Amount.Text = Team_Members.Count().ToString();
                Text_Box_Team_Members.Text = Temporal_A;
            }


            string Variant_Value = Xml_Utility(Team_Xml_File, Team_Name, "Variant_Of_Existing_Type");

            //============== Setting Variant_Of_Existing_Type ==============     
            if (Variant_Value == "" | Variant_Value == null)
            {
                if (Check_Box_Team_Is_Variant.Checked == true)
                {  // Making sure the Checkbox is unchecked
                    Check_Box_Team_Is_Variant.Checked = false;                                
                    Button_Open_Team_Variant.Visible = false;
                }
                // Clearing Textbox
                Text_Box_Team_Is_Variant.Text = "";
            }
            else if (Variant_Value != "" & Variant_Value != null)
            {
                if (Check_Box_Team_Is_Variant.Checked == false)
                {   // Checking the "Is Variant" Checkbox in order to enable the Textfield
                    Check_Box_Team_Is_Variant.Checked = true;             
                    Button_Open_Team_Variant.Visible = true;
                }
                // Inserting the value for Parent Xml Instance into the UI Field
                Text_Box_Team_Is_Variant.Text = Variant_Value;

                // And into the Squadron list if it doesen't already contain them.
                // if (!List_View_Teams.Text.Contains(Variant_Value)) { List_View_Teams.Items.Add(Variant_Value); }
            }



            Text_Box_Shuttle_Type.Text = Xml_Utility(Team_Xml_File, Team_Name, "Company_Transport_Unit");


            // For space Units we set a offset
            if (Combo_Box_Class.Text == "Fighter") { Text_Box_Team_Offsets.Text = "3"; }
            else if (Combo_Box_Class.Text == "Bomber") { Text_Box_Team_Offsets.Text = "4"; }



            // ======== Loading all tags (which have priority over the tag of the same name from the team member instance) ========
            // Needs to be Xml_Utility because Load_Instance_Tag() currently only loads from the teammember unit

            try {
       
            //======================= Loading some Art Settings ======================= 
            Text_Box_Text_Id.Text = Xml_Utility(Team_Xml_File, Team_Name, "Text_ID");
            Text_Box_Encyclopedia_Text.Text = Xml_Utility(Team_Xml_File, Team_Name, "Encyclopedia_Text");
            Text_Box_Unit_Class.Text = Xml_Utility(Team_Xml_File, Team_Name, "Encyclopedia_Unit_Class");

            // TODO: <Is_Visible_On_Radar>


            Text_Box_Icon_Name.Text = Xml_Utility(Team_Xml_File, Team_Name, "Icon_Name");
            Text_Box_Radar_Icon.Text = Xml_Utility(Team_Xml_File, Team_Name, "Radar_Icon_Name");
            Text_Box_Radar_Size.Text = Xml_Utility(Team_Xml_File, Team_Name, "Radar_Icon_Size");
     

            //========================= Loading Power Values =========================             
            Text_Box_AI_Combat.Text = Xml_Utility(Team_Xml_File, Team_Name, "AI_Combat_Power");

      
            // Processing population value, according to these two tags:
            Temporal_A = Xml_Utility(Team_Xml_File, Team_Name, "Population_Value");
            Temporal_B = Xml_Utility(Team_Xml_File, Team_Name, "Additional_Population_Capacity");

            if (Temporal_B != "") // If existing we parse it to int 
            {
                Int32.TryParse(Temporal_A, out Temporal_C);
                Int32.TryParse(Temporal_B, out Temporal_D);

                // If Additional Population Capacity is bigger then Population Capacity we take Additional Population Capacity instead  
                if (Temporal_C < Temporal_D)
                {
                    Temporal_A = (Temporal_D - Temporal_C).ToString();
                    // Making sure Operator is set to +
                    if (Toggle_Operator_Population == false) { Button_Operator_Population_Click(null, null); }
                }
            }
            else
            {   // Otherwise there is no "Additional_Population_Capacity", making sure Operator is set to - 
                if (Toggle_Operator_Population) { Button_Operator_Population_Click(null, null); }
            }
            Text_Box_Population.Text = Temporal_A;

            try 
            { 
                Temporal_A = Xml_Utility(Team_Xml_File, Team_Name, "Projectile_Damage");

                if (Temporal_A != null & Temporal_A.Contains(".")) // Then it must be decimal
                {
                    decimal Decimal_Text;
                    decimal.TryParse(Temporal_A, out Decimal_Text);
                    int Integer_Text = (int)Decimal_Text;
                    Text_Box_Projectile.Text = Integer_Text.ToString();
                }
                else if (Temporal_A != null) { Text_Box_Projectile.Text = Temporal_A; }
            } catch {}


            //======================= Loading Unit Properties ======================== 

            Temporal_A = Xml_Utility(Team_Xml_File, Team_Name, "Is_Named_Hero");
            if (Temporal_A == "Yes") { Toggle_Button(Switch_Button_Is_Hero, "Button_On", "Button_Off", 0, true); Toggle_Is_Hero = true; }
            else if (Temporal_A == "No") { Toggle_Button(Switch_Button_Is_Hero, "Button_On", "Button_Off", 0, false); Toggle_Is_Hero = false; }

            Temporal_A = Xml_Utility(Team_Xml_File, Team_Name, "Show_Hero_Head");
            if (Temporal_A == "Yes") { Toggle_Button(Switch_Button_Show_Head, "Button_On", "Button_Off", 0, true); Toggle_Show_Head = true; }
            else if (Temporal_A == "No") { Toggle_Button(Switch_Button_Show_Head, "Button_On", "Button_Off", 0, false); Toggle_Show_Head = false; }

            /* // Disabled because it is supposed to load from the Unit itself
            Text_Box_Lua_Script.Text = Xml_Utility(Team_Xml_File, Team_Name, "Lua_Script");
            if (Text_Box_Lua_Script.Text == "ObjectScript_Hyper_Space") { Toggle_Button(Switch_Button_Use_Particle, "Button_On", "Button_Off", 0, true); Toggle_Use_Particle = true; }
            else { Toggle_Button(Switch_Button_Use_Particle, "Button_On", "Button_Off", 0, false); Toggle_Use_Particle = false; }
            */


            if (Xml_Utility(Team_Xml_File, Team_Name, "Hyperspace") == "Yes")
            {
                Check_Box_Has_Hyperspace.Checked = true;
                Text_Box_Hyperspace_Speed.Text = Xml_Utility(Team_Xml_File, Team_Name, "Hyperspace_Speed");
            }
            else { Check_Box_Has_Hyperspace.Checked = false; }



            Temporal_A = Xml_Utility(Team_Xml_File, Team_Name, "Starting_Spawned_Units_Tech_0");
            if (Temporal_A != null & Temporal_A != "")
            {   try
                {   Temporal_E = Temporal_A.Split(',');
                    Text_Box_Starting_Unit_Name.Text = Temporal_E[0];
                    Text_Box_Spawned_Unit.Text = Temporal_E[1];
                } catch { }             
            }

            Temporal_B = Xml_Utility(Team_Xml_File, Team_Name, "Reserve_Spawned_Units_Tech_0");
            if (Temporal_B != null & Temporal_B != "")
            {   try
                {   Temporal_E = Temporal_B.Split(',');
                    Text_Box_Reserve_Unit_Name.Text = Temporal_E[0];
                    Text_Box_Reserve_Unit.Text = Temporal_E[1];
                } catch { }
            }

     
          
            //====================== Loading Build Requirements ====================== 

            //========= Getting the Affiliation Tag and putting its value into a List Box Element.
            List_Box_Inactive_Affiliations.Items.Clear();
            List_Box_Active_Affiliations.Items.Clear();


            Temporal_A = Xml_Utility(Team_Xml_File, Team_Name, "Affiliation");

            if (Temporal_A != null & Temporal_A != "")
            {   
                Temporal_E = Temporal_A.Split(',');

                foreach (string Entry in Temporal_E)
                {
                    // Using Regex to get all Values into a List Box
                    Temporal_B = Regex.Replace(Entry, "[\n\r\t ]", "");

                    // To make sure we won't add any empty string
                    if (Temporal_B != "") List_Box_Active_Affiliations.Items.Add(Temporal_B);
                }
            }



            //=========== Removing matching entries in the Pool of Unused Affiliations

            string[] The_Factions = All_Factions.Split(',');

            foreach (string Entry in The_Factions)
            {
                // First we add all these entries, and later if matched they get removed
                if (Entry != "") { List_Box_Inactive_Affiliations.Items.Add(Entry); }

                // i = Amount of Entries -1 because the List starts at -1; until I reached 0; count -1 at each iteration
                for (int i = List_Box_Active_Affiliations.Items.Count - 1; i >= 0; --i)
                {
                    string Affiliation = List_Box_Active_Affiliations.Items[i].ToString();

                    // Removing the currently selected Tag from list box
                    if (Affiliation == Entry)
                    {   // Removing matched Items                                                                     
                        List_Box_Inactive_Affiliations.Items.Remove(Entry);
                    }
                }
            }



            Temporal_A = Xml_Utility(Team_Xml_File, Team_Name, "Build_Tab_Space_Units");

            if (Temporal_A == "Yes") { Toggle_Build_Tab = true; Toggle_Button(Switch_Button_Build_Tab, "Button_On", "Button_Off", 0, true); }
            else if (Temporal_A == "No") { Toggle_Build_Tab = false; Toggle_Button(Switch_Button_Build_Tab, "Button_On", "Button_Off", 0, false); }


            Text_Box_Build_Cost.Text = Xml_Utility(Team_Xml_File, Team_Name, "Build_Cost_Credits");
            Text_Box_Skirmish_Cost.Text = Xml_Utility(Team_Xml_File, Team_Name, "Tactical_Build_Cost_Multiplayer");

            Text_Box_Build_Time.Text = Xml_Utility(Team_Xml_File, Team_Name, "Build_Time_Seconds");
            Text_Box_Tech_Level.Text = Xml_Utility(Team_Xml_File, Team_Name, "Tech_Level");

            Text_Box_Star_Base_LV.Text = Xml_Utility(Team_Xml_File, Team_Name, "Required_Star_Base_Level");
            Text_Box_Ground_Base_LV.Text = Xml_Utility(Team_Xml_File, Team_Name, "Required_Ground_Base_Level");

            Text_Box_Required_Timeline.Text = Xml_Utility(Team_Xml_File, Team_Name, "Required_Timeline");


            Temporal_A = Xml_Utility(Team_Xml_File, Team_Name, "Build_Initially_Locked");
            if (Temporal_A == "Yes") { Toggle_Innitially_Locked = true; Toggle_Button(Switch_Button_Innitially_Locked, "Button_On", "Button_Off", 0, true); }
            else if (Temporal_A == "No") { Toggle_Innitially_Locked = false; Toggle_Button(Switch_Button_Innitially_Locked, "Button_On", "Button_Off", 0, false); }

            Text_Box_Slice_Cost.Text = Xml_Utility(Team_Xml_File, Team_Name, "Slice_Cost_Credits");
            if (Text_Box_Slice_Cost.Text != "") { Check_Box_Slice_Cost.Checked = true; }

            Text_Box_Current_Limit.Text = Xml_Utility(Team_Xml_File, Team_Name, "Build_Limit_Current_Per_Player");
            if (Text_Box_Current_Limit.Text != "") { Check_Box_Current_Limit.Checked = true; }

            Text_Box_Lifetime_Limit.Text = Xml_Utility(Team_Xml_File, Team_Name, "Build_Limit_Lifetime_Per_Player");
            if (Text_Box_Lifetime_Limit.Text != "") { Check_Box_Lifetime_Limit.Checked = true; }



            } catch { Imperial_Console(600, 100, Add_Line + "    The Team/Squadron loading function crashed."
                                               + Add_Line + "    One or more tags caused the issue.");}
            //======================================================================== 

            Text_Box_Team_Members.Focus();

            Application.UseWaitCursor = false;
            Application.DoEvents();

            User_Input = true;
        }


        // =================================================//


        void Clear_Team_Values()
        {
            Control[] All_UI_Text_Boxes = 
            {   Combo_Box_Team_Type, Text_Box_Name, Text_Box_Team_Is_Variant, Text_Box_Shuttle_Type,
                Text_Box_Team_Amount, Text_Box_Team_Offsets, Text_Box_Team_Members,           
            };

            foreach (Control Text_Box in All_UI_Text_Boxes)
            {   // Setting all Textboxes empty if they arn't already
                if (Text_Box.Text != "") { Text_Box.Text = ""; }
            }

            List_View_Teams.Items.Clear();
        }


        //=====================/
        void Clear_Ability_Values()
        {
            Toggle_Button(Switch_Button_Auto_Fire, "Button_On", "Button_Off", 0, false); 
            Toggle_Auto_Fire = false;

            Control[] All_UI_Text_Boxes = 
            {   Combo_Box_Ability_Type, Combo_Box_Activated_GUI_Ability, 
                Text_Box_Expiration_Seconds, Text_Box_Recharge_Seconds, 
                Text_Box_Alternate_Name, Text_Box_Alternate_Description, Text_Box_Ability_Icon, 
                Text_Box_SFX_Ability_Activated, Text_Box_SFX_Ability_Deactivated, 
                Combo_Box_Mod_Multiplier, Text_Box_Mod_Multiplier,
                // They must not be used or they delete text when switching between primary and secondary ability:
                // Combo_Box_Additional_Abilities, Text_Box_Additional_Abilities          
            };

            foreach (Control Text_Box in All_UI_Text_Boxes)
            {   // Setting all Textboxes empty if they arn't already
                if (Text_Box.Text != "") { Text_Box.Text = ""; }
            }      
        }
  
        //=====================//

        void Show_Message_Box_Xml_Fail(Size Caution_Window_Size)
        {   //========== Displaying Error Messages to User
            string Error_Text = "";

            if (Xml_Exception_Files.Count > 0)
            {
                foreach (string Error in Xml_Exception_Files)
                {
                    Error_Text += "\n   " + Error + ", ";
                }
           


                // Innitiating new Form
                Caution_Window Display = new Caution_Window();

                Display.Opacity = Transparency;
                Display.Size = Caution_Window_Size;

                // Using Theme colors for Text and Background
                Display.Text_Box_Caution_Window.BackColor = Color_05;
                Display.Text_Box_Caution_Window.ForeColor = Color_03;

                Display.Text_Box_Caution_Window.Text = "   (You can deactivate this Message using \"Show Load Issues\" in Settings.)\n" + "   Failed to load the following Xmls:" + "\n   " + Error_Text;
                Display.Show();
            }
        }
        //=====================//

        void Show_Message_Box_One_Button(Size Caution_Window_Size, string The_Xml_File, string New_Text)
        {   
            //========== Displaying Error Messages to User   
            // Innitiating new Form
            Caution_Window Display = new Caution_Window();
            Display.Opacity = Transparency;
            Display.Size = Caution_Window_Size;

            // Using Theme colors for Text and Background
            Display.Text_Box_Caution_Window.BackColor = Color_05;
            Display.Text_Box_Caution_Window.ForeColor = Color_03;


            Display.Button_Caution_Box_1.Visible = true;
            Display.Button_Caution_Box_1.Text = "Ok";
            Display.Button_Caution_Box_1.Location = new Point(184, 86);

            if (New_Text == null)
            { Display.Text_Box_Caution_Window.Text = "   Failed to load" + "\n" + "   " + Path.GetFileName(The_Xml_File); }
            else { Display.Text_Box_Caution_Window.Text = "   " + New_Text;}


            Display.Show();
        }
     
        //=====================//
        void Store_Savegame_Cache()
        {
            string Save_Path = Savegame_Path + @"Empire At War\Save\";
            string Target_Path = Savegame_Path + @"Empire At War Backup\";
           
            int Current_Count = 0;
            Int32.TryParse(Load_Setting(Setting, "EAW_Savegame_Count"), out Current_Count);  
           
    
            if (Game_Path == Game_Path_FOC) 
            {   Save_Path = Savegame_Path + @"Empire At War - Forces of Corruption\Save\"; 
                Target_Path = Savegame_Path + @"Forces of Corruption Backup\";
                Int32.TryParse(Load_Setting(Setting, "FOC_Savegame_Count"), out Current_Count);
            }

            if (!Directory.Exists(Target_Path)) { Directory.CreateDirectory(Target_Path); }

            

            if (Directory.Exists(Save_Path))
            {
                // We put all found Files inside of our target folder into a string table
                string[] File_Paths = Directory.GetFiles(Save_Path);                            
                       
                if (Current_Count < 10 & Current_Count != 0) {Current_Count++;}
                else {Current_Count = 01;} // Otherwise we start at 01 and overwrite the old 01 entry

                

                string Last_Entry = Target_Path + @"Save_" + (Current_Count -1).ToString();
               
                // Making sure Target directory was cleared and is existing
                if (Directory.Exists(Last_Entry))
                {                                             
                    // Returning function because this is only supposed to store the first entry per day
                    if (File.GetCreationTime(Last_Entry).Date == DateTime.Today.Date) { return; }
                    // Imperial_Console(600, 100, Add_Line + File.GetCreationTime(Last_Entry).Date + ",  " + DateTime.Today.Date);
                }
                
                // Creating target directory
                Directory.CreateDirectory(Target_Path + @"Save_" + Current_Count.ToString());



                if (Game_Path == Game_Path_FOC) { Save_Setting(Setting, "FOC_Savegame_Count", Current_Count.ToString()); }
                else { Save_Setting(Setting, "EAW_Savegame_Count", Current_Count.ToString()); }


                foreach (string Savegame in File_Paths)
                {   // Copying all Savegame files into the Target Path with the name Save_ + value of Savegame_Counter 
                    File.Copy(Savegame, Target_Path + @"Save_" + Current_Count.ToString() + @"\" + Path.GetFileName(Savegame) , true);
                }

            }

            // This tells the user which is the newest savegame direcory in the Backup folder
            Label_EAW_Savegame.Text = "EAW: " + Load_Setting(Setting, "EAW_Savegame_Count");
            Label_FOC_Savegame.Text = "FOC: " + Load_Setting(Setting, "FOC_Savegame_Count");
           
        }

        //=====================//
        void Push_Mod_Savegame_Directory()
        {
            if (Savegame_Path != "" & Mod_Name != "" & Mod_Name != "Choose_Mod")
            {   string Target_Path = Savegame_Path + @"Mod_Savegames\" + Mod_Name;

                if (Directory.Exists(Target_Path))
                {
                    string Source_Path = Savegame_Path + @"Empire At War\Save";
                    if (Game_Path == Game_Path_FOC) { Source_Path = Savegame_Path + @"Empire At War - Forces of Corruption\Save"; }

                    Moving(Source_Path, Target_Path);
                }
            }
        }


        void Pull_Mod_Savegame_Directory()
        {
            if (Savegame_Path != "" & Mod_Name != "" & Mod_Name != "Choose_Mod")
            {
                string Source_Path = Savegame_Path + @"Mod_Savegames\" + Mod_Name + @"\Save";

                if (Directory.Exists(Source_Path))
                {   string Target_Path = Savegame_Path + "Empire At War";
                    if (Game_Path == Game_Path_FOC) { Target_Path = Savegame_Path + "Empire At War - Forces of Corruption"; }
                   
                    Moving(Source_Path, Target_Path);
                }
               
            }
        }
        //=====================//


        // MessageBox.Show(Tag.First().ToString());

        // Imperial_Console(600, 100, Add_Line + "Da");
        // Save_Setting(Setting, "Name", Name);
        // Load_Setting(Setting, "Value");
        // Toggle_Checkbox(Check_Box, "1", "Variable_Name");
        //======================================================== End of File  ====================================================    
    }

}