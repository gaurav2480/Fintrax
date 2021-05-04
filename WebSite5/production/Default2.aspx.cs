using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
public partial class WebSite5_production_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {//var fileToOpen = "C:/Tally_Logs/2142021_Logs.txt";

		 string path = "C:/Log1/";
        VerifyDir(path);
        string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
        try
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
            file.WriteLine(DateTime.Now.ToString() + ": " + "HELOO WORLD");
           
            file.Close();
        }
        catch (Exception) { }
  Process.Start("IExplore.exe", "www.northwindtraders.com");
                System.Diagnostics.Process.Start(@"notepad.exe", @"C:/Tally_Logs/2142021_Logs.txt");
    }

  public static void VerifyDir(string path)
    {
        try
        {    
            DirectoryInfo dir = new DirectoryInfo(path);
           
            if (!dir.Exists)
            {
                dir.Create();
            }else
            {
                foreach (FileInfo file in dir.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo di in dir.GetDirectories())
                {
                    dir.Delete(true);
                }
                dir.Create();
            }
        }
        catch (Exception ex)
        {

        }
    }





    
}