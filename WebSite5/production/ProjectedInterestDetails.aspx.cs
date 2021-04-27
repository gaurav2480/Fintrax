
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
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
        string US = Request.Form["US"]; 
        string overdueExCount = Request.Form["overdueExCount"];
        string date = fromDate + " " + toDate;

        DataSet ds;

        DataSet ds1;

        DataSet dataset1;

        

        ds = Fintrax.ProjectedInterestDetail(fromdate, todate, statusValue, disStatus, overdueExCount);
        ds1 = Fintrax.ProjectedInterestDetailsReport_Summary(fromdate, todate, statusValue, disStatus, US);
        dataset1 = Fintrax.OVERDUE_WITH_SUMMARY_ASOFTODAY(statusValue, disStatus,US);
        
        foreach (var column in ds.Tables[0].Columns.Cast<DataColumn>().ToArray())
        {
            if (ds.Tables[0].AsEnumerable().All(dr => dr.IsNull(column)))
                ds.Tables[0].Columns.Remove(column);
        }

        foreach (var column in ds.Tables[1].Columns.Cast<DataColumn>().ToArray())
        {
            if (ds.Tables[1].AsEnumerable().All(dr => dr.IsNull(column)))
                ds.Tables[1].Columns.Remove(column);
        }

        ds.Tables[0].TableName = "PROJECTED DETAILS";
        ds1.Tables[0].TableName = "PROJECTED SUMMARY INR";
        ds1.Tables[1].TableName = "PROJECTED SUMMARY USD";

        ds.Tables[1].TableName = "PROJECTED EXCLUDING OVERDUE";


        dataset1.Tables[0].TableName = "OVERDUE ROW WISE"; 
        dataset1.Tables[1].TableName = "OVERDUE SUMMARY"; 
      
        dataset1.Tables[3].TableName = "ADVANCE ROW WISE";
        dataset1.Tables[2].TableName = "ADVANCE SUMMARY";
        dataset1.Tables[4].TableName = "UNADJUSTED"; 

        dataset1.Tables[5].TableName = "OVERALL SUMMARY"; 
        dataset1.Tables[6].TableName = "RANGE WISE OVERDUE SUMMARY INR";

        dataset1.Tables[7].TableName = "RANGE WISE OVERDUE SUMMARY USD";


        using (XLWorkbook wb = new XLWorkbook())
        {
            foreach (DataTable dt in ds.Tables)
            {
                //Add DataTable as Worksheet.
                wb.Worksheets.Add(dt);
            }
            foreach (DataTable dt in ds1.Tables)
            {
                //Add DataTable as Worksheet.
                wb.Worksheets.Add(dt);
            }

            foreach (DataTable dt in dataset1.Tables)
            {
                //Add DataTable as Worksheet.
                wb.Worksheets.Add(dt);
            }

            //Export the Excel file.
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=Projected_Collection_" + fromdate + "_to_" + todate + "s.xlsx");
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