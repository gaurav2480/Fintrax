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
public partial class WebSite5_production_TallySync_Transactions : System.Web.UI.Page
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
    {string NARRATION="";
string REMARKS="";
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
            string query = "select  distinct LRID,convert(datetime,lrdate-2,103),lr.DRLEDID,lr.CRLEDID,led2.LEDGERNAME,led.LEDGERNAME,cast(lr.LEDAMOUNT as float),LR.ATID,lr.TRID,lr.NARRATION,lr.LNID,lr.REMARKS,lr.SYNID from LEDGERREGISTER LR left join LEDGER Led on LR.CRLEDID=led.LEDID left join LEDGER Led2 on lr.DRLEDID=led2.LEDID where  SYNID=0 and  (LEDAMOUNT>0) and convert(datetime,lrdate-2,103)  between convert(datetime,'"+ startDate + "',120) and convert(datetime,'"+endDate+"',120) and ATID in(1,2)";
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
             //   string xmlstc = "";
                //string LedgerGroup = "";
                long LRID = reader.GetInt64(0);
                DateTime LRDATE = reader.GetDateTime(1);
                string DEBITLED = reader.GetString(4);
                string CREDITLED = reader.GetString(5);
                double AMOUNT = reader.GetDouble(6);
                long TRANSTYPE = reader.GetInt64(7);
                NARRATION = reader.GetString(9);
                REMARKS = reader.GetString(11);
  		NARRATION = NARRATION.Replace("  ", " ");
	        NARRATION = NARRATION.Replace("   ", " ");
		NARRATION = NARRATION.Replace(" (", "(");
		NARRATION = NARRATION.Replace("( ", "(");
		NARRATION = NARRATION.Replace(" )", ")");
		
                REMARKS = REMARKS.Replace("  ", " ");
		REMARKS = REMARKS.Replace("   ", " ");
		REMARKS = REMARKS.Replace(" (", "(");
                REMARKS = REMARKS.Replace("( ", "(");
 		REMARKS = REMARKS.Replace(" )", ")");
		
 
             //   DateTime dates = reader.GetDateTime(12);
            //    int loanMonth = reader.GetInt32(13);
                //int loanYear = reader.GetInt32(14);

              //  string stateName = myTI.ToTitleCase(state.ToLower());


                int dayInt = LRDATE.Day;
                string day = LRDATE.Day.ToString();
                if (dayInt < 10)
                {
                    day = "0" + day;
                }
                int monthInt = LRDATE.Month;
                string month = LRDATE.Month.ToString();
                if(monthInt < 10)
                {
                    month = "0" + month;
                }
                int year = LRDATE.Year;

                if (TRANSTYPE== 2)
                {
                    string xmlstc = "<ENVELOPE>" + "\r\n";
                    xmlstc = xmlstc + "<HEADER>" + "\r\n";
                    xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                    xmlstc = xmlstc + "</HEADER>" + "\r\n";
                    xmlstc = xmlstc + "<BODY>" + "\r\n";
                    xmlstc = xmlstc + "<IMPORTDATA>" + "\r\n";
                    xmlstc = xmlstc + "<REQUESTDESC>" + "\r\n";
                    xmlstc = xmlstc + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                    xmlstc = xmlstc + "<STATICVARIABLES>" + "\r\n";
                    xmlstc = xmlstc + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                    xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
                    xmlstc = xmlstc + "</REQUESTDESC>" + "\r\n";
                    xmlstc = xmlstc + "<REQUESTDATA>" + "\r\n";
                    xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                  
                    xmlstc = xmlstc + "<VOUCHER   VCHTYPE =" + "\"" + "LMS JOURNAL" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Accounting Voucher View" + "\" >\r\n";
                    xmlstc = xmlstc + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                    xmlstc = xmlstc + "<NARRATION>" + NARRATION+" "+REMARKS + "</NARRATION>\r\n";
                    xmlstc = xmlstc + "<PARTYNAME>" + DEBITLED + "</PARTYNAME>\r\n";
                    xmlstc = xmlstc + "<PARTYLEDGERNAME>" + DEBITLED + "</PARTYLEDGERNAME>\r\n";
                    xmlstc = xmlstc + "<PARTYLEDGERNAME>" + DEBITLED + "</PARTYLEDGERNAME>\r\n";
                    xmlstc = xmlstc + "<VOUCHERTYPENAME>" + "LMS JOURNAL" + "</VOUCHERTYPENAME>\r\n";
                    xmlstc = xmlstc + "<VOUCHERNUMBER>" + LRID + "</VOUCHERNUMBER>\r\n";
                    xmlstc = xmlstc + "<BASICBASEPARTYNAME>" + DEBITLED + "</BASICBASEPARTYNAME>\r\n";
                    xmlstc = xmlstc + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                    xmlstc = xmlstc + "<PERSISTEDVIEW>" + "Accounting Voucher View" + "</PERSISTEDVIEW>\r\n";
                    xmlstc = xmlstc + "<ENTEREDBY>" + "Finance" + "</ENTEREDBY>\r\n";
                    xmlstc = xmlstc + "<VOUCHERTYPEORIGNAME>" + "LMS JOURNAL" + "</VOUCHERTYPEORIGNAME>\r\n";
                    xmlstc = xmlstc + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                    xmlstc = xmlstc + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";

                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERNAME>" + DEBITLED + "</LEDGERNAME>\r\n";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc = xmlstc + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc = xmlstc + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc = xmlstc + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                    xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc = xmlstc + "<AMOUNT>-" + AMOUNT + "</AMOUNT>\r\n";
                    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";

                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERNAME>" + CREDITLED + "</LEDGERNAME>\r\n";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc = xmlstc + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc = xmlstc + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc = xmlstc + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                    xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc = xmlstc + "<AMOUNT>"+ AMOUNT + "</AMOUNT>\r\n";
                    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";



                    xmlstc = xmlstc + "</VOUCHER>\r\n";
                    xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
                    xmlstc = xmlstc + "</REQUESTDATA>\r\n";
                    xmlstc = xmlstc + "</IMPORTDATA>\r\n";
                    xmlstc = xmlstc + "</BODY>";
                    xmlstc = xmlstc + "</ENVELOPE>";


                 HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
                    //    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://103.87.174.195:" + "9035");
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


                }
                else if(TRANSTYPE ==1)
                {

                    string xmlstc = "<ENVELOPE>" + "\r\n";
                    xmlstc = xmlstc + "<HEADER>" + "\r\n";
                    xmlstc = xmlstc + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                    xmlstc = xmlstc + "</HEADER>" + "\r\n";
                    xmlstc = xmlstc + "<BODY>" + "\r\n";
                    xmlstc = xmlstc + "<IMPORTDATA>" + "\r\n";
                    xmlstc = xmlstc + "<REQUESTDESC>" + "\r\n";
                    xmlstc = xmlstc + "<REPORTNAME>Vouchers</REPORTNAME>" + "\r\n";
                    xmlstc = xmlstc + "<STATICVARIABLES>" + "\r\n";
                    xmlstc = xmlstc + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                    xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
                    xmlstc = xmlstc + "</REQUESTDESC>" + "\r\n";
                    xmlstc = xmlstc + "<REQUESTDATA>" + "\r\n";
                    xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";

                    xmlstc = xmlstc + "<VOUCHER DATE='01-Jan-2019' TAGNAME = 'Voucher Number' TAGVALUE='450481'  VCHTYPE =" + "\"" + "LMS RECEIPT" + "\"  Action =" + "\"" + "Alter" + "\"  OBJVIEW=" + "\"" + "Accounting Voucher View" + "\" >\r\n";
                    xmlstc = xmlstc + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                    xmlstc = xmlstc + "<NARRATION>" + NARRATION + " " + REMARKS + "</NARRATION>\r\n";
                    xmlstc = xmlstc + "<PARTYNAME>" + CREDITLED + "</PARTYNAME>\r\n";
                    xmlstc = xmlstc + "<PARTYLEDGERNAME>" + CREDITLED + "</PARTYLEDGERNAME>\r\n";
                    xmlstc = xmlstc + "<VOUCHERTYPENAME>" + "LMS RECEIPT" + "</VOUCHERTYPENAME>\r\n";
                    xmlstc = xmlstc + "<VOUCHERNUMBER>" + LRID + "</VOUCHERNUMBER>\r\n";
                    xmlstc = xmlstc + "<BASICBASEPARTYNAME>" + CREDITLED + "</BASICBASEPARTYNAME>\r\n";
                    xmlstc = xmlstc + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                    xmlstc = xmlstc + "<PERSISTEDVIEW>" + "Accounting Voucher View" + "</PERSISTEDVIEW>\r\n";
                    xmlstc = xmlstc + "<ENTEREDBY>" + "Finance" + "</ENTEREDBY>\r\n";
                    xmlstc = xmlstc + "<VOUCHERTYPEORIGNAME>" + "LMS RECEIPT" + "</VOUCHERTYPEORIGNAME>\r\n";
                    xmlstc = xmlstc + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                    xmlstc = xmlstc + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";
                    xmlstc = xmlstc + "<HASCASHFLOW>" + "Yes" + "</HASCASHFLOW>\r\n";


                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERNAME>" + CREDITLED + "</LEDGERNAME>\r\n";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc = xmlstc + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc = xmlstc + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc = xmlstc + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                    xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc = xmlstc + "<AMOUNT>" + AMOUNT + "</AMOUNT>\r\n";
                    xmlstc = xmlstc + "<VATEXPAMOUNT>" + AMOUNT + "</VATEXPAMOUNT>\r\n";
                    xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<NAME>" + REMARKS + "</NAME>\r\n";
                    xmlstc = xmlstc + "<BILLTYPE>" + "New Ref" + "</BILLTYPE>\r\n";
                    xmlstc = xmlstc + "<AMOUNT>" + AMOUNT + "</AMOUNT>\r\n";
                    xmlstc = xmlstc + "</BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";

                 

                    xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<LEDGERNAME>" + DEBITLED + "</LEDGERNAME>\r\n";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc = xmlstc + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc = xmlstc + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc = xmlstc + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                    xmlstc = xmlstc + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc = xmlstc + "<AMOUNT>-" + AMOUNT + "</AMOUNT>\r\n";
                    xmlstc = xmlstc + "<VATEXPAMOUNT>-" + AMOUNT + "</VATEXPAMOUNT>\r\n";
                    xmlstc = xmlstc + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<CATEGORY>" + "Primary Cost Category" + "</CATEGORY>\r\n";
                    xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc = xmlstc + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<NAME>" + "Thrash" + "</NAME>\r\n";
                    xmlstc = xmlstc + "<AMOUNT>-" + AMOUNT + "</AMOUNT>\r\n";
                    xmlstc = xmlstc + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                    xmlstc = xmlstc + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";
                    xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";

                    
                    xmlstc = xmlstc + "</VOUCHER>\r\n";
                    xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
                    xmlstc = xmlstc + "</REQUESTDATA>\r\n";
                    xmlstc = xmlstc + "</IMPORTDATA>\r\n";
                    xmlstc = xmlstc + "</BODY>";
                    xmlstc = xmlstc + "</ENVELOPE>";

                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
                    //    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://103.87.174.195:" + "9035");
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


                }
                else if (TRANSTYPE==3)
                {

                }


                if (result.Contains("<CREATED>1</CREATED>"))
                {
		    j++;
                    string query1 = "update LEDGERREGISTER set SYNID=1 where LRID='"+LRID+"'";
                    SqlCommand cmd1 = new SqlCommand(query1, sqlcon);
                    cmd1.ExecuteNonQuery();
                }
                  else if (result.Contains("<ALTERED>1</ALTERED>"))
                {
                    k++;
                }

                else if (result.Contains("<ERRORS>1</ERRORS>"))
                {
                    l++;
                }

            }
 	   log += "LEDGER REGISTER SYNC: CREATED:" + j + " ALTERED:" + k + " ERRORS:" + l + "\r\n";
           

            string data = log;

                string path = @"\\192.168.0.16\C$\Tally_Logs\";
           //string path = "C:/Tally_Logs/";
            VerifyDir(path);
            string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
            file.WriteLine(data);
            file.Close();

           Label2.Text = "Updated Successfully!!!";
 // Label2.Text = result ;
            Response.AppendHeader("Refresh", "04;url=TallySync_Transactions.aspx");

            reader.Close();
            sqlcon.Close();
        }

        
        catch (Exception ex)
        {
           // Label3.Text = "Failed :"+ex.Message;

            Label3.Text = "Failed :"+REMARKS+" "+NARRATION;
            Response.AppendHeader("Refresh", "10;url=TallySync_Transactions.aspx");
        }
        

    }

}