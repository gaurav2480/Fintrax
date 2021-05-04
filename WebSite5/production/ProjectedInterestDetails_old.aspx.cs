
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite5_production_ProjectedInterestDetails : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {




    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string fromdate = Request.Form["fromDate"];
        string todate = Request.Form["toDate"];
        string statusValue = Request.Form["status"];
        string disStatus = Request.Form["disStatus"];

        string date = fromDate + " " + toDate;

        DataTable ds;

        DataTable ds1;

        DataSet dataset1;

        ds = Fintrax.ProjectedInterestDetail(fromdate, todate, statusValue, disStatus);
        ds1 = Fintrax.ProjectedInterestDetailsReport_Summary(fromdate, todate, statusValue, disStatus);
        dataset1 = Fintrax.OVERDUE_WITH_SUMMARY_ASOFTODAY(statusValue, disStatus);



        foreach (var column in ds.Columns.Cast<DataColumn>().ToArray())
        {
            if (ds.AsEnumerable().All(dr => dr.IsNull(column)))
                ds.Columns.Remove(column);
        }
      

        GridView GridView1 = new GridView();
        GridView1.DataSource = ds;
        GridView1.DataBind();


        GridView GridView2 = new GridView();
        GridView2.DataSource = ds1;
        GridView2.DataBind();


        GridView GridView3 = new GridView();
        GridView3.DataSource = dataset1.Tables[0];
        GridView3.DataBind();

        GridView GridView4 = new GridView();
        GridView4.DataSource = dataset1.Tables[1];
        GridView4.DataBind();

        GridView GridView5 = new GridView();
        GridView5.DataSource = dataset1.Tables[2];
        GridView5.DataBind();

        GridView GridView6 = new GridView();
        GridView6.DataSource = dataset1.Tables[3];
        GridView6.DataBind();

        GridView GridView7 = new GridView();
        GridView7.DataSource = dataset1.Tables[4];
        GridView7.DataBind();

        GridView GridView8 = new GridView();
        GridView8.DataSource = dataset1.Tables[5];
        GridView8.DataBind();

        GridView GridView9 = new GridView();
        GridView9.DataSource = dataset1.Tables[6];
        GridView9.DataBind();


        Response.Clear();

        Response.Buffer = true;



        Response.AddHeader("content-disposition",

         "attachment;filename=Projected_Collection_"+ fromdate + "_to_"+ todate + "s.xls");

        Response.Charset = "";

        Response.ContentType = "application/vnd.ms-excel";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);



        Table tb = new Table();

        TableRow tr1 = new TableRow();

        TableCell cell1 = new TableCell();

        cell1.Controls.Add(GridView1);

        tr1.Cells.Add(cell1);

        
        TableCell cell2 = new TableCell();
        cell2.Text = "&nbsp;";
        TableRow tr2 = new TableRow();
        tr2.Cells.Add(cell2);
     


        TableRow tr3 = new TableRow();
        TableCell cell3 = new TableCell();
        cell3.Controls.Add(GridView2);
        tr3.Cells.Add(cell3);


        TableCell cell4 = new TableCell();
        cell4.Text = "&nbsp;";
        TableRow tr4 = new TableRow();
        tr4.Cells.Add(cell4);
      


        TableRow tr5 = new TableRow();
        TableCell cell5 = new TableCell();
        cell5.Controls.Add(GridView3);
        tr5.Cells.Add(cell5);


        TableCell cell6 = new TableCell();
        cell6.Text = "&nbsp;";
        TableRow tr6 = new TableRow();
        tr6.Cells.Add(cell6);
      


        TableRow tr7 = new TableRow();
        TableCell cell7 = new TableCell();
        cell7.Controls.Add(GridView4);
        tr7.Cells.Add(cell7);


        TableCell cell8 = new TableCell();
        cell8.Text = "&nbsp;";
        TableRow tr8 = new TableRow();
        tr8.Cells.Add(cell8);
      


        TableRow tr9 = new TableRow();
        TableCell cell9 = new TableCell();
        cell9.Controls.Add(GridView5);
        tr9.Cells.Add(cell9);


        TableCell cell10 = new TableCell();
        cell10.Text = "&nbsp;";
        TableRow tr10 = new TableRow();
        tr10.Cells.Add(cell10);


        TableRow tr11 = new TableRow();
        TableCell cell11 = new TableCell();

        cell11.Controls.Add(GridView6);
        tr11.Cells.Add(cell11);


        TableCell cell12 = new TableCell();
        cell12.Text = "&nbsp;";
        TableRow tr12 = new TableRow();
        tr12.Cells.Add(cell12);


        TableRow tr13 = new TableRow();
        TableCell cell13 = new TableCell();

        cell13.Controls.Add(GridView7);
        tr13.Cells.Add(cell13);


        TableCell cell14 = new TableCell();
        cell14.Text = "&nbsp;";
        TableRow tr14 = new TableRow();
        tr14.Cells.Add(cell14);

        TableRow tr15 = new TableRow();
        TableCell cell15 = new TableCell();

        cell15.Controls.Add(GridView8);
        tr15.Cells.Add(cell15);

        TableCell cell16 = new TableCell();
        cell16.Text = "&nbsp;";
        TableRow tr16 = new TableRow();
        tr16.Cells.Add(cell16);

        TableRow tr17 = new TableRow();
        TableCell cell17 = new TableCell();

        cell17.Controls.Add(GridView9);
        tr17.Cells.Add(cell17);




        tb.Rows.Add(tr1);

        tb.Rows.Add(tr2);

        tb.Rows.Add(tr3);

        tb.Rows.Add(tr4);

        tb.Rows.Add(tr5);

        tb.Rows.Add(tr6);

        tb.Rows.Add(tr7);

        tb.Rows.Add(tr8);

        tb.Rows.Add(tr13);

        tb.Rows.Add(tr12);


        tb.Rows.Add(tr9);

        tb.Rows.Add(tr10);

        tb.Rows.Add(tr11);

        tb.Rows.Add(tr14);

        tb.Rows.Add(tr15);

        tb.Rows.Add(tr16);

        tb.Rows.Add(tr17);


        tb.RenderControl(hw);



        //style to format numbers to string

        string style = @"<style> .textmode { mso-number-format:\@; } </style>";

        Response.Write(style);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();
    }

}