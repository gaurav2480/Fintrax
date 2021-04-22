using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite5_production_demo4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        /* string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString1"].ConnectionString;
         SqlConnection sqlcon = new SqlConnection(connectionString);

         try
         {
             sqlcon.Open();

             string str1 = "USE master;";
             string str2 = "RESTORE DATABASE Fintraxlivedb FROM DISK = 'C:\\Fintrax_Test_backup_2020_01_09_120003_2472326.bak' WITH REPLACE";
             SqlCommand cmd = new SqlCommand(str1, sqlcon);
             SqlCommand cmd1 = new SqlCommand(str2, sqlcon);
             cmd.ExecuteNonQuery();
             cmd1.ExecuteNonQuery();
         }
         catch (Exception ex)
         {
             throw;
         }
         finally
         {
             sqlcon.Dispose();
             sqlcon.Close();
         }

        string path = "C:/Log/";
        VerifyDir(path);
        string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
        try
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
            file.WriteLine(DateTime.Now.ToString() + ": " + "HELOO WORLD");
           
            file.Close();
        }
        catch (Exception) { }*/
        Response.Write("<script>window.open ('Default.aspx?val=" + "HELLO" + "','_blank');</script>");
        //        System.Diagnostics.Process.Start(@"notepad.exe", @"C:/Tally_Logs/2142021_Logs.txt");

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