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
using System.Net;
using System.Globalization;
using System.Data.OleDb;

public partial class WebSite5_production_TallySyncMrktg_Pay_Sales_O : System.Web.UI.Page
{




    [WebMethod]
    public static string data()
    {
       
        string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        SqlConnection sqlcon = new SqlConnection(conn);
        string JSON = "{\n \"names\":[";
        string query = "select distinct convert(varchar(10) ,convert(datetime,date,103),105) from TallySyncAcc_Mrktg_Pay_Sales_O  where Response_Code='0 - Approved' and status='Active';";
        sqlcon.Open();
        SqlCommand cmd = new SqlCommand(query, sqlcon);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
              
                string date = reader.GetString(0);
               

                JSON += "[\"" + date + "\"],";

            }
            JSON = JSON.Substring(0, JSON.Length - 1);
            JSON += "] \n}";
        }
        else
        {
            JSON += "[\"" + "" + "\"],";
            JSON = JSON.Substring(0, JSON.Length - 1);
            JSON += "] \n}";
        }

        sqlcon.Close();
        return JSON;



    }


    protected void Page_Load(object sender, EventArgs e)
    {

     


    }



   



    protected void Button2_Click(object sender, EventArgs e)
    {
        try
        {


            string Dates = Request.Form["date"];
            string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection sqlcon1 = new SqlConnection(conn);
            sqlcon1.Open(); 

            string selectDataQuery = "select Merchant_Transaction_Reference,Amount,Date ,* from TallySyncAcc_Mrktg_Pay_Sales_O where status='Active' and Response_Code='0 - Approved'  and convert(varchar(10) ,convert(datetime,date,103),105) ='"+Dates+"'";
            SqlCommand cmd2 = new SqlCommand(selectDataQuery, sqlcon1);
            SqlDataReader reader1 = cmd2.ExecuteReader();
            while (reader1.Read())
            {
                string result = "";
                string Merchant_Transaction_Reference = reader1.GetString(0);
                double Amount = reader1.GetDouble(1);
                DateTime Date = reader1.GetDateTime(2);
                string Transaction_ID = reader1.GetString(4);

                int dayInt = Date.Day;
                string day = Date.Day.ToString();
                if (dayInt < 10)
                {
                    day = "0" + day;
                }
                int monthInt = Date.Month;
                string month = Date.Month.ToString();
                if (monthInt < 10)
                {
                    month = "0" + month;
                }
                int year = Date.Year;


              
                        int i = 3662;
                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP - (2feb2017-18)</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Sales-O" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";
                        xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS.LIST TYPE='String'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>@ Haathi Mahal Mobor,Cavelossim</BASICBUYERADDRESS>\r\n";
                        xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>Salcette-Goa 403731</BASICBUYERADDRESS>\r\n";
                        xmlstc1 = xmlstc1 + "</BASICBUYERADDRESS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                // xmlstc1 = xmlstc1 + "<DATE>" + "20210301" + "</DATE>\r\n";
                xmlstc1 = xmlstc1 + "<GSTREGISTRATIONTYPE>Unregistered</GSTREGISTRATIONTYPE>\r\n";
                xmlstc1 = xmlstc1 + "<VATDEALERTYPE>Unregistered</VATDEALERTYPE>\r\n";
                xmlstc1 = xmlstc1 + "<STATENAME>Goa</STATENAME>\r\n";
                xmlstc1 = xmlstc1 + "<NARRATION>MIGS-"+Merchant_Transaction_Reference+"</NARRATION>\r\n";
                xmlstc1 = xmlstc1 + "<COUNTRYOFRESIDENCE>India</COUNTRYOFRESIDENCE>\r\n";
                xmlstc1 = xmlstc1 + "<PLACEOFSUPPLY>Goa</PLACEOFSUPPLY>\r\n";
                xmlstc1 = xmlstc1 + "<PARTYNAME>AXIS BANK C/C-Goa</PARTYNAME>\r\n";
                xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>AXIS BANK C/C-Goa</PARTYLEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>Sales-O</VOUCHERTYPENAME>\r\n";
                xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>"+""+"</VOUCHERNUMBER>\r\n";
                xmlstc1 = xmlstc1 + "<BASICBASEPARTYNAME>AXIS BANK C/C-Goa</BASICBASEPARTYNAME>\r\n";

                xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                xmlstc1 = xmlstc1 + "<BASICBUYERNAME>AXIS BANK C/C-Goa</BASICBUYERNAME>\r\n";

                xmlstc1 = xmlstc1 + "<CONSIGNEESTATENAME>" + "Goa" + "</CONSIGNEESTATENAME>\r\n";
                xmlstc1 = xmlstc1 + "<ENTEREDBY>" + "kevin" + "</ENTEREDBY>\r\n";
                xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>"+""+"</EFFECTIVEDATE>\r\n";

                xmlstc1 = xmlstc1 + "<HASCASHFLOW>" + "Yes" + "</HASCASHFLOW>\r\n";
                xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";


                xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "AXIS BANK C/C-Goa" + "</LEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>-" + Amount + "</AMOUNT>\r\n";
             
                xmlstc1 = xmlstc1 + "<BANKALLOCATIONS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                xmlstc1 = xmlstc1 + "<INSTRUMENTDATE>" + year + "" + month + "" + day + "</INSTRUMENTDATE>\r\n";
                xmlstc1 = xmlstc1 + "<TRANSACTIONTYPE>" + "Others" + "</TRANSACTIONTYPE>\r\n";
            
                xmlstc1 = xmlstc1 + "<PAYMENTFAVOURING>" + "Call Centre Booking Fees" + "</PAYMENTFAVOURING>\r\n";
                xmlstc1 = xmlstc1 + "<STATUS>" + "No" + "</STATUS>\r\n";
                xmlstc1 = xmlstc1 + "<PAYMENTMODE>" + "Transacted" + "</PAYMENTMODE>\r\n";

                xmlstc1 = xmlstc1 + "<BANKPARTYNAME>" + "Call Centre Booking Fees" + "</BANKPARTYNAME>\r\n";
                xmlstc1 = xmlstc1 + "<ISCONNECTEDPAYMENT>" + "No" + "</ISCONNECTEDPAYMENT>\r\n";
                xmlstc1 = xmlstc1 + "<ISSPLIT>" + "No" + "</ISSPLIT>\r\n";
                xmlstc1 = xmlstc1 + "<CHEQUEPRINTED>" + "1" + "</CHEQUEPRINTED>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>-" + Amount + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "</BANKALLOCATIONS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";




                xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Call Centre Booking Fees" + "</LEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>" + Amount + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Amount + "</VATEXPAMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";
                
                xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                xmlstc1 = xmlstc1 + "</BODY>";
                xmlstc1 = xmlstc1 + "</ENVELOPE>";

                        HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
                        httpWebRequest1.Method = "POST";
                        httpWebRequest1.ContentLength = xmlstc1.Length;
                        httpWebRequest1.ContentType = "application/x-www-form-urlencoded";
                        StreamWriter streamWriter1 = new StreamWriter(httpWebRequest1.GetRequestStream());
                        streamWriter1.Write(xmlstc1);
                        streamWriter1.Close();

                        HttpWebResponse objResponse1 = (HttpWebResponse)httpWebRequest1.GetResponse();
                        using (StreamReader sr1 = new StreamReader(objResponse1.GetResponseStream()))
                        {
                            result = sr1.ReadToEnd();
                            sr1.Close();
                        }

                        i++;

                

                if (result.Contains("<CREATED>1</CREATED>"))
                {
                    string query1 = "update TallySyncAcc_Mrktg_Pay_Sales_O set status='Inactive' where Transaction_ID='" + Transaction_ID + "' and Amount='" + Amount + "'";
                    SqlCommand cmd1 = new SqlCommand(query1, sqlcon1);
                    cmd1.ExecuteNonQuery();
                }

            }
              
            

              



            

            Label5.Text = "Updated Successfully!!!";
            Response.AppendHeader("Refresh", "03;url=TallySyncMrktg_Pay_Sales_O.aspx");

        }

        catch (Exception ex)
        {
            Label6.Text = "Failed :" + ex.Message;
            Response.AppendHeader("Refresh", "10;url=TallySyncMrktg_Pay_Sales_O.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try { 
        string filePath = string.Empty;
        if (FileUpload1.PostedFile != null)
        {
            string path = Server.MapPath("~/Uploads/");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            filePath = path + Path.GetFileName(FileUpload1.FileName);
            string extension = Path.GetExtension(FileUpload1.FileName);
            FileUpload1.SaveAs(filePath);

            string conString = string.Empty;
            switch (extension)
            {
                case ".xls": //Excel 97-03.
                    conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                    break;
                case ".xlsx": //Excel 07 and above.
                    conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                    break;
            }

            DataTable dt = new DataTable();
            conString = string.Format(conString, filePath);

            using (OleDbConnection connExcel = new OleDbConnection(conString))
            {
                using (OleDbCommand cmdExcel = new OleDbCommand())
                {
                    using (OleDbDataAdapter odaExcel = new OleDbDataAdapter())
                    {
                        cmdExcel.Connection = connExcel;

                        //Get the name of First Sheet.
                        connExcel.Open();
                        DataTable dtExcelSchema;
                        dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                        string sheetName = dtExcelSchema.Rows[0]["TABLE_NAME"].ToString();
                        connExcel.Close();

                        //Read Data from First Sheet.
                        connExcel.Open();
                        cmdExcel.CommandText = "SELECT * From [" + sheetName + "]";
                        odaExcel.SelectCommand = cmdExcel;
                        odaExcel.Fill(dt);
                        connExcel.Close();
                    }
                }
            }

          
            string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            foreach (DataRow row in dt.Rows)
            {
               
                SqlConnection sqlcon = new SqlConnection(conn);
                sqlcon.Open();

                string selectQuery = "select * from TallySyncAcc_Mrktg_Pay_Sales_O where Transaction_ID='" + row["Transaction ID"] + "' and Amount='"+row["Amount"]+"'";
                SqlCommand cmd1 = new SqlCommand(selectQuery, sqlcon);
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {

                }else
                {
                    string InsertQuery = "Insert into TallySyncAcc_Mrktg_Pay_Sales_O values('" + row["Transaction ID"] + "','" + row["Date"] + "','" + row["Merchant ID"] + "','" + row["Order ID"] + "','" + row["Merchant Transaction Reference"] + "','" + row["Amount"] + "','" + row["Response Code"] + "','" + row["Merchant Transaction Source"] + "','" + row["Order Date"] + "','" + row["Card Type"] + "','" + row["Card Number"] + "','" + row["Card Expiry Month"] + "','" + row["Card Expiry Year"] + "','Active')";
                    SqlCommand cmd = new SqlCommand(InsertQuery, sqlcon);
                    cmd.ExecuteNonQuery();
                        
                }

                    reader.Close();
                sqlcon.Close();

            }

              
          
        }
        
          
          Label2.Text = "Updated Successfully!!!";
        //  Response.AppendHeader("Refresh", "04;url=TallySyncMrktg_Pay_Sales_O.aspx");

      }
    
      catch (Exception ex)
      {
          Label3.Text = "Failed :"+ex.Message;
          Response.AppendHeader("Refresh", "10;url=TallySyncMrktg_Pay_Sales_O.aspx");
      }



    
    }

}

