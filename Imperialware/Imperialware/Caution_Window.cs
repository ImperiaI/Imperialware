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
using Imperialware;



namespace Imperialware.Resources
{
    public partial class Caution_Window : Form   
    {
        public Caution_Window()
        {
            InitializeComponent();          

        }

        
        // Declaring static variable to pass it to the Main_Window Form:
        public static class Passed_Value_A
        {
            public static string Text_Data { get; set; }        
        }
     

        private void Caution_Window_Load(object sender, EventArgs e)
        {
            Passed_Value_A.Text_Data = "false";
        }
      


        public void Button_Caution_Box_1_Click(object sender, EventArgs e)
        {   
            // Setting the passed value to true (its a string because later we might need texts)
            Passed_Value_A.Text_Data = "true";
            this.Close();
        }

        public void Button_Caution_Box_2_Click(object sender, EventArgs e)
        {            
            Passed_Value_A.Text_Data = "false";
            this.Close();
        }

        private void Button_Caution_Box_3_Click(object sender, EventArgs e)
        {
            Passed_Value_A.Text_Data = "else";
            this.Close();
        }


      
      
       
    }
}
