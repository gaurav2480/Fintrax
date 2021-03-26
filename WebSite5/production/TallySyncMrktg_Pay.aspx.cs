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

public partial class WebSite5_production_TallySyncMrktg_Pay : System.Web.UI.Page
{




    [WebMethod]
    public static string data()
    {
       
        string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        SqlConnection sqlcon = new SqlConnection(conn);
        string JSON = "{\n \"names\":[";
        string query = "select distinct resort from TallySyncAcc_Mrktg_Pay where status='Active';";
        sqlcon.Open();
        SqlCommand cmd = new SqlCommand(query, sqlcon);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
              
                string resort = reader.GetString(0);
               

                JSON += "[\"" + resort + "\"],";

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

            string result = "";
            string resort = Request.Form["Venue"];
            string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection sqlcon1 = new SqlConnection(conn);
            sqlcon1.Open();

            string selectDataQuery = "select * from TallySyncAcc_Mrktg_Pay where resort='" + resort + "' and status='Active'";
            SqlCommand cmd2 = new SqlCommand(selectDataQuery, sqlcon1);
            SqlDataReader reader1 = cmd2.ExecuteReader();
            while (reader1.Read())
            {
               
                string receipt_No = reader1.GetString(1);
                DateTime Transmittal_Date = reader1.GetDateTime(2);
                string Booking_ID = reader1.GetString(3);
                string Client_Name = reader1.GetString(4);
                double Booking_Fees = reader1.GetDouble(5);
                double CGST = reader1.GetDouble(6);
                double SGST = reader1.GetDouble(7);
                double Surcharge = reader1.GetDouble(8);
                double Resort_Credit = reader1.GetDouble(9);
                double Total = reader1.GetDouble(10);
                string Payment_Method = reader1.GetString(11);
                DateTime Transaction_Date = reader1.GetDateTime(12);
                string Transaction_Id = reader1.GetString(13);
                string QStatus = reader1.GetString(14);
                string Resort = reader1.GetString(15);
                DateTime Check_In_Date = reader1.GetDateTime(16);
                DateTime Check_Out_Date = reader1.GetDateTime(17);

                // CGST = Math.Round(Booking_Fees * 6 /100,2);
                //  SGST = Math.Round(Booking_Fees * 6 / 100,2);

                int dayInt = Transaction_Date.Day;
                string day = Transaction_Date.Day.ToString();
                if (dayInt < 10)
                {
                    day = "0" + day;
                }
                int monthInt = Transaction_Date.Month;
                string month = Transaction_Date.Month.ToString();
                if (monthInt < 10)
                {
                    month = "0" + month;
                }
                int year = Transaction_Date.Year;


                if (Resort == "KARMA HAVELI JAIPUR")
                {

                    if (CGST == 0 && SGST == 0)
                    {
                        int i = 82;
                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP (Jaipur)</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Sales" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";
                        xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                       // xmlstc1 = xmlstc1 + "<DATE>" + "20210301" + "</DATE>\r\n";
                        xmlstc1 = xmlstc1 + "<NARRATION>" + Transaction_Id + " " + receipt_No + " " + Convert.ToDateTime(Transmittal_Date).ToString("dd/MM/yyyy") + " " + Booking_ID + " " + Client_Name + "</NARRATION>\r\n";
                        xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</PARTYLEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Sales" + "</VOUCHERTYPENAME>\r\n";
                        xmlstc1 = xmlstc1 + "<REFERENCE>" + receipt_No + "</REFERENCE>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                        xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                        xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                       // xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + "20210301" + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>-" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Booking Fees (Mktg.) Exempted" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Booking_Fees + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORY>" + "Primary Cost Category" + "</CATEGORY>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + "Non Mem" + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                        xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</BODY>";
                        xmlstc1 = xmlstc1 + "</ENVELOPE>";

                        HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9090");
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



                    }
                    else
                    {
                        int i = 82;
                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP (Jaipur)</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Sales" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";
                        xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                      //  xmlstc1 = xmlstc1 + "<DATE>" + "20210301" + "</DATE>\r\n";
                        xmlstc1 = xmlstc1 + "<NARRATION>" + Transaction_Id + " " + receipt_No + " " + Convert.ToDateTime(Transmittal_Date).ToString("dd/MM/yyyy") + " " + Booking_ID + " " + Client_Name + "</NARRATION>\r\n";
                        xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</PARTYLEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Sales" + "</VOUCHERTYPENAME>\r\n";
                        xmlstc1 = xmlstc1 + "<REFERENCE>" + receipt_No + "</REFERENCE>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                        xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                        //xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + "20210301" + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>-" + Total + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Booking Fees (Marketing) 12%" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Booking_Fees + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORY>" + "Primary Cost Category" + "</CATEGORY>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + "Non Mem" + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Surcharge(12 % Booking)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Surcharge + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Surcharge + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "6" + "</BASICRATEOFINVOICETAX>\r\n";
                        xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<ROUNDTYPE>" + "Normal Rounding" + "</ROUNDTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "OUTPUT TAX CGST-6 %" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<ROUNDLIMIT>" + "1" + "</ROUNDLIMIT>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + CGST + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + CGST + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "6" + "</BASICRATEOFINVOICETAX>\r\n";
                        xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<ROUNDTYPE>" + "Normal Rounding" + "</ROUNDTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "OUTPUT TAX SGST-6%" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<ROUNDLIMIT>" + "1" + "</ROUNDLIMIT>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + SGST + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + SGST + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                        xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</BODY>";
                        xmlstc1 = xmlstc1 + "</ENVELOPE>";

                        HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9090");
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

                    }


                }
                else if (Resort == "KARMA CHAKRA")
                {
                    if (CGST == 0 && SGST == 0)
                    {
                        int i = 166;
                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP (Kerala)</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Sales" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";
                        xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                       // xmlstc1 = xmlstc1 + "<DATE>" + "20210301" + "</DATE>\r\n";
                        xmlstc1 = xmlstc1 + "<NARRATION>" + Transaction_Id + " " + receipt_No + " " + Transmittal_Date + " " + Booking_ID + " " + Client_Name + "</NARRATION>\r\n";
                        xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</PARTYLEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Sales" + "</VOUCHERTYPENAME>\r\n";
                        xmlstc1 = xmlstc1 + "<REFERENCE>" + receipt_No + "</REFERENCE>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                        xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                      //  xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + "20210301" + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>-" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Booking Fees (Mktg.) Exempted" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Booking_Fees + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORY>" + "Primary Cost Category" + "</CATEGORY>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + "Non Mem" + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                        xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</BODY>";
                        xmlstc1 = xmlstc1 + "</ENVELOPE>";

                        HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9090");
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



                    }
                    else
                    {
                        int i = 166;
                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP (Kerala)</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Sales" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";
                        xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                      //  xmlstc1 = xmlstc1 + "<DATE>" + "20210301" + "</DATE>\r\n";
                        xmlstc1 = xmlstc1 + "<NARRATION>" + Transaction_Id + " " + receipt_No + " " + Transmittal_Date + " " + Booking_ID + " " + Client_Name + "</NARRATION>\r\n";
                        xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</PARTYLEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Sales" + "</VOUCHERTYPENAME>\r\n";
                        xmlstc1 = xmlstc1 + "<REFERENCE>" + receipt_No + "</REFERENCE>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                        xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                       // xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + "20210301" + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>-" + Total + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Booking Fees (Marketing) 12%" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Booking_Fees + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORY>" + "Primary Cost Category" + "</CATEGORY>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + "Non Mem" + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Surcharge(12 % Booking)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Surcharge + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Surcharge + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "6" + "</BASICRATEOFINVOICETAX>\r\n";
                        xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<ROUNDTYPE>" + "Normal Rounding" + "</ROUNDTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "OUTPUT TAX CGST-6 %" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<ROUNDLIMIT>" + "1" + "</ROUNDLIMIT>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + CGST + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + CGST + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "6" + "</BASICRATEOFINVOICETAX>\r\n";
                        xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<ROUNDTYPE>" + "Normal Rounding" + "</ROUNDTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "OUTPUT TAX SGST-6%" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<ROUNDLIMIT>" + "1" + "</ROUNDLIMIT>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + SGST + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + SGST + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                        xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</BODY>";
                        xmlstc1 = xmlstc1 + "</ENVELOPE>";

                        HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9090");
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

                    }



                }
                else if (Resort == "KARMA SITABANI")
                {

                    if (CGST == 0 && SGST == 0)
                    {
                        int i = 728;
                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP(Sitabani)</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Sales" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";
                        xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                      //  xmlstc1 = xmlstc1 + "<DATE>" + "20210301" + "</DATE>\r\n";
                        xmlstc1 = xmlstc1 + "<NARRATION>" + Transaction_Id + " " + receipt_No + " " + Transmittal_Date + " " + Booking_ID + " " + Client_Name + "</NARRATION>\r\n";
                        xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</PARTYLEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Sales" + "</VOUCHERTYPENAME>\r\n";
                        xmlstc1 = xmlstc1 + "<REFERENCE>" + receipt_No + "</REFERENCE>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                        xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                       // xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + "20210301" + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>-" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Booking Fees (Mktg.) Exempted" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Booking_Fees + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORY>" + "Primary Cost Category" + "</CATEGORY>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + "Non Member" + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                        xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</BODY>";
                        xmlstc1 = xmlstc1 + "</ENVELOPE>";

                        HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9090");
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



                    }
                    else
                    {
                        int i = 728;
                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP(Sitabani)</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Sales" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";
                        xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                       // xmlstc1 = xmlstc1 + "<DATE>" + "20210301" + "</DATE>\r\n";
                        xmlstc1 = xmlstc1 + "<NARRATION>" + Transaction_Id + " " + receipt_No + " " + Transmittal_Date + " " + Booking_ID + " " + Client_Name + "</NARRATION>\r\n";
                        xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</PARTYLEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Sales" + "</VOUCHERTYPENAME>\r\n";
                        xmlstc1 = xmlstc1 + "<REFERENCE>" + receipt_No + "</REFERENCE>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                        xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                      //  xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + "20210301" + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>-" + Total + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Booking Fees (Marketing) 12%" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Booking_Fees + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORY>" + "Primary Cost Category" + "</CATEGORY>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + "Non Member" + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Surcharge(12 % Booking)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Surcharge + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Surcharge + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "6" + "</BASICRATEOFINVOICETAX>\r\n";
                        xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<ROUNDTYPE>" + "Normal Rounding" + "</ROUNDTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "OUTPUT TAX CGST-6 %" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<ROUNDLIMIT>" + "1" + "</ROUNDLIMIT>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + CGST + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + CGST + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "6" + "</BASICRATEOFINVOICETAX>\r\n";
                        xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<ROUNDTYPE>" + "Normal Rounding" + "</ROUNDTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "OUTPUT TAX SGST-6%" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<ROUNDLIMIT>" + "1" + "</ROUNDLIMIT>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + SGST + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + SGST + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                        xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</BODY>";
                        xmlstc1 = xmlstc1 + "</ENVELOPE>";

                        HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9090");
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

                    }


                }
                else if (Resort == "KARMA EXOTICA")
                {
                    if (CGST == 0 && SGST == 0)
                    {
                        int i = 444;
                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP (Dharamshala)</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Sales" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";
                        xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                       // xmlstc1 = xmlstc1 + "<DATE>" + "20210301" + "</DATE>\r\n";
                        xmlstc1 = xmlstc1 + "<NARRATION>" + Transaction_Id + " " + receipt_No + " " + Transmittal_Date + " " + Booking_ID + " " + Client_Name + "</NARRATION>\r\n";
                        xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</PARTYLEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Sales" + "</VOUCHERTYPENAME>\r\n";
                        xmlstc1 = xmlstc1 + "<REFERENCE>" + receipt_No + "</REFERENCE>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                        xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                        //xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + "20210301" + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>-" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Booking Fees (Mktg.) Exempted" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Booking_Fees + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORY>" + "Primary Cost Category" + "</CATEGORY>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + "Non Mem" + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</BODY>";
                        xmlstc1 = xmlstc1 + "</ENVELOPE>";

                        HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9090");
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



                    }
                    else
                    {
                        int i = 444;
                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP (Dharamshala)</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Sales" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";
                        xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                     //   xmlstc1 = xmlstc1 + "<DATE>" + "20210301" + "</DATE>\r\n";
                        xmlstc1 = xmlstc1 + "<NARRATION>" + Transaction_Id + " " + receipt_No + " " + Transmittal_Date + " " + Booking_ID + " " + Client_Name + "</NARRATION>\r\n";
                        xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</PARTYLEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Sales" + "</VOUCHERTYPENAME>\r\n";
                        xmlstc1 = xmlstc1 + "<REFERENCE>" + receipt_No + "</REFERENCE>\r\n";
                        xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                        xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                      //  xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + "20210301" + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                        xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Prestige Holiday Resorts (Booking Fees)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>-" + Total + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Booking Fees (Marketing) 12%" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Booking_Fees + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<CATEGORY>" + "Primary Cost Category" + "</CATEGORY>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + "Non Mem" + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Booking_Fees + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Surcharge(12 % Booking)" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + Surcharge + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Surcharge + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "6" + "</BASICRATEOFINVOICETAX>\r\n";
                        xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<ROUNDTYPE>" + "Normal Rounding" + "</ROUNDTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "OUTPUT TAX CGST-6 %" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<ROUNDLIMIT>" + "1" + "</ROUNDLIMIT>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + CGST + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + CGST + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                        xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "6" + "</BASICRATEOFINVOICETAX>\r\n";
                        xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";

                        xmlstc1 = xmlstc1 + "<ROUNDTYPE>" + "Normal Rounding" + "</ROUNDTYPE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "OUTPUT TAX SGST-6%" + "</LEDGERNAME>\r\n";
                        xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                        xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                        xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                        xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                        xmlstc1 = xmlstc1 + "<ROUNDLIMIT>" + "1" + "</ROUNDLIMIT>\r\n";
                        xmlstc1 = xmlstc1 + "<AMOUNT>" + SGST + "</AMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + SGST + "</VATEXPAMOUNT>\r\n";
                        xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                        xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</BODY>";
                        xmlstc1 = xmlstc1 + "</ENVELOPE>";

                        HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9090");
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

                    }






                }
                else
                {

                }

                if (result.Contains("<CREATED>1</CREATED>"))
                {
                    string query1 = "update TallySyncAcc_Mrktg_Pay set status='Inactive' where Receipt_No='" + receipt_No + "' and Resort='" + resort + "'";
                    SqlCommand cmd1 = new SqlCommand(query1, sqlcon1);
                    cmd1.ExecuteNonQuery();
                }



            }

            Label5.Text = "Updated Successfully!!!";
            Response.AppendHeader("Refresh", "04;url=TallySyncMrktg_Pay.aspx");

        }

        catch (Exception ex)
        {
            Label6.Text = "Failed :" + ex.Message;
            Response.AppendHeader("Refresh", "10;url=TallySyncMrktg_Pay.aspx");
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
                sqlcon.Close();

            }

              
          
        }
        
          
          Label2.Text = "Updated Successfully!!!";
          Response.AppendHeader("Refresh", "04;url=TallySyncMrktg_Pay.aspx");

      }
    
      catch (Exception ex)
      {
          Label3.Text = "Failed :"+ex.Message;
          Response.AppendHeader("Refresh", "10;url=TallySyncMrktg_Pay.aspx");
      }



    
    }

}

