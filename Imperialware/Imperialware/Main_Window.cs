using Imperialware.Resources;
// using System.Xml.XPath;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using Microsoft.VisualBasic.FileIO;
using Microsoft.Win32;
using System.Resources;

using System.Reflection;
using System.Threading;




// Imperial_Console(600, 100, Add_Line + Message);

//====================================================== Start ======================================================

namespace Imperialware
{
    public partial class MainWindow : Form 
    {
        //================= Loading Variables from .properties =================

        // Innitiating Variables
        FormWindowState LastWindowState = FormWindowState.Minimized;
        Size Last_Size;
        public static Color Color_01, Color_02, Color_03, Color_04, Color_05, Color_06, Color_07;


        double Transparency;
        double Imperialware_Version = 0.1;

        // The Settings Files!
        string Setting, Maximum_Values;      
          
        // Defining as global variable
        public static string Add_Line = Environment.NewLine;

        public static string Program_Directory = "";
    



        // All settings loaded from the Settings.txt must be innitiated as public static string if innitiated by the foreach loop below!      
        // === Game Values ===
        public static string Game_Path_EAW, Game_Path_FOC, Game_Path, Savegame_Path, Game_Language, Use_Language, Evade_Language, Language_Mode, Windowed_Mode, EAW_Savegame_Count, FOC_Savegame_Count,
            Copy_Art_Into_Editor, Play_Vanilla_Game, Close_After_Launch, Map_Editor_Game, Map_Editor_EAW, Map_Editor_FOC, Mod_Name, Host_Mod, Addon_Path, Loaded_Addon, Last_Addon;
   
      
        // === User Interface ===
        public static string Selected_Theme, Button_Color, Auto_Apply, Auto_Theme, Auto_Color, Image_Cycle, Cycle_Mod_Image, Background_Image_Path, Background_Image_A, Background_Image_B, Moved_Images,
            Planet_Switch, Teleportation_Mode, Maximal_Value_Class, Selected_Object, Checkmate_Color, Debug_Mode, Show_Mod_Button, Show_Addon_Button,
            Size_Expander_A, Size_Expander_B, Size_Expander_C, Size_Expander_D, Size_Expander_E, Size_Expander_F, Size_Expander_G, Size_Expander_H, Size_Expander_I, Size_Expander_J, 
            Size_Expander_K, Size_Expander_L, Size_Expander_M, Size_Expander_N, Dashboard_Mod, Selected_Dashboard;


        // === XML Values ===
        public static string Selected_Xml, Last_Editor_Xml, Selected_Faction, All_Factions, Playable_Factions, Selected_Galaxy, Found_Planets, Selected_Units, Selected_Instance, Selected_Team,
            Selected_Planet, Start_Planet, Target_Planet, Unit_Type, Last_Type_Search, Load_Rescent_File, Show_Load_Issues, Show_Tag_Issues, Add_Core_Code, Allowed_Patching, Use_Mod_Savegame_Dir,
            Costum_Tag_1_Name, Costum_Tag_2_Name, Costum_Tag_3_Name, Costum_Tag_4_Name, Costum_Tag_5_Name, Costum_Tag_6_Name;


        // === Maximum Values ===
        int Maximum_Credits, Maximum_Hull, Maximum_Shield, Maximum_Shield_Rate, Maximum_Energy, Maximum_Speed, Maximum_Energy_Rate, Maximum_AI_Combat, Maximum_Projectile,
            Costum_Tag_4_Max_Value, Costum_Tag_5_Max_Value, Costum_Tag_6_Max_Value, General_Maximal_Xml_Value,          
            Maximum_Build_Cost, Maximum_Skirmish_Cost, Maximum_Slice_Cost, Maximum_Build_Time, Maximum_Build_Limit, Maximum_Lifetime_Limit,
            Maximum_Tech_Level, Maximum_Star_Base_LV, Maximum_Ground_Base_LV, Maximum_Timeline, Maximum_Model_Scale, Maximum_Model_Height,
            Maximum_Select_Box_Scale, Maximum_Health_Bar_Height;
       

        // === Only in Memory ===
        public static string Data_Directory, Art_Directory, Xml_Directory, Info_Directory_Path, Maximum_Values_Fighter, Maximum_Values_Bomber, Maximum_Values_Corvette,
            Maximum_Values_Frigate, Maximum_Values_Capital, Maximum_Values_Infantry, Maximum_Values_Vehicle, Maximum_Values_Air, Maximum_Values_Hero, Maximum_Values_Structure,
            Selected_Xml_Text, Addon_Name, Temporal_A, Temporal_B, Behavoir_Tag, Dashboard_Faction, Unit_Mode, Team_Type, Team_Units;

     
        // === Int Values ===
        int Last_Tab, Window_X_Position, Window_Y_Position, Window_Width, Window_Height, Last_Width, Last_Height,
            Launch_Button_X, Launch_Button_Y, Mod_Button_X, Mod_Button_Y, Addon_Button_X, Addon_Button_Y, Temporal_C, Temporal_D;


        string[] Temporal_E;
        public static int Loadscreen_Speed { get; set; } 
        int Mod_Image_Size;
        int Error_Segment, Last_Scroll_Value = 0; // = Inactive
        int Selected_Ability = 1;
        int Switch_Required_Object = 1;


        bool User_Input, Loading_Completed, Loading_To_UI, Scrolling = false;
        bool Silent_Mode = false;

        bool Setting_Directory = false;
        bool Found_Install_Archive = true;
        bool Missed_Program_Dir = false;
        bool Upgraded = false;

        bool Edited_Selected_Unit = false;
        bool Is_Team, Launch_Team = false;  
        bool Toggle_Is_Hero, Toggle_Show_Head, Toggle_God_Mode, Toggle_Use_Particle, Toggle_Enable_All, Toggle_Build_Tab, Toggle_Innitially_Locked = false;
        bool Toggle_Operator_Model_Height, Toggle_Select_Box_Height, Toggle_Operator_Population, Toggle_Text_Info = false;
        bool Toggle_Auto_Fire = false;

        
        string Health_Bar_Size = "1";
        string Mods_Directory = @"Mods\";
        string Selected_App = "false";
        string Vanilla_Commandbar = "EAW";
        string Costum_Commandbar = "Vanilla_User_Interface";
        string Selected_Xml_Tag, Selected_Xml_Value, Original_Xml_Value, Last_Search = "";

        string Add_To_Game_Object_Files = "false";
        string Add_To_Box_Hard_Point_Data = "false";
        string Destroy_Target_Planet = "Destroy_Target_Planet = false";
        string Activate_Spawn = "Spawn = false";
        string Teleport_Units = "Teleport_Units = false";
        string Teleport_from_Planet = "Teleport_from_Planet = false";
        string Teleport_Units_on_Planet = "Teleport_Both = false";
        List<string> Selected_Instance_Tags = new List<string>();
        int Player_Credits = 0;
        int Loaded_Recent_Units = 0;     
        int Search_Toggle = 0; 
        int Loaded_Xmls = 0;


        // Innitializing Lists 
        List<string> Changed_Tag_Names = new List<string>();
        List<string> Changed_Tag_Old_Values = new List<string>();
        List<string> Changed_Tag_New_Values = new List<string>();
        List<string> Undo_Changed_Tag_Names = new List<string>();
        List<string> Undo_Changed_Tag_New_Values = new List<string>();
        List<string> Undo_Changed_Tag_Old_Values = new List<string>();

        List<string> Xml_Exception_Files = new List<string>();
        List<string> Error_List = new List<string>();
        List<string> Misloaded_Tags = new List<string>();
        List<Control> Move_List_A, Move_List_B, Move_List_C, Move_List_D, Move_List_E, Move_List_F, Move_List_G, Move_List_H, Move_List_I, Move_List_for_Team, Move_List_Info_Text, Move_List_J, Move_List_K;
        List<string> Search_Index = new List<string>();
        List<string> All_Playable_Factions = new List<string>();


        XDocument Save_Xml_File = new XDocument();
        IEnumerable<XElement> Instance;
        string Root_Tag = "";




        //=========== Ability Variables ===========
        // We will use these variables as container, to allow the user to switch between the Values of primary and secondary ability
        string Ability_1_Type, Ability_1_Remove, Ability_1_Activated_GUI, Ability_1_Expiration_Time, Ability_1_Recharge_Time, Ability_1_Name,
        Ability_1_Description, Ability_1_Icon, Ability_1_Activated_SFX, Ability_1_Deactivated_SFX, Ability_1_Mod_Multipliers = "";

        string Ability_2_Type, Ability_2_Remove, Ability_2_Activated_GUI, Ability_2_Expiration_Time, Ability_2_Recharge_Time, Ability_2_Name,
        Ability_2_Description, Ability_2_Icon, Ability_2_Activated_SFX, Ability_2_Deactivated_SFX, Ability_2_Mod_Multipliers, Last_Tag = "";

        bool Ability_1_Toggle_Auto_Fire, Ability_2_Toggle_Auto_Fire = false;
 
               

        /* TODO
        // Importing external Dll
        [System.Runtime.InteropServices.DllImport(@"C:\Program Files\Imperialware\" + @"Misc\Control.Draggable.dll")]


        public class ControlExtension { }

        // public static extern PlaySound(string filename, long hmodule, int dword);
        public static extern void ControlExtension.Draggable(Control Object, bool Make_Draggable);
         */
 

        //============================== End of Variable Setup ============================= 


        public MainWindow()
        {          
            InitializeComponent();      
            Thread.Sleep(2000); 
        }

    


        public void MainWindow_Load(object sender, EventArgs e)
        {
           
            string Old_Program_Directory = "";

            try
            {   // Fetching Program Directories setting from Registry
                Program_Directory = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware",
                                        "Program_Directory", "").ToString();
            } catch { }

            try 
            {   Old_Program_Directory = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware",
                                        "Old_Program_Directory", "").ToString();
            } catch { } // Both variables stay as ""
 
                    
            Debug_Mode = "false"; // Needs any value in order to get the "moving" function to work



            // Trying to change directory into itself
            if (Old_Program_Directory == Program_Directory & Program_Directory != "")
            { Old_Program_Directory = ""; Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware", "Old_Program_Directory", ""); }

            else if (Old_Program_Directory != "" & Old_Program_Directory != "Delete_It" & Old_Program_Directory != "Upgrade")
            {    
                // === Moving from Old to new Program Directory
                if (!Directory.Exists(Program_Directory)) { Directory.CreateDirectory(Program_Directory); }
                try 
                {   // Copying Files 
                    string[] File_Paths = Directory.GetFiles(Old_Program_Directory); 
                    foreach (string Item in File_Paths) { Moving(Item, Program_Directory); }

                    // Copying Directories (damn it microsoft -.-)
                    File_Paths = Directory.GetDirectories(Old_Program_Directory);
                    foreach (string Item in File_Paths) { Moving(Item, Program_Directory); }

                    Deleting(Old_Program_Directory);

                    // Removing value so next usage goes smoothly
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware", "Old_Program_Directory", "");
             

                } catch { Missed_Program_Dir = true; }
            }

       
            // Uninstallation function 
            if (Old_Program_Directory == "Delete_It")
            {
                RegistryKey Registry_Key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE", RegistryKeyPermissionCheck.ReadWriteSubTree);

                // Getting rid all Imperialware Registry entries
                Registry_Key.DeleteSubKeyTree("Imperialware");

                        
                Deleting(Program_Directory);

                MessageBox.Show("    Removed all Imperialware Files, if you reopen Imperialware"
                   + Add_Line + "    after closing it will auto install again.");
                
                // Exiting Load function
                Application.Exit();
                // And making sure this function has stopped already while the Application is being closed.
                return;
            }
            // Upgrade Part 1: Removing all Directories and upgrading .txt files
            else if (Old_Program_Directory == "Upgrade")
            {   try 
                {   // If the Xml directory was found 
                    if (Directory.Exists(Program_Directory))
                    {
                        // Deleting(Program_Directory);    
                        Deleting(Program_Directory + "Addons");
                        Deleting(Program_Directory + "Images");
                        Deleting(Program_Directory + "Misc");

                        Deleting(Program_Directory + "Mods");
                        Deleting(Program_Directory + "Themes");
                        Deleting(Program_Directory + "Xml_Core");

                        // We are going to update the "Installed_Version" value in EACH user Settings.txt
                        foreach (string The_File in Directory.GetFiles(Program_Directory))
                        { 
                            if (The_File.EndsWith(".TXT") | The_File.EndsWith(".txt")) { Save_Setting(The_File, "Installed_Version", Imperialware_Version.ToString()); }
                        }                         
                    }
                } catch {}
          
                Upgraded = true;
            }
           



            // Setting value to Textbox and saving it via "Textbox changed event" function
            if (Program_Directory != "") { Text_Box_Folder_Path.Text = Program_Directory;}      
            else
            {   // Otherwise we prompt the user and ask for a installation Directory                     
                Program_Directory = @"C:\Program Files\Imperialware\";
                Text_Box_Folder_Path.Text = Program_Directory;
           
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware", "Program_Directory", Program_Directory);
     
                Tab_Control_01.SelectedIndex = 6;
                Text_Box_Folder_Path.Select();
          
                // This causes the restoring function of last selected tab to stop in certain situations
                Setting_Directory = true;
            }




            // Loading and assigning Variables from Settings.txt  
            Setting = Program_Directory + "Settings_" + Environment.UserName + ".txt";


            // Getting Installed Version
            if (Old_Program_Directory != "Upgrade")
            {
                double Installed_Version = 0; // Won't work because Splash Screen loads from Imperialware Directory
                double.TryParse(Load_Setting(Setting, "Installed_Version"), out Installed_Version);

                // Checking versions
                if (Installed_Version != 0 & Installed_Version < Imperialware_Version & File.Exists("Imperialware_Installation.zip"))
                {
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware", "Old_Program_Directory", "Upgrade");

                    // Need to restart because otherwise we can't trash the Program Directory because it is being used.
                    Application.Restart();
                }
            }
      
          
            // =======================================================================================
           
            string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

 


            // If the application starts with this archive in the same directory, this will be the source of installation
            if (File.Exists("Imperialware_Installation.zip"))
            {   
                if (Old_Program_Directory == "Upgrade" | !Directory.Exists(Program_Directory))
                {
                    Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware", "Old_Program_Directory", "");

                    System.IO.Compression.ZipFile.ExtractToDirectory("Imperialware_Installation.zip", Program_Directory);
                    // Deleting("Imperialware_Installation.zip");
                    // Showing editor Docs to the User
                    Moving(Program_Directory + @"Misc\Map_Editor\Star Wars - EAW Editor Docs", Desktop);  
                }                        
            }


            // If no Imperialware_Installation.zip is available we're still making sure the Program directory and Settings.txt are exisisting in a smaller version          
            else if (!Directory.Exists(Program_Directory))
            {
                byte[] Archive = Imperialware.Properties.Resources.Main_Directory;
               
                // Temporally moving the archive to Desktop
                File.WriteAllBytes(Desktop + @"\Delete_Me.zip", Archive);

                System.IO.Compression.ZipFile.ExtractToDirectory(Desktop + @"\Delete_Me.zip", Program_Directory);
                
                // Trashing after extraction, named the archive "Delete_Me" for the case that its autodeletion fails
                Deleting(Desktop + @"\Delete_Me.zip");

                // Telling the exception reporting function below to inform the user
                Found_Install_Archive = false;
            }
                     
            

            
            if (!File.Exists(Setting))
            {   // Making sure Settings file exist
                byte[] Settings = Imperialware.Properties.Resources.Settings;
                File.WriteAllBytes(Setting, Settings);
            }

         
                                
            //=====================//
            Debug_Mode = Load_Setting("1", "Debug_Mode");

         

            string[] Load_Settings = 
            { 
              // === Game Values ===
              "Game_Path_EAW", "Game_Path_FOC", "Game_Path", "Savegame_Path", "Game_Language", "Use_Language", "Evade_Language", "Language_Mode", "Windowed_Mode", 
              "Copy_Art_Into_Editor", "Play_Vanilla_Game", "Close_After_Launch", "Mod_Name", "Host_Mod", "Addon_Path", "Loaded_Addon", "Last_Addon",
        
              // === User Interface ===
              "Selected_Theme", "Button_Color", "Auto_Apply", "Auto_Theme", "Auto_Color", "Image_Cycle", "Cycle_Mod_Image", "Background_Image_Path", "Background_Image_A", "Background_Image_B", "Moved_Images",
              "Planet_Switch", "Teleportation_Mode", "Maximal_Value_Class", "Selected_Object", "Checkmate_Color", "Debug_Mode", "Show_Mod_Button", "Show_Addon_Button", "Size_Expander_I", "Size_Expander_J",       
              "Size_Expander_A", "Size_Expander_B", "Size_Expander_C", "Size_Expander_D", "Size_Expander_E", "Size_Expander_F", "Size_Expander_G", "Size_Expander_H", 
              "Size_Expander_K", "Size_Expander_L", "Size_Expander_M", "Size_Expander_N", "Dashboard_Mod", "Selected_Dashboard",

               // === XML Values ===
               "Selected_Xml", "Last_Editor_Xml", "Selected_Faction",  "All_Factions", "Playable_Factions", "Selected_Galaxy", "Found_Planets", "Selected_Units", "Selected_Instance", "Selected_Team",
               "Selected_Planet", "Start_Planet", "Target_Planet", "Unit_Type", "Last_Type_Search", "Load_Rescent_File", "Show_Load_Issues", "Show_Tag_Issues", "Add_Core_Code", "Allowed_Patching", "Use_Mod_Savegame_Dir",
               "Costum_Tag_1_Name", "Costum_Tag_2_Name", "Costum_Tag_3_Name", "Costum_Tag_4_Name", "Costum_Tag_5_Name", "Costum_Tag_6_Name",             
            };
            
            Load_All_Settings(Setting, Load_Settings);



            // Addon_Name = Loaded_Addon;
                 
            // Loading Int type Variables from Max Values settings file (its location is depending on selected mod)  
            Set_Maximal_Value_Directories(); 
          
            if (Maximal_Value_Class == "1") { Load_Maximal_Values(Maximum_Values_Fighter); }
            else if (Maximal_Value_Class == "2") { Load_Maximal_Values(Maximum_Values_Bomber); }
            else if (Maximal_Value_Class == "3") { Load_Maximal_Values(Maximum_Values_Corvette); }
            else if (Maximal_Value_Class == "4") { Load_Maximal_Values(Maximum_Values_Frigate); }
            else if (Maximal_Value_Class == "5") { Load_Maximal_Values(Maximum_Values_Capital); }

            else if (Maximal_Value_Class == "6") { Load_Maximal_Values(Maximum_Values_Infantry); }
            else if (Maximal_Value_Class == "7") { Load_Maximal_Values(Maximum_Values_Vehicle); }
            else if (Maximal_Value_Class == "8") { Load_Maximal_Values(Maximum_Values_Air); }
            else if (Maximal_Value_Class == "9") { Load_Maximal_Values(Maximum_Values_Hero); }
            else if (Maximal_Value_Class == "10") { Load_Maximal_Values(Maximum_Values_Structure); }

            // Loading Int type Variables from settings file
            Int32.TryParse(Load_Setting(Setting, "Last_Tab"), out Last_Tab);         
            Int32.TryParse(Load_Setting("2", "Mod_Image_Size"), out Mod_Image_Size);

            Int32.TryParse(Load_Setting(Setting, "Window_Width"), out Window_Width);
            Int32.TryParse(Load_Setting(Setting, "Window_Height"), out Window_Height);   
   
            Int32.TryParse(Load_Setting(Setting, "Window_X_Position"), out Window_X_Position);
            Int32.TryParse(Load_Setting(Setting, "Window_Y_Position"), out Window_Y_Position); 

           
            Int32.TryParse(Load_Setting(Setting, "Launch_Button_X"), out Launch_Button_X);
            Int32.TryParse(Load_Setting(Setting, "Launch_Button_Y"), out Launch_Button_Y); 

            Int32.TryParse(Load_Setting(Setting, "Mod_Button_X"), out Mod_Button_X);
            Int32.TryParse(Load_Setting(Setting, "Mod_Button_Y"), out Mod_Button_Y); 

            Int32.TryParse(Load_Setting(Setting, "Addon_Button_X"), out Addon_Button_X);
            Int32.TryParse(Load_Setting(Setting, "Addon_Button_Y"), out Addon_Button_Y);




            // Loading Colors
            try { Color_01 = Load_Color(Setting, "Color_01"); } catch { Color_01 = Color.FromArgb(135, 206, 250, 255); } // Main Color: Sky Blue
            try { Color_02 = Load_Color(Setting, "Color_02"); } catch { Color_02 = Color.FromArgb(40, 130, 240, 220); } // Unselect: Dark Blue

            try { Color_03 = Load_Color(Setting, "Color_03"); } catch { Color_03 = Color.FromArgb(200, 200, 200, 200); } // Select: White
            try { Color_04 = Load_Color(Setting, "Color_04"); } catch { Color_04 = Color.FromArgb(0, 0, 0, 255); } // Button Color: Black

            try { Color_05 = Load_Color(Setting, "Color_05"); } catch { Color_05 = Color.FromArgb(64, 64, 64, 240); } // UI Color: Neutral Gray
            try { Color_06 = Load_Color(Setting, "Color_06"); } catch { Color_06 = Color.FromArgb(147, 112, 219, 255); } // Costum Tag Color: Medium Purple

            try { Color_07 = Load_Color(Setting, "Color_07"); } catch { Color_07 = Color.FromArgb(120, 120, 120, 255); } // Checker Color: 


            // Setting Colors: 
            try { Set_Color(Color_01, 01); } catch { }
            try { Set_Color(Color_02, 02); } catch { }

            Label_Amount.ForeColor = Color_02;
            try { Set_Checkbox_Color(Color_02, false); } catch { }

            try { Set_Color(Color_03, 03); } catch { }
            try { Set_Color(Color_04, 04); } catch { }

            try { Set_Color(Color_05, 05); } catch { }
            try { Set_Color(Color_06, 06); } catch { }

            // Setting Checkmate Image color
            Picture_Box_Checkmate_Color.BackColor = Color_07;


            //=====================//
            try 
            { 

            try 
            {   if (Game_Path_EAW == "false" | Game_Path_EAW == "")
                {
                    // We save this with quotemarks (using "0") and store it in the memory without quotes
                    Save_Setting("0", "Game_Path_EAW", Get_Game_Path("EAW"));

                    // For first usage we innitialize the path of original EAW
                    Game_Path = Game_Path_EAW;
                    Save_Setting(Setting, "Mod_Name", "Choose_Mod");
                }
            } catch {}

            try 
            {   if (Game_Path_FOC == "false" | Game_Path_FOC == "") 
                {
                    Game_Path_FOC = Get_Game_Path("FOC");
                    Save_Setting("0", "Game_Path_FOC", Get_Game_Path("FOC"));           
                }

                Text_Box_EAW_Path.Text = Game_Path_EAW;
                Text_Box_FOC_Path.Text = Game_Path_FOC;
                Text_Box_Savegame_Path.Text = Savegame_Path;              
            } catch {}


       


            // Setting Version according to Variable at the top
            Label_Imperialware_Version.Text = Imperialware_Version.ToString().Replace(",", ".");
            Text_Box_Hyperspace_Speed.Text = "1.0";


            if (Costum_Tag_1_Name != "<Tag_1_Name>") { Text_Box_Tag_1_Name.Text = Costum_Tag_1_Name; Text_Box_Tag_1_Name.ForeColor = Color_01; }
            if (Costum_Tag_2_Name != "<Tag_2_Name>") { Text_Box_Tag_2_Name.Text = Costum_Tag_2_Name; Text_Box_Tag_2_Name.ForeColor = Color_01; }
            if (Costum_Tag_3_Name != "<Tag_3_Name>") { Text_Box_Tag_3_Name.Text = Costum_Tag_3_Name; Text_Box_Tag_3_Name.ForeColor = Color_01; }
          
            if (Costum_Tag_4_Name != "<Tag_4_Name>") 
            {   Text_Box_Tag_4_Name.Text = Costum_Tag_4_Name; 
                Text_Box_Tag_4_Name.ForeColor = Color_01;
                Label_Maximum_Costum_4.Text = Text_Box_Tag_4_Name.Text; // Setting Name of the Max Value Label according to Value
            }

            if (Costum_Tag_5_Name != "<Tag_5_Name>") 
            {   Text_Box_Tag_5_Name.Text = Costum_Tag_5_Name; 
                Text_Box_Tag_5_Name.ForeColor = Color_01;
                Label_Maximum_Costum_5.Text = Text_Box_Tag_5_Name.Text;
            }
            if (Costum_Tag_6_Name != "<Tag_6_Name>") 
            {   Text_Box_Tag_6_Name.Text = Costum_Tag_6_Name; 
                Text_Box_Tag_6_Name.ForeColor = Color_01;
                Label_Maximum_Costum_6.Text = Text_Box_Tag_6_Name.Text;
            }



          

            // Assigning Directories
            Data_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\";
            Xml_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\Xml\";
            Art_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\Art\";
            Info_Directory_Path = Program_Directory + Selected_Object;

            } catch { Error_Segment = 1; }



            try 
            { 

               

            if (Debug_Mode == "true")
            {
                Check_Box_Debug_Mode.ForeColor = Color_03;
                Check_Box_Debug_Mode.Checked = !Check_Box_No_Mod.Checked;
            }

            // Innitializing the UI Elements, according to the last session:      
            //=====================//

            // Setting Window Size and Loaction (1040, 960)
            // Size New_Size = new Size( Window_Width, Window_Height);    
            this.Size = new Size(Window_Width, Window_Height);
            this.Location = new Point(Window_X_Position, Window_Y_Position);

       

            Make_Backgrounds_Transparent();

            Set_All_Switch_Buttons();
            Set_Color_Segment_Buttons(); // Thes need seperate treatment
            Set_All_Segment_Buttons();
            // Image_Button, Active_Button_Name, Inactive_Button_Name, Trashold for rim, Is_Activated flag (choose second or third parameter)
            Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false);
            Toggle_Button(Switch_Button_Build_Tab, "Button_On", "Button_Off", 0, false);
            Toggle_Button(Switch_Button_Innitially_Locked, "Button_On", "Button_Off", 0, false);

            Toggle_Button(Button_Operator_Model_Height, "Button_Plus", "Button_Minus", -5, false);
            Toggle_Button(Button_Operator_Select_Box_Height, "Button_Plus", "Button_Minus", -5, false);
            Toggle_Button(Button_Operator_Population, "Button_Plus", "Button_Minus", -5, false);

            Toggle_Button(Button_Add_Ability, "Button_Plus", "Button_Plus", -2, false);

                

            // this sets Button color according to the current Color_02 (Buttons) and Color_03 (Text) 
            Change_Button_Color();

            //=====================//
            // Setting the last tab                   
            if (Setting_Directory == false) { Tab_Control_01.SelectedIndex = Last_Tab; }


            // Setting Background Image of Tab 1 according to the active theme and loading 2 UI based Checkbox values         
            if (Auto_Theme == "true")
            {   Check_Box_Set_Theme.ForeColor = Color_03;
                Check_Box_Set_Theme.Checked = !Check_Box_Set_Theme.Checked;
            }

            if (Auto_Color == "true")
            {   Check_Box_Set_Color.ForeColor = Color_03;
                Check_Box_Set_Color.Checked = !Check_Box_Set_Color.Checked;
            }

            if (Cycle_Mod_Image == "true")
            {   Check_Box_Cycle_Mod_Image.ForeColor = Color_03;
                Check_Box_Cycle_Mod_Image.Checked = !Check_Box_Cycle_Mod_Image.Checked;
            }

           
            // If our user activated the cycle mode last time we are going to trigger another cycle
            if (Background_Image_A == "Cycle_Mode") { Button_Cycle_Background_Click(null, null); }
            // Otherwise we set the stored file as Background
            else
            {   try
                {   if (this.Size.Width < 1025 | this.Size.Height < 781) { Adjust_Wallpaper(Background_Image_Path + Background_Image_B); }
                    else { Adjust_Wallpaper(Background_Image_Path + Background_Image_A); }
                }
                catch 
                {
                    try { Adjust_Wallpaper(Background_Image_Path + Background_Image_A); }
                    catch 
                    {
                        try { Change_Theme(Program_Directory + @"Themes\Default\", true); } catch { }
                    }            
                }                
            }

            }  catch { Error_Segment = 2; }
            //=========== User Interface related Tasks ==========//

            try 
            {

            
            int DPI_X_Size = 0;

            // Getting Font scale setting of user
            using (Graphics gfx = this.CreateGraphics())
            {
                DPI_X_Size = (int)gfx.DpiX;
            }


            // Depending on Windows Font scale setting of user, we adjust Application text if not at 96 or 100%:         
            if (DPI_X_Size == 120) { Adjust_Text_Fonts(DPI_X_Size); }
            // This is the normal size, 100% and 150% return 96:
            // else if (DPI_X_Size == 96) { }


            // Need to Change current culture, in order to prevent the program from using , instead of . for european languagages
            System.Globalization.CultureInfo The_Culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            //  if (Thread.CurrentThread.CurrentCulture.Name == "fr-FR") culture = CultureInfo.CreateSpecificCulture("en-US");    

            Thread.CurrentThread.CurrentCulture = The_Culture;
            Thread.CurrentThread.CurrentUICulture = The_Culture;




            // Setting Reset Button to Red Color 
            Button_Reset_All_Settings.BackColor = Color.Red;
            Button_Withdraw_Files.BackColor = Color.Red;
            Button_Uninstall_Imperialware.BackColor = Color.Red; 


            try
            {               
                // Setting the Tombnail image according to User selection           
                Picture_Box_Dashboard_Preview.Image = Resize_Image(Program_Directory + @"Mods\" + Dashboard_Mod + @"\Dashboards\", Selected_Dashboard + ".png", 768, 233);
            }   // Otherwise we try .jpg
            catch { Picture_Box_Dashboard_Preview.Image = Resize_Image(Program_Directory + @"Mods\" + Dashboard_Mod + @"\Dashboards\", Selected_Dashboard + ".jpg", 768, 233); }

            // Setting Innitial Values
            // Combo_Box_Target_Faction.Text = "Human_Player";
            Combo_Box_Dashboard_Mod.Text = Dashboard_Mod;
            Combo_Box_Dashboard_Mod_SelectedIndexChanged(null, null);
         


            // Setting Color Picker Image from current Theme
            if (File.Exists(Selected_Theme + @"Buttons\" + "Color_Picker.png")) { Picture_Box_Color_Picker.Image = Resize_Image(Selected_Theme, "Color_Picker.png", 320, 320); }
            else { try { Picture_Box_Color_Picker.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Color_Picker.png", 320, 320); } catch {} }





            /* Todo
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.AssemblyResolve += new ResolveEventHandler(ResolveEventHandler_01);
            
            
            try { System.Reflection.Assembly Draggable_Control_Assembly = System.Reflection.Assembly.LoadFrom(Program_Directory + @"Misc\Control.Draggable.dll"); }
            catch { }

            

            // Load the object
            string fullTypeName = "MyNamespace.YourType";

            
            // Type The_Type = Type.GetType("ControlExtension");
            Type The_Type = Draggable_Control_Assembly.CreateInstance("ControlExtension.Draggable");
         



                // ===== Launch Buttons  ===== // 
                // Making these 3 controls drag and drop'able using the nuget packet "control draggable" by intrueder
                try
                {
                    ControlExtension.Draggable(Button_Debug_Mode, true);
                    ControlExtension.Draggable(Button_Launch_Mod, true);
                    ControlExtension.Draggable(Button_Mod_Only, true);
                    ControlExtension.Draggable(Button_Launch_Addon, true);
                }
                catch { }
     
                   */


            // Placing launch buttons according to their last location
            Button_Launch_Mod.Location = new Point(Launch_Button_X, Launch_Button_Y);
            Button_Mod_Only.Location = new Point(Mod_Button_X, Mod_Button_Y);
            Button_Launch_Addon.Location = new Point(Addon_Button_X, Addon_Button_Y);



            if (Show_Mod_Button == "true")
            {
                Check_Box_Show_Mod_Button.ForeColor = Color_03;
                Check_Box_Show_Mod_Button.Checked = !Check_Box_Show_Mod_Button.Checked;
                // Setting button to visible
                Button_Mod_Only.Visible = true;
            }

            if (Show_Addon_Button == "true")
            {
                Check_Box_Show_Addon_Button.ForeColor = Color_03;
                Check_Box_Show_Addon_Button.Checked = !Check_Box_Show_Addon_Button.Checked;
                Button_Launch_Addon.Visible = true;
            }

            Set_Launch_Button(Button_Launch_Mod, "Button_Launch.png", "Button_Launch.png", 34, "Launch", 158);
            Button_Launch_Mod.Parent = Background_Wallpaper;

            Set_Launch_Button(Button_Mod_Only, "Button_Mod.png", "Button_Launch.png", 20, "Mod Only", 100);       
            Button_Mod_Only.Parent = Background_Wallpaper;

            Set_Launch_Button(Button_Launch_Addon, "Button_Addon.png", "Button_Launch.png", 8, "Addon + Mod", 100); 
            Button_Launch_Addon.Parent = Background_Wallpaper;

            Toggle_Button(Button_Expand_Program_Info, "Button_Info_Highlighted", "Button_Info", 0, false);
            Button_Expand_Program_Info.BackColor = Text_Box_Info.BackColor;
            Label_Info_Text.BackColor = Text_Box_Info.BackColor;
            Picture_Box_License.BackColor = Text_Box_Info.BackColor;
       

            } catch { Error_Segment = 3; }


            try { 
        
            // Need to undo the additonal 100 bonus from the Track_Bar_Mod_Image_Size function below
            if (Mod_Image_Size != 948) { Track_Bar_Mod_Image_Size.Value = (Mod_Image_Size / 100) - 1; }
            else { Track_Bar_Mod_Image_Size.Value = (Mod_Image_Size / 100); }
            Track_Bar_Mod_Image_Size_Scroll(null, null);
    
            //=====================//

            // Loading from settings.txt and parsing it to double
            Double.TryParse(Load_Setting(Setting, "Transparency"), out Transparency); 
            // Applying Variable to the window opacity
            this.Opacity = Transparency;

            // Getting double to int and applying it to the Track Bar 
            int Opacity_Scale = Convert.ToInt32(Transparency * 100);
            Track_Bar_Transperency.Value = Opacity_Scale;
             
            //=====================//         
            if (Evade_Language == "true")
            {
                Check_Box_Evade_Language.ForeColor = Color_03;
                Check_Box_Evade_Language.Checked = !Check_Box_Evade_Language.Checked;            
            }
            //=====================//
            // Displaying Names of Game, Mod and Addon to their Labels and selecting the right Radio box
            if (Game_Path == Game_Path_EAW)
            { Button_Select_EAW.Select();
            Label_Game_Name.Text = "Empire at War";}
            else if (Game_Path == Game_Path_FOC)
            { Button_Select_FOC.Select();
            Label_Game_Name.Text = "Forces of Corruption";}

            Label_Mod_Name.Text = Mod_Name;
            Label_Mod_Name_2.Text = Mod_Name;
            Label_Addon_Name.Text = Loaded_Addon;
                       
            // Disabling Text as long it is not needed
            Label_Mod_Info.Text = "";


            // This tells the user which is the newest savegame direcory in the Backup folder
            Label_EAW_Savegame.Text = "EAW: " + Load_Setting(Setting, "EAW_Savegame_Count");
            Label_FOC_Savegame.Text = "FOC: " + Load_Setting(Setting, "FOC_Savegame_Count");
            //=====================//

            if (Windowed_Mode == "true")
            {   Check_Box_Windowed_Mode.ForeColor = Color_03;
                Check_Box_Windowed_Mode.Checked = !Check_Box_No_Mod.Checked;}                    
            //=====================//

            if (Use_Mod_Savegame_Dir == "true")
            {
                Check_Box_Mod_Savegame_Directory.ForeColor = Color_03;
                Check_Box_Mod_Savegame_Directory.Checked = !Check_Box_No_Mod.Checked;
            }
            //=====================//

            // Autoselecting image of active mod
            try { List_Box_Mod_Selection.SelectedItem = Mod_Name; } catch { }       
            //=====================//
            if (Play_Vanilla_Game == "true")
            {  // Then we change the font color to active and recheck the checkbox
                Check_Box_No_Mod.ForeColor = Color_03;
                Check_Box_No_Mod.Checked = !Check_Box_No_Mod.Checked;

                Label_Mod_Name.Text = "Inactive";
                Label_Mod_Name_2.Text = "Inactive";
            }  
             //=====================//

            if (Close_After_Launch == "true")
            {   Check_Box_Close_After_Launch.ForeColor = Color_03;
                Check_Box_Close_After_Launch.Checked = !Check_Box_No_Mod.Checked;}   
            //=====================//

            if (Copy_Art_Into_Editor == "true")
            {   Check_Box_Copy_Editor_Art.ForeColor = Color_03;
                Check_Box_Copy_Editor_Art.Checked = !Check_Box_Copy_Editor_Art.Checked;}         
            //=====================//
                      
            // Turning Selection buttons to unpressable state in Mods Mode.
            if (Selected_Object == "Mods")
            {   Picture_Box_Load_Addon.Visible = true;
                Picture_Box_Unload_Addon.Visible = true;

                Button_Load_Addon.Visible = false;
                Button_Unload_Addon.Visible = false;}
            //=====================//        
        
                if (Checkmate_Color == "true")
            {   // Checking Checkbox
                Check_Box_Xml_Checkmate.ForeColor = Color_03;
                Check_Box_Xml_Checkmate.Checked = !Check_Box_Xml_Checkmate.Checked;
            }
            //=====================//

            if (Auto_Apply == "true")
            {   Check_Box_Allow_Auto_Apply.ForeColor = Color_03;
                Check_Box_Allow_Auto_Apply.Checked = !Check_Box_Allow_Auto_Apply.Checked; }
            //=====================//

            if (Allowed_Patching == "true")
            {   Check_Box_Allow_Patching.ForeColor = Color_03;
                Check_Box_Allow_Patching.Checked = !Check_Box_Allow_Patching.Checked;}             
            //=====================//

            if (Show_Load_Issues == "true")
            {   Check_Box_Load_Issues.ForeColor = Color_03;
                Check_Box_Load_Issues.Checked = !Check_Box_Load_Issues.Checked; }           
            //=====================//         

            if (Show_Tag_Issues == "true")
            {   Check_Box_Load_Tag_Issues.ForeColor = Color_03;
                Check_Box_Load_Tag_Issues.Checked = !Check_Box_Load_Tag_Issues.Checked; }      
            //=====================//         

            if (Add_Core_Code == "true")
            {   Check_Box_Add_Core_Code.ForeColor = Color_03;
                Check_Box_Add_Core_Code.Checked = !Check_Box_Add_Core_Code.Checked; }                
            //=====================//

            if (Load_Rescent_File == "true")
            {   Check_Box_Load_Rescent.ForeColor = Color_03;
                Check_Box_Load_Rescent.Checked = !Check_Box_Load_Rescent.Checked; }
            //=====================//

            // Selecting Checkbox for Planet Variables
            if (Selected_Planet == "Target_Planet")
            { Radio_Button_Planet_1.Select(); }
            // And the Target planet is the default one to autoselect, in case 2 as well
            else if (Selected_Planet == "Start_Planet")
            { Radio_Button_Planet_1.Select(); }
            else if (Selected_Planet == "Build_Planet")
            { Radio_Button_Planet_3.Select(); }
            else if (Selected_Planet == "Planet_List")
            { Radio_Button_Planet_3.Select(); }  
            //=====================//
            // Setting the innitial cathegory for spawns, depending on our stored variable

            try { Combo_Box_Filter_Type.Text = Last_Type_Search; } catch {}         
            //=====================//

            // Setting the Checkbox for usage of language parameter and changing its color
            if (Use_Language == "true")
            {  Check_Box_Use_Language.Checked = !Check_Box_Use_Language.Checked;
               Check_Box_Use_Language.ForeColor = Color_03; }          
            //=====================//

            } catch { Error_Segment = 4; }


            try { 

            // This List defines all Controls that need to be repositioned after this Expander closed
                Move_List_A = new List<Control>() { Group_Box_Power_Values, Group_Box_Abilities, Group_Box_Properties, Group_Box_Build_Requirements, Group_Box_Costum_Tags, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As };
            // Closing the Expander button to close if its property is set to false
            if (Size_Expander_A == "false")
            {  ToggleControlY(Group_Box_Unit_Settings, null, Move_List_A, -1336);
               // And change the name of the Expander Button to - for the next usage
               Button_Expand_A.Text = "+";}
            //=====================//

            Move_List_B = new List<Control>() { Group_Box_Abilities, Group_Box_Properties, Group_Box_Build_Requirements, Group_Box_Costum_Tags, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As };
            // Same for the second Expander
            if (Size_Expander_B == "false")
            {  ToggleControlY(Group_Box_Power_Values, null, Move_List_B, -980);
               // And change the name of the Expander Button to - for the next usage
               Button_Expand_B.Text = "+";}
            //=====================//

            Move_List_J = new List<Control>() { Group_Box_Build_Requirements, Group_Box_Properties, Group_Box_Costum_Tags, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As };
            // Same for the second Expander
            if (Size_Expander_J == "false")
            {
                int Expansion_Value = 2556;

                // Need to concider the Child Expander Box
                if (Size_Expander_K == "false") { Expansion_Value = 1716; }

                ToggleControlY(Group_Box_Abilities, null, Move_List_J, -Expansion_Value);
                Button_Expand_J.Text = "+";
            }
            //=====================//

            Move_List_K = new List<Control>() { Button_Collaps_J, Group_Box_Build_Requirements, Group_Box_Properties, Group_Box_Costum_Tags, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As };
       
            if (Size_Expander_K == "false")
            {
                Temporal_C = 840;

                ToggleControlY(Group_Box_Additional_Abilities, null, Move_List_K, -Temporal_C);
                // And change the name of the Expander Button to - for the next usage
                Button_Expand_K.Text = "+";

                Combo_Box_Additional_Abilities.Visible = false;
                Check_Box_Use_In_Team.Visible = false;
                Button_Add_Ability.Visible = false;

                // Changing Size of Parent Group Box as well
                Group_Box_Abilities.Height += -Temporal_C;
            }
            //=====================//

            Move_List_C = new List<Control>() { Label_Xml_Name, Group_Box_Unit_Name, Group_Box_Properties, Button_Open_Xml, Button_Create_New_Xml, Group_Box_Unit_Settings, Group_Box_Power_Values, Group_Box_Abilities, Group_Box_Build_Requirements, 
                                                Group_Box_Costum_Tags, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As};
            // Same for the second Expander
            if (Size_Expander_C == "false")
            {   int Expansion_Value = 1530;

                // Need to concider the Child Expander Box
                if (Size_Expander_E == "false") {Expansion_Value = 1000;}
                
                ToggleControlY(Group_Box_Cheating, null, Move_List_C, -Expansion_Value);
                // And change the name of the Expander Button to - for the next usage
                Button_Expand_C.Text = "+";}              
            //=====================//

            Move_List_D = new List<Control>() { Button_Reset_All_Settings, Button_Withdraw_Files, Button_Uninstall_Imperialware };
            // Same for the second Expander
            if (Size_Expander_D == "false")
            {   ToggleControlY(Group_Box_Max_Values, null, Move_List_D, -1670);
                // And change the name of the Expander Button to - for the next usage
                Button_Expand_D.Text = "+";}
            //=====================//

            Move_List_E = new List<Control>() {Label_Units, Text_Box_Search_Bar, Combo_Box_Filter_Type, Label_Clear_Searchbar, Button_Search_Unit, List_Box_All_Instances, Mini_Button_Copy_Instance, Label_Amount,
            Combo_Box_Amount, Button_Add_Selected, Button_Remove_Selection, Button_Spawn, Button_Edit, Button_Edit_Xml, List_Box_All_Spawns, Label_Credits, Progress_Bar_Credits, Button_Give_Credits, Track_Bar_Credits, Text_Box_Credits,
            Label_Credit_Sign, Label_KaChing, Label_Xml_Name, Group_Box_Unit_Name, Button_Open_Xml, Button_Create_New_Xml, Button_Collaps_C, Group_Box_Unit_Settings, Group_Box_Power_Values, Group_Box_Abilities, Group_Box_Properties, Group_Box_Build_Requirements, Group_Box_Costum_Tags, 
            Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As };

            // Same for the second Expander
            if (Size_Expander_E == "false")           
            {   ToggleControlY(Group_Box_Planets, null, Move_List_E, -530);
                // And change the name of the Expander Button to - for the next usage
                Button_Expand_E.Text = "+";

                // Changing Size of Parent Group Box as well
                Group_Box_Cheating.Height += -530;
            }
                             
            //=====================//   

            Move_List_G = new List<Control>() { Group_Box_Costum_Tags, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As };
          
            if (Size_Expander_G == "false")
            {
                int Expansion_Value = -2482; 
                      
                // Need to concider the Child Expander Box
                if (Size_Expander_H == "false") { Expansion_Value = -1626; }

                ToggleControlY(Group_Box_Build_Requirements, null, Move_List_G, Expansion_Value);
                // And change the name of the Expander Button to - for the next usage
                Button_Expand_G.Text = "+";
            }
            //=====================// 

            Move_List_I = new List<Control>() { Group_Box_Build_Requirements, Group_Box_Costum_Tags, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As };
            // Same for the second Expander
            if (Size_Expander_I == "false")
            {
                int Expansion_Value = 2255;

                // Need to concider the Child Expander Box
                if (!Is_Team) { Expansion_Value = 1419; }

                ToggleControlY(Group_Box_Properties, null, Move_List_I, -Expansion_Value);            
                Button_Expand_I.Text = "+";
            }
            //=====================//

            Move_List_for_Team = new List<Control>() 
            { 
                Label_Is_Hero, Label_Show_Head, Label_God_Mode, Label_Use_Particle, Check_Box_Has_Hyperspace,
                Label_Starting_Spawned_Units, Label_Reserve_Spawned_Units, Label_Death_Clone, 
                Label_Death_Clone_Model, Label_Inactive_Behavoir, Label_Active_Behavoir, 
                Label_Start_Units_Amount, Label_Reserve_Units_Amount, Label_Death_Clone_Alo,

                Text_Box_Hyperspace_Speed, Text_Box_Starting_Unit_Name, Text_Box_Spawned_Unit,
                Text_Box_Reserve_Unit_Name, Text_Box_Reserve_Unit, Text_Box_Death_Clone, Text_Box_Death_Clone_Model,
                List_Box_Inactive_Behavoir, List_Box_Active_Behavoir,
               
                Switch_Button_Is_Hero, Switch_Button_Show_Head, Switch_Button_God_Mode, Switch_Button_Use_Particle,
                Picture_Box_Has_Hyperspace, Progress_Bar_Hyperspace_Speed, Track_Bar_Hyperspace_Speed, Track_Bar_Spawned_Unit,
                Track_Bar_Reserve_Unit, Button_Death_Clone_Model, Button_Move_to_Active, Button_Behavoir_Exchange, Button_Move_to_Inactive, Button_Collaps_I,               
                
                Group_Box_Build_Requirements, Group_Box_Costum_Tags, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As };


                ToggleControlY(Group_Box_Team, null, Move_List_for_Team, -836);
                // Changing Size of Parent Group Box as well
                Group_Box_Properties.Height += -836;


            //=====================//
            
            Move_List_Info_Text = new List<Control>() { Group_Box_Max_Values, Button_Reset_All_Settings, Button_Withdraw_Files, Button_Uninstall_Imperialware };
            ToggleControlY(Text_Box_Info, null, Move_List_Info_Text, -574);
            Toggle_Button(Picture_Box_License, "License", "License", 0, false);          

            //=====================//

            Move_List_H = new List<Control>() { Button_Collaps_G, Group_Box_Costum_Tags, Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As };
          
            if (Size_Expander_H == "false")
            {
                ToggleControlY(Group_Box_Requirements_Small, null, Move_List_H, -856);
                // And change the name of the Expander Button to - for the next usage
                Button_Expand_H.Text = "+";

                // Changing Size of Parent Group Box as well
                Group_Box_Build_Requirements.Height += -856;
            }               
            //=====================//

            Move_List_F = new List<Control>() { Check_Box_Game_Object_Files, Check_Box_Hard_Point_Data, Button_Save, Button_Test_Ingame, Button_Add_Into, Button_Save_As };
            // Same for the second Expander
            if (Size_Expander_F == "false")
            {
                ToggleControlY(Group_Box_Costum_Tags, null, Move_List_F, -710);
                // And change the name of the Expander Button to - for the next usage
                Button_Expand_F.Text = "+";
            }
            //=====================//        

            // Setting last Factions
            string[] The_Factions = All_Factions.Split(',');

            foreach (string Faction in The_Factions)
            { Combo_Box_Faction.Items.Add(Faction); }
       
            Combo_Box_Faction.Text = Selected_Faction;


            // Setting Playable Factions
            string[] The_Playable_Factions = Playable_Factions.Split(',');

            foreach (string Faction in The_Playable_Factions)
            { All_Playable_Factions.Add(Faction); }
            //=====================//

                 
            // Setting Language, according to the User Settings (Function in Functions.cs)
            Set_Language();

       

            ListView[] List_Views = { List_View_Teams, List_View_Selected_Tag, List_View_Dashboards, List_View_Theme_Selection };

            foreach (ListView List_View in List_Views)
            {// Optional: ListView als ListBox "tarnen"
                List_View.View = View.Details;
                List_View.HeaderStyle = ColumnHeaderStyle.None;
                List_View.FullRowSelect = true;
                List_View.Columns.Add("Farben");
                List_View.Columns[0].Width = List_View.Width - 8;
            }



            try // Playing Start Sound
            {   System.Media.SoundPlayer Start_Sound = new System.Media.SoundPlayer(Selected_Theme + @"Start.wav");
                Start_Sound.Play();
            }
            catch // If not existing in selected theme we take the default one
            {   try
            {
                System.Media.SoundPlayer Start_Sound = new System.Media.SoundPlayer(Program_Directory + @"Themes\Default\Start.wav");
                    Start_Sound.Play();
                } catch { }
            } 
               

 
            } catch { Error_Segment = 5; }
        


            // After the Check Boxes were checked by the program we need to get it running again, using this variable:
            User_Input = true;

        }


        // Used for events after loading function, mainly for error masseges
        private void MainWindow_Shown(object sender, EventArgs e)
        {          
            Loading_Completed = true;

            // Opening Team tab here, because otherwise it would interfere with load function above (timing issue)
            if (Launch_Team) 
            {   if (Size_Expander_I == "false") { Button_Expand_I_Click(null, null); }
                Switch_Button_Is_Team_Click(null, null); Launch_Team = false; 
            }


            // Reporting that no Program Directory is currently set
            if (Setting_Directory)
            {
                // Setting Savegame path (different for each user)
                Button_Reset_Savegame_Path_Click(null, null);

                Imperial_Console(600, 100, Add_Line + @"    If you wish you can change the ""Imperialware Folder Path""."
                                         + Add_Line + @"    And hit then the ""Set"" button.");
            }


            // Report function for all missloaded Variables
            if (Debug_Mode == "true" & Error_List.Count > 0)
            {
                string Error_Text = "";
                int Console_Height = 200;
                if (Error_List.Count > 5) { Console_Height = 600; }

                foreach (string Error in Error_List) { Error_Text += "    " + Error + Add_Line; }
                Imperial_Console(600, Console_Height, Add_Line + "    Error: Could not load " + Add_Line + Error_Text);

                Error_List.Clear();
            }

            if (Upgraded) { Imperial_Console(660, 100, Add_Line + "    Imperialware seems to have upgraded to Version " + Imperialware_Version.ToString());}


            if (Missed_Program_Dir) { Imperial_Console(600, 100, Add_Line + "    Error, wasn't able to move to " + Add_Line + Program_Directory); }

            if (Found_Install_Archive == false)
            {
                Imperial_Console(700, 150, @"I could not find ""Imperialware_Installation.zip"" in"
                               + Add_Line + "Imperialware's startup directory. Installed with minimal resources."
                               + Add_Line + "To install properly please delete " + Program_Directory
                               + Add_Line + "And start Imperialware with that .zip archive in the same directory.");
            }
                
            // If any of the Loading function segments is setting this error value it will be shown to the User
            else if (Error_Segment > 0) { Imperial_Console(500, 100, Add_Line + "    Loadig Error: Segment " + Error_Segment.ToString()); }
            

        }


        // Todo
        private Assembly ResolveEventHandler_01(object sender, ResolveEventArgs args)
        {
            //This handler is called only when the common language runtime tries to bind to the assembly and fails.
            
            //Load the assembly from the specified path.                    
            System.Reflection.Assembly Draggable_Control_Assembly = System.Reflection.Assembly.LoadFrom(Program_Directory + @"Misc\Control.Draggable.dll");  

            //Return the loaded assembly.
            return Draggable_Control_Assembly;
        }

        //================================================ Page 1: Launch ================================================

        
        private void Button_Launch_Mod_Mouse_Over(object sender, EventArgs e)
        {
            // ((PictureBox)sender).Image = Text_Image(Selected_Theme, "Button_Launch.png", 158, 158, 34, 60, 18, "Launch");          
            Set_Launch_Button(((PictureBox)sender), "Button_Launch_Highlighted.png", "Button_Launch.png", 34, "Launch", 158);       
        }

        private void Button_Launch_Mod_Mouse_Leave(object sender, EventArgs e)
        {
            // ((PictureBox)sender).Image = Text_Image(Selected_Theme, "Button_Silver.png", 158, 158, 34, 60, 18, "Launch");
            Set_Launch_Button(((PictureBox)sender), "Button_Launch.png", "Button_Launch.png", 34, "Launch", 158);
        }


        private void Button_Launch_Mod_Click(object sender, EventArgs e)
        {
            try // Playing Sound
            {   System.Media.SoundPlayer Launch_Sound = new System.Media.SoundPlayer(Selected_Theme + @"Launch.wav");
                Launch_Sound.Play();
            }
            catch // If not existing in selected theme we take the default one
            {
                try
                {   System.Media.SoundPlayer Launch_Sound = new System.Media.SoundPlayer(Program_Directory + @"Themes\Default\Launch.wav");
                    Launch_Sound.Play();
                }
                catch { }
            } 


            // Testing if any Mod is set as Default Mod:
            if (Mod_Name == "" | Mod_Name == "Choose_Mod")
            {
                Imperial_Console(700, 100, "    There is no default mod set, please choose a Mod in the Mods tab.");
                return;
            }

            // This maintains a history of 10 Savegame Sessions (one per Day)
            Store_Savegame_Cache();
         
        

            //====== Unloading the Map Editor Xml directory and replacing it back with the original one ======//
            
            string Game_Data = Game_Path + @"Data\";

            // Checking the variables in the Settings.txt file
            Map_Editor_Game = Load_Setting(Setting, "Map_Editor_Game");
            Map_Editor_EAW = Load_Setting(Setting, "Map_Editor_EAW");
            Map_Editor_FOC = Load_Setting(Setting, "Map_Editor_FOC");

      
            
            // Making sure we take the right Game path, otherwise the art lands in the wrong game ^^
            string Old_Game_Path = Game_Path_EAW;

            // Innitiating 2 Variables, and setting them to FOC if the last game was FOC
            string Original_Game_Data = Old_Game_Path + @"Data\";
            string Loaded_Map_Editor = Map_Editor_EAW;
       

            if (Map_Editor_Game == "FOC")
            {   Old_Game_Path = Game_Path_FOC;
                Original_Game_Data = Old_Game_Path + @"Data\"; 
                Loaded_Map_Editor = Map_Editor_FOC;               
            }



            // Moving the art files from previous editors away
            if (Directory.Exists(Original_Game_Data + @"Art\Models_Original") & Loaded_Map_Editor != "")
            {   Moving(Original_Game_Data + @"Art\Models", Old_Game_Path + @"Mods\" + Loaded_Map_Editor + @"\Data\Art\");
                Renaming(Original_Game_Data + @"Art\Models_Original", "Models");
            }

            if (Directory.Exists(Original_Game_Data + @"Art\Textures_Original") & Loaded_Map_Editor != "")
            {   Moving(Original_Game_Data + @"Art\Textures", Old_Game_Path + @"Mods\" + Loaded_Map_Editor + @"\Data\Art\");
                Renaming(Original_Game_Data + @"Art\Textures_Original", "Textures");
            }


            /*
            // Copying Security backups back to their Position (they ware saved from the Map editor which would delete them together with all .tga's).       
            string Source_Directory = Game_Path + Mods_Directory + Loaded_Map_Editor + @"\Data\Art\Shaders\Original_Textures";
            string Destination_Directory = Game_Path + Mods_Directory + Loaded_Map_Editor + @"\Data\Art\Textures";


            // If a entry exists for the Last mod opend by the Map Editor
            if (Loaded_Map_Editor != "" & Directory.Exists(Game_Path + Mods_Directory + Loaded_Map_Editor))
            {  
                // Re-Creating subdirectory structure in destination    
                foreach (string dir in Directory.GetDirectories(Source_Directory, "*", System.IO.SearchOption.AllDirectories))
                {
                    try { Directory.CreateDirectory(Destination_Directory + dir.Substring(Source_Directory.Length)); } catch { }
                }


                // Moving all non .dds files back to texture Directory
                foreach (string File_Name in Directory.GetFiles(Source_Directory, "*.*", System.IO.SearchOption.AllDirectories))
                {
                    // Replacing this folder in the middle of the Path that leads to the new path.
                    string New_Path = Path.GetFullPath(File_Name.Replace(@"Shaders\Original_Textures", "Textures"));

                    // Moving all files to new path
                    try { System.IO.File.Move(File_Name, New_Path); }
                    catch { }
                }
            }
             */

          
            if (Loaded_Map_Editor != "")
            {
                // Renaming Xml dir to the name of last active Mod        
                Renaming(Original_Game_Data + "Xml", Loaded_Map_Editor);
                // Then putting it into the inactive dir
                Moving(Original_Game_Data + Loaded_Map_Editor, Original_Game_Data + @"Xml_Inactive\");

                try 
                {   // And move the inactive Vanilla Xml dir back to its position               
                    Moving(Original_Game_Data + @"Xml_Inactive\Xml", Original_Game_Data);

                    if (Map_Editor_Game == "EAW") { Save_Setting(Setting, "Map_Editor_EAW", ""); }
                    else if (Map_Editor_Game == "FOC") { Save_Setting(Setting, "Map_Editor_FOC", ""); }
                } catch { }
             

                // Making sure the Commandbar was not deleted meanwhile
                if (!File.Exists(Original_Game_Data + @"Art\Textures\Mt_Commandbar.tga") & !File.Exists(Original_Game_Data + @"Art\Textures\Mt_Commandbar.dds"))
                {
                    File.Copy(Program_Directory + @"Misc\Command_Bars\" + Map_Editor_Game + @"\" + Costum_Commandbar + @"\Mt_Commandbar.dds", Original_Game_Data + @"Art\Textures\Mt_Commandbar.dds", true);
                    File.Copy(Program_Directory + @"Misc\Command_Bars\" + Map_Editor_Game + @"\" + Costum_Commandbar + @"\Mt_Commandbar.mtd", Original_Game_Data + @"Art\Textures\Mt_Commandbar.mtd", true);
                }

            }

            //==================//


            // We make sure the Core templates exist in the Mod Xml directory
            if (Directory.Exists(Xml_Directory))
            {
                if (!Directory.Exists(Xml_Directory + @"Core")) { Directory.CreateDirectory(Xml_Directory + @"Core"); }

                string[] Templates = {"Template_Space_Fighter.xml", "Template_Space_Bomber.xml",
                                      "Template_Space_Corvette.xml", "Template_Space_Frigate.xml", "Template_Space_Capitalship.xml",
                                      "Template_Land_Infantry.xml", "Template_Land_Vehicle.xml", "Template_Land_Air.xml", "Template_Hero.xml", "Template_Structure.xml"};

                foreach (string Template in Templates) { Validate_Xml_Template(Template); }              
            }

            //==================//


            // Defining Arguments
            string Argument_1 = @"MODPATH=Mods\" + Mod_Name;


            string Argument_2 = null;
            // Please be aware that we need that empty space between first and second argument or the game won't find the mod or language! 
            // If you try to Launch the game in a Language that doesen't exist as .dat file, the original EAW can crash,
            // So we give the User some control over this variable we use the Checkbox Check_Box_Use_Language and its value Use_Language


            string Speech_Path = @"Data\Audio\Speech\";
            string Text_Path = @"Data\Text\";
           
            if (Language_Mode == "Mod") {
                Speech_Path = Mods_Directory + Mod_Name + @"\Data\Audio\Speech\";
                Text_Path = Mods_Directory + Mod_Name + @"\Data\Text\";
            }


            if (Use_Language == "true")
            {
                if (Evade_Language == "true")
                {
                    if (Directory.Exists(Game_Path + Speech_Path + Game_Language)) { Argument_2 = " LANGUAGE=" + Game_Language; }
                    // Then we check the Game Speech folders and cycle through all other Languages and try to evade to the right one
                    else if (Game_Language != "English" & Directory.Exists(Game_Path + Speech_Path + "English")) { Argument_2 = " LANGUAGE=English"; }
                    else if (Game_Language != "German" & Directory.Exists(Game_Path + Speech_Path + "German")) { Argument_2 = " LANGUAGE=German"; }
                    else if (Game_Language != "French" & Directory.Exists(Game_Path + Speech_Path + "French")) { Argument_2 = " LANGUAGE=French"; }
                    else if (Game_Language != "Spanish" & Directory.Exists(Game_Path + Speech_Path + "Spanish")) { Argument_2 = " LANGUAGE=Spanish"; }
                    else if (Game_Language != "Italian" & Directory.Exists(Game_Path + Speech_Path + "Italian")) { Argument_2 = " LANGUAGE=Italian"; }
                    else if (Game_Language != "Russian" & Directory.Exists(Game_Path + Speech_Path + "Russian")) { Argument_2 = " LANGUAGE=Russian"; }
                   
                    else // If no language directory was identified we are going to check the text .dat files
                    {   
                        if (File.Exists(Game_Path + Text_Path + "MasterTextFile_" + Game_Language + ".dat")) { Argument_2 = " LANGUAGE=" + Game_Language; }
                        // Then we check the Game .text files and cycle through all other Languages and try to evade to the right one
                        else if (Game_Language != "English" & File.Exists(Game_Path + Text_Path + "MasterTextFile_English.dat")) { Argument_2 = " LANGUAGE=English"; }
                        else if (Game_Language != "German" & File.Exists(Game_Path + Text_Path + "MasterTextFile_German.dat")) { Argument_2 = " LANGUAGE=German"; }
                        else if (Game_Language != "French" & File.Exists(Game_Path + Text_Path + "MasterTextFile_French.dat")) { Argument_2 = " LANGUAGE=French"; }
                        else if (Game_Language != "Italian" & File.Exists(Game_Path + Text_Path + "MasterTextFile_Italian.dat")) { Argument_2 = " LANGUAGE=Italian"; }
                        else if (Game_Language != "Spanish" & File.Exists(Game_Path + Text_Path + "MasterTextFile_Spanish.dat")) { Argument_2 = " LANGUAGE=Spanish"; }
                        else if (Game_Language != "Russian" & File.Exists(Game_Path + Text_Path + "MasterTextFile_Russian.dat")) { Argument_2 = " LANGUAGE=Russian"; }
                        else
                        {
                            Imperial_Console(600, 100, Add_Line + Data_Directory + @"Text\MasterTextFile_" + Game_Language + ".dat"
                                                     + Add_Line + " doesn't exist, please try a other language in Game Settings.");
                            return;
                        }
                    }
                }
                // Otherwise we just use the language defined by the user
                else { Argument_2 = " LANGUAGE=" + Game_Language; }
            }


            // Attaching the additional option for "Windowed_Mode" to the second parameter
            if (Windowed_Mode == "true") { Argument_2 = Argument_2 + "- Windowed"; }
       
          

            // This function cycles at each launch to replace .tga or .dds files for Splash, Menuback_Overlay, Loadmenuback and anything else the program finds in these directories!
            if (Directory.Exists(Art_Directory + @"Textures\Cycle_Images")) { Switch_Loading_Screens(); }



            // If the User checked the checkbox for playing Vanilla game only
            if (Play_Vanilla_Game == "true")
            {
                // If the game is set to Empire at War
                if (Game_Path == Game_Path_EAW)
                {
                    // Starting the Game using this path and the arguments:
                    System.Diagnostics.Process.Start(Game_Path + "sweaw.exe", Argument_2);
                }
                // If the game is set to Foreces of Corruption
                else if (Game_Path == Game_Path_FOC)
                {   // Starting the Game using this path and the arguments:
                    System.Diagnostics.Process.Start(Game_Path + "swfoc.exe", Argument_2);
                }
            } // Otherwise we check if the Mod was set for EAW or FOC and launch that mod

            else
            {
                Temporal_A = Mod_Name;

                // This line just makes sure we use the same info directory for different version of the Stargate TPC Mod
                if (Mod_Name == "StargateAdmin" | Mod_Name == "StargateBeta" | Mod_Name == "StargateOpenBeta") { Temporal_A = "Stargate"; }

                // Gathering Mod and Game Information    
                string Info_File = Program_Directory + Mods_Directory + Temporal_A + @"\Info.txt";            
                string Info_Game = Load_Setting(Info_File, "Game");       

              

                // If the game is set to Empire at War
                if (Game_Path == Game_Path_EAW)
                {
                    if (!Directory.Exists(Game_Path + Mods_Directory + Mod_Name) & Info_Game == "Forces_of_Corruption")
                    {   // If Mod Directory not found we try the other Game                     
                        System.Diagnostics.Process.Start(Game_Path_FOC + "swfoc.exe", Argument_1 + Argument_2);                   
                    }
                     
                    try
                    {   // Starting the Game using this path and the arguments:
                        System.Diagnostics.Process.Start(Game_Path + "sweaw.exe", Argument_1 + Argument_2);
                    }
                    catch
                    {   Imperial_Console(700, 100, Add_Line + "    Sorry, the Mod you are trying to launch is not for Empire at War."
                                                 + Add_Line + "    Please select the Forces of Corruption checkbox in the Mods tab.");
                        return;
                    }                                         
                }

                // If the game is set to Forces of Corruption
                else if (Game_Path == Game_Path_FOC)
                {
                    if (!Directory.Exists(Game_Path + Mods_Directory + Mod_Name) & Info_Game == "Empire_at_War")
                    { System.Diagnostics.Process.Start(Game_Path_EAW + "sweaw.exe", Argument_1 + Argument_2); }

                    try
                    {   // Starting the Game using this path and the arguments:
                        System.Diagnostics.Process.Start(Game_Path + "swfoc.exe", Argument_1 + Argument_2);
                    }
                    catch
                    {   Imperial_Console(700, 100, Add_Line + "    Sorry, the Mod you are trying to launch is not for Empire at War."
                                                 + Add_Line + "    Please select the Forces of Corruption checkbox in the Mods tab.");
                        return;
                    }                   
                }                           
            }

            // Exiting the Application to save RAM and not bug the User in the Background
            // this.Close();
            if (Close_After_Launch == "true") Application.Exit();
        }
    


        private void Button_Mod_Only_Mouse_Over(object sender, EventArgs e)
        {
            Set_Launch_Button(((PictureBox)sender), "Button_Mod_Highlighted.png", "Button_Mod.png", 20, "Mod Only", 100);
        }

        private void Button_Mod_Only_Mouse_Leave(object sender, EventArgs e)
        {
            Set_Launch_Button(((PictureBox)sender), "Button_Mod.png", "Button_Launch.png", 20, "Mod Only", 100);         
        }


        private void Button_Mod_Only_Click(object sender, EventArgs e)
        {
            try // Playing Sound
            {
                System.Media.SoundPlayer Launch_Mod_Sound = new System.Media.SoundPlayer(Selected_Theme + @"Launch_Mod.wav");
                Launch_Mod_Sound.Play();
            }
            catch // If not existing in selected theme we take the default one
            {   try
                {   System.Media.SoundPlayer Launch_Mod_Sound = new System.Media.SoundPlayer(Program_Directory + @"Themes\Default\Launch_Mod.wav");
                    Launch_Mod_Sound.Play();
                } catch { }
            } 


            Silent_Mode = true;

            // Checking the loaded Addon variable in the Settings.txt file
            Loaded_Addon = Load_Setting(Setting, "Loaded_Addon");


            // We need to make sure the selected App/Addon unloads only if currently loaded
            if (Loaded_Addon != "" & Loaded_Addon != "false")
            { Button_Unload_Addon_Click(null, null); }


            if (Play_Vanilla_Game == "true") { Toggle_Checkbox(Check_Box_No_Mod, Setting, "Play_Vanilla_Game"); }

            // This button acts as shortcut to use the Launch Mod button in the Launch tab.
            if (Mod_Name != "Inactive" | Mod_Name != "") { Button_Launch_Mod_Click(null, null); }
            else { Imperial_Console(600, 100, Add_Line + "    No mod seems to be selected"); }
            Silent_Mode = false;
        }



        private void Button_Launch_Addon_Mouse_Over(object sender, EventArgs e)
        {
            Set_Launch_Button(((PictureBox)sender), "Button_Addon_Highlighted.png", "Button_Addon.png", 8, "Addon + Mod", 100); 
        }

        private void Button_Launch_Addon_Mouse_Leave(object sender, EventArgs e)
        {
            Set_Launch_Button(((PictureBox)sender), "Button_Addon.png", "Button_Launch.png", 8, "Addon + Mod", 100);            
        }


        private void Button_Launch_Addon_Click(object sender, EventArgs e)
        {
            try // Playing Sound
            {   System.Media.SoundPlayer Launch_Addon_Sound = new System.Media.SoundPlayer(Selected_Theme + @"Launch_Addon.wav");
                Launch_Addon_Sound.Play();
            }
            catch // If not existing in selected theme we take the default one
            {
                try
                {   System.Media.SoundPlayer Launch_Addon_Sound = new System.Media.SoundPlayer(Program_Directory + @"Themes\Default\Launch_Addon.wav");
                    Launch_Addon_Sound.Play();
                } catch { }
            } 


            // Checking the loaded Addon variable in the Settings.txt file
            Loaded_Addon = Load_Setting(Setting, "Loaded_Addon");
            Last_Addon = Load_Setting(Setting, "Last_Addon");

            // We need to make sure the selected App/Addon loads
            if (Loaded_Addon == "" & Loaded_Addon == "false")
            {
                Imperial_Console(600, 100, " Sorry, currently no Addon seems to be loaded. Please load a Addon in the Apps tab.");
                return;
            }


            if (!Directory.Exists(Data_Directory + @"Xml_Inactive\Xml"))
            {
                // That means the Addon is listed but not loaded so we need to re-load it                    
                Selected_App = Last_Addon;
                Button_Load_Addon_Click(null, null);
            }

            // This button acts as shortcut to use the Launch Mod button in the Launch tab.
            if (Mod_Name != "" & Mod_Name != "Inactive")
            { Button_Launch_Mod_Click(null, null);}
            else { Imperial_Console(600, 100, Add_Line + "    No mod seems to be selected"); }
        }

    
    
         
        //================================================ Page 2: Mods ================================================

        private void Button_Select_EAW_CheckedChanged(object sender, EventArgs e)
        {
            // Switching Name Text Color, in order to highlight the selected Checkbox
            Button_Select_EAW.ForeColor = Color_03;
            Button_Select_FOC.ForeColor = Color_02;

            // Setting path to the chosen Game      
            Save_Setting("0", "Game_Path", Game_Path_EAW);           

            // Displaying Name of Game
            Label_Game_Name.Text = "Empire at War";



            string Theme_Path = Program_Directory + @"Themes\Star_Wars_Empire_at_War\";

            if (Play_Vanilla_Game == "true" & Auto_Theme == "true" & Background_Image_Path != Theme_Path) 
            {   try
                {   // We are going to set EAW as our Theme                              
                    Change_Theme(Theme_Path, false);

                } catch { }
            }


            // Calling the refresh funktions to get the content of the other directory
            Refresh_Mods_Click(null, null);

            // Choosing the right Vanilla game image
            List_Box_Mod_Selection_SelectedIndexChanged(null, null);
        }

        private void Button_Select_FOC_CheckedChanged(object sender, EventArgs e)
        {
            // Switching Name Text Color, in order to highlight the selected Checkbox
            Button_Select_EAW.ForeColor = Color_02;
            Button_Select_FOC.ForeColor = Color_03;

            Save_Setting("0", "Game_Path", Game_Path_FOC);  

            Label_Game_Name.Text = "Forces of Corruption";



            string Theme_Path = Program_Directory + @"Themes\Star_Wars_Forces_of_Corruption\";

            if (Play_Vanilla_Game == "true" & Auto_Theme == "true" & Background_Image_Path != Theme_Path)
            {   try
                {                                
                    Change_Theme(Theme_Path, false);
                } catch { }
            }


            // Calling the refresh funktions to get the content of the other directory
            Refresh_Mods_Click(null, null);

            // Choosing the right Vanilla game image
            List_Box_Mod_Selection_SelectedIndexChanged(null, null);
        }

        private void Check_Box_No_Mod_CheckedChanged(object sender, EventArgs e)
        {   
            // If pressed by the User:
            if (User_Input == true)
            { 
                if (Check_Box_No_Mod.Checked) 
                {  // Then we change the font color and save that variable as true, so the Launching function knows how to start
                    Check_Box_No_Mod.ForeColor = Color_03;                   
                    Save_Setting(Setting, "Play_Vanilla_Game", "true");

                    Label_Mod_Name.Text = "Inactive";
                    Label_Mod_Name_2.Text = "Inactive";

      
                    string Theme_Path = Program_Directory + @"Themes\Star_Wars_Empire_at_War\";
                    if (Game_Path == Game_Path_FOC) { Theme_Path = Program_Directory + @"Themes\Star_Wars_Forces_of_Corruption\"; }


                    if (Auto_Theme == "true")
                    {                           
                        try 
                        {   
                            Change_Theme(Theme_Path, false);
                         
                            // Showing the right tab, so the user gets the connection
                            // if (User_Input == true) { Tab_Control_01.SelectedIndex = 0; }
                        } catch { Imperial_Console(740, 100, Add_Line + "    Could not find the " + Program_Directory + @"Themes\Game\Background image" + Theme_Path + Background_Image_A); }
                    }

                    // Selecting the Vanilla game image
                    List_Box_Mod_Selection_SelectedIndexChanged(null, null);
                }
                else
                {
                    Check_Box_No_Mod.ForeColor = Color_02;
                    Save_Setting(Setting, "Play_Vanilla_Game", "false");

                    Label_Mod_Name.Text = Mod_Name;
                    Label_Mod_Name_2.Text = Mod_Name;


                    try
                    {   // Resetting background Image according to theme  

                        Change_Theme(Selected_Theme, false);

                        // Save_Setting(Setting, "Background_Image_Path", Selected_Theme);

                        // Adjust_Wallpaper(Selected_Theme + Background_Image_A);
                    } catch {}


                    List_Box_Mod_Selection_SelectedIndexChanged(null, null);
                }
            }
        }




        private void List_Box_Mod_Selection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Getting the currently selected Mod or Game 
            string Current_Selection = List_Box_Mod_Selection.GetItemText(List_Box_Mod_Selection.SelectedItem).ToString();


            //================= Showing Game Image =================//

            string The_Image = "";
            bool File_Not_Found = false;


            if (Play_Vanilla_Game == "true")
            {
                string Image_Path = "";

                if (Game_Path == Game_Path_EAW) { Image_Path = Program_Directory + @"Themes\Star_Wars_Empire_at_War\"; }
                else if (Game_Path == Game_Path_FOC) { Image_Path = Program_Directory + @"Themes\Star_Wars_Forces_of_Corruption\"; }



                if (File.Exists(Image_Path + "01.png")) { The_Image = "01.png"; }
                else if (File.Exists(Image_Path + "01.jpg")) { The_Image = "01.jpg"; }

                try
                {
                    int X_Size = Mod_Image_Size;
                    int Y_Size = Mod_Image_Size;

                    Mod_Picture.Width = X_Size;
                    Mod_Picture.Height = Y_Size;
                    Mod_Picture.BackgroundImage = Resize_Image(Image_Path, The_Image, X_Size, Y_Size);

                    Text_Box_Mod_Description.Text = Image_Path + "Description_English.txt";
                    Label_Mod_Info.Text = "";
                
                }
                catch { File_Not_Found = true; }


                //======= Showing Game Text =======//
                try
                {
                    string Result = "";
                    string Info_Text = "";

                    string The_Language = Game_Language;
                    if (!File.Exists(Image_Path + "Description_" + The_Language + ".txt")) { The_Language = "English"; }


                    foreach (string Line in File.ReadLines(Image_Path + "Description_" + The_Language + ".txt"))
                    {
                        // If contains our Variable or if the line doesent start with //, # or :: (Comments) we remove it
                        if (Regex.IsMatch(Line, "^//.*?") | Regex.IsMatch(Line, "^#.*?") | Regex.IsMatch(Line, "^::.*?")) { Regex.Replace(Line, ".*?", ""); }

                        else if (Regex.IsMatch(Line, "^Link.*?"))
                        {
                            // Then we split the line into a string array, at positon of the = sign.
                            string[] Value = Line.Split('=');
                            try { Result = Value[1]; }
                            catch { }
                            Regex.Replace(Result, " ", "");
                        }
                        else { Info_Text += Line + "\n"; }
                    }

                    // Then we are going to show the text we just scanned into the Info_Text variable in this Text Box
                    Text_Box_Mod_Description.Text = Info_Text;
                }
                catch
                {   // Resetting Textbox
                    Text_Box_Mod_Description.Text = "";
                }

                 // Exiting function, as the vanilla game intercepts all code below.
                 return;
            }
            //======================================//


     

            // IN CASE 2: This line just makes sure we use the same info directory for different version of the Stargate TPC Mod
            if (Current_Selection == "StargateAdmin" | Current_Selection == "StargateBeta" | Current_Selection == "StargateOpenBeta") { Current_Selection = "Stargate"; }

            // Distinguishing between EAW and FOC version of Stargate
            if (Game_Path == Game_Path_EAW & Current_Selection == "Stargate") { Current_Selection = "Stargate_Empire_at_War"; }



            //================= Showing Description.txt =================//
                   
            try
            {   string Result = "";
                string Info_Text = "";

                string The_Language = Game_Language;
                if (!File.Exists(Program_Directory + @"Mods\" + Current_Selection + @"\Descriptions\Description_" + The_Language + ".txt")) { The_Language = "English"; }


                foreach (string Line in File.ReadLines(Program_Directory + @"Mods\" + Current_Selection + @"\Descriptions\Description_" + The_Language + ".txt"))
                {
                    // If contains our Variable or if the line doesent start with //, # or :: (Comments) we remove it
                    if (Regex.IsMatch(Line, "^//.*?") | Regex.IsMatch(Line, "^#.*?") | Regex.IsMatch(Line, "^::.*?")) { Regex.Replace(Line, ".*?", ""); }

                    else if (Regex.IsMatch(Line, "^Link.*?"))
                    {
                        // Then we split the line into a string array, at positon of the = sign.
                        string[] Value = Line.Split('=');
                        // Value[0] is the Variable itself, Value[1] is its Value! We try it because sometimes the value is empty, to prevent it from loading a non existing table
                        try { Result = Value[1]; } catch { }
                        Regex.Replace(Result, " ", "");
                    }
                    else { Info_Text += Line + "\n"; }
                }

                // Then we are going to show the text we just scanned into the Info_Text variable in this Text Box
                Text_Box_Mod_Description.Text = Info_Text;
            }
            catch
            {   // Resetting Textbox
                Text_Box_Mod_Description.Text = "";
            }



     
            //======== If a data directory of this mod is found in Imperialwares Directory: showing Mod Image ========//
            if (Directory.Exists(Program_Directory + @"Mods\" + Current_Selection))
            {   // We are going to look for a Mod Image of it to use
            
                if (Cycle_Mod_Image == "true") 
                {
                    // Innitiating int list and adding just 0 to get rid of the 0 slot, so we can later start at 1
                    List<string> Found_Numbers = new List<string>();
                    Found_Numbers.Add("0");

                    // This for loop iterates up to 20 until all images were found
                    for (int i = 1; i <= 20; ++i)
                    {

                        // We need to concider the 0 from the first 9 digits, we use this state to test the value.
                        if (i < 10)
                        {   // Storing the found images in a string table, we are going to randomely choose one of them from that list later
                            if (File.Exists(Program_Directory + @"Mods\" + Current_Selection + @"\Images\0" + i + ".png")) { Found_Numbers.Add("0" + i + ".png"); }
                            // .jpg has priority but if no .jpg was found we try .png and other formats
                            else if (File.Exists(Program_Directory + @"Mods\" + Current_Selection + @"\Images\0" + i + ".jpg")) { Found_Numbers.Add("0" + i + ".jpg"); }
                        }
                        else
                        {
                            if (File.Exists(Program_Directory + @"Mods\" + Current_Selection + @"\Images\" + i + ".png")) { Found_Numbers.Add(i + ".png"); }
                            else if (File.Exists(Program_Directory + @"Mods\" + Current_Selection + @"\Images\" + i + ".jpg")) { Found_Numbers.Add(i + ".jpg"); }
                        }
                    }


                    // Generating random Variable
                    Random Randomizer = new Random();
                    // Generating random Number from this range
                    int Random = Randomizer.Next(1, Found_Numbers.Count());


                    try
                    {   int X_Size = Mod_Image_Size;
                        int Y_Size = Mod_Image_Size;

                        Mod_Picture.Width = X_Size;
                        Mod_Picture.Height = Y_Size;
                        Mod_Picture.BackgroundImage = Resize_Image(Program_Directory + @"Mods\" + Current_Selection + @"\Images\", Found_Numbers[Random], X_Size, Y_Size);
                        // Disabling Text as long it is not needed
                        Label_Mod_Info.Text = "";
                    } catch { File_Not_Found = true; }

                    Found_Numbers.Clear();
                }

                // This runns if the Cycle function for the mod image viewer is disabled
                else if (Cycle_Mod_Image == "false")
                {
                   
                    if (File.Exists(Program_Directory + @"Mods\" + Current_Selection + @"\Images\01.png")) { The_Image = "01.png"; }
                    else if (File.Exists(Program_Directory + @"Mods\" + Current_Selection + @"\Images\01.jpg")) { The_Image = "01.jpg"; }

                    try
                    {
                        int X_Size = Mod_Image_Size;
                        int Y_Size = Mod_Image_Size;

                        Mod_Picture.Width = X_Size;
                        Mod_Picture.Height = Y_Size;
                        Mod_Picture.BackgroundImage = Resize_Image(Program_Directory + @"Mods\" + Current_Selection + @"\Images\", The_Image, X_Size, Y_Size);                       
                    } catch { File_Not_Found = true; }
                }


                if (File_Not_Found == true)
                {   try { Mod_Picture.BackgroundImage = new Bitmap(Program_Directory + @"Images\Plain_Mod_Image.jpg"); } catch { }
                    Label_Mod_Info.Text = Current_Selection;
                }
            }

            else
            { // Setting the Mod Image to black if no mod is selected            
                try { Mod_Picture.BackgroundImage = new Bitmap(Program_Directory + @"Images\Plain_Mod_Image.jpg"); } catch { }

                // If the selection is not the error massage from the Refresh_Mods_Click() function below
                if (!Current_Selection.Contains("seems to be wrong.") & !Current_Selection.Contains("Please use Settings/Reset")) 
                { Label_Mod_Info.Text = Current_Selection; }               
            }

            // Focusing so the user can use the arrow keys to navigate the Mods
            List_Box_Mod_Selection.Focus();       
        }





        private void Button_Open_Map_Editor_Click(object sender, EventArgs e)
        {
            // Making sure Map Editor is installed
            try 
            {   if (Game_Path == Game_Path_EAW)
                {
                    if (File.Exists(Game_Path + "EAW Terrain Editor.exe"))
                    {   // Deleting the old one because it delets all .tga files -.- it is going to be replaced by a patched one that donesent do that!
                        Deleting(Game_Path + "EAW Terrain Editor.exe"); 
                    }
                    // We provide it form Imperialware's Program Directory
                    Verify_Copy(Program_Directory + @"Misc\Map_Editor\EAW_Terrain_Editor.exe", Game_Path_EAW + "EAW_Terrain_Editor.exe"); 

                    Verify_Copy(Program_Directory + @"Misc\Map_Editor\binkw32.dll", Game_Path_EAW + "binkw32.dll");
                    Verify_Copy(Program_Directory + @"Misc\Map_Editor\mss32.dll", Game_Path_EAW + "mss32.dll");
                    Verify_Copy(Program_Directory + @"Misc\Map_Editor\PerceptionFunctionG.dll", Game_Path_EAW + "PerceptionFunctionG.dll");
                }
                else if (Game_Path == Game_Path_FOC)
                {
                    if (File.Exists(Game_Path + "EAW Terrain Editor.exe"))
                    { Deleting(Game_Path + "EAW Terrain Editor.exe"); }
                    Verify_Copy(Program_Directory + @"Misc\Map_Editor\FOC_Terrain_Editor.exe", Game_Path_FOC + "FOC_Terrain_Editor.exe"); 

                    Verify_Copy(Program_Directory + @"Misc\Map_Editor\binkw32.dll", Game_Path_FOC + "binkw32.dll");
                    Verify_Copy(Program_Directory + @"Misc\Map_Editor\mss32.dll", Game_Path_FOC + "mss32.dll");
                    Verify_Copy(Program_Directory + @"Misc\Map_Editor\PerceptionFunctionG.dll", Game_Path_FOC + "PerceptionFunctionG.dll");
                }
            } catch { Imperial_Console(600, 100, Add_Line + @"    Imperialware's Map editor is not installed, please download"
                                               + Add_Line + @"    ""Imperialware_Installation.zip"" and drop it into Imperialwares" 
                                               + Add_Line + @"    start Directory. Then restart Imperialware and try again."); }
                      
         
            
            // Getting the currently selected Mod or Game 
            string Current_Selection = List_Box_Mod_Selection.GetItemText(List_Box_Mod_Selection.SelectedItem).ToString();

        
            // Checking the variables in the Settings.txt file
            Map_Editor_Game = Load_Setting(Setting, "Map_Editor_Game");
            Map_Editor_EAW = Load_Setting(Setting, "Map_Editor_EAW");
            Map_Editor_FOC = Load_Setting(Setting, "Map_Editor_FOC");
            Copy_Art_Into_Editor = Load_Setting(Setting, "Copy_Art_Into_Editor");


            string Game_Data = Game_Path + @"Data\";

            // Innitiating 2 Variables, and setting them to FOC if the last game was FOC
            string Original_Game_Data = Game_Path_EAW + @"Data\";                   
            string Loaded_Map_Editor = Map_Editor_EAW;
            if (Map_Editor_Game == "FOC") { Original_Game_Data = Game_Path_FOC + @"Data\"; Loaded_Map_Editor = Map_Editor_FOC; }
       

            // Loading last Editor Mod from settings.txt if none is selected right now
            if (Current_Selection == "") { Current_Selection = Loaded_Map_Editor; }
          

            // If nothing is selected
            if (Loaded_Map_Editor == "" & Current_Selection == "" & Play_Vanilla_Game == "false")
            { Imperial_Console(600, 100, Add_Line + @"    Please select a mod or the ""No Mod"" Checkbox."); return; }



            //====================== Vanilla Option ======================
            if (Play_Vanilla_Game == "true")
            {
                if (Loaded_Map_Editor != "")
                {
                    // Renaming Xml dir to the name of last active Mod        
                    Renaming(Original_Game_Data + "Xml", Loaded_Map_Editor);
                    // Then putting it into the inactive dir
                    Moving(Original_Game_Data + Loaded_Map_Editor, Original_Game_Data + @"Xml_Inactive\");


                    // And move the inactive Vanilla Xml dir back to its position
                    try { Moving(Original_Game_Data + @"Xml_Inactive\Xml", Original_Game_Data); } catch { }
                }

                // Clearing Variables
                Save_Setting(Setting, "Map_Editor_Game", "");
                Save_Setting(Setting, "Map_Editor_EAW", "");
                Save_Setting(Setting, "Map_Editor_FOC", "");

                if (Game_Path == Game_Path_EAW)
                {   // Starting the Terrain Editor of the selected Game, the Map Editor will delete all .tga's in texture directory!!
                    try { System.Diagnostics.Process.Start(Game_Path_EAW + "EAW_Terrain_Editor.exe"); } catch { } 
                }
                else if (Game_Path == Game_Path_FOC)
                {
                    try { System.Diagnostics.Process.Start(Game_Path_FOC + "FOC_Terrain_Editor.exe"); } catch { } 
                }

                // Escaping if no mod is supposed to load
                return;
            }
            //======================================================


            // Directly starting Editor if the selection is the same as last time
            if (Current_Selection == Loaded_Map_Editor & Play_Vanilla_Game == "false")
            {
                if (Map_Editor_Game == "EAW")
                { try { System.Diagnostics.Process.Start(Game_Path_EAW + "EAW_Terrain_Editor.exe"); } catch { } }
                else if (Map_Editor_Game == "FOC")
                { try { System.Diagnostics.Process.Start(Game_Path_FOC + "FOC_Terrain_Editor.exe"); } catch { } }

                // Exiting function
                return;
            }

            // Otherwise we unload the other loaded Mod from Map Editor (this only triggers to change for the current mod because of Game_Data == Original_Game_Data)
            else if (Loaded_Map_Editor != Current_Selection & Loaded_Map_Editor != "")
            {                                                             
                // Renaming Xml dir to the name of last active Mod        
                Renaming(Original_Game_Data + "Xml", Loaded_Map_Editor);
                // Then putting it into the inactive dir
                Moving(Original_Game_Data + Loaded_Map_Editor, Original_Game_Data + @"Xml_Inactive\");


                // And move the inactive Vanilla Xml dir back to its position
                try { Moving(Original_Game_Data + @"Xml_Inactive\Xml", Original_Game_Data); } catch { }
             
            }

          
            
            //======= Checking Directories =======//        
            if (!Directory.Exists(Game_Path + Mods_Directory)) { Directory.CreateDirectory(Game_Path + Mods_Directory); }

            // If no Backup of the original Xml directory exists we are going to create one          
            if (!Directory.Exists(Game_Data + "Xml_Backup")) { Copy_Now(Game_Data + "Xml", Game_Data + "Xml_Backup"); }
            




            // INSTALLATION: If no Mod Xml Directory with the same name is installed in the .xml directory of the Game or loaded in the Game .xml directory
            if (Directory.Exists(Game_Path + @"Mods\" + Current_Selection) & !Directory.Exists(Game_Data + @"Xml_Inactive\" + Current_Selection) & Loaded_Map_Editor != Current_Selection)
            {
                // We need a new inactive Xml directory because the current one is being replaced by the installed Addon
                if (!Directory.Exists(Game_Data + @"Xml_Inactive\Xml"))
                {   // Another copy of the Original mod Xml goes into inactive, because the Addon we install now is active
                    try { Copy_Now(Game_Data + "Xml_Backup", Game_Data + @"Xml_Inactive\Xml"); }
                    catch { Copy_Now(Game_Data + "Xml", Game_Data + @"Xml_Inactive\Xml"); }
                }

                       

                // Writing a Copy of the Mod .xml directory into the one of the Game, so the Map Editor has full access to all Xml instances.
                Copy_Now(Game_Path + @"Mods\" + Current_Selection + @"\Data\Xml\", Game_Data + @"Xml\");


                // Saving a copy of all Mod Art assets into the Game Art Directory and overwriting all files
                if (Copy_Art_Into_Editor == "true")
                {   Copy_Now(Game_Path + @"Mods\" + Current_Selection + @"\Data\Art\Models\", Game_Data + @"Art\Models\");
                    Copy_Now(Game_Path + @"Mods\" + Current_Selection + @"\Data\Art\Textures\", Game_Data + @"Art\Textures\");
                }                                                         
            }

            // If already installed
            else if (Directory.Exists(Game_Data + @"Xml_Inactive\" + Current_Selection))
            {
                Moving(Game_Data + "Xml", Game_Data + @"Xml_Inactive\");

                // Moving the last active Mod one level up 
                Moving(Game_Data + @"Xml_Inactive\" + Current_Selection, Game_Data);
                // Renaming last active Mod to "Xml"    
                Renaming(Game_Data + Current_Selection, "Xml");
            }




          
            /*
            string Source_Directory = Game_Path + Mods_Directory + Current_Selection + @"\Data\Art\Textures";
            string Destination_Directory = Game_Path + Mods_Directory + Current_Selection + @"\Data\Art\Shaders\Original_Textures";


            // Need this Directory to temporary drop Original Commandbar .tga and all other non .dds files into it
            if (!Directory.Exists(Source_Directory) & Current_Selection != "") { Directory.CreateDirectory(Source_Directory); }


            if (Current_Selection != "" & Directory.Exists(Game_Path + Mods_Directory + Current_Selection))
            {  
                // Re-Creating subdirectory structure in destination    
                foreach (string dir in Directory.GetDirectories(Source_Directory, "*", System.IO.SearchOption.AllDirectories))
                {
                    try { Directory.CreateDirectory(Destination_Directory + dir.Substring(Source_Directory.Length)); } catch { }

                }

                // Moving all non .dds files into secure directory to prevent the Map Editor.exe from deleting them!
                foreach (string File_Name in Directory.GetFiles(Source_Directory, "*.*", System.IO.SearchOption.AllDirectories))
                {
                    if (!File_Name.EndsWith(".dds") & !File_Name.EndsWith(".DDS"))
                    {
                        // Replacing this folder in the middle of the Path that leads to the new path.
                        string New_Path = File_Name.Replace("Textures", @"Shaders\Original_Textures");

                        // Moving all .tga files to new path
                        try { System.IO.File.Move(File_Name, New_Path); }
                        catch { }
                    }
                }
            }
            */

            // Making sure we take the right Game path, otherwise the art lands in the wrong game ^^
            string Old_Game_Path = Game_Path_EAW;

            if (Map_Editor_Game == "FOC")
            { Old_Game_Path = Game_Path_FOC; }
               

            // Moving the art files from previous editors away
            if (Directory.Exists(Original_Game_Data + @"Art\Models_Original") & Loaded_Map_Editor != "")
            {   Moving(Original_Game_Data + @"Art\Models", Old_Game_Path + @"Mods\" + Loaded_Map_Editor + @"\Data\Art\");
                Renaming(Original_Game_Data + @"Art\Models_Original", "Models");              
            }

            if (Directory.Exists(Original_Game_Data + @"Art\Textures_Original") & Loaded_Map_Editor != "")
            {   Moving(Original_Game_Data + @"Art\Textures", Old_Game_Path + @"Mods\" + Loaded_Map_Editor + @"\Data\Art\");
                Renaming(Original_Game_Data + @"Art\Textures_Original", "Textures");           
            }


            // Moving the art files around in order to save discspace!
            if (Copy_Art_Into_Editor == "false" & Current_Selection != "")
            {   Renaming(Game_Data + @"Art\Models", "Models_Original");
                Moving(Game_Path + @"Mods\" + Current_Selection + @"\Data\Art\Models", Game_Data + @"Art\");

                Renaming(Game_Data + @"Art\Textures", "Textures_Original");
                Moving(Game_Path + @"Mods\" + Current_Selection + @"\Data\Art\Textures", Game_Data + @"Art\");
            }
             




            // Changing meaning of "Current_Selection": Saving the new value for a different Mod
            if (Current_Selection != "") 
            {   if (Game_Path == Game_Path_EAW) 
                {   Save_Setting(Setting, "Map_Editor_EAW", Current_Selection);
                    Save_Setting(Setting, "Map_Editor_FOC", "");
                }
                else 
                {   Save_Setting(Setting, "Map_Editor_FOC", Current_Selection);
                    Save_Setting(Setting, "Map_Editor_EAW", "");
                }          
            }



            // LOADING: If our Mod exists in the Data Directory it is going to be renamed to Xml
            if (Directory.Exists(Game_Data + Loaded_Map_Editor))
            {
                Moving(Game_Data + "Xml", Game_Data + "Xml_Inactive");
                Renaming(Game_Data + Loaded_Map_Editor, "Xml");
            }


            if (!Directory.Exists(Game_Data + "Xml_Inactive"))
            {
                // Another copy of the Original mod Xml goes into inactive, because the Addon we install now is active
                Copy_Now(Game_Data + "Xml_Backup", Game_Data + @"Xml_Inactive\Xml");
            }
            
      
            // Finally saving game path so we know where to start from
            if (Game_Path == Game_Path_EAW)
            {   Save_Setting(Setting, "Map_Editor_Game", "EAW");
                // Starting the Terrain Editor of the selected Game, the Map Editor will delete all .tga's in texture directory!!
                try { System.Diagnostics.Process.Start(Game_Path_EAW + "EAW_Terrain_Editor.exe"); } catch { } 
            }
            else if (Game_Path == Game_Path_FOC)
            {   Save_Setting(Setting, "Map_Editor_Game", "FOC");
                try { System.Diagnostics.Process.Start(Game_Path_FOC + "FOC_Terrain_Editor.exe"); } catch { }
            }
                
        }



        private void Button_Set_Mod_Click(object sender, EventArgs e)
        {
            // Getting the currently selected Mod or Game 
            string Current_Selection = List_Box_Mod_Selection.GetItemText(List_Box_Mod_Selection.SelectedItem).ToString();


            // We push all current savegames of the Mod specigic Savegame directory of the former Mod_Name variable
            if (Use_Mod_Savegame_Dir == "true") { Push_Mod_Savegame_Directory(); }



            // Making sure the player can't replace the old selection with empty strings
            if (Current_Selection != "")
            {
                // Setting the Active Mod 
                Save_Setting(Setting, "Mod_Name", Current_Selection);

                // Now that "Mod_Name" changed, lets check whether there is any Directory of this mod exists that we could load to the savegame directory
                if (Use_Mod_Savegame_Dir == "true") { Pull_Mod_Savegame_Directory(); }



                // This line just makes sure we use the same info directory for different version of the Stargate TPC Mod
                if (Current_Selection == "StargateAdmin" | Current_Selection == "StargateBeta" | Current_Selection == "StargateOpenBeta") { Current_Selection = "Stargate"; }

                // Distinguishing between EAW and FOC version of Stargate
                if (Game_Path == Game_Path_EAW & Current_Selection == "Stargate") { Current_Selection = "Stargate_Empire_at_War"; }
                          

                if (!Directory.Exists(Program_Directory + Mods_Directory + Current_Selection + @"\Maximal_Values"))
                {   Directory.CreateDirectory(Program_Directory + Mods_Directory + Current_Selection + @"\Maximal_Values");
                    try { Copy_Now(Program_Directory + @"Misc\Maximal_Values\", Program_Directory + Mods_Directory + Current_Selection + @"\Maximal_Values\"); } catch { }
                }
            }       

            // Assigning new default mod to its Label
            Label_Mod_Name.Text = Mod_Name;
            Label_Mod_Name_2.Text = Mod_Name;

            // Reinnitializing variables because of the switched Mod Directory    
            Data_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\";
            Xml_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\Xml\";
            Art_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\Art\";


            // Clearing usage history to prevent "missing link" issues:   
            User_Input = false;
            Combo_Box_Faction.Text = "Load Factions";

            List_Box_Galaxy.Items.Clear();
            Save_Setting(Setting, "Selected_Galaxy", "");
            Save_Setting(Setting, "Found_Planets", "");
            
            List_Box_All_Instances.Items.Clear();
            Save_Setting(Setting, "Selected_Units", "");          

            // Human_Player means it auto sets to current player
            Combo_Box_Faction.Text = "Human_Player";  
            User_Input = true;

            // Forgetting cached Files from the last "Selected_Mod"
            Save_Setting(Setting, "Last_Editor_Xml", "");
            Save_Setting(Setting, "Selected_Xml", "");
            Save_Setting(Setting, "Selected_Instance", "");
          
                     

            Set_Maximal_Value_Directories();

            if (Maximal_Value_Class == "1") { Load_Maximal_Values(Maximum_Values_Fighter); }
            else if (Maximal_Value_Class == "2") { Load_Maximal_Values(Maximum_Values_Bomber); }
            else if (Maximal_Value_Class == "3") { Load_Maximal_Values(Maximum_Values_Corvette); }
            else if (Maximal_Value_Class == "4") { Load_Maximal_Values(Maximum_Values_Frigate); }
            else if (Maximal_Value_Class == "5") { Load_Maximal_Values(Maximum_Values_Capital); }

            else if (Maximal_Value_Class == "6") { Load_Maximal_Values(Maximum_Values_Infantry); }
            else if (Maximal_Value_Class == "7") { Load_Maximal_Values(Maximum_Values_Vehicle); }
            else if (Maximal_Value_Class == "8") { Load_Maximal_Values(Maximum_Values_Air); }
            else if (Maximal_Value_Class == "9") { Load_Maximal_Values(Maximum_Values_Hero); }
            else if (Maximal_Value_Class == "10") { Load_Maximal_Values(Maximum_Values_Structure); }


            if (Auto_Theme == "true" & Directory.Exists(Program_Directory + @"Themes\" + Current_Selection)) 
            {           
                Selected_Theme = Program_Directory + @"Themes\" + Current_Selection + @"\";
                Change_Theme(Selected_Theme, true);                         
            }

        }



        private void Button_Start_Mod_Click(object sender, EventArgs e)
        {         
            // Getting the currently selected Mod or Game and temporally setting the Active Mod
            Mod_Name = List_Box_Mod_Selection.GetItemText(List_Box_Mod_Selection.SelectedItem).ToString();

            // This button acts as shortcut to use the Launch Mod button in the Launch tab.
            Button_Launch_Mod_Click(null, null);

        }

   

        private void Refresh_Mods_Click(object sender, EventArgs e)
        {

            // Removing all listed items from the listbox in order to refresh
            List_Box_Mod_Selection.Items.Clear();
            Combo_Box_Dashboard_Mod.Items.Clear();

            // Loading the Modpath         

            string Mod_Path = Game_Path + Mods_Directory;

            // If the Mods directory was found 
            if (Directory.Exists(Mod_Path))
            {
                // We put all found directories inside of our target folder into a string table
                string[] File_Paths = Directory.GetDirectories(Mod_Path);
                int FileCount = File_Paths.Count();

                // Cycling up from 0 to the total count of folders found above in this directory;
                for (int Cycle_Count = 0; Cycle_Count < FileCount; Cycle_Count = Cycle_Count + 1)
                {
                    // Getting the Name only from all folder paths, Cycle_Count increases by 1 in each cycle 
                    string List_Value = Path.GetFileName(File_Paths[Cycle_Count]);
                    
                    // And inserting that folder name into the Listbox
                    if (List_Value != "" & List_Value != "Data") 
                    { 
                        List_Box_Mod_Selection.Items.Add(List_Value);

                        // And into the Combo box for Games/Dashboard selection
                        Combo_Box_Dashboard_Mod.Items.Add(List_Value);
                    }
                } 
            } 
         
            else // Probably the path is wrong
            {
                List_Box_Mod_Selection.Items.Add("Your Gamedir path seems to be wrong.");
                List_Box_Mod_Selection.Items.Add("Please use Settings/Reset or type it manually."); 
            }

            // Making sure this box displays these two in order to offer Imperialwares costum Dashboards
            if (!Combo_Box_Dashboard_Mod.Items.Contains("Stargate") & !Combo_Box_Dashboard_Mod.Items.Contains("StargateAdmin")
              & !Combo_Box_Dashboard_Mod.Items.Contains("StargateBeta") & !Combo_Box_Dashboard_Mod.Items.Contains("StargateOpenBeta")) 
            {Combo_Box_Dashboard_Mod.Items.Add("Stargate");}

            if (!Combo_Box_Dashboard_Mod.Items.Contains("Andromeda")) { Combo_Box_Dashboard_Mod.Items.Add("Andromeda"); }

        }


        private void Button_Save_Text_Box_Description_Click(object sender, EventArgs e)
        {
            string Current_Selection = List_Box_Mod_Selection.GetItemText(List_Box_Mod_Selection.SelectedItem).ToString();
            string The_Language = Game_Language;

            // This line just makes sure we use the same info directory for different version of the Stargate TPC Mod
            if (Current_Selection == "StargateAdmin" | Current_Selection == "StargateBeta" | Current_Selection == "StargateOpenBeta") { Current_Selection = "Stargate"; }

            // Distinguishing between EAW and FOC version of Stargate
            if (Game_Path == Game_Path_EAW & Current_Selection == "Stargate") { Current_Selection = "Stargate_Empire_at_War"; }


            // Just to make sure at least the english Description is shown
            if (!File.Exists(Program_Directory + @"Mods\" + Current_Selection + @"\Descriptions\Description_" + The_Language + ".txt")) { The_Language = "English"; }

            try 
            {   // This is the tiny button to save the Info text of each mod.
                File.WriteAllText(Program_Directory + @"Mods\" + Current_Selection + @"\Descriptions\Description_" + The_Language + ".txt", Text_Box_Mod_Description.Text);
            } catch { }

        }

        private void Track_Bar_Mod_Image_Size_Scroll(object sender, EventArgs e)
        {   // Ignoring zero to prevent it from null value exception
            if (Track_Bar_Mod_Image_Size.Value != 0)
            {
                int Scale_Bonus = 100;

                // 900 + 48 is the maximal scale size for stage 9
                if (Track_Bar_Mod_Image_Size.Value == 9) { Scale_Bonus = 48; }

                Mod_Image_Size = (Track_Bar_Mod_Image_Size.Value * 100) + Scale_Bonus;
                // Using Parameter "2" for int values, to bypass automatic variable setup which would return a error!
                Save_Setting("2", "Mod_Image_Size", Mod_Image_Size.ToString());

                // If the user just started the application with no item selected before we make sure the selected mod is.. selected ^^
                if (List_Box_Mod_Selection.SelectedIndex == -1) { List_Box_Mod_Selection.SelectedItem = Mod_Name; }
                List_Box_Mod_Selection_SelectedIndexChanged(null, null);
            }
        }
        

        //================================================ Page 3: Apps ================================================

        private void Button_Download_App_Click(object sender, EventArgs e)
        {   
            string Link = Load_Setting(Info_Directory_Path + Selected_App + @"\Info.txt", "Link");
            
            try { System.Diagnostics.Process.Start(Link); }
            catch {}
        }


        private void Button_Add_App_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Game_Path + Mods_Directory)) { Directory.CreateDirectory(Game_Path + Mods_Directory); }


            if (Selected_Object == "Mods")
            {
                // Just a little special case for Stargate EAW, this has to run after the info.txt check or Imperialware will confuse the game dirs
                if (Selected_App == "Stargate_Empire_at_War") { Selected_App = "Stargate"; }

                // Gathering Mod and Game Information   
                string Info_File = Program_Directory + @"Mods\" + Selected_App + @"\Info.txt";
                string Info_Game = Load_Setting(Info_File, "Game");

                // Itializing root Directory to the changed Modname  
                if (Info_Game == "Empire_at_War") { Temporal_A = Game_Path_EAW + Mods_Directory; }
                else if (Info_Game == "Forces_of_Corruption") { Temporal_A = Game_Path_FOC + Mods_Directory; }



                // If not Installed
                if (!Directory.Exists(Temporal_A + Selected_App))
                {
                    string Mod_Path = "";
                    string Selected_Mod = "";

                    using (var Folder_Browser_Dialog_1 = new FolderBrowserDialog())
                    {
                        if (Folder_Browser_Dialog_1.ShowDialog() == DialogResult.OK)
                        {
                            Mod_Path = Path.GetDirectoryName(Folder_Browser_Dialog_1.SelectedPath) + @"\";
                            Selected_Mod = Path.GetFileNameWithoutExtension(Folder_Browser_Dialog_1.SelectedPath);
                        }
                        else { return; } // Otherwise the User found no Mod or abborted.
                    }

                    if (!Directory.Exists(Mod_Path + Selected_Mod + @"\Data"))
                    {
                        Imperial_Console(600, 100, @"Could not find """ + Selected_Mod + @"\Data"". Please make sure "
                                                                        + Add_Line + "to select the directory one level over the Data folder."
                                                                        + Add_Line + "And also make sure its name is " + Selected_App);
                        return;
                    }

                    // Moving the selected Directory into the Mod Folder
                    Moving(Mod_Path + Selected_Mod, Temporal_A);


                    // Making sure the Mod Directory Name matches the one in Imperialware's library
                    if (Selected_Mod != Selected_App)
                    { Renaming(Temporal_A + Selected_Mod, Selected_App); }


                    // Showing Mod directory to the User                 
                    try { System.Diagnostics.Process.Start(Temporal_A); }
                    catch
                    {
                        Imperial_Console(700, 100, Add_Line + @"    Could not find " + Temporal_A
                            + Add_Line + @"    Please try hitting Settings/Reset Modpath or to find and type it manually.");
                    }
                }
                else { Imperial_Console(700, 100, "Sorry, " + Selected_App + " seems to already be installed."); }
            }

            else if (Selected_Object == "Addons")
            {
                // Otherwise its a addon that needs to be loaded or installed
                Button_Load_Addon_Click(null, null);
            }

        }


        private void Button_Remove_App_Click(object sender, EventArgs e)
        {   bool Switched_Game = false;
        
            if (Selected_Object == "Mods")
            {
                // If our App is the selected Mod for launching we are going to deactivate it first of all.
                if (Selected_App == Mod_Name) { Mod_Name = "Inactive"; }
                       
                // Just a little special case for Stargate EAW, this has to run after the info.txt check or Imperialware will confuse the game dirs
                if (Selected_App == "Stargate_Empire_at_War") { Selected_App = "Stargate"; }


                // Gathering Mod and Game Information   
                string Info_File = Program_Directory + @"Mods\" + Selected_App + @"\Info.txt";
                string Info_Game = Load_Setting(Info_File, "Game");

                // Reinnitializing Game Directories to the Hostmod   
                if (Info_Game == "Empire_at_War") { Game_Path = Game_Path_EAW; if (Game_Path != Game_Path_EAW) { Switched_Game = true; } }
                else if (Info_Game == "Forces_of_Corruption") { Game_Path = Game_Path_FOC; if (Game_Path != Game_Path_FOC) { Switched_Game = true; } }


                // Itializing root Directory to the changed Modname
                Temporal_A = Game_Path + Mods_Directory;                             
            }

            else // Otherwise it is a Addon
            {
                             
                // Checking the loaded Addon variable in the Settings.txt file
                Loaded_Addon = Load_Setting(Setting, "Loaded_Addon");

                // We need to make sure the selected App/Addon unloads before deletion
                if (Loaded_Addon == Selected_App) { Button_Unload_Addon_Click(null, null); }

                // Gathering Mod and Game Information   
                string Info_File = Program_Directory + @"Addons\" + Selected_App + @"\Info.txt";
                string Info_Game = Load_Setting(Info_File, "Game");
                // Setting Hostmod according to the requirement of the selected Addon
                Host_Mod = Load_Setting(Info_File, "Mod");
        

                // Reinnitializing Game Directories to the Hostmod   
                if (Info_Game == "Empire_at_War") { Game_Path = Game_Path_EAW; if (Game_Path != Game_Path_EAW) { Switched_Game = true; } }
                else if (Info_Game == "Forces_of_Corruption") { Game_Path = Game_Path_FOC; if (Game_Path != Game_Path_FOC) { Switched_Game = true; } }

                // Itializing root Directory to the changed Modname
                Temporal_A = Game_Path + Mods_Directory + Host_Mod + @"\Data\";                            
            }


            if (Directory.Exists(Temporal_A + Selected_App))
            {
                // Call Imperial's Dialogue with resolution of 540x160, Button 1 Name , Button 2 Name and Dialogue Text. Then we wait for user input
                Imperial_Dialogue(700, 160, "Trash it", "Cancel", "false", "    Are you sure you wish to move " + Selected_App + " into the recycle bin?");

                // If user verificated to delete the selected app it will be trashed by the "Deleting" costum function 
                if (Caution_Window.Passed_Value_A.Text_Data == "true")
                {
                    Deleting(Temporal_A + Selected_App);
                    Imperial_Console(500, 160, "    Removed " + Selected_App + Add_Line +
                    "    You can manually remove the app entry inside of " + Add_Line + "    " + Program_Directory + Selected_Object);
                    // Setting back to false to prevent missfiring when user closes the verification dialogue next time.
                    Caution_Window.Passed_Value_A.Text_Data = "false";
                }
            }
            else
            { Imperial_Console(500, 160, "The selected object seems to be not installed."); }
                  

            // Rolling back all Reinnitializations!  
            if (Switched_Game & Game_Path == Game_Path_EAW) { Game_Path = Game_Path_FOC; Switched_Game = false; }
            else if (Switched_Game & Game_Path == Game_Path_FOC) { Game_Path = Game_Path_EAW; Switched_Game = false; }
            
        }



        private void Button_Select_Mods_Click(object sender, EventArgs e)
        {
            Save_Setting(Setting, "Selected_Object", "Mods");

            // Refreshing layout
            Refresh_Apps();

            // Turning Selection buttons to unpressable state
            Picture_Box_Load_Addon.Visible = true;
            Picture_Box_Unload_Addon.Visible = true;

            Button_Load_Addon.Visible = false;
            Button_Unload_Addon.Visible = false;
        }

        private void Button_Select_Addons_Click(object sender, EventArgs e)
        {
            Save_Setting(Setting, "Selected_Object", "Addons");

            // Refreshing layout
            Refresh_Apps();

            // Turning Selection buttons selectable again
            Picture_Box_Load_Addon.Visible = false;
            Picture_Box_Unload_Addon.Visible = false;

            Button_Load_Addon.Visible = true;
            Button_Unload_Addon.Visible = true;
        }


        private void Button_Search_Object_Click(object sender, EventArgs e)
        {
            // Making sure list is not empty 
            if (Search_Index.Count == 0) { Search_Index.Add("First_Slot"); }

            string The_Text = "";
            bool Recently_Listed = false;


            try 
            {   // Because the user typed his search text into the textbox by now, we can search the list for his Text
                foreach (string File_Name in Get_Mod_Info_Directories())
                {
                    Recently_Listed = false;


                    if (Regex.IsMatch(File_Name, "(?i).*?" + Text_Box_Object_Searchbar.Text + ".*?"))
                    {
                        // We check all entries of the Search Intex list for the Entry                   
                        for (int i = Search_Index.Count - 1; i >= 0; --i)
                        {
                            var Item = Search_Index[i];

                            if (Item == File_Name)
                            {
                                Recently_Listed = true;
                                break;
                            }
                        }

                        if (Recently_Listed == false) // Otherwise it already was listed last time and we ignore it
                        {
                            Search_Index.Add(File_Name);

                            // Disabling user input so App_Image_Click() knows the command is piped by the search function
                            User_Input = false;
                            Selected_App = File_Name;
                            App_Image_Click(null, null);

                            return; // As we found and selected the first one
                        }
                    }
                }       
            } catch { }
           

            // If the function didn't returned by now that means nothing was found and we can reset the Index list
            Search_Index.Clear();   

        }



        // This event triggers for each Appstore button we click!!
        void App_Image_Click(object sender, EventArgs e)
        {           
            if (User_Input) { Selected_App = ((Button)sender).Name; }
            else { User_Input = true; }



            string Image_Path = Program_Directory + @"Mods\" + Selected_App + @"\Images\";

            // Exception for some mods with own images, there it will search their Addon directory
            string Exception_List = "Wrackage_and_Bodies_Stay_Mod";
            if (Exception_List.Contains(Selected_App) & Selected_Object == "Addons") { Image_Path = Info_Directory_Path + Selected_App + @"\Images\"; }

            else if (Selected_Object == "Addons")
            {
                // Gathering Mod and Game Information of the selected Addon    
                string Info_File = Program_Directory + @"Addons\" + Selected_App + @"\Info.txt";
                // Setting Hostmod according to the requirement of the selected Addon
                string Host_Mod = Load_Setting(Info_File, "Mod");

                // There are currently no Addons for TPC, so we choose Stargate - EAW
                if (Host_Mod == "Stargate") { Host_Mod = "Stargate_Empire_at_War"; }

                // Setting image path according to the mod inside of the Info.txt file
                Image_Path = Program_Directory + @"Mods\" + Host_Mod + @"\Images\";
            }

           


            // string Image_Path = Info_Directory_Path + Selected_App + @"\Images\";
            string Image_Name = "01.jpg";


            // If no .jpg was found we switch to .png
            if (!File.Exists(Image_Path + Image_Name)) { Image_Name = "01.png"; }

            Addon_Picture.BackgroundImage = Resize_Image(Image_Path, Image_Name, 256, 256);

            // Blacking out the Addon Image
            //((Button)sender).Image = new Bitmap (Program_Directory + @"Images\Plain_Mod_Image.jpg"); 



            //===== Showing Description.txt =====//
            try
            {
                string Result = "";
                string Info_Text = "";
                string The_Language = Game_Language;

                // Making sure english verion is shown if no other Language is available
                if (!File.Exists(Info_Directory_Path + Selected_App + @"\Descriptions\Description_" + The_Language + ".txt")) { The_Language = "English"; }


                foreach (string Line in File.ReadLines(Info_Directory_Path + Selected_App + @"\Descriptions\Description_" + The_Language + ".txt"))
                {
                    // If contains our Variable or if the line doesent start with //, # or :: (Comments) we remove it
                    if (Regex.IsMatch(Line, "^//.*?") | Regex.IsMatch(Line, "^#.*?") | Regex.IsMatch(Line, "^::.*?")) { Regex.Replace(Line, ".*?", ""); }

                    else if (Regex.IsMatch(Line, "^Link.*?"))
                    {
                        // Then we split the line into a string array, at positon of the = sign.
                        string[] Value = Line.Split('=');
                        // Value[0] is the Variable itself, Value[1] is its Value! We try it because sometimes the value is empty, to prevent it from loading a non existing table
                        try { Result = Value[1]; }
                        catch { }
                        Regex.Replace(Result, " ", "");
                    }
                    else { Info_Text += Line + "\n"; }
                }

                // Then we are going to show the text we just scanned into the Info_Text variable in this Text Box
                Text_Box_Addon_Description.Text = Info_Text;
            }
            catch
            {   // Resetting Textbox
                Text_Box_Addon_Description.Text = "";
            }
        }

        private void Button_Save_App_Box_Description_Click(object sender, EventArgs e)
        {
            string Current_Selection = Selected_App;
            string The_Language = Game_Language;

            // This line just makes sure we use the same info directory for different version of the Stargate TPC Mod
            if (Current_Selection == "StargateAdmin" | Current_Selection == "StargateBeta" | Current_Selection == "StargateOpenBeta") { Current_Selection = "Stargate"; }

            // Distinguishing between EAW and FOC version of Stargate
            if (Game_Path == Game_Path_EAW & Current_Selection == "Stargate") { Current_Selection = "Stargate_Empire_at_War"; }


            // Making sure english verion is shown if no other Language is available
            if (!File.Exists(Info_Directory_Path + Selected_App + @"\Descriptions\Description_" + The_Language + ".txt")) { The_Language = "English"; }

            try
            {   // This is the tiny button to save the Info text of each mod.
                File.WriteAllText(Info_Directory_Path + Selected_App + @"\Descriptions\Description_" + The_Language + ".txt", Text_Box_Addon_Description.Text);
            }
            catch { }
        }



        private void Button_Start_App_Click(object sender, EventArgs e)
        {
            if (Selected_Object == "Mods")
            {   // Trying to get the currently selected Mod or Game and temporally setting the Active Mod
                Mod_Name = Selected_App;

                // Gathering Mod and Game Information of the selected Mod    
                string Info_File = Program_Directory + @"Mods\" + Mod_Name + @"\Info.txt";
                // Setting Game according to the requirement of the selected Mod
                string Info_Game = Load_Setting(Info_File, "Game");


                // Reinnitializing Game Directories according to the Info_File   
                if (Info_Game == "Empire_at_War")
                {
                    // "0" means we save into the Settings.txt WITH Quotemarks
                    Save_Setting("0", "Game_Path", Game_Path_EAW);  
                    Label_Game_Name.Text = "Empire at War";
                    Button_Select_EAW.Select();
                }
                else if (Info_Game == "Forces_of_Corruption")
                {
                    Save_Setting("0", "Game_Path", Game_Path_FOC);  
                    Label_Game_Name.Text = "Forces of Corruption";
                    Button_Select_FOC.Select();
                }


                // Reinnitializing Directories to the changed Modname
                Data_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\";
                Xml_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\Xml\";
                Art_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\Art\";

                Set_Maximal_Value_Directories();   
            }
          

            else // Otherwise it is a Addon
            {
                // Checking the loaded Addon variable in the Settings.txt file
                Loaded_Addon = Load_Setting(Setting, "Loaded_Addon");

                // We need to make sure the selected App/Addon loads
                if (Loaded_Addon != Selected_App) { Button_Load_Addon_Click(null, null); }
               
            }
            
            // This button acts as shortcut to use the Launch Mod button in the Launch tab.
            Button_Launch_Mod_Click(null, null);
        }







        private void Button_Load_Addon_Click(object sender, EventArgs e)
        {
            // This wont work in Mods Mode.
            if (Selected_Object == "Mods") { return; }
          
            bool Just_Installed = false;                 

            // Checking the variable in the Settings.txt file
            Loaded_Addon = Load_Setting(Setting, "Loaded_Addon");

            

          
            if (Loaded_Addon == Selected_App)
            {               
                Imperial_Console(600, 100, Add_Line + "   This Addon seems to already be loaded.");
                // Exiting function
                return;
            }
            // Otherwise we unload the other loaded addon
            else { Button_Unload_Addon_Click(null, null); }

            Addon_Name = Selected_App;


            // Button_Unload_Addon_Click() changes this variable to somethig else, we set it back to the needs of this function
     
            // Save_Setting(Setting, "Loaded_Addon", Selected_App);
            // Loaded_Addon = Addon_Name;
            // Label_Addon_Name.Text = Addon_Name;

            
            // Gathering Mod and Game Information of the selected Addon    
            string Info_File = Program_Directory + @"Addons\" + Addon_Name + @"\Info.txt";           
            // Setting Hostmod according to the requirement of the selected Addon
            Host_Mod = Load_Setting(Info_File, "Mod");
            string Info_Game = Load_Setting(Info_File, "Game");

            // Making sure we have the right Hostmod selected
            if (Mod_Name != Host_Mod)
            {                            
                Save_Setting(Setting, "Mod_Name", Host_Mod);  
                Label_Mod_Name.Text = Host_Mod;
                Label_Mod_Name_2.Text = Host_Mod;
            }

            Save_Setting(Setting, "Host_Mod", Mod_Name);

       
            // Reinnitializing Game Directories according to the Hostmod   
            if (Info_Game == "Empire_at_War")
            {
                Save_Setting("0", "Game_Path", Game_Path_EAW);  
                Label_Game_Name.Text = "Empire at War";
                Button_Select_EAW.Select();
            }
            else if (Info_Game == "Forces_of_Corruption")
            {
                Save_Setting("0", "Game_Path", Game_Path_FOC);  
                Label_Game_Name.Text = "Forces of Corruption";
                Button_Select_FOC.Select();
            }


            // Reinnitializing Directories to the changed Modname
            Data_Directory = Game_Path + Mods_Directory + Host_Mod + @"\Data\";
            Xml_Directory = Game_Path + Mods_Directory + Host_Mod + @"\Data\Xml\";
            Art_Directory = Game_Path + Mods_Directory + Host_Mod + @"\Data\Art\";

            Set_Maximal_Value_Directories();   


            //======= Checking Directories =======//        
            if (!Directory.Exists(Game_Path + Mods_Directory)) { Directory.CreateDirectory(Game_Path + Mods_Directory); }

            // If no Backup of the original Xml directory exists we are going to create one
            if (Directory.Exists(Data_Directory) & !Directory.Exists(Data_Directory + "Xml_Backup")) { Copy_Now(Data_Directory + "Xml", Data_Directory + "Xml_Backup"); }
      


             


            // INSTALLATION: If no addon with the same name is installed in the .xml directory of the selected mod, we move our Addon Directory there
            if (!Directory.Exists(Data_Directory + Addon_Name) & !Directory.Exists(Xml_Directory + Addon_Name))
            {
                using (var Folder_Browser_Dialog_1 = new FolderBrowserDialog())
                {
                    if (Folder_Browser_Dialog_1.ShowDialog() == DialogResult.OK)
                    {
                        Addon_Path = Path.GetDirectoryName(Folder_Browser_Dialog_1.SelectedPath) + @"\";
                        Addon_Name = Path.GetFileNameWithoutExtension(Folder_Browser_Dialog_1.SelectedPath);
                    }
                    else { return; } // Otherwise the User found no Addon or abborted.
                }

                if (!Directory.Exists(Addon_Path + Addon_Name + @"\Data"))
                {   Imperial_Console(600, 100, @"Could not find """ + Addon_Name + @"\Data"". Please make sure " 
                                                                    + Add_Line + "to select any directory one level over the Data folder.");
                    return;
                }

                // We need a new inactive Xml directory because the current one is being replaced by the installed Addon
                if (Directory.Exists(Data_Directory + "Xml_Backup"))
                {
                    // Another copy of the Original mod Xml goes into inactive, because the Addon we install now is active
                    Copy_Now(Data_Directory + "Xml_Backup", Data_Directory + @"Xml_Inactive\Xml");
                }

                Moving(Addon_Path + Addon_Name, Xml_Directory);
                // Saving a copy of all Art assets into the Hostmod Art Directory and overwriting all files
                Copy_Now(Xml_Directory + Addon_Name + @"\Data\Art", Art_Directory);
                // Overwriting Mod Xml direcory with the files from the Addon.
                Copy_Now(Xml_Directory + Addon_Name + @"\Data\Xml", Xml_Directory);
                Just_Installed = true;

                Save_Setting(Setting, "Loaded_Addon", Selected_App);
                Loaded_Addon = Addon_Name;
                Label_Addon_Name.Text = Addon_Name;
            }

           

            // LOADING: If our Addon exists in the Data Directory it is going to be renamed to Xml
            if (Directory.Exists(Data_Directory + Addon_Name))
            {
                Save_Setting(Setting, "Loaded_Addon", Selected_App);
                Loaded_Addon = Addon_Name;
                Label_Addon_Name.Text = Addon_Name;

                Moving(Data_Directory + "Xml", Data_Directory + "Xml_Inactive");
                Renaming(Data_Directory + Addon_Name, "Xml");
            }


            if (!Directory.Exists(Data_Directory + "Xml_Inactive"))
            {
                // Another copy of the Original mod Xml goes into inactive, because the Addon we install now is active
                Copy_Now(Data_Directory + "Xml_Backup", Data_Directory + @"Xml_Inactive\Xml");
            }



            //======= Replacing Text Files =======//        
            // This directory is needed to put the original one into it while the Addon text is loaded
            if (!Directory.Exists(Data_Directory + @"Text\Original_Text") & Mod_Name != "") { Directory.CreateDirectory(Data_Directory + @"Text\Original_Text"); }

            // Only if the Addon has something to replace!
            if (File.Exists(Xml_Directory + Addon_Name + @"\Data\Text\" + "MasterTextFile_" + Game_Language + ".dat"))
            {   Moving(Data_Directory + @"Text\MasterTextFile_" + Game_Language + ".dat", Data_Directory + @"Text\Original_Text");

                // Moving Addon Text file into the Hostmod
                Moving(Xml_Directory + Addon_Name + @"\Data\Text\" + "MasterTextFile_" + Game_Language + ".dat", Data_Directory + "Text");
            }




            //======= Replacing Commandbar Images =======//
            if (!Directory.Exists(Art_Directory + "Textures")) { Directory.CreateDirectory(Art_Directory + "Textures"); }

            // Need this Directory to temporary drop Original Commandbar .mtd and .tga into it.
            if (!Directory.Exists(Art_Directory + @"Textures\Original_Commandbar") & Mod_Name != "") { Directory.CreateDirectory(Art_Directory + @"Textures\Original_Commandbar"); }

            // Only if the Addon has any Commandbar to replace.
            if (File.Exists(Xml_Directory + Addon_Name + @"\Data\Art\Textures\Mt_Commandbar.mtd"))
            {
                Moving(Art_Directory + @"Textures\Mt_Commandbar.mtd", Art_Directory + @"Textures\Original_Commandbar");
                Moving(Art_Directory + @"Textures\Mt_Commandbar.tga", Art_Directory + @"Textures\Original_Commandbar");

                // Moving Addon Addon Command Bar .mtd and .tga files into the Hostmod
                Moving(Xml_Directory + Addon_Name + @"\Data\Art\Textures\Mt_Commandbar.mtd", Art_Directory + "Textures");
                Moving(Xml_Directory + Addon_Name + @"\Data\Art\Textures\Mt_Commandbar.tga", Art_Directory + "Textures");
            }

            // This Addon installs a weird background image, we make sure to unload that as well
            if (Addon_Name == "Stargate_Dark_Jump" & File.Exists(Xml_Directory + Addon_Name + @"\Data\Art\Textures\MENUBACK_OVERLAY_GERMAN_FRENCH.dds"))
            { Moving(Xml_Directory + Addon_Name + @"\Data\Art\Textures\MENUBACK_OVERLAY_GERMAN_FRENCH.dds", Art_Directory + "Textures"); }



            //======= Replacing Menu Resources File =======//
            // This code only activates if the Addon has a Guidialogs.rc file.
            if (File.Exists(Xml_Directory + Addon_Name + @"\Data\Resources\guidialog\Guidialogs.rc"))
            {
                if (!Directory.Exists(Data_Directory + @"Resources\Deactivated_Menu")) { Directory.CreateDirectory(Data_Directory + @"Resources\Deactivated_Menu\"); }

                // Removing the Menu.rc file of the Mod to get space for the one of the Addon
                Moving(Data_Directory + @"Resources\guidialog\Guidialogs.rc", Data_Directory + @"Resources\Deactivated_Menu");
                Moving(Xml_Directory + Addon_Name + @"\Data\Resources\guidialog\Guidialogs.rc", Data_Directory + @"Resources\guidialog");
            }


            if (Just_Installed == true)
            {              
                Imperial_Console(600, 100, "   Cheers... " + Addon_Name + " installed sucessfully!");
                Just_Installed = false;
            } 
            else
            { Imperial_Console(600, 100, "    " + Addon_Name + " seems to have loaded!"); }
        }





        private void Button_Unload_Addon_Click(object sender, EventArgs e)
        {
            // This wont work in Mods Mode.
            if (Selected_Object == "Mods") { return; }


            // Checking the Addon variables in the Settings.txt file
            Loaded_Addon = Load_Setting(Setting, "Loaded_Addon");
            Addon_Name = Loaded_Addon;

            bool Is_Unloaded = false;
            bool Switched_Game = false;

            // Temporally switching selected Mod in order to unload the right Addon    
            // string Selected_Mod = Host_Mod;


            // If the Addon Directory is not inside of the Xml Folder that means our Addon is not loaded
            // if (Directory.Exists(Xml_Directory + Addon_Name))

            if (Loaded_Addon == "false" | Loaded_Addon == "") { Is_Unloaded = true; }
              


            // Gathering Mod and Game Information of the selected Addon    
            string Info_File = Program_Directory + @"Addons\" + Addon_Name + @"\Info.txt";
            // Setting Hostmod according to the requirement of the selected Addon
            Host_Mod = Load_Setting(Info_File, "Mod");
            string Info_Game = Load_Setting(Info_File, "Game");

                
            // Reinnitializing Game Directories to the Hostmod   
            if (Info_Game == "Empire_at_War") { Game_Path = Game_Path_EAW; if (Game_Path != Game_Path_EAW) {Switched_Game = true;} }
            else if (Info_Game == "Forces_of_Corruption") { Game_Path = Game_Path_FOC; if (Game_Path != Game_Path_FOC) { Switched_Game = true; } }
              

            // Reinnitializing Directories to the changed Modname
            Data_Directory = Game_Path + Mods_Directory + Host_Mod + @"\Data\";
            Xml_Directory = Game_Path + Mods_Directory + Host_Mod + @"\Data\Xml\";
            Art_Directory = Game_Path + Mods_Directory + Host_Mod + @"\Data\Art\";

            Set_Maximal_Value_Directories();   



            //======= Checking Directories =======//   
            if (!Directory.Exists(Data_Directory + "Xml_Inactive"))
            {   // Another copy of the Original mod Xml goes into inactive, because the Addon we install now is active
                Copy_Now(Data_Directory + "Xml_Backup", Data_Directory + "Xml_Inactive");
            }

            // Cant use the "Xml_Directory" shortcut below because there is a \ sign at the end that blocks the action 
            Renaming(Game_Path + Mods_Directory + Host_Mod + @"\Data\Xml", Addon_Name);
            Moving(Data_Directory + @"Xml_Inactive\Xml", Data_Directory);
             
        


            //======= Replacing Text Files =======//    
            if (!Directory.Exists(Data_Directory + @"Text\Original_Text") & Mod_Name != "") { Directory.CreateDirectory(Data_Directory + @"Text\Original_Text"); }

            // Only if anything was put into the Original_Text directory before
            if (File.Exists(Data_Directory + @"Text\Original_Text\" + "MasterTextFile_" + Game_Language + ".dat"))
            {   // Moving Addon Text file back into the Addon dir.
                Moving(Data_Directory + @"Text\" + "MasterTextFile_" + Game_Language + ".dat", Data_Directory + Addon_Name + @"\" + Addon_Name + @"\Data\Text");
                // Inserting original Mod text back into the mod.
                Moving(Data_Directory + @"Text\Original_Text\" + "MasterTextFile_" + Game_Language + ".dat", Data_Directory + @"Text");
            }


            //======= Replacing Commandbar Images =======//
            if (!Directory.Exists(Art_Directory + "Textures")) { Directory.CreateDirectory(Art_Directory + "Textures"); }

            // Need this Directory to temporary drop Original Commandbar .mtd and .tga into it.
            if (!Directory.Exists(Art_Directory + @"Textures\Original_Commandbar") & Mod_Name != "") { Directory.CreateDirectory(Art_Directory + @"Textures\Original_Commandbar"); }


            // Only if thre is anything inside of the Original_Commandbar directory to switch back to the beginning positions.
            if (File.Exists(Art_Directory + @"Textures\Original_Commandbar\Mt_Commandbar.mtd"))
            {
                // Moving Addon Addon Command Bar .mtd and .tga files back into the Addon
                Moving(Art_Directory + @"Textures\Mt_Commandbar.mtd", Data_Directory + Addon_Name + @"\" + Addon_Name + @"\Data\Art\Textures");
                Moving(Art_Directory + @"Textures\Mt_Commandbar.tga", Data_Directory + Addon_Name + @"\" + Addon_Name + @"\Data\Art\Textures");

                Moving(Art_Directory + @"Textures\Original_Commandbar\Mt_Commandbar.mtd", Art_Directory + "Textures");
                Moving(Art_Directory + @"Textures\Original_Commandbar\Mt_Commandbar.tga", Art_Directory + "Textures");
            }

            // This Addon installs a weird background image, we make sure to unload that as well
            if (Addon_Name == "Stargate_Dark_Jump" & File.Exists(Art_Directory + @"Textures\MENUBACK_OVERLAY_GERMAN_FRENCH.dds"))
            { Moving(Art_Directory + @"Textures\MENUBACK_OVERLAY_GERMAN_FRENCH.dds", Data_Directory + Addon_Name + @"\" + Addon_Name + @"\Data\Art\Textures"); }



            //======= Replacing Menu Resources File =======//
            // This code only activates if the Addon deactivated the default Guidialogs.rc file from the Mod.
            if (File.Exists(Data_Directory + @"Resources\Deactivated_Menu\Guidialogs.rc"))
            {
                Moving(Data_Directory + @"Resources\guidialog\Guidialogs.rc", Data_Directory + Addon_Name + @"\" + Addon_Name + @"\Data\Resources\guidialog");
                Moving(Data_Directory + @"Resources\Deactivated_Menu\Guidialogs.rc", Data_Directory + @"Resources\guidialog");
            }



            // Rolling back all Reinnitializations!  
            if (Switched_Game & Game_Path == Game_Path_EAW) { Game_Path = Game_Path_FOC; Switched_Game = false; }
            else if (Switched_Game & Game_Path == Game_Path_FOC) { Game_Path = Game_Path_EAW; Switched_Game = false; }
             
            // Reinnitializing Directories
            Data_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\";
            Xml_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\Xml\";
            Art_Directory = Game_Path + Mods_Directory + Mod_Name + @"\Data\Art\";

            Set_Maximal_Value_Directories();   



            if (Is_Unloaded == false & Silent_Mode == false)
            {
                string Unload_Message = "   " + Addon_Name + " seems to have sucessfully UNloaded!";
                Imperial_Console(600, 100, Unload_Message);
            }

         

            // storing last entry
            if (Loaded_Addon != "" & Loaded_Addon != "false") 
            { Save_Setting(Setting, "Last_Addon", Loaded_Addon); }

            // Making sure Loaded_Addon is corectly noted in Settings.txt
            Save_Setting(Setting, "Loaded_Addon", "");
        }


  


        //================================================ Page 4: Manage ================================================


        private void Mini_Button_Save_Click(object sender, EventArgs e)
        { Save_Click(null, null); }

        private void Mini_Button_Save_MouseHover(object sender, EventArgs e)
        {   Temporal_A = "Save";
            PictureBox The_Button = Mini_Button_Save;
            if (File.Exists(Selected_Theme + @"Buttons\Button_" + Temporal_A + "_Highlighted.png")) { The_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_" + Temporal_A + "_Highlighted.png", The_Button.Size.Width, The_Button.Size.Height); }
            else { The_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_" + Temporal_A + "_Highlighted.png", The_Button.Size.Width, The_Button.Size.Height); }
        }

        private void Mini_Button_Save_MouseLeave(object sender, EventArgs e)
        {   Temporal_A = "Save";
            PictureBox The_Button = Mini_Button_Save;
            if (File.Exists(Selected_Theme + @"Buttons\Button_" + Temporal_A + ".png")) { The_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_" + Temporal_A + ".png", The_Button.Size.Width, The_Button.Size.Height); }
            else { The_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_" + Temporal_A + ".png", The_Button.Size.Width, The_Button.Size.Height); }
        }



        private void Mini_Button_Save_As_Click(object sender, EventArgs e)
        { Save_As_Click(null, null); }

        private void Mini_Button_Save_As_MouseHover(object sender, EventArgs e)
        {   Temporal_A = "Save_As";
            PictureBox The_Button = Mini_Button_Save_As;
            if (File.Exists(Selected_Theme + @"Buttons\Button_" + Temporal_A + "_Highlighted.png")) { The_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_" + Temporal_A + "_Highlighted.png", The_Button.Size.Width, The_Button.Size.Height); }
            else { The_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_" + Temporal_A + "_Highlighted.png", The_Button.Size.Width, The_Button.Size.Height); }
        }

        private void Mini_Button_Save_As_MouseLeave(object sender, EventArgs e)
        {   Temporal_A = "Save_As";
            PictureBox The_Button = Mini_Button_Save_As;
            if (File.Exists(Selected_Theme + @"Buttons\Button_" + Temporal_A + ".png")) { The_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_" + Temporal_A + ".png", The_Button.Size.Width, The_Button.Size.Height); }
            else { The_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_" + Temporal_A + ".png", The_Button.Size.Width, The_Button.Size.Height); }
        }

     
        
        private void Combo_Box_Faction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Combo_Box_Faction.Text == "Load Factions")
            {   // Getting rid of the Load entry
                // Combo_Box_Faction.Items.Remove("Load Factions");

                Application.UseWaitCursor = true;
                Application.DoEvents();             

                // Removing all listed items from the Combo Box in order to refresh
                Combo_Box_Faction.Items.Clear();
                All_Playable_Factions.Clear();

                Playable_Factions = "";

                // Adding the variable Human_Player, if chosen the Lua script auto assigns it to current Player!
                All_Factions = "Human_Player, ";              
                Combo_Box_Faction.Items.Add("Human_Player");

                try
                {
                    foreach (var Xml in Get_Xmls())
                    {   // To prevent the program from crashing because of xml errors:          
                        try
                        {   // Loading the Xml File PATHS:
                            XElement Xml_File = XElement.Load(Xml);


                            // Defining a XElement List
                            IEnumerable<XElement> List_01 =

                            // Selecting all Tags with this string Value:
                            from All_Tags in Xml_File.Descendants("Faction")
                            // Selecting all non empty tags (null because we need all selected)
                            where (string)All_Tags.Attribute("Name") != null                      
                            select All_Tags;
                            

                            foreach (XElement Tags in List_01)
                            {
                                // Putting these Element of the Tags into a string Variable
                                string Selected_Tag = (string)Tags.FirstAttribute.Value;
                               
                                // And listing it in its Combo Box
                                Combo_Box_Faction.Items.Add(Selected_Tag);

                                // Additionally adding it to this variable, so we can write it into the Cheating Dummy!
                                All_Factions += Selected_Tag + ", ";
                                

                                if ((string)Tags.Descendants("Is_Playable").First().Value == "true" & Selected_Tag != "") 
                                {   
                                    // Todo; Pseudo Code; Change Color from Black to Grey to indicate it is unplayable
                                    All_Playable_Factions.Add(Selected_Tag);
                                    Playable_Factions += Selected_Tag + ", ";
                                }
                                
                            }
                        } catch { }
                    }

                    Application.UseWaitCursor = false;
                    Application.DoEvents();

                    try 
                    {   //======================= Loading .Xml File =======================

                        string Cheating_Dummy = Xml_Directory + @"Core\Cheating_Dummy.xml";

                        // Reading File      
                        string The_Text = File.ReadAllText(Cheating_Dummy);
              
                        // We replace anything in all Affiliation tags with the current Playable_Factions
                        The_Text = Regex.Replace(The_Text, @"<Affiliation>.*?</Affiliation>", "<Affiliation>" + Playable_Factions + "</Affiliation>");

                        // Saving Changes
                        File.WriteAllText(Cheating_Dummy, The_Text);

                        
                        // Old Style:
                        /*
                        XDocument Spawner_File = XDocument.Load(Cheating_Dummy, LoadOptions.PreserveWhitespace);

                        try
                        {   // Refreshing Affiliation Tag
                            if (Playable_Factions != "" & Playable_Factions != Spawner_File.Descendants("Affiliation").First().Value)                          
                            { Spawner_File.Descendants("Affiliation").First().Value = Playable_Factions; }
                        }
                        catch { }


                        //========== Saving Changes we just applied to the .xml ==========
                        Spawner_File.Save(Cheating_Dummy);
                        */

                    } catch { }


                }
                catch
                {   // If the Mod has no costum Factions: Factions.xml/ Expansion_Factions.xml or Imperialware can't find these xmls, then we use the default values:
                    if (Combo_Box_Faction.Items.Count < 2)
                    {   Combo_Box_Faction.Items.Add("Rebel");
                        Combo_Box_Faction.Items.Add("Empire");
                        Combo_Box_Faction.Items.Add("Underworld");
                    }
                }

                // Opening the Combobox, so the user sees it found something
                if (User_Input) { Combo_Box_Faction.DroppedDown = true; }

                // Allowing the User to reload
                Combo_Box_Faction.Items.Add("Load Factions");
            }         
            // Otherwise if the User selected a Fraction, we are going to set that value
            else if (Combo_Box_Faction.Text != null) 
            { 
                Save_Setting(Setting, "Selected_Faction", Combo_Box_Faction.Text);
                Save_Setting(Setting, "All_Factions", All_Factions);
                Save_Setting(Setting, "Playable_Factions", Playable_Factions);                
            }          
        }


        private void Button_Refresh_Galaxy_Click(object sender, EventArgs e)
        {   
            // Turning Selection button to unpressable state
            Picture_Box_Select_Planet.Visible = true;
            Button_Select_Planet.Visible = false;
            Label_Galaxy_File.Text = "Galactic Conquest";

            // Removing all listed items from the List Box in order to refresh
            List_Box_Galaxy.Items.Clear();
            
            // Clearing Variable from last usge
            Save_Setting(Setting, "Selected_Galaxy", "");


            foreach (var Xml in Get_Xmls())
            {
                try // To prevent the program from crashing because of xml errors:  
                {
                    // Loading the Xml File PATHS:
                    XElement Xml_File = XElement.Load(Xml);


                    // Defining a XElement List
                    IEnumerable<XElement> List_03 =

                    // Selecting all Tags with the string Value Campaign:
                    from All_Tags in Xml_File.Descendants("Campaign")
                    // Selecting all non empty tags (null because we need all selected)
                    where (string)All_Tags.Attribute("Name") != null
                    select All_Tags;

                    foreach (XElement Tags in List_03)
                    {
                        // Putting these Element of the Tags into a string Variable  
                        string Selected_Tag = (string)Tags.FirstAttribute.Value;


                        // And listing it in its List Box
                        List_Box_Galaxy.Items.Add(Selected_Tag);

                        // Setting last Galaxy
                        Selected_Galaxy += Selected_Tag.ToString() + ",";
                    }

                    Save_Setting(Setting, "Selected_Galaxy", Selected_Galaxy);


                    if (Planet_Switch != "Galaxy")
                    {
                        Save_Setting(Setting, "Planet_Switch", "Galaxy");

                        // Setting one segment of this UI button to active, the other to inactive state.                      
                        Render_Segment_Button(Button_Refresh_Galaxy, "Left", true, 82, 34, 17, 8, 10, Color_03, "Galaxy");
                        Render_Segment_Button(Button_Refresh_Planets, "Right", false, 82, 34, 17, 8, 10, Color_04, "Planets");                  
                    }
                }
                catch { }
            }
        }

        //===============================//


        private void Button_Refresh_Planets_Click(object sender, EventArgs e)
        {            
            // Getting the currently selected Galaxy
            var Galaxy = List_Box_Galaxy.GetItemText(List_Box_Galaxy.SelectedItem).ToString();
            string Galaxy_Xml = "";

            // Clearing Variable from last usage.
            Save_Setting(Setting, "Found_Planets", "");


            // Making sure the button doesent show in Galactic Mode
            if (List_Box_Galaxy.Items.Count > 0)
            {
                if (Galaxy != "")
                {

                    foreach (var Xml in Get_Xmls())
                    {   // To prevent the program from crashing because of xml errors:          
                        try
                        {
                            // Loading the Xml File PATHS:
                            XElement Xml_File = XElement.Load(Xml);


                            // Defining a XElement List
                            IEnumerable<XElement> List_04 =

                            // Selecting all Tags with the current string Value:
                            from All_Tags in Xml_File.Descendants("Campaign")
                            // Selecting the tag with our desired Unit Attribute
                            where (string)All_Tags.Attribute("Name") == Galaxy
                            select All_Tags;


                            // Here we loop for each Tag in List_02 (which only contains the Attribute name of "Unit"
                            foreach (XElement Tags in List_04)
                            {
                                // If this tag was found (and you need to be aware we are still in the Get_Xmls() loop and check this for each .xml) 
                                // Then we found the file that contains our unit and we return its Pathname
                                if (Tags != null)
                                {   // And we select it for editing
                                    Galaxy_Xml = Xml.ToString();
                                    Label_Galaxy_File.Text = Path.GetFileName(Galaxy_Xml);
                                }
                            }
                        }
                        catch { }
                    }

                    // If the Variable did not change by now that means, no files were matched and the User probably has files from a other mod in the cache variable 
                    if (Galaxy_Xml == "") { Imperial_Console(640, 100, Add_Line + "    These seem to be Galaxies of a other mod, please press \"Galaxy\" to refresh."); }
                    else
                    {
                        // Turning Selection button to pressable state
                        Picture_Box_Select_Planet.Visible = false;
                        Button_Select_Planet.Visible = true;
                    }
                }
                else { Imperial_Console(400, 100, Add_Line + "    Please select a Galactic Conquest."); }
            }
            else { Imperial_Console(400, 100, Add_Line + "    Please press Galaxy first."); }


            // ================================= Part II =================================
            // Now that we know which file contains our xml instance (The Attribute is its root tag), we load it
            try
            {

                XElement Galaxy_Xml_File = XElement.Load(Galaxy_Xml);


                // Creating another Enumerable List
                IEnumerable<XElement> List_05 =

                   // Searching this file for the Root Tag
                   from Entry in Galaxy_Xml_File.Descendants("Campaign")
                   // Which needs to have the same attribute as the selected text in the Text Box
                   where (string)Entry.Attribute("Name") == Galaxy

                   // Selecting that tag
                   select Entry.Element("Locations");



                // Removing all listed items from the List Box in order to replace the Galaxies for Planets, then the User can choose one
                List_Box_Galaxy.Items.Clear();
              

                // This loop displays all results
                foreach (XElement Entry in List_05)
                {
                    string text = Entry.Value;
                    string[] bits = text.Split(',');

                    foreach (string bit in bits)
                    {
                        // Using Regex to filter the Galaxy Model prop and Emptyspaces out
                        // Value to Replace - Signs to remove - Replace them with
                        string Remove_Galaxy_Map = Regex.Replace(bit, ".*_Core_Art_Model$", "");
                        string Remove_Emptyspace = Regex.Replace(Remove_Galaxy_Map, "[\n\r\t ]", "");

                        // To make sure we won't add any empty string
                        if (Remove_Emptyspace != "") 
                        {   List_Box_Galaxy.Items.Add(Remove_Emptyspace);
                            Found_Planets += Remove_Emptyspace + ",";
                        }

                    }
                }

                Save_Setting(Setting, "Found_Planets", Found_Planets);             

                if (Planet_Switch == "Galaxy")
                {   Save_Setting(Setting, "Planet_Switch", Label_Galaxy_File.Text);

                Render_Segment_Button(Button_Refresh_Galaxy, "Left", false, 82, 34, 17, 8, 10, Color_04, "Galaxy");
                Render_Segment_Button(Button_Refresh_Planets, "Right", true, 82, 34, 17, 8, 10, Color_03, "Planets");
                }

            } catch { }
        }
  

        private void Radio_Button_Planet_1_CheckedChanged(object sender, EventArgs e)
        {
            // Switching Name Text Color, in order to highlight the selected Checkbox
            Radio_Button_Planet_1.ForeColor = Color_03;
            Radio_Button_Planet_2.ForeColor = Color_02;
            Radio_Button_Planet_3.ForeColor = Color_02;
            Radio_Button_Planet_4.ForeColor = Color_02;

            // Setting the Unit to spawn to Space and adjusting the Type Filter Box
            Save_Setting(Setting, "Selected_Planet", "Target_Planet");

            // Reactivating Variables 
            Label_Planet_1_Name.Text = Target_Planet;
            Label_Planet_2_Name.Text = Start_Planet; 
        }

        private void Radio_Button_Planet_2_CheckedChanged(object sender, EventArgs e)
        {   
            // Switching Name Text Color, in order to highlight the selected Checkbox
            Radio_Button_Planet_2.ForeColor = Color_03;
            Radio_Button_Planet_1.ForeColor = Color_02;
            Radio_Button_Planet_3.ForeColor = Color_02;
            Radio_Button_Planet_4.ForeColor = Color_02;
            
            // Setting the Unit to spawn to Space and adjusting the Type Filter Box
            Save_Setting(Setting, "Selected_Planet", "Start_Planet");

            // Displaying slected Planets to the User
            Label_Planet_1_Name.Text = Target_Planet;
            Label_Planet_2_Name.Text = Start_Planet;  
        }

        private void Radio_Button_Planet_3_CheckedChanged(object sender, EventArgs e)
        {
            // Switching Name Text Color, in order to highlight the selected Checkbox            
            Radio_Button_Planet_1.ForeColor = Color_02;
            Radio_Button_Planet_2.ForeColor = Color_02;
            Radio_Button_Planet_3.ForeColor = Color_03;
            Radio_Button_Planet_4.ForeColor = Color_02;
           

            // Setting the Unit to spawn to Space and adjusting the Type Filter Box;
            Save_Setting(Setting, "Selected_Planet", "Build_Planet");
           
            
            // Displaying to the User that slected Planet is inactive 
            Label_Planet_1_Name.Text = "Location of Build";
            Label_Planet_2_Name.Text = "Inactive";
        }

        private void Radio_Button_Planet_4_CheckedChanged(object sender, EventArgs e)
        {
            // Switching Name Text Color, in order to highlight the selected Checkbox            
            Radio_Button_Planet_1.ForeColor = Color_02;
            Radio_Button_Planet_2.ForeColor = Color_02;
            Radio_Button_Planet_3.ForeColor = Color_02;
            Radio_Button_Planet_4.ForeColor = Color_03;


            // Setting the Unit to spawn to Space and adjusting the Type Filter Box;
            Save_Setting(Setting, "Selected_Planet", "Planet_List");

            // Displaying to the User that slected Planet is inactive 
            Label_Planet_1_Name.Text = "List of selected Planets";
            Label_Planet_2_Name.Text = "Inactive";
        }


        private void Button_Select_Click(object sender, EventArgs e)
        {  // Getting current selection
            string Current_Selection = List_Box_Galaxy.GetItemText(List_Box_Galaxy.SelectedItem).ToString();

            if (Selected_Planet == "Target_Planet")
            {   // Setting Planet
                Save_Setting(Setting, "Target_Planet", Current_Selection);

                // Displaying slected planet to the User
                Label_Planet_1_Name.Text = Current_Selection;
            }
            else if (Selected_Planet == "Start_Planet")
            {                
                Save_Setting(Setting, "Start_Planet", Current_Selection);
                Label_Planet_2_Name.Text = Current_Selection;
            }     
        }

        private void Button_Destroy_Planet_Click(object sender, EventArgs e)
        {
            // Depending on whether Galaxies or Planets are selected we set our target
            if (Button_Select_Planet.Visible == false) 
            {   Destroy_Target_Planet = "Destroy_Galaxy = true";
                Imperial_Console(640, 100, Add_Line + "    Because the Planet Box is in Galactic Conquest mode all Planets are selected" 
                                         + Add_Line + "    for Destruction. If you wish to destroy only the Target Planet please highlight"
                                         + Add_Line + "    a GC, click on Planet. Then please highlight any Planet, click Select and hit Destroy again.");
            }
            else { Destroy_Target_Planet = "Destroy_Target_Planet = true";}
            
            Check_Cheat_Dummy();
            Apply_Cheats();
        }



        private void Button_Teleport_Planets_Click(object sender, EventArgs e)
        {
            // Choosing Teleport Method
            Teleport_Units = "Teleport_Units = false";
            Teleport_from_Planet = "Teleport_from_Planet = true";
            Teleport_Units_on_Planet = "Teleport_Both = false";

            if (Teleportation_Mode == "Units" | Teleportation_Mode == "Planets_and_Units")
            {
                Save_Setting(Setting, "Teleportation_Mode", "Planets");

                // Setting one segment of this UI button to active, the other two to inactive state.
                Render_Segment_Button(Button_Teleport_Planets, "Left", true, 82, 34, 12, 6, 13, Color_03, "Planets");
                Render_Segment_Button(Button_Teleport_Both, "Middle", false, 82, 34, 20, 6, 13, Color_04, "Both");
                Render_Segment_Button(Button_Teleport_Units, "Right", false, 82, 34, 15, 6, 13, Color_04, "Units");    
            }
        }

        private void Button_Teleport_Both_Click(object sender, EventArgs e)
        {
            // Choosing Teleport Method
            Teleport_Units = "Teleport_Units = false";
            Teleport_from_Planet = "Teleport_from_Planet = false";
            Teleport_Units_on_Planet = "Teleport_Both = true";
        
            if (Teleportation_Mode == "Planets" | Teleportation_Mode == "Units")
            {
                Save_Setting(Setting, "Teleportation_Mode", "Planets_and_Units");

                Render_Segment_Button(Button_Teleport_Planets, "Left", false, 82, 34, 12, 6, 13, Color_04, "Planets");
                Render_Segment_Button(Button_Teleport_Both, "Middle", true, 82, 34, 20, 6, 13, Color_03, "Both");
                Render_Segment_Button(Button_Teleport_Units, "Right", false, 82, 34, 15, 6, 13, Color_04, "Units");    
            }
        }

        private void Button_Teleport_Units_Click(object sender, EventArgs e)
        {
            // Choosing Teleport Method
            Teleport_Units = "Teleport_Units = true";
            Teleport_from_Planet = "Teleport_from_Planet = false";
            Teleport_Units_on_Planet = "Teleport_Both = false";

            if (Teleportation_Mode == "Planets" | Teleportation_Mode == "Planets_and_Units")
            {
                Save_Setting(Setting, "Teleportation_Mode", "Units");

                Render_Segment_Button(Button_Teleport_Planets, "Left", false, 82, 34, 12, 6, 13, Color_04, "Planets");
                Render_Segment_Button(Button_Teleport_Both, "Middle", false, 82, 34, 20, 6, 13, Color_04, "Both");
                Render_Segment_Button(Button_Teleport_Units, "Right", true, 82, 34, 15, 6, 13, Color_03, "Units");    
            }
        }



        private void Button_Teleport_Click(object sender, EventArgs e)
        {
            if (Teleport_Units == "Teleport_Units = false"  && Teleport_from_Planet == "Teleport_from_Planet = false" && Teleport_Units_on_Planet == "Teleport_Both = false")
            {    // If none of the Methods were selected
                 MessageBox.Show("Please select Teleportation Method: Planets, Units or Both (Specified Units on Startplanet)");
            }
            else 
            {                   
                Check_Cheat_Dummy();   
                Apply_Cheats();
            }
        }

      

        //=========================================================================

        private void Label_Clear_Searchbar_Click(object sender, EventArgs e)
        {
            Text_Box_Search_Bar.Text = "";
            Text_Box_Search_Bar.Focus();
        }


        private void Button_Search_Unit_Click(object sender, EventArgs e)
        {   
            // Making sure list is not empty 
            if (Search_Index.Count == 0) { Search_Index.Add("First_Slot"); }

            string The_Text = "";
            bool Recently_Listed = false;
          
 
            if (Search_Toggle == 0)
            {   
                Text_Box_Search_Bar.Visible = true;
                Label_Clear_Searchbar.Visible = true;
                Text_Box_Search_Bar.Focus();
                Combo_Box_Filter_Type.Visible = false;
                Search_Toggle = 1;          
            }
            else if (Search_Toggle == 1)
            {
                Text_Box_Search_Bar.Visible = false;
                Label_Clear_Searchbar.Visible = false;
                Combo_Box_Filter_Type.Visible = true;
                Search_Toggle = 0;


                try
                {   // Because the user typed his search text into the textbox by now, we can search the list for his Text
                    foreach (var Entry in List_Box_All_Instances.Items)
                    {
                        string File_Name = Entry.ToString();
                        Recently_Listed = false;


                        if (Regex.IsMatch(File_Name, "(?i).*?" + Text_Box_Search_Bar.Text + ".*?"))
                        {
                            // We check all entries of the Search Intex list for the Entry                   
                            for (int i = Search_Index.Count - 1; i >= 0; --i)
                            {
                                var Item = Search_Index[i];

                                if (Item == File_Name)
                                {
                                    Recently_Listed = true;
                                    break;
                                }
                            }

                            if (Recently_Listed == false) // Otherwise it already was listed last time and we ignore it
                            {
                                Search_Index.Add(File_Name);

                                // If not found Selecting that item and adding it to Index_List
                                List_Box_All_Instances.SelectedIndex = List_Box_All_Instances.FindString(File_Name);

                                return; // As we found and selected the first one
                            }
                        }
                    }
                } catch { }

                // If the function didn't returned by now that means nothing was found and we can reset the Index list
                Search_Index.Clear();
                Text_Box_Search_Bar.Focus();          
            }          
            
        }



        private void Combo_Box_Filter_Type_SelectedIndexChanged(object sender, EventArgs e)
        {   if (User_Input) { Refresh_Units(); }
            Save_Setting(Setting, "Last_Type_Search", Combo_Box_Filter_Type.Text);
        }


        // As soon the user presses return
        private void Text_Box_Search_Bar_TextChanged(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = Button_Search_Unit; // Button_Search_Unit will be 'clicked' when user presses return          
        }


        private void Mini_Button_Copy_Instance_Click(object sender, EventArgs e)
        {
            // Getting the currently selected Unit
            Temporal_A = List_Box_All_Instances.GetItemText(List_Box_All_Instances.SelectedItem).ToString();
            Clipboard.SetText(Temporal_A);
        }

        private void Mini_Button_Copy_Instance_MouseHover(object sender, EventArgs e)
        {   Temporal_A = "Copy";
            PictureBox The_Button = Mini_Button_Copy_Instance;
            if (File.Exists(Selected_Theme + @"Buttons\Button_" + Temporal_A + "_Highlighted.png")) { The_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_" + Temporal_A + "_Highlighted.png", The_Button.Size.Width, The_Button.Size.Height); }
            else { The_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_" + Temporal_A + "_Highlighted.png", The_Button.Size.Width, The_Button.Size.Height); }
        }

        private void Mini_Button_Copy_Instance_MouseLeave(object sender, EventArgs e)
        {   Temporal_A = "Copy";
            PictureBox The_Button = Mini_Button_Copy_Instance;
            if (File.Exists(Selected_Theme + @"Buttons\Button_" + Temporal_A + ".png")) { The_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_" + Temporal_A + ".png", The_Button.Size.Width, The_Button.Size.Height); }
            else { The_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_" + Temporal_A + ".png", The_Button.Size.Width, The_Button.Size.Height); }
        }



        private void Button_Add_Selected_Click(object sender, EventArgs e)
        {                                 
            // Getting the currently selected Mod or Game 
            string Current_Selection = List_Box_All_Instances.GetItemText(List_Box_All_Instances.SelectedItem).ToString();

            if (Current_Selection != "")
            { 
                if (Combo_Box_Filter_Type.Text == "GroundInfantry" | Combo_Box_Filter_Type.Text == "GroundVehicle")
                {
                    Imperial_Console(700, 200, Add_Line + @"I apologize, " + Combo_Box_Filter_Type.Text + " Units can not be spawned. But you can "
                                             + Add_Line + @"click ""Edit"" then find and spawn the GroundCompany"
                                             + Add_Line + "or HeroCompany of this unit instead. It will be listed"
                                             + Add_Line + @"in the ""Unit Properties\Team"" name textbox below."); return;

                }
            }
            else { Imperial_Console(600, 100, Add_Line + "No Unit selected."); return; }


            if (Combo_Box_Amount.Text == "x") // Then we add only 1
            { List_Box_All_Spawns.Items.Add(Current_Selection); }

            // If anything was chosen we add the currently selected unit to the list below in the UI
            else if (Combo_Box_Amount.Text != "")
            {   // Getting the text string into int
                Int32.TryParse(Combo_Box_Amount.Text, out Temporal_C);

                if (Temporal_C > 20 & Combo_Box_Filter_Type.Text == "Squadron") 
                { Imperial_Console(700, 100, Add_Line + "    Spawn limit for Squadrons is 20, otherwise they slow the game down!"); return; } 

                else if (Temporal_C > 50) { Imperial_Console(600, 100, Add_Line + "    Are you serious? The amount value is too high."); }
                else
                {                    
                    for (int i = 0; i < Temporal_C; ++i)
                    { List_Box_All_Spawns.Items.Add(Current_Selection); }      
                }
                       
            }
        }


        private void Button_Remove_Selection_Click(object sender, EventArgs e)
        {
            string Current_Selection = List_Box_All_Spawns.GetItemText(List_Box_All_Spawns.SelectedItem).ToString();
          
            if (Current_Selection == "") { Imperial_Console(600, 100, Add_Line + "No Unit selected."); return; }
            List_Box_All_Spawns.Items.Remove(Current_Selection);
        }



        private void Button_Spawn_Click(object sender, EventArgs e)
        {
            Check_Cheat_Dummy();

            // Activating Spawn           
            Activate_Spawn = "Spawn = true";

            // Calling fucntion to apply the cheats
            Apply_Cheats();

            // Removing all listed items from the listbox
            List_Box_All_Spawns.Items.Clear();
        }


     

        private void Button_Edit_Click(object sender, EventArgs e)
        {
            Application.UseWaitCursor = true;
            Application.DoEvents();

            User_Input = false; // Preventing trigger of "are you sure you want to exit" massage box

            // Getting the currently selected Unit
            var Unit = List_Box_All_Instances.GetItemText(List_Box_All_Instances.SelectedItem).ToString();

            // Exiting function if nothing is selected
            if (Unit == "") { Imperial_Console(400, 100, Add_Line + "    No Unit selected"); return; }
            if (Combo_Box_Filter_Type.Text == "") { Imperial_Console(500, 100, Add_Line + "    Please select a Unit type in the Unit Dropdown Box."); return; }




            Team_Type = Combo_Box_Filter_Type.Text;

            if (Team_Type != "Squadron" & Team_Type != "GroundCompany" & Team_Type != "HeroCompany")
            {   // Setting Variable for the Selected_Instance, and saving it for editing
                Save_Setting(Setting, "Selected_Instance", Unit);
            }     

            else //============= For Teams/Squadrons we Load them and then grab one Member for loading ===============
            {
                Save_Setting(Setting, "Selected_Team", Unit);
            
                // Assigning the type of Team or Squadron 
                if (Team_Type == "Squadron") { Team_Units = "Squadron_Units"; }
                else if (Team_Type == "HeroCompany") { Team_Units = "Company_Units"; }
                else if (Team_Type == "GroundCompany") { Team_Units = "Company_Units"; }


                Temporal_A = Xml_Utility(null, Unit, Team_Units);
             
                // Temporal_E is a string [] array 
                if (Temporal_A != null) 
                {  
                    Temporal_B = Regex.Replace(Temporal_A, " ", "");
                    Temporal_A = Regex.Replace(Temporal_B, @"\t", "");
                    Temporal_E = Temporal_A.Split(','); 
                }
                
                else
                {   // If nothing was found there is no Squad Unit we could load
                    User_Input = true; Imperial_Console(600, 100, "    Sorry, this Team/Squadron seems to have no Team Units.");                   
                    if (!Is_Team) 
                    {   if (Size_Expander_I == "false") { Button_Expand_I_Click(null, null); }
                        Switch_Button_Is_Team_Click(null, null);
                    } //Enabling
                    Clear_Team_Values();
                    // Loading the Squadron
                    Load_Team_To_UI(null, Unit);                   
                    return; 
                }

                if (Temporal_E.Count() > 1)
                {   // If has more then 1 Team member, that means we try to select not the Captain (0) but the first generic team mate
                    Unit = Temporal_E[1];  
                }
                else if (Temporal_E.Count() > 0) { Unit = Temporal_E[0]; }
            
                // Setting Variable for the Selected_Instance, and saving it for editing
                Save_Setting(Setting, "Selected_Instance", Unit);
               
            }        
            //================= Getting the Xml of the Selected Unit ===================
                        
            foreach (var Xml in Get_Xmls())
            {   try 
                {   // ===================== Opening Xml File =====================
                    XDocument Xml_File = XDocument.Load(Xml, LoadOptions.PreserveWhitespace);
                   
                    var Instances =
                        from All_Tags in Xml_File.Root.Descendants()
                        // Selecting all non empty tags that have the Attribute "Name", null because we need all selected.
                        where (string)All_Tags.Attribute("Name") == Selected_Instance
                        select All_Tags;


                    // =================== Checking Xml Instance ===================
                    foreach (XElement Instance in Instances)
                    {
                        // Getting Name of the Root Tag
                        Unit_Type = (string)Instance.Name.ToString();

                        Selected_Xml = Xml;
                        Save_Setting("2", "Selected_Xml", @"""" + Selected_Xml + @"""");                     
                    }
                } catch {}
            }

         
            Load_Unit_To_UI(Selected_Xml, Selected_Instance);

            User_Input = true;  
        }


        private void Button_Edit_Xml_Click(object sender, EventArgs e)
        {
            // Getting the currently selected Unit
            var Unit = List_Box_All_Instances.GetItemText(List_Box_All_Instances.SelectedItem).ToString();


            //========================= Getting the Xml of the Selected Unit =========================
            foreach (var Xml in Get_Xmls())
            {   // To prevent the program from crashing because of xml errors:          
                try
                {
                    // Loading the Xml File PATHS:
                    XElement Xml_File = XElement.Load(Xml);


                    // Defining a XElement List
                    IEnumerable<XElement> List_04 =

                    // Selecting all Tags with the current string Value:
                    from All_Tags in Xml_File.Descendants(Unit_Type)
                    // Selecting the tag with our desired Unit Attribute
                    where (string)All_Tags.Attribute("Name") == Unit
                    select All_Tags;


                    // Here we loop for each Tag in List_02 (which only contains the Attribute name of "Unit"
                    foreach (XElement Tags in List_04)
                    {
                        // If this tag was found (and you need to be aware we are still in the Get_Xmls() loop and check this for each .xml) 
                        // Then we found the file that contains our unit and we return its Pathname
                        if (Tags != null)
                        {
                            Save_Setting(Setting, "Selected_Xml", Xml.ToString());
                            Label_Xml_Name.Text = Path.GetFileName(Selected_Xml);
                        }
                    }

                    // Setting the Xml tab        
                    Tab_Control_01.SelectedIndex = 4;


                    // After Selection of any Item in List_Box_Xmls, its event handler will select and load that .xml
                    if (List_Box_Xmls.Items.Contains(Path.GetFileName(Selected_Xml)))
                    { List_Box_Xmls.SelectedItem = Path.GetFileName(Selected_Xml); }

                } catch { }
            }                        
        }



        private void Track_Bar_Credits_Scroll(object sender, EventArgs e)
        {
                            
            // Setting Credits Value, according to the Value Maximum_Credits
            Text_Box_Credits.Text = (Track_Bar_Credits.Value * 100).ToString();
            // Setting amount of Hull in the Trackbar according to the Maximum Value
            Track_Bar_Credits.Maximum = Maximum_Credits / 100;

            // Adjusting the Maximum Value of the Progress Bar and multiplicating x 100 to get the percentage
            Progress_Bar_Credits.Maximum = Maximum_Credits;
            Progress_Bar_Credits.Value = (Track_Bar_Credits.Value * 100);

            // Showing/Hiding the Money sign
            if (Track_Bar_Credits.Value != 0) Label_Credit_Sign.Text = "$";
            else
            {   Player_Credits = 0;
                Label_Credit_Sign.Text = "";
            }
        }

        private void Text_Box_Credits_TextChanged(object sender, EventArgs e)
        {  
            int Typed_Value;
            Int32.TryParse(Text_Box_Credits.Text, out Typed_Value);

            Check_Numeral_Text_Box(Text_Box_Credits, Maximum_Credits, false);

            try
            {
                // Setting amount of Credits in the Trackbar according to the Value Maximum_Credits
                Track_Bar_Credits.Maximum = Maximum_Credits / 100;
                // Then we need to set the Track Bar according to the Text box / 100      
                Track_Bar_Credits.Value = Typed_Value / 100;


                // Adjusting the Maximum Value of the Progress Bar and multiplicating x 100 to get the percentage
                Progress_Bar_Credits.Maximum = Maximum_Credits;
                Progress_Bar_Credits.Value = Typed_Value;
                // Showing/Hiding the Money sign
                if (Track_Bar_Credits.Value != 0) Label_Credit_Sign.Text = "$";
                else Label_Credit_Sign.Text = "";
            }
            catch { }
        }

        private void Button_Give_Credits_Click(object sender, EventArgs e)
        {   // Paying the Player
            Player_Credits = Track_Bar_Credits.Value * 100;

            Label_Credit_Sign.Text = "";
            Label_KaChing.Visible = true;

            Check_Cheat_Dummy();
            Apply_Cheats();                 
        }


        private void Button_Expand_C_Click(object sender, EventArgs e)
        {
            // If our expander button property is true = open, we toggle it to close
            if (Size_Expander_C == "true")
            {   // Negative value means move all buttons together
                int Expansion_Value = -1530;
                if (Size_Expander_E == "false") Expansion_Value = -1000; // Need to concider the whole size of the parent box

                ToggleControlY(Group_Box_Cheating, null, Move_List_C, Expansion_Value);

                // And change the name of the Expander Button to + for the next usage
                Button_Expand_C.Text = "+";

                Save_Setting(Setting, "Size_Expander_C", "false");
            }
            else if (Size_Expander_C == "false")
            {// Otherwise our expander button is false = closed, we toggle it to reopen
                // Positive value means, pull them back to the open position
                int Expansion_Value = 1530;
                if (Size_Expander_E == "false") Expansion_Value = 1000;

                ToggleControlY(Group_Box_Cheating, null, Move_List_C, Expansion_Value);

                // And change the name of the Expander Button to - for the next usage
                Button_Expand_C.Text = "-";
                Save_Setting(Setting, "Size_Expander_C", "true");
            }
        }

        private void Button_Expand_E_Click(object sender, EventArgs e)
        {
            // If our expander button property is true = open, we toggle it to close
            if (Size_Expander_E == "true")
            {   // Negative value means move all buttons together
                int Expansion_Value = -530;

                ToggleControlY(Group_Box_Planets, null, Move_List_E, Expansion_Value);

                // Changing Size of Parent Group Box as well
                Group_Box_Cheating.Height += Expansion_Value;

                // And change the name of the Expander Button to + for the next usage
                Button_Expand_E.Text = "+";
                Save_Setting(Setting, "Size_Expander_E", "false");
            }
            else if (Size_Expander_E == "false")
            {// Otherwise our expander button is false = closed, we toggle it to reopen
                // Positive value means, pull them back to the open position
                int Expansion_Value = 530;
                ToggleControlY(Group_Box_Planets, null, Move_List_E, Expansion_Value);

                // Changing Size of Parent Group Box as well
                Group_Box_Cheating.Height += Expansion_Value;

                // And change the name of the Expander Button to - for the next usage
                Button_Expand_E.Text = "-";
                Save_Setting(Setting, "Size_Expander_E", "true");
            }
        }


        private void Button_Expand_A_Click(object sender, EventArgs e)
        {
            // If our expander button property is true = open, we toggle it to close
            if (Size_Expander_A == "true")
            {   // Negative value means move all buttons together
                int Expansion_Value = -1336;

                ToggleControlY(Group_Box_Unit_Settings, null, Move_List_A, Expansion_Value);

                // And change the name of the Expander Button to + for the next usage
                Button_Expand_A.Text = "+";
                Save_Setting(Setting, "Size_Expander_A", "false");
            }
            else if (Size_Expander_A == "false")
            {// Otherwise our expander button is false = closed, we toggle it to reopen
                // Positive value means, pull them back to the open position
                int Expansion_Value = 1336;
                ToggleControlY(Group_Box_Unit_Settings, null, Move_List_A, Expansion_Value);

                // And change the name of the Expander Button to - for the next usage
                Button_Expand_A.Text = "-";
                Save_Setting(Setting, "Size_Expander_A", "true");
            }

        }


        private void Button_Expand_B_Click(object sender, EventArgs e)
        {
            // If our expander button property is true = open, we toggle it to close
            if (Size_Expander_B == "true")
            {   // This Button does the same as the one above, have a look at its comments
                int Expansion_Value = -980;
                ToggleControlY(Group_Box_Power_Values, null, Move_List_B, Expansion_Value);

                Button_Expand_B.Text = "+";
                Save_Setting(Setting, "Size_Expander_B", "false");
            }
            else if (Size_Expander_B == "false")
            {
                int Expansion_Value = 980;
                ToggleControlY(Group_Box_Power_Values, null, Move_List_B, Expansion_Value);

                Button_Expand_B.Text = "-";
                Save_Setting(Setting, "Size_Expander_B", "true");
            }
        }

        private void Button_Expand_J_Click(object sender, EventArgs e)
        {
            int Expansion_Value = 2556;

            // Need to concider the size of its Child box, so the value shrinks by the size of its child then
            if (Size_Expander_K == "false") { Expansion_Value = 1716; }

            if (Size_Expander_J == "true")
            {
                ToggleControlY(Group_Box_Abilities, null, Move_List_J, -Expansion_Value);

                Button_Expand_J.Text = "+";
                Save_Setting(Setting, "Size_Expander_J", "false");
            }
            else if (Size_Expander_J == "false")
            {
                ToggleControlY(Group_Box_Abilities, null, Move_List_J, Expansion_Value);

                Button_Expand_J.Text = "-";
                Save_Setting(Setting, "Size_Expander_J", "true");
            }

        }
 
        private void Button_Expand_K_Click(object sender, EventArgs e)
        {
            int Expansion_Value = 840;

            if (Size_Expander_K == "true")
            {
                ToggleControlY(Group_Box_Additional_Abilities, null, Move_List_K, -Expansion_Value);

                // Changing Size of Parent Group Box as well
                Group_Box_Abilities.Height += -Expansion_Value;

                Combo_Box_Additional_Abilities.Visible = false;
                Check_Box_Use_In_Team.Visible = false;
                Button_Add_Ability.Visible = false;           

                Button_Expand_K.Text = "+";
                Save_Setting(Setting, "Size_Expander_K", "false");
            }
            else if (Size_Expander_K == "false")
            {
                ToggleControlY(Group_Box_Additional_Abilities, null, Move_List_K, Expansion_Value);
                Group_Box_Abilities.Height += Expansion_Value;
                Combo_Box_Additional_Abilities.Visible = true;
                Check_Box_Use_In_Team.Visible = true;
                Button_Add_Ability.Visible = true;

                Button_Expand_K.Text = "-";
                Save_Setting(Setting, "Size_Expander_K", "true");
            }
        }

        private void Button_Expand_I_Click(object sender, EventArgs e)
        {
            int Expansion_Value = 2255;

            // Need to concider the Child Expander Box
            if (!Is_Team) { Expansion_Value = 1419; }

            if (Size_Expander_I == "true")
            {   ToggleControlY(Group_Box_Properties, null, Move_List_I, -Expansion_Value);

                Button_Expand_I.Text = "+";
                Save_Setting(Setting, "Size_Expander_I", "false");
            }
            else if (Size_Expander_I == "false")
            {   ToggleControlY(Group_Box_Properties, null, Move_List_I, Expansion_Value);

                Button_Expand_I.Text = "-";
                Save_Setting(Setting, "Size_Expander_I", "true");
            }
        }


        private void Button_Expand_F_Click(object sender, EventArgs e)
        {
            // If our expander button property is true = open, we toggle it to close
            if (Size_Expander_F == "true")
            {   // This Button does the same as the one above, have a look at its comments               
                int Expansion_Value = -710;
                ToggleControlY(Group_Box_Costum_Tags, null, Move_List_F, Expansion_Value);

                Button_Expand_F.Text = "+";
                Save_Setting(Setting, "Size_Expander_F", "false");
            }
            else if (Size_Expander_F == "false")
            {
                int Expansion_Value = 710;
                ToggleControlY(Group_Box_Costum_Tags, null, Move_List_F, Expansion_Value);

                Button_Expand_F.Text = "-";
                Save_Setting(Setting, "Size_Expander_F", "true");
            }
        }


        private void Button_Expand_G_Click(object sender, EventArgs e)
        {
            int Expansion_Value = 2482;
            // Need to concider the size of its Child box, so the value shrinks by the size of its child then
            if (Size_Expander_H == "false") Expansion_Value = 1626; 

            if (Size_Expander_G == "true")
            {               
                ToggleControlY(Group_Box_Build_Requirements, null, Move_List_G, -Expansion_Value);

                Button_Expand_G.Text = "+";
                Save_Setting(Setting, "Size_Expander_G", "false");
            }
            else if (Size_Expander_G == "false")
            {
                ToggleControlY(Group_Box_Build_Requirements, null, Move_List_G, Expansion_Value);

                Button_Expand_G.Text = "-";
                Save_Setting(Setting, "Size_Expander_G", "true");
            }
        }

        private void Button_Expand_H_Click(object sender, EventArgs e)
        {
            if (Size_Expander_H == "true")
            {
                int Expansion_Value = -856;

                ToggleControlY(Group_Box_Requirements_Small, null, Move_List_H, Expansion_Value);

                // Changing Size of Parent Group Box as well
                Group_Box_Build_Requirements.Height += Expansion_Value;

                Button_Expand_H.Text = "+";
                Save_Setting(Setting, "Size_Expander_H", "false");
            }
            else if (Size_Expander_H == "false")
            {
                int Expansion_Value = 856;
                ToggleControlY(Group_Box_Requirements_Small, null, Move_List_H, Expansion_Value);

                // Changing Size of Parent Group Box as well
                Group_Box_Build_Requirements.Height += Expansion_Value;

                Button_Expand_H.Text = "-";
                Save_Setting(Setting, "Size_Expander_H", "true");
            }
        }



        private void Button_Collaps_A_Click(object sender, EventArgs e)
        { Button_Expand_A_Click(null, null);
          Group_Box_Power_Values.Focus(); }

        private void Button_Collaps_B_Click(object sender, EventArgs e)
        { Button_Expand_B_Click(null, null);
          Group_Box_Abilities.Focus();}

        private void Button_Collaps_J_Click(object sender, EventArgs e)
        { Button_Expand_J_Click(null, null);
          Group_Box_Properties.Focus(); }

        private void Button_Collaps_C_Click(object sender, EventArgs e)
        { Button_Expand_C_Click(null, null);
          Label_Xml_Name.Focus();}

        private void Button_Collaps_I_Click(object sender, EventArgs e)
        { Button_Expand_I_Click(null, null);
          Group_Box_Build_Requirements.Focus(); }
          
        private void Button_Collaps_D_Click(object sender, EventArgs e)
        { Button_Expand_D_Click(null, null);
          Label_Folder_Path.Focus(); }

        private void Button_Collaps_F_Click(object sender, EventArgs e)
        { Button_Expand_F_Click(null, null);
          Group_Box_Cheating.Focus(); }

        private void Button_Collaps_G_Click(object sender, EventArgs e)
        { Button_Expand_G_Click(null, null);
          Group_Box_Costum_Tags.Focus(); }

        


        // Browsing for .alo Model
        private void Button_Search_Model_Click(object sender, EventArgs e)
        {           
            // Setting Innitial Filename and Data for the Open Menu
            Open_File_Dialog_1.FileName = "";
            Open_File_Dialog_1.InitialDirectory = Data_Directory + @"Art\Models";
            Open_File_Dialog_1.Filter = "alo files (*.alo)|*.alo|All files (*.*)|*.*";
            Open_File_Dialog_1.FilterIndex = 1;
            Open_File_Dialog_1.RestoreDirectory = true;
            Open_File_Dialog_1.CheckFileExists = true;
            Open_File_Dialog_1.CheckPathExists = true;
           

            try
            {   // If the Open Dialog found a File
                if (Open_File_Dialog_1.ShowDialog() == DialogResult.OK)
                {   // We are going to set that file to our Textbox
                    Text_Box_Model_Name.Text = Path.GetFileNameWithoutExtension(Open_File_Dialog_1.FileName);}                 
            }
            catch (Exception The_Exception) 
            { MessageBox.Show(The_Exception.Message, "MainWindow", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void Button_Default_Radar_Icon_Click(object sender, EventArgs e)
        {
            Text_Box_Radar_Icon.Text = "I_Radar_Capital_Ship";
            Text_Box_Radar_Size.Text = "0.06 0.06";
        }

        private void Text_Box_Radar_Size_TextChanged(object sender, EventArgs e)
        {
            if (User_Input & Regex.IsMatch(Text_Box_Radar_Size.Text, "[,]"))
            {
                Imperial_Console(500, 100, Add_Line + "    The game expects . the other comma sign.");
                // Removing the wrong Text
                Text_Box_Radar_Size.Text = "";
            }
            // Making sure the User doesen't type any Letters
            else if (User_Input & Regex.IsMatch(Text_Box_Radar_Size.Text, "[^. 0-9]"))
            {
                Imperial_Console(660, 100, Add_Line + "    Please enter only numbers, the game expects 2 decimal values seperated by empty space.");
                // Removing the wrong Text
                Text_Box_Radar_Size.Text = "";
            }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } 
        }


        // Need these to detect whether the User made any change that needs to be saved before quiting.
        private void Text_Box_Model_Name_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }

        private void Text_Box_Icon_Name_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }

        private void Text_Box_Radar_Icon_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }

        private void Text_Box_Text_Id_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }

        private void Text_Box_Unit_Class_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }

        private void Text_Box_Encyclopedia_Text_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }



        //======== Numeral  Art Values ========

        private void Track_Bar_Scale_Factor_Scroll(object sender, EventArgs e)
        { if (User_Input) { Scroll_Xml_Decimal(Track_Bar_Scale_Factor, Progress_Bar_Scale_Factor, Text_Box_Scale_Factor, Maximum_Model_Scale); } }

        private void Text_Box_Scale_Factor_TextChanged(object sender, EventArgs e)
        { Set_From_Float_Text_Box(Text_Box_Scale_Factor, Track_Bar_Scale_Factor, Progress_Bar_Scale_Factor, Maximum_Model_Scale, 10, false); } 
        

        private void Track_Bar_Model_Height_Scroll(object sender, EventArgs e)
        {   if (User_Input)
            {   
                Scroll_Xml_Value(Track_Bar_Model_Height, Progress_Bar_Model_Height, Text_Box_Model_Height, Maximum_Model_Height, 10);
                if (Auto_Apply == "true")
                {
                    Scroll_Xml_Value(Track_Bar_Model_Height, Progress_Bar_Select_Box_Height, Text_Box_Select_Box_Height, Maximum_Model_Height, 10);
                    Track_Bar_Select_Box_Height.Value = Track_Bar_Model_Height.Value;
                }              
            }
        }


        private void Text_Box_Model_Height_TextChanged(object sender, EventArgs e)
        { Set_From_Decimal_Text_Box(Text_Box_Model_Height, Track_Bar_Model_Height, Progress_Bar_Model_Height, Maximum_Model_Height, 10, true); }


        private void Button_Operator_Model_Height_Click(object sender, EventArgs e)
        {
            // Mirroring this setting to a similar setting if the user likes Auto settings
            if (Auto_Apply == "true" & User_Input)
            {
                if (Toggle_Operator_Model_Height == false & Toggle_Select_Box_Height == false)
                { Toggle_Button(Button_Operator_Select_Box_Height, "Button_Plus", "Button_Minus", -5, true); Toggle_Select_Box_Height = true; }

                else if (Toggle_Operator_Model_Height & Toggle_Select_Box_Height)
                { Toggle_Button(Button_Operator_Select_Box_Height, "Button_Plus", "Button_Minus", -5, false); Toggle_Select_Box_Height = false; }
            }


            if (Toggle_Operator_Model_Height == false)
            {   // Toggeling Operator Sign Switch (+ -)
                Toggle_Button(Button_Operator_Model_Height, "Button_Plus", "Button_Minus", -5, true); Toggle_Operator_Model_Height = true;
            }
            else { Toggle_Button(Button_Operator_Model_Height, "Button_Plus", "Button_Minus", -5, false); Toggle_Operator_Model_Height = false; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Button_Operator_Model_Height_MouseHover_1(object sender, EventArgs e)
        {
            if (Toggle_Operator_Model_Height) { Toggle_Button(Button_Operator_Model_Height, "Button_Plus", "Button_Plus_Highlighted", -5, false); }
            else { Toggle_Button(Button_Operator_Model_Height, "Button_Minus", "Button_Minus_Highlighted", -5, false); }
        }

        private void Button_Operator_Model_Height_MouseLeave_1(object sender, EventArgs e)
        {
            if (Toggle_Operator_Model_Height) { Toggle_Button(Button_Operator_Model_Height, "Button_Plus", "Button_Plus_Highlighted", -5, true); }
            else { Toggle_Button(Button_Operator_Model_Height, "Button_Minus", "Button_Minus_Highlighted", -5, true); }
        }




        private void Track_Bar_Select_Box_Scale_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Select_Box_Scale, Progress_Bar_Select_Box_Scale, Text_Box_Select_Box_Scale, Maximum_Select_Box_Scale, 10); }

        private void Text_Box_Select_Box_Scale_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Select_Box_Scale, Maximum_Model_Height, false);
            Text_Box_Text_Changed(Track_Bar_Select_Box_Scale, Progress_Bar_Select_Box_Scale, Maximum_Model_Height, Typed_Value, 10);
        }


        private void Track_Bar_Select_Box_Height_Scroll(object sender, EventArgs e)
        { if (User_Input) { Scroll_Xml_Value(Track_Bar_Select_Box_Height, Progress_Bar_Select_Box_Height, Text_Box_Select_Box_Height, Maximum_Model_Height, 10); } }

        private void Text_Box_Select_Box_Height_TextChanged(object sender, EventArgs e)
        { Set_From_Decimal_Text_Box(Text_Box_Select_Box_Height, Track_Bar_Select_Box_Height, Progress_Bar_Select_Box_Height, Maximum_Model_Height, 10, true); }
                     


        private void Button_Operator_Select_Box_Height_Click(object sender, EventArgs e)
        {
            if (Toggle_Select_Box_Height == false)
            { Toggle_Button(Button_Operator_Select_Box_Height, "Button_Plus", "Button_Minus", -5, true); Toggle_Select_Box_Height = true; }
            else { Toggle_Button(Button_Operator_Select_Box_Height, "Button_Plus", "Button_Minus", -5, false); Toggle_Select_Box_Height = false; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }

        private void Button_Operator_Select_Box_Height_MouseHover(object sender, EventArgs e)
        {
            if (Toggle_Select_Box_Height) { Toggle_Button(Button_Operator_Select_Box_Height, "Button_Plus", "Button_Plus_Highlighted", -5, false); }
            else { Toggle_Button(Button_Operator_Select_Box_Height, "Button_Minus", "Button_Minus_Highlighted", -5, false); }
        }

        private void Button_Operator_Select_Box_Height_MouseLeave(object sender, EventArgs e)
        {
            if (Toggle_Select_Box_Height) { Toggle_Button(Button_Operator_Select_Box_Height, "Button_Plus", "Button_Plus_Highlighted", -5, true); }
            else { Toggle_Button(Button_Operator_Select_Box_Height, "Button_Minus", "Button_Minus_Highlighted", -5, true); }
        }





        private void Check_Box_Health_Bar_Size_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_Box_Health_Bar_Size.Checked)
            {
                Combo_Box_Health_Bar_Size.Visible = true;
                Picture_Box_Health_Bar_Size.Visible = false;

                Track_Bar_Health_Bar_Size.Visible = true;
                Check_Box_Health_Bar_Size.ForeColor = Color_01;

                Combo_Box_Health_Bar_Size_SelectedIndexChanged(null, null);
            }
            else
            {
                Combo_Box_Health_Bar_Size.Visible = false;
                Picture_Box_Health_Bar_Size.Visible = true;

                Track_Bar_Health_Bar_Size.Visible = false;
                Check_Box_Health_Bar_Size.ForeColor = Color_02;

                Progress_Bar_Health_Bar_Size.Value = 0;
            }
        }

        private void Track_Bar_Health_Bar_Size_Scroll(object sender, EventArgs e)
        {
            if (Track_Bar_Health_Bar_Size.Value == 0)
            {
                Combo_Box_Health_Bar_Size.Text = "Small";
                Health_Bar_Size = "0";
                Progress_Bar_Health_Bar_Size.Value = 33;
            }
            else if (Track_Bar_Health_Bar_Size.Value == 1)
            {
                Combo_Box_Health_Bar_Size.Text = "Medium";
                Health_Bar_Size = "1";
                Progress_Bar_Health_Bar_Size.Value = 66;
            }
            else if (Track_Bar_Health_Bar_Size.Value == 2)
            {
                Combo_Box_Health_Bar_Size.Text = "Large";
                Health_Bar_Size = "2";
                Progress_Bar_Health_Bar_Size.Value = 100;
            }
        }

        private void Combo_Box_Health_Bar_Size_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Combo_Box_Health_Bar_Size.Text == "Small") { Track_Bar_Health_Bar_Size.Value = 0; Progress_Bar_Health_Bar_Size.Value = 33; }
            else if (Combo_Box_Health_Bar_Size.Text == "Medium") { Track_Bar_Health_Bar_Size.Value = 1; Progress_Bar_Health_Bar_Size.Value = 66; }
            else if (Combo_Box_Health_Bar_Size.Text == "Large") { Track_Bar_Health_Bar_Size.Value = 2; Progress_Bar_Health_Bar_Size.Value = 100; }
        }



        private void Track_Bar_Health_Bar_Height_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Decimal(Track_Bar_Health_Bar_Height, Progress_Bar_Health_Bar_Height, Text_Box_Health_Bar_Height, Maximum_Health_Bar_Height); }

        private void Text_Box_Health_Bar_Height_TextChanged(object sender, EventArgs e)
        { Set_From_Float_Text_Box(Text_Box_Health_Bar_Height, Track_Bar_Health_Bar_Height, Progress_Bar_Health_Bar_Height, Maximum_Health_Bar_Height, 10, true); }




        // ======== Costum Tags ========

        private void Text_Box_Costum_Tag_1_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }

        private void Text_Box_Costum_Tag_2_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }

        private void Text_Box_Costum_Tag_3_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }


        private void Label_Unit_Name_Click(object sender, EventArgs e)
        { Text_Box_Name.Text = Clipboard.GetText(); }

        // If Name or Type changes it will tell the Save Button below to Save this Field
        private void Text_Box_Name_TextChanged(object sender, EventArgs e)
        {
            if (Text_Box_Name.Text == "") { Text_Box_Death_Clone.Text = ""; Text_Box_Team_Name.Text = ""; }

            if (Auto_Apply == "true" & Text_Box_Name.Text != "" & Combo_Box_Class.Text != "Fighter" & Combo_Box_Class.Text != "Bomber") 
            { Text_Box_Death_Clone.Text = Text_Box_Name.Text + "_Death_Clone"; }
            
            else if (Combo_Box_Class.Text == "Fighter" | Combo_Box_Class.Text == "Bomber") 
            { 
                Text_Box_Death_Clone.Text = "";

                if (Team_Type == "Squadron") { Temporal_A = "_Squadron"; }
                else { Temporal_A = "_Team"; }

                if (Text_Box_Name.Text != "") { Text_Box_Team_Name.Text = Text_Box_Name.Text + Temporal_A; }                       
            }
         

            // Needed to know whether to pop the "are you sure you want to exit" menu when exiting without save
            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Mini_Button_Copy_Unit_Click(object sender, EventArgs e)
        {   // Getting Unit     
            if (Text_Box_Name.Text != "") { Clipboard.SetText(Text_Box_Name.Text); }
        }

        private void Mini_Button_Copy_Unit_MouseHover(object sender, EventArgs e)
        {   Temporal_A = "Copy";
            PictureBox The_Button = Mini_Button_Copy_Unit;
            if (File.Exists(Selected_Theme + @"Buttons\Button_" + Temporal_A + "_Highlighted.png")) { The_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_" + Temporal_A + "_Highlighted.png", The_Button.Size.Width, The_Button.Size.Height); }
            else { The_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_" + Temporal_A + "_Highlighted.png", The_Button.Size.Width, The_Button.Size.Height); }
        }

        private void Mini_Button_Copy_Unit_MouseLeave(object sender, EventArgs e)
        {   Temporal_A = "Copy";
            PictureBox The_Button = Mini_Button_Copy_Unit;
            if (File.Exists(Selected_Theme + @"Buttons\Button_" + Temporal_A + ".png")) { The_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_" + Temporal_A + ".png", The_Button.Size.Width, The_Button.Size.Height); }
            else { The_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_" + Temporal_A + ".png", The_Button.Size.Width, The_Button.Size.Height); }
        }


        private void Label_Is_Variant_Click(object sender, EventArgs e)
        { Text_Box_Is_Variant.Text = Clipboard.GetText(); }

        // Starting integrated Petroglyph Editors
        private void Label_Model_Alo_Click(object sender, EventArgs e)
        {
            try
            {   string The_File = Art_Directory + @"Models\" + Text_Box_Model_Name.Text + ".alo";
                System.Diagnostics.Process.Start(The_File);
            } 
            catch
            {
                if (Text_Box_Model_Name.Text == "") { Imperial_Console(540, 100, Add_Line + "Please add any modelname into the model textbox."); }
                else { Imperial_Console(540, 100, Add_Line + "Error; Maybe you need to assign .alo files"
                                                + Add_Line +  "to the Alo Viewer tool in"
                                                + Add_Line + @".\Imperialware_Directory\Misc\Modding_Tools"); }               
            }
        }

        private void Label_Icon_Tga_Click(object sender, EventArgs e)
        { System.Diagnostics.Process.Start(Program_Directory + @"Misc\Modding_Tools\MTD_Editor.exe"); }


        private void Label_Radar_Icon_Tga_Click(object sender, EventArgs e)
        { System.Diagnostics.Process.Start(Program_Directory + @"Misc\Modding_Tools\MTD_Editor.exe"); }

        private void Label_Dat_Editor_Click(object sender, EventArgs e)
        { System.Diagnostics.Process.Start(Program_Directory + @"Misc\Modding_Tools\Dat_Editor.exe"); }

        private void Label_String_Editor_Click(object sender, EventArgs e)
        { System.Diagnostics.Process.Start(Program_Directory + @"Misc\Modding_Tools\String_Editor.exe"); }


        // Auto-pasting from Clipboard
        private void Label_Model_Name_Click(object sender, EventArgs e)
        { Text_Box_Model_Name.Text = Clipboard.GetText(); }

        private void Label_Icon_Name_Click(object sender, EventArgs e)
        { Text_Box_Icon_Name.Text = Clipboard.GetText(); }

        private void Label_Radar_Icon_Click(object sender, EventArgs e)
        { Text_Box_Radar_Icon.Text = Clipboard.GetText(); }

        private void Label_Radar_Icon_Size_Click(object sender, EventArgs e)
        { Text_Box_Radar_Size.Text = Clipboard.GetText(); }

        private void Label_Text_Id_Click(object sender, EventArgs e)
        { Text_Box_Text_Id.Text = Clipboard.GetText(); }

        private void Label_Unit_Class_Click(object sender, EventArgs e)
        { Text_Box_Unit_Class.Text = Clipboard.GetText(); }

        private void Label_Encyclopedia_Text_Click(object sender, EventArgs e)
        { Text_Box_Encyclopedia_Text.Text = Clipboard.GetText(); }




        private void Combo_Box_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Setting Value, as this is the most important Variable used to find Xml Units in Linq!
            // Disabled because it interferes with the Saving!
            // if (Combo_Box_Type.Text != "") { Save_Setting(Setting, "Unit_Type", Combo_Box_Type.Text); }

                   

            // Depending on which type of Xml Instance, we choose a mode for the Classes in the Class Scroll Bar below.
            if (Combo_Box_Type.Text == "SpaceUnit" | Combo_Box_Type.Text == "UniqueUnit" | Combo_Box_Type.Text == "Squadron" | Combo_Box_Type.Text == "HeroCompany" | Combo_Box_Type.Text == "TransportUnit")
            {   Unit_Mode = "Space";              
                // Setting types for "<Cathegory_Mask>", which is used by the game very frequently.
                string[] Unit_Types = { "Fighter", "Bomber", "SpaceHero", "", "Corvette", "Frigate", "Capital" };
                Combo_Box_Class.DataSource = Unit_Types;

                Combo_Box_Team_Type.Text = "Squadron";
           
                // We can use a different name for this Tag
                Label_Hull.Text = "Hull";
                Picture_Box_Shuttle_Type.Visible = true;
                Text_Box_Shuttle_Type.Visible = false;

                Text_Box_Team_Offsets.Visible = true;
                if (Text_Box_Team_Name.Text.EndsWith("Team")) { Text_Box_Team_Name.Text = Text_Box_Team_Name.Text.Replace("Team", "Squadron"); }
            }              

            else if (Combo_Box_Type.Text == "GroundInfantry" | Combo_Box_Type.Text == "GroundVehicle" | Combo_Box_Type.Text == "HeroUnit" | 
                     Combo_Box_Type.Text == "GroundCompany")
            {   Unit_Mode = "Ground";              
                string[] Unit_Types = { "Infantry", "Vehicle", "Air", "LandHero", "", "AntiInfantry", "AntiVehicle", "AntiAir", "AntiStructure" };
                Combo_Box_Class.DataSource = Unit_Types;

                Combo_Box_Team_Type.Text = "GroundCompany";
                Label_Hull.Text = "Health";
                Picture_Box_Shuttle_Type.Visible = false;
                Text_Box_Shuttle_Type.Visible = true;

                Text_Box_Team_Offsets.Visible = false;
                if (Text_Box_Team_Name.Text.EndsWith("Squadron")) { Text_Box_Team_Name.Text = Text_Box_Team_Name.Text.Replace("Squadron", "Team"); }
            }

            else if (Combo_Box_Type.Text == "StarBase" | Combo_Box_Type.Text == "SpaceBuildable" | Combo_Box_Type.Text == "SpecialStructure" | Combo_Box_Type.Text == "TechBuilding" |
                     Combo_Box_Type.Text == "GroundBase" | Combo_Box_Type.Text == "GroundStructure" | Combo_Box_Type.Text == "GroundBuildable")
            {   Unit_Mode = "Structure";
                Combo_Box_Class.Text = "Structure";
                string[] Unit_Types = { "Structure", "AntiInfantry", "AntiVehicle", "AntiAir", "AntiStructure" };
                Combo_Box_Class.DataSource = Unit_Types;
                // Selecting the Structure Max Settings, because these are the right ones no matter which type is chosen.
                Button_Class_10_Click(null, null);

                Combo_Box_Team_Type.Text = "GroundCompany";
                Label_Hull.Text = "Integrity";

                Picture_Box_Shuttle_Type.Visible = false;
                Text_Box_Shuttle_Type.Visible = true;

                Text_Box_Team_Offsets.Visible = false;
            }
           
            
            //============ Setting specific Classes ============//
            switch (Combo_Box_Type.Text)
            {
                case "SpaceUnit":
                    Combo_Box_Class.Text = "Frigate";
                    break;
                case "UniqueUnit":
                    Combo_Box_Class.Text = "Capital";
                    break;
                case "HeroCompany":
                    Combo_Box_Class.Text = "Capital";
                    break;
                case "TransportUnit":
                    Combo_Box_Class.Text = "Corvette";
                    break;


                case "GroundInfantry":
                    Combo_Box_Class.Text = "Infantry";                  
                    break;
                case "GroundVehicle":
                    Combo_Box_Class.Text = "Vehicle";
                    break;
                case "HeroUnit":
                    Combo_Box_Class.Text = "LandHero";
                    break;              
                case "GroundCompany":
                    Combo_Box_Class.Text = "LandHero";
                    break;       
                       

                case "StarBase":
                    Combo_Box_Class.Text = "Structure";
                    break;
                case "SpaceBuildable":
                    Combo_Box_Class.Text = "Structure";
                    break;
                case "SpecialStructure":
                    Combo_Box_Class.Text = "Structure";
                    break;

                case "TechBuilding":
                    Combo_Box_Class.Text = "Structure";
                    break;
                case "GroundBase":
                    Combo_Box_Class.Text = "Structure";
                    break;
                case "GroundStructure":
                    Combo_Box_Class.Text = "Structure";
                    break;              
                case "GroundBuildable":
                    Combo_Box_Class.Text = "Structure";
                    break;   
            }
             



            //============ Setting Ability Pool ============//

            // Clearing Listbox and Reloading Inactive Behavoir Pool
            List_Box_Inactive_Behavoir.Items.Clear();

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }

            string [] Unit_Behavoir = Load_Behavoir_Pool();

            foreach (var Entry in Unit_Behavoir)
            { List_Box_Inactive_Behavoir.Items.Add(Entry); }


            //Removing matching entries in the Pool of Inactive Behavoir 
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


            //============ Setting Affiliation Pool ============//

            // Clearing Listbox and Reloading Inactive Affiliation Pool
            List_Box_Active_Affiliations.Items.Clear();
            List_Box_Inactive_Affiliations.Items.Clear();


            // Setting last Factions
            string[] The_Factions = All_Factions.Split(',');

            // Setting Factions of current Mod  
            foreach (string Entry in The_Factions)
            { if (Entry != "") {List_Box_Inactive_Affiliations.Items.Add(Entry);} }


            // Removing matching entries in the Pool 
            foreach (string Entry in The_Factions)
            {           
                // i = Amount of Entries -1 because the List starts at -1; until I reached 0; count -1 at each iteration
                for (int i = List_Box_Active_Affiliations.Items.Count - 1; i >= 0; --i)
                {
                    string Affiliation = List_Box_Active_Affiliations.Items[i].ToString();

                    // Removing the currently selected Tag from list box
                    if (Affiliation == Entry)
                    {   // Removing matched Items                                                                     
                        List_Box_Inactive_Behavoir.Items.Remove(Entry);
                    }
                }
            }  

            
        }


        private void Check_Box_Is_Variant_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_Box_Is_Variant.Checked)
            {   
                Check_Box_Is_Variant.ForeColor = Color_03;
                Text_Box_Is_Variant.Visible = true;
                Picture_Box_Is_Variant.Visible = false;
                Button_Open_Variant.Visible = true;
            }
            else
            {   Check_Box_Is_Variant.ForeColor = Color_02;
                Text_Box_Is_Variant.Visible = false;
                Picture_Box_Is_Variant.Visible = true;
                Button_Open_Variant.Visible = false;
            }

            if (User_Input == true) { if (Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }
        }

        
        private void Button_Open_Variant_Click(object sender, EventArgs e)
        {
            if (Text_Box_Is_Variant.Text == "") { return; }

            if (Combo_Box_Filter_Type.Text != Combo_Box_Type.Text) { Combo_Box_Filter_Type.Text = Combo_Box_Type.Text; }

            if (Edited_Selected_Unit == true)
            {   
                // Call Imperial Dialogue with resolution of 540x160, Button 1 Name , Button 2 Name and Dialogue Text. Then we wait for user input
                Imperial_Dialogue(580, 160, "Save + Open", "Cancel", "Don't Save", Add_Line + "    There are unsaved changes in this Unit."
                                                                    + Add_Line + "    Are you sure you wish to open another Unit yet?");

                // If user aborted the program execution
                if (Caution_Window.Passed_Value_A.Text_Data == "true")
                {
                    // Saving before exit
                    if (Label_Xml_Name.Text == "Creating New File") { Save_As_Click(null, null); }

                    // Otherwise we hopefully have a other Xml open which can be saved
                    else { Save_Click(null, null); }
                  
                }
                else if (Caution_Window.Passed_Value_A.Text_Data == "false")
                {
                    return;
                }             
            }


            try // Trying to auto select the current Variant in the Instance ListView and to forward it for editing
            {   List_Box_All_Instances.SelectedItem = Text_Box_Is_Variant.Text;
                Button_Edit_Click(null, null);
            } catch { }
           
        }

        private void Button_Open_Variant_MouseHover(object sender, EventArgs e)
        {
            if (File.Exists(Selected_Theme + @"Buttons\Button_Arrow_Highlighted.png")) { Button_Open_Variant.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_Arrow_Highlighted.png", Button_Open_Variant.Size.Width, Button_Open_Variant.Size.Height); }
            else { Button_Open_Variant.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_Arrow_Highlighted.png", Button_Open_Variant.Size.Width, Button_Open_Variant.Size.Height); }
        }

        private void Button_Open_Variant_MouseLeave(object sender, EventArgs e)
        {
            if (File.Exists(Selected_Theme + @"Buttons\Button_Arrow.png")) { Button_Open_Variant.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_Arrow.png", Button_Open_Variant.Size.Width, Button_Open_Variant.Size.Height); }
            else { Button_Open_Variant.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_Arrow.png", Button_Open_Variant.Size.Width, Button_Open_Variant.Size.Height); }
        }

        // ==================

        private void Button_Create_New_Xml_Click(object sender, EventArgs e)
        {
            if (Edited_Selected_Unit == true)
            {
                // Call Imperial Dialogue with resolution of 540x160, Button 1 Name , Button 2 Name and Dialogue Text. Then we wait for user input
                Imperial_Dialogue(580, 160, "Save + Erase", "Cancel", "Don't Save", Add_Line + "    There are unsaved changes in this Unit."
                                                                    + Add_Line + "    Are you sure you wish to erase all from the UI?");


                // If user aborted the program execution
                if (Caution_Window.Passed_Value_A.Text_Data == "true")
                {
                    // Saving before exit
                    if (Label_Xml_Name.Text == "Creating New File") { Save_As_Click(null, null); }

                    // Otherwise we hopefully have a other Xml open which can be saved
                    else { Save_Click(null, null); }

                }
                else if (Caution_Window.Passed_Value_A.Text_Data == "false")
                {
                    return;
                }
            }


            Control[] All_UI_Text_Boxes = 
            {   Text_Box_Name, Combo_Box_Type, Text_Box_Is_Variant, Text_Box_Model_Name, Text_Box_Icon_Name, Text_Box_Radar_Icon, 
                Text_Box_Radar_Size, Text_Box_Text_Id, Text_Box_Encyclopedia_Text, Text_Box_Model_Height, 
                Text_Box_Health_Bar_Height, Text_Box_Select_Box_Scale, Text_Box_Select_Box_Height,

                Text_Box_Unit_Class, Combo_Box_Class, Text_Box_Hull,
                Text_Box_Shield, Text_Box_Shield_Rate, Text_Box_Energy, Text_Box_Energy_Rate, Text_Box_Speed, Text_Box_Rate_Of_Turn, 
                Text_Box_Bank_Turn_Angle, Text_Box_AI_Combat, Text_Box_Population, Text_Box_Projectile, 

                Combo_Box_Team_Type, Text_Box_Name, Text_Box_Team_Is_Variant, Text_Box_Shuttle_Type,
                Text_Box_Team_Amount, Text_Box_Team_Offsets, Text_Box_Team_Members, 

                Text_Box_Starting_Unit_Name, Text_Box_Spawned_Unit,
                Text_Box_Reserve_Unit_Name, Text_Box_Reserve_Unit, Text_Box_Death_Clone,
                Text_Box_Death_Clone_Model, Text_Box_Lua_Script,

                Text_Box_Build_Cost, Text_Box_Skirmish_Cost, Text_Box_Build_Time, Text_Box_Tech_Level, Text_Box_Star_Base_LV, 
                Text_Box_Ground_Base_LV, Text_Box_Required_Timeline, Text_Box_Slice_Cost, Text_Box_Current_Limit, Text_Box_Lifetime_Limit, 
                Text_Box_Costum_Tag_1, Text_Box_Costum_Tag_2, Text_Box_Costum_Tag_3, Text_Box_Costum_Tag_4, Text_Box_Costum_Tag_5, Text_Box_Costum_Tag_6
            };

            foreach (Control Text_Box in All_UI_Text_Boxes)
            {   // Setting all Textboxes empty if they arn't already
                if (Text_Box.Text != "") { Text_Box.Text = ""; }
            }

            Text_Box_Scale_Factor.Text = "1.0";

            // Setting UI Elements of Class to 0 as well     
            Track_Bar_Class.Value = 0;
            Progress_Bar_Class.Value = 0;
            Combo_Box_Health_Bar_Size.Text = "Medium";

            Clear_Ability_Values();
            Combo_Box_Additional_Abilities.Text = "";
            Text_Box_Additional_Abilities.Text = "";

            // Need to reset all of them
            Ability_1_Type = "";
            Ability_1_Activated_GUI = "";
            Ability_1_Toggle_Auto_Fire = false;
            Ability_1_Expiration_Time = ""; 
            Ability_1_Recharge_Time = "";
            Ability_1_Name = "";
            Ability_1_Description = ""; 
            Ability_1_Icon = "";
            Ability_1_Activated_SFX = "";
            Ability_1_Deactivated_SFX = "";
            Ability_1_Mod_Multipliers = "";

            Ability_2_Type = "";
            Ability_2_Activated_GUI = "";
            Ability_2_Toggle_Auto_Fire = false;
            Ability_2_Expiration_Time = "";
            Ability_2_Recharge_Time = "";
            Ability_2_Name = "";
            Ability_2_Description = "";
            Ability_2_Icon = "";
            Ability_2_Activated_SFX = "";
            Ability_2_Deactivated_SFX = ""; 
            Ability_2_Mod_Multipliers = "";
         

            // If Operators are in + mode we set it back to -
            if (Toggle_Operator_Model_Height) { Button_Operator_Model_Height_Click(null, null); }
            if (Toggle_Select_Box_Height) { Button_Operator_Select_Box_Height_Click(null, null); }
            if (Toggle_Operator_Population) { Button_Operator_Population_Click(null, null); }


            // Deactivating all Switches
            if (Toggle_Is_Hero) { Switch_Button_Is_Hero_Click(null, null); }
            if (Toggle_Show_Head) { Switch_Button_Show_Head_Click(null, null); }
            if (Toggle_God_Mode) { Switch_Button_God_Mode_Click(null, null); }
            if (Toggle_Use_Particle) { Switch_Button_Use_Particle_Click(null, null); }

            if (Check_Box_Health_Bar_Size.Checked == false) { Check_Box_Health_Bar_Size.Checked = true; }           
            if (Check_Box_Has_Hyperspace.Checked == false) { Check_Box_Has_Hyperspace.Checked = true; }
            Text_Box_Hyperspace_Speed.Text = "1.0";


            if (Loading_Completed & Is_Team) { Switch_Button_Is_Team_Click(null, null); }
            if (Toggle_Enable_All) { Switch_Button_Enable_All_Click(null, null); }
            if (Toggle_Build_Tab) { Switch_Button_Build_Tab_Click(null, null); }
            if (Toggle_Innitially_Locked) { Switch_Button_Innitially_Locked_Click(null, null); }
            if (Loading_Completed & Size_Expander_H == "true") { Button_Expand_H_Click(null, null); } // Closing UI subtab

            //======== Behavoir ========//
            List_Box_Inactive_Behavoir.Items.Clear();
            List_Box_Active_Behavoir.Items.Clear();
             
            //====== Affiliations ======//

            List_Box_Inactive_Affiliations.Items.Clear();
            List_Box_Active_Affiliations.Items.Clear();

            Temporal_E = All_Factions.Split(',');

            foreach (string Entry in Temporal_E)
            {   // First we add all these entries, and later if matched they get removed
                if (Entry != "") { List_Box_Inactive_Affiliations.Items.Add(Entry); }
            }
            //=========================//

            List_View_Teams.Items.Clear();

            // Preventing the User from pressing "Save" and overwriting the last selected unit
            if (User_Input) 
            {   Selected_Xml = "";
                Label_Xml_Name.Text = "Creating New File";
            }
         
            // Making sure the Variant Checkbox is not checked
            if (Check_Box_Is_Variant.Checked & Add_Core_Code == "false") { Check_Box_Is_Variant.Checked = !Check_Box_Is_Variant.Checked; }

            Edited_Selected_Unit = false;
        }


        private void Button_Open_Xml_Click(object sender, EventArgs e)
        {
            // Setting Innitial Filename and Data for the Open Menu
            Open_File_Dialog_2.FileName = "";
            Open_File_Dialog_2.InitialDirectory = Xml_Directory;
            Open_File_Dialog_2.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            Open_File_Dialog_2.FilterIndex = 1;
            Open_File_Dialog_2.RestoreDirectory = true;
            Open_File_Dialog_2.CheckFileExists = true;
            Open_File_Dialog_2.CheckPathExists = true;


            try
            {   // If the Open Dialog found a File
                if (Open_File_Dialog_2.ShowDialog() == DialogResult.OK)
                {   // Setting the Xml for Editing
                    Selected_Xml = Open_File_Dialog_2.FileName;
                }
            }
            catch { Imperial_Console(400, 100, Add_Line + "    Failed to open Xml."); }


            if (Size_Expander_C == "false")
            {   // If closed we make sure the cheating tag expands
                Button_Expand_C_Click(null, null);
            }

            // Removing older entries
            List_Box_All_Instances.Items.Clear();
            // Making sure they are not sorted alphabetically but in preserved order from top of file 
            List_Box_All_Instances.Sorted = false;


            // Selecting a higher UI Element to attract the User attention to it
            Label_Units.Focus();

            

            try // To prevent the program from crashing because of xml errors: 
            {   // Loading the Xml File PATHS:
                XElement Xml_File = XElement.Load(Selected_Xml);


                // Defining a XElement List
                IEnumerable<XElement> List_01 =

                // Selecting all Tags with this string Value:
                from All_Tags in Xml_File.Descendants()
                // Selecting all non empty tags (null because we need all selected)
                where (string)All_Tags.Attribute("Name") != null
                select All_Tags;

                foreach (XElement Tags in List_01)
                {
                    // Putting these Element of the Tags into a string Variable
                    string Selected_Tag = (string)Tags.FirstAttribute.Value;


                    if (!Selected_Tag.Contains("Death_Clone") & !Tags.Name.ToString().Contains("Ability")) // We filter all Death clone instances out
                    {   // And listing it in its Listbox
                        List_Box_All_Instances.Items.Add(Selected_Tag);

                        // Setting Unit Cache
                        Selected_Units += Selected_Tag.ToString() + ",";
                    }   
                       
                    // Trying to select the first List Box Entry
                    try { List_Box_All_Instances.SelectedItem = List_Box_All_Instances.Items[0]; }
                    catch { }                   
                }

                Save_Setting(Setting, "Selected_Units", Selected_Units);
            }
            catch 
            {   Size Window_Size = new Size(516, 200);
                // Using the Costum Massage Box
                Show_Message_Box_One_Button(Window_Size, Selected_Xml, null);

                // Clearing Variable
                Selected_Xml = "";
            }       
        }

        //==================

        // If Class changes it will tell the Save Button below to Save this Field
        private void Combo_Box_Class_SelectedIndexChanged(object sender, EventArgs e)
        {

            bool Is_Variant_of_Template = false;
            // Defining Path to the Template file
            string Template_Path = Program_Directory + @"Xml_Core\";

            string [] Templates = {"Fighter_Template", "Bomber_Template", "Corvette_Template", "Frigate_Template", "Capitalship_Template", 
                                   "Infantry_Template", "Vehicle_Template", "Air_Template", "Hero_Template", "Structure_Template"};

            // If any template was matched in the Variant Textbox, that means it is a variant that might be the class of former selection, we want then to update below
            foreach (string Entry in Templates) { if (Text_Box_Is_Variant.Text == Entry) { Is_Variant_of_Template = true;}; }


            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }


            if (Combo_Box_Class.Text == "Fighter")
            {   Track_Bar_Class.Value = 0;
                Progress_Bar_Class.Value = 20;
                Combo_Box_Health_Bar_Size.Text = "Small";
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                Button_Class_1_Click(null, null);               
                // Loading default Values into Editor UI
                // Load_Unit_To_UI(Template_Path + "Template_Space_Fighter.xml", "Fighter_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Fighter_Template"; return; }
                //if (!Is_Team) { Switch_Button_Is_Team_Click(null, null); } //Enabling
            }
            else if (Combo_Box_Class.Text == "Bomber")
            {   Track_Bar_Class.Value = 1;
                Progress_Bar_Class.Value = 40;
                Combo_Box_Health_Bar_Size.Text = "Small";
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                Button_Class_2_Click(null, null);
                // Load_Unit_To_UI(Template_Path + "Template_Space_Bomber.xml", "Bomber_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Bomber_Template"; return; }
                // if (!Is_Team) { Switch_Button_Is_Team_Click(null, null); } //Enabling
            }
            else if (Combo_Box_Class.Text == "Corvette")
            {   Track_Bar_Class.Value = 2;
                Progress_Bar_Class.Value = 60;
                Combo_Box_Health_Bar_Size.Text = "Medium";
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "100"; Text_Box_Select_Box_Height.Text = "100"; }
                Button_Class_3_Click(null, null);
                // Load_Unit_To_UI(Template_Path + "Template_Space_Corvette.xml", "Corvette_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Corvette_Template"; return; }
                // if (Is_Team) { Switch_Button_Is_Team_Click(null, null); } //Disabling
            }
            else if (Combo_Box_Class.Text == "Frigate")
            {   Track_Bar_Class.Value = 3;
                Progress_Bar_Class.Value = 80;
                Combo_Box_Health_Bar_Size.Text = "Medium";
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "200"; Text_Box_Select_Box_Height.Text = "200"; }
                Button_Class_4_Click(null, null);
                // Load_Unit_To_UI(Template_Path + "Template_Space_Frigate.xml", "Frigate_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Frigate_Template"; return; }
                //if (Is_Team) { Switch_Button_Is_Team_Click(null, null); } //Disabling
            }
            else if (Combo_Box_Class.Text == "Capital")
            {   Track_Bar_Class.Value = 4;
                Progress_Bar_Class.Value = 100;
                Combo_Box_Health_Bar_Size.Text = "Large";                
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "400"; Text_Box_Select_Box_Height.Text = "400"; }
                Button_Class_5_Click(null, null);
                // Load_Unit_To_UI(Template_Path + "Template_Space_Capitalship.xml", "Capitalship_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Capitalship_Template"; return; }
                //if (Is_Team) { Switch_Button_Is_Team_Click(null, null); }
            }
  

            // If the User chooses any Ground Units it will toggle the Combo Box into Ground Unit Mode
            else if (Combo_Box_Class.Text == "Infantry")
            {   Track_Bar_Class.Value = 1;
                Progress_Bar_Class.Value = 40;
                Combo_Box_Health_Bar_Size.Text = "Small";
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                Button_Class_6_Click(null, null);
                // Load_Unit_To_UI(Template_Path + "Template_Land_Infantry.xml", "Infantry_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Infantry_Template"; return; }
                //if (!Is_Team) { Switch_Button_Is_Team_Click(null, null); }
            }
            else if (Combo_Box_Class.Text == "Vehicle")
            {   Track_Bar_Class.Value = 2;
                Progress_Bar_Class.Value = 60;
                Combo_Box_Health_Bar_Size.Text = "Medium";
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                Button_Class_7_Click(null, null);
                // Load_Unit_To_UI(Template_Path + "Template_Land_Vehicle.xml", "Vehicle_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Vehicle_Template"; return; }
                //if (!Is_Team) { Switch_Button_Is_Team_Click(null, null); }
            }
            else if (Combo_Box_Class.Text == "Air")
            {   Track_Bar_Class.Value = 3; 
                Progress_Bar_Class.Value = 80;
                Combo_Box_Health_Bar_Size.Text = "Small";
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                Button_Class_8_Click(null, null);
                // Load_Unit_To_UI(Template_Path + "Template_Land_Air.xml", "Air_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Air_Template"; return; }
                //if (!Is_Team) { Switch_Button_Is_Team_Click(null, null); }
            }
            else if (Combo_Box_Class.Text == "LandHero" | Combo_Box_Class.Text == "SpaceHero")
            {   Track_Bar_Class.Value = 4;
                Progress_Bar_Class.Value = 100;
                Button_Class_9_Click(null, null);
                Combo_Box_Health_Bar_Size.Text = "Medium";
                if (Auto_Apply == "true" & Combo_Box_Class.Text == "LandHero") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                // Load_Unit_To_UI(Template_Path + "Template_Hero.xml", "Hero_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Hero_Template"; return; }
                //if (!Is_Team) { Switch_Button_Is_Team_Click(null, null); }
            }


            // Structure Mode, the "if (Unit_Mode == "Ground")" auto selects Max Values of the Air Class, which is a bit stronger            
            else if (Combo_Box_Class.Text == "AntiStructure")
            {   Track_Bar_Class.Value = 0;
                Progress_Bar_Class.Value = 20;  
                Combo_Box_Health_Bar_Size.Text = "Medium";
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                if (Unit_Mode == "Ground") { Button_Class_8_Click(null, null);
                // Load_Unit_To_UI(Template_Path + "Template_Land_Air.xml", "Air_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Air_Template"; return; }
                }                                          
            }    
            else if (Combo_Box_Class.Text == "Structure")
            {   Track_Bar_Class.Value = 4; 
                Progress_Bar_Class.Value = 100;
                Combo_Box_Health_Bar_Size.Text = "Medium";
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                // Load_Unit_To_UI(Template_Path + "Template_Structure.xml", "Structure_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Structure_Template"; return; }
                //if (Is_Team) { Switch_Button_Is_Team_Click(null, null); }
            }
    

            else if (Combo_Box_Class.Text == "AntiInfantry")
            {   Track_Bar_Class.Value = 1;
                Progress_Bar_Class.Value = 40;
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                if (Unit_Mode == "Ground") {Button_Class_8_Click(null, null);           
                // Load_Unit_To_UI(Template_Path + "Template_Land_Air.xml", "Air_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Air_Template"; return; }}             
            }       
            else if (Combo_Box_Class.Text == "AntiVehicle")
            {   Track_Bar_Class.Value = 2;
                Progress_Bar_Class.Value = 60;
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                if (Unit_Mode == "Ground") { Button_Class_8_Click(null, null);
                // Load_Unit_To_UI(Template_Path + "Template_Land_Air.xml", "Air_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Air_Template"; return; }}               
            }
            else if (Combo_Box_Class.Text == "AntiAir")
            {   Track_Bar_Class.Value = 3;
                Progress_Bar_Class.Value = 80;
                if (Auto_Apply == "true") { Text_Box_Model_Height.Text = "0"; Text_Box_Select_Box_Height.Text = "0"; }
                if (Unit_Mode == "Ground") { Button_Class_8_Click(null, null);
                // Load_Unit_To_UI(Template_Path + "Template_Land_Air.xml", "Air_Template");
                if (Is_Variant_of_Template) { Text_Box_Is_Variant.Text = "Air_Template"; return; }            
                }                         
            }


        }


        private void Track_Bar_Class_Scroll(object sender, EventArgs e)
        {          
            if (Unit_Mode == "Space") 
            {
                if (Track_Bar_Class.Value == 0)
                {Combo_Box_Class.Text = "Fighter";
                Progress_Bar_Class.Value = 20;
                if (!Is_Team) { if (Size_Expander_I == "false") { Button_Expand_I_Click(null, null); } Switch_Button_Is_Team_Click(null, null); }  //Enabling 
                if (Text_Box_Team_Offsets.Text == "" | Text_Box_Team_Offsets.Text == "4") { Text_Box_Team_Offsets.Text = "3"; }
                }

                else if (Track_Bar_Class.Value == 1)
                {Combo_Box_Class.Text = "Bomber";
                Progress_Bar_Class.Value = 40;
                if (!Is_Team) { if (Size_Expander_I == "false") { Button_Expand_I_Click(null, null); } Switch_Button_Is_Team_Click(null, null); } //Enabling
                if (Text_Box_Team_Offsets.Text == "" | Text_Box_Team_Offsets.Text == "3") { Text_Box_Team_Offsets.Text = "4"; }
                }

                else if (Track_Bar_Class.Value == 2)
                {Combo_Box_Class.Text = "Corvette";
                Progress_Bar_Class.Value = 60;
                if (Is_Team) { if (Size_Expander_I == "false") { Button_Expand_I_Click(null, null); } Switch_Button_Is_Team_Click(null, null); } //Disabling
                }

                else if (Track_Bar_Class.Value == 3)
                {Combo_Box_Class.Text = "Frigate"; 
                Progress_Bar_Class.Value = 80;
                if (Is_Team) { if (Size_Expander_I == "false") { Button_Expand_I_Click(null, null); } Switch_Button_Is_Team_Click(null, null); } //Disabling
                }

                else if (Track_Bar_Class.Value == 4)
                {Combo_Box_Class.Text = "Capital"; 
                Progress_Bar_Class.Value = 100;
                if (Is_Team) { if (Size_Expander_I == "false") { Button_Expand_I_Click(null, null); } Switch_Button_Is_Team_Click(null, null); } //Disabling
                }                
            }

            else if (Unit_Mode == "Ground") 
            {
                if (!Is_Team) { if (Size_Expander_I == "false") { Button_Expand_I_Click(null, null); } Switch_Button_Is_Team_Click(null, null); } //Enabling
                
                if (Track_Bar_Class.Value == 0)
                {Combo_Box_Class.Text = "";
                Progress_Bar_Class.Value = 20;}

                else if (Track_Bar_Class.Value == 1)
                {Combo_Box_Class.Text = "Infantry";
                Progress_Bar_Class.Value = 40;}

                else if (Track_Bar_Class.Value == 2)
                {Combo_Box_Class.Text = "Vehicle";
                Progress_Bar_Class.Value = 60;}

                else if (Track_Bar_Class.Value == 3)
                {Combo_Box_Class.Text = "Air"; 
                Progress_Bar_Class.Value = 80;}

                else if (Track_Bar_Class.Value == 4)
                {Combo_Box_Class.Text = "LandHero"; 
                Progress_Bar_Class.Value = 100;}                
            }

            else if (Unit_Mode == "Structure")
            { 
                if (Track_Bar_Class.Value == 0)
                {Combo_Box_Class.Text = "AntiStructure";
                 Progress_Bar_Class.Value = 20;}

                else if (Track_Bar_Class.Value == 1)
                {Combo_Box_Class.Text = "AntiInfantry";
                 Progress_Bar_Class.Value = 40;}

                else if (Track_Bar_Class.Value == 2)
                {Combo_Box_Class.Text = "AntiVehicle";
                 Progress_Bar_Class.Value = 60;}

                else if (Track_Bar_Class.Value == 3)
                {Combo_Box_Class.Text = "AntiAir";
                 Progress_Bar_Class.Value = 80;}

                else if (Track_Bar_Class.Value == 4)
                {Combo_Box_Class.Text = "Structure";
                 Progress_Bar_Class.Value = 100;}
            }
            
         
        }


        
      

        private void Track_Bar_Hull_Scroll(object sender, EventArgs e)
        {   Scroll_Xml_Value(Track_Bar_Hull, Progress_Bar_Hull, Text_Box_Hull, Maximum_Hull, 100);
            if (User_Input) { Process_Balancing_Percentage(); }          
        }

              
        private void Text_Box_Hull_TextChanged(object sender, EventArgs e)
        {
            if (!Scrolling)
            {   int Typed_Value = Check_Numeral_Text_Box(Text_Box_Hull, Maximum_Hull, false);
                Text_Box_Text_Changed(Track_Bar_Hull, Progress_Bar_Hull, Maximum_Hull, Typed_Value, 100);
            }
            if (User_Input) { Process_Balancing_Percentage(); }
        }


        private void Track_Bar_Shield_Scroll(object sender, EventArgs e)
        {   Scroll_Xml_Value(Track_Bar_Shield, Progress_Bar_Shield, Text_Box_Shield, Maximum_Shield, 100);

            // This automatically sets a value for the second Textbox of the similar tag Shield_Refresh_Rate
            Process_Tag_Value(Progress_Bar_Shield, Text_Box_Shield_Rate, Maximum_Shield, Maximum_Shield_Rate);

            if (User_Input) { Process_Balancing_Percentage(); }
        }

       
        private void Text_Box_Shield_TextChanged(object sender, EventArgs e)
        {   int Typed_Value = Check_Numeral_Text_Box(Text_Box_Shield, Maximum_Shield, false);           
            Text_Box_Text_Changed(Track_Bar_Shield, Progress_Bar_Shield, Maximum_Shield, Typed_Value, 100);
            if (User_Input) { Process_Balancing_Percentage(); }
        }


        private void Text_Box_Shield_Rate_TextChanged(object sender, EventArgs e)
        {   int Typed_Value = Check_Numeral_Text_Box(Text_Box_Shield_Rate, Maximum_Shield_Rate, false);

            // If Godmodeis active and our Shield rate is changed by the user we make sure it auto disables
            if (Toggle_God_Mode & Text_Box_Shield_Rate.Text != "God") 
            { Toggle_Button(Switch_Button_God_Mode, "Button_On", "Button_Off", 0, false); Toggle_God_Mode = false; }
        }


        private void Track_Bar_Energy_Scroll(object sender, EventArgs e)
        {   Scroll_Xml_Value(Track_Bar_Energy, Progress_Bar_Energy, Text_Box_Energy, Maximum_Energy, 100);
            Process_Tag_Value(Progress_Bar_Energy, Text_Box_Energy_Rate, Maximum_Energy, Maximum_Energy_Rate);
        }

        private void Text_Box_Energy_TextChanged(object sender, EventArgs e)
        {   int Typed_Value = Check_Numeral_Text_Box(Text_Box_Energy, Maximum_Energy, false);                        
            Text_Box_Text_Changed(Track_Bar_Energy, Progress_Bar_Energy, Maximum_Energy, Typed_Value, 100);
        }

        private void Text_Box_Energy_Rate_TextChanged(object sender, EventArgs e)
        { int Typed_Value = Check_Numeral_Text_Box(Text_Box_Energy_Rate, Maximum_Energy_Rate, false); }



        private void Track_Bar_Speed_Scroll(object sender, EventArgs e)
        {
            Scroll_Xml_Decimal(Track_Bar_Speed, Progress_Bar_Speed, Text_Box_Speed, Maximum_Speed);

            decimal Current_Value = 0;
            decimal.TryParse(Text_Box_Speed.Text, out Current_Value);

            // Rate of turn is double the value of Max_Speed, then / 10 to get one . sign over
            Text_Box_Rate_Of_Turn.Text = ((Current_Value * 2) / 10).ToString(System.Globalization.CultureInfo.InvariantCulture.NumberFormat);


            // Fighters are a special case: The more they accelerate ahead, the more centrifugal force increases their bank turn angle.
            if (Combo_Box_Class.Text == "Fighter" | Combo_Box_Class.Text == "Bomber") { Text_Box_Bank_Turn_Angle.Text = (Current_Value * 10).ToString(); }
            else
            {   // Bank turn Angle rises here when Max_Speed (Current_Value) falls and vice versa, also x10 to move one . position
                Text_Box_Bank_Turn_Angle.Text = ((4 - Current_Value) * 10).ToString();
            }

            if (User_Input) { Process_Balancing_Percentage(); }
        }

        private void Text_Box_Speed_TextChanged(object sender, EventArgs e)
        {   Set_From_Float_Text_Box(Text_Box_Speed, Track_Bar_Speed, Progress_Bar_Speed, Maximum_Speed, 10, true);
            if (User_Input) { Process_Balancing_Percentage(); }
        }


        private void Track_Bar_AI_Combat_Scroll(object sender, EventArgs e)
        {   Scroll_Xml_Value(Track_Bar_AI_Combat, Progress_Bar_AI_Combat, Text_Box_AI_Combat, Maximum_AI_Combat, 100);
            if (User_Input) { Process_Balancing_Percentage(); }
        }


        private void Text_Box_AI_Combat_TextChanged(object sender, EventArgs e)
        {   int Typed_Value = Check_Numeral_Text_Box(Text_Box_AI_Combat, Maximum_AI_Combat, false);              
            Text_Box_Text_Changed(Track_Bar_AI_Combat, Progress_Bar_AI_Combat, Maximum_AI_Combat, Typed_Value, 100);
            if (User_Input) { Process_Balancing_Percentage(); }
        }


        private void Track_Bar_Population_Scroll(object sender, EventArgs e)
        {   Scroll_Xml_Value(Track_Bar_Population, Progress_Bar_Population, Text_Box_Population, 20, 1);
            if (User_Input) { Process_Balancing_Percentage(); }
        }

        private void Text_Box_Population_TextChanged(object sender, EventArgs e)
        {   int Typed_Value = Check_Numeral_Text_Box(Text_Box_Population, 20, false);
            Text_Box_Text_Changed(Track_Bar_Population, Progress_Bar_Population, 20, Typed_Value, 1);
            if (User_Input) { Process_Balancing_Percentage(); }
        }

        private void Button_Operator_Population_Click(object sender, EventArgs e)
        {
            if (Toggle_Operator_Population == false)
            { Toggle_Button(Button_Operator_Population, "Button_Plus", "Button_Minus", -5, true); Toggle_Operator_Population = true; }
            else { Toggle_Button(Button_Operator_Population, "Button_Plus", "Button_Minus", -5, false); Toggle_Operator_Population = false; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
            if (User_Input) { Process_Balancing_Percentage(); }
        }

        private void Button_Operator_Population_MouseHover(object sender, EventArgs e)
        {
            if (Toggle_Operator_Population) { Toggle_Button(Button_Operator_Population, "Button_Plus", "Button_Plus_Highlighted", -5, false); }
            else { Toggle_Button(Button_Operator_Population, "Button_Minus", "Button_Minus_Highlighted", -5, false); }
        }

        private void Button_Operator_Population_MouseLeave(object sender, EventArgs e)
        {
            if (Toggle_Operator_Population) { Toggle_Button(Button_Operator_Population, "Button_Plus", "Button_Plus_Highlighted", -5, true); }
            else { Toggle_Button(Button_Operator_Population, "Button_Minus", "Button_Minus_Highlighted", -5, true); }
        }


        private void Track_Bar_Projectile_Scroll(object sender, EventArgs e)
        {   Scroll_Xml_Value(Track_Bar_Projectile, Progress_Bar_Projectile, Text_Box_Projectile, Maximum_Projectile, 1);
            if (User_Input) { Process_Balancing_Percentage(); }
        }

        private void Text_Box_Projectile_TextChanged(object sender, EventArgs e)
        {   int Typed_Value = Check_Numeral_Text_Box(Text_Box_Projectile, Maximum_Projectile, false);            
            Text_Box_Text_Changed(Track_Bar_Projectile, Progress_Bar_Projectile, Maximum_Projectile, Typed_Value, 1);
            if (User_Input) { Process_Balancing_Percentage(); }
        }


        //========================= Unit Abilities =========================

        private void Button_Primary_Ability_Click(object sender, EventArgs e)
        {
            if (Selected_Ability != 1)
            {
                Render_Segment_Button(Button_Primary_Ability, "Left", true, 85, 36, 11, 8, 12, Color_03, "Primary");
                Render_Segment_Button(Button_Secondary_Ability, "Right", false, 85, 36, 6, 9, 10, Color_04, "Secondary");
                Selected_Ability = 1;
            }

            // Setting Ability Values from memory
            User_Input = false;
            Combo_Box_Ability_Type.Text = Ability_1_Type;
            if (Ability_1_Toggle_Auto_Fire) { Toggle_Button(Switch_Button_Auto_Fire, "Button_On", "Button_Off", 0, true); Toggle_Auto_Fire = true; }
            else { Toggle_Button(Switch_Button_Auto_Fire, "Button_On", "Button_Off", 0, false); Toggle_Auto_Fire = false; }

            Combo_Box_Activated_GUI_Ability.Text = Ability_1_Activated_GUI;
            Text_Box_Expiration_Seconds.Text = Ability_1_Expiration_Time;
            Text_Box_Recharge_Seconds.Text = Ability_1_Recharge_Time;
            Text_Box_Alternate_Name.Text = Ability_1_Name;
            Text_Box_Alternate_Description.Text = Ability_1_Description;
            Text_Box_Ability_Icon.Text = Ability_1_Icon;
            Text_Box_SFX_Ability_Activated.Text = Ability_1_Activated_SFX;
            Text_Box_SFX_Ability_Deactivated.Text = Ability_1_Deactivated_SFX;

            try
            {
                Temporal_A = "";
                foreach (string Item in Ability_1_Mod_Multipliers.Split(';'))
                { Temporal_A += Item + Add_Line; }
            }
            catch { }

            Text_Box_Mod_Multiplier.Text = Temporal_A;
            // When loading anything User_Input is supposed to be false
            if (Loading_To_UI == false) { User_Input = true; }
        }


        private void Button_Secondary_Ability_Click(object sender, EventArgs e)
        {
            if (Selected_Ability != 2)
            {
                Render_Segment_Button(Button_Primary_Ability, "Left", false, 85, 36, 18, 9, 10, Color_04, "Primary");
                Render_Segment_Button(Button_Secondary_Ability, "Right", true, 85, 36, 2, 8, 12, Color_03, "Secondary");
                Selected_Ability = 2;
            }

            User_Input = false;
            Combo_Box_Ability_Type.Text = Ability_2_Type;
            if (Ability_2_Toggle_Auto_Fire) { Toggle_Button(Switch_Button_Auto_Fire, "Button_On", "Button_Off", 0, true); Toggle_Auto_Fire = true; }
            else { Toggle_Button(Switch_Button_Auto_Fire, "Button_On", "Button_Off", 0, false); Toggle_Auto_Fire = false; }

            Combo_Box_Activated_GUI_Ability.Text = Ability_2_Activated_GUI;
            Text_Box_Expiration_Seconds.Text = Ability_2_Expiration_Time;
            Text_Box_Recharge_Seconds.Text = Ability_2_Recharge_Time;
            Text_Box_Alternate_Name.Text = Ability_2_Name;
            Text_Box_Alternate_Description.Text = Ability_2_Description;
            Text_Box_Ability_Icon.Text = Ability_2_Icon;
            Text_Box_SFX_Ability_Activated.Text = Ability_2_Activated_SFX;
            Text_Box_SFX_Ability_Deactivated.Text = Ability_2_Deactivated_SFX;
            Text_Box_Mod_Multiplier.Text = Ability_2_Mod_Multipliers;

            try
            {
                Temporal_A = "";
                foreach (string Item in Ability_2_Mod_Multipliers.Split(';'))
                { Temporal_A += Item + Add_Line; }
            }
            catch { }

            Text_Box_Mod_Multiplier.Text = Temporal_A;
            // When loading anything User_Input is supposed to be false
            if (Loading_To_UI == false) { User_Input = true; }
        }

        private void Combo_Box_Ability_Type_SelectedIndexChanged(object sender, EventArgs e)
        {   // We need to store these values in variables to enable dynamic switching between values of first and second ability!
            if (Selected_Ability == 1)
            {
                if (Combo_Box_Ability_Type.Text == " NONE" | Combo_Box_Ability_Type.Text == "") { Ability_1_Remove = Ability_1_Type; }
                Ability_1_Type = Combo_Box_Ability_Type.Text;
            }
            else if (Selected_Ability == 2)
            {
                if (Combo_Box_Ability_Type.Text == " NONE") { Ability_2_Remove = Ability_2_Type; }

                // Making sure Ability 1 is always the first assigned one
                if (Ability_1_Type == "")
                {
                    if (Combo_Box_Ability_Type.Text == " NONE") { Ability_1_Remove = Ability_1_Type; }
                    Ability_1_Type = Combo_Box_Ability_Type.Text;
                    Button_Primary_Ability_Click(null, null);
                }
                else { Ability_2_Type = Combo_Box_Ability_Type.Text; }
            }


            if (Combo_Box_Ability_Type.Text == "HYPERJUMP")
            {   // Reenabling User Input
                Track_Bar_Expiration_Seconds.Enabled = true;
                Text_Box_Expiration_Seconds.Enabled = true;
                Track_Bar_Recharge_Seconds.Enabled = true;
                Text_Box_Recharge_Seconds.Enabled = true;
            }
            else if (User_Input & Auto_Apply == "true")
            {
                if (Text_Box_Expiration_Seconds.Text == "") { Text_Box_Expiration_Seconds.Text = "30"; }
                if (Text_Box_Recharge_Seconds.Text == "") { Text_Box_Recharge_Seconds.Text = "60"; }
            }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }

            bool Verify_Ability_Behavoir = false;
            string Prepend_Text = "";
            string Append_Text = "";
            string Ability_Lua_Script = "";


            // Different Abilities add different individual tags:
            switch (Combo_Box_Ability_Type.Text)
            {
                case "AREA_EFFECT_STUN":
                    if (!Text_Box_Mod_Multiplier.Text.Contains("Spawned_Object_Type"))
                    { Prepend_Text = "<Spawned_Object_Type>Proj_Special_Han_Solo_Stun_Blast</Spawned_Object_Type>"; }
                    break;
                case "BARRAGE":
                    // Verifying Ability Behavoir
                    if (!List_Box_Active_Behavoir.Items.Contains("BARRAGE"))
                    { List_Box_Active_Behavoir.Items.Add("BARRAGE"); List_Box_Inactive_Behavoir.Items.Remove("BARRAGE"); }

                    // Prepending
                    if (!Text_Box_Mod_Multiplier.Text.Contains("Projectile_Types_Override"))
                    { Prepend_Text = "<Projectile_Types_Override></Projectile_Types_Override>"; }

                    // Appending to Prepend Text
                    if (!Text_Box_Mod_Multiplier.Text.Contains("Area_Effect_Decal_Distance"))
                    { Prepend_Text += Add_Line + "<Area_Effect_Decal_Distance>300.0</Area_Effect_Decal_Distance>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Targeting_Fire_Inaccuracy_Fixed_Radius_Override"))
                    { Prepend_Text += Add_Line + "<Targeting_Fire_Inaccuracy_Fixed_Radius_Override>300.0</Targeting_Fire_Inaccuracy_Fixed_Radius_Override>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Target_Position_Z_Offset"))
                    { Prepend_Text += Add_Line + "<Target_Position_Z_Offset>-200</Target_Position_Z_Offset>"; }

                    // Appending
                    if (!Text_Box_Mod_Multiplier.Text.Contains("FIRE_RATE_MULTIPLIER"))
                    { Append_Text = "<Mod_Multiplier>FIRE_RATE_MULTIPLIER, 3.0f</Mod_Multiplier>"; }
                    break;
                case "BLAST":
                    Combo_Box_Activated_GUI_Ability.Text = "Unit_Blast";

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Targeting_Max_Attack_Distance"))
                    { Prepend_Text = "<Targeting_Max_Attack_Distance>2000</Targeting_Max_Attack_Distance>"; }

                    break;
                case "CLUSTER_BOMB":
                    Combo_Box_Activated_GUI_Ability.Text = "Cluster_Bomb";
                    Text_Box_SFX_Ability_Activated.Text = "Unit_Cluster_Bombs_MC30_Frigate";

                    if (!Toggle_Auto_Fire) { Switch_Button_Auto_Fire_Click(null, null); }
                    Ability_Lua_Script = "ObjectScript_MC30"; //cript from the Vanilla game that controlls this Ability 

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Effective_Radius"))
                    { Prepend_Text = "<Effective_Radius>500.0</Effective_Radius>"; }
                    break;

                case "CAPTURE_VEHICLE":
                    Verify_Ability_Behavoir = true;
                    Combo_Box_Activated_GUI_Ability.Text = "Vehicle_Thief";
                    break;


                case "CONCENTRATE_FIRE":
                    Combo_Box_Activated_GUI_Ability.Text = "Concentrate_Fire";

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Effective_Radius"))
                    { Prepend_Text = "<Effective_Radius>8000</Effective_Radius>"; }

                    // Appending to Prepend Text
                    if (!Text_Box_Mod_Multiplier.Text.Contains("Particle_Effect"))
                    { Prepend_Text += Add_Line + "<Particle_Effect>Home_One_Target_Particles</Particle_Effect>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("SFXEvent_Target_Ability"))
                    { Prepend_Text += Add_Line + "<SFXEvent_Target_Ability>Unit_Barrage_Ackbar</SFXEvent_Target_Ability>"; }

                    break;

                case "HARMONIC_BOMB":
                    if (!Text_Box_Mod_Multiplier.Text.Contains("Spawned_Object_Type"))
                    { Prepend_Text = "<Spawned_Object_Type></Spawned_Object_Type>"; }

                    break;

                case "HYPERJUMP":
                    if (Toggle_Auto_Fire) { Switch_Button_Auto_Fire_Click(null, null); }

                    Temporal_A = "false"; // Need to prevent this event from triggering below for Weaken enemy somehow
                    Combo_Box_Ability_Type.Text = "WEAKEN_ENEMY";

                    // Prepending
                    if (!Text_Box_Mod_Multiplier.Text.Contains("Spawned_Object_Type"))
                    { Prepend_Text = "<Spawned_Object_Type>Hyperspace_Jump_Target</Spawned_Object_Type>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Effective_Radius"))
                    { Prepend_Text += Add_Line + "<Effective_Radius>40000</Effective_Radius>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Owner_Particle_Bone_Name"))
                    { Prepend_Text += Add_Line + "<Owner_Particle_Bone_Name>ROOT</Owner_Particle_Bone_Name>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Bomb_Countdown_Seconds"))
                    { Prepend_Text += Add_Line + "<Bomb_Countdown_Seconds>-1</Bomb_Countdown_Seconds>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Layer_Z_Adjust"))
                    { Prepend_Text += Add_Line + "<Layer_Z_Adjust>0.0</Layer_Z_Adjust>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Area_Effect_Decal_Distance"))
                    { Prepend_Text += Add_Line + "<Area_Effect_Decal_Distance>20</Area_Effect_Decal_Distance>"; }

                    // Decreasing Energy strengh as a Hyperjump costs a lot of power
                    if (!Text_Box_Mod_Multiplier.Text.Contains("ENERGY_REGEN_MULTIPLIER"))
                    { Append_Text = "<Mod_Multiplier>ENERGY_REGEN_MULTIPLIER, 0.2f</Mod_Multiplier>"; }


                    // Need the Recharge time to be the same as in the Hyper_Space script, or the ability would not work!
                    Text_Box_Expiration_Seconds.Text = "5";
                    Text_Box_Recharge_Seconds.Text = "120";

                    Track_Bar_Expiration_Seconds.Enabled = false;
                    Text_Box_Expiration_Seconds.Enabled = false;

                    Track_Bar_Recharge_Seconds.Enabled = false;
                    Text_Box_Recharge_Seconds.Enabled = false;

                    Text_Box_Alternate_Name.Text = "TEXT_BUTTON_HYPERSPACE";
                    Text_Box_Ability_Icon.Text = "I_SA_FORCE_CONFUSE";

                    // Activating Jump Lua script and validating Particle models      
                    if (!Toggle_Use_Particle) { Switch_Button_Use_Particle_Click(null, null); }

                    break;


                case " NONE":
                    Clear_Ability_Values();

                    break;
                case "WEAKEN_ENEMY":
                    // This should not run if the event is triggered by the "HYPERJUMP" ability
                    if (Temporal_A == "false") { Temporal_A = ""; return; }

                    // Prepending
                    if (!Text_Box_Mod_Multiplier.Text.Contains("Spawned_Object_Type"))
                    { Prepend_Text = "<Spawned_Object_Type></Spawned_Object_Type>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Effective_Radius"))
                    { Prepend_Text += Add_Line + "<Effective_Radius>40000</Effective_Radius>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Owner_Particle_Bone_Name"))
                    { Prepend_Text += Add_Line + "<Owner_Particle_Bone_Name>ROOT</Owner_Particle_Bone_Name>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Bomb_Countdown_Seconds"))
                    { Prepend_Text += Add_Line + "<Bomb_Countdown_Seconds>-1</Bomb_Countdown_Seconds>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Layer_Z_Adjust"))
                    { Prepend_Text += Add_Line + "<Layer_Z_Adjust>0.0</Layer_Z_Adjust>"; }

                    if (!Text_Box_Mod_Multiplier.Text.Contains("Area_Effect_Decal_Distance"))
                    { Prepend_Text += Add_Line + "<Area_Effect_Decal_Distance>20</Area_Effect_Decal_Distance>"; }

                    break;
            }


            if (Verify_Ability_Behavoir)
            {   // Only work if the Ability Type and the Behavoir have both the same name!
                if (!List_Box_Active_Behavoir.Items.Contains(Combo_Box_Ability_Type.Text))
                { List_Box_Active_Behavoir.Items.Add(Combo_Box_Ability_Type.Text); List_Box_Inactive_Behavoir.Items.Remove(Combo_Box_Ability_Type.Text); }
            }

            if (!List_Box_Active_Behavoir.Items.Contains("ABILITY_COUNTDOWN"))
            { List_Box_Active_Behavoir.Items.Add("ABILITY_COUNTDOWN"); List_Box_Inactive_Behavoir.Items.Remove("ABILITY_COUNTDOWN"); }

            Temporal_A = Add_Line;
            if (Text_Box_Mod_Multiplier.Text == "") { Temporal_A = ""; }

            // Prepending 
            if (Prepend_Text != "") { Text_Box_Mod_Multiplier.Text = Temporal_A + Prepend_Text + Text_Box_Mod_Multiplier.Text; Text_Box_Mod_Multiplier.Focus(); }

            // Appending
            if (Append_Text != "") { Text_Box_Mod_Multiplier.Text += Add_Line + Append_Text; Text_Box_Mod_Multiplier.Focus(); }



            if (Ability_Lua_Script != "" & Text_Box_Lua_Script.Text == "")
            { Text_Box_Lua_Script.Text = Ability_Lua_Script; }

            else if (Ability_Lua_Script != "" & Text_Box_Lua_Script.Text != Ability_Lua_Script)
            {
                Imperial_Dialogue(500, 160, "Yes", "No", "false", Add_Line + "    Do you wish to overwrite the Lua_Script tag"
                                                                + Add_Line + "    with the " + Combo_Box_Ability_Type.Text + " ability script?.");

                if (Caution_Window.Passed_Value_A.Text_Data == "true")
                {
                    Text_Box_Lua_Script.Text = Ability_Lua_Script;

                    // Setting back to false to prevent missfiring when user closes the verification dialogue next time.
                    Caution_Window.Passed_Value_A.Text_Data = "false";
                }
            }

        }

        private void Button_Search_Ability_Click(object sender, EventArgs e)
        {
            if (Combo_Box_Ability_Type.Text != "")
            {
                Temporal_A = Combo_Box_Filter_Type.Text;
                if (Temporal_A == "Squadron") { Combo_Box_Filter_Type.Text = "SpaceUnit"; }
                else if (Temporal_A == "HeroCompany" | Temporal_A == "GroundCompany") { Combo_Box_Filter_Type.Text = "GroundInfantry"; }

                List_Box_All_Instances.Items.Clear();
                if (Size_Expander_C == "false") { Button_Expand_C_Click(null, null); }
                Temporal_E = Ability_Utility(null, null, Combo_Box_Ability_Type.Text, null).Split(',');


                foreach (string Item in Temporal_E)
                {
                    List_Box_All_Instances.Items.Add(Item);
                }

                List_Box_All_Instances.Focus();
            }
        }

        private void Switch_Button_Auto_Fire_Click(object sender, EventArgs e)
        {
            if (Toggle_Auto_Fire) { Toggle_Button(Switch_Button_Auto_Fire, "Button_On", "Button_Off", 0, false); Toggle_Auto_Fire = false; }
            else { Toggle_Button(Switch_Button_Auto_Fire, "Button_On", "Button_Off", 0, true); Toggle_Auto_Fire = true; }

            // Storing this seperately for each Ability 
            if (Selected_Ability == 1) { Ability_1_Toggle_Auto_Fire = Toggle_Auto_Fire; }
            else if (Selected_Ability == 2) { Ability_2_Toggle_Auto_Fire = Toggle_Auto_Fire; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Combo_Box_Activated_GUI_Ability_SelectedIndexChanged(object sender, EventArgs e)
        {
            Temporal_A = Combo_Box_Activated_GUI_Ability.Text;
            string Current_Code = "\t\t\t";

            if (Selected_Ability == 1) { Ability_1_Activated_GUI = Combo_Box_Activated_GUI_Ability.Text; }
            else if (Selected_Ability == 2) { Ability_2_Activated_GUI = Combo_Box_Activated_GUI_Ability.Text; }


            if (Temporal_A == "Unit_Blast" & !Text_Box_Additional_Abilities.Text.Contains("Blast_Ability"))
            {
                Current_Code +=
      @"<Blast_Ability Name=""Unit_Blast"">
        <Activation_Style>User_Input</Activation_Style>
        <Charge_Up_Seconds>5</Charge_Up_Seconds>
        <Damage_Multiplier>1</Damage_Multiplier>
        <Charging_Effect></Charging_Effect>
        <Charged_Effect></Charged_Effect>
        <SFXEvent_Activate></SFXEvent_Activate>
      </Blast_Ability>";
            }

            else if (Temporal_A == "Vehicle_Thief" & !Text_Box_Additional_Abilities.Text.Contains("Vehicle_Thief_Ability"))
            {
                Current_Code +=
      @"<Vehicle_Thief_Ability Name=""Vehicle_Thief"">
        <Activation_Style>User_Input</Activation_Style>
        <Applicable_Unit_Categories></Applicable_Unit_Categories>  								
        <Applicable_Unit_Types>
        </Applicable_Unit_Types> 				
        <Activation_Min_Range>0.0</Activation_Min_Range>
        <Activation_Max_Range>150.0</Activation_Max_Range>
        <Activation_Chance>1.0</Activation_Chance>
        <SFXEvent_Activate>Unit_Chewie_Beat_Up_SFX</SFXEvent_Activate>
  	  </Vehicle_Thief_Ability>";
            }

            else if (Temporal_A == "Cluster_Bomb" & !Text_Box_Additional_Abilities.Text.Contains("Cluster_Bomb_Ability"))
            {
                Current_Code +=
      @"<Cluster_Bomb_Ability Name=""Cluster_Bomb"">
        <Activation_Style>User_Input</Activation_Style>
        <Detonation_Time_In_Secs>1.5</Detonation_Time_In_Secs>
        <Bomb_Type>Proj_Cluster_Bomb</Bomb_Type>
        <Number_Of_Bombs>12</Number_Of_Bombs>
        <SFXEvent_Activate>Unit_MC30_Launch_Cluster_Bomb</SFXEvent_Activate>
      </Cluster_Bomb_Ability>";
            }

            else if (Temporal_A == "Concentrate_Fire" & !Text_Box_Additional_Abilities.Text.Contains("Concentrate_Fire_Attack_Ability"))
            {
                Current_Code +=
      @"<Concentrate_Fire_Attack_Ability Name=""Concentrate_Fire"">
		<Activation_Style> User_Input </Activation_Style>
		<Applicable_Unit_Categories>Fighter | Bomber | Transport | Corvette | Frigate | Capital</Applicable_Unit_Categories>
		<Applicable_Unit_Types></Applicable_Unit_Types>
		<Target_Damage_Increase_Percent>0.5</Target_Damage_Increase_Percent>
		<Target_Speed_Decrease_Percent>0.0</Target_Speed_Decrease_Percent>
		<Stacking_Category>0</Stacking_Category>
	  </Concentrate_Fire_Attack_Ability>";
            }






            // Appending Code
            if (Current_Code != "\t\t\t")
            {
                if (Text_Box_Additional_Abilities.Text.Contains("</Abilities>"))
                { Text_Box_Additional_Abilities.Text = Regex.Replace(Text_Box_Additional_Abilities.Text, "</Abilities>", Current_Code + Add_Line + "\t\t</Abilities>"); }
                else { Text_Box_Additional_Abilities.Text = @"<Abilities SubObjectList=""Yes"">" + Add_Line + Current_Code + Add_Line + "\t\t</Abilities>"; }
            }


            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }



        private void Track_Bar_Expiration_Seconds_Scroll(object sender, EventArgs e)
        {
            Scroll_Xml_Value(Track_Bar_Expiration_Seconds, Progress_Bar_Expiration_Seconds, Text_Box_Expiration_Seconds, 200, 1);
            // This automatically sets a value for the second Textbox 
            if (Auto_Apply == "true") { Scroll_Xml_Value(Track_Bar_Expiration_Seconds, Progress_Bar_Recharge_Seconds, Text_Box_Recharge_Seconds, 200, 1); }
        }

        private void Text_Box_Expiration_Seconds_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Expiration_Seconds, 200, false);
            Text_Box_Text_Changed(Track_Bar_Expiration_Seconds, Progress_Bar_Expiration_Seconds, 200, Typed_Value, 1);

            if (Selected_Ability == 1) { Ability_1_Expiration_Time = Text_Box_Expiration_Seconds.Text; }
            else if (Selected_Ability == 2) { Ability_2_Expiration_Time = Text_Box_Expiration_Seconds.Text; }
        }


        private void Track_Bar_Recharge_Seconds_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Recharge_Seconds, Progress_Bar_Recharge_Seconds, Text_Box_Recharge_Seconds, 200, 1); }

        private void Text_Box_Recharge_Seconds_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Recharge_Seconds, 200, false);
            Text_Box_Text_Changed(Track_Bar_Recharge_Seconds, Progress_Bar_Recharge_Seconds, 200, Typed_Value, 1);

            if (Selected_Ability == 1) { Ability_1_Recharge_Time = Text_Box_Recharge_Seconds.Text; }
            else if (Selected_Ability == 2) { Ability_2_Recharge_Time = Text_Box_Recharge_Seconds.Text; }
        }


        private void Label_Alternate_Name_Click(object sender, EventArgs e)
        { Text_Box_Alternate_Name.Text = Clipboard.GetText(); }

        private void Text_Box_Alternate_Name_TextChanged(object sender, EventArgs e)
        {
            if (Selected_Ability == 1) { Ability_1_Name = Text_Box_Alternate_Name.Text; }
            else if (Selected_Ability == 2) { Ability_2_Name = Text_Box_Alternate_Name.Text; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Label_Alternate_Description_Click(object sender, EventArgs e)
        { Text_Box_Alternate_Description.Text = Clipboard.GetText(); }

        private void Text_Box_Alternate_Description_TextChanged(object sender, EventArgs e)
        {
            if (Selected_Ability == 1) { Ability_1_Description = Text_Box_Alternate_Description.Text; }
            else if (Selected_Ability == 2) { Ability_2_Description = Text_Box_Alternate_Description.Text; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Label_Alternate_Icon_Click(object sender, EventArgs e)
        { Text_Box_Ability_Icon.Text = Clipboard.GetText(); }

        private void Label_Ability_Icon_Click(object sender, EventArgs e)
        { System.Diagnostics.Process.Start(Program_Directory + @"Misc\Modding_Tools\MTD_Editor.exe"); }

        private void Text_Box_Ability_Icon_TextChanged(object sender, EventArgs e)
        {
            if (Selected_Ability == 1) { Ability_1_Icon = Text_Box_Ability_Icon.Text; }
            else if (Selected_Ability == 2) { Ability_2_Icon = Text_Box_Ability_Icon.Text; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Label_SFX_Ability_Activated_Click(object sender, EventArgs e)
        { Text_Box_SFX_Ability_Activated.Text = Clipboard.GetText(); }

        private void Text_Box_SFX_Ability_Activated_TextChanged(object sender, EventArgs e)
        {
            if (Selected_Ability == 1) { Ability_1_Activated_SFX = Text_Box_SFX_Ability_Activated.Text; }
            else if (Selected_Ability == 2) { Ability_2_Activated_SFX = Text_Box_SFX_Ability_Activated.Text; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Label_SFX_Ability_Deactivated_Click(object sender, EventArgs e)
        { Text_Box_SFX_Ability_Deactivated.Text = Clipboard.GetText(); }

        private void Text_Box_SFX_Ability_Deactivated_TextChanged(object sender, EventArgs e)
        {
            if (Selected_Ability == 1) { Ability_1_Deactivated_SFX = Text_Box_SFX_Ability_Deactivated.Text; }
            else if (Selected_Ability == 2) { Ability_2_Deactivated_SFX = Text_Box_SFX_Ability_Deactivated.Text; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }



        private void Combo_Box_Mod_Multiplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Selected_Ability == 1 & Text_Box_Mod_Multiplier.Text != "") { Ability_1_Mod_Multipliers = Text_Box_Mod_Multiplier.Text; }
            else if (Selected_Ability == 2 & Text_Box_Mod_Multiplier.Text != "") { Ability_2_Mod_Multipliers = Text_Box_Mod_Multiplier.Text; }

            string Bonus_Amount = ", 1.0f";
            if (Combo_Box_Mod_Multiplier.Text != "")
            {
                if (Combo_Box_Mod_Multiplier.Text == "SHIELD_REGEN_INTERVAL_MULTIPLIER" | Combo_Box_Mod_Multiplier.Text == "ENERGY_REGEN_INTERVAL_MULTIPLIER") { Bonus_Amount = ", 0.1f"; }
                Text_Box_Mod_Multiplier.Text += Add_Line + "<Mod_Multiplier>" + Combo_Box_Mod_Multiplier.Text + Bonus_Amount + "</Mod_Multiplier>";
            }
        }

        private void Text_Box_Mod_Multiplier_TextChanged(object sender, EventArgs e)
        {
            if (User_Input)
            {
                if (Selected_Ability == 1) { Ability_1_Mod_Multipliers = Text_Box_Mod_Multiplier.Text; }
                else if (Selected_Ability == 2) { Ability_2_Mod_Multipliers = Text_Box_Mod_Multiplier.Text; }

                if (Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
            }
        }



        private void Check_Box_Use_In_Team_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_Box_Use_In_Team.Checked) { Check_Box_Use_In_Team.ForeColor = Color_03; }
            else { Check_Box_Use_In_Team.ForeColor = Color.Gray; }
        }

        private void Combo_Box_Additional_Abilities_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Button_Add_Ability_Click(object sender, EventArgs e)
        { if (Combo_Box_Additional_Abilities.Text != "") { Text_Box_Additional_Abilities.Text += Combo_Box_Additional_Abilities.Text + Add_Line; } }

        private void Button_Add_Ability_MouseHover(object sender, EventArgs e)
        { Toggle_Button(Button_Add_Ability, "Button_Plus", "Button_Plus_Highlighted", -2, false); }

        private void Button_Add_Ability_MouseLeave(object sender, EventArgs e)
        { Toggle_Button(Button_Add_Ability, "Button_Plus_Highlighted", "Button_Plus", -2, false); }

        private void Text_Box_Additional_Abilities_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }



        // ================================= Unit Properties =================================//


        // ================== Team Subbox ==================//

        private void Switch_Button_Is_Team_Click(object sender, EventArgs e)
        {
            int Expansion_Value = 836;

            if (Is_Team)
            {
                Toggle_Button(Switch_Button_Is_Team, "Button_On", "Button_Off", 0, false); Is_Team = false;

                ToggleControlY(Group_Box_Team, null, Move_List_for_Team, -Expansion_Value);
                // Changing Size of Parent Group Box as well
                Group_Box_Properties.Height += -Expansion_Value;


                // Replacing Locomotor Behavoir inside of the pool
                if (List_Box_Inactive_Behavoir.Items.Contains("FIGHTER_LOCOMOTOR"))
                { List_Box_Inactive_Behavoir.Items.Remove("FIGHTER_LOCOMOTOR"); }

                if (List_Box_Active_Behavoir.Items.Contains("FIGHTER_LOCOMOTOR"))
                { List_Box_Active_Behavoir.Items.Remove("FIGHTER_LOCOMOTOR"); }

                if (!List_Box_Active_Behavoir.Items.Contains("SIMPLE_SPACE_LOCOMOTOR") & !List_Box_Inactive_Behavoir.Items.Contains("SIMPLE_SPACE_LOCOMOTOR"))
                { List_Box_Active_Behavoir.Items.Add("SIMPLE_SPACE_LOCOMOTOR"); }                          
            }

            else
            {
                Toggle_Button(Switch_Button_Is_Team, "Button_On", "Button_Off", 0, true); Is_Team = true;

                ToggleControlY(Group_Box_Team, null, Move_List_for_Team, Expansion_Value);
                Group_Box_Properties.Height += Expansion_Value;

                if (Auto_Apply == "true")
                {
                    if (Team_Type == "Squadron") { Temporal_A = "_Squadron"; }
                    else { Temporal_A = "_Team"; }

                    // Only if Text_Box_Team_Name.Text is empty, or it would overwrite the Squadron we selected (which has most amount of tags and thus contains probably the full code)
                    if (Text_Box_Name.Text != "" & Text_Box_Team_Name.Text == "") { Text_Box_Team_Name.Text = Text_Box_Name.Text + Temporal_A; }

                    // Only if both are empty, or it would overwrite what ever got loaded
                    if (Text_Box_Team_Members.Text == "" & Text_Box_Team_Amount.Text == "") { Text_Box_Team_Amount.Text = "1"; }
                }


                if (List_Box_Inactive_Behavoir.Items.Contains("SIMPLE_SPACE_LOCOMOTOR"))
                { List_Box_Inactive_Behavoir.Items.Remove("SIMPLE_SPACE_LOCOMOTOR"); }

                if (List_Box_Active_Behavoir.Items.Contains("SIMPLE_SPACE_LOCOMOTOR"))
                { List_Box_Active_Behavoir.Items.Remove("SIMPLE_SPACE_LOCOMOTOR"); }

                if (!List_Box_Active_Behavoir.Items.Contains("FIGHTER_LOCOMOTOR") & !List_Box_Inactive_Behavoir.Items.Contains("FIGHTER_LOCOMOTOR"))
                { List_Box_Active_Behavoir.Items.Add("FIGHTER_LOCOMOTOR"); }                       

            }
        }

        private void List_View_Teams_Click(object sender, EventArgs e)
        {
            string Selection = "";

            // Getting the currently selected Item
            if (List_View_Teams.SelectedItems.Count > 0)
            { Selection = List_View_Teams.SelectedItems[0].Text; }


            if (User_Input & Selection != Text_Box_Team_Name.Text)
            {   // Showing the user he needs to wait until this finished
                Application.UseWaitCursor = true;
                Application.DoEvents();

                Load_Team_To_UI(null, Selection);
            }
        }


        private void Label_Team_Name_Click(object sender, EventArgs e)
        { Text_Box_Team_Name.Text = Clipboard.GetText(); }

        private void Text_Box_Team_Name_TextChanged(object sender, EventArgs e)
        { 
            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } 
        }


        private void Mini_Button_Copy_Team_Click(object sender, EventArgs e)
        {   // Getting Unit     
            Clipboard.SetText( Text_Box_Team_Name.Text);
        }

        private void Mini_Button_Copy_Team_MouseHover(object sender, EventArgs e)
        {   Temporal_A = "Copy";
            PictureBox The_Button = Mini_Button_Copy_Team;
            if (File.Exists(Selected_Theme + @"Buttons\Button_" + Temporal_A + "_Highlighted.png")) { The_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_" + Temporal_A + "_Highlighted.png", The_Button.Size.Width, The_Button.Size.Height); }
            else { The_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_" + Temporal_A + "_Highlighted.png", The_Button.Size.Width, The_Button.Size.Height); }
        }

        private void Mini_Button_Copy_Team_MouseLeave(object sender, EventArgs e)
        {   Temporal_A = "Copy";
            PictureBox The_Button = Mini_Button_Copy_Team;
            if (File.Exists(Selected_Theme + @"Buttons\Button_" + Temporal_A + ".png")) { The_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_" + Temporal_A + ".png", The_Button.Size.Width, The_Button.Size.Height); }
            else { The_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_" + Temporal_A + ".png", The_Button.Size.Width, The_Button.Size.Height); }
        }


     

        private void Combo_Box_Team_Type_SelectedIndexChanged(object sender, EventArgs e)
        {   // Setting global variable, Add_Tag_To_Team() will use it later
            Team_Type = Combo_Box_Team_Type.Text;

            if (Combo_Box_Team_Type.Text == "Squadron") { Label_Is_Team.Text = "Is Squadron"; Check_Box_Use_In_Team.Text = "Use in Squadron"; }
            else if (Label_Is_Team.Text != "Is Team") { Label_Is_Team.Text = "Is Team"; Check_Box_Use_In_Team.Text = "Use in Team";}

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }



        private void Label_Team_Is_Variant_Click(object sender, EventArgs e)
        { Text_Box_Team_Is_Variant.Text = Clipboard.GetText(); }

        private void Text_Box_Team_Is_Variant_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }

        private void Check_Box_Team_Is_Variant_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_Box_Team_Is_Variant.Checked)
            {
                Check_Box_Team_Is_Variant.ForeColor = Color_03;
                Text_Box_Team_Is_Variant.Visible = true;
                Picture_Box_Team_Is_Variant.Visible = false;
                Button_Open_Team_Variant.Visible = true;
            }
            else
            {
                Check_Box_Team_Is_Variant.ForeColor = Color_02;
                Text_Box_Team_Is_Variant.Visible = false;
                Picture_Box_Team_Is_Variant.Visible = true;
                Button_Open_Team_Variant.Visible = false;
            }

            if (User_Input == true) { if (Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }
        }

        private void Button_Open_Team_Variant_Click(object sender, EventArgs e)
        {
            if (Text_Box_Team_Is_Variant.Text == "") { return; }

            if (Edited_Selected_Unit == true)
            {
                // Call Imperial Dialogue with resolution of 540x160, Button 1 Name , Button 2 Name and Dialogue Text. Then we wait for user input
                Imperial_Dialogue(580, 160, "Save + Open", "Cancel", "Don't Save", Add_Line + "    There are unsaved changes in this Unit."
                                                                    + Add_Line + "    Are you sure you wish to open another Unit yet?");

                // If user aborted the program execution
                if (Caution_Window.Passed_Value_A.Text_Data == "true")
                {
                    // Saving before exit
                    if (Label_Xml_Name.Text == "Creating New File") { Save_As_Click(null, null); }

                    // Otherwise we hopefully have a other Xml open which can be saved
                    else { Save_Click(null, null); }

                }
                else if (Caution_Window.Passed_Value_A.Text_Data == "false")
                {
                    return;
                }
            }

            Application.UseWaitCursor = true;
            Application.DoEvents();

            Load_Team_To_UI(null, Text_Box_Team_Is_Variant.Text);
        }

        private void Button_Open_Team_Variant_MouseHover(object sender, EventArgs e)
        {
            if (File.Exists(Selected_Theme + @"Buttons\Button_Arrow_Highlighted.png")) { Button_Open_Team_Variant.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_Arrow_Highlighted.png", Button_Open_Team_Variant.Size.Width, Button_Open_Team_Variant.Size.Height); }
            else { Button_Open_Team_Variant.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_Arrow_Highlighted.png", Button_Open_Team_Variant.Size.Width, Button_Open_Team_Variant.Size.Height); }
        }

        private void Button_Open_Team_Variant_MouseLeave(object sender, EventArgs e)
        {
            if (File.Exists(Selected_Theme + @"Buttons\Button_Arrow.png")) { Button_Open_Team_Variant.Image = Resize_Image(Selected_Theme + @"Buttons\", "Button_Arrow.png", Button_Open_Team_Variant.Size.Width, Button_Open_Team_Variant.Size.Height); }
            else { Button_Open_Team_Variant.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", "Button_Arrow.png", Button_Open_Team_Variant.Size.Width, Button_Open_Team_Variant.Size.Height); }
        }



        private void Label_Shuttle_Type_Click(object sender, EventArgs e)
        { Text_Box_Shuttle_Type.Text = Clipboard.GetText(); }

        private void Text_Box_Shuttle_Type_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }



        private void Track_Bar_Team_Members_Scroll(object sender, EventArgs e)
        {
            Scroll_Xml_Value(Track_Bar_Team_Members, Progress_Bar_Team_Members, Text_Box_Team_Amount, 5, 1);

            // This automatically sets a value for the second Textbox of the similar tag Shield_Refresh_Rate
            if (Text_Box_Team_Offsets.Text == "" & Combo_Box_Class.Text == "Fighter") { Text_Box_Team_Offsets.Text = "3"; }
            else if (Text_Box_Team_Offsets.Text == "" & Combo_Box_Class.Text == "Bomber") { Text_Box_Team_Offsets.Text = "4"; }

            if (User_Input) { Process_Balancing_Percentage(); }
        }

        private void Text_Box_Team_Amount_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Team_Amount, 10, false);
            Text_Box_Text_Changed(Track_Bar_Team_Members, Progress_Bar_Team_Members, 5, Typed_Value, 1);



            Int32.TryParse(Text_Box_Team_Amount.Text, out Temporal_C);
            Temporal_A = "";

            for (int i = 0; i < Temporal_C; ++i)
            {
                if (i < Temporal_C - 1)
                { Temporal_A += Text_Box_Name.Text + ", " + Add_Line; }
                else { Temporal_A += Text_Box_Name.Text; }
            }

            Text_Box_Team_Members.Text = Temporal_A;


            if (User_Input)
            {
                if (Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
                Process_Balancing_Percentage();
            }
        }

        private void Text_Box_Team_Offsets_TextChanged(object sender, EventArgs e)
        {
            Check_Numeral_Text_Box(Text_Box_Team_Offsets, 10, false);
            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }

     
        // =================================================//


        private void Switch_Button_Is_Hero_Click(object sender, EventArgs e)
        {
            if (Toggle_Is_Hero)
            { Toggle_Button(Switch_Button_Is_Hero, "Button_On", "Button_Off", 0, false); Toggle_Is_Hero = false; }

            else
            {
                Toggle_Button(Switch_Button_Is_Hero, "Button_On", "Button_Off", 0, true); Toggle_Is_Hero = true;

                // If this is active, the other one auto activates as well
                if (Toggle_Show_Head == false) { Toggle_Button(Switch_Button_Show_Head, "Button_On", "Button_Off", 0, true); Toggle_Show_Head = true; }
            }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Switch_Button_Show_Head_Click(object sender, EventArgs e)
        {
            if (Toggle_Show_Head) { Toggle_Button(Switch_Button_Show_Head, "Button_On", "Button_Off", 0, false); Toggle_Show_Head = false; }
            else { Toggle_Button(Switch_Button_Show_Head, "Button_On", "Button_Off", 0, true); Toggle_Show_Head = true; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Switch_Button_God_Mode_Click(object sender, EventArgs e)
        {
            if (Toggle_God_Mode) 
            { 
                Toggle_Button(Switch_Button_God_Mode, "Button_On", "Button_Off", 0, false); Toggle_God_Mode = false;
                Process_Tag_Value(Progress_Bar_Shield, Text_Box_Shield_Rate, Maximum_Shield, Maximum_Shield_Rate);
                Process_Balancing_Percentage(); 
            }
            else 
            {   Toggle_Button(Switch_Button_God_Mode, "Button_On", "Button_Off", 0, true); Toggle_God_Mode = true;
                User_Input = false; // Otherwise it won't allow to write non numbers
                Text_Box_Shield_Rate.Text = "God";
                User_Input = true;           
            }
        }


        private void Switch_Button_Use_Particle_Click(object sender, EventArgs e)
        {                   
            if (Toggle_Use_Particle) 
            {   Toggle_Button(Switch_Button_Use_Particle, "Button_On", "Button_Off", 0, false); Toggle_Use_Particle = false;
                Text_Box_Lua_Script.Focus();

                if (Text_Box_Lua_Script.Text == "ObjectScript_Hyper_Space") { Text_Box_Lua_Script.Text = ""; }
            }
            else
            {
                if (Allowed_Patching == "true")
                {   // Making sure the models are in the Selected Mod
                    foreach (string The_File in Directory.GetFiles(Program_Directory + @"\Misc\Art\Models"))
                    { Verify_Copy(The_File, Art_Directory + @"Models\" + Path.GetFileName(The_File)); }


                    if (!Directory.Exists(Data_Directory + @"Scripts\GameObject")) { Directory.CreateDirectory(Data_Directory + @"Scripts\GameObject"); }

                    string Hyperspace_Script = Data_Directory + @"Scripts\GameObject\ObjectScript_Hyper_Space.lua";

                    // If no Hyperspace Script file exists, we copy it
                    if (!File.Exists(Hyperspace_Script))
                    {
                        byte[] Dummy_Resource = Imperialware.Properties.Resources.ObjectScript_Hyper_Space;
                        File.WriteAllBytes(Hyperspace_Script, Dummy_Resource);
                    }

                    // As the code for all Hyperspace Particles is inside of the Cheating Dummy, we need to make sure it exists
                    Check_Cheat_Dummy();
                }
                else
                {
                    Temporal_A = "false";

                    foreach (string The_File in Directory.GetFiles(Program_Directory + @"\Misc\Art\Models"))
                    { if (!File.Exists(Art_Directory + @"Models\" + Path.GetFileName(The_File))) { Temporal_A = "true"; } }


                    if (Temporal_A == "true")
                    {
                        Imperial_Dialogue(500, 160, "Yes", "No", "false", Add_Line + "    May Imperialware copy Hyperspace Particles"
                                                                       + Add_Line + "    into your Mod Directory?.");

                        if (Caution_Window.Passed_Value_A.Text_Data == "true")
                        {
                            foreach (string The_File in Directory.GetFiles(Program_Directory + @"\Misc\Art\Models"))
                            { Verify_Copy(The_File, Art_Directory + @"Models\" + Path.GetFileName(The_File)); }

                            // Setting back to false to prevent missfiring when user closes the verification dialogue next time.
                            Caution_Window.Passed_Value_A.Text_Data = "false";
                        } else { return; }
                    }                   
                }



                if (Size_Expander_J == "false") { Button_Expand_J_Click(null, null); }
                Text_Box_Lua_Script.Focus();

                if (Text_Box_Lua_Script.Text == "")
                {   // Giving the User a opportunity to see what changes: System.Threading.Thread.Sleep(2000);
                    Text_Box_Lua_Script.Text = "ObjectScript_Hyper_Space";
                    Toggle_Button(Switch_Button_Use_Particle, "Button_On", "Button_Off", 0, true); Toggle_Use_Particle = true;
                }
                else if (Text_Box_Lua_Script.Text != "ObjectScript_Hyper_Space")
                {
                    Imperial_Dialogue(500, 160, "Yes", "No", "false", Add_Line + "    Do you wish to overwrite the Lua_Script tag"
                                                                    + Add_Line + "    with the Hyperspace particle script?.");

                    if (Caution_Window.Passed_Value_A.Text_Data == "true")
                    {   Text_Box_Lua_Script.Text = "ObjectScript_Hyper_Space";
                        Toggle_Button(Switch_Button_Use_Particle, "Button_On", "Button_Off", 0, true); Toggle_Use_Particle = true;
                        
                        // Setting back to false to prevent missfiring when user closes the verification dialogue next time.
                        Caution_Window.Passed_Value_A.Text_Data = "false";
                    }
                }
            }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }



        private void Check_Box_Has_Hyperspace_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_Box_Has_Hyperspace.Checked)
            {
                Text_Box_Hyperspace_Speed.Visible = true;
                Picture_Box_Has_Hyperspace.Visible = false;

                Track_Bar_Hyperspace_Speed.Visible = true;
                Check_Box_Has_Hyperspace.ForeColor = Color_01;
            }
            else
            {
                Text_Box_Hyperspace_Speed.Text = "";
                Text_Box_Hyperspace_Speed.Visible = false;
                Picture_Box_Has_Hyperspace.Visible = true;

                Progress_Bar_Hyperspace_Speed.Value = 0;
                Track_Bar_Hyperspace_Speed.Visible = false;
                Check_Box_Has_Hyperspace.ForeColor = Color_02;
            }
        }

        private void Track_Bar_Hyperspace_Speed_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Decimal(Track_Bar_Hyperspace_Speed, Progress_Bar_Hyperspace_Speed, Text_Box_Hyperspace_Speed, 2); }

        private void Text_Box_Hyperspace_Speed_TextChanged(object sender, EventArgs e)
        { Set_From_Float_Text_Box(Text_Box_Hyperspace_Speed, Track_Bar_Hyperspace_Speed, Progress_Bar_Hyperspace_Speed, 2, 10, true); }



        private void Label_Starting_Spawned_Units_Click(object sender, EventArgs e)
        { Text_Box_Starting_Unit_Name.Text = Clipboard.GetText(); }

        private void Text_Box_Starting_Unit_Name_TextChanged(object sender, EventArgs e)
        {   // Needed to know whether to pop the "are you sure you want to exit" menu when exiting without save
            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Track_Bar_Spawned_Unit_Scroll(object sender, EventArgs e)
        {
            Track_Bar_Spawned_Unit.Maximum = 20;
            Text_Box_Spawned_Unit.Text = Track_Bar_Spawned_Unit.Value.ToString();
        }

        private void Text_Box_Spawned_Unit_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Spawned_Unit, 20, false);

            // If the value is higher then Maximum we make sure the Bars are full
            if (Typed_Value > 20) { Track_Bar_Spawned_Unit.Value = Track_Bar_Spawned_Unit.Maximum; }
            else
            {
                try
                {
                    Track_Bar_Spawned_Unit.Maximum = 20;
                    Track_Bar_Spawned_Unit.Value = Typed_Value;
                }
                catch { }
            }

        }


        private void Label_Reserve_Spawned_Units_Click(object sender, EventArgs e)
        { Text_Box_Reserve_Unit_Name.Text = Clipboard.GetText(); }

        private void Text_Box_Reserve_Unit_Name_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }


        private void Track_Bar_Reserve_Unit_Scroll(object sender, EventArgs e)
        {
            Track_Bar_Reserve_Unit.Maximum = 20;
            Text_Box_Reserve_Unit.Text = Track_Bar_Reserve_Unit.Value.ToString();
        }

        private void Text_Box_Reserve_Unit_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Reserve_Unit, 20, false);

            // If the value is higher then Maximum we make sure the Bars are full
            if (Typed_Value > 20) { Track_Bar_Reserve_Unit.Value = Track_Bar_Reserve_Unit.Maximum; }
            else
            {
                try
                {
                    Track_Bar_Reserve_Unit.Maximum = 20;
                    Track_Bar_Reserve_Unit.Value = Typed_Value;
                }
                catch { }
            }
        }


        private void Label_Death_Clone_Click(object sender, EventArgs e)
        { Text_Box_Death_Clone.Text = Clipboard.GetText(); }

        private void Text_Box_Death_Clone_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }



        private void Label_Death_Clone_Model_Click(object sender, EventArgs e)
        { Text_Box_Death_Clone_Model.Text = Clipboard.GetText(); }

        private void Text_Box_Death_Clone_Model_TextChanged(object sender, EventArgs e)
        { if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } }


        private void Label_Death_Clone_Alo_Click(object sender, EventArgs e)
        {   try
            {   string The_File = Art_Directory + @"Models\" + Text_Box_Death_Clone_Model.Text + ".alo";
                System.Diagnostics.Process.Start(The_File);
            }
            catch
            {   if (Text_Box_Death_Clone_Model.Text == "") { Imperial_Console(540, 100, Add_Line + "Please add any modelname into the model textbox."); }
                else
                {
                    Imperial_Console(540, 100, Add_Line + "Error; Maybe you need to assign .alo files"
                                              + Add_Line + "to the Alo Viewer tool in"
                                              + Add_Line + @".\Imperialware_Directory\Misc\Modding_Tools");
                }
            }
        }


        private void Button_Death_Clone_Model_Click(object sender, EventArgs e)
        {   try
            {   // If the Open Dialog found a File
                if (Open_File_Dialog_1.ShowDialog() == DialogResult.OK)
                {   // We are going to set that file to our Textbox
                    Text_Box_Death_Clone_Model.Text = Path.GetFileNameWithoutExtension(Open_File_Dialog_1.FileName);
                }
            } catch { }
        }



        private void Lable_Lua_Script_Click(object sender, EventArgs e)
        { Text_Box_Lua_Script.Text = Clipboard.GetText(); }

        private void Text_Box_Lua_Script_TextChanged(object sender, EventArgs e)
        {
            if (Text_Box_Lua_Script.Text != "ObjectScript_Hyper_Space" & Toggle_Use_Particle)
            { Toggle_Button(Switch_Button_Use_Particle, "Button_On", "Button_Off", 0, false); Toggle_Use_Particle = false; }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; } 
        }

      
        // Two Fields for Behavoir
        private void Button_Move_to_Active_Click(object sender, EventArgs e)
        { // Position of these 2 parameters (left or right side) decides in which direction they move!
          Switch_Value_Activity(List_Box_Inactive_Behavoir, List_Box_Active_Behavoir);
          
          if (List_Box_Inactive_Behavoir.Items.Count == 0)
          {   Imperial_Console(600, 100, Add_Line + "    Inactive Behavoir is empty."
                                       + Add_Line + "    You need to select a Unit Type in the Combobox above."); }
        }

        private void Button_Behavoir_Exchange_Click(object sender, EventArgs e)
        { Exchange_Table_Values(List_Box_Active_Behavoir, List_Box_Inactive_Behavoir); }

        private void Button_Move_to_Inactive_Click(object sender, EventArgs e)
        {  Switch_Value_Activity(List_Box_Active_Behavoir, List_Box_Inactive_Behavoir); }

        // ===================== Requirement Tags ===================== //

        // Two Fields for Affiliation
        private void Button_Affiliation_to_Active_Click(object sender, EventArgs e)
        { Switch_Value_Activity(List_Box_Inactive_Affiliations, List_Box_Active_Affiliations); }

        private void Button_Affiliation_Exchange_Click(object sender, EventArgs e)
        { Exchange_Table_Values(List_Box_Active_Affiliations, List_Box_Inactive_Affiliations); }

        private void Button_Affiliation_to_Inactive_Click(object sender, EventArgs e)
        { Switch_Value_Activity(List_Box_Active_Affiliations, List_Box_Inactive_Affiliations); }

        private void Check_Box_Add_To_Base_CheckedChanged(object sender, EventArgs e)
        {
            if (User_Input)
            {
                User_Input = false; 

                if (Check_Box_Add_To_Base.Checked) { Check_Box_Add_To_Skirmish.Checked = true; }

                else
                {
                    Check_Box_Add_To_Skirmish.Checked = false;
                    Check_Box_Add_To_Campaign.Checked = false;
                }
                User_Input = true;
            }
        }

        private void Check_Box_Add_To_Skirmish_CheckedChanged(object sender, EventArgs e)
        {   
            if (User_Input)
            {   User_Input = false; 

                if (Check_Box_Add_To_Base.Checked) 
                {
                    if (!Check_Box_Add_To_Skirmish.Checked & !Check_Box_Add_To_Campaign.Checked) { Check_Box_Add_To_Base.Checked = false; }                    
                }
                else { Check_Box_Add_To_Base.Checked = true; }

               User_Input = true; 
            }
        }

        private void Check_Box_Add_To_Campaign_CheckedChanged(object sender, EventArgs e)
        {
            Check_Box_Add_To_Skirmish_CheckedChanged(null, null);
        }
     

        private void Switch_Button_Enable_All_Click(object sender, EventArgs e)
        {
            if (Toggle_Enable_All == false)
            {   // Toggeling Switch
                Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, true);                            
                Toggle_Enable_All = true;

                // Closing UI subtab
                if (Size_Expander_H == "true") { Button_Expand_H_Click(null, null); }
           
                // Making sure Build Tab is enabled
                if (Toggle_Build_Tab == false) { Toggle_Button(Switch_Button_Build_Tab, "Button_On", "Button_Off", 0, true); Toggle_Build_Tab = true; }

                // Making sure 75% of the Price and Buildtime for this class is available
                if (Text_Box_Build_Cost.Text == "") { Text_Box_Build_Cost.Text = ((Maximum_Build_Cost / 4) * 3).ToString(); }
                if (Text_Box_Skirmish_Cost.Text == "") { Scroll_Xml_Value(Track_Bar_Build_Cost, Progress_Bar_Skirmish_Cost, Text_Box_Skirmish_Cost, Maximum_Skirmish_Cost, 100); }
                if (Text_Box_Build_Time.Text == "") { Process_Tag_Value(Progress_Bar_Build_Cost, Text_Box_Build_Time, Maximum_Build_Cost, Maximum_Build_Time); }

                // Settnig Requirements to 0
                Text_Box_Tech_Level.Text = "0";
                Text_Box_Star_Base_LV.Text = "0";
                Text_Box_Ground_Base_LV.Text = "0";
                Text_Box_Required_Timeline.Text = "0";

                if (Toggle_Innitially_Locked == true) { Toggle_Button(Switch_Button_Innitially_Locked, "Button_On", "Button_Off", 0, false); Toggle_Innitially_Locked = false; }
                if (Check_Box_Slice_Cost.Checked) { Check_Box_Slice_Cost.Checked = false; }
                if (Check_Box_Current_Limit.Checked) { Check_Box_Current_Limit.Checked = false; }
                if (Check_Box_Lifetime_Limit.Checked) { Check_Box_Lifetime_Limit.Checked = false; }
            }

            else { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false); Toggle_Enable_All = false; }
        }


        private void Switch_Button_Build_Tab_Click(object sender, EventArgs e)
        {
            if (Toggle_Build_Tab == false) { Toggle_Button(Switch_Button_Build_Tab, "Button_On", "Button_Off", 0, true); Toggle_Build_Tab = true; }
            else
            {   Toggle_Button(Switch_Button_Build_Tab, "Button_On", "Button_Off", 0, false);
                Toggle_Build_Tab = false; 

                // Not longer the case for all to be enabled, so the button turns off and acts as indicator
                if (Toggle_Enable_All == true) { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false); Toggle_Enable_All = false; }    
            }

            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
        }


        private void Check_Box_Slice_Cost_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_Box_Slice_Cost.Checked)
            {
                Text_Box_Slice_Cost.Visible = true;
                Picture_Box_Slice_Cost.Visible = false;

                Track_Bar_Slice_Cost.Visible = true;
                Check_Box_Slice_Cost.ForeColor = Color_01;
                if (Toggle_Enable_All == true) { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, true); Toggle_Enable_All = false; }
            }
            else
            {
                Text_Box_Slice_Cost.Text = "";
                Text_Box_Slice_Cost.Visible = false;
                Picture_Box_Slice_Cost.Visible = true;

                Track_Bar_Slice_Cost.Visible = false;
                Check_Box_Slice_Cost.ForeColor = Color_02;
            }        
        }

        private void Track_Bar_Slice_Cost_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Slice_Cost, Progress_Bar_Slice_Cost, Text_Box_Slice_Cost, Maximum_Slice_Cost, 100); }

        private void Text_Box_Slice_Cost_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Slice_Cost, Maximum_Slice_Cost, false);
            Text_Box_Text_Changed(Track_Bar_Slice_Cost, Progress_Bar_Slice_Cost, Maximum_Slice_Cost, Typed_Value, 100);
        }



        private void Track_Bar_Build_Cost_Scroll(object sender, EventArgs e)
        {   Scroll_Xml_Value(Track_Bar_Build_Cost, Progress_Bar_Build_Cost, Text_Box_Build_Cost, Maximum_Build_Cost, 100);

            if (Auto_Apply == "true" & User_Input)
            {   Scroll_Xml_Value(Track_Bar_Build_Cost, Progress_Bar_Skirmish_Cost, Text_Box_Skirmish_Cost, Maximum_Build_Cost, 100);
                // This automatically sets a value for the second Textbox of the similar tag 
                Process_Tag_Value(Progress_Bar_Build_Cost, Text_Box_Build_Time, Maximum_Build_Cost, Maximum_Build_Time);
            }
        }

        private void Text_Box_Build_Cost_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Build_Cost, Maximum_Build_Cost, false);
            Text_Box_Text_Changed(Track_Bar_Build_Cost, Progress_Bar_Build_Cost, Maximum_Build_Cost, Typed_Value, 100);
        }


        private void Track_Bar_Skirmish_Cost_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Skirmish_Cost, Progress_Bar_Skirmish_Cost, Text_Box_Skirmish_Cost, Maximum_Skirmish_Cost, 100); }

        private void Text_Box_Skirmish_Cost_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Skirmish_Cost, Maximum_Skirmish_Cost, false);
            Text_Box_Text_Changed(Track_Bar_Skirmish_Cost, Progress_Bar_Skirmish_Cost, Maximum_Skirmish_Cost, Typed_Value, 100);
        }


        private void Track_Bar_Build_Time_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Build_Time, Progress_Bar_Build_Time, Text_Box_Build_Time, Maximum_Build_Time, 1); }

        private void Text_Box_Build_Time_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Build_Time, Maximum_Build_Time, false);
            Text_Box_Text_Changed(Track_Bar_Build_Time, Progress_Bar_Build_Time, Maximum_Build_Time, Typed_Value, 1);
        }



        private void Check_Box_Current_Limit_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_Box_Current_Limit.Checked)
            {
                Text_Box_Current_Limit.Visible = true;
                Picture_Box_Current_Limit.Visible = false;

                Track_Bar_Current_Limit.Visible = true;
                Check_Box_Current_Limit.ForeColor = Color_01;
                if (Toggle_Enable_All == true) { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false); Toggle_Enable_All = false; }
            }
            else
            {   // -1 Because that means deactivated in the EAW Game!
                Text_Box_Current_Limit.Text = "-1";
                Text_Box_Current_Limit.Visible = false;
                Picture_Box_Current_Limit.Visible = true;

                Track_Bar_Current_Limit.Visible = false;
                Check_Box_Current_Limit.ForeColor = Color_02;
            }
        }

        private void Track_Bar_Current_Limit_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Current_Limit, Progress_Bar_Current_Limit, Text_Box_Current_Limit, Maximum_Build_Limit, 1); }

        private void Text_Box_Current_Limit_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Current_Limit, Maximum_Build_Limit, false);
            Text_Box_Text_Changed(Track_Bar_Current_Limit, Progress_Bar_Current_Limit, Maximum_Build_Limit, Typed_Value, 1);
        }



        private void Check_Box_Lifetime_Limit_CheckedChanged(object sender, EventArgs e)
        {
            if (Check_Box_Lifetime_Limit.Checked)
            {
                Text_Box_Lifetime_Limit.Visible = true;
                Picture_Box_Lifetime_Limit.Visible = false;

                Track_Bar_Lifetime_Limit.Visible = true;
                Check_Box_Lifetime_Limit.ForeColor = Color_01;
                if (Toggle_Enable_All == true) { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false); Toggle_Enable_All = false; }
            }
            else
            {   // -1 Because that means deactivated in the EAW Game!
                Text_Box_Lifetime_Limit.Text = "-1";
                Text_Box_Lifetime_Limit.Visible = false;
                Picture_Box_Lifetime_Limit.Visible = true;

                Track_Bar_Lifetime_Limit.Visible = false;
                Check_Box_Lifetime_Limit.ForeColor = Color_02;
            }
        }

        private void Track_Bar_Lifetime_Limit_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Lifetime_Limit, Progress_Bar_Lifetime_Limit, Text_Box_Lifetime_Limit, Maximum_Lifetime_Limit, 1); }

        private void Text_Box_Lifetime_Limit_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Lifetime_Limit, Maximum_Lifetime_Limit, false);
            Text_Box_Text_Changed(Track_Bar_Lifetime_Limit, Progress_Bar_Lifetime_Limit, Maximum_Lifetime_Limit, Typed_Value, 1);
        }


        private void Track_Bar_Tech_Level_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Tech_Level, Progress_Bar_Tech_Level, Text_Box_Tech_Level, Maximum_Tech_Level, 1); }

        private void Text_Box_Tech_Level_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Tech_Level, Maximum_Tech_Level, false);
            Text_Box_Text_Changed(Track_Bar_Tech_Level, Progress_Bar_Tech_Level, Maximum_Tech_Level, Typed_Value, 1);
            if (Toggle_Enable_All == true & Text_Box_Tech_Level.Text != "") { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false); Toggle_Enable_All = false; }
        }


        private void Track_Bar_Star_Base_LV_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Star_Base_LV, Progress_Bar_Star_Base_LV, Text_Box_Star_Base_LV, Maximum_Star_Base_LV, 1); }

        private void Text_Box_Star_Base_LV_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Star_Base_LV, Maximum_Star_Base_LV, false);
            Text_Box_Text_Changed(Track_Bar_Star_Base_LV, Progress_Bar_Star_Base_LV, Maximum_Star_Base_LV, Typed_Value, 1);
            if (Toggle_Enable_All == true & Text_Box_Star_Base_LV.Text != "") { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false); Toggle_Enable_All = false; }
        }


        private void Track_Bar_Ground_Base_LV_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Ground_Base_LV, Progress_Bar_Ground_Base_LV, Text_Box_Ground_Base_LV, Maximum_Ground_Base_LV, 1); }

        private void Text_Box_Ground_Base_LV_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Ground_Base_LV, Maximum_Ground_Base_LV, false);
            Text_Box_Text_Changed(Track_Bar_Ground_Base_LV, Progress_Bar_Ground_Base_LV, Maximum_Ground_Base_LV, Typed_Value, 1);
            if (Toggle_Enable_All == true & Text_Box_Ground_Base_LV.Text != "") { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false); Toggle_Enable_All = false; }
        }


        private void Track_Bar_Required_Timeline_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Required_Timeline, Progress_Bar_Required_Timeline, Text_Box_Required_Timeline, Maximum_Timeline, 1); }

        private void Text_Box_Required_Timeline_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Required_Timeline, Maximum_Timeline, false);
            Text_Box_Text_Changed(Track_Bar_Required_Timeline, Progress_Bar_Required_Timeline, Maximum_Timeline, Typed_Value, 1);
            if (Toggle_Enable_All == true & Text_Box_Required_Timeline.Text != "") { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false); Toggle_Enable_All = false; }
        }


        private void Switch_Button_Innitially_Locked_Click(object sender, EventArgs e)
        {
            if (Toggle_Innitially_Locked == false) 
            {   Toggle_Button(Switch_Button_Innitially_Locked, "Button_On", "Button_Off", 0, true);
                Toggle_Innitially_Locked = true;
                if (Toggle_Enable_All == true) { Toggle_Button(Switch_Button_Enable_All, "Button_On", "Button_Off", 0, false); Toggle_Enable_All = false; }
            }
            else { Toggle_Button(Switch_Button_Innitially_Locked, "Button_On", "Button_Off", 0, false); Toggle_Innitially_Locked = false; }         
        }


        private void Button_Required_Planets_Click(object sender, EventArgs e)
        {
            if (Switch_Required_Object != 1)
            {
                Render_Segment_Button(Button_Required_Planets, "Left", true, 85, 36, 13, 8, 12, Color_03, "Planets");
                Render_Segment_Button(Button_Required_Structures, "Right", false, 85, 36, 9, 8, 10, Color_04, "Structures");
                Switch_Required_Object = 1;
            }
        }

        private void Button_Required_Structures_Click(object sender, EventArgs e)
        {
            if (Switch_Required_Object != 2)
            {
                Render_Segment_Button(Button_Required_Planets, "Left", false, 85, 36, 20, 9, 10, Color_04, "Planets");
                Render_Segment_Button(Button_Required_Structures, "Right", true, 85, 36, 1, 8, 12, Color_03, "Structures");
                Switch_Required_Object = 2;
            }
        }


        // ===================== Costum Tags ===================== //
        private void Button_Save_Names_Click(object sender, EventArgs e)
        {
            Save_Setting(Setting, "Costum_Tag_1_Name", Text_Box_Tag_1_Name.Text);
            Save_Setting(Setting, "Costum_Tag_2_Name", Text_Box_Tag_2_Name.Text);
            Save_Setting(Setting, "Costum_Tag_3_Name", Text_Box_Tag_3_Name.Text);
            Save_Setting(Setting, "Costum_Tag_4_Name", Text_Box_Tag_4_Name.Text);
            Save_Setting(Setting, "Costum_Tag_5_Name", Text_Box_Tag_5_Name.Text);
            Save_Setting(Setting, "Costum_Tag_6_Name", Text_Box_Tag_6_Name.Text);

            Color_01 = Load_Color(Setting, "Color_01");

            // Changing Tag Colors and names of Max Values
            if (Text_Box_Tag_1_Name.Text != "<Tag_1_Name>") { Text_Box_Tag_1_Name.ForeColor = Color_01; }
            if (Text_Box_Tag_2_Name.Text != "<Tag_2_Name>") { Text_Box_Tag_2_Name.ForeColor = Color_01; }
            if (Text_Box_Tag_3_Name.Text != "<Tag_3_Name>") { Text_Box_Tag_3_Name.ForeColor = Color_01; }
            if (Text_Box_Tag_4_Name.Text != "<Tag_4_Name>") { Text_Box_Tag_4_Name.ForeColor = Color_01; Label_Maximum_Costum_4.Text = Text_Box_Tag_4_Name.Text;}
            if (Text_Box_Tag_5_Name.Text != "<Tag_5_Name>") { Text_Box_Tag_5_Name.ForeColor = Color_01; Label_Maximum_Costum_5.Text = Text_Box_Tag_5_Name.Text;}
            if (Text_Box_Tag_6_Name.Text != "<Tag_6_Name>") { Text_Box_Tag_6_Name.ForeColor = Color_01; Label_Maximum_Costum_6.Text = Text_Box_Tag_6_Name.Text;}        
        }

        private void Button_Reset_Tag_Names_Click(object sender, EventArgs e)
        {   // Resetting the Variables in Settings.txt
            Save_Setting(Setting, "Costum_Tag_1_Name", "<Tag_1_Name>");
            Save_Setting(Setting, "Costum_Tag_2_Name", "<Tag_2_Name>");
            Save_Setting(Setting, "Costum_Tag_3_Name", "<Tag_3_Name>");
            Save_Setting(Setting, "Costum_Tag_4_Name", "<Tag_4_Name>");
            Save_Setting(Setting, "Costum_Tag_5_Name", "<Tag_5_Name>");
            Save_Setting(Setting, "Costum_Tag_6_Name", "<Tag_6_Name>");

            // Resetting Tag Colors
            Text_Box_Tag_1_Name.ForeColor = Color_01;
            Text_Box_Tag_2_Name.ForeColor = Color_01;
            Text_Box_Tag_3_Name.ForeColor = Color_01;
            Text_Box_Tag_4_Name.ForeColor = Color_01;
            Text_Box_Tag_5_Name.ForeColor = Color_01;
            Text_Box_Tag_6_Name.ForeColor = Color_01;


            // Resetting the display Text
            Text_Box_Tag_1_Name.Text = Costum_Tag_1_Name; 
            Text_Box_Tag_2_Name.Text = Costum_Tag_2_Name;
            Text_Box_Tag_3_Name.Text = Costum_Tag_3_Name;
            Text_Box_Tag_4_Name.Text = Costum_Tag_4_Name;
            Text_Box_Tag_5_Name.Text = Costum_Tag_5_Name;
            Text_Box_Tag_6_Name.Text = Costum_Tag_6_Name;

            // Clearing all Values
            Text_Box_Costum_Tag_1.Text = "";
            Text_Box_Costum_Tag_2.Text = "";
            Text_Box_Costum_Tag_3.Text = "";
            Text_Box_Costum_Tag_4.Text = "";
            Text_Box_Costum_Tag_5.Text = "";
            Text_Box_Costum_Tag_6.Text = "";
        }


        private void Track_Bar_Costum_Tag_4_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Costum_Tag_4, Progress_Bar_Costum_Tag_4, Text_Box_Costum_Tag_4, Costum_Tag_4_Max_Value, 100); }


        private void Text_Box_Costum_Tag_4_TextChanged(object sender, EventArgs e)
        {
            // We use the Name of this Control Element and the Value we just parsed as Max value for this function
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Costum_Tag_4, Costum_Tag_4_Max_Value, false);
            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }       
            Text_Box_Text_Changed(Track_Bar_Costum_Tag_4, Progress_Bar_Costum_Tag_4, Costum_Tag_4_Max_Value, Typed_Value, 100);
        }


        private void Track_Bar_Costum_Tag_5_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Costum_Tag_5, Progress_Bar_Costum_Tag_5, Text_Box_Costum_Tag_5, Costum_Tag_5_Max_Value, 100); }

        private void Text_Box_Costum_Tag_5_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Costum_Tag_5, Costum_Tag_5_Max_Value, false);
            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
            Text_Box_Text_Changed(Track_Bar_Costum_Tag_5, Progress_Bar_Costum_Tag_5, Costum_Tag_5_Max_Value, Typed_Value, 100);
        }


        private void Track_Bar_Costum_Tag_6_Scroll(object sender, EventArgs e)
        { Scroll_Xml_Value(Track_Bar_Costum_Tag_6, Progress_Bar_Costum_Tag_6, Text_Box_Costum_Tag_6, Costum_Tag_6_Max_Value, 100); }

        private void Text_Box_Costum_Tag_6_TextChanged(object sender, EventArgs e)
        {
            int Typed_Value = Check_Numeral_Text_Box(Text_Box_Costum_Tag_6, Costum_Tag_6_Max_Value, false);
            if (User_Input & Edited_Selected_Unit == false) { Edited_Selected_Unit = true; }
            Text_Box_Text_Changed(Track_Bar_Costum_Tag_6, Progress_Bar_Costum_Tag_6, Costum_Tag_6_Max_Value, Typed_Value, 100);
        }


        
        private void Check_Box_Game_Object_Files_CheckedChanged(object sender, EventArgs e)
        {
            if (User_Input == true)
            {
                if (!Check_Box_Game_Object_Files.Checked)
                {
                    Check_Box_Game_Object_Files.ForeColor = Color_02;
                    Add_To_Game_Object_Files = "false";
                }
                else
                {
                    Check_Box_Game_Object_Files.ForeColor = Color_03;
                    Add_To_Game_Object_Files = "true";
                }
            }
        }

        private void Check_Box_Hard_Point_Data_CheckedChanged(object sender, EventArgs e)
        {
            if (User_Input == true)
            {
                if (!Check_Box_Hard_Point_Data.Checked)
                {
                    Check_Box_Hard_Point_Data.ForeColor = Color_02;
                    Add_To_Box_Hard_Point_Data = "false";
                }
                else
                {
                    Check_Box_Hard_Point_Data.ForeColor = Color_03;
                    Add_To_Box_Hard_Point_Data = "true";
                }
            }
        }


        //================================================================================================================== 
        //                                                SAVING 
        //================================================================================================================== 
        private void Save_Click(object sender, EventArgs e)
        {

            // Needed for the .xml Editor Functions
            string Unit_Type = Combo_Box_Type.Text;
            string Unit_Name = Text_Box_Name.Text;



           try { 
        
            //======================= Loading .Xml File =======================
            XDocument Xml_File = XDocument.Load(Selected_Xml, LoadOptions.PreserveWhitespace);


            // Getting the currently selected Unit
            // var Selected_Unit = List_Box_All_Instances.GetItemText(List_Box_All_Instances.SelectedItem).ToString();
            var Selected_Unit = Selected_Instance;


            Instance =
               // Selecting all child Tags of Root   
               from All_Tags in Xml_File.Root.Descendants()
               // Selecting our Unit
               where (string)All_Tags.Attribute("Name") == Selected_Unit
               select All_Tags;



            try
            {   // Unit Type - Choose between: SpaceUnit, UniqueUnit, TransportUnit, HeroUnit, HeroCompany, GroundCompany, GroundInfantry, GroundVehicle
                if (Combo_Box_Type.Text != "" & Combo_Box_Type.Text != Instance.First().Name)
                {   // Loading Tag Value from the Combo Box in order to change the Type of unit
                    Instance.First().Name = Combo_Box_Type.Text;

                    Save_Setting(Setting, "Unit_Type", Combo_Box_Type.Text);               
                }
            } catch { }

            
            try
            {
                if (Unit_Name != "" & Unit_Name != Instance.First().Attribute("Name").Value) // Need to make sure it won't trigger if empty, or it causes an exception
                {
                    // Setting Attribute Value, used to be "Xml_File.Descendants().Elements(Unit_Type).SingleOrDefault"          
                    var Element = Instance.SingleOrDefault(x => // If that value is whether the selected unit of one of the strings below
                                                                                                  x.Attribute("Name").Value == Selected_Unit
                                                                                                | x.Attribute("Name").Value == "Fighter_Template"
                                                                                                | x.Attribute("Name").Value == "Bomber_Template"
                                                                                          
                                                                                                | x.Attribute("Name").Value == "Corvette_Template"
                                                                                                | x.Attribute("Name").Value == "Frigate_Template"
                                                                                                | x.Attribute("Name").Value == "Capitalship_Template" 

                                                                                                | x.Attribute("Name").Value == "Infantry_Template"
                                                                                                | x.Attribute("Name").Value == "Vehicle_Template"
                                                                                                | x.Attribute("Name").Value == "Air_Template" 

                                                                                                | x.Attribute("Name").Value == "Hero_Template"
                                                                                                | x.Attribute("Name").Value == "Structure_Template"  );


                    // And set the Attribute Name to the content of the Textbox
                    Element.SetAttributeValue("Name", Text_Box_Name.Text);

                    if (Text_Box_Name.Text != "") { Save_Setting(Setting, "Selected_Instance", Text_Box_Name.Text); }
                }
            }
            catch { Imperial_Console(600, 100, Add_Line + "Failed to change he Unit Type."); }



            if (Check_Box_Is_Variant.Checked & Text_Box_Is_Variant.Text != "")
            { Validate_Tag_In_Xml(Xml_File, Selected_Instance, "Variant_Of_Existing_Type", Text_Box_Is_Variant.Text, false); }


            //======================= Art Settings ======================== 


            string Model_Environment = "";
            if (Unit_Mode == "Space" | Unit_Type == "StarBase" | Unit_Type == "SpaceBuildable" | Unit_Type == "SpecialStructure")
            { Model_Environment = "Space_Model_Name"; }
            else if (Unit_Mode == "Ground" | Unit_Mode == "Structure") { Model_Environment = "Land_Model_Name"; }

         
            Verify_Tag_with_Extension(null, Model_Environment, Text_Box_Model_Name, ".alo");
           
            if (!Is_Team)
            {   Verify_Tag_with_Extension(null, "Icon_Name", Text_Box_Icon_Name, ".tga");
                Verify_Tag_with_Extension(null, "Radar_Icon_Name", Text_Box_Radar_Icon, ".tga");
                Validate_Save_Tag(Text_Box_Radar_Size, "Radar_Icon_Size");         
            }
            
            Validate_Save_Tag(Text_Box_Text_Id, "Text_ID");
            Validate_Save_Tag(Text_Box_Unit_Class, "Encyclopedia_Unit_Class");
            Validate_Save_Tag(Text_Box_Encyclopedia_Text, "Encyclopedia_Text");           
           



            // Scrollable Art Values
            Validate_Save_Tag(Text_Box_Scale_Factor, "Scale_Factor");

            if (Toggle_Operator_Model_Height == false) // false means the operator is - and not +
            { Validate_Save_Text("Layer_Z_Adjust", "-" + Text_Box_Model_Height.Text); }
            else // Otherwise we validate save it normally as + value
            { Validate_Save_Tag(Text_Box_Model_Height, "Layer_Z_Adjust"); }
          


            if (!Check_Box_Health_Bar_Size.Checked)
            {   // Activating to hide the Health + Shield Bars
                Validate_Save_Text("GUI_Hide_Health_Bar", "Yes");
            } 
            else 
            {   // Making sure the Health Bar getting shown again
                Validate_Save_Text("GUI_Hide_Health_Bar", "No"); 


                // Applying size of Health Bar (0-2)              
                if (Combo_Box_Health_Bar_Size.Text == "Small")
                { Validate_Save_Text("GUI_Bracket_Size", "0"); }

                else if (Combo_Box_Health_Bar_Size.Text == "Medium")
                { Validate_Save_Text("GUI_Bracket_Size", "1"); }

                else if (Combo_Box_Health_Bar_Size.Text == "Large")
                { Validate_Save_Text("GUI_Bracket_Size", "2"); }
            }       

            Validate_Save_Tag(Text_Box_Health_Bar_Height, "GUI_Bounds_Scale");

            Validate_Save_Tag(Text_Box_Select_Box_Scale, "Select_Box_Scale");


            if (Toggle_Select_Box_Height == false) // false means the operator is - and not +
            { Validate_Save_Text("Select_Box_Z_Adjust", "-" + Text_Box_Select_Box_Height.Text); }
            else // Otherwise we validate save it normally as + value
            { Validate_Save_Tag(Text_Box_Select_Box_Height, "Select_Box_Z_Adjust"); }


         
                
            //======================= Power Values =======================

            if (Unit_Mode == "Space")
            {   Validate_Save_Text("MovementClass", "Space"); }
            else if (Unit_Mode == "Ground")
            {   Validate_Save_Text("MovementClass", "Tracked");
                Validate_Save_Text("Space_Layer", "Land");           
            } 


            // Ship_Class
            if (Combo_Box_Class.Text != "") 
            {   // If the User changes the Class, in the UI we are going to replace that value with the text in the Combo Box                
                Validate_Save_Tag(Combo_Box_Class, "Ship_Class");    
                if (Unit_Mode != "Ground") { Validate_Save_Tag(Combo_Box_Class, "Space_Layer"); }
                Validate_Save_Tag(Combo_Box_Class, "CategoryMask");           
             }
            

            Validate_Save_Tag(Text_Box_Hull, "Tactical_Health");

            Validate_Save_Tag(Text_Box_Shield, "Shield_Points");


            if (Toggle_God_Mode)
            {   try
                {   Temporal_A = Load_Setting(Setting, "GMU");
                    string[] God_Mode_Unit = Temporal_A.Split(',');
                    Temporal_B = God_Mode_Unit[1].Replace(" ", "");
                   
                    if (Temporal_B != Text_Box_Name.Text) 
                    {   // This function sets Shield refresh rate of old Unit with God Mode to 75% of its class, as only 1 unit is supposed to have God Mode at a time!
                        Remove_God_Mode(); 
                    }
                } catch { }
             
                Validate_Save_Text("Shield_Refresh_Rate", "100000");
                // Only if the user really saves the God mode this unit will be memorized
                Save_Setting("2", "GMU", @"""" + Selected_Xml + ", " + Text_Box_Name.Text + @"""");               
            }
            else 
            {
                // We need to overwrite Godmode Shield rate with 75% if the user specified no value:
                if (Text_Box_Shield_Rate.Text == "") { Text_Box_Shield_Rate.Text = ((Maximum_Shield_Rate / 4) * 3).ToString(); }
               
                Validate_Save_Tag(Text_Box_Shield_Rate, "Shield_Refresh_Rate"); 
                Remove_God_Mode();
                Save_Setting("2", "GMU", "");
            }            


            Validate_Save_Tag(Text_Box_Energy, "Energy_Capacity");

            Validate_Save_Tag(Text_Box_Energy_Rate, "Energy_Refresh_Rate");


            Validate_Save_Tag(Text_Box_Speed, "Max_Speed");

            // Rate of Roll is bounded to the Text_Box_Speed value
            decimal Rate_of_Roll = 0;
            decimal.TryParse(Text_Box_Speed.Text, out Rate_of_Roll);
            Rate_of_Roll = Rate_of_Roll / 10;


            if (Rate_of_Roll != 0) { Validate_Save_Text("Max_Rate_Of_Roll", Rate_of_Roll.ToString()); }
                 
            Validate_Save_Tag(Text_Box_Rate_Of_Turn, "Max_Rate_Of_Turn");

            Validate_Save_Tag(Text_Box_Bank_Turn_Angle, "Bank_Turn_Angle");


            if (!Is_Team) 
            { 
                Validate_Save_Tag(Text_Box_AI_Combat, "AI_Combat_Power");

                if (Toggle_Operator_Population)
                {
                    Validate_Save_Tag(Text_Box_Population, "Additional_Population_Capacity");

                    // If Operator is + we make sure the "Population_Value" (-) is inactive      
                    Overwrite_Save_Text("Population_Value", "");
                }
                else
                {
                    Validate_Save_Tag(Text_Box_Population, "Population_Value");

                    // If Operator is - we make sure the "Additional_Population_Capacity" (+) is inactive   
                    Overwrite_Save_Text("Additional_Population_Capacity", "");
                }
            }

            if (Text_Box_Projectile.Text != "") // If that tag exists
            {   try 
                {   if (Text_Box_Projectile.Text != Instance.Descendants("Projectile_Damage").First().Value)
                    { Instance.Descendants("Projectile_Damage").First().Value = Text_Box_Projectile.Text; }                                    
                } catch { }
            }


            Validate_Save_Tag(Text_Box_Lua_Script, "Lua_Script");


            //====== Saving Abilities ======
            Save_Abilities();
     
           
            //========================= Unit Properties ========================
            
            if (!Is_Team)
            {
                Save_Switch("Is_Named_Hero", "0", Toggle_Is_Hero);

                Save_Switch("Show_Hero_Head", "0", Toggle_Show_Head);
               

                // If checked we set that tag to Yes, otherwise its No
                if (Check_Box_Has_Hyperspace.Checked)
                {
                    Validate_Save_Text("Hyperspace", "Yes");
                     
                    Validate_Save_Tag(Text_Box_Hyperspace_Speed, "Hyperspace_Speed");
                } 
                else { Validate_Save_Text("Hyperspace", "No"); }
               

                try
                {   if (Text_Box_Starting_Unit_Name.Text != "" & Text_Box_Spawned_Unit.Text != "")
                    { Validate_Save_Text("Starting_Spawned_Units_Tech_0", Text_Box_Starting_Unit_Name.Text.Replace(" ", "") + ", " + Text_Box_Spawned_Unit.Text.Replace(" ", "")); }

                    else { Overwrite_Save_Text("Starting_Spawned_Units_Tech_0", ""); }
                } catch { }

                try
                {
                    if (Text_Box_Reserve_Unit_Name.Text != "" & Text_Box_Reserve_Unit.Text != "")
                    { Validate_Save_Text("Reserve_Spawned_Units_Tech_0", Text_Box_Reserve_Unit_Name.Text.Replace(" ", "") + ", " + Text_Box_Reserve_Unit.Text.Replace(" ", "")); }

                    else { Overwrite_Save_Text("Reserve_Spawned_Units_Tech_0", ""); }
                } catch { }          
            }

            Validate_Save_Tag(Text_Box_Death_Clone, "Death_Clone");


            if (Text_Box_Death_Clone.Text != "")
            {
                var The_Death_Clone =
                    // Selecting all child Tags of Root   
                    from All_Tags in Xml_File.Root.Descendants()
                    // Selecting our Unit
                    where (string)All_Tags.Attribute("Name") == Text_Box_Death_Clone.Text
                    select All_Tags;


                // Model_Environment points at wether Space_Model_Name or Land_Model_Name
                Verify_Tag_with_Extension(The_Death_Clone, Model_Environment, Text_Box_Death_Clone_Model, ".alo");

                Validate_Save_Unit_Tag(The_Death_Clone, "Scale_Factor", Text_Box_Scale_Factor.Text);

                Validate_Save_Unit_Tag(The_Death_Clone, "Layer_Z_Adjust", Text_Box_Model_Height.Text);

               //if (Text_Box_Death_Clone_Model.Text != "")
               // { The_Death_Clone.First().Add(new XElement(Model_Environment, Text_Box_Death_Clone_Model.Text)); }
            }
      

   
            
            
         

            // If there is any entry in the Behavoir List Box we add this tag
            if (List_Box_Active_Behavoir.Items.Count != 0)
            {
                string Behavoir_Pool = "";
                int Cycle_Count = 0;

                foreach (var Item in List_Box_Active_Behavoir.Items)
                {
                    Cycle_Count++;

                    if (Cycle_Count == List_Box_Active_Behavoir.Items.Count)
                    {   // This only triggers for the last item, which is not supposed to append the , sign
                        Behavoir_Pool += Item.ToString();
                    }
                    // Appending all Items to a single string
                    else { Behavoir_Pool += Item.ToString() + ", "; }
                }
              
                if (Behavoir_Pool != "") { Validate_Save_Text(Behavoir_Tag, Behavoir_Pool); }
                         
            }


            //======================= Build Requirements =======================
            if (!Is_Team)
            { // If there is any entry in the Affiliation List Box we add this tag
                if (List_Box_Active_Affiliations.Items.Count != 0)
                {
                    string Affiliation_Pool = "";
                    int Cycle_Count = 0;

                    foreach (var Item in List_Box_Active_Affiliations.Items)
                    {
                        Cycle_Count++;

                        if (Cycle_Count == List_Box_Active_Affiliations.Items.Count)
                        {   // This only triggers for the last item, which is not supposed to append the , sign
                            Affiliation_Pool += Item.ToString();
                        }
                        // Adding all Items to a single string
                        else { Affiliation_Pool += Item.ToString() + ", "; }
                    }

                    try
                    {
                        if (Affiliation_Pool != "") { Validate_Save_Text("Affiliation", Affiliation_Pool); }
                        else
                        {
                            Imperial_Dialogue(580, 160, "Save Anyway", "Cancel", "false", Add_Line + "    There is no active Affiliation."
                                                                        + Add_Line + "    Are you sure you wish to save anyways?");

                            // If user aborted the program execution
                            if (Caution_Window.Passed_Value_A.Text_Data == "false")
                            {
                                // Abborting save process
                                return;
                            }

                            Instance.Descendants("Affiliation").First().Value = Affiliation_Pool;
                        }
                    } catch { }
                }


                Save_Switch("Build_Tab_Space_Units", "0", Toggle_Build_Tab);

                Overwrite_Save_Tag(Text_Box_Build_Cost, "Build_Cost_Credits");
                Overwrite_Save_Tag(Text_Box_Build_Time, "Build_Time_Seconds");

                Overwrite_Save_Tag(Text_Box_Skirmish_Cost, "Tactical_Build_Cost_Multiplayer");                
                Overwrite_Save_Tag(Text_Box_Build_Time, "Tactical_Build_Time_Seconds");
          
                Overwrite_Save_Tag(Text_Box_Tech_Level, "Tech_Level");
                Overwrite_Save_Tag(Text_Box_Star_Base_LV, "Required_Star_Base_Level");
                Overwrite_Save_Tag(Text_Box_Ground_Base_LV, "Required_Ground_Base_Level");
                Overwrite_Save_Tag(Text_Box_Required_Timeline, "Required_Timeline");

                Save_Switch("Build_Initially_Locked", "0", Toggle_Innitially_Locked);



                // If checked we set that tag to Yes, otherwise its No
                if (Check_Box_Slice_Cost.Checked) { Validate_Save_Text("Build_Can_Be_Unlocked_By_Slicer", "Yes"); }
                else if (!Check_Box_Slice_Cost.Checked) { Validate_Save_Text("Build_Can_Be_Unlocked_By_Slicer", "No"); }



                Validate_Save_Tag(Text_Box_Slice_Cost, "Slice_Cost_Credits");


                // If checked we set that tag to Yes, otherwise its No
                if (!Check_Box_Current_Limit.Checked) { Validate_Save_Text("Build_Limit_Current_Per_Player", "-1"); }
                else { Validate_Save_Tag(Text_Box_Current_Limit, "Build_Limit_Current_Per_Player"); }

                if (!Check_Box_Lifetime_Limit.Checked) { Validate_Save_Text("Build_Limit_Lifetime_Per_Player", "-1"); }
                else { Validate_Save_Tag(Text_Box_Current_Limit, "Build_Limit_Lifetime_Per_Player"); }         
            }
                 
            //======================= Costum Tag Values =======================

            Validate_Save_Costum_Tag(Text_Box_Tag_1_Name, Text_Box_Costum_Tag_1);
           
            Validate_Save_Costum_Tag(Text_Box_Tag_2_Name, Text_Box_Costum_Tag_2);
           
            Validate_Save_Costum_Tag(Text_Box_Tag_3_Name, Text_Box_Costum_Tag_3);

            Validate_Save_Costum_Tag(Text_Box_Tag_4_Name, Text_Box_Costum_Tag_4);

            Validate_Save_Costum_Tag(Text_Box_Tag_5_Name, Text_Box_Costum_Tag_5);

            Validate_Save_Costum_Tag(Text_Box_Tag_6_Name, Text_Box_Costum_Tag_6);



            //======================= Saving Team/Squadron =======================


            if (Is_Team & Text_Box_Team_Name.Text != "")
            {
                string Team_Name = Text_Box_Team_Name.Text;
              

                Instance =
                    // Selecting all child Tags of Root   
                    from All_Tags in Xml_File.Root.Descendants()
                    // Selecting our Unit
                    where (string)All_Tags.Attribute("Name") == Selected_Team
                    select All_Tags;


                try
                {   // Team Type - Choose between: Sqadron, GroundCompany and HeroCompany
                    if (Combo_Box_Team_Type.Text != "" & Combo_Box_Team_Type.Text != Instance.First().Name)
                    {   // Loading Tag Value from the Combo Box in order to change the Type of unit
                        Instance.First().Name = Combo_Box_Team_Type.Text;

                        Save_Setting(Setting, "Team_Type", Combo_Box_Team_Type.Text);
                    }
                } catch { }


                try // Changing Team Name
                {   if (Text_Box_Team_Name.Text != "" & Team_Name != Selected_Team) 
                    {   
                        Instance.First().Attribute("Name").Value = Team_Name; 
                        Save_Setting(Setting, "Selected_Team", Text_Box_Team_Name.Text); 
                    } 
                } catch {}



                if (Check_Box_Team_Is_Variant.Checked & Text_Box_Team_Is_Variant.Text != "")
                { Validate_Tag_In_Xml(Xml_File, Team_Name, "Variant_Of_Existing_Type", Text_Box_Team_Is_Variant.Text, false); }



                //=======================================================================

                string Team_Tag = "Squadron_Units";
                string Offset_Tag = "Squadron_Offsets";


                try
                {   if (Team_Type == "Squadron")
                    {   // Renaming Tag to Squadron type
                        if (Instance.First().Descendants("Company_Units").Any()) { Instance.First().Descendants("Company_Units").First().Name = "Squadron_Units"; }
                    }
                    else if (Team_Type != "Squadron")
                    {
                        Team_Tag = "Company_Units";
                        // Renaming Tag to Team type
                        if (Instance.First().Descendants("Squadron_Units").Any()) { Instance.First().Descendants("Squadron_Units").First().Name = "Company_Units"; }
                        // Getting rid of all Offset tags, as they are obsolete for ground units after conversion from Squadron to Ground Unit
                        Instance.Elements(Offset_Tag).Where(x => x.Value != "false").Remove();
                    }
                } catch { Imperial_Console(600, 100, Add_Line + "    Error: Could not find the Team Name."); }


                if (Text_Box_Team_Members.Text != "") { Validate_Save_Text(Team_Tag, Regex.Replace(Text_Box_Team_Members.Text, @"\t|\n|\r", "")); }




                if (Team_Type == "Squadron")
                {    
                    try
                    {   
                        // This formula makes will process the right offset, dependant on amount of team members per squad
                        int Offset = 0;
                        Int32.TryParse(Text_Box_Team_Amount.Text, out Temporal_C);
                        Int32.TryParse(Text_Box_Team_Offsets.Text, out Offset);

                        // A array with all possible 10 offset values
                        string[] Offset_Value = 
                        {   "0, 0, 0", 
                            (Offset * 2) + ", 0, 0", 
                            " 0, " + Offset + ", 0", 
                            " 0," + -Offset + ", 0",
 
                            -(Offset * 2) + ", " + (Offset * 2) + ", 0", 
                            -(Offset * 2) + "," + -(Offset * 2) + ", 0", 
                            -(Offset * 2) + ", 0, 0",
 
                            -(Offset * 3) + ", 0, 0", 
                            -(Offset * 3) + ", " + (Offset * 3) + ", 0", 
                            -(Offset * 3) + "," + -(Offset * 3) + ", 0", 
                            -(Offset * 4) + ", 0, 0", 
                        };



                        Temporal_D = 0;

                        foreach (var Tag in Instance.Descendants())
                        {
                            if (Tag.Name == Offset_Tag)
                            {
                                Temporal_D++;

                                // If Amount of Units is smaller then the current cycle that means we need to delete all remaining (obsolete) tags.
                                if (Temporal_C < Temporal_D)
                                { Tag.Value = ""; } // Setting empty to delete below, using .Remove() directly here would crash the loop!

                                else if (Temporal_C == 1) { Tag.Value = Offset_Value[0]; } // A single unit can have the position 0, 0, 0

                                    // Overwriting each tag value with a new one from the offset.
                                else { Tag.Value = Offset_Value[Temporal_D]; }
                            }
                        }

                        // Getting rid of all obsolete Offset tags
                        Instance.Elements(Offset_Tag).Where(x => x.Value == "").Remove();


                        // Processing amout of Tags we need to append in the case there were not enough offset tags in this unit.
                        // Temporal_C = Units we need and Temporal_D = Tags we already made
                        if (Temporal_C > Temporal_D)
                        {
                            if (Temporal_D == 0) // If the Unit has no Offset_Tag we add it to the "Squadron_Units" of this Unit
                            {
                                int Teamleader_Position = 1;
                                if (Temporal_C == 1) { Teamleader_Position = 0; }

                                Instance.First().Descendants(Team_Tag).First().AddAfterSelf(Add_Line, "\t\t", new XElement(Offset_Tag, Offset_Value[Teamleader_Position]));
                                Temporal_D = 1;
                            }

                            for (int i = 1; i < (Temporal_C - Temporal_D) + 1; i++)
                            { Instance.First().Descendants(Offset_Tag).Last().AddAfterSelf(Add_Line, "\t\t", new XElement(Offset_Tag, Offset_Value[Temporal_D + i])); }
                        }
                    } catch { }
                }
                   
              


                Validate_Save_Tag(Text_Box_Shuttle_Type, "Company_Transport_Unit");          

                //======================= Art Settings ======================== 
                
                Validate_Save_Tag(Text_Box_Text_Id, "Text_ID");
                Validate_Save_Tag(Text_Box_Unit_Class, "Encyclopedia_Unit_Class");
                Validate_Save_Tag(Text_Box_Encyclopedia_Text, "Encyclopedia_Text");


                Verify_Tag_with_Extension(null, "Icon_Name", Text_Box_Icon_Name, ".tga");
                Verify_Tag_with_Extension(null, "Radar_Icon_Name", Text_Box_Radar_Icon, ".tga");
                Validate_Save_Tag(Text_Box_Radar_Size, "Radar_Icon_Size");
                // TODO: <Is_Visible_On_Radar>

                //======================= Power Values ======================== 
                
                // If the User changes the Class, in the UI we are going to replace that value with the text in the Combo Box                              
                Validate_Save_Tag(Combo_Box_Class, "CategoryMask");
                
                if (Unit_Mode != "Ground")
                {
                    Validate_Save_Text("MovementClass", "Space");
                }
                else
                { Validate_Save_Text("MovementClass", "Tracked"); }



                Validate_Save_Tag(Text_Box_AI_Combat, "AI_Combat_Power");

                if (Toggle_Operator_Population)
                {
                    Validate_Save_Tag(Text_Box_Population, "Additional_Population_Capacity");

                    // If Operator is + we make sure the "Population_Value" (-) is inactive      
                    Overwrite_Save_Text("Population_Value", "");
                }
                else
                {
                    Validate_Save_Tag(Text_Box_Population, "Population_Value");

                    // If Operator is - we make sure the "Additional_Population_Capacity" (+) is inactive   
                    Overwrite_Save_Text("Additional_Population_Capacity", "");
                }



                Save_Abilities();
      

                //===================== Unit Properties ======================= 
                Save_Switch("Is_Named_Hero", "0", Toggle_Is_Hero);

                Save_Switch("Show_Hero_Head", "0", Toggle_Show_Head);


                // If checked we set that tag to Yes, otherwise its No
                if (Check_Box_Has_Hyperspace.Checked)
                {   Validate_Save_Text("Hyperspace", "Yes");

                    Validate_Save_Tag(Text_Box_Hyperspace_Speed, "Hyperspace_Speed");
                }
                else { Validate_Save_Text("Hyperspace", "No"); }
              

                try
                {   if (Text_Box_Starting_Unit_Name.Text != "" & Text_Box_Spawned_Unit.Text != "")
                    { Validate_Save_Text("Starting_Spawned_Units_Tech_0", Text_Box_Starting_Unit_Name.Text.Replace(" ", "") + ", " + Text_Box_Spawned_Unit.Text.Replace(" ", "")); }

                    else { Overwrite_Save_Text("Starting_Spawned_Units_Tech_0", ""); }
                } catch { }

                try
                {   if (Text_Box_Reserve_Unit_Name.Text != "" & Text_Box_Reserve_Unit.Text != "")
                    { Validate_Save_Text("Reserve_Spawned_Units_Tech_0", Text_Box_Reserve_Unit_Name.Text.Replace(" ", "") + ", " + Text_Box_Reserve_Unit.Text.Replace(" ", "")); }

                    else { Overwrite_Save_Text("Reserve_Spawned_Units_Tech_0", ""); }
                } catch { }


                // Depending on the Unit type it gets a Behvoir Tag
                if (Team_Type == "Squadron") { Temporal_A = "DUMMY_SPACE_FIGHTER_SQUADRON"; Temporal_B = "SpaceBehavior"; }
                else { Temporal_A = "DUMMY_GROUND_COMPANY"; Temporal_B = "LandBehavior"; }
                Validate_Save_Text("Behavior", Temporal_A + ", SELECTABLE");
                Validate_Save_Text(Temporal_B, "REVEAL, ABILITY_COUNTDOWN");


                //======================= Build Requirements =======================

                // If there is any entry in the Affiliation List Box we add this tag
                if (List_Box_Active_Affiliations.Items.Count != 0)
                {
                    string Affiliation_Pool = "";
                    int Cycle_Count = 0;

                    foreach (var Item in List_Box_Active_Affiliations.Items)
                    {
                        Cycle_Count++;

                        if (Cycle_Count == List_Box_Active_Affiliations.Items.Count)
                        {   // This only triggers for the last item, which is not supposed to append the , sign
                            Affiliation_Pool += Item.ToString();
                        }
                        // Adding all Items to a single string
                        else { Affiliation_Pool += Item.ToString() + ", "; }
                    }

                    try
                    {
                        if (Affiliation_Pool != "") { Validate_Save_Text("Affiliation", Affiliation_Pool); }
                        else
                        {
                            Imperial_Dialogue(580, 160, "Save Anyway", "Cancel", "false", Add_Line + "    There is no active Affiliation."
                                                                        + Add_Line + "    Are you sure you wish to save anyways?");

                            // If user aborted the program execution
                            if (Caution_Window.Passed_Value_A.Text_Data == "false")
                            {
                                // Abborting save process
                                return;
                            }

                            Instance.Descendants("Affiliation").First().Value = Affiliation_Pool;
                        }
                    } catch { }
                }


                if (Team_Type == "Squadron") { Temporal_A = "Build_Tab_Space_Units"; }
                else { Temporal_A = "Build_Tab_Land_Units"; }    

                Save_Switch(Temporal_A, "0", Toggle_Build_Tab);

                Overwrite_Save_Tag(Text_Box_Build_Cost, "Build_Cost_Credits");
                Overwrite_Save_Tag(Text_Box_Build_Time, "Build_Time_Seconds");

                Overwrite_Save_Tag(Text_Box_Skirmish_Cost, "Tactical_Build_Cost_Multiplayer");
                Overwrite_Save_Tag(Text_Box_Build_Time, "Tactical_Build_Time_Seconds");        

                Overwrite_Save_Tag(Text_Box_Tech_Level, "Tech_Level");

                Overwrite_Save_Tag(Text_Box_Star_Base_LV, "Required_Star_Base_Level");

                Overwrite_Save_Tag(Text_Box_Ground_Base_LV, "Required_Ground_Base_Level");

                Overwrite_Save_Tag(Text_Box_Required_Timeline, "Required_Timeline");

                Save_Switch("Build_Initially_Locked", "0", Toggle_Innitially_Locked);


                // If checked we set that tag to Yes, otherwise its No
                if (Check_Box_Slice_Cost.Checked) { Validate_Save_Text("Build_Can_Be_Unlocked_By_Slicer", "Yes"); }
                else if (!Check_Box_Slice_Cost.Checked) { Validate_Save_Text("Build_Can_Be_Unlocked_By_Slicer", "No"); }


                Validate_Save_Tag(Text_Box_Slice_Cost, "Slice_Cost_Credits");


                // If checked we set that tag to Yes, otherwise its No
                if (!Check_Box_Current_Limit.Checked) { Validate_Save_Text("Build_Limit_Current_Per_Player", "-1"); }
                else { Validate_Save_Tag(Text_Box_Current_Limit, "Build_Limit_Current_Per_Player"); }

                if (!Check_Box_Lifetime_Limit.Checked) { Validate_Save_Text("Build_Limit_Lifetime_Per_Player", "-1"); }
                else { Validate_Save_Tag(Text_Box_Current_Limit, "Build_Limit_Lifetime_Per_Player"); }

            }

            //======================= End of Unit Setup =======================
         


            // This patches the Selected_Instance into all Starbases of the Active Factions if they are Playable
            // And if these Factions are inside of these tags: the Affiliation Tag and the Tactical_Buildable_Objects_Campaign / Tactical_Buildable_Objects_Multiplayer of the Starbase
            if (Check_Box_Add_To_Base.Checked)
            {
                foreach (string Faction in List_Box_Active_Affiliations.Items)
                {
                    if (Check_Box_Add_To_Skirmish.Checked & Check_Box_Add_To_Campaign.Checked) { Make_Buildable_In_Base(Faction, "StarBase", Selected_Instance, Selected_Instance, false); }
                    else if (Check_Box_Add_To_Skirmish.Checked) { Make_Buildable_In_Base(Faction, "StarBase", null, Selected_Instance, false); }
                    else if (Check_Box_Add_To_Campaign.Checked) { Make_Buildable_In_Base(Faction, "StarBase", Selected_Instance, null, false); }
                }
            }


            //========= Inserting to GameObjectFiles and HardpointDataFiles
            if (Check_Box_Game_Object_Files.Checked) 
            {        
                // Loading the Xml File PATH:
                string Game_Objects_File = Xml_Directory + "GameObjectFiles.xml";

                // Adding the tag "File" to the specified Xml File above with the name of the selected xml
                Save_Tag_Into_Xml(Game_Objects_File, "Root", "File", Path.GetFileName(Selected_Xml), false);
            }
            
            if (Check_Box_Hard_Point_Data.Checked) 
            {
                string Hardpoint_Data_File = Xml_Directory + "HardpointDataFiles.xml";

                // Adding the tag "File" to the specified Xml File above 
                Save_Tag_Into_Xml(Hardpoint_Data_File, "Root", "File", Path.GetFileName(Selected_Xml), false);
            }


           
            
            //========= Saving All Changes we just applied to the .xml =========
            Xml_File.Save(Selected_Xml);

          
         
            // Need to refresh in order to prevent the User from trying to select and edit a object that already changed its Name/Type!!
            if (Debug_Mode == "false")
            {   // But only while not in Debug Mode (buggs me when testing) and Combo_Box_Filter_Type.Text is set to the right type
                if (Last_Type_Search == Unit_Type | Last_Type_Search == "Squadron" | Last_Type_Search == "GroundCompany" | Last_Type_Search == "HeroCompany") { Refresh_Units(); }
            }
          

            // Refresh_Selected_Xml();


            // Removing unwanted comment entries
            Temporal_A = Regex.Replace(File.ReadAllText(Selected_Xml), "<!--here", "");
            Temporal_B = Regex.Replace(Temporal_A, "here-->", "");
            // Removing all Emptyspace lines because we try to clean the ones that summ together in the ability tag
            Temporal_A = Regex.Replace(Temporal_B, @"^\s*$", "", RegexOptions.Multiline);


            File.WriteAllText(Selected_Xml, Temporal_A);

         
        
          }          
          catch { Imperial_Console(720, 100, Add_Line + @"    Save function crashed!"  
                                           + Add_Line + @"    Did you pressed ""Create New""? Then you need to use ""Save As"""
                                           + Add_Line + @"    instead of this button. Otherwise saving one of the Tags may cause trouble.");
          }         
         
           


          // Resetting the value that checks whether any changes were done by the User
          Edited_Selected_Unit = false;

        }






        private void Save_As_Click(object sender, EventArgs e)
        {           
            string Unit_Name = Text_Box_Name.Text;


            // Setting Innitial Filename and Data for the Save Menu
            Save_File_Dialog_1.FileName = "";
            // Save_File_Dialog_1.RestoreDirectory = true;
            Save_File_Dialog_1.InitialDirectory = Xml_Directory;
            Save_File_Dialog_1.Filter = ".xml files (*.xml)|*.xml|All files (*.*)|*.*";
            Save_File_Dialog_1.FilterIndex = 1;

     

            // ========= Preparing Template Code ========= //

            if (Add_Core_Code == "true")
            {
                // Defining Path to the Template file
                string Template = Program_Directory + @"Xml_Core\";


                // Depending on our Segment Button switch (which relies on the chosen unit class) we choose a template file
                switch (Maximal_Value_Class)
                {
                    case "1":
                        Template = Template + "Template_Space_Fighter.xml";
                        break;
                    case "2":
                        Template = Template + "Template_Space_Bomber.xml";
                        break;           
                    case "3":
                        Template = Template + "Template_Space_Corvette.xml";
                        break;
                    case "4":
                        Template = Template + "Template_Space_Frigate.xml";
                        break;
                    case "5":
                        Template = Template + "Template_Space_Capitalship.xml";
                        break;


                    case "6":
                        Template = Template + "Template_Land_Infantry.xml";
                        break;
                    case "7":
                        Template = Template + "Template_Land_Vehicle.xml";
                        break;
                    case "8":
                        Template = Template + "Template_Land_Air.xml";
                        break;
                    case "9":
                        Template = Template + "Template_Hero.xml";
                        break;
                    case "10":
                        Template = Template + "Template_Structure.xml";
                        break;                  
                }


                if (Save_File_Dialog_1.ShowDialog() == DialogResult.OK)
                {
                    // Copying the template code into the new File
                    Overwrite_Copy(Template, Save_File_Dialog_1.FileName);
                }

                // Saving Xml and Instance values of the Recent Unit, so it can be loaded in next program startup
                Selected_Xml = Save_File_Dialog_1.FileName;
                Save_Setting("2", "Selected_Xml", @"""" + Selected_Xml + @"""");

                if (Unit_Name != "") { Save_Setting(Setting, "Selected_Instance", Unit_Name); }
                Label_Xml_Name.Text = Path.GetFileName(Selected_Xml);


                // Now that we use the template as base file, there is no longer a need to create a new file
                // So we just run the normal Save function which deals with the variable "Selected_Xml" we just specified above
                Save_Click(null, null);

                // Exiting this function
                return;

            }

            // ================== //

             try { 

                if (Save_File_Dialog_1.ShowDialog() == DialogResult.OK)
                {
                  
                    // Needed for the .xml Editor Functions
                    Unit_Type = Combo_Box_Type.Text;

                    // CAUTION, Unit_Type here doesen't targets the Global variable from the "Select_Space_Ships" checkbox, below it means Combo_Box_Type.Text !!
                    Root_Tag = Unit_Type + "s";


                    // string Add_Empty_Line = "\n";                 
                   

                    // Creating New Xml File:
                    XDocument Xml_File = new XDocument                   
                    (                     
                        // Creating Root Tag
                        new XElement
                        (Root_Tag,
                                     new XComment(" ================= Generated using Imperialware ================ "),

                                     // This is the Body that gets all Child tags inserted !!
                                     new XElement(Unit_Type, // Root Tag Name
                                                             new XAttribute("Name", Unit_Name))
                        )
                                             
                    );

                    // Used in some functions for saving in this file
                    Save_Xml_File = Xml_File;
                    
                    //================== Making it a variant of the Core Templates ================== 

                    // Making sure the Variant Checkbox is checked
                    if (!Check_Box_Is_Variant.Checked) { Check_Box_Is_Variant.Checked = !Check_Box_Is_Variant.Checked; }
           

                    if (Text_Box_Is_Variant.Text == "") // Only if NOT defined by the user
                    {   // Depending on our Segment Button switch (which relies on the chosen unit class) we choose a template file
                        switch (Maximal_Value_Class)
                        {                          
                            case "1":
                                Text_Box_Is_Variant.Text = "Fighter_Template";
                                break;
                            case "2":
                                Text_Box_Is_Variant.Text = "Bomber_Template";
                                break;
                            case "3":
                                Text_Box_Is_Variant.Text = "Corvette_Template";
                                break;
                            case "4":
                                Text_Box_Is_Variant.Text = "Frigate_Template";
                                break;
                            case "5":
                                Text_Box_Is_Variant.Text = "Capitalship_Template";
                                break;


                            case "6":
                                Text_Box_Is_Variant.Text = "Infantry_Template";
                                break;
                            case "7":
                                Text_Box_Is_Variant.Text = "Vehicle_Template";
                                break;
                            case "8":
                                Text_Box_Is_Variant.Text = "Air_Template";
                                break;
                            case "9":
                                Text_Box_Is_Variant.Text = "Hero_Template";
                                break;
                            case "10":
                                Text_Box_Is_Variant.Text = "Structure_Template";
                                break;                  
                        }
                    }

                    // Setting the Variant_Of_Existing_Type to the template of this unit class
                    Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Variant_Of_Existing_Type", Text_Box_Is_Variant.Text)); 


                    //========================= Saving Art Settings ========================= 

                    if (Text_Box_Text_Id.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Text_ID", Text_Box_Text_Id.Text)); }

                    if (Text_Box_Unit_Class.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Encyclopedia_Unit_Class", Text_Box_Unit_Class.Text)); }

                    if (Text_Box_Encyclopedia_Text.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Encyclopedia_Text", Text_Box_Encyclopedia_Text.Text)); }


                    string Model_Environment = "";
                    if (Unit_Mode == "Space" | Unit_Type == "StarBase" | Unit_Type == "SpaceBuildable" | Unit_Type == "SpecialStructure")
                    { Model_Environment = "Space_Model_Name"; }
                    else if (Unit_Mode == "Ground" | Unit_Mode == "Structure") { Model_Environment = "Land_Model_Name"; }


                    // Remembering the user if the (essential) model is missing
                    if (Text_Box_Model_Name.Text == "")
                    {
                        Imperial_Dialogue(580, 160, "Save Anyway", "Cancel", "false", Add_Line + "    This unit has no Model.alo set."
                                                                    + Add_Line + "    Are you sure you wish to save anyways?");

                        // If user aborted the program execution
                        if (Caution_Window.Passed_Value_A.Text_Data == "false")
                        {
                            // Abborting save process
                            return;
                        }                      
                    }

                    Add_Tag_with_Extension(Unit_Type, Model_Environment, Text_Box_Model_Name, ".alo");


                    if (!Is_Team) 
                    {                                                                                                              
                        Add_Tag_with_Extension(Unit_Type, "Icon_Name", Text_Box_Icon_Name, ".tga");

                        Add_Tag_with_Extension(Unit_Type, "Radar_Icon_Name", Text_Box_Radar_Icon, ".tga");
                        if (Text_Box_Radar_Size.Text != "")
                        { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Radar_Icon_Size", Text_Box_Radar_Size.Text)); }
                    }

                                   
                    // ==== Scallable Art Values          
                
                    if (Text_Box_Scale_Factor.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Scale_Factor", Text_Box_Scale_Factor.Text)); }



                    string Death_Clone_Height = "";
                    if (Text_Box_Model_Height.Text != "") { Death_Clone_Height = "-" + Text_Box_Model_Height.Text; }


                    if (Text_Box_Model_Height.Text != "")
                    {   // false means the operator is - and not +
                        if (Toggle_Operator_Model_Height == false) 
                        { 
                            Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Layer_Z_Adjust", "-" + Text_Box_Model_Height.Text)); 
                        }
                        
                        else // Otherwise we validate save it normally as + value
                        {   Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Layer_Z_Adjust", Text_Box_Model_Height.Text));
                            Death_Clone_Height = Text_Box_Model_Height.Text;
                        }                    
                    }



                    if (!Check_Box_Health_Bar_Size.Checked)
                    {   // If unchecked we add this Tag, which disables Healthbar of the Unit!
                        Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("GUI_Hide_Health_Bar", "Yes"));                                                                  
                    } 
                   
                    if (Combo_Box_Health_Bar_Size.Text == "Small") { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("GUI_Bracket_Size", "0")); }
                    else if (Combo_Box_Health_Bar_Size.Text == "Medium") { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("GUI_Bracket_Size", "1")); }
                    else if (Combo_Box_Health_Bar_Size.Text == "Large") { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("GUI_Bracket_Size", "2")); }


                    if (Text_Box_Health_Bar_Height.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("GUI_Bounds_Scale", Text_Box_Health_Bar_Height.Text)); }

                    if (Text_Box_Select_Box_Scale.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Select_Box_Scale", Text_Box_Select_Box_Scale.Text)); }


                    if (Text_Box_Select_Box_Height.Text != "")
                    {   if (Toggle_Select_Box_Height) { Temporal_A = Text_Box_Select_Box_Height.Text; }
                        else { Temporal_A = "-" + Text_Box_Select_Box_Height.Text; }

                        Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Select_Box_Z_Adjust", Temporal_A)); 
                    }


               

                    //========================= Saving Power Values =========================
                    if (Combo_Box_Class.Text != "")
                    {
                        if (Unit_Mode != "Ground") 
                        {   Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Ship_Class", Combo_Box_Class.Text));
                            Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Space_Layer", Combo_Box_Class.Text));
                        }
                        else
                        {
                            Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("MovementClass", "Tracked"));
                            Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Space_Layer", "Land"));
                        }
                        
                        Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("CategoryMask", Combo_Box_Class.Text));
                    }

                    if (Text_Box_Hull.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Tactical_Health", Text_Box_Hull.Text)); }


                    if (Text_Box_Shield.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Shield_Points", Text_Box_Shield.Text)); }

                    if (Text_Box_Shield_Rate.Text != "")
                    {   // God Mode means a immense amount of shield refresh rate
                        if (Toggle_God_Mode) 
                        { 
                            Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Shield_Refresh_Rate", "100000"));
                            // This unit will be memorized as the God Mode unit 
                            Save_Setting("2", "GMU", @"""" + Selected_Xml + ", " + Text_Box_Name.Text + @"""");  
                        }
                        else { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Shield_Refresh_Rate", Text_Box_Shield_Rate.Text)); }
                    }

                    if (Text_Box_Energy.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Energy_Capacity", Text_Box_Energy.Text)); }

                    if (Text_Box_Energy_Rate.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Energy_Refresh_Rate", Text_Box_Energy_Rate.Text)); }



                    if (Text_Box_Speed.Text != "")
                    {   Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Max_Speed", Text_Box_Speed.Text));

                        decimal Rate_of_Roll = 0;
                        decimal.TryParse(Text_Box_Speed.Text, out Rate_of_Roll);
                        Rate_of_Roll = Rate_of_Roll / 10;
                        Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Max_Rate_Of_Roll", Rate_of_Roll));
                    }
                
                    if (Text_Box_Rate_Of_Turn.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Max_Rate_Of_Turn", Text_Box_Rate_Of_Turn.Text)); }

                    if (Text_Box_Bank_Turn_Angle.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Bank_Turn_Angle", Text_Box_Bank_Turn_Angle.Text)); }



                    if (!Is_Team & Text_Box_Population.Text != "")
                    {   
                        if (Toggle_Operator_Population)
                        {   // If Operator is + we save it as "Additional_Population_Capacity"                       
                            Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Additional_Population_Capacity", Text_Box_Population.Text));                        
                        }
                        else
                        {   // If Operator is - we save it as "Population_Value"                         
                            Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Population_Value", Text_Box_Population.Text));                         
                        }
                    }

                    if (!Is_Team & Text_Box_AI_Combat.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("AI_Combat_Power", Text_Box_AI_Combat.Text)); }


                    /* // We better disable the Projectile, makes no sense to cause trouble with it in a Unit it doesent belong to
                    if (Text_Box_Projectile.Text != "")
                    {Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Projectile_Damage", Text_Box_Projectile.Text));}
                    */


                    Add_Tag(Text_Box_Lua_Script, Xml_File, Root_Tag, "Lua_Script");



                    //======================== Saving Abilities ========================

                    Ability_1_Icon = Verify_Ability_Icon(Ability_1_Icon);
                    Ability_2_Icon = Verify_Ability_Icon(Ability_2_Icon);

               
                    try { Temporal_A = Regex.Replace(Ability_1_Mod_Multipliers, ";", "\n");} catch {}

                 

                    if (Ability_1_Type != null & Ability_1_Type != "" & Ability_1_Type != " NONE")
                    {   Xml_File.Element(Root_Tag).Element(Unit_Type).Add(
                        new XComment(" ========== Primary Ability ========== "),
                        new XElement("Unit_Abilities_Data", new XAttribute("SubObjectList", "Yes"), 
                            new XElement("Unit_Ability", 
                                new XElement("Type", Ability_1_Type),                             
                                new XElement("Supports_Autofire", Ability_1_Toggle_Auto_Fire.ToString()),
                                new XElement("GUI_Activated_Ability_Name", Ability_1_Activated_GUI),
                                new XElement("Expiration_Seconds", Ability_1_Expiration_Time),
                                new XElement("Recharge_Seconds", Ability_1_Recharge_Time),
                              
                                new XElement("Alternate_Name_Text", Ability_1_Name),
                                new XElement("Alternate_Description_Text", Ability_1_Description),
                                new XElement("Alternate_Icon_Name", Ability_1_Icon),

                                new XElement("SFXEvent_GUI_Unit_Ability_Activated", Ability_1_Activated_SFX),
                                new XElement("SFXEvent_GUI_Unit_Ability_Deactivated", Ability_1_Deactivated_SFX),
                              
                                // We use a XComment that is marked with "here" that we remove later in order to preserve < and > signs, also we add "\t\t\t\t" to each line
                                new XComment("here" + Regex.Replace(Temporal_A, "\n", "\n\t\t\t\t") + "here")                                                                                                           
                            )                                                                                 
                        ));                        
                    }

                    try { Temporal_A = Regex.Replace(Ability_2_Mod_Multipliers, ";", "\n"); } catch {}

                    if (Ability_2_Type != null & Ability_2_Type != "" & Ability_2_Type != " NONE") 
                    {
                        Xml_File.Element(Root_Tag).Element(Unit_Type).Element("Unit_Abilities_Data").Add(
                            new XComment(" ========= Secondary Ability ========= "),                           
                            new XElement("Unit_Ability",
                                new XElement("Type", Ability_2_Type),
                                new XElement("Supports_Autofire", Ability_2_Toggle_Auto_Fire.ToString()),
                                new XElement("GUI_Activated_Ability_Name", Ability_2_Activated_GUI),
                                new XElement("Expiration_Seconds", Ability_2_Expiration_Time),
                                new XElement("Recharge_Seconds", Ability_2_Recharge_Time),

                                new XElement("Alternate_Name_Text", Ability_2_Name),
                                new XElement("Alternate_Description_Text", Ability_2_Description),
                                new XElement("Alternate_Icon_Name", Ability_2_Icon),

                                new XElement("SFXEvent_GUI_Unit_Ability_Activated", Ability_2_Activated_SFX),
                                new XElement("SFXEvent_GUI_Unit_Ability_Deactivated", Ability_2_Deactivated_SFX),

                                new XComment("here" + Regex.Replace(Temporal_A, "\n", "\n\t\t\t\t") + "here")                             
                           )
                        );
                    }



                    // Making sure it ends with only 1 "</Abilities>"
                    if (!Text_Box_Additional_Abilities.Text.EndsWith("</Abilities>") & Text_Box_Additional_Abilities.Text != "")
                    {
                        Text_Box_Additional_Abilities.Focus();

                        Imperial_Dialogue(500, 160, "Save", "Cancel", "false", Add_Line + "    Warning: The text inside of the Passive Abilities"
                                                                     + Add_Line + @"    Box does not end with ""</Abilities>"".");

                        if (Caution_Window.Passed_Value_A.Text_Data == "false") { return; }
                    }
                    else if (Text_Box_Additional_Abilities.Text.EndsWith("</Abilities></Abilities>")) 
                    { Text_Box_Additional_Abilities.Text = Text_Box_Additional_Abilities.Text.Replace("</Abilities></Abilities>", "</Abilities>"); }




                    if (Text_Box_Additional_Abilities.Text != "") 
                    {   try
                        {   Xml_File.Element(Root_Tag).Element(Unit_Type).Element("Unit_Abilities_Data").AddAfterSelf(
                               new XComment(" ======== Additional Abilities ========= "),
                               new XComment("here" + Text_Box_Additional_Abilities.Text + "here"),
                               new XComment(" ======================================= ")
                            ); // End of Abilities
                        } catch { }   
                    }
                    else
                    {   try 
                        {   Xml_File.Element(Root_Tag).Element(Unit_Type).Element("Unit_Abilities_Data").AddAfterSelf(
                            new XComment(" ======================================= ")
                            ); // End of Abilities
                        } catch { }                    
                    }



                    // Resetting Icon values
                    Ability_1_Icon = Ability_1_Icon.Replace(".tga", "");
                    Ability_2_Icon = Ability_2_Icon.Replace(".tga", "");
             
                    //========================= Unit Properties ========================
                    if (!Is_Team)
                    {
                        if (Toggle_Is_Hero == true) { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Is_Named_Hero", "Yes")); }
                        else if (Toggle_Is_Hero == false) { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Is_Named_Hero", "No")); }

                        if (Toggle_Show_Head) { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Show_Hero_Head", "Yes")); }
                        else if (Toggle_Show_Head == false) { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Show_Hero_Head", "No")); }

                        if (Check_Box_Has_Hyperspace.Checked)
                        {   // If checked we add hyperspace functions
                            Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Hyperspace", "Yes"));

                            Add_Tag(Text_Box_Hyperspace_Speed, Xml_File, Root_Tag, "Hyperspace_Speed");
                        }

                        if (Text_Box_Starting_Unit_Name.Text != "" & Text_Box_Spawned_Unit.Text != "")
                        { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Starting_Spawned_Units_Tech_0", Text_Box_Starting_Unit_Name.Text + ", " + Text_Box_Spawned_Unit.Text)); }

                        if (Text_Box_Reserve_Unit_Name.Text != "" & Text_Box_Reserve_Unit.Text != "")
                        { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Reserve_Spawned_Units_Tech_0", Text_Box_Reserve_Unit_Name.Text + ", " + Text_Box_Reserve_Unit.Text)); }                   
                    }


                    if (Text_Box_Death_Clone.Text != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Death_Clone", "Damage_Normal, " + Text_Box_Death_Clone.Text)); }
                                            

                    // Depending on the Unit type it gets a Behvoir Tag
                    if (Unit_Mode == "Space") { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Behavior", "DUMMY_STARSHIP, " + "SELECTABLE")); }
                    else if (Unit_Mode == "Ground") { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Behavior", "DUMMY_GROUND_COMPANY, " + "SELECTABLE")); }


                   
                    string Behavoir_Pool = "";
                    int Cycle_Count = 0;

                    foreach (var Item in List_Box_Active_Behavoir.Items)
                    {
                        Cycle_Count++;

                        if (Cycle_Count == List_Box_Active_Behavoir.Items.Count)
                        {   // This only triggers for the last item, which is not supposed to append the , sign
                            Behavoir_Pool += Item.ToString();
                        }
                        // Appending all Items to a single string
                        else { Behavoir_Pool += Item.ToString() + ", "; }
                    }
              
                    // Additonally, if there is any entry in Behavoir_Pool we add this tag
                    if (Behavoir_Pool != "")
                    { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Behavoir_Tag, Behavoir_Pool)); }
                    

                    //======================= Build Requirements =======================

                    if (!Is_Team)
                    {   string Affiliation_Pool = "";
                        // Re-Cycling the Cycle_Count from above ^^
                        Cycle_Count = 0;

                        foreach (var Item in List_Box_Active_Affiliations.Items)
                        {
                            Cycle_Count++;

                            if (Cycle_Count == List_Box_Active_Affiliations.Items.Count)
                            {   // This only triggers for the last item, which is not supposed to append the , sign
                                Affiliation_Pool += Item.ToString();
                            }
                            // Appending all Items to a single string
                            else { Affiliation_Pool += Item.ToString() + ", "; }
                        }
                  
                        try
                        {   if (Affiliation_Pool != "")
                            { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Affiliation", Affiliation_Pool)); }
                            else if (Affiliation_Pool == "")
                            {
                                Imperial_Dialogue(580, 160, "Save Anyway", "Cancel", "false", Add_Line + "    There is no active Affiliation."
                                                                            + Add_Line + "    Are you sure you wish to save anyways?");

                                // If user aborted the program execution
                                if (Caution_Window.Passed_Value_A.Text_Data == "false")
                                {
                                    // Abborting save process
                                    return;
                                }

                                Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Affiliation", ""));
                            }
                        } catch { }
                   


                        // Aligning Tag value to Toggle_Build_Tab
                        if (Toggle_Build_Tab == true) { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Build_Tab_Space_Units", "Yes")); }
                        else if (Toggle_Build_Tab == false) { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Build_Tab_Space_Units", "No")); }

                        Add_Tag(Text_Box_Build_Cost, Xml_File, Root_Tag, "Build_Cost_Credits");
                        Add_Tag(Text_Box_Build_Time, Xml_File, Root_Tag, "Build_Time_Seconds");
                        Add_Tag(Text_Box_Skirmish_Cost, Xml_File, Root_Tag, "Tactical_Build_Cost_Multiplayer");                       
                        Add_Tag(Text_Box_Build_Time, Xml_File, Root_Tag, "Tactical_Build_Time_Seconds");

                        Add_Tag(Text_Box_Tech_Level, Xml_File, Root_Tag, "Tech_Level");
                        Add_Tag(Text_Box_Star_Base_LV, Xml_File, Root_Tag, "Required_Star_Base_Level");
                        Add_Tag(Text_Box_Ground_Base_LV, Xml_File, Root_Tag, "Required_Ground_Base_Level");
                        Add_Tag(Text_Box_Required_Timeline, Xml_File, Root_Tag, "Required_Timeline");
                    

                        // Aligning Tag value to Toggle_Build_Tab
                        if (Toggle_Innitially_Locked == true) { Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement("Build_Initially_Locked", "Yes")); }
                                                
                        Add_Tag(Text_Box_Slice_Cost, Xml_File, Root_Tag, "Slice_Cost_Credits");
                        Add_Tag(Text_Box_Current_Limit, Xml_File, Root_Tag, "Build_Limit_Current_Per_Player");
                        Add_Tag(Text_Box_Lifetime_Limit, Xml_File, Root_Tag, "Build_Limit_Lifetime_Per_Player");
                    }               
                    //========================= Saving Costum Values =========================

                    if (Text_Box_Tag_1_Name.Text != "<Tag_1_Name>" & Text_Box_Costum_Tag_1.Text != "")
                    {// Removing <> signs from name, if the User keeps the brackets
                     string Costum_Tag_1_Name_Value = Regex.Replace(Text_Box_Tag_1_Name.Text,"[</>]", "");
                     Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Costum_Tag_1_Name_Value, Text_Box_Costum_Tag_1.Text));}

                    if (Text_Box_Tag_2_Name.Text != "<Tag_2_Name>" & Text_Box_Costum_Tag_2.Text != "")
                    {string Costum_Tag_2_Name_Value = Regex.Replace(Text_Box_Tag_2_Name.Text, "[</>]", "");
                    Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Costum_Tag_2_Name_Value, Text_Box_Costum_Tag_2.Text));}

                    if (Text_Box_Tag_3_Name.Text != "<Tag_3_Name>" & Text_Box_Costum_Tag_3.Text != "")
                    {string Costum_Tag_3_Name_Value = Regex.Replace(Text_Box_Tag_3_Name.Text, "[</>]", "");
                    Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Costum_Tag_3_Name_Value, Text_Box_Costum_Tag_3.Text));}

                    if (Text_Box_Tag_4_Name.Text != "<Tag_4_Name>" & Text_Box_Costum_Tag_4.Text != "")
                    {string Costum_Tag_4_Name_Value = Regex.Replace(Text_Box_Tag_4_Name.Text, "[</>]", "");
                    Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Costum_Tag_4_Name_Value, Text_Box_Costum_Tag_4.Text));}

                    if (Text_Box_Tag_5_Name.Text != "<Tag_5_Name>" & Text_Box_Costum_Tag_5.Text != "")
                    {string Costum_Tag_5_Name_Value = Regex.Replace(Text_Box_Tag_5_Name.Text, "[</>]", "");
                    Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Costum_Tag_5_Name_Value, Text_Box_Costum_Tag_5.Text));}

                    if (Text_Box_Tag_6_Name.Text != "<Tag_6_Name>" & Text_Box_Costum_Tag_6.Text != "")
                    {string Costum_Tag_6_Name_Value = Regex.Replace(Text_Box_Tag_6_Name.Text, "[</>]", "");
                    Xml_File.Element(Root_Tag).Element(Unit_Type).Add(new XElement(Costum_Tag_6_Name_Value, Text_Box_Costum_Tag_6.Text));}


                    //========================= Appending Death Clone =========================

                    Temporal_A = "";
                    if (Text_Box_Death_Clone_Model.Text != "") { Temporal_A = Text_Box_Death_Clone_Model.Text + ".alo"; }


                    if (Text_Box_Death_Clone.Text != "")
                    {   Xml_File.Element(Root_Tag).Add(
                        new XComment(" ========================= Death Clone ========================= "),
                        new XElement(Unit_Type, // Root Tag Name
                                            new XAttribute("Name", Text_Box_Death_Clone.Text),


                        new XElement("Space_Model_Name", Temporal_A),
                        new XElement("Scale_Factor", Text_Box_Scale_Factor.Text),
                        new XElement("Layer_Z_Adjust", Death_Clone_Height),
                   
                        new XElement("Death_Explosions", "Large_Explosion_Space"),
                        new XElement("Death_SFXEvent_Start_Die", "Unit_Star_Destroyer_Death_SFX"),                  
                        new XElement("Death_Persistence_Duration", "0"),
                        new XElement("Remove_Upon_Death", "true")                                                                                                                                                                                                                                                                                                                                                                                                        
                        ));                     
                    }


                 

                    if (Is_Team & Text_Box_Team_Name.Text != "") 
                    {
                        // Setted global variables, Add_Tag_To_Team() will use both later
                        // Info: Team_Type = Combo_Box_Team_Type.Text;
                        // Save_Xml_File = Xml_File;
                        string Offset_Tag = "";


                        if (Team_Type == "Squadron") { Temporal_A = "Squadron"; Offset_Tag = "Squadron_Offsets"; }
                        else { Temporal_A = "Team"; Offset_Tag = "Company_Units"; }

                    
                        Xml_File.Element(Root_Tag).Add(
                        new XComment(" =========================== " + Temporal_A + " ========================== "),
                        new XElement(Team_Type, // Team Type Tag Name
                                            new XAttribute("Name", Text_Box_Team_Name.Text)
                                         
                        ));


                        //================== Saving Variant of Existing Type ================== 

                        // Making sure the Variant Checkbox is checked
                        if (Check_Box_Team_Is_Variant.Checked & Text_Box_Team_Is_Variant.Text != "") 
                        {                            
                            // Making sure Input is enabled
                            Check_Box_Team_Is_Variant.Enabled = true;

                            // Setting the Variant_Of_Existing_Type to the template of this unit class
                            Xml_File.Element(Root_Tag).Element(Team_Type).Add(new XElement("Variant_Of_Existing_Type", Text_Box_Team_Is_Variant.Text));
                        } 



                        //========================= Saving Art Settings =========================

                        Add_Tag_To_Team("Text_ID", Text_Box_Text_Id);
                        Add_Tag_To_Team("Encyclopedia_Text", Text_Box_Encyclopedia_Text);
                        Add_Tag_To_Team("Encyclopedia_Unit_Class", Text_Box_Unit_Class);
                  

                        if (Text_Box_Team_Members.Text != "")
                        {   Xml_File.Element(Root_Tag).Element(Team_Type).Add(
                            new XElement("Squadron_Units", Regex.Replace(Text_Box_Team_Members.Text, @"\t|\n|\r", ""))); 
                        }


                        if (Unit_Mode == "Space")
                        {   
                            // This formula makes will process the right offset, dependant on amount of team members per squad
                            int Offset = 0;
                            Int32.TryParse(Text_Box_Team_Amount.Text, out Temporal_C);
                            Int32.TryParse(Text_Box_Team_Offsets.Text, out Offset);

                            // First row (Y Axis, X Axis and Z Axis settings)                         
                            if (Temporal_C > 0) { Add_Text_To_Team(Offset_Tag, (Offset * 2) + ", 0, 0"); }
                            if (Temporal_C > 1) { Add_Text_To_Team(Offset_Tag, " 0, " + Offset + ", 0"); }
                            if (Temporal_C > 2) { Add_Text_To_Team(Offset_Tag, " 0," + -Offset + ", 0"); }

                            // Second Row                       
                            if (Temporal_C > 3) { Add_Text_To_Team(Offset_Tag, -(Offset * 2) + ", " + (Offset * 2) + ", 0"); }
                            if (Temporal_C > 4) { Add_Text_To_Team(Offset_Tag, -(Offset * 2) + "," + -(Offset * 2) + ", 0"); }
                            if (Temporal_C > 5) { Add_Text_To_Team(Offset_Tag, -(Offset * 2) + ", 0, 0"); }

                            // Third Row
                            if (Temporal_C > 6) { Add_Text_To_Team(Offset_Tag, -(Offset * 3) + ", 0, 0"); }
                            if (Temporal_C > 7) { Add_Text_To_Team(Offset_Tag, -(Offset * 3) + ", " + (Offset * 3) + ", 0"); }
                            if (Temporal_C > 8) { Add_Text_To_Team(Offset_Tag, -(Offset * 3) + "," + -(Offset * 3) + ", 0"); }

                            if (Temporal_C > 9) { Add_Text_To_Team(Offset_Tag, -(Offset * 4) + ", 0, 0"); }                   
                        }



                        Add_Tag_To_Team("Company_Transport_Unit", Text_Box_Shuttle_Type);


                        // TODO: <Is_Visible_On_Radar>
                 
                        Add_Tag_with_Extension(Team_Type, "Icon_Name", Text_Box_Icon_Name, ".tga");
                        Add_Tag_with_Extension(Team_Type, "Radar_Icon_Name", Text_Box_Radar_Icon, ".tga");                
                        Add_Tag_To_Team("Radar_Icon_Size", Text_Box_Radar_Size);


                       
                        //========================= Saving Power Values =========================
                        if (Combo_Box_Class.Text != "")
                        {   if (Unit_Mode != "Ground") 
                            {
                                // Add_Tag_To_Team("Ship_Class", Combo_Box_Class);
                                Add_Text_To_Team("MovementClass", "Space");
                            }
                            else
                            { Add_Text_To_Team("MovementClass", "Tracked"); }                            
                        }


                        Add_Tag_To_Team("AI_Combat_Power", Text_Box_AI_Combat);

                        if (Text_Box_Population.Text != "")
                        {
                            if (Toggle_Operator_Population)
                            {   // If Operator is + we save it as "Additional_Population_Capacity"                       
                                Add_Tag_To_Team("Additional_Population_Capacity", Text_Box_Population); 
                            }
                            else
                            {   // If Operator is - we save it as "Population_Value"  
                                Add_Tag_To_Team("Population_Value", Text_Box_Population);                              
                            }
                        }
                       

                        if (Toggle_Show_Head) { Add_Text_To_Team("Show_Hero_Head", "Yes"); }
                        else { Add_Text_To_Team("Show_Hero_Head", "No"); }


                        if (Check_Box_Has_Hyperspace.Checked)
                        {   // If checked we add hyperspace functions
                            Add_Text_To_Team("Hyperspace", "Yes");
                            Add_Tag_To_Team("Hyperspace_Speed", Text_Box_Hyperspace_Speed);                                                     
                        }

                        if (Text_Box_Starting_Unit_Name.Text != "" & Text_Box_Spawned_Unit.Text != "")
                        { Add_Text_To_Team("Starting_Spawned_Units_Tech_0", Regex.Replace(Text_Box_Starting_Unit_Name.Text, " ", "") + ", " + Regex.Replace(Text_Box_Spawned_Unit.Text, " ", "")); }

                        if (Text_Box_Reserve_Unit_Name.Text != "" & Text_Box_Reserve_Unit.Text != "")
                        { Add_Text_To_Team("Reserve_Spawned_Units_Tech_0", Regex.Replace(Text_Box_Reserve_Unit_Name.Text, " ", "") + ", " + Regex.Replace(Text_Box_Reserve_Unit.Text, " ", "")); }
                          

                        if (Team_Type == "Squadron") { Temporal_A = "DUMMY_SPACE_FIGHTER_SQUADRON"; Temporal_B = "SpaceBehavior"; }
                        else { Temporal_A = "DUMMY_GROUND_COMPANY"; Temporal_B = "LandBehavior"; }
                        Add_Text_To_Team("Behavior", Temporal_A + ", SELECTABLE");
                        Add_Text_To_Team(Temporal_B, "REVEAL, ABILITY_COUNTDOWN");


                    

                        //======================== Saving Abilities ========================

                        Ability_1_Icon = Verify_Ability_Icon(Ability_1_Icon);
                        Ability_2_Icon = Verify_Ability_Icon(Ability_2_Icon);
                     


                        if (Ability_1_Mod_Multipliers != "" & Ability_1_Mod_Multipliers != null) { Temporal_A = Regex.Replace(Ability_1_Mod_Multipliers, ";", "\n"); }
                        else { Temporal_A = ""; }

                        if (Ability_1_Type != null & Ability_1_Type != "")
                        {
                            Xml_File.Element(Root_Tag).Element(Team_Type).Add(
                            new XComment(" ========== Primary Ability ========== "),
                            new XElement("Unit_Abilities_Data", new XAttribute("SubObjectList", "Yes"),
                                new XElement("Unit_Ability",
                                    new XElement("Type", Ability_1_Type),
                                    new XElement("Supports_Autofire", Ability_1_Toggle_Auto_Fire.ToString()),
                                    new XElement("GUI_Activated_Ability_Name", Ability_1_Activated_GUI),
                                    new XElement("Expiration_Seconds", Ability_1_Expiration_Time),
                                    new XElement("Recharge_Seconds", Ability_1_Recharge_Time),

                                    new XElement("Alternate_Name_Text", Ability_1_Name),
                                    new XElement("Alternate_Description_Text", Ability_1_Description),
                                    new XElement("Alternate_Icon_Name", Ability_1_Icon),

                                    new XElement("SFXEvent_GUI_Unit_Ability_Activated", Ability_1_Activated_SFX),
                                    new XElement("SFXEvent_GUI_Unit_Ability_Deactivated", Ability_1_Deactivated_SFX),

                                    // We use a XComment that is marked with "here" that we remove later in order to preserve < and > signs, also we add "\t\t\t\t" to each line
                                    new XComment("here" + Regex.Replace(Temporal_A, "\n", "\n\t\t\t\t") + "here")   

                                )
                            ));
                        }


                        if (Ability_2_Mod_Multipliers != "" & Ability_2_Mod_Multipliers != null) { Temporal_A = Regex.Replace(Ability_2_Mod_Multipliers, ";", "\n"); }
                        else { Temporal_A = ""; }

                        if (Ability_2_Type != null & Ability_2_Type != "")
                        {
                            Xml_File.Element(Root_Tag).Element(Team_Type).Element("Unit_Abilities_Data").Add(
                                new XComment(" ========= Secondary Ability ========= "),
                                new XElement("Unit_Ability",
                                    new XElement("Type", Ability_2_Type),
                                    new XElement("Supports_Autofire", Ability_2_Toggle_Auto_Fire.ToString()),
                                    new XElement("GUI_Activated_Ability_Name", Ability_2_Activated_GUI),
                                    new XElement("Expiration_Seconds", Ability_2_Expiration_Time),
                                    new XElement("Recharge_Seconds", Ability_2_Recharge_Time),

                                    new XElement("Alternate_Name_Text", Ability_2_Name),
                                    new XElement("Alternate_Description_Text", Ability_2_Description),
                                    new XElement("Alternate_Icon_Name", Ability_2_Icon),

                                    new XElement("SFXEvent_GUI_Unit_Ability_Activated", Ability_2_Activated_SFX),
                                    new XElement("SFXEvent_GUI_Unit_Ability_Deactivated", Ability_2_Deactivated_SFX),

                                    new XComment("here" + Regex.Replace(Temporal_A, "\n", "\n\t\t\t\t") + "here")   
                               )
                            );
                        }

                        Xml_File.Element(Root_Tag).Element(Team_Type).Element("Unit_Abilities_Data").AddAfterSelf(
                           new XComment(" ======================================= ")
                        ); // End of Abilities



                        // Resetting Icon values
                        Ability_1_Icon = Ability_1_Icon.Replace(".tga", "");
                        Ability_2_Icon = Ability_2_Icon.Replace(".tga", "");
             
                        //========================= Saving Build Requirements =========================
                        string Affiliation_Pool = "";
                        // Re-Cycling the Cycle_Count from above ^^
                        Cycle_Count = 0;

                        foreach (var Item in List_Box_Active_Affiliations.Items)
                        {
                            Cycle_Count++;

                            if (Cycle_Count == List_Box_Active_Affiliations.Items.Count)
                            {   // This only triggers for the last item, which is not supposed to append the , sign
                                Affiliation_Pool += Item.ToString();
                            }
                            // Appending all Items to a single string
                            else { Affiliation_Pool += Item.ToString() + ", "; }
                        }

                        Add_Text_To_Team("Affiliation", Affiliation_Pool);


                        if (Team_Type == "Squadron") { Temporal_A = "Build_Tab_Space_Units"; }
                        else { Temporal_A = "Build_Tab_Land_Units"; }

                        if (Toggle_Build_Tab) { Add_Text_To_Team(Temporal_A, "Yes"); }
                        else { Add_Text_To_Team(Temporal_A, "No"); }

                        Add_Tag_To_Team("Build_Cost_Credits", Text_Box_Build_Cost);
                        Add_Tag_To_Team("Tactical_Build_Cost_Multiplayer", Text_Box_Skirmish_Cost);
                        Add_Tag_To_Team("Build_Time_Seconds", Text_Box_Build_Time);
                        Add_Tag_To_Team("Tactical_Build_Time_Seconds", Text_Box_Build_Time);
                        Add_Tag_To_Team("Tech_Level", Text_Box_Tech_Level);

                        Add_Tag_To_Team("Required_Star_Base_Level", Text_Box_Star_Base_LV);
                        Add_Tag_To_Team("Required_Ground_Base_Level", Text_Box_Ground_Base_LV);
                        Add_Tag_To_Team("Required_Timeline", Text_Box_Required_Timeline);
                          
                        // Aligning Tag value to Toggle_Build_Tab
                        if (Toggle_Innitially_Locked) { Add_Text_To_Team("Build_Initially_Locked", "Yes"); }

                        Add_Tag_To_Team("Slice_Cost_Credits", Text_Box_Slice_Cost);
                        Add_Tag_To_Team("Build_Limit_Current_Per_Player", Text_Box_Current_Limit);
                        Add_Tag_To_Team("Build_Limit_Lifetime_Per_Player", Text_Box_Lifetime_Limit);

                    }


                    Xml_File.Element(Root_Tag).Add(new XComment(" ========================= End of File ========================= "));







                    //============= Inserting to GameObjectFiles and HardpointDataFiles
                    if (Check_Box_Game_Object_Files.Checked)
                    {
                        // Loading the Xml File PATH:
                        string Game_Objects_File = Xml_Directory + "GameObjectFiles.xml";

                        // Adding the tag "File" to the specified Xml File above with the Name of the new file.
                        Save_Tag_Into_Xml(Game_Objects_File, "Root", "File", Path.GetFileName(Save_File_Dialog_1.FileName), false);                  
                    }

                    if (Check_Box_Hard_Point_Data.Checked)
                    {
                        string Hardpoint_Data_File = Xml_Directory + "HardpointDataFiles.xml";

                        // Adding the tag "File" to the specified Xml File above with the Name of the new file.
                        Save_Tag_Into_Xml(Hardpoint_Data_File, "Root", "File", Path.GetFileName(Save_File_Dialog_1.FileName), false);                 
                    }

                    





                    // Saving New Xml File using what ever name the User typed into the Save File Dialog:
                    Xml_File.Save(Save_File_Dialog_1.FileName);


            
                    // Removing unwanted comment entries
                    Temporal_A = Regex.Replace(File.ReadAllText(Save_File_Dialog_1.FileName), "<!--here", "");
                    Temporal_A = Regex.Replace(Temporal_A, "here-->", "");
                                      

                    // Temporal_A = Regex.Replace(File.ReadAllText(Save_File_Dialog_1.FileName), "&lt;", "<");
                    // Temporal_A = Regex.Replace(Temporal_A, "&gt;", ">");

                    File.WriteAllText(Save_File_Dialog_1.FileName, Temporal_A);


                    // Saving Xml and Instance values of the Recent Unit, so it can be loaded in next program startup
                    Selected_Xml = Save_File_Dialog_1.FileName;
                    Save_Setting("2", "Selected_Xml", @"""" + Selected_Xml + @"""");

                    if (Unit_Name != "") { Save_Setting(Setting, "Selected_Instance", Unit_Name); }
                    Label_Xml_Name.Text = Path.GetFileName(Selected_Xml);


                    // Resetting the value that checks whether any changes were done by the User
                    Edited_Selected_Unit = false;
                }
            } catch { Imperial_Console(600, 100, Add_Line + "    Failed to save Xml, one or more tags caused an issue."); }

        }



        private void Button_Test_Ingame_Click(object sender, EventArgs e)
        {   // Escaping if our unit has no name
            if (Text_Box_Name.Text == "" | Selected_Xml == "")
            {
                Imperial_Console(520, 100, Add_Line + "Error: Did you save the current Unit?"
                                                    + Add_Line + "Also don't forget to check the GameObjectFiles"
                                                    + Add_Line + "and HardPointDataFiles if necessary."); return;
            }

            List_Box_All_Spawns.Items.Add(Text_Box_Name.Text);

            Check_Cheat_Dummy();

            // Activating Spawn           
            Activate_Spawn = "Spawn = true";

            // Calling fucntion to apply the cheats
            Apply_Cheats();

            // Removing all listed items from the listbox
            List_Box_All_Spawns.Items.Clear();


            Button_Launch_Mod_Click(null, null);
        }


        //================================================ Page 5: Edit Xmls ================================================

        private void Button_Xml_Search_Click(object sender, EventArgs e)
        {          
            // Making sure list is not empty 
            if (Search_Index.Count == 0) { Search_Index.Add("First_Slot"); }
           
            string The_Text = "";
            bool Recently_Listed = false;

            // Because the user typed his search text into the textbox by now, we can search the list for his Text
            foreach (string Xml in Directory.GetFiles(Xml_Directory))
            {
                string File_Name = Path.GetFileName(Xml);
                Recently_Listed = false;
                   
                // if (Entry.ToString().Contains(Text_Box_Xml_Search_Bar.Text))
                if (Regex.IsMatch(File_Name, "(?i).*?" + Text_Box_Xml_Search_Bar.Text + ".*?"))                             
                {
                    // We check all entries of the Search Intex list for the Entry                   
                    for (int i = Search_Index.Count - 1; i >= 0; --i)
                    {
                        var Item = Search_Index[i];

                        if (Item == File_Name) 
                        {   Recently_Listed = true;
                            break;
                        }                          
                    }

                    if (Recently_Listed == false) // Otherwise it already was listed last time and we ignore it
                    {
                        Search_Index.Add(File_Name);
                        // List_Box_Xmls.Items.Add(File_Name);

                        // If not found Selecting that item and adding it to Index_List
                        List_Box_Xmls.SelectedIndex = List_Box_Xmls.FindString(File_Name);
                            
                        return; // As we found and selected the first one
                    }                      
                }                                            
            }


            // The loop above searched file names only, this one looks inside of the files
            foreach (string Xml in Directory.GetFiles(Xml_Directory))
            {
                string File_Name = Path.GetFileName(Xml);
                Recently_Listed = false;

                // ======== Reading Files and searching inside of them for results ========     
                The_Text = File.ReadAllText(Xml);


                // If that file contains our search word anywhere, it will be a match
                // if (Regex.IsMatch(The_Text, "(?i).*?" + Text_Box_Xml_Search_Bar.Text + ".*?"))
                if (The_Text.Contains(Text_Box_Xml_Search_Bar.Text))
                {
                    // We check all entries of the Search Intex list for the Entry                   
                    for (int i = Search_Index.Count - 1; i >= 0; --i)
                    {
                        var Item = Search_Index[i];

                        if (Item == File_Name)
                        {
                            Recently_Listed = true;
                            break;
                        }
                    }


                    if (Recently_Listed == false)
                    {
                        Search_Index.Add(File_Name);
                        // List_Box_Xmls.Items.Add(File_Name);

                        // If not found Selecting that item and adding it to Index_List
                        List_Box_Xmls.SelectedIndex = List_Box_Xmls.FindString(File_Name);
                        break;
                    }
                }
            }


            // Highlighting Tag
            if (Recently_Listed == false)
            {
                foreach (ListViewItem Tag in List_View_Selected_Tag.Items)
                {                   
                    // Regex highlights not case sensitive, but takes too long:   if (Regex.IsMatch(Tag.Text, "(?i).*?" + Text_Box_Xml_Search_Bar.Text + ".*?"))
                    // We loop through all tags and search for the one we need
                    if (Tag.Text.Contains(Text_Box_Xml_Search_Bar.Text))
                    {
                        // Highlighting Background Color
                        Tag.ForeColor = Color_03; Tag.BackColor = Color_04;

                        // Need to exit the loop or it will cause an exception!
                        return;
                    }
                }
            }
           

            // If the function didn't returned by now that means nothing was found and we can reset the Index list
            Search_Index.Clear();          
        }



        private void Text_Box_Xml_Search_Bar_TextChanged(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = Button_Xml_Search; // Button_Xml_Search will be 'clicked' when user presses return
        }


        private void List_Box_Xmls_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If there is any edited tag in the list "Changed_Tag_Names" 
            if (Changed_Tag_Names.Count() > 0)
            {               
                // Call Imperial Dialogue with resolution of 540x160, Button 1 Name , Button 2 Name and Dialogue Text. Then we wait for user input
                Imperial_Dialogue(580, 160, "Don't Save", "Cancel", "Save + Open", Add_Line + "    There are unsaved changes in this Xml."
                                                                    + Add_Line + "    Are you sure you wish to open another Xml instead?");


                // If user aborted the program execution
                if (Caution_Window.Passed_Value_A.Text_Data == "false")
                {
                    // Abborting the opening of any other Xml
                    return;
                }
                else if (Caution_Window.Passed_Value_A.Text_Data == "else")
                {   // Saving before opening next xml
                    Button_Save_Xml_Click(null, null);
                }

                // Clearing Variables to make sure this dialogue won't trigger later unless the User changed anything again.
                Changed_Tag_Names.Clear();
                Changed_Tag_New_Values.Clear();
                Changed_Tag_Old_Values.Clear();
            }



            // Getting the currently selected Xml
            Selected_Xml = List_Box_Xmls.GetItemText(List_Box_Xmls.SelectedItem).ToString();

            // Loading all Tags of the selected Xml Instance into the big List Box
            Load_Xml_File(Xml_Directory + Selected_Xml);

            // Storing for Autoload of recent Xml the next program startup
            Save_Setting("2", "Last_Editor_Xml", @"""" + Xml_Directory + Selected_Xml + @"""");  
        }


        private void List_View_Selected_Tag_SelectedIndexChanged(object sender, EventArgs e)
        {   
            string Selection = "";
            int index =0; 

            // Getting the currently selected Tag
            if (List_View_Selected_Tag.SelectedItems.Count > 0) 
            { Selection = List_View_Selected_Tag.SelectedItems[0].Text; index = List_View_Selected_Tag.SelectedItems[0].Index;}


            // If the value is not empty and was changed
            if (Selected_Xml_Value != "" && Selected_Xml_Value != Original_Xml_Value)
            {              
                
                foreach (ListViewItem Tag in List_View_Selected_Tag.Items)
                {  
                    
                    // We loop through all tags and search for the edited one, if it has the old value and is a Attribute
                    if (Regex.IsMatch(Tag.Text, ".*?<" + Selected_Xml_Tag + @" Name=""" + Original_Xml_Value + @""">"))
                    {
                        // <SpaceUnit Name="Corvette_Template">

                        // Assigning new Value                      
                        Tag.Text = "<" + Selected_Xml_Tag + @" Name=""" + Selected_Xml_Value + @""">";

                        // If the Xml Value was ediited, we change the Tag Color to highlight the changes
                        if (Original_Xml_Value != Selected_Xml_Value) { Tag.ForeColor = Color_03; Tag.BackColor = Color_04; }
                    
                        // Need to exit the loop or it will cause an exception!
                        break;
                    }


                    // We loop through all tags and search for the edited one, if it has the old value it is valid for replacement
                    else if (Tag.Text.Equals("<" + Selected_Xml_Tag + ">" + Original_Xml_Value + @"</" + Selected_Xml_Tag + ">"))
                    {                     
                        // Assigning new Value                      
                        Tag.Text = "<" + Selected_Xml_Tag + ">" + Selected_Xml_Value + @"</" + Selected_Xml_Tag + ">";

                        // If the Xml Value was ediited, we change the Tag Color to highlight the changes
                        if (Original_Xml_Value != Selected_Xml_Value) { Tag.ForeColor = Color_03; Tag.BackColor = Color_04; }
                               
                        // Need to exit the loop or it will cause an exception!
                        break;
                    }



                }
            }

          

            // If the value is not empty and was changed, adding Changes to List to save them later
            if (Selected_Xml_Value != "" & Selected_Xml_Value != Original_Xml_Value)
            {
                Changed_Tag_Names.Add(Selected_Xml_Tag);
                Changed_Tag_New_Values.Add(Selected_Xml_Value);
                Changed_Tag_Old_Values.Add(Original_Xml_Value);

                // Update the List Value of the old selection

                // Some Devellopment Testcode to see which variable get into the tables temp
                /*
                if (Selected_Xml_Tag != null) { List_Box_Xmls.Items.Add(Selected_Xml_Tag); }
                if (Selected_Xml_Value != null) { List_Box_Xmls.Items.Add(Selected_Xml_Value); }
                if (Original_Xml_Value != null) { List_Box_Xmls.Items.Add(Original_Xml_Value); }
                */

                // Clearing temporal variables in order to prevent double entries
                Selected_Xml_Tag = "";
                Selected_Xml_Value = "";
                Original_Xml_Value = "";
            }



            //  Tags with Attribute are usually Instance header like: <SpaceUnit Name="Corvette_Template">
            if (Selection.Contains(@"Name="""))
            {
                Temporal_A = Regex.Replace(Selection, @".*?<.*?Name=""", "");             
                Original_Xml_Value = Regex.Replace(Temporal_A, @""">", "");
            }
            else 
            {   // Removing all <XML_Tags> to get the value only
                Original_Xml_Value = Regex.Replace(Selection, @"<.*?>", "");
            }

           
            // Giving the retrieved value to the User to edit it
            Selected_Xml_Text = Original_Xml_Value;
            Text_Box_Editor.Text = Original_Xml_Value;

            // Selecting the Textbox so the User can type straight away.
            Text_Box_Editor.Select();
        }


        private void Text_Box_Editor_TextChanged(object sender, EventArgs e)
        {   
           
            // Only if the User changes the selected value, we want to add these changes to storag (Deactivated because it causes saving trouble)
            // if (Original_Xml_Value != Text_Box_Editor.Text && Original_Xml_Value != "") 
            if (Text_Box_Editor.Text == "Yes" | Text_Box_Editor.Text == "No" | Text_Box_Editor.Text == "yes" | Text_Box_Editor.Text == "no" |
                Text_Box_Editor.Text == "True" | Text_Box_Editor.Text == "False" | Text_Box_Editor.Text == "true" | Text_Box_Editor.Text == "false")
            {   // Switching active UI Buttons
                Track_Bar_Xml_Values.Visible = false;
                Button_Toggle_Value.Visible = true;
            }


            // If NOT matches any letter
            else if (!Regex.IsMatch(Selected_Xml_Text, "[A-Za-z]")) 
            {   
                // Switching active UI Buttons
                Track_Bar_Xml_Values.Visible = true;
                Button_Toggle_Value.Visible = false;


                int Typed_Value;
                // Switching input mode in order to prevent unwanted message pop ups
                User_Input = false;


                if (Selected_Xml_Text.Contains(".")) // Then it must be decimal
                {
                    decimal Decimal_Text;
                    decimal.TryParse(Selected_Xml_Text, out Decimal_Text);
                    General_Maximal_Xml_Value = (int)Decimal_Text;
                    General_Maximal_Xml_Value = General_Maximal_Xml_Value * 4;

                    decimal Decimal_Value;
                    decimal.TryParse(Text_Box_Editor.Text, out Decimal_Value);
                    Typed_Value = (int)Decimal_Value;

                    // Verifying that no unwanted character was used
                    if (!Regex.IsMatch(Text_Box_Editor.Text, "[0-9.,]")) { Text_Box_Editor.Text = ""; }
                }

                else // Otherwise its int type
                {
                    // Parsing the Textbox values to int and multiplicating to get a max value with +75% range of original value (for the Trackbar).
                    Int32.TryParse(Selected_Xml_Text, out General_Maximal_Xml_Value);

                    if (General_Maximal_Xml_Value > 100)
                    {   // Verifying that no Letters or unwanted signs were typed
                        Typed_Value = Check_Numeral_Text_Box(Text_Box_Editor, General_Maximal_Xml_Value * 4, false);
                    }
                    else { Int32.TryParse(Text_Box_Editor.Text, out Typed_Value); }

                    General_Maximal_Xml_Value = General_Maximal_Xml_Value * 4;
                }    


                try
                {   // Making sure this won't force the user into a track bar range between 0 and 1
                    if (General_Maximal_Xml_Value > 99)
                    {   // Setting amount in the Trackbar according to the Maximum Value                   
                        Track_Bar_Xml_Values.Maximum = General_Maximal_Xml_Value / 100;                                                                 
                    }

                    // Then we need to set the Track Bar according to the Text box / 100      
                    Track_Bar_Xml_Values.Value = Typed_Value / 100;

                } catch { } 

                

                // Rolling back to User Mode
                User_Input = true;
            }

            else
            {   Track_Bar_Xml_Values.Visible = false;
                Button_Toggle_Value.Visible = false;
            }



            string Selection = "";

            // Getting the currently selected Tag
            if (List_View_Selected_Tag.SelectedItems.Count > 0) { Selection = List_View_Selected_Tag.SelectedItems[0].Text; }


        

            //  Tags with Attribute are usually Instance header like: <SpaceUnit Name="Corvette_Template">
            if (Selection.Contains(@"Name="""))
            {
                Temporal_A = Regex.Replace(Selection, @" Name="".*?"">", "");
                Selected_Xml_Tag = Regex.Replace(Temporal_A, @"<", "");
            }
            else 
            {
                // Removing everything after the first closing tag of the first <XML_Tags> line and replacing it by > to get the deleted closing sign back
                Temporal_A = Regex.Replace(Selection, @">.*?</.*?>", "");
                Selected_Xml_Tag = Regex.Replace(Temporal_A, @"<", "");
            }

            // After we set the Selected_Xml_Tag, we now need the NEW value
            Selected_Xml_Value = Text_Box_Editor.Text;           
        }
     

        private void Text_Box_Editor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                List_View_Selected_Tag_SelectedIndexChanged(null, null);
                // Clearing text because it seems to select the old value
                Text_Box_Editor.Text = "";
            }
        }


        private void Track_Bar_Xml_Values_Scroll(object sender, EventArgs e)
        {
            // Setting Value of the Text Box, according to the Maximum Value
            Text_Box_Editor.Text = (Track_Bar_Xml_Values.Value * 100).ToString();
        }

        private void Track_Bar_Xml_Values_MouseUp(object sender, MouseEventArgs e)
        {
            List_View_Selected_Tag_SelectedIndexChanged(null, null);
            Text_Box_Editor.Text = "";
        }


        private void Button_Toggle_Value_Click(object sender, EventArgs e)
        {
            // Depending on the boolean value in the Text box we cause that value to toggle to opposite state
            switch (Text_Box_Editor.Text)
            {
                case "True":
                    Text_Box_Editor.Text = "False";
                    break;
                case "False":
                    Text_Box_Editor.Text = "True";
                    break;
                case "true":
                    Text_Box_Editor.Text = "false";
                    break;
                case "false":
                    Text_Box_Editor.Text = "true";
                    break;

                case "Yes":
                    Text_Box_Editor.Text = "No";
                    break;
                case "No":
                    Text_Box_Editor.Text = "Yes";
                    break;
                case "no":
                    Text_Box_Editor.Text = "yes";
                    break;
                case "yes":
                    Text_Box_Editor.Text = "no";
                    break;
            }
         
       
            // If the value is not empty and was changed
            if (Selected_Xml_Value != "" && Selected_Xml_Value != Original_Xml_Value)
            {
                foreach (ListViewItem Tag in List_View_Selected_Tag.Items)
                {
                    // We loop through all tags and search for the edited one, if it has the old value it is valid for replacement
                    if (Tag.Text.Equals("<" + Selected_Xml_Tag + ">" + Original_Xml_Value + @"</" + Selected_Xml_Tag + ">"))
                    {
                        // Assigning new Value                      
                        Tag.Text = "<" + Selected_Xml_Tag + ">" + Selected_Xml_Value + @"</" + Selected_Xml_Tag + ">";

                        // If the Xml Value was ediited, we change the Tag Color to highlight the changes
                        if (Original_Xml_Value != Selected_Xml_Value) { Tag.ForeColor = Color_03; Tag.BackColor = Color_04; }
                  
                        // Need to exit the loop or it will cause an exception!
                        break;
                    }
                }
            }       
        

        }


        private void Button_Save_Xml_Click(object sender, EventArgs e)
        {
            try
            { // Making sure the current selection updates
               List_View_Selected_Tag_SelectedIndexChanged(null, null);
            }
            catch { }


            // Reversing order of all Elements to make sure the newest entry gets saved instead of the first one 
            Changed_Tag_Names.Reverse();
            Changed_Tag_New_Values.Reverse();
            Changed_Tag_Old_Values.Reverse();



            // Loading
            XElement Xml_File = XElement.Load(Xml_Directory + Selected_Xml);



            foreach (var Tag in Changed_Tag_Names)
            {
                // Getting item index and the Value of it
                int index = Changed_Tag_Names.IndexOf(Tag);
                string The_Changed_Value = Changed_Tag_New_Values[index].ToString();
                string The_Tag = Changed_Tag_Names[index].ToString();

                // Imperial_Console(600, 100, The_Tag); return; 

           
                // Todo
                /*
                // If the new value of a edited Attribute has changed
                if (The_Changed_Value != Xml_File.Descendants().Elements(The_Tag).First().Attribute("Name").Value.ToString())
                {
                    // Setting Attribute Value, if the selected Attribute has the OLD value             
                    // var Element = Xml_File.Descendants().Elements(Tag.ToString()).SingleOrDefault(x => x.Attribute("Name").Value == Changed_Tag_Old_Values[index].ToString());

                    // Setting the new Attribute Name to the content of the Textbox
                    // Element.SetAttributeValue("Name", The_Changed_Value);

                    Xml_File.Descendants().Elements(The_Tag).First().SetAttributeValue("Name", The_Changed_Value);
                }
                */
          
                // Making sure to not overwrite the same entry with itself
                if (Xml_File.Descendants(Tag.ToString()).First().Value != The_Changed_Value)
                {
                    // Because both lists get their feed almost paralell, we use the value with the same index to set it as value for our tag
                    Xml_File.Descendants(Tag.ToString()).First().Value = The_Changed_Value;
                }

            }


            // Saving
            Xml_File.Save(Xml_Directory + Selected_Xml);


            // Clearing History Cache, this helps the "do you really wish to exit" massage box to decide wether it pops up
            Changed_Tag_Names.Clear();
            Changed_Tag_New_Values.Clear();
            Changed_Tag_Old_Values.Clear();

            Undo_Changed_Tag_Names.Clear();
            Undo_Changed_Tag_New_Values.Clear();
            Undo_Changed_Tag_Old_Values.Clear();
        }


        private void Button_Add_Xml_Tag_Click(object sender, EventArgs e)
        {          
            // i = Amount of Entries -1 because the List starts at -1; until I reached 0; count -1 at each iteration
            for (int i = List_View_Selected_Tag.Items.Count - 1; i >= 0; --i)
            {
                var Tag = List_View_Selected_Tag.Items[i];

                // If contains this sign and is not a comment
                if (Tag.Text.Contains(@"</") & !Tag.Text.Contains(@"--"))
                {                    
                    string Closing_Tab = Tag.Text;

                    // Appending the new Tag
                    Tag.Text = "<" + "test" + ">" + "</" + "test" + ">";
                    // Appending the original Closing Tab
                    List_View_Selected_Tag.Items.Add(Closing_Tab);
                    return;
                }
            }

            // Todo: This is not the right solution, find a better one to add the tag after the selected one
        }


        private void Button_Remove_Xml_Tag_Click(object sender, EventArgs e)
        {
            // Caution_Window() Caution = new Caution_Window();
            // Caution.Show();

        
            string Selection = "";

            // Getting the currently selected Tag
            if (List_View_Selected_Tag.SelectedItems.Count > 0) {Selection = List_View_Selected_Tag.SelectedItems[0].Text;}
               
                
            // i = Amount of Entries -1 because the List starts at -1; until I reached 0; count -1 at each iteration
            for (int i = List_View_Selected_Tag.Items.Count - 1; i >= 0; --i)
            {
                var Tag = List_View_Selected_Tag.Items[i];

                // Deleting the currently selected Tag
                if (Tag.Text.Equals(Selection))
                {
                    List_View_Selected_Tag.Items.RemoveAt(i);
                }
            }

            // Todo: Save deletion using Linq
        }


        private void Button_Load_Xml_Click(object sender, EventArgs e)
        {
            // Setting Innitial Filename and Data for the Open Menu
            Open_File_Dialog_1.FileName = "";
            Open_File_Dialog_1.InitialDirectory = Xml_Directory;
            Open_File_Dialog_1.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*";
            Open_File_Dialog_1.FilterIndex = 1;
            Open_File_Dialog_1.RestoreDirectory = true;
            Open_File_Dialog_1.CheckFileExists = true;
            Open_File_Dialog_1.CheckPathExists = true;


            try
            {   // If the Open Dialog found a File
                if (Open_File_Dialog_1.ShowDialog() == DialogResult.OK)
                {
                    // Getting the currently selected Xml
                    Selected_Xml = Open_File_Dialog_1.FileName;

                    // Loading all Tags of the selected Xml Instance into the big List Box
                    Load_Xml_File(Selected_Xml);
                }
            }
            catch { }

        }


        private void Button_Save_Xml_As_Click(object sender, EventArgs e)
        {
            // Setting Innitial Filename and Data for the Save Menu
            Save_File_Dialog_1.FileName = "";
            Save_File_Dialog_1.RestoreDirectory = true;
            Save_File_Dialog_1.InitialDirectory = Xml_Directory;
            Save_File_Dialog_1.Filter = ".xml files (*.xml)|*.xml|All files (*.*)|*.*";
            Save_File_Dialog_1.FilterIndex = 1;


            string New_Xml_File = "";


            // Getting path + name of new file (or a existing one to overwrite).
            if (Save_File_Dialog_1.ShowDialog() == DialogResult.OK)
            {
                New_Xml_File = Save_File_Dialog_1.FileName;
            }


            string The_Text = "";


            foreach (ListViewItem Line in List_View_Selected_Tag.Items)
            {

                if  // If it is a root tag that contains one of these Unit types
                (   Line.Text.Contains("SpaceUnit") | Line.Text.Contains("UniqueUnit") | Line.Text.Contains("TransportUnit") |
                    Line.Text.Contains("HeroUnit") | Line.Text.Contains("GroundInfantry") | Line.Text.Contains("GroundVehicle") |
                    Line.Text.Contains("Squadron") | Line.Text.Contains("HeroCompany") | Line.Text.Contains("GroundCompany") |
                    Line.Text.Contains("StarBase") | Line.Text.Contains("SpaceBuildable") | Line.Text.Contains("SpecialStructure") |
                    Line.Text.Contains("TechBuilding") | Line.Text.Contains("GroundBase") | Line.Text.Contains("GroundStructure") |
                    Line.Text.Contains("GroundBuildable") | Line.Text.Contains("Planet") | Line.Text.Contains("SpaceProp") |                   
                    Line.Text.Contains("HardPoint") | Line.Text.Contains("Campaign") | Line.Text.Contains("xmlversion") 
                )
                { The_Text += "  " + Line.Text + "\n"; }
               
                // commented lines 
                else if (Line.Text.Contains("<!--"))
                {   // Saving with 2 emptyspaces and removing all other emptyspaces infront of the < sign, \s+ means multiple times a single space character                
                    The_Text += "  " + Regex.Replace(Line.Text, @"\s+<!", "<!") + "\n";
                }


                // Usually we use 4x emptyspace 
                else
                {
                    // We summ all text up into one prosa with 4 emptispaces infront of it and a new line at the end
                    The_Text += "    " + Line.Text + "\n";
                }

            }


            // Saving "The_Text" in the New_Xml_File
            File.WriteAllText(New_Xml_File, The_Text);


            // Saving Xml and Instance values of the Recent Unit
            Selected_Xml = Save_File_Dialog_1.FileName;


            // Clearing Variables to make sure the exiting dialogue won't trigger later unless the User changed anything again.
            Changed_Tag_Names.Clear();
            Changed_Tag_New_Values.Clear();
            Changed_Tag_Old_Values.Clear();
        }



        private void Button_Undo_Change_Click(object sender, EventArgs e)
        {                    
            try 
            {
                // Adding the count of total Items - 1 (because of 0 slot) of Changed_Tag_ Variables, which is the newest entry ontop of the stack! 
                Undo_Changed_Tag_Names.Add(Changed_Tag_Names[Changed_Tag_Names.Count - 1]);
                Undo_Changed_Tag_New_Values.Add(Changed_Tag_New_Values[Changed_Tag_New_Values.Count - 1]);
                Undo_Changed_Tag_Old_Values.Add(Changed_Tag_Old_Values[Changed_Tag_Old_Values.Count - 1]);


                // Just to making the Variable visible for Devs in the Xml List on the left
                /*
                List_Box_Xmls.Items.Add("Undo_Changed Tags " + Undo_Changed_Tag_Names[Undo_Changed_Tag_Names.Count - 1]);
                List_Box_Xmls.Items.Add(Undo_Changed_Tag_New_Values[Undo_Changed_Tag_New_Values.Count - 1]);
                List_Box_Xmls.Items.Add(Undo_Changed_Tag_Old_Values[Undo_Changed_Tag_Old_Values.Count - 1]);
                */
            } catch { }
            
            try
            {
                // Removing the newest Entry from the Stack of Variables to apply in the Save process
                Changed_Tag_Names.RemoveAt(Changed_Tag_Names.Count - 1);
                Changed_Tag_New_Values.RemoveAt(Changed_Tag_New_Values.Count - 1);
                Changed_Tag_Old_Values.RemoveAt(Changed_Tag_Old_Values.Count - 1);
                        

                // Making it Visible again:
                /*
                List_Box_Xmls.Items.Add("Removed_Changed Tags " + Undo_Changed_Tag_Names[Undo_Changed_Tag_Names.Count - 1]);
                List_Box_Xmls.Items.Add(Undo_Changed_Tag_New_Values[Undo_Changed_Tag_New_Values.Count - 1]);
                List_Box_Xmls.Items.Add(Undo_Changed_Tag_Old_Values[Undo_Changed_Tag_Old_Values.Count - 1]);
                */
            } catch { }


            try 
            { 
                foreach (ListViewItem Tag in List_View_Selected_Tag.Items)
                {
                    // We loop through all tags and search for the edited one, if it has the NEW value it is valid for replacement
                    if (Tag.Text.Equals("<" + Undo_Changed_Tag_Names[Undo_Changed_Tag_Names.Count - 1] + @" Name=""" + Undo_Changed_Tag_New_Values[Undo_Changed_Tag_New_Values.Count - 1] + @""">"))
                    {
                        // Assigning new Value                      
                        Tag.Text = "<" + Undo_Changed_Tag_Names[Undo_Changed_Tag_Names.Count - 1] + @" Name=""" + Undo_Changed_Tag_Old_Values[Undo_Changed_Tag_Old_Values.Count - 1] + @""">";

                        Undo_Change_Line_Color(Tag);

                        // Need to exit the loop or it will cause an exception!
                        break;
                    }
                    
                    else if (Tag.Text.Equals("<" + Undo_Changed_Tag_Names[Undo_Changed_Tag_Names.Count - 1] + ">" + Undo_Changed_Tag_New_Values[Undo_Changed_Tag_New_Values.Count - 1] + @"</" + Undo_Changed_Tag_Names[Undo_Changed_Tag_Names.Count - 1] + ">"))
                    {
                        // Assigning new Value                      
                        Tag.Text = "<" + Undo_Changed_Tag_Names[Undo_Changed_Tag_Names.Count - 1] + ">" + Undo_Changed_Tag_Old_Values[Undo_Changed_Tag_Old_Values.Count - 1] + @"</" + Undo_Changed_Tag_Names[Undo_Changed_Tag_Names.Count - 1] + ">";

                        Undo_Change_Line_Color(Tag);

                        // Need to exit the loop or it will cause an exception!
                        break;
                    }
                  
                }
            } catch { Imperial_Console(600, 100, Add_Line + "    No change found."); }

        }

        void Undo_Change_Line_Color(ListViewItem Tag)
        {
            // Getting the currently selected Tag (Make sure the MultiSelect property is false!)
            if (List_View_Selected_Tag.SelectedItems.Count > 0)
            {
                List_View_Selected_Tag.Items[Tag.Index].Selected = true;
                List_View_Selected_Tag.Focus();
            }


            // Restoring the checkmate pattern before the change
            if (Checkmate_Color == "true" & Tag.Index % 2 == 0)
            {
                Tag.ForeColor = Color_03;
                Tag.BackColor = Color_07;
            }
            else
            {
                Tag.BackColor = Color.White;

                string The_Value = Regex.Replace(Tag.Text, @"<.*?>", "");

                // Highlighting boolean type values, disabled because triggers to highlight wrong tags
                if (The_Value == "Yes" | The_Value == "No" | The_Value == "yes" | The_Value == "no") { Tag.ForeColor = Color_02; }
                else if (The_Value == "True" | The_Value == "False" | The_Value == "true" | The_Value == "false") { Tag.ForeColor = Color_02; }
                else { Tag.ForeColor = Color.Black; }
            }        
        }



        private void Button_Redo_Change_Click(object sender, EventArgs e)
        {
            // Doing the same as in "Button_Undo_Change_Click", but exchanging variables to achive the opposite effect

            // Adding the count of total Items - 1 (because of 0 slot) of Changed_Tag_ Variables, which is the newest entry ontop of the stack! 
            Changed_Tag_Names.Add(Undo_Changed_Tag_Names[Undo_Changed_Tag_Names.Count - 1]);
            Changed_Tag_New_Values.Add(Undo_Changed_Tag_New_Values[Undo_Changed_Tag_New_Values.Count - 1]);
            Changed_Tag_Old_Values.Add(Undo_Changed_Tag_Old_Values[Undo_Changed_Tag_Old_Values.Count - 1]);

            // Just to make the Variable visible for Devs in the Xml List on the left
            /*
            List_Box_Xmls.Items.Add(Changed_Tag_Names[Changed_Tag_Names.Count - 1]);
            List_Box_Xmls.Items.Add(Changed_Tag_New_Values[Changed_Tag_New_Values.Count - 1]);
            List_Box_Xmls.Items.Add(Changed_Tag_Old_Values[Changed_Tag_Old_Values.Count - 1]);
            */

            try
            {   // To prevent Exceptions, if bigger then 0
                if (Undo_Changed_Tag_Names.Count - 1 > 0)
                {   // Removing the newest Entry from the Stack of Variables to apply in the Save process
                    Undo_Changed_Tag_Names.RemoveAt(Undo_Changed_Tag_Names.Count - 1);
                    Undo_Changed_Tag_New_Values.RemoveAt(Undo_Changed_Tag_New_Values.Count - 1);
                    Undo_Changed_Tag_Old_Values.RemoveAt(Undo_Changed_Tag_Old_Values.Count - 1);

                    // Making it Visible:
                    List_Box_Xmls.Items.Add(Undo_Changed_Tag_Names[Undo_Changed_Tag_Names.Count - 1]);
                }
            } catch { }


            foreach (ListViewItem Tag in List_View_Selected_Tag.Items)
            {
                // We loop through all tags and search for the edited one, if it has the OLD value it is valid for replacement       
                if (Tag.Text.Equals("<" + Changed_Tag_Names[Changed_Tag_Names.Count - 1] + @" Name=""" + Changed_Tag_Old_Values[Changed_Tag_Old_Values.Count - 1] + @""">"))
                {
                    // Assigning new Value to Attribute                     
                    Tag.Text = "<" + Changed_Tag_Names[Changed_Tag_Names.Count - 1] + @" Name=""" + Changed_Tag_New_Values[Changed_Tag_New_Values.Count - 1] + @""">";

                    // Getting the currently selected Tag (Make sure the MultiSelect property is false!)
                    if (List_View_Selected_Tag.SelectedItems.Count > 0)
                    {
                        List_View_Selected_Tag.Items[Tag.Index].Selected = true;
                        List_View_Selected_Tag.Focus();
                    }

                    // We change the Tag Color to highlight the changes
                    Tag.ForeColor = Color_03;
                    Tag.BackColor = Color_04;

                    // Need to exit the loop or it will cause an exception!
                    break;  
                }
               
                else if (Tag.Text.Equals("<" + Changed_Tag_Names[Changed_Tag_Names.Count - 1] + ">" + Changed_Tag_Old_Values[Changed_Tag_Old_Values.Count - 1] + @"</" + Changed_Tag_Names[Changed_Tag_Names.Count - 1] + ">"))
                {
                    // Assigning new Value to normal Tag                  
                    Tag.Text = "<" + Changed_Tag_Names[Changed_Tag_Names.Count - 1] + ">" + Changed_Tag_New_Values[Changed_Tag_New_Values.Count - 1] + @"</" + Changed_Tag_Names[Changed_Tag_Names.Count - 1] + ">";

                    // Getting the currently selected Tag (Make sure the MultiSelect property is false!)
                    if (List_View_Selected_Tag.SelectedItems.Count > 0)
                    {
                        List_View_Selected_Tag.Items[Tag.Index].Selected = true;
                        List_View_Selected_Tag.Focus();
                    }

                    // We change the Tag Color to highlight the changes
                    Tag.ForeColor = Color_03;
                    Tag.BackColor = Color_04;

                    // Need to exit the loop or it will cause an exception!
                    break;  
                }                        
            }
        }

        //================================================== Page 6: Game ==================================================
        private void Combo_Box_Language_SelectedIndexChanged(object sender, EventArgs e)
        {       
            // Setting Variable for the Language, and saving it
            Save_Setting(Setting, "Game_Language", Combo_Box_Language.Text);

            // Setting Language, (Function in Functions.cs)
            Set_Language();
        }

        private void Check_Box_Use_Language_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Use_Language"); }


        private void Check_Box_Evade_Language_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Evade_Language"); }


        private void Button_Use_Game_Language_Click(object sender, EventArgs e)
        {
            if (Use_Language == "false")
            {   // Need to set the other Checkbox activated or this one will have no effect
                Toggle_Checkbox(Check_Box_Use_Language, "1", "Use_Language");
            }


            if (Language_Mode == "Mod" | Language_Mode == "false")
            {
                Save_Setting(Setting, "Language_Mode", "Game");
                Render_Segment_Button(Button_Use_Game_Language, "Left", true, 85, 36, 6, 8, 10, Color_03, "From Game");
                 
                // Setting other segment buttons to "inactive"
                Render_Segment_Button(Button_Use_Mod_Language, "Right", false, 85, 36, 6, 8, 10, Color_04, "From Mod");
            }
            else if (Language_Mode == "Game")
            {
                Save_Setting(Setting, "Language_Mode", "false");
                Render_Segment_Button(Button_Use_Game_Language, "Left", false, 85, 36, 6, 8, 10, Color_04, "From Game");
            }

        }


        private void Button_Use_Mod_Language_Click(object sender, EventArgs e)
        {
            if (Use_Language == "false")
            {   // Need to set the other Checkbox activated or this one will have no effect
                Toggle_Checkbox(Check_Box_Use_Language, "1", "Use_Language");
            }

            if (Language_Mode == "Game" | Language_Mode == "false")
            {
                Save_Setting(Setting, "Language_Mode", "Mod");
                Render_Segment_Button(Button_Use_Mod_Language, "Right", true, 85, 36, 6, 8, 10, Color_03, "From Mod");

                Render_Segment_Button(Button_Use_Game_Language, "Left", false, 85, 36, 6, 8, 10, Color_04, "From Game");
            }
            else if (Language_Mode == "Mod")
            {
                Save_Setting(Setting, "Language_Mode", "false");
                Render_Segment_Button(Button_Use_Mod_Language, "Right", false, 85, 36, 6, 8, 10, Color_04, "From Mod");
            }
        }


        private void Check_Box_Windowed_Mode_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Windowed_Mode"); }


        //================================================ Page 7: Settings ================================================
   

        private void Track_Bar_Transperency_Scroll(object sender, EventArgs e)
        {          
            // We set the Opacity to depend on the value of this Trackbar devided to 100 %
            this.Opacity = (double)Track_Bar_Transperency.Value / 100;
            double Opacity_Value = Track_Bar_Transperency.Value;

            string The_Transparency = ((double)Track_Bar_Transperency.Value / 100).ToString();
            Save_Setting("2", "Transparency", The_Transparency);

            // Alternative: Saving Value to .property file
            // Transparency = Imperialware.Properties.Settings.Default.Transparency = (double)Track_Bar_Transperency.Value / 100; 
            // Imperialware.Properties.Settings.Default.Save();          
        }


        private void Check_Box_Close_After_Launch_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Close_After_Launch"); }
     

        private void Check_Box_Debug_Mode_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Debug_Mode"); }
        
  
        private void Check_Box_Copy_Editor_Art_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Copy_Art_Into_Editor"); }


        private void Check_Box_Allow_Patching_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Allowed_Patching"); }


        private void Check_Box_Show_Mod_Button_CheckedChanged(object sender, EventArgs e)
        {   
            if (User_Input == true)
            {   
                if (!Check_Box_Show_Mod_Button.Checked)
                {
                    Check_Box_Show_Mod_Button.ForeColor = Color_02;
                    Save_Setting(Setting, "Show_Mod_Button", "false");
                    Button_Mod_Only.Visible = false;
                } 
                else
                {   Check_Box_Show_Mod_Button.ForeColor = Color_03;         
                    Save_Setting(Setting, "Show_Mod_Button", "true");
                    Button_Mod_Only.Visible = true;
                }
            }
        }

          
        private void Check_Box_Show_Addon_Button_CheckedChanged(object sender, EventArgs e)
        {   
            if (User_Input == true)
            {   
                if (!Check_Box_Show_Addon_Button.Checked)
                {
                    Check_Box_Show_Addon_Button.ForeColor = Color_02;
                    Save_Setting(Setting, "Show_Addon_Button", "false");
                    Button_Launch_Addon.Visible = false;                                              
                } 
                else
                {   Check_Box_Show_Addon_Button.ForeColor = Color_03;
                    Save_Setting(Setting, "Show_Addon_Button", "true");
                    Button_Launch_Addon.Visible = true;
                }
            }
        }




        private void Check_Box_Xml_Checkmate_CheckedChanged(object sender, EventArgs e)
        {   
            if (User_Input == true)
            {   // If the variable is already true, we are going to toggle it to false
                if (!Check_Box_Xml_Checkmate.Checked)
                {   
                    Check_Box_Xml_Checkmate.ForeColor = Color_02;
                    Save_Setting(Setting, "Checkmate_Color", "false");
                    // Picture_Box_Checkmate_Color.Visible = false;
                } 

                else
                {   Check_Box_Xml_Checkmate.ForeColor = Color_03;
                    Save_Setting(Setting, "Checkmate_Color", "true");
                    // Picture_Box_Checkmate_Color.Visible = true;
                }

                Refresh_Theme_List();
                Combo_Box_Dashboard_Mod_SelectedIndexChanged(null, null);
                try { Load_Xml_File(Selected_Xml); } catch {}
            }
                
        }



      
        private void Check_Box_Load_Rescent_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Load_Rescent_File"); }
      

        private void Check_Box_Load_Issues_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Show_Load_Issues"); }
       

        private void Check_Box_Load_Tag_Issues_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Show_Tag_Issues"); }


        private void Check_Box_Add_Core_Code_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Add_Core_Code"); }


            
    

        // ============= User Interface Settings =================

        private void Check_Box_Allow_Auto_Apply_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Auto_Apply"); }

        private void Check_Box_Set_Theme_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Auto_Theme"); }

        private void Check_Box_Set_Color_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Auto_Color"); }


        private void Check_Box_Cycle_Mod_Image_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Cycle_Mod_Image"); }



        private void Button_Add_Scepter_Sway_Click(object sender, EventArgs e)
        {
            string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            if (!File.Exists(Desktop + @"\Szepter Sway.exe"))
            {   byte[] Szepter_Sway = Imperialware.Properties.Resources.Szepter_Sway;
                File.WriteAllBytes(Desktop + @"\Szepter Sway.exe", Szepter_Sway);
            }
            else { Imperial_Console(700, 100, Add_Line + "    Szepter_Sway.exe already exists on your Desktop. You need only"
                                            + Add_Line + "    one copy of it to instant access the currently selected mod settings.");
            }
        }



        // =============== Color Selection ===============

        private void Picture_Box_Color_Picker_MouseMove(object sender, MouseEventArgs e)
        {
            //int Brightness = 0;

            try
            {   Bitmap bmp = new Bitmap(Picture_Box_Color_Picker.Image);
                // Getting the color pixel under the location of the Mouse X and Y value:
                Color The_Color = bmp.GetPixel(e.X, e.Y);
                // Setting current pixel color as background color
                Picture_Box_Current_Color.BackColor = The_Color;

 
                // Brightness = Convert.ToInt32(The_Color.GetBrightness() * 100);

                // Setting that Value to our Textbox, so the user can see and influence it.
                Text_Box_Color_Selection.Text = The_Color.R.ToString() + ", " + The_Color.G.ToString() + ", " + The_Color.B.ToString() + ", " + The_Color.A.ToString();
                

            } catch { }

            // Preventing the Textbox to display 0 when the outer rim of the round selection image is touched
            if (Text_Box_Color_Selection.Text == "0, 0, 0, 0")
            {
                Text_Box_Color_Selection.Text = Picture_Box_Selected_Color.BackColor.R.ToString() + ", " + Picture_Box_Selected_Color.BackColor.G.ToString()
                 + ", " + Picture_Box_Selected_Color.BackColor.B.ToString() + ", " + Picture_Box_Selected_Color.BackColor.A.ToString();
            }

        }

        private void Picture_Box_Color_Picker_MouseLeave(object sender, EventArgs e)
        {   // Making sure the selected color is always displayed in the textbox, unless the user clicks the other checkbox
            Text_Box_Color_Selection.Text = Picture_Box_Selected_Color.BackColor.R.ToString() + ", " + Picture_Box_Selected_Color.BackColor.G.ToString()
                  + ", " + Picture_Box_Selected_Color.BackColor.B.ToString() + ", " + Picture_Box_Selected_Color.BackColor.A.ToString();
        }

        private void Picture_Box_Color_Picker_Click(object sender, EventArgs e)
        {   // When clicking the Image we set the color of the current color box to the enduring one
            Picture_Box_Selected_Color.BackColor = Picture_Box_Current_Color.BackColor;
        }


        private void Picture_Box_Selected_Color_Click(object sender, EventArgs e)
        {
            Text_Box_Color_Selection.Text = Picture_Box_Selected_Color.BackColor.R.ToString() + ", " + Picture_Box_Selected_Color.BackColor.G.ToString()
               + ", " + Picture_Box_Selected_Color.BackColor.B.ToString() + ", " + Picture_Box_Selected_Color.BackColor.A.ToString();
        }

        private void Picture_Box_Current_Color_Click(object sender, EventArgs e)
        {
            Text_Box_Color_Selection.Text = Picture_Box_Current_Color.BackColor.R.ToString() + ", " + Picture_Box_Current_Color.BackColor.G.ToString()
              + ", " + Picture_Box_Current_Color.BackColor.B.ToString() + ", " + Picture_Box_Current_Color.BackColor.A.ToString();
        }


        private void Button_Color_1_Click(object sender, EventArgs e)
        {  
            Color_01 = Picture_Box_Selected_Color.BackColor;
            Save_Setting("2", "Color_01", Text_Box_Color_Selection.Text);

            Set_Color(Color_01, 01);
        }


        private void Button_Color_2_Click(object sender, EventArgs e)
        {  
            Color_02 = Picture_Box_Selected_Color.BackColor;
            Save_Setting("2", "Color_02", Text_Box_Color_Selection.Text);

            Label_Amount.ForeColor = Color_02;
            Set_Color(Color_02, 02);
            Set_Checkbox_Color(Color_02, false);
            Change_Button_Color();
        }


        private void Button_Color_3_Click(object sender, EventArgs e)
        {
            Color_03 = Picture_Box_Selected_Color.BackColor;
            Save_Setting("2", "Color_03", Text_Box_Color_Selection.Text);

            Set_Color(Color_03, 03);
            Set_Checkbox_Color(Color_03, true);
            Set_All_Segment_Buttons();

            // Refreshing Checkmate Lists after color change
            Refresh_Theme_List();
            Combo_Box_Dashboard_Mod_SelectedIndexChanged(null, null);
        }


        private void Button_Color_4_Click(object sender, EventArgs e)
        {
            Color_04 = Picture_Box_Selected_Color.BackColor;
            Save_Setting("2", "Color_04", Text_Box_Color_Selection.Text);

            Set_Color(Color_04, 04);
            Set_All_Segment_Buttons();
        }

        private void Button_Color_5_Click(object sender, EventArgs e)
        {
            Color_05 = Picture_Box_Selected_Color.BackColor;
            Save_Setting("2", "Color_05", Text_Box_Color_Selection.Text);

            Set_Color(Color_05, 05);
        }

        private void Button_Color_6_Click(object sender, EventArgs e)
        {
            Color_06 = Picture_Box_Selected_Color.BackColor;
            Save_Setting("2", "Color_06", Text_Box_Color_Selection.Text);

            Set_Color(Color_06, 06);
        }


        // This Image will act as 7th color setter
        private void Picture_Box_Checkmate_Color_Click(object sender, EventArgs e)
        {
            Color_07 = Picture_Box_Selected_Color.BackColor;
            Save_Setting("2", "Color_07", Text_Box_Color_Selection.Text);

            Picture_Box_Checkmate_Color.BackColor = Color_07;

            // Refreshing Lists after color change
            Refresh_Theme_List();
            Combo_Box_Dashboard_Mod_SelectedIndexChanged(null, null);
        }




        private void Button_Color_Switch_1_Click(object sender, EventArgs e)
        {
            Save_Setting(Setting, "Button_Color", "Red");
            Set_All_Switch_Buttons();
            Set_Color_Segment_Buttons();
            Set_All_Segment_Buttons();
        }

        private void Button_Color_Switch_2_Click(object sender, EventArgs e)
        {
            Save_Setting(Setting, "Button_Color", "Ice");
            Set_All_Switch_Buttons();
            Set_Color_Segment_Buttons();
            Set_All_Segment_Buttons();
        }

        private void Button_Color_Switch_3_Click(object sender, EventArgs e)
        {
            Save_Setting(Setting, "Button_Color", "Green_Matrix");
            Set_All_Switch_Buttons();
            Set_Color_Segment_Buttons();
            Set_All_Segment_Buttons();
        }

        private void Button_Color_Switch_4_Click(object sender, EventArgs e)
        {
            Save_Setting(Setting, "Button_Color", "Gold");
            Set_All_Switch_Buttons();
            Set_Color_Segment_Buttons();
            Set_All_Segment_Buttons();
        }

        private void Button_Color_Switch_5_Click(object sender, EventArgs e)
        {
            Save_Setting(Setting, "Button_Color", "Pink");
            Set_All_Switch_Buttons();
            Set_Color_Segment_Buttons();
            Set_All_Segment_Buttons();
        }

        private void Button_Color_Switch_6_Click(object sender, EventArgs e)
        {
            Save_Setting(Setting, "Button_Color", "Stargate");
            Set_All_Switch_Buttons();
            Set_Color_Segment_Buttons();
            Set_All_Segment_Buttons();
        }

        private void Button_Color_Switch_7_Click(object sender, EventArgs e)
        {
            Save_Setting(Setting, "Button_Color", "Metal");
            Set_All_Switch_Buttons();
            Set_Color_Segment_Buttons();
            Set_All_Segment_Buttons();
        }



        // =============== Theme Selection ===============

        private void List_View_Theme_Selection_SelectedIndexChanged(object sender, EventArgs e)
        {
            // IMPORTRANT: This check prevents a timing exception!
            if (List_View_Theme_Selection.SelectedItems.Count > 0)
            {
                if (List_View_Theme_Selection.SelectedItems[0].Text != "")
                { Selected_Theme = Program_Directory + @"Themes\" + List_View_Theme_Selection.SelectedItems[0].Text + @"\"; }             
            }
         
        }

        private void Button_Choose_Theme_Click(object sender, EventArgs e)
        {
            Change_Theme(Selected_Theme, true);          
        }


        private void Button_Set_Background_Click(object sender, EventArgs e)
        {
            // Setting Innitial Filename and Data for the Open Menu
            Open_File_Dialog_1.FileName = "";
            Open_File_Dialog_1.InitialDirectory = Program_Directory + @"Themes\";
            Open_File_Dialog_1.Filter = "jpg files (*.jpg)|*.jpg|png files (*.png)|*.png|All files (*.*)|*.*";
            Open_File_Dialog_1.FilterIndex = 1;
            Open_File_Dialog_1.RestoreDirectory = true;
            Open_File_Dialog_1.CheckFileExists = true;
            Open_File_Dialog_1.CheckPathExists = true;


            try
            {   // If the Open Dialog found a File
                if (Open_File_Dialog_1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {   // We are going to set that file as our background Image            
                        Background_Image_Path = Path.GetDirectoryName(Open_File_Dialog_1.FileName) + @"\";         
                        Background_Image_A = Path.GetFileName(Open_File_Dialog_1.FileName);

                        Adjust_Wallpaper(Background_Image_Path + Background_Image_A);


                        Save_Setting(Setting, "Background_Image_Path", @"""" + Background_Image_Path + @"""");
                        Save_Setting(Setting, "Background_Image_A", @"""" + Background_Image_A + @"""");

                        // Showing the right tab, so the user gets the connection
                        if (User_Input == true) { Tab_Control_01.SelectedIndex = 0; }
                    }
                    catch { Imperial_Console(600, 100, "Could not find the " + Program_Directory + @"Theme\Background image"); }
                }
            }
            catch { Imperial_Console(600, 100, "Error: File not found."); }
           
        }


        private void Button_Cycle_Background_Click(object sender, EventArgs e)
        {
            // If Themes exist and if not already in cycle mode
            if (Directory.Exists(Program_Directory + "Themes"))
            {
                // Innitiating list and adding just 0 to get rid of the 0 slot, so we can later start at 1
                List<string> Found_Directories = new List<string>();


                // Inserting all found dirs into our list
                foreach (string Folder in Directory.GetDirectories(Program_Directory + "Themes"))
                {
                    Found_Directories.Add(Folder);
                }


                // Generating random Variable
                Random Randomizer = new Random();
                // Generating random Number from this range
                int Random = Randomizer.Next(1, Found_Directories.Count());


                try
                {   // We are going to set that file as our .png background Image
                    Adjust_Wallpaper(Found_Directories[Random] + @"\Background.png");
                    Save_Setting(Setting, "Background_Image_A", "Cycle_Mode");

                    // Using this variable as temporary storage for the random image
                    Background_Image_Path = Found_Directories[Random] + @"\Background.png";
                    // Showing the right tab, so the user gets the connection
                    if (User_Input == true) { Tab_Control_01.SelectedIndex = 0; }
                }
                catch
                {
                    try
                    {   // Otherwise we are going to try setting that file as .jpg background Image
                        // Background_Wallpaper.Image = Image.FromFile(Found_Directories[Random] + @"\Background.jpg");
                        Adjust_Wallpaper(Found_Directories[Random] + @"\Background.jpg");
                        Save_Setting(Setting, "Background_Image_A", "Cycle_Mode");

                        // Using this variable as temporary storage for the random image
                        Background_Image_Path = Found_Directories[Random] + @"\Background.jpg";
                        // Showing the right tab, so the user gets the connection
                        if (User_Input == true) { Tab_Control_01.SelectedIndex = 0; }
                    }
                    catch { Background_Wallpaper.Image = null; }
                }

                // Clearing for next usage
                Found_Directories.Clear();
            }

            else
            {   // Setting the Imperialware background image to false as no themes directory exists.          
                Background_Wallpaper.Image = null;
            }

        }


        // This event triggers for each Button we highlight!!
        void Highlight_Button(object sender, EventArgs e)
        {           
            if (User_Input) 
            {   // For which ever button the user hovers with the mouse pointer we set it here as variable
                Button Selected_Button = ((Button)sender);
                int Selected_Width = Selected_Button.Size.Width;
                int Selected_Height = Selected_Button.Size.Height;
                string Replacement_Button = "Button_Highlighted.png";

                if (Selected_Button == Button_Search_Object | Selected_Button == Button_Search_Unit | Selected_Button == Button_Xml_Search | Selected_Button == Button_Search_Ability)
                {   Replacement_Button = "Button_Search_Highlighted.png";
                    Selected_Width = Selected_Button.Size.Width - 4;
                    Selected_Height = Selected_Button.Size.Width - 4;
                }
                else if (Selected_Button == Button_Undo_Change)
                {   Replacement_Button = "Button_Undo_Highlighted.png";
                    Selected_Width = Selected_Button.Size.Width - 4;
                    Selected_Height = Selected_Button.Size.Height - 4;
                }
                else if (Selected_Button == Button_Redo_Change)
                {   Replacement_Button = "Button_Redo_Highlighted.png";
                    Selected_Width = Selected_Button.Size.Width - 4;
                    Selected_Height = Selected_Button.Size.Height - 4;
                }
                
                // And try to set a button image from the selected theme, if that fails we use the one in the default theme directory:
                if (File.Exists(Selected_Theme + @"Buttons\" + Replacement_Button)) { Selected_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", Replacement_Button, Selected_Width, Selected_Height); }
                else { Selected_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", Replacement_Button, Selected_Width, Selected_Height); }
            }     
        }

        void Unhighlight_Button(object sender, EventArgs e)
        {
            if (User_Input)
            {
                Button Selected_Button = ((Button)sender);
                int Selected_Width = Selected_Button.Size.Width;
                int Selected_Height = Selected_Button.Size.Height;

                string Replacement_Button = "Button.png";

                if (Selected_Button == Button_Search_Object | Selected_Button == Button_Search_Unit | Selected_Button == Button_Xml_Search | Selected_Button == Button_Search_Ability)
                {   Replacement_Button = "Button_Search.png";
                    Selected_Width = Selected_Button.Size.Width - 4;
                    Selected_Height = Selected_Button.Size.Width - 4;
                }
                else if (Selected_Button == Button_Undo_Change)
                {   Replacement_Button = "Button_Undo.png";
                    Selected_Width = Selected_Button.Size.Width - 4;
                    Selected_Height = Selected_Button.Size.Height - 4;
                }
                else if (Selected_Button == Button_Redo_Change)
                {   Replacement_Button = "Button_Redo.png";
                    Selected_Width = Selected_Button.Size.Width - 4;
                    Selected_Height = Selected_Button.Size.Height - 4;
                }

                if (File.Exists(Selected_Theme + @"Buttons\" + Replacement_Button)) { Selected_Button.Image = Resize_Image(Selected_Theme + @"Buttons\", Replacement_Button, Selected_Width, Selected_Height); }
                else { Selected_Button.Image = Resize_Image(Program_Directory + @"Themes\Default\Buttons\", Replacement_Button, Selected_Width, Selected_Height); }
            }
        }

        
        // ======================= //
    
        private void Text_Box_EAW_Path_TextChanged(object sender, EventArgs e)
        {
            if (Text_Box_EAW_Path.Text != "")
            {   // We're going to need this 
                if (Game_Path == Game_Path_EAW) { Temporal_A = "true"; }            

                Save_Setting("0", "Game_Path_EAW", Text_Box_EAW_Path.Text);

                if (Temporal_A == "true")
                {
                    Save_Setting("0", "Game_Path", Game_Path_EAW);
                    Temporal_A = "false";
                }
            }
        }

      
        private void Button_Open_EAW_Path_Click(object sender, EventArgs e)
        {
            try { System.Diagnostics.Process.Start(Game_Path_EAW + Mods_Directory); }
            catch
            {
                Imperial_Console(700, 100, Add_Line + @"    Could not find .\LucasArts\Star Wars Empire at War\GameData\"
                    + Add_Line + @"    Please try hitting Reset or to find and type it manually.");
            }
        }


        private void Button_Reset_EAW_Path_Click(object sender, EventArgs e)
        {
            string Temporal_C = "";

            if (Game_Path == Game_Path_EAW) { Temporal_C = "true"; }

            Save_Setting("0", "Game_Path_EAW", Get_Game_Path("EAW"));
            Text_Box_EAW_Path.Text = Game_Path_EAW;


            if (Temporal_C == "true")
            {
                Save_Setting("0", "Game_Path", Game_Path_EAW);
                Temporal_C = "false";
            }

            Refresh_Mods_Click(null, null);
        }

       

        private void Text_Box_FOC_Path_TextChanged(object sender, EventArgs e)
        {
            if (Text_Box_FOC_Path.Text != "")
            {
                if (Game_Path == Game_Path_FOC) { Temporal_A = "true"; }

                Save_Setting("0", "Game_Path_FOC", Text_Box_FOC_Path.Text);


                if (Temporal_A == "true")
                {
                    Save_Setting("0", "Game_Path", Game_Path_FOC);
                    Temporal_A = "false";
                }
            }
        }


        private void Button_Open_FOC_Path_Click(object sender, EventArgs e)
        {
            try { System.Diagnostics.Process.Start(Game_Path_FOC + Mods_Directory); }
            catch
            {
                Imperial_Console(720, 100, Add_Line + @"    Could not find .\LucasArts\Star Wars Empire at War Forces of Corruption\"
                    + Add_Line + @"    Please try hitting Reset or to find and type it manually.");
            }
        }


        private void Button_Reset_FOC_Path_Click(object sender, EventArgs e)
        {
            string Temporal_C = "";

            if (Game_Path == Game_Path_FOC) { Temporal_C = "true"; }

            Save_Setting("0", "Game_Path_FOC", Get_Game_Path("FOC"));
            Text_Box_FOC_Path.Text = Game_Path_FOC;

            if (Temporal_C == "true")
            {
                Save_Setting("0", "Game_Path", Game_Path_FOC);
                Temporal_C = "false";
            }

            Refresh_Mods_Click(null, null);
        }





        private void Text_Box_Savegame_Path_TextChanged(object sender, EventArgs e)
        {
            if (Text_Box_Savegame_Path.Text != "") 
            { Save_Setting("0", "Savegame_Path", Text_Box_Savegame_Path.Text); }
        }

        private void Check_Box_Mod_Savegame_Directory_CheckedChanged(object sender, EventArgs e)
        { Auto_Toggle_Checkbox((CheckBox)sender, Setting, "Use_Mod_Savegame_Dir"); }


        private void Button_Open_Savegame_Path_Click(object sender, EventArgs e)
        {
            try { System.Diagnostics.Process.Start(Savegame_Path); }
            catch
            {
                Imperial_Console(600, 100, Add_Line + @"    Could not find C:\Users\???\AppData\Roaming\Petroglyph\"
                    + Add_Line + @"    Please try hitting Reset or to find and type it manually.");
            }
        }


        private void Button_Reset_Savegame_Path_Click(object sender, EventArgs e)
        {
            // Text_Box_Savegame_Path.Text = @"C:\Users\Jago\AppData\Roaming\Petroglyph";
            string App_Data = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            
            Text_Box_Savegame_Path.Text = App_Data + @"\Petroglyph\";
            Save_Setting("0", "Savegame_Path", Text_Box_Savegame_Path.Text);
        }


        private void Button_EAW_Command_Bar_Click(object sender, EventArgs e)
        {
            if (Vanilla_Commandbar == "FOC")
            {
                Vanilla_Commandbar = "EAW";
                Render_Segment_Button(Button_EAW_Command_Bar, "Left", true, 85, 36, 22, 8, 12, Color_03, "EAW");
                // Setting other segment buttons to "inactive"
                Render_Segment_Button(Button_FOC_Command_Bar, "Right", false, 85, 36, 22, 8, 12, Color_04, "FOC");

                Button_Commandbar_Color_Blue.Visible = false;
            }
        }


        private void Button_FOC_Command_Bar_Click(object sender, EventArgs e)
        {
            if (Vanilla_Commandbar == "EAW")
            {
                Vanilla_Commandbar = "FOC";
                Render_Segment_Button(Button_EAW_Command_Bar, "Left", false, 85, 36, 22, 8, 12, Color_04, "EAW");
                // Setting other segment buttons to "inactive"
                Render_Segment_Button(Button_FOC_Command_Bar, "Right", true, 85, 36, 22, 8, 12, Color_03, "FOC");

                Button_Commandbar_Color_Blue.Visible = true;
            }
        }


        private void Button_Commandbar_Color_Blue_Click(object sender, EventArgs e)
        {   // Setting new Costum value:
            Costum_Commandbar = "Blue";
            Button_Reset_Vanilla_Commandbar_Click(null, null);
        }



        private void Button_Reset_Vanilla_Commandbar_Click(object sender, EventArgs e)
        {
            // Making sure no mod is loaded in the Map editor, otherwise this button would overwrite Commandfiles of that mod!
            Map_Editor_EAW = Load_Setting(Setting, "Map_Editor_EAW");
            Map_Editor_FOC = Load_Setting(Setting, "Map_Editor_FOC");

            if (Map_Editor_EAW != "" | Map_Editor_FOC != "")
            {
                Costum_Commandbar = "Vanilla_User_Interface";
                Imperial_Console(700, 100, Add_Line + "    The Map Editor is still loaded and blocks the process. Please launch"
                                         + Add_Line + "    any Mod or the Vanilla Game then quit the game and try again");
                return; // Exiting function
            }
            

            string Desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            /* 
            // Making sure the Command Bar directory exists - Moved Command bars into Main_Directory  
            if (!Directory.Exists(Program_Directory + @"Misc\Command_Bars"))
            {
                byte[] Archive = Imperialware.Properties.Resources.Command_Bars;

                // Temporally moving the archive to Desktop
                File.WriteAllBytes(Desktop + @"\Delete_Me_Please.zip", Archive);

                System.IO.Compression.ZipFile.ExtractToDirectory(Desktop + @"\Delete_Me_Please.zip", Program_Directory + "Misc");

                // Trashing after extraction, named the archive "Delete_Me_Please" for the case that its autodeletion fails
                Deleting(Desktop + @"\Delete_Me_Please.zip");
            }
            */


            string Current_Game_Path = Game_Path_EAW;
            if (Vanilla_Commandbar == "FOC") { Current_Game_Path = Game_Path_FOC; }

            // Clearig .tga files, because the Map Editor has the habit to delete them anyway -.-
            if (File.Exists(Current_Game_Path + @"Data\Art\Textures\Mt_Commandbar.tga")) { Deleting(Current_Game_Path + @"Data\Art\Textures\Mt_Commandbar.tga"); }




            // Overwiting existing Commandbar
            File.Copy(Program_Directory + @"Misc\Command_Bars\" + Vanilla_Commandbar + @"\" + Costum_Commandbar + @"\Mt_Commandbar.dds", Current_Game_Path + @"Data\Art\Textures\Mt_Commandbar.dds", true);
            File.Copy(Program_Directory + @"Misc\Command_Bars\" + Vanilla_Commandbar + @"\" + Costum_Commandbar + @"\Mt_Commandbar.mtd", Current_Game_Path + @"Data\Art\Textures\Mt_Commandbar.mtd", true);


            if (!Directory.Exists(Current_Game_Path + @"Data\Art\Textures\Game_Commandbar")) { Directory.CreateDirectory(Current_Game_Path + @"Data\Art\Textures\Game_Commandbar\"); }

            File.Copy(Program_Directory + @"Misc\Command_Bars\" + Vanilla_Commandbar + @"\" + Costum_Commandbar + @"\Mt_Commandbar.dds", Current_Game_Path + @"Data\Art\Textures\Game_Commandbar\Mt_Commandbar.dds", true);
            File.Copy(Program_Directory + @"Misc\Command_Bars\" + Vanilla_Commandbar + @"\" + Costum_Commandbar + @"\Mt_Commandbar.mtd", Current_Game_Path + @"Data\Art\Textures\Game_Commandbar\Mt_Commandbar.mtd", true);


            // Telling the User
            Imperial_Console(680, 100, Add_Line + "    Successfully loaded " + Costum_Commandbar + " as " + Vanilla_Commandbar + " Command Bar.");

            // Resetting Costum Commandbar value to the vanilla one
            Costum_Commandbar = "Vanilla_User_Interface";
        }




        private void Combo_Box_Dashboard_Mod_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This line just makes sure we use the same info directory for different version of the Stargate TPC Mod
            if (Combo_Box_Dashboard_Mod.Text == "StargateAdmin" | Combo_Box_Dashboard_Mod.Text == "StargateBeta" | Combo_Box_Dashboard_Mod.Text == "StargateOpenBeta")
            { Combo_Box_Dashboard_Mod.Text = "Stargate"; }

   

            Dashboard_Mod = Combo_Box_Dashboard_Mod.Text;
            string Dashboard_Directory = Program_Directory + @"Mods\" + Combo_Box_Dashboard_Mod.Text + @"\Dashboards\";



            // Removing all listed items from the list view in order to refresh
            List_View_Dashboards.Items.Clear();



            if (Directory.Exists(Dashboard_Directory))
            {
                // We put all found files inside of our target folder into a string table
                string[] The_Files = Directory.GetFiles(Dashboard_Directory);
                int FileCount = The_Files.Count();


                if (FileCount != 0)
                {   // Cycling up from 0 to the total count of folders found above in this directory;
                    for (int Cycle_Count = 0; Cycle_Count < FileCount; Cycle_Count = Cycle_Count + 1)
                    {
                        // Getting the Name only from all file paths, Cycle_Count increases by 1 in each cycle 
                        string List_Value = Path.GetFileName(The_Files[Cycle_Count]);

                        // Inserting that folder name into the List View box                    
                        var Item = List_View_Dashboards.Items.Add(List_Value.Remove(List_Value.Length - 4));


                        Item.ForeColor = Color.Black;

                        // Every second item should have this value in order to create a checkmate pattern with good contrast
                        if (Checkmate_Color == "true" & Item.Index % 2 == 0)
                        {
                            Item.ForeColor = Color_03;
                            Item.BackColor = Color_07;
                        }
                    }

                    Label_Dashboard_Info.Text = ""; // Removing explanation text
                }
                else // Probably the path is wrong
                {
                    List_View_Dashboards.Items.Add("Rebel");
                    List_View_Dashboards.Items.Add("Empire");
                    List_View_Dashboards.Items.Add("Underworld");

                    Label_Dashboard_Info.Text = "This Mod has no Dashboard thumbnail images.";
                }
            }
            else
            {
                List_View_Dashboards.Items.Add("Rebel");
                List_View_Dashboards.Items.Add("Empire");
                List_View_Dashboards.Items.Add("Underworld");

                Label_Dashboard_Info.Text = "This Mod has no Dashboard thumbnail images.";
            }


            // We are going to select the first one right away
            if (List_View_Dashboards.Items.Count > 0)
            {
                List_View_Dashboards.FocusedItem = List_View_Dashboards.Items[0];
                List_View_Dashboards.Items[0].Selected = true;
                List_View_Dashboards.Select();
                // List_View_Dashboards.EnsureVisible(0); 
            }

        }


        private void Combo_Box_Target_Faction_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This faction will be used to name the dashboard after it.
            if (Combo_Box_Target_Faction.Text == "Rebel") { Dashboard_Faction = "Faction_1"; }
            else if (Combo_Box_Target_Faction.Text == "Empire") { Dashboard_Faction = "Faction_2"; }
            else if (Combo_Box_Target_Faction.Text == "Underworld") { Dashboard_Faction = "Faction_3"; }
            else if (Combo_Box_Target_Faction.Text == "Other") { Dashboard_Faction = "Faction_3"; }
        }


        private void List_View_Dashboards_SelectedIndexChanged(object sender, EventArgs e)
        {
            // This line just makes sure we use the same info directory for different version of the Stargate TPC Mod
            if (Combo_Box_Dashboard_Mod.Text == "StargateAdmin" | Combo_Box_Dashboard_Mod.Text == "StargateBeta" | Combo_Box_Dashboard_Mod.Text == "StargateOpenBeta")
            { Combo_Box_Dashboard_Mod.Text = "Stargate"; }



            string Dashboard_Directory = Program_Directory + @"\Mods\" + Combo_Box_Dashboard_Mod.Text + @"\Dashboards\";

            // IMPORTRANT: This check prevents a timing exception!
            if (List_View_Dashboards.SelectedItems.Count > 0)
            {
                if (List_View_Dashboards.SelectedItems[0].Text != "")
                { Selected_Dashboard = List_View_Dashboards.SelectedItems[0].Text; }
            }

            try
            {   // Setting the Tombnail image according to User selection           
                Picture_Box_Dashboard_Preview.Image = Resize_Image(Dashboard_Directory, Selected_Dashboard + ".png", 768, 233);
            }   // Otherwise we try .jpg
            catch { Picture_Box_Dashboard_Preview.Image = Resize_Image(Dashboard_Directory, Selected_Dashboard + ".jpg", 768, 233); }
        }




        private void Button_Apply_Dashboard_Click(object sender, EventArgs e)
        {
            // Setting source of Dashboards from the Art directory of the selected Mod
            string Dashboard_Directory = Game_Path + Mods_Directory + Dashboard_Mod + @"\Data\Art\Textures\";
            string Dashboard_Mode = "Vanilla_Models";

            string Sourceboard_Wide = "I_Galactic_Dashboard_Wide";
            string Sourceboard_Skirmish = "I_Galactic_Dashboard_Skirmish";




            // Imperialware is going to look for these Files in the Source Mod, and copy them over to the Target mod:
            if (Selected_Dashboard == "Rebel")
            {
                Sourceboard_Wide = "I_Galactic_Dashboard_Wide_Rebel";
                Sourceboard_Skirmish = "I_Galactic_Dashboard_Skrimish_Rebel";
            }
            else if (Selected_Dashboard == "Underworld")
            {
                Sourceboard_Wide = "I_Galactic_Dashboard_Wide_Under.dds";
                Sourceboard_Skirmish = "I_Galactic_Dashboard_Skrimish_Under.dds";
            }

            // Setting Targets according to the decisions above
            string Targetboard_Wide = Sourceboard_Wide;
            string Targetboard_Skirmish = Sourceboard_Skirmish;



            // If existing in Imperialwares Programdirectory, the sources will be redirected and used from there, otherwise its the paths above.
            if (Directory.Exists(Program_Directory + @"Mods\" + Dashboard_Mod + @"\Dashboards\" + Selected_Dashboard + @"\"))
            {
                Dashboard_Directory = Program_Directory + @"Mods\" + Dashboard_Mod + @"\Dashboards\" + Selected_Dashboard + @"\";
                Dashboard_Mode = "Imperialware";
                Targetboard_Wide = "I_Galactic_Dashboard_" + Dashboard_Faction + "_Wide";
                Targetboard_Skirmish = "I_Galactic_Dashboard_" + Dashboard_Faction + "_Skirmish";
            }




            // Making sure the Dashboard Model is patched  
            Overwrite_Copy(Program_Directory + @"Misc\Dashboard_Models\" + Dashboard_Mode + @"\I_Galactic_Controls.alo", Art_Directory + @"Models\" + "I_Galactic_Controls.alo");
            Overwrite_Copy(Program_Directory + @"Misc\Dashboard_Models\" + Dashboard_Mode + @"\I_Tactical_Controls.alo", Art_Directory + @"Models\" + "I_Tactical_Controls.alo");




            try // Copying the desired dashboard to the desired Faction and overwriting older Dashboard files of Imperialware
            {
                Overwrite_Copy(Dashboard_Directory + Sourceboard_Wide + ".dds", Art_Directory + @"Textures\" + Targetboard_Wide + ".dds");
                Overwrite_Copy(Dashboard_Directory + Sourceboard_Wide + ".dds", Art_Directory + @"Textures\" + Targetboard_Wide + ".dds");
            }
            catch
            {
                Overwrite_Copy(Dashboard_Directory + Sourceboard_Wide + ".tga", Art_Directory + @"Textures\" + Targetboard_Wide + ".tga");
                Overwrite_Copy(Dashboard_Directory + Sourceboard_Wide + ".tga", Art_Directory + @"Textures\" + Targetboard_Wide + ".tga");
            }


            try
            {
                Overwrite_Copy(Dashboard_Directory + Sourceboard_Skirmish + ".dds", Art_Directory + @"Textures\" + Targetboard_Skirmish + ".dds");
                Overwrite_Copy(Dashboard_Directory + Sourceboard_Skirmish + ".dds", Art_Directory + @"Textures\" + Targetboard_Skirmish + ".dds");
            }
            catch
            {
                Overwrite_Copy(Dashboard_Directory + Sourceboard_Skirmish + ".tga", Art_Directory + @"Textures\" + Targetboard_Skirmish + ".tga");
                Overwrite_Copy(Dashboard_Directory + Sourceboard_Skirmish + ".tga", Art_Directory + @"Textures\" + Targetboard_Skirmish + ".tga");
            }


            // Saving Mod (part of the path) and name of the Dashboard together.
            Save_Setting(Setting, "Dashboard_Mod", Dashboard_Mod);
            Save_Setting(Setting, "Selected_Dashboard", Selected_Dashboard);
        }



        private void Button_Restore_Dashboards_Click(object sender, EventArgs e)
        {
            Imperial_Dialogue(520, 160, "Reset Settings", "Cancel", "false", Add_Line + "    Are you sure you wish to apply Vanilla Dashboards"
                                                                                      + Add_Line + "    to the selected Mod?");

            // If user verificated to reset all functions 
            if (Caution_Window.Passed_Value_A.Text_Data == "true")
            {
                // Restoring the broken Vanilla Dashboards
                Overwrite_Copy(Program_Directory + @"Misc\Dashboard_Models\Vanilla_Models\I_Galactic_Controls.alo", Art_Directory + @"Models\" + "I_Galactic_Controls.alo");
                Overwrite_Copy(Program_Directory + @"Misc\Dashboard_Models\Vanilla_Models\I_Tactical_Controls.alo", Art_Directory + @"Models\" + "I_Tactical_Controls.alo");
            }
        }

        private void Label_Galactic_Alo_Click(object sender, EventArgs e)
        {
            try
            {
                string The_File = Art_Directory + @"Models\I_Galactic_Controls.alo";
                System.Diagnostics.Process.Start(The_File);
            }
            catch
            {
                Imperial_Console(540, 100, Add_Line + "Error; Maybe you need to assign .alo files"
                                         + Add_Line + "to the Alo Viewer tool in"
                                         + Add_Line + @".\Imperialware_Directory\Misc\Modding_Tools");
            }

        }

        private void Label_Tactical_Alo_Click(object sender, EventArgs e)
        {
            try
            {
                string The_File = Art_Directory + @"Models\I_Tactical_Controls.alo";
                System.Diagnostics.Process.Start(The_File);
            }
            catch
            {
                Imperial_Console(540, 100, Add_Line + "Error; Maybe you need to assign .alo files"
                                         + Add_Line + "to the Alo Viewer tool in"
                                         + Add_Line + @".\Imperialware_Directory\Misc\Modding_Tools");
            }
        }







        private void Button_Set_Folder_Path_Click(object sender, EventArgs e)
        {
            if (Text_Box_Folder_Path.Text != "")
            {
                // Storing original directory Value, needed for moving
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware", "Old_Program_Directory", Program_Directory);


            
                Program_Directory = Text_Box_Folder_Path.Text;

                if (!Program_Directory.EndsWith(@"\")) 
                {   Program_Directory = Text_Box_Folder_Path.Text + @"\";
                    Text_Box_Folder_Path.Text = Program_Directory;
                }

                // Storing new value:
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware", "Program_Directory", Program_Directory);
                // Save_Setting("0", "Program_Directory", Text_Box_Folder_Path.Text);


                // Need to restart because otherwise we can't move the Program Directory away.
                Application.Restart();
            }
        }

        private void Button_Open_Folder_Path_Click(object sender, EventArgs e)
        {
            try { System.Diagnostics.Process.Start(Program_Directory); }
            catch
            {
                Imperial_Console(720, 100, Add_Line + @"    Could not find " + Program_Directory 
                    + Add_Line + @"    Please try moving it to a other place, press reset or type manually:"
                    + Add_Line + @"    C:\Program Files\Imperialware\ And hit the move button." );
            }
        }


        private void Button_Reset_Folder_Path_Click(object sender, EventArgs e)
        {           
            Text_Box_Folder_Path.Text = @"C:\Program Files\Imperialware\";
            Button_Set_Folder_Path_Click(null, null);           
        }



        private void Button_Expand_Program_Info_Click(object sender, EventArgs e)
        {
            int Expansion_Value = 574;

            if (Toggle_Text_Info)
            {
                ToggleControlY(Text_Box_Info, null, Move_List_Info_Text, -Expansion_Value);
                Picture_Box_License.Visible = false;
                Label_Info_Text.Visible = true;
                Toggle_Text_Info = false;
                Text_Box_Info.Text = "";
            }

            else
            {
                ToggleControlY(Text_Box_Info, null, Move_List_Info_Text, Expansion_Value);
                Picture_Box_License.Visible = true;
                Label_Info_Text.Visible = false;
                Toggle_Text_Info = true;
                Text_Box_Info.Text =
                      @"
        Imperialware is an open Mod Manager project under the 
    Attribution-NonCommercial-ShareAlike 4.0 International
    License, written and designed by Imperial.
    https://creativecommons.org/licenses/by-nc-sa/4.0/
    https://creativecommons.org/licenses/by-nc-sa/4.0/legalcode
    
    Which means you are free to share, copy and redistribute 
    Imperialware. You can adapt, transform and build your own 
    code upon it and also release own devirations of Imperialware
    together with your own mods.

    Please just don't use it for any commercial purposes and it has to 
    preserve this license. Please also keep ""Imperialware"" in the 
    custom name of own devirations.

    -/ Thanks to:
    - Everyone who uploads free image resources to pixabay,  
    as some of them were used for this program.
    - All Beta testers.
  
    
    -/ History:
    Originally I just needed a program UI to apply my LUA script 
    cheats to Star Wars - Empire at War: Forces of Corruption.
    
    I wanted to extend that by a Universal Launcher feature.
    And by the time more and more features were added until
    the software has grown to a full Mod manager program 
    with Xml Unit creator features for the game 
    Star Wars - Empire at War.
    ";

            }

        }

        private void Button_Expand_Program_Info_MouseHover(object sender, EventArgs e)
        { Toggle_Button(Button_Expand_Program_Info, "Button_Info", "Button_Info_Highlighted", 0, false); }

        private void Button_Expand_Program_Info_MouseLeave(object sender, EventArgs e)
        { Toggle_Button(Button_Expand_Program_Info, "Button_Info_Highlighted", "Button_Info", 0, false); }

       



        private void Button_Expand_D_Click(object sender, EventArgs e)
        {
            List<Control> Move_List_D = new List<Control>() { Button_Reset_All_Settings, Button_Withdraw_Files, Button_Uninstall_Imperialware};

            int Expansion_Value = 1670;

            // If our expander button property is true = open, we toggle it to close
            if (Size_Expander_D == "true")
            {   // This Button does the same as the one above, have a look at its comments
               
                ToggleControlY(Group_Box_Max_Values, null, Move_List_D, -Expansion_Value);

                Button_Expand_D.Text = "+";
                Save_Setting(Setting, "Size_Expander_D", "false");
            }
            else if (Size_Expander_D == "false")
            {                
                ToggleControlY(Group_Box_Max_Values, null, Move_List_D, Expansion_Value);

                Button_Expand_D.Text = "-";
                Save_Setting(Setting, "Size_Expander_D", "true");

                // Refreshing Max values to the currently selected Mod
                Set_Maximal_Value_Directories();
            }
        }


        //====================== Editing Maximal Values ====================== 

        private void Button_Class_1_Click(object sender, EventArgs e)
        {   Save_Setting(Setting, "Maximal_Value_Class", "1");
            Load_Maximal_Values(Maximum_Values_Fighter);
            Set_Class_Button();
        }

        private void Button_Class_2_Click(object sender, EventArgs e)
        {   Save_Setting(Setting, "Maximal_Value_Class", "2");
            Load_Maximal_Values(Maximum_Values_Bomber);
            Set_Class_Button();
        }

        private void Button_Class_3_Click(object sender, EventArgs e)
        {   Save_Setting(Setting, "Maximal_Value_Class", "3");
            Load_Maximal_Values(Maximum_Values_Corvette);
            Set_Class_Button();
        }

        private void Button_Class_4_Click(object sender, EventArgs e)
        {   Save_Setting(Setting, "Maximal_Value_Class", "4");
            Load_Maximal_Values(Maximum_Values_Frigate);
            Set_Class_Button();
        }

        private void Button_Class_5_Click(object sender, EventArgs e)
        {   Save_Setting(Setting, "Maximal_Value_Class", "5");
            Load_Maximal_Values(Maximum_Values_Capital);
            Set_Class_Button();
        }


        // ====== Ground Units and Spacebuildings + Groundbuildings ====== //
        private void Button_Class_6_Click(object sender, EventArgs e)
        {   Save_Setting(Setting, "Maximal_Value_Class", "6");
            Load_Maximal_Values(Maximum_Values_Infantry);
            Set_Class_Button();
        }

        private void Button_Class_7_Click(object sender, EventArgs e)
        {   Save_Setting(Setting, "Maximal_Value_Class", "7");
            Load_Maximal_Values(Maximum_Values_Vehicle);
            Set_Class_Button();
        }

        private void Button_Class_8_Click(object sender, EventArgs e)
        {   Save_Setting(Setting, "Maximal_Value_Class", "8");
            Load_Maximal_Values(Maximum_Values_Air);
            Set_Class_Button();
        }

        private void Button_Class_9_Click(object sender, EventArgs e)
        {   Save_Setting(Setting, "Maximal_Value_Class", "9");
            Load_Maximal_Values(Maximum_Values_Hero);
            Set_Class_Button();
        }

        private void Button_Class_10_Click(object sender, EventArgs e)
        {   Save_Setting(Setting, "Maximal_Value_Class", "10");
            Load_Maximal_Values(Maximum_Values_Structure);
            Set_Class_Button();
        }
        // ======================== //

        private void Text_Box_Max_Model_Scale_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Model_Scale", Maximum_Model_Scale); }

        private void Text_Box_Max_Model_Height_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Model_Height", Maximum_Model_Height); }

        private void Text_Box_Max_Select_Box_Scale_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Select_Box_Scale", Maximum_Select_Box_Scale); }

        private void Text_Box_Max_Health_Bar_Height_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Health_Bar_Height", Maximum_Health_Bar_Height); }
         
        private void Text_Box_Max_Credits_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Credits", Maximum_Credits); }
  
        private void Text_Box_Max_Hull_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Hull", Maximum_Hull); }

        private void Text_Box_Max_Shield_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Shield", Maximum_Shield); }          

        private void Text_Box_Max_Shield_Rate_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Shield_Rate", Maximum_Shield_Rate); }
           
        private void Text_Box_Max_Energy_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Energy", Maximum_Energy); }
      
        private void Text_Box_Max_Energy_Rate_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Energy_Rate", Maximum_Energy_Rate); }


        private void Text_Box_Max_Speed_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Speed", Maximum_Speed); }

        private void Text_Box_Max_AI_Combat_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_AI_Combat", Maximum_AI_Combat); }    

        private void Text_Box_Max_Projectile_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Projectile", Maximum_Projectile); }




        private void Text_Box_Max_Build_Cost_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Build_Cost", Maximum_Build_Cost); }

        private void Text_Box_Max_Skirmish_Cost_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Skirmish_Cost", Maximum_Skirmish_Cost); }


        private void Text_Box_Max_Slice_Cost_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Slice_Cost", Maximum_Slice_Cost); }

        private void Text_Box_Max_Build_Time_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Build_Time", Maximum_Build_Time); }


        private void Text_Box_Max_Tech_Level_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Tech_Level", Maximum_Tech_Level); }

        private void Text_Box_Max_Timeline_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Timeline", Maximum_Timeline); }


        private void Text_Box_Max_Star_Base_LV_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Star_Base_LV", Maximum_Star_Base_LV); }

        private void Text_Box_Max_Ground_Base_LV_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Ground_Base_LV", Maximum_Ground_Base_LV); }


        private void Text_Box_Max_Build_Limit_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Build_Limit", Maximum_Build_Limit); }

        private void Text_Box_Max_Lifetime_Limit_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Maximum_Lifetime_Limit", Maximum_Lifetime_Limit); }




        private void Text_Box_Max_Costum_4_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Costum_Tag_4_Max_Value", Costum_Tag_4_Max_Value); }


        private void Text_Box_Max_Costum_5_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Costum_Tag_5_Max_Value", Costum_Tag_5_Max_Value); }


        private void Text_Box_Max_Costum_6_TextChanged(object sender, EventArgs e)
        { Set_Maximal_Values((TextBox)sender, "Costum_Tag_6_Max_Value", Costum_Tag_6_Max_Value); }


        
        private void Button_Restore_Default_Values_Click(object sender, EventArgs e)
        {   // Setting Maximal Values of Space Units:
            // Fighter
            if (Maximal_Value_Class == "1")
            {   Maximum_Model_Scale = 3;
                Maximum_Model_Height = 100;
                Maximum_Select_Box_Scale = 300;
                Maximum_Health_Bar_Height = 1;
                Maximum_Credits = 10000;
                Maximum_Hull = 100;
                Maximum_Shield = 100;
                Maximum_Shield_Rate = 10;
                Maximum_Energy = 600;
                Maximum_Energy_Rate = 300;
                Maximum_Speed = 6;
                Maximum_AI_Combat = 100;
                Maximum_Projectile = 30;
               
                Maximum_Build_Cost = 1000;
                Maximum_Skirmish_Cost = 2000;
                Maximum_Slice_Cost = 3000;
                Maximum_Build_Time = 20;
                Maximum_Build_Limit = 16;
                Maximum_Lifetime_Limit = 16;
                Maximum_Tech_Level = 6;
                Maximum_Star_Base_LV = 5;
                Maximum_Ground_Base_LV = 5;
                Maximum_Timeline = 5;

            }
            // Bomber
            else if (Maximal_Value_Class == "2")
            {   Maximum_Model_Scale = 3;
                Maximum_Model_Height = 100;
                Maximum_Select_Box_Scale = 300;
                Maximum_Health_Bar_Height = 1;
                Maximum_Credits = 10000;
                Maximum_Hull = 100;
                Maximum_Shield = 100;
                Maximum_Shield_Rate = 10;
                Maximum_Energy = 600;
                Maximum_Energy_Rate = 300;
                Maximum_Speed = 4;
                Maximum_AI_Combat = 150;
                Maximum_Projectile = 60;
               
                Maximum_Build_Cost = 2000;
                Maximum_Skirmish_Cost = 2000;
                Maximum_Slice_Cost = 3000;
                Maximum_Build_Time = 20;
                Maximum_Build_Limit = 16;
                Maximum_Lifetime_Limit = 16;
                Maximum_Tech_Level = 6;
                Maximum_Star_Base_LV = 5;
                Maximum_Ground_Base_LV = 5;
                Maximum_Timeline = 5;
            }

             // Corvette
            else if (Maximal_Value_Class == "3") 
            {   Maximum_Model_Scale = 2;
                Maximum_Model_Height = 300;
                Maximum_Select_Box_Scale = 300;
                Maximum_Health_Bar_Height = 1;
                Maximum_Credits = 10000;
                Maximum_Hull = 2000;
                Maximum_Shield = 2000;
                Maximum_Shield_Rate = 30;
                Maximum_Energy = 6000;
                Maximum_Energy_Rate = 600;
                Maximum_Speed = 3;
                Maximum_AI_Combat = 1000;
                Maximum_Projectile = 30;

                Maximum_Build_Cost = 2000;
                Maximum_Skirmish_Cost = 3000;
                Maximum_Slice_Cost = 3000;
                Maximum_Build_Time = 30;
                Maximum_Build_Limit = 16;
                Maximum_Lifetime_Limit = 16;
                Maximum_Tech_Level = 6;
                Maximum_Star_Base_LV = 5;
                Maximum_Ground_Base_LV = 5;
                Maximum_Timeline = 5;
            }
            // Firgate
            else if (Maximal_Value_Class == "4")
            {   Maximum_Model_Scale = 2;
                Maximum_Model_Height = 400;
                Maximum_Select_Box_Scale = 600;
                Maximum_Health_Bar_Height = 1;
                Maximum_Credits = 10000;
                Maximum_Hull = 3000;
                Maximum_Shield = 3000;
                Maximum_Shield_Rate = 40;
                Maximum_Energy = 8000;
                Maximum_Energy_Rate = 800;
                Maximum_Speed = 3;
                Maximum_AI_Combat = 2000;
                Maximum_Projectile = 40;

                Maximum_Build_Cost = 4000;
                Maximum_Skirmish_Cost = 4000;
                Maximum_Slice_Cost = 4000;
                Maximum_Build_Time = 40;
                Maximum_Build_Limit = 16;
                Maximum_Lifetime_Limit = 16;
                Maximum_Tech_Level = 6;
                Maximum_Star_Base_LV = 5;
                Maximum_Ground_Base_LV = 5;
                Maximum_Timeline = 5;
            }
            // Capital
            else if (Maximal_Value_Class == "5")
            {   Maximum_Model_Scale = 2;
                Maximum_Model_Height = 500;
                Maximum_Select_Box_Scale = 1000;
                Maximum_Health_Bar_Height = 2;
                Maximum_Credits = 10000;
                Maximum_Hull = 4000;
                Maximum_Shield = 4000;
                Maximum_Shield_Rate = 60;
                Maximum_Energy = 10000;
                Maximum_Energy_Rate = 1000;
                Maximum_Speed = 2;
                Maximum_AI_Combat = 3000;
                Maximum_Projectile = 50;

                Maximum_Build_Cost = 5000;
                Maximum_Skirmish_Cost = 6000;
                Maximum_Slice_Cost = 4000;
                Maximum_Build_Time = 60;
                Maximum_Build_Limit = 16;
                Maximum_Lifetime_Limit = 16;
                Maximum_Tech_Level = 6;
                Maximum_Star_Base_LV = 5;
                Maximum_Ground_Base_LV = 5;
                Maximum_Timeline = 5;
            }


            // =========== Ground Units =========== 
            // Infantry
            else if (Maximal_Value_Class == "6")
            {   Maximum_Model_Scale = 2;
                Maximum_Model_Height = 100;
                Maximum_Select_Box_Scale = 300;
                Maximum_Health_Bar_Height = 1;
                Maximum_Credits = 10000;
                Maximum_Hull = 1000;
                Maximum_Shield = 1000;
                Maximum_Shield_Rate = 20;
                Maximum_Energy = 1000;
                Maximum_Energy_Rate = 100;
                Maximum_AI_Combat = 300;
                Maximum_Projectile = 20;
                Maximum_Speed = 60;

                Maximum_Build_Cost = 800;
                Maximum_Skirmish_Cost = 1000;
                Maximum_Slice_Cost = 1000;
                Maximum_Build_Time = 20;
                Maximum_Build_Limit = 20;
                Maximum_Lifetime_Limit = 20;
                Maximum_Tech_Level = 6;
                Maximum_Star_Base_LV = 5;
                Maximum_Ground_Base_LV = 5;
                Maximum_Timeline = 5;           
            }
            // Vehicle
            else if (Maximal_Value_Class == "7")
            {   Maximum_Model_Scale = 2;
                Maximum_Model_Height = 200;
                Maximum_Select_Box_Scale = 400;
                Maximum_Health_Bar_Height = 1;
                Maximum_Credits = 10000;
                Maximum_Hull = 1000;
                Maximum_Shield = 1000;
                Maximum_Shield_Rate = 40;
                Maximum_Energy = 1000;
                Maximum_Energy_Rate = 100;
                Maximum_AI_Combat = 300;
                Maximum_Projectile = 30;
                Maximum_Speed = 60;

                Maximum_Build_Cost = 2000;
                Maximum_Skirmish_Cost = 2000;
                Maximum_Slice_Cost = 2000;
                Maximum_Build_Time = 60;
                Maximum_Build_Limit = 16;
                Maximum_Lifetime_Limit = 16;
                Maximum_Tech_Level = 6;
                Maximum_Star_Base_LV = 5;
                Maximum_Ground_Base_LV = 5;
                Maximum_Timeline = 5;                      
            }
            // Air
            else if (Maximal_Value_Class == "8")
            {   Maximum_Model_Scale = 2;
                Maximum_Model_Height = 100;
                Maximum_Select_Box_Scale = 300;
                Maximum_Health_Bar_Height = 1;
                Maximum_Credits = 10000;
                Maximum_Hull = 100;
                Maximum_Shield = 100;
                Maximum_Shield_Rate = 10;
                Maximum_Energy = 600;
                Maximum_Energy_Rate = 300;
                Maximum_AI_Combat = 150;
                Maximum_Projectile = 60;
                Maximum_Speed = 40;

                Maximum_Build_Cost = 2000;
                Maximum_Skirmish_Cost = 2000;
                Maximum_Slice_Cost = 3000;
                Maximum_Build_Time = 20;
                Maximum_Build_Limit = 16;
                Maximum_Lifetime_Limit = 16;
                Maximum_Tech_Level = 6;
                Maximum_Star_Base_LV = 5;
                Maximum_Ground_Base_LV = 5;
                Maximum_Timeline = 5;           
            }
            // Hero
            else if (Maximal_Value_Class == "9")
            {   Maximum_Model_Scale = 2;
                Maximum_Model_Height = 500;
                Maximum_Select_Box_Scale = 800;
                Maximum_Health_Bar_Height = 2;
                Maximum_Credits = 10000;
                Maximum_Hull = 100;
                Maximum_Shield = 100;
                Maximum_Shield_Rate = 10;
                Maximum_Energy = 600;
                Maximum_Energy_Rate = 300;
                Maximum_AI_Combat = 150;
                Maximum_Projectile = 60;
                Maximum_Speed = 30;

                Maximum_Build_Cost = 3000;
                Maximum_Skirmish_Cost = 3000;
                Maximum_Slice_Cost = 3000;
                Maximum_Build_Time = 40;
                Maximum_Build_Limit = 16;
                Maximum_Lifetime_Limit = 16;
                Maximum_Tech_Level = 6;
                Maximum_Star_Base_LV = 5;
                Maximum_Ground_Base_LV = 5;
                Maximum_Timeline = 5;
            }
            // Structure
            else if (Maximal_Value_Class == "10")
            { 
                Maximum_Model_Scale = 2;
                Maximum_Model_Height = 200;
                Maximum_Select_Box_Scale = 800;
                Maximum_Health_Bar_Height = 2;
                Maximum_Credits = 10000;
                Maximum_Hull = 100;
                Maximum_Shield = 100;
                Maximum_Shield_Rate = 10;
                Maximum_Energy = 600;
                Maximum_Energy_Rate = 300;
                Maximum_AI_Combat = 150;
                Maximum_Projectile = 60;
                Maximum_Speed = 30;

                Maximum_Build_Cost = 6000;
                Maximum_Skirmish_Cost = 8000;
                Maximum_Slice_Cost = 8000;
                Maximum_Build_Time = 100;
                Maximum_Build_Limit = 200;
                Maximum_Lifetime_Limit = 200;
                Maximum_Tech_Level = 6;
                Maximum_Star_Base_LV = 5;
                Maximum_Ground_Base_LV = 5;
                Maximum_Timeline = 5;
            }

            Costum_Tag_4_Max_Value = 3000;
            Costum_Tag_5_Max_Value = 3000;
            Costum_Tag_6_Max_Value = 3000;



            // ================= Resetting Maximal Value Text Boxes ================= //
            // Vielleicht setzt es die Variable automatisch wenn du nur hier den Text einfügst?

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
     
        }
        //====================== End of Maximal Values ====================== 


        private void Button_Reset_All_Settings_Click(object sender, EventArgs e)
        {
            Imperial_Dialogue(540, 160, "Reset Settings", "Cancel", "false", Add_Line + "    Are you sure you wish to factory reset all settings?");

            // If user verificated to reset all functions 
            if (Caution_Window.Passed_Value_A.Text_Data == "true")
            {   // We use the Deleting costum function to move the settings file to trash
                Deleting(Setting);
               
                /*
                // Creating a fresh setting file
                byte[] Settings = Imperialware.Properties.Resources.Settings;
                File.WriteAllBytes(Setting, Settings);

                // Reloading the whole application
                MainWindow_Load(null, null);
                */

                // Setting back to false to prevent missfiring when user closes the verification dialogue next time.
                Caution_Window.Passed_Value_A.Text_Data = "false";

                Application.Restart();
            }                 
        }

        private void Button_Withdraw_Files_Click(object sender, EventArgs e)
        {
            Imperial_Dialogue(560, 160, "Remove", "Cancel", "false", Add_Line + "    Are you sure you wish to remove all Imperialware Files"
                                                                   + Add_Line + "    from " + Mod_Name + "?");

            // If user verificated to reset all functions 
            if (Caution_Window.Passed_Value_A.Text_Data == "true")
            {   // We use the Deleting costum function to move the settings file to trash
                Deleting(Xml_Directory + "Core");
                Deleting(Data_Directory + @"Scripts\GameObject\Story_Cheating.lua");

                // Setting back to false to prevent missfiring when user closes the verification dialogue next time.
                Caution_Window.Passed_Value_A.Text_Data = "false";
            }
        }

        private void Button_Uninstall_Imperialware_Click(object sender, EventArgs e)
        {
            Imperial_Dialogue(560, 160, "Uninstall", "Cancel", "false", Add_Line + "    Are you sure you wish to uninstall Imperialware?");

            // If user verificated to reset all functions 
            if (Caution_Window.Passed_Value_A.Text_Data == "true")
            {   // We use the Deleting costum function to move the settings file to trash
                // Deleting(Xml_Directory + "Core"); 
                // Deleting(Data_Directory + @"Scripts\GameObject\Story_Cheating.lua");


                Refresh_Mods_Click(null, null);

                foreach (string Mod_Dir in List_Box_Mod_Selection.Items)
                {
                    try { Deleting(Game_Path + Mods_Directory + Mod_Dir + @"\Data\Xml\Core"); } catch { }
                    try { Deleting(Game_Path + Mods_Directory + Mod_Dir + @"\Data\Scripts\GameObject\Story_Cheating.lua"); } catch { }
                }

                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Imperialware", "Old_Program_Directory", "Delete_It");


                // Setting back to false to prevent missfiring when user closes the verification dialogue next time.
                Caution_Window.Passed_Value_A.Text_Data = "false";

                // Need to restart because otherwise we can't trash the Program Directory.
                Application.Restart();
            }
        }

        //================================================ Closing Sequence ================================================  


        // Playing Sound for Tab selection
        private void Tab_Control_01_Selecting(object sender, TabControlCancelEventArgs e)
        {  
            if (Loading_Completed)
            {   try // Playing Tab
                {   System.Media.SoundPlayer Select_Tab = new System.Media.SoundPlayer(Selected_Theme + @"Select_Tab.wav");
                    Select_Tab.Play();
                }
                catch // If not existing in selected theme we take the default one
                {   try
                    {   System.Media.SoundPlayer Select_Tab = new System.Media.SoundPlayer(Program_Directory + @"Themes\Default\Select_Tab.wav");
                        Select_Tab.Play();
                    } catch { }
                }
            }          
        }

        // Setting the Last Tab
        private void Tab_Control_01_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tab_Control_01.SelectedIndex == 1)
            {  if (Play_Vanilla_Game == "false") { Label_Mod_Name.Text = Mod_Name;}               
               Label_Addon_Name.Text = Loaded_Addon;  
            }

            // Displaying all known Mods/Addons in the Apps tab
            else if (Tab_Control_01.SelectedIndex == 2) { MainWindow_ResizeEnd(null, null); }

            else if (Tab_Control_01.SelectedIndex == 3)
            {               
                if (Loaded_Recent_Units == 0) 
                {   // Loading rescent entries, depending on last session
                    if (Planet_Switch == "Galaxy") {Load_Last_Galaxy();}
                    // If not "Galaxy" that means it stores the name of the .xml that contains the recent Planet list!
                    else if (Planet_Switch != "Galaxy") { Load_Last_Planets(); }

                    Load_Recent_Units();
                    if (Load_Rescent_File == "true")
                    {   User_Input = false;
                        Load_Unit_To_UI(Selected_Xml, Selected_Instance); 
                        User_Input = true;
                        Loaded_Recent_Units = 1;
                    } 
                }
            } // Text_Box_Name.Focus(); 

            else if (Tab_Control_01.SelectedIndex == 4)
            {              
                if (Loaded_Xmls == 0) 
                { 
                    Get_Xmls(); Loaded_Xmls = 1;

                    // Loading last Xml into the Editor
                    if (Load_Rescent_File == "true" & Last_Editor_Xml != "")
                    {   // Making sure the last Editor Xml loads (once only)                       
                        Load_Xml_File(Last_Editor_Xml);
                        Selected_Xml = Path.GetFileName(Last_Editor_Xml);
                    }               
                }
                
            } // Loading the Xmls only for the first time

            else if (Tab_Control_01.SelectedIndex == 5)
            {   if (Size_Expander_D == "true") { Set_Maximal_Value_Directories(); }
                Label_Mod_Name_2.Text = Mod_Name;              
            } 
  
            else if (Tab_Control_01.SelectedIndex == 6) { Refresh_Theme_List(); }         
         
        }
       

        private void Manage_Scroll(object sender, ScrollEventArgs e)
        {
            // specify intervall of milliseconds
            Clock.Interval = 200; 
            Clock.Tick += new EventHandler(Timer_Tick_Scroll);
            Clock.Start();
            Last_Scroll_Value = Manage.HorizontalScroll.Value;        
        }
    
        void Timer_Tick_Scroll(object sender, EventArgs e)
        {
            // Making sure these buttons stay in their position when scrolling, because they shoud always be accessible!
            Mini_Button_Save.Location = new Point(8, 8);
            Mini_Button_Save_As.Location = new Point(this.Size.Width - 68, 8);

            Clock.Stop();
        }

        private void Manage_Click(object sender, EventArgs e)
        {   Mini_Button_Save.Location = new Point(8, 8);
            Mini_Button_Save_As.Location = new Point(this.Size.Width - 68, 8);
            // Resetting scroll value, to prevent the form from jumping around when the User focuses the program again
            Manage.HorizontalScroll.Value = Last_Scroll_Value;
        }
      
        //====================== Keyboard Control Capturing ====================== 
        private void Text_Box_Object_Searchbar_TextChanged(object sender, EventArgs e)
        {
            ActiveForm.AcceptButton = Button_Search_Object;
        }



        //====================== Window Size/Location changes ====================== 
        private void MainWindow_ResizeEnd(object sender, EventArgs e)
        {
            if (Tab_Control_01.SelectedIndex == 2) 
            {
                // I also took + 280 costum treshold of my UI layout that shifts it to the right
                int Treshold_Shift = 280;
                int Button_Size = 130;

                // These values are increasing all 130 pixels of the Form size change, to rasterize where the next row is supposed to appear. 
                int[] Values = { Treshold_Shift + Button_Size, Treshold_Shift + (Button_Size * 2), Treshold_Shift + (Button_Size * 3), Treshold_Shift + (Button_Size * 4), Treshold_Shift + (Button_Size * 5), 
                          Treshold_Shift + (Button_Size * 6), Treshold_Shift + (Button_Size * 7), Treshold_Shift +  (Button_Size * 8), Treshold_Shift + (Button_Size * 9), Treshold_Shift + (Button_Size * 10), 
                          Treshold_Shift + (Button_Size * 11), Treshold_Shift + (Button_Size * 12), Treshold_Shift + (Button_Size * 13)};


                // Now you could do this instead of keeping a own track of Raws like above using the Refresh_Apps() and Remove_Row() function, but don't use this one
                // Because the one in the function allow the program to add multiple rows at the same time, the table.columnCount function seems to be not fast enough for the loop.
                // Raws = tableLayoutPanel1.ColumnCount;

                int Innitial_Colums = 4;
                int Maximal_Colums = 13;




                // Just checking whether the Form is now bigger or smaller then before, so we can push the for loop in the right direction of - or +
                if (this.Width != Last_Width)
                {
                    Last_Width = this.Width;

                    for (int i = Innitial_Colums; i <= Maximal_Colums; ++i)
                    {
                        // if (this.Size.Width > (Treshold_Shift + (Button_Size * (i - 1))) & this.Size.Width < (Treshold_Shift + (Button_Size * (i + 1))))      
                        // If current Width is larger then the combined diameter of "i" x buttons, AND still smaller then the value before that,  then its in range
                        try 
                        {   if (this.Size.Width > Values[i - 1] & this.Size.Width < Values[i + 1])
                            {   // Thus we set our amount of colums to i
                                Table_Layout_Panel_Appstore.ColumnCount = i;
                            }
                        } catch { }
                    }

                    Refresh_Apps();
                }

            }
    

            // When the Window is minimized the Launch Buttons will pop right back to their original position, so the user can't loose them ^^
            if (this.Width == 837) 
            {
                Save_Setting("2", "Launch_Button_X", "645");
                Save_Setting("2", "Launch_Button_Y", "424");

                Save_Setting("2", "Mod_Button_X", "530");
                Save_Setting("2", "Mod_Button_Y", "482");

                Save_Setting("2", "Addon_Button_X", "703");
                Save_Setting("2", "Addon_Button_Y", "309");


                // Resetting the positions for launch buttons
                Button_Launch_Mod.Location = new Point(645, 424);
                Button_Mod_Only.Location = new Point(530, 482);
                Button_Launch_Addon.Location = new Point(703, 309);         
            }

        }



        private void MainWindow_Resize(object sender, System.EventArgs e)
        {
            string Background_Image = Background_Image_A;

           
            try 
            {   // If the window is smaller and a small image alternative is available we'd change to the small version.
                if (this.Size.Width < 1024 | this.Size.Height < 781) { Background_Image = Background_Image_B; }
            } catch { }
           


            if (Background_Image_A == "Cycle_Mode") { Adjust_Wallpaper(Background_Image_Path); }
            else
            {
                if (this.Size != Last_Size) 
                { 
                    try { Adjust_Wallpaper(Background_Image_Path + Background_Image); } catch { } 
                }
                Last_Size = this.Size;
            }



            // I also took + 280 costum treshold of my UI layout that shifts it to the right
            int Treshold_Shift = 280;
            int Button_Size = 130;

            // These values are increasing all 130 pixels of the Form size change, to rasterize where the next row is supposed to appear. 
            int[] Values = { Treshold_Shift + Button_Size, Treshold_Shift + (Button_Size * 2), Treshold_Shift + (Button_Size * 3), Treshold_Shift + (Button_Size * 4), Treshold_Shift + (Button_Size * 5), 
                      Treshold_Shift + (Button_Size * 6), Treshold_Shift + (Button_Size * 7), Treshold_Shift +  (Button_Size * 8), Treshold_Shift + (Button_Size * 9), Treshold_Shift + (Button_Size * 10), 
                      Treshold_Shift + (Button_Size * 11), Treshold_Shift + (Button_Size * 12), Treshold_Shift + (Button_Size * 13)};

        
          
            /* Todo
            //======== When window state changes ========//
            if (WindowState != LastWindowState)
            {   LastWindowState = WindowState;

                if (WindowState == FormWindowState.Maximized)
                {
                    // Maximized!
                    Table_Layout_Panel_Appstore.ColumnCount = 12;
                }
                if (WindowState == FormWindowState.Normal)
                {
                    for (int i = 4; i <= 13; ++i)
                    {   // If current Width is larger then the combined diameter of "i" x buttons, AND still smaller then the value before that,  then its in range
                        if (this.Size.Width > Values[i - 1] & this.Size.Width < Values[i + 1]) { Table_Layout_Panel_Appstore.ColumnCount = i; }
                    }                
                
                    Refresh_Apps();                      
                }
            }
             */

        }
   

        
        private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
        {   
            // If the Window was closed by the User:
            if (e.CloseReason == CloseReason.UserClosing)                         
            {  // Saving Window Size and Position, run your variable savings here
        
                // Saving last Tab
                Save_Setting("2", "Last_Tab", Tab_Control_01.SelectedIndex.ToString());

                // Saving window location and Positon
                Save_Setting("2", "Window_Width", this.Size.Width.ToString());
                Save_Setting("2", "Window_Height", this.Size.Height.ToString());

                Save_Setting("2", "Window_X_Position", this.Location.X.ToString());
                Save_Setting("2", "Window_Y_Position", this.Location.Y.ToString());


                // Saving Button Positions
                Save_Setting("2", "Launch_Button_X", Button_Launch_Mod.Location.X.ToString());
                Save_Setting("2", "Launch_Button_Y", Button_Launch_Mod.Location.Y.ToString());

                Save_Setting("2", "Mod_Button_X", Button_Mod_Only.Location.X.ToString());
                Save_Setting("2", "Mod_Button_Y", Button_Mod_Only.Location.Y.ToString());

                Save_Setting("2", "Addon_Button_X", Button_Launch_Addon.Location.X.ToString());
                Save_Setting("2", "Addon_Button_Y", Button_Launch_Addon.Location.Y.ToString());                           
            }

            // If Closed by a Shutdown:
            if (e.CloseReason == CloseReason.WindowsShutDown)
            { }


            // If there is any edited tag in the list "Changed_Tag_Names" 
            if (Changed_Tag_Names.Count() > 0) 
            {
                // Choosing that tab
                Tab_Control_01.SelectedIndex = 4;

                // Call Imperial Dialogue with resolution of 540x160, Button 1 Name , Button 2 Name and Dialogue Text. Then we wait for user input
                Imperial_Dialogue(580, 160, "Close", "Cancel", "Save + Close", Add_Line + "    There are unsaved changes in this Xml." 
                                                                    + Add_Line + "    Are you sure you wish to close the application yet?");


                // If user aborted the program execution
                if (Caution_Window.Passed_Value_A.Text_Data == "false")
                {
                    // Abborting the Program Closing event
                    e.Cancel = true;                  
                }
                else if (Caution_Window.Passed_Value_A.Text_Data == "else")
                {   // Saving before exit
                    Button_Save_Xml_Click(null, null);
                }
            }

            // If any change was done in the selected unit of the Manage tab
            else if (Edited_Selected_Unit == true) 
            {                
                Tab_Control_01.SelectedIndex = 3;


                // Call Imperial Dialogue with resolution of 540x160, Button 1 Name , Button 2 Name and Dialogue Text. Then we wait for user input
                Imperial_Dialogue(580, 160, "Close", "Cancel", "Save + Close", Add_Line + "    There are unsaved changes in this Xml."
                                                                    + Add_Line + "    Are you sure you wish to close the application yet?");

                // If user aborted the program execution
                if (Caution_Window.Passed_Value_A.Text_Data == "false")
                {
                    // Abborting the Program Closing event
                    e.Cancel = true;
                }
                else if (Caution_Window.Passed_Value_A.Text_Data == "else")
                {   
                    // Saving before exit
                    if (Label_Xml_Name.Text == "Creating New File") { Save_As_Click(null, null); }
                   
                    // Otherwise we hopefully have a other Xml open which can be saved
                    else { Save_Click(null, null); }
                }
            }


        }




        private void Button_Debug_Mode_Click(object sender, EventArgs e)
        {
           
            /*
            if (Ability_2_Type == null)
            { Imperial_Console(600, 100, Add_Line + "   NULL"); }
            else 
            {
                Imperial_Console(600, 100, Add_Line + Ability_2_Type.Replace(" ", "_"));
            }
             */
             

            Imperial_Console(600, 100, Add_Line + User_Input);          

        }





       

       


      // Imperial_Console(600, 100, Add_Line + "Da");
      // Save_Setting(Setting, "Name", Name); 
      // Save_Setting(Maximum_Values, "Maximum_Credits", 10000.ToString());  
      // Load_Setting(Setting, "Value");
      // Toggle_Checkbox(Check_Box, "1", "Variable_Name");


    //======================================================== End of File  ====================================================  
    }

}

