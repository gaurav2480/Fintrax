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
public partial class WebSite5_production_searchLoanEdit : System.Web.UI.Page
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
    public static string searchProfile(string loanNo)
 {
        string JSON = "{\n \"names\":[";

       
            if (loanNo == "" || loanNo == null)
            {
                JSON += "[\"" + "" + "\"],";
                JSON = JSON.Substring(0, JSON.Length - 1);
                JSON += "] \n}";
            }
            else
            {
            //    string office = HttpContext.Current.Session["Office"].ToString();
                string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
                SqlConnection sqlcon = new SqlConnection(conn);

                string query = "select distinct top(200) LoanNo,[Consumer Name],[Telephone No#Mobile],[Email ID 1] from Cybil where loanNo like '" + loanNo + "%'";
                sqlcon.Open();
                SqlCommand cmd = new SqlCommand(query, sqlcon);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                    while (reader.Read())
                    {

                        string loanNumber = reader.GetString(0);
                        string customerName = reader.GetString(1);
                        string TelephoneNoMobile = reader.GetString(2);
                        string EmailID1 = reader.GetString(3);

                    JSON += "[\"" + loanNumber + "\" , \"" + customerName + "\",\"" + TelephoneNoMobile + "\" , \"" + EmailID1 + "\"],";


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

                reader.Close();
                sqlcon.Close();

            }
        
       
        
         return JSON;
        

    }

    [WebMethod]
    public static string getlink(string loanNo)
    {

        //string office = HttpContext.Current.Session["office"].ToString();
            string JSON = "{\n \"names\":[";
            string val = "EditCybil.aspx?LoanNo=" + loanNo + "";
            JSON += "[\"" + val + "\"],";
            JSON = JSON.Substring(0, JSON.Length - 1);
            JSON += "] \n}";
     
        return JSON;
        
    }

}