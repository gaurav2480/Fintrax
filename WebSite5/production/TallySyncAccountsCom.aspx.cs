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

public partial class WebSite5_production_TallySyncAccountsCom : System.Web.UI.Page
{




    [WebMethod]
    public static string data()
    {
       
        string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        SqlConnection sqlcon = new SqlConnection(conn);
        string JSON = "{\n \"names\":[";
        string query = "select distinct Type from TallySyncAccountsCom  where  status='Active' and Total > 0 ;";
        sqlcon.Open();
        SqlCommand cmd = new SqlCommand(query, sqlcon);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
              
                string Type = reader.GetString(0);
               

                JSON += "[\"" + Type + "\"],";

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


            string Type = Request.Form["typeValue"];
            string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection sqlcon1 = new SqlConnection(conn);
            sqlcon1.Open();
                
            string selectDataQuery = "select ID,Date,InvoiceNo,Name,Total,CGST,SGST,IGST,TDS,Net,Narration,Type from TallySyncAccountsCom where status='Active' and Type='" + Type + "'";
            SqlCommand cmd2 = new SqlCommand(selectDataQuery, sqlcon1);
            SqlDataReader reader1 = cmd2.ExecuteReader();
            while (reader1.Read())
            {
                string result = "";
                int ID = reader1.GetInt32(0);
                DateTime Date = reader1.GetDateTime(1);
                string InvoiceNo = reader1.GetString(2);
                string Name = reader1.GetString(3);
                double Total = reader1.GetDouble(4);
                double CGST = reader1.GetDouble(5);
                double SGST = reader1.GetDouble(6);
                double IGST = reader1.GetDouble(7);
                double TDS = reader1.GetDouble(8);
                double Net = reader1.GetDouble(9);
                string Narration = reader1.GetString(10);
                string Types = reader1.GetString(11);

                string ledgerType = "";

                if(Types=="COM FIXED")
                {
                    if (Total > 0 && CGST > 0 && SGST > 0)
                    {
                        ledgerType = "Com Fixed Comm (Reg)";
                    }
                    else if (Total > 0 && CGST == 0 && SGST == 0 && IGST > 0)
                    {
                        ledgerType = "Com Fixed Comm (Reg-IGST)";

                    }
                    else if (Total > 0 && CGST == 0 && SGST == 0 && IGST == 0)
                    {
                        ledgerType = "COM FIXED COMMISSION (Unreg)";
                    }

                      
                }else if (Types == "SPIFF/BONUS")
                {
                    if (Total > 0 && CGST > 0 && SGST > 0)
                    {
                        ledgerType = "Spiff/Bonus ( Registered)";
                    }
                    else if (Total > 0 && CGST == 0 && SGST == 0 && IGST > 0)
                    {
                        ledgerType = "Spiff/Bonus (REG- IGST)";

                    }
                    else if (Total > 0 && CGST == 0 && SGST == 0 && IGST == 0)
                    {
                        ledgerType = "Spiff/Bonus (Unreg)";
                    }
                    

                }else if (Types == "COM BONUS")
                {
                    if (Total > 0 && CGST > 0 && SGST > 0)
                    {
                        ledgerType = "Com Bonus (Reg)";
                    }
                    else if (Total > 0 && CGST == 0 && SGST == 0 && IGST > 0)
                    {
                        ledgerType = "Com Bonus (Reg-IGST)";

                    }
                    else if (Total > 0 && CGST == 0 && SGST == 0 && IGST == 0)
                    {
                        ledgerType = "COM-BONUS (Unreg)";
                    }
                }

                DataSet ds = Fintrax.GSTIN_Details(Name);
                string GSTIN_No= ds.Tables[0].Rows[0]["GSTIN"].ToString();
                string State = ds.Tables[0].Rows[0]["STATE"].ToString();




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



                if (Total>0 && CGST > 0 && SGST > 0)
                {
                    int i = 1451;
                    string xmlstc1 = "<ENVELOPE>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP</SVCURRENTCOMPANY>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHER   VCHTYPE =" + "\"" + "Purchase" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";

                    xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS.LIST TYPE='String'>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>@ Haathi Mahal Mobor,Cavelossim</BASICBUYERADDRESS>\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>Salcette-Goa 403731</BASICBUYERADDRESS>\r\n";
                    xmlstc1 = xmlstc1 + "</BASICBUYERADDRESS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                    xmlstc1 = xmlstc1 + "<REFERENCEDATE>" + year + "" + month + "" + day + "</REFERENCEDATE>\r\n";
                    xmlstc1 = xmlstc1 + "<STATENAME>"+ State + "</STATENAME>\r\n";
                 
                    xmlstc1 = xmlstc1 + "<NARRATION>" + Narration + "</NARRATION>\r\n";
                    xmlstc1 = xmlstc1 + "<COUNTRYOFRESIDENCE>India</COUNTRYOFRESIDENCE>\r\n";
                    xmlstc1 = xmlstc1 + "<PARTYGSTIN>" + GSTIN_No + "</PARTYGSTIN>\r\n";
                    xmlstc1 = xmlstc1 + "<PLACEOFSUPPLY>"+ State + "</PLACEOFSUPPLY>\r\n";
                    xmlstc1 = xmlstc1 + "<PARTYNAME>" + Name + "</PARTYNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + Name + "</PARTYLEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Purchase" + "</VOUCHERTYPENAME>\r\n";
                    xmlstc1 = xmlstc1 + "<REFERENCE>" + InvoiceNo + "</REFERENCE>\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBASEPARTYNAME>" + Name + "</BASICBASEPARTYNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                    xmlstc1 = xmlstc1 + "<CONSIGNEEGSTIN>" + "30AATFP9052E1Z5" + "</CONSIGNEEGSTIN>\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBUYERNAME>" + "Prestige Holiday Resorts LLP" + "</BASICBUYERNAME>\r\n";

                    xmlstc1 = xmlstc1 + "<CONSIGNEESTATENAME>" + "Goa" + "</CONSIGNEESTATENAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ENTEREDBY>" + "anita" + "</ENTEREDBY>\r\n";
                    xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                    xmlstc1 = xmlstc1 + "<HASCASHFLOW>" + "Yes" + "</HASCASHFLOW>\r\n";
                    xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                    xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";
                    
                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + Name + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + Net + "</AMOUNT>\r\n";

                    xmlstc1 = xmlstc1 + "<BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<NAME>" + InvoiceNo + "</NAME>\r\n";
                    xmlstc1 = xmlstc1 + "<BILLTYPE>" + "New Ref" + "</BILLTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + Net + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                    
                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + ledgerType + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>-" + Total + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>-" + Total + "</VATEXPAMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "9" + "</BASICRATEOFINVOICETAX>\r\n";
                    xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Input Tax  CGST- 9%" + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>-" + CGST + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>-" + CGST + "</VATEXPAMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "9" + "</BASICRATEOFINVOICETAX>\r\n";
                    xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Input Tax  SGST- 9%" + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>-" + SGST + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>-" + SGST + "</VATEXPAMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Tds on Commission Payable" + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + TDS + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + TDS + "</VATEXPAMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                    
                    xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                    xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                    xmlstc1 = xmlstc1 + "</BODY>";
                    xmlstc1 = xmlstc1 + "</ENVELOPE>";

                     //  HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
                    HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://103.87.174.195:" + "9035");
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
                else if(Total > 0 && CGST ==0 && SGST==0 && IGST>0)
                {


                    int i = 1451;
                    string xmlstc1 = "<ENVELOPE>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP</SVCURRENTCOMPANY>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Purchase" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";

                    xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS.LIST TYPE='String'>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>@ Haathi Mahal Mobor,Cavelossim</BASICBUYERADDRESS>\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>Salcette-Goa 403731</BASICBUYERADDRESS>\r\n";
                    xmlstc1 = xmlstc1 + "</BASICBUYERADDRESS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                    xmlstc1 = xmlstc1 + "<REFERENCEDATE>" + year + "" + month + "" + day + "</REFERENCEDATE>\r\n";
                    xmlstc1 = xmlstc1 + "<STATENAME>"+State+"</STATENAME>\r\n";
                    xmlstc1 = xmlstc1 + "<NARRATION>" + Narration + "</NARRATION>\r\n";
                    xmlstc1 = xmlstc1 + "<COUNTRYOFRESIDENCE>India</COUNTRYOFRESIDENCE>\r\n";
                    xmlstc1 = xmlstc1 + "<PARTYGSTIN>" + GSTIN_No + "</PARTYGSTIN>\r\n";
                    xmlstc1 = xmlstc1 + "<PLACEOFSUPPLY>" + State + "</PLACEOFSUPPLY>\r\n";
                    xmlstc1 = xmlstc1 + "<PARTYNAME>" + Name + "</PARTYNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + Name + "</PARTYLEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Purchase" + "</VOUCHERTYPENAME>\r\n";
                    xmlstc1 = xmlstc1 + "<REFERENCE>" + InvoiceNo + "</REFERENCE>\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBASEPARTYNAME>" + Name + "</BASICBASEPARTYNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                    xmlstc1 = xmlstc1 + "<CONSIGNEEGSTIN>" + "30AATFP9052E1Z5" + "</CONSIGNEEGSTIN>\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBUYERNAME>" + "Prestige Holiday Resorts LLP" + "</BASICBUYERNAME>\r\n";

                    xmlstc1 = xmlstc1 + "<CONSIGNEESTATENAME>" + "Goa" + "</CONSIGNEESTATENAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ENTEREDBY>" + "anita" + "</ENTEREDBY>\r\n";
                    xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                    xmlstc1 = xmlstc1 + "<HASCASHFLOW>" + "Yes" + "</HASCASHFLOW>\r\n";
                    xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                    xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";



                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + Name + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + Net + "</AMOUNT>\r\n";

                    xmlstc1 = xmlstc1 + "<BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<NAME>" + InvoiceNo + "</NAME>\r\n";
                    xmlstc1 = xmlstc1 + "<BILLTYPE>" + "New Ref" + "</BILLTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + Net + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + ledgerType + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>-" + Total + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>-" + Total + "</VATEXPAMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE='Number'>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "18" + "</BASICRATEOFINVOICETAX>\r\n";
                    xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "INPUT TAX IGST  18%" + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>-" + IGST + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>-" + IGST + "</VATEXPAMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                    

                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Tds on Commission Payable" + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + TDS + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + TDS + "</VATEXPAMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                    xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                    xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                    xmlstc1 = xmlstc1 + "</BODY>";
                    xmlstc1 = xmlstc1 + "</ENVELOPE>";

                     //HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
                    HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://103.87.174.195:" + "9035");
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
                else if(Total > 0 && CGST == 0 && SGST == 0 && IGST == 0)
                {
                    int i = 1451;
                    string xmlstc1 = "<ENVELOPE>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP</SVCURRENTCOMPANY>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Purchase" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";
                    
                    xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS.LIST TYPE='String'>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>@ Haathi Mahal Mobor,Cavelossim</BASICBUYERADDRESS>\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>Salcette-Goa 403731</BASICBUYERADDRESS>\r\n";
                    xmlstc1 = xmlstc1 + "</BASICBUYERADDRESS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                    xmlstc1 = xmlstc1 + "<REFERENCEDATE>" + year + "" + month + "" + day + "</REFERENCEDATE>\r\n";
                    xmlstc1 = xmlstc1 + "<STATENAME>Goa</STATENAME>\r\n";
                    xmlstc1 = xmlstc1 + "<NARRATION>" + Narration + "</NARRATION>\r\n";
                    xmlstc1 = xmlstc1 + "<COUNTRYOFRESIDENCE>India</COUNTRYOFRESIDENCE>\r\n";
                    xmlstc1 = xmlstc1 + "<PLACEOFSUPPLY>Goa</PLACEOFSUPPLY>\r\n";
                    xmlstc1 = xmlstc1 + "<PARTYNAME>"+Name+"</PARTYNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + Name + "</PARTYLEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Purchase" + "</VOUCHERTYPENAME>\r\n";
                    xmlstc1 = xmlstc1 + "<REFERENCE>" + InvoiceNo + "</REFERENCE>\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                    xmlstc1 = xmlstc1 + "<BASICBASEPARTYNAME>" + Name + "</BASICBASEPARTYNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                    xmlstc1 = xmlstc1 + "<CONSIGNEEGSTIN>" + "30AATFP9052E1Z5" + "</CONSIGNEEGSTIN>\r\n"; 
                    xmlstc1 = xmlstc1 + "<BASICBUYERNAME>" + "Prestige Holiday Resorts LLP - (2feb2017 - 18)" + "</BASICBUYERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<CONSIGNEESTATENAME>" + "Goa" + "</CONSIGNEESTATENAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ENTEREDBY>" + "anita" + "</ENTEREDBY>\r\n";
                    xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                    xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                    xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";


                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + Name + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + Net + "</AMOUNT>\r\n";

                    xmlstc1 = xmlstc1 + "<BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<NAME>" + InvoiceNo + "</NAME>\r\n";
                    xmlstc1 = xmlstc1 + "<BILLTYPE>" + "New Ref" + "</BILLTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + Net + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + ledgerType + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>-" + Total + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>-" + Total + "</VATEXPAMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Tds on Commission Payable" + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + TDS + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + TDS + "</VATEXPAMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                   


                    xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                    xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                    xmlstc1 = xmlstc1 + "</BODY>";
                    xmlstc1 = xmlstc1 + "</ENVELOPE>";

                   // HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
                     HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://103.87.174.195:" + "9035");
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

              
                  

                

                if (result.Contains("<CREATED>1</CREATED>"))
                {
                    string query1 = "update TallySyncAccountsCom set status='Inactive' where ID='" + ID + "'";
                    SqlCommand cmd1 = new SqlCommand(query1, sqlcon1);
                    cmd1.ExecuteNonQuery();
                }

            }
              
            

              



            

            Label5.Text = "Updated Successfully!!!";
            Response.AppendHeader("Refresh", "03;url=TallySyncAccountsCom.aspx");

        }

        catch (Exception ex)
        {
            Label6.Text = "Failed :" + ex.Message;
            Response.AppendHeader("Refresh", "10;url=TallySyncAccountsCom.aspx");
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try {

            string type = Request.Form["type"];
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

                string selectQuery = "select * from [TallySyncAccountsCom] where InvoiceNo='" + row["DocNo"] + "' and Net='"+row["NetAmount"]+"'";
                SqlCommand cmd1 = new SqlCommand(selectQuery, sqlcon);
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {

                }else
                {
                    string InsertQuery = "Insert into TallySyncAccountsCom values('" + row["Date"] + "','" + row["DocNo"] + "','"+ row["PartyLedger"] + "','"+ row["Total"] + "','"+ row["CGST"] + "','" + row["SGST"] + "','" + row["IGST"] + "','" + row["TDS"] + "','" + row["NetAmount"] + "','"+ row["Remarks"] + "','"+ type + "','Active')";
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
          Response.AppendHeader("Refresh", "10;url=TallySyncAccountsCom.aspx");
      }



    
    }

}

