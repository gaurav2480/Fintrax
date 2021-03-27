﻿using System;
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

public partial class WebSite5_production_TallySyncUnidentified_Receipts : System.Web.UI.Page
{





    protected void Page_Load(object sender, EventArgs e)
    {

     


    }



   



    protected void Button2_Click(object sender, EventArgs e)
    {

        string bankName = Request.Form["bank"];

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
                    string result = "";

                    string narration = row["Description"].ToString();
                    DateTime valueDate = Convert.ToDateTime(row["Value Date"].ToString());
                    double amount = (double)row["Transaction Amount(INR)"];



                   int dayInt = valueDate.Day;
                    string day = valueDate.Day.ToString();
                    if (dayInt < 10)
                    {
                        day = "0" + day;
                    }
                    int monthInt = valueDate.Month;
                    string month = valueDate.Month.ToString();
                    if (monthInt < 10)
                    {
                        month = "0" + month;
                    }
                    int year = valueDate.Year;



                    int i = 2116;
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
                    xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Receipt" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Accounting Voucher View" + "\" >\r\n";
                    xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";

                    xmlstc1 = xmlstc1 + "<NARRATION>" + narration +"</NARRATION>\r\n";
                    xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + bankName + "</PARTYLEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Receipt" + "</VOUCHERTYPENAME>\r\n";
                   // xmlstc1 = xmlstc1 + "<REFERENCE>" + receipt_No + "</REFERENCE>\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                    xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Accounting Voucher View" + "</PERSISTEDVIEW>\r\n";
                    xmlstc1 = xmlstc1 + "<ENTEREDBY>" + "Sameer" + "</ENTEREDBY>\r\n";
                    xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                    xmlstc1 = xmlstc1 + "<HASCASHFLOW>" + "Yes" + "</HASCASHFLOW>\r\n";
                    xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";

                    xmlstc1 = xmlstc1 + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Unidentified Money" + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT> " + amount + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</ALLLEDGERENTRIES.LIST>" + "\r\n";


                    xmlstc1 = xmlstc1 + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + bankName + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>-" + amount + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<BANKALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                    xmlstc1 = xmlstc1 + "<INSTRUMENTDATE>" + year + "" + month + "" + day + "</INSTRUMENTDATE>\r\n";
                    xmlstc1 = xmlstc1 + "<TRANSACTIONTYPE>" + "Cheque/DD" + "</TRANSACTIONTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<PAYMENTFAVOURING>" + "Unidentified Money" + "</PAYMENTFAVOURING>\r\n";
                    xmlstc1 = xmlstc1 + "<PAYMENTMODE>" + "Transacted" + "</PAYMENTMODE>\r\n";
                    xmlstc1 = xmlstc1 + "<BANKPARTYNAME>" + "Unidentified Money" + "</BANKPARTYNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<CHEQUEPRINTED>" + "1" + "</CHEQUEPRINTED>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>-" + amount + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</BANKALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</ALLLEDGERENTRIES.LIST>" + "\r\n";

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

                    /*   SqlConnection sqlcon = new SqlConnection(conn);
                       sqlcon.Open();

                       string selectQuery = "select * from TallySyncAcc_Mrktg_Pay where Receipt_No='" + row["Receipt No"] + "'";
                       SqlCommand cmd1 = new SqlCommand(selectQuery, sqlcon);
                       SqlDataReader reader = cmd1.ExecuteReader();
                       if (reader.HasRows)
                       {

                       }else
                       {
                           string InsertQuery = "Insert into TallySyncAcc_Mrktg_Pay values('" + row["Receipt No"] + "','" + row["Transmittal Date"].ToString() + "','" + row["Booking ID"].ToString() + "','" + row["Client Name"].ToString() + "','" + row["Booking Fees"] + "','" + row["CGST"] + "','" + row["SGST"] + "','" + row["Surcharge"] + "','" + row["Resort Credit"] + "','" + row["Total"] + "','" + row["Payment Method"].ToString() + "','" + row["Transaction Date"].ToString() + "','" + row["Transaction Id"].ToString() + "','" + row["Booking Prscd As"].ToString() + "','" + row["Resort"].ToString() + "','" + row["Check In Date"].ToString() + "','" + row["Check Out Date"].ToString() + "','Active')";
                           SqlCommand cmd = new SqlCommand(InsertQuery, sqlcon);
                           cmd.ExecuteNonQuery();


                       }

                       reader.Close();
                       sqlcon.Close();*/

                }

              
          
        }


            Label5.Text = "Updated Successfully!!!";
          Response.AppendHeader("Refresh", "04;url=TallySyncUnidentified_Receipts.aspx");

      }
    
      catch (Exception ex)
      {
            Label6.Text = "Failed :"+ex.Message;
          Response.AppendHeader("Refresh", "10;url=TallySyncUnidentified_Receipts.aspx");
      }



    
    }

}
