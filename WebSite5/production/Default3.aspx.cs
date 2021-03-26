using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite5_production_Default3 : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
     {
        string LOANNO = "FIN1/040207/MR0112/EU";
        DataTable d1 = Fintrax.demo(LOANNO);

        ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report
       
        crystalReport.Load(Server.MapPath("~/reports/LoanRepayment.rpt"));
      
        crystalReport.SetDataSource(d1);
      

        CrystalReportViewer1.ReportSource = crystalReport;
        ExportFormatType formatType = ExportFormatType.NoFormat;
        formatType = ExportFormatType.PortableDocFormat;
        crystalReport.ExportToHttpResponse(formatType, Response, true, "Crystal");
        Response.End();
    }
}