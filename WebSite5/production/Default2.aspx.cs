

using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite5_production_Default2 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {


    


        
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable ds;

        DataTable ds1;

        ds = Fintrax.ProjectedInterestDetailsReport_Summary1();
        ds1 = Fintrax.ProjectedInterestDetailsReport_Summary();

        GridView GridView1 = new GridView();
        GridView1.DataSource = ds;

        GridView1.DataBind();


        GridView GridView2 = new GridView();
        GridView2.DataSource = ds1;

        GridView2.DataBind();

        
        Response.Clear();

        Response.Buffer = true;



        Response.AddHeader("content-disposition",

         "attachment;filename=GridViewExport.xls");

        Response.Charset = "";

        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);



        Table tb = new Table();

        TableRow tr1 = new TableRow();

        TableCell cell1 = new TableCell();

        cell1.Controls.Add(GridView1);

        tr1.Cells.Add(cell1);

        TableCell cell3 = new TableCell();

        cell3.Controls.Add(GridView2);

        TableCell cell2 = new TableCell();

        cell2.Text = "&nbsp;";

        if (rbPreference.SelectedValue == "2")

        {

            tr1.Cells.Add(cell2);

            tr1.Cells.Add(cell3);

            tb.Rows.Add(tr1);

        }

        else

        {

            TableRow tr2 = new TableRow();

            tr2.Cells.Add(cell2);

            TableRow tr3 = new TableRow();

            tr3.Cells.Add(cell3);

            tb.Rows.Add(tr1);

            tb.Rows.Add(tr2);

            tb.Rows.Add(tr3);

        }

        tb.RenderControl(hw);



        //style to format numbers to string

        string style = @"<style> .textmode { mso-number-format:\@; } </style>";

        Response.Write(style);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();
    }

  
}