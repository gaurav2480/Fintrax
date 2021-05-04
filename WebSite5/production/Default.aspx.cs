using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite5_production_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string Name = Request.QueryString["name"];

        DataTable ds = Fintrax.LoadLoanDetailsOnCollector1(Name);

        GridView GridView1 = new GridView();
        GridView1.AllowPaging = false;
        GridView1.DataSource = ds;
        GridView1.DataBind();



        Response.Clear();

        Response.Buffer = true;

        Response.AddHeader("content-disposition", "attachment;filename="+ Name + ".xls");

        Response.Charset = "";

        Response.ContentType = "application/vnd.xls";

        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        GridView1.RenderControl(hw);

        Response.Output.Write(sw.ToString());

        Response.Flush();

        Response.End();
    }
}