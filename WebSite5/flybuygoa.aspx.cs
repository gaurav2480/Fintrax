using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Web.Services;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using Microsoft.Reporting.WebForms;

public partial class WebSite5_production_Inhousesumm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    private void show_report()
    {
        ReportViewer2.Reset();


        DataTable dt = Queries2.flybuygoa(TextBox1.Text, TextBox2.Text, TextBox3.Text);
        DataTable dt2 = Queries2.Seapdate(TextBox1.Text);
        ReportDataSource rds = new ReportDataSource("DataSet1", dt);
        ReportDataSource rds2 = new ReportDataSource("DataSet2", dt2);

        ReportViewer2.LocalReport.DataSources.Add(rds);
        ReportViewer2.LocalReport.DataSources.Add(rds2);
        ReportViewer2.LocalReport.ReportPath = "reports/flybuygoa.rdlc";

        ReportParameter[] rptParam = new ReportParameter[]
        {
              new ReportParameter("input",TextBox1.Text),
               new ReportParameter("venue",TextBox2.Text),
              new ReportParameter("gvenue",TextBox3.Text),
        };


        ReportViewer2.LocalReport.SetParameters(rptParam);
        ReportViewer2.LocalReport.Refresh();
       



    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        show_report();
    }
}