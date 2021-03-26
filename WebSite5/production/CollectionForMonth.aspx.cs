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
using ClosedXML.Excel;
using System.IO;

public partial class WebSite5_production_Interest_Waiver : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DataSet ds;
        string startDate = Request.Form["startDate"];
        string endDate = Request.Form["endDate"];
        string type = Request.Form["type"];

        if (type=="0")
        {
            ds = Fintrax.collectionForMonth_Rows(startDate, endDate);
        }
        else
        {
            ds = Fintrax.collectionForMonth(startDate, endDate);
        }


        ds.Tables[0].TableName = "COLLECTION FOR MONTH";

        
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

            if (type == "0")
            {

                Response.AddHeader("content-disposition", "attachment;filename=Collection_With_Rows.xlsx");
            }
            else
            {

                Response.AddHeader("content-disposition", "attachment;filename=Collection.xlsx");
            }

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