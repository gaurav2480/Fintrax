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
public partial class WebSite5_production_CollectionBeforeDib : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string startDate = Request.Form["startDate"];
        DateTime startDate1 = Convert.ToDateTime(startDate);
        string endDate = Request.Form["endDate"];
        string lastdayofpreviousmonth = new DateTime(startDate1.Year, startDate1.Month, 1).AddMonths(0).AddDays(-1).ToString("yyyy-MM-dd");


        DataSet ds = Fintrax.collectionBeforeDisb(startDate, endDate, lastdayofpreviousmonth);

        ds.Tables[0].TableName = "COLLECTION BEFORE DISBURSEMENT";

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
            Response.AddHeader("content-disposition", "attachment;filename=CollectionBeforeDisbursement.xlsx");
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