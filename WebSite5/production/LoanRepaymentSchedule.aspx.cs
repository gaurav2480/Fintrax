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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

public partial class WebSite5_production_LoanRepaymentSchedule : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string loanNos = Request.Form["loanNo"];

        loanNo.Text = "";
        DataTable d1 = Fintrax.LoanRepaymentSchedule(loanNos);

        ReportDocument crystalReport = new ReportDocument(); // creating object of crystal report

        crystalReport.Load(Server.MapPath("~/reports/LoanRepayment.rpt"));

        crystalReport.SetDataSource(d1);


        CrystalReportViewer1.ReportSource = crystalReport;
        ExportFormatType formatType = ExportFormatType.NoFormat;
        formatType = ExportFormatType.PortableDocFormat;
        crystalReport.ExportToHttpResponse(formatType, Response, true, loanNos);
        Response.Flush();
        Response.End();


    }

}