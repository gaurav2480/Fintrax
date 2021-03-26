using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite5_production_demo4 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
 
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString1"].ConnectionString;
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
    
}
}