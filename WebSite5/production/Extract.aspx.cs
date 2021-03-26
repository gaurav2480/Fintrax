using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using System.IO;
using System.Data;

public partial class WebSite5_production_Extract : System.Web.UI.Page
{

  
 

    protected void Page_Load(object sender, EventArgs e)
    {

       // string user = (string)Session["username"];
       //  // office = (string)Session["office"];
       // if (user == null)
       // {
       //     Response.Redirect("login.aspx");
       // }

       // //string user = (string)Session["username"];
       //// Label1.Text = "HI!! " + user;
       // Label2.Text = user;
       // string val = getdata();
    }




    private void show_report(string date, string date2)
    {

        DataTable ds = Fintrax.getCybil(date,date2);

        GridView GridView1 = new GridView();
        GridView1.AllowPaging = false;
        GridView1.DataSource = ds;
        GridView1.DataBind();



        Response.Clear();

        Response.Buffer = true;

        Response.AddHeader("content-disposition", "attachment;filename=Cybil.xls");

        Response.Charset = "";

        Response.ContentType = "application/vnd.xls";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        GridView1.RenderControl(hw);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();








    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        string date = Request.Form["TextBox1"];

        string date2 = Request.Form["TextBox2"];

        show_report(date, date2);

    }









}