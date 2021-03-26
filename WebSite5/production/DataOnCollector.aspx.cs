using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Web.Services;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using Microsoft.Reporting.WebForms;
public partial class WebSite5_production_DataOnCollector : System.Web.UI.Page
{

    
  
    //public string getdata()
    //{

    //    string user = (string)Session["username"];
    //    string htmlstr = "";
    //    string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
    //    string query = "select distinct parentnode,ReportOrder from user_group_access ug where username ='"+user+"' order by ReportOrder asc";
    //    SqlConnection sqlcon = new SqlConnection(conn);
    //    sqlcon.Open();
    //    SqlCommand cmd = new SqlCommand(query, sqlcon);
    //    SqlDataReader reader = cmd.ExecuteReader();

    //    while (reader.Read())
    //    {
    //        string name = reader.GetString(0);
    //        if (name == "")
    //        {

    //        }
    //        else
    //        {
    //            if (name == "Reports")
    //            {
    //                htmlstr += "<li><a href='reportSlider.aspx'><i class='fa fa-home'></i>" + name + " <span class='fa fa-chevron - down'></span> </a><ul class='nav child_menu'>";
    //                htmlstr += "</ul></li>";
    //                name = "";
    //            }
    //            if (name == "")
    //            {

    //            }
    //            else
    //            {
    //                htmlstr += "<li><a><i class='fa fa-home'></i>" + name + " <span class='fa fa-chevron - down'></span> </a><ul class='nav child_menu'>";
    //                SqlConnection sqlcon1 = new SqlConnection(conn);
    //                sqlcon1.Open();
    //                string query1 = "select * from user_group_access ug where ug.ParentNode='" + name + "' and username ='" + user + "' order By page_order asc";
    //                SqlCommand cmd1 = new SqlCommand(query1, sqlcon1);


    //                SqlDataReader reader1 = cmd1.ExecuteReader();
    //                while (reader1.Read())
    //                {
    //                    string pagename = reader1.GetString(1);
    //                    string pageurl = reader1.GetString(3);
    //                    string AccessName = reader1.GetString(6);

    //                    htmlstr += "<li><a href=" + pageurl + "?name=" + AccessName + ">" + pagename + " </a></li>";
    //                    Session["pagename"] = pagename;
    //                    string office = Queries.GetOffice(user);
    //                    Session["office"] = office;
    //                    Session["username"] = user;
    //                }

    //                htmlstr += "</ul></li>";



    //                reader1.Close();
    //                sqlcon1.Close();

    //            }

    //        }
    //    }

    //    reader.Close();
    //    sqlcon.Close();
    //    return htmlstr;

    //}

    protected void Page_Load(object sender, EventArgs e)
    {
       // Session["pname"] = "";
       // Session["pname"] = Request.QueryString["name"];
       //// office = (string)Session["office"];
       //string user = (string)Session["username"];
       // if (user == null)
       // {
       //     Response.Redirect("login.aspx");
       // }

       // //string user = (string)Session["username"];
       //// Label1.Text = "HI!! " + user;
       // Label2.Text = user;
       // string val = getdata();
    }


    //[WebMethod]
    //public static string getAdminRights()
    //{
    //    string user = HttpContext.Current.Session["username"].ToString();
    //    string JSON = "{\n \"names\":[";
    //    string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
    //    SqlConnection sqlcon = new SqlConnection(conn);

    //    string query = "select name,PageName from user_group_access  where Username='" + user + "' and PageType='Admin'";
    //    sqlcon.Open();
    //    SqlCommand cmd = new SqlCommand(query, sqlcon);
    //    SqlDataReader reader = cmd.ExecuteReader();

    //    if (reader.HasRows)
    //    {

    //        while (reader.Read())
    //        {

    //            string name = reader.GetString(0);
    //            string PageName = reader.GetString(1);

    //            JSON += "[\"" + name + "\" , \"" + PageName + "\"],";


    //        }
    //        JSON = JSON.Substring(0, JSON.Length - 1);
    //        JSON += "] \n}";


    //    }
    //    else
    //    {

    //        JSON += "[\"" + "" + "\"],";
    //        JSON = JSON.Substring(0, JSON.Length - 1);
    //        JSON += "] \n}";
    //    }

    //    reader.Close();
    //    sqlcon.Close();



    //    return JSON;



    //}


    [WebMethod]
    public static string LoadLoanDetailsOnCollector(string Name)
    {

        string JSON = "{\n \"names\":[";


        SqlDataReader reader = Fintrax.LoadLoanDetailsOnCollector(Name);
        if (reader.HasRows)
        {
            while (reader.Read())
            {
                string loanNO = reader.GetString(0);
                string ledgerName = reader.GetString(1);
                string loanStatus = reader.GetString(2);
                double emiValue = reader.GetDouble(3);
                double overdue = reader.GetDouble(4);
                double lateFee = reader.GetDouble(5);
         

                JSON += "[\"" + loanNO + "\",\"" + ledgerName + "\",\"" + loanStatus + "\",\"" + emiValue + "\",\"" + overdue + "\",\"" + lateFee + "\",\"" + Name + "\"],";
            }
            JSON = JSON.Substring(0, JSON.Length - 1);
            JSON += "] \n}";
        }
        else
        {
            JSON += "[\"" + "" + "\"],";
            JSON = JSON.Substring(0, JSON.Length - 1);
            JSON += "] \n}";
        }


        return JSON;
    }
    
}