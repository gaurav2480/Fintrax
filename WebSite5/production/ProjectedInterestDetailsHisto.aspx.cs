
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
public partial class WebSite5_production_ProjectedInterestDetailsHisto : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {




    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string fromdate = Request.Form["fromDate"];
        string todate = Request.Form["toDate"];
        string disStatus = Request.Form["disStatus"];
  

        string date = fromDate + " " + toDate;

        DataSet ds;


        ds = Fintrax.ProjectedInterestDetail_Histo(fromdate, todate, disStatus);

        
        foreach (var column in ds.Tables[0].Columns.Cast<DataColumn>().ToArray())
        {
            if (ds.Tables[0].AsEnumerable().All(dr => dr.IsNull(column)))
                ds.Tables[0].Columns.Remove(column);
        }

        ds.Tables[0].TableName = "HISTORICAL EMI VIEW";


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
            Response.AddHeader("content-disposition", "attachment;filename=HISTORICAL_EMI_VIEW" + fromdate + "_to_" + todate + "s.xlsx");
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