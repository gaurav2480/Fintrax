using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Web.Services;
using System.Configuration;
using System.Data;
using System.IO;
using ClosedXML.Excel;
public partial class WebSite5_production_Cancelled_Loans : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {




    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string Startdate = Request.Form["startdate"];
        string Enddate = Request.Form["enddate"];
        string UPTODATE = Request.Form["UPTODATE"];

        DataSet ds = Fintrax.Cancelled_Loans(Startdate,Enddate, UPTODATE);

        ds.Tables[0].TableName = "SCHEDULE BEFORE CANCELLED";
        ds.Tables[1].TableName = "SCHEDULE AFTER CANCELLED";


        using (XLWorkbook wb = new XLWorkbook())
        {
            foreach (DataTable dt in ds.Tables)
            {
                //Add DataTable as Worksheet.
                wb.Worksheets.Add(dt);
            }

            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Cancelled_Loans-" + Startdate + "-" + Enddate + ".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
    }

}