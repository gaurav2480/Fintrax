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

public partial class WebSite5_production_Realisation_Status : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {




    }


    public string getData()
    {
        string data="";
        string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        SqlConnection sqlcon = new SqlConnection(conn);
        sqlcon.Open();

        string query = "select CHEQUEDATE,convert(varchar,convert(datetime,CHEQUEDATE-2,103),105),CHEQUENO,LNID,AMOUNT,case when CHSTATID=0 then 'DEPOSITED' end as [Status],CSIDENTITY  from BANKDEPOSITSLIPDETAILS where LNID= 0 and CHSTATID=0";
        SqlCommand cmd = new SqlCommand(query, sqlcon);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            long date = reader.GetInt64(0);
            string chequeDate = reader.GetString(1);
            string checkNo = reader.GetString(2);
            long lnid = reader.GetInt64(3);
            decimal amount =  reader.GetDecimal(4);
            string status = reader.GetString(5);
            long ID = reader.GetInt64(6);

            data += "<tr><td style='display:none'>" + ID + "</td><td style='display:none'>" + date + "</td><td>" + chequeDate+ "</td><td>" + checkNo + "</td><td>" + lnid + "</td><td>" + amount + "</td><td>" + status + "</td></tr>";
        }

        sqlcon.Close();


        return data;
    }


    protected void Button1_Click(object sender, EventArgs e)
    {

        string data = "";
        string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
         SqlConnection sqlcon = new SqlConnection(conn);
        sqlcon.Open();

        string query = "select CHEQUEDATE,convert(varchar,convert(datetime,CHEQUEDATE-2,103),105),CHEQUENO,LNID,AMOUNT,case when CHSTATID=0 then 'DEPOSITED' end as [Status],CSIDENTITY  from BANKDEPOSITSLIPDETAILS where LNID= 0 and CHSTATID=0";
        SqlCommand cmd = new SqlCommand(query, sqlcon);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            long date = reader.GetInt64(0);
            string chequeDate = reader.GetString(1);
            string checkNo = reader.GetString(2);
            long lnid = reader.GetInt64(3);
            decimal amount = reader.GetDecimal(4);
            string status = reader.GetString(5);
            long ID = reader.GetInt64(6);


            string query2 = "Update BANKDEPOSITSLIPDETAILS set REALISATIONDATE=" + date + " , CHSTATID='1' where CSIDENTITY=" + ID + " and lnid ='0'";
            SqlCommand cmd1 = new SqlCommand(query2, sqlcon);
            cmd1.ExecuteNonQuery();

        }

        reader.Close();
        sqlcon.Close();



    }

}