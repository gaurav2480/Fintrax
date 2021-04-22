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
using System.Text;
public partial class WebSite5_production_TallySyncPA_NO : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {




    }


 public static void VerifyDir(string path)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(path);
           
            if (!dir.Exists)
            {
                dir.Create();
            }else{
            foreach (FileInfo file in dir.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo di in dir.GetDirectories())
            {
                dir.Delete(true);
            }
dir.Create();

}
        }
        catch (Exception ex)
        {

        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {   int j = 0;
            int k = 0;
            int l = 0;

            int m = 0;
            int n = 0;
            int o = 0;
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            string startDate = fromDate.Text;
            string endDate = toDate.Text;
            string result = "";
            string result1 = "";
  	    string log = "";
            string log1 = "";
            string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(conn);
            sqlcon.Open();
            string query = "select LN.PURCHASEAGGREMENTNO,LEDAD.ADDRESS1,LEDAD.ADDRESS2,LEDAD.CITY,LEDAD.[STATE],LEDAD.PIN,LEDAD.COUNTRY,LEDAD.ADDRESS3 as Pancard,LED.LEDGERNAME,cast(LN.DOCFEEVALUE as float),cast(LN.SERVICETAXVALUE as float),LN.LOANNO,Convert(datetime,LN.LNDATE-2,103),month(convert(datetime,ln.lndate-2,103)),Year(convert(datetime,ln.lndate-2,103))   from LOANS LN join LEDGER LED on LN.LEDID=LED.LEDID join LEDGERADDRESS LEDAD on LED.LEDID=LEDAD.LEDID  where LN.ACTIVE in(0,3) and LN.DISBURSEMENTSTATUS=0 and LN.DOCFEEVALUE not in(0) and convert(datetime,LN.LNDATE-2,103) between convert(datetime,'" + startDate + "',120) and convert(datetime,'"+endDate+ "',120) and LN.PURCHASEAGGREMENTNO not in(select ContractNo from [ContractSyncTally])";
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                //string LedgerGroup = "";
                string agreementNo = reader.GetString(0);
                string Address = reader.GetString(1);
                string Address2 = reader.GetString(2);
                string city = reader.GetString(3);
                string state = reader.GetString(4);
                string pin = reader.GetString(5);
                string country = reader.GetString(6);
                string pan = reader.GetString(7);
                string name = reader.GetString(8);
                double amount = reader.GetDouble(9);
                double Gst = reader.GetDouble(10);
                string loanNo = reader.GetString(11);
                double total = amount + Gst;
                DateTime dates = reader.GetDateTime(12);
                int loanMonth = reader.GetInt32(13);
                int loanYear = reader.GetInt32(14);

                string stateName = myTI.ToTitleCase(state.ToLower());


                int dayInt = dates.Day;
                string day = dates.Day.ToString();
                if (dayInt < 10)
                {
                    day = "0" + day;
                }
                int monthInt = dates.Month;
                string month = dates.Month.ToString();
                if(monthInt < 10)
                {
                    month = "0" + month;
                }
                int year = dates.Year;
                //  string strGRNDate = "2/02/2014";
                //  string strVoucherNo = "3";
                //  string strGRNNo = "2";
                //   string strSupplierName = "Abhinav Sharma";
                //  string strPurchaseOrder = "2";
                //   string strGRN = "1";
                //   string strGRNValueNtv = "2";


                string xmlstc = "<ENVELOPE>" + "\r\n";
                xmlstc = xmlstc + "<HEADER>" + "\r\n";
                xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                xmlstc = xmlstc + "</HEADER>" + "\r\n";
                xmlstc = xmlstc + "<BODY>" + "\r\n";
                xmlstc = xmlstc + "<IMPORTDATA>" + "\r\n";
                xmlstc = xmlstc + "<REQUESTDESC>" + "\r\n";
                xmlstc = xmlstc + "<REPORTNAME>All Masters</REPORTNAME>" + "\r\n";
                xmlstc = xmlstc + "<STATICVARIABLES>" + "\r\n";
                xmlstc = xmlstc + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
                xmlstc = xmlstc + "</REQUESTDESC>" + "\r\n";
                xmlstc = xmlstc + "<REQUESTDATA>" + "\r\n";
                xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                xmlstc = xmlstc + "<LEDGER NAME=" + "\"" + agreementNo + "\" Action =" + "\"" + "Create" + "\">\r\n";
                xmlstc = xmlstc + "<NAME.LIST TYPE = 'String'>" + "\r\n";
                xmlstc = xmlstc + "<NAME>" + agreementNo + "</NAME>\r\n";
                xmlstc = xmlstc + "</NAME.LIST>" + "\r\n";
                xmlstc = xmlstc + "<COUNTRYNAME>" + country + "</COUNTRYNAME>\r\n";
                xmlstc = xmlstc + "<LEDSTATENAME>" + stateName + "</LEDSTATENAME>\r\n";
                xmlstc = xmlstc + "<PINCODE>" + pin + "</PINCODE>\r\n";
                xmlstc = xmlstc + "<MAILINGNAME.LIST TYPE = 'String'>" + "\r\n";
                xmlstc = xmlstc + "<MAILINGNAME>" + name + "</MAILINGNAME>\r\n";
                xmlstc = xmlstc + "</MAILINGNAME.LIST>" + "\r\n";
                xmlstc = xmlstc + "<ADDRESS.LIST TYPE = 'String'>" + "\r\n";
                xmlstc = xmlstc + "<ADDRESS>" + Address + "</ADDRESS>\r\n";
                xmlstc = xmlstc + "<ADDRESS>" + Address2 + "</ADDRESS>\r\n";
                xmlstc = xmlstc + "<ADDRESS>" + city + "</ADDRESS>\r\n";
                xmlstc = xmlstc + "<GSTREGISTRATIONTYPE>" + "Unregistered" + "</GSTREGISTRATIONTYPE>\r\n";
                xmlstc = xmlstc + "</ADDRESS.LIST>" + "\r\n";
                xmlstc = xmlstc + "<INCOMETAXNUMBER>" + pan + "</INCOMETAXNUMBER>\r\n";
                xmlstc = xmlstc + "<PARENT>" + "Debtors for Documentation Fee" + "</PARENT>\r\n";
                xmlstc = xmlstc + "</LEDGER>\r\n";
                xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
                xmlstc = xmlstc + "</REQUESTDATA>\r\n";
                xmlstc = xmlstc + "</IMPORTDATA>\r\n";
                xmlstc = xmlstc + "</BODY>";
                xmlstc = xmlstc + "</ENVELOPE>";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://103.87.174.195:" + "9028");
                httpWebRequest.Method = "POST";
                httpWebRequest.ContentLength = xmlstc.Length;
                httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                streamWriter.Write(xmlstc);
                streamWriter.Close();

                HttpWebResponse objResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                {
                    result = sr.ReadToEnd();
  	            log += result + "\r\n";
                    sr.Close();
                }


	       if (result.Contains("<CREATED>1</CREATED>"))
                {
                    j++;
                }

              else if (result.Contains("<ALTERED>1</ALTERED>"))
                {
                    k++;
                }

              else if (result.Contains("<ERRORS>1</ERRORS>"))
                {
                    l++;
                }

                // REMOTEID = "+"\"" + "e2847e40-a9e0-11d5-9edd-0050bad06d31-000c7297" + "\"
                int i =0;
                string voucherNo="";
                if (loanMonth >= 4 && loanYear == 2020 || loanMonth <= 3 && loanYear == 2021)
                {

                    i = 485;
                    voucherNo = "DF0" + i + "/FY20-21";



                }
                else if (loanMonth >= 4 && loanYear == 2021 || loanMonth <= 3 && loanYear == 2022)
                {
                    i = 001;
                    voucherNo = "DF0" + i + "/FY21-22";
                }

                string xmlstc1 = "<ENVELOPE>" + "\r\n";
                xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                xmlstc1 = xmlstc1 + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\""+ "Sales"+"\"  Action =" + "\"" + "Create" + "\"  OBJVIEW="+"\""+"Invoice Voucher View"+"\" >\r\n";
                xmlstc1 = xmlstc1 + "<ISOPTIONAL>" + "No" + "</ISOPTIONAL>\r\n";
                xmlstc1 = xmlstc1 + "<USEFORGAINLOSS>" + "No" + "</USEFORGAINLOSS>\r\n";
                xmlstc1 = xmlstc1 + "<USEFORCOMPOUND>" + "No" + "</USEFORCOMPOUND>\r\n";
                xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Sales" + "</VOUCHERTYPENAME>\r\n";
                xmlstc1 = xmlstc1 + "<DATE>" + year+""+month+""+day + "</DATE>\r\n";
              //  xmlstc1 = xmlstc1 + "<DATE>" + "20210202"+ "</DATE>\r\n";
                xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
             //   xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + "20210202" + "</EFFECTIVEDATE>\r\n";
                xmlstc1 = xmlstc1 + "<ISCANCELLED>" + "No" + "</ISCANCELLED>\r\n";
                xmlstc1 = xmlstc1 + "<USETRACKINGNUMBER>" + "No" + "</USETRACKINGNUMBER>\r\n";
                xmlstc1 = xmlstc1 + "<ISPOSTDATED>" + "No" + "</ISPOSTDATED>\r\n";
                xmlstc1 = xmlstc1 + "<ISINVOICE>" + "Yes" + "</ISINVOICE>\r\n";
                xmlstc1 = xmlstc1 + "<DIFFACTUALQTY>" + "No" + "</DIFFACTUALQTY>\r\n";
                xmlstc1 = xmlstc1 + "<ASPAYSLIP>" + "No" + "</ASPAYSLIP>\r\n";
                xmlstc1 = xmlstc1 + "<ADDRESS.LIST TYPE = 'String'>" + "\r\n";
                xmlstc1 = xmlstc1 + "<ADDRESS>" + Address + "</ADDRESS>\r\n";
                xmlstc1 = xmlstc1 + "<ADDRESS>" + Address2 + "</ADDRESS>\r\n";
                xmlstc1 = xmlstc1 + "<ADDRESS>" + city + "</ADDRESS>\r\n";
                xmlstc1 = xmlstc1 + "</ADDRESS.LIST>" + "\r\n";

                xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS.LIST TYPE = 'String'>" + "\r\n";
                xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>" + Address + "</BASICBUYERADDRESS>\r\n";
                xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>" + Address2 + "</BASICBUYERADDRESS>\r\n";
                xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>" + city + "</BASICBUYERADDRESS>\r\n";
                xmlstc1 = xmlstc1 + "</BASICBUYERADDRESS.LIST>" + "\r\n";
          
               xmlstc1 = xmlstc1 + "<STATENAME>" + stateName + "</STATENAME>\r\n";
              //  xmlstc1 = xmlstc1 + "<GUID>" + "e2847e40-a9e0-11d5-9edd-0050bad06d31-000c7297" + " </GUID>\r\n";
                xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + voucherNo + "</VOUCHERNUMBER>\r\n";
                xmlstc1 = xmlstc1 + "<REFERENCE>" + loanNo + "</REFERENCE>\r\n";
                xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + agreementNo + "</PARTYLEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                xmlstc1 = xmlstc1 + "<PLACEOFSUPPLY>" + stateName + "</PLACEOFSUPPLY>\r\n";
                xmlstc1 = xmlstc1 + "<NARRATION>BEING DOC FEE FOR " + loanNo + "</NARRATION>\r\n";


                xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERNAME>" + agreementNo + "</LEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n"; 
                xmlstc1 = xmlstc1 + "<AMOUNT>" +"-"+ total + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<CATEGORY>" + "Accounts" + "</CATEGORY>\r\n";
                xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<NAME>" + "Accounts" + "</NAME>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>" + "-" + total + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<BILLALLOCATIONS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<NAME>" + voucherNo + "</NAME>\r\n";
                xmlstc1 = xmlstc1 + "<BILLTYPE>" + "New Ref" + "</BILLTYPE>\r\n";
                xmlstc1 = xmlstc1 + "<TDSDEDUCTEEISSPECIALRATE>" + "No" + "</TDSDEDUCTEEISSPECIALRATE>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>" + "-" + total + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "</BILLALLOCATIONS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "LOAN DOCUMENTATION FEE(GST)" + "</LEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>"  + amount + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + amount + "</VATEXPAMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "<RATEDETAILS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<GSTRATEDUTYHEAD>" + "Integrated Tax" + "</GSTRATEDUTYHEAD>\r\n";
                xmlstc1 = xmlstc1 + "<GSTRATEVALUATIONTYPE>" + "Based on Value" + "</GSTRATEVALUATIONTYPE>\r\n";
                xmlstc1 = xmlstc1 + "</RATEDETAILS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<RATEDETAILS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<GSTRATEDUTYHEAD>" + "Central Tax" + "</GSTRATEDUTYHEAD>\r\n";
                xmlstc1 = xmlstc1 + "<GSTRATEVALUATIONTYPE>" + "Based on Value" + "</GSTRATEVALUATIONTYPE>\r\n";
                xmlstc1 = xmlstc1 + "</RATEDETAILS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<RATEDETAILS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<GSTRATEDUTYHEAD>" + "State Tax" + "</GSTRATEDUTYHEAD>\r\n";
                xmlstc1 = xmlstc1 + "<GSTRATEVALUATIONTYPE>" + "Based on Value" + "</GSTRATEVALUATIONTYPE>\r\n";
                xmlstc1 = xmlstc1 + "</RATEDETAILS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<RATEDETAILS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<GSTRATEDUTYHEAD>" + "Cess" + "</GSTRATEDUTYHEAD>\r\n";
                xmlstc1 = xmlstc1 + "<GSTRATEVALUATIONTYPE>" + "Based on Value" + "</GSTRATEVALUATIONTYPE>\r\n";
                xmlstc1 = xmlstc1 + "</RATEDETAILS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE = 'Number'>" + "\r\n";
                xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "18" + "</BASICRATEOFINVOICETAX>\r\n";
                xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<ROUNDTYPE>" + "Normal Rounding" + "</ROUNDTYPE>\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "IGST" + "</LEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<ROUNDLIMIT>" + "1" + "</ROUNDLIMIT>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>" + Gst + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + Gst + "</VATEXPAMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "<GSTDUTYAMOUNT>" + "0.00" + "</GSTDUTYAMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";



                xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                xmlstc1 = xmlstc1 + "</BODY>";
                xmlstc1 = xmlstc1 + "</ENVELOPE>";
                i++;

                
              
                DateTime date =  DateTime.Now;
                       

                HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://103.87.174.195:" + "9028");
                httpWebRequest1.Method = "POST";
                httpWebRequest1.ContentLength = xmlstc1.Length;
                httpWebRequest1.ContentType = "application/x-www-form-urlencoded";
                StreamWriter streamWriter1 = new StreamWriter(httpWebRequest1.GetRequestStream());
                streamWriter1.Write(xmlstc1);
                streamWriter1.Close();

                HttpWebResponse objResponse1 = (HttpWebResponse)httpWebRequest1.GetResponse();
                using (StreamReader sr1 = new StreamReader(objResponse1.GetResponseStream()))
                {
                    result1 = sr1.ReadToEnd();
  		    log1 += result1 + "\r\n";
                    sr1.Close();
                }

                 if (result1.Contains("<CREATED>1</CREATED>"))
                {
                    m++;
                    string query1 = "insert into [ContractSyncTally] values('" + agreementNo + "','"+ date + "')";
                    SqlCommand cmd1 = new SqlCommand(query1, sqlcon);
                    cmd1.ExecuteNonQuery();

                }
                else if (result1.Contains("<ALTERED>1</ALTERED>"))
                {
                    n++;
                }

               else if (result1.Contains("<ERRORS>1</ERRORS>"))
                {
                    o++;
                }

                

            }
            log += "PA LEDGER CREATION: CREATED:" + j + " ALTERED:" + k + " ERRORS:" + l + "\r\n";
            log1 += "SALES VOUCHER CREATION: CREATED:" + m + " ALTERED: " + n + " ERRORS: " + o + " ";

            string data = log + " " + log1;
            string path = @"\\192.168.0.16\C$\Tally_Logs\";
            //string path = "C:/Log/";
            VerifyDir(path);
            string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
          
           System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
//System.IO.StreamWriter file = new System.IO.StreamWriter(File.Open(path + fileName, FileMode.OpenOrCreate, FileAccess.Write), Encoding.UTF8);
            file.WriteLine(data);
            file.Close();

            Label2.Text = "Updated Successfully!!!";
            Response.AppendHeader("Refresh", "04;url=TallySyncPA_NO.aspx");

        }

        
        catch (Exception ex)
        {
            Label3.Text = "Failed :"+ex.Message;
            Response.AppendHeader("Refresh", "10;url=TallySyncPA_NO.aspx");
        }
        

    }

}