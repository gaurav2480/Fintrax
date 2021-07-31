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

public partial class WebSite5_production_UpdateCybil : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {




    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {


        Session["DateofLastPayment"] = ""; Session["DateClosed"] = ""; Session["DateReported"] = ""; Session["CurrentBalance"] = ""; Session["AmtOverdue"] = "";
        Session["ActualPaymentAmt"] = ""; Session["overdue"] = ""; Session["No_of_days_past_due"] = "";
        string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        SqlConnection sqlcon = new SqlConnection(conn);
        sqlcon.Open();
        string query = "select distinct LoanNo from Cybil";
        SqlCommand cmd = new SqlCommand(query, sqlcon);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string loanNo = reader.GetString(0);
            DataSet ds = Fintrax.LoadCybilUpdateDetailsOnLoanNo(loanNo);
            DataSet dss = Fintrax.overdue(loanNo);

	 if(dss.Tables[0].Rows.Count == 0)
                {
                    Session["overdue"] ="0";
                    Session["No_of_days_past_due"] = "0";
                }
                else
                {
                    Session["overdue"] = dss.Tables[0].Rows[0]["EMIAMOUNT"].ToString();
                    Session["No_of_days_past_due"] = ds.Tables[0].Rows[0]["No_of_days_past_due"].ToString();
                }

            Session["DateofLastPayment"] = ds.Tables[0].Rows[0]["DateofLastPayment"].ToString();
            Session["DateClosed"] = ds.Tables[0].Rows[0]["DateClosed"].ToString();
            Session["DateReported"] = ds.Tables[0].Rows[0]["DateReported"].ToString();
            Session["CurrentBalance"] = ds.Tables[0].Rows[0]["CurrentBalance"].ToString();
            Session["ActualPaymentAmt"] = ds.Tables[0].Rows[0]["ActualPaymentAmt"].ToString();
                // Session["overdue"] = dss.Tables[0].Rows[0]["EMIAMOUNT"].ToString();
                

            string query1 = "update Cybil set [Date of Last Payment]='"+ Session["DateofLastPayment"].ToString() + "',[Current  Balance]='" + Session["CurrentBalance"].ToString() + "',[Actual Payment Amt]='" + Session["ActualPaymentAmt"].ToString() + "',[No of Days Past Due]='" + Session["No_of_days_past_due"].ToString() + "', [Amt Overdue]='"+ Session["overdue"].ToString() + "',[Date Reported]='" + Session["DateReported"].ToString() + "',[Date Closed]='" + Session["DateClosed"].ToString() + "' where LoanNo='" + loanNo+"'";
            SqlCommand cmd1 = new SqlCommand(query1, sqlcon);
            cmd1.ExecuteNonQuery();

      }
        label1.Text = "All Records Updated Successfully!!!";
        reader.Close();
        sqlcon.Close();

        }
        catch (Exception ex )
        {
            label1.Text = ex.Message+" Please run the process again!";

        }








    }

}