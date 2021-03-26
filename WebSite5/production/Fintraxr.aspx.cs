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
using Microsoft.Reporting.WebForms;
public partial class Fintraxr : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string date = Request.Form["Date"];

        string date1 = Request.Form["Date1"];


        ReportViewer1.Reset();


        DataTable dt = Fintrax.fintrax_max(date, date1);
        DataTable dt2 = Fintrax.fintrax_min(date,date1);
        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        ReportDataSource rds2 = new ReportDataSource("DataSet2", dt2);




        ReportViewer1.LocalReport.DataSources.Add(rds);

        ReportViewer1.LocalReport.DataSources.Add(rds2);
        ReportViewer1.LocalReport.ReportPath = "reports/Fintrax.rdlc";

        ReportParameter[] rptParam = new ReportParameter[]
        {
            new ReportParameter("LOGDATE",date),
             new ReportParameter("LOGDATE1",date1),
        };

        ReportViewer1.LocalReport.SetParameters(rptParam);
        ReportViewer1.LocalReport.Refresh();



    }
}