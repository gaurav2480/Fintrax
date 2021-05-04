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

public partial class WebSite5_production_TallySyncCustomerLedger : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {




    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
           
            int j = 0;
            int k = 0;
            int l = 0;

            int m = 0;
            int n = 0;
            int o = 0;
            // string LoanNo = loanNo.Text;
            string result1 = "";
            string result = "";
            string log = "";
            string log1 = "";
            string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(conn);
            sqlcon.Open();
            // string query = "select LN.LOANNO,LED.LEDGERNAME,LEDAD.ADDRESS1,LEDAD.ADDRESS2,LEDAD.CITY,LEDAD.[STATE],LEDAD.PIN,LEDAD.COUNTRY,LEDAD.ADDRESS3 as Pancard,LT.LTID,month(LN.LNDATE),Year(LN.LNDATE)  from LOANS LN join LEDGER LED on LN.LEDID = LED.LEDID join LEDGERADDRESS LEDAD on LED.LEDID = LEDAD.LEDID join LOANTYPE LT on LT.LTID = LN.LTID  where LN.ACTIVE in(3,0) and ln.DISBURSEMENTSTATUS = 0 and LOANNO in('PS624Z3')";
            string query = "declare @Table as table ([Count] int, LOANNO varchar(50),LEDGERNAME varchar(150),[Address1] varchar(150),[Address2] varchar(150),[CITY] varchar(80),[STATE] varchar(80), PIN varchar(20), COUNTRY varchar(20), Pan varchar(30), LoanType bigint, loanMonth int, LoanYear int) insert into @Table select distinct count(BDS.CSIDENTITY),LN.LOANNO,LED.LEDGERNAME,LEDAD.ADDRESS1,LEDAD.ADDRESS2,LEDAD.CITY,LEDAD.[STATE],LEDAD.PIN,LEDAD.COUNTRY,LEDAD.ADDRESS3 as Pancard,LT.LTID,month(convert(datetime,ln.lndate-2,103)),Year(convert(datetime,ln.lndate-2,103))  from LOANS LN join LEDGER LED on LN.LEDID = LED.LEDID join LEDGERADDRESS LEDAD on LED.LEDID = LEDAD.LEDID join LOANTYPE LT on LT.LTID = LN.LTID join BANKDEPOSITSLIPDETAILS BDS on LN.LNID = BDS.LNID join LEDGERREGISTER LR on LN.LNID=LR.LNID where LN.ACTIVE in(3,0) and ln.DISBURSEMENTSTATUS = 0 and BDS.CHSTATID in(0,1) and(BDS.INSTALLMENTNO like '%DOC FEE%' or BDS.INSTALLMENTNO like '%1ST EMI%' or BDS.INSTALLMENTNO like '%1ST%' or BDS.INSTALLMENTNO like '%FULL AND FINAL%') and LR.SYNID=0  group by LN.LOANNO,LED.LEDGERNAME,LN.EMIAMOUNT,LEDAD.ADDRESS1,LEDAD.ADDRESS2,LEDAD.CITY ,LEDAD.[STATE],LEDAD.PIN,LN.LNDATE,LEDAD.COUNTRY,LEDAD.ADDRESS3,LT.LTID select LOANNO,LEDGERNAME,Address1,Address2,CITY,[STATE],PIN,COUNTRY,Pan,LoanType,loanMonth,LoanYear from @Table union select  distinct '',case when led2.LEDGERNAME like 'AP%' then led2.LEDGERNAME when led.LEDGERNAME like 'AP%' then led.LEDGERNAME end as [Name] ,'','','','','','','','',0,Year(convert(datetime,ln.lndate-2,103)) from LEDGERREGISTER LR left join LEDGER Led on LR.CRLEDID=led.LEDID left join LEDGER Led2 on lr.DRLEDID=led2.LEDID left join loans ln on lr.LNID=ln.LNID where  SYNID=0 and  (LEDAMOUNT>0) and (led2.LEDGERNAME like 'AP%' or led.LEDGERNAME like 'AP%') and ln.LNID!=0 and ATID in(1,2)";
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
               // string xmlstc1 = "";
                string LedgerGroup = "";
                string LOANNO = reader.GetString(0);
                string name = reader.GetString(1);
                string Address = reader.GetString(2);
                string Address2 = reader.GetString(3);
                string city = reader.GetString(4);
                string state = reader.GetString(5);
                string pin = reader.GetString(6);
                string country = reader.GetString(7);
                string pan = reader.GetString(8);
                long loantype = reader.GetInt64(9);
                int loanMonth = reader.GetInt32(10);
                int loanYear = reader.GetInt32(11);

                if (LOANNO == "")
                {
                    if (loanMonth == 0 && loanYear == 2020 || loanMonth == 0 && loanYear == 2021 || loanMonth == 0 && loanYear == 2022)
                    {
                        if (loanYear == 2020)
                        {
                            LedgerGroup = "Accrued Interest on Interest Free Loans - FY-2020";
                        }
                        else if (loanYear == 2021)
                        {
                            LedgerGroup = "Accrued Interest on Interest Free Loans - FY-2021";

                        }
                        else if (loanYear == 2022)
                        {
                            LedgerGroup = "Accrued Interest on Interest Free Loans - FY-2022";
                        }


                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>All Masters</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                        // xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<GROUP NAME=" + "\"" + LedgerGroup + "\" Action =" + "\"" + "Create" + "\">\r\n";
                        xmlstc1 = xmlstc1 + "<NAME.LIST TYPE = 'String'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + LedgerGroup + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "</NAME.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<PARENT>" + "Accrued Interest" + "</PARENT>\r\n";
                        xmlstc1 = xmlstc1 + "</GROUP>\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>" + "\r\n";
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
                            result1 = sr1.ReadToEnd();
                            log += result1 + "\r\n";
                            sr1.Close();
                        }


                    }

                }
                else
                {

                    if (loanMonth >= 4 && loanYear == 2020 || loanMonth <= 3 && loanYear == 2021)
                    {
                        if (loantype == 26)
                        {
                            LedgerGroup = "FY-2020-21-25%Deposit 3 Months Int Waiver";
                        }
                        else
                        {
                            LedgerGroup = "FY-2020-21-10%Deposit 3 Months Int Waiver";
                        }

                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>All Masters</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                        //    xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n"; 
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<GROUP NAME=" + "\"" + LedgerGroup + "\" Action =" + "\"" + "Create" + "\">\r\n";
                        xmlstc1 = xmlstc1 + "<NAME.LIST TYPE = 'String'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + LedgerGroup + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "</NAME.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<PARENT>" + "Loan Contracts Sanctioned" + "</PARENT>\r\n";
                        xmlstc1 = xmlstc1 + "</GROUP>\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</BODY>";
                        xmlstc1 = xmlstc1 + "</ENVELOPE>";
                        //    HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
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
                            result1 = sr1.ReadToEnd();
                            log += result1 + "\r\n";
                            sr1.Close();
                        }
                        Label2.Text = result1;




                    }
                    else if (loanMonth >= 4 && loanYear == 2021 || loanMonth <= 3 && loanYear == 2022)
                    {
                        if (loantype == 26)
                        {
                            LedgerGroup = "FY-2021-22-25%Deposit 3 Months Int Waiver";
                        }
                        else
                        {
                            LedgerGroup = "FY-2021-22-10%Deposit 3 Months Int Waiver";
                        }

                        string xmlstc1 = "<ENVELOPE>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REPORTNAME>All Masters</REPORTNAME>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                        // xmlstc1 = xmlstc1 + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</STATICVARIABLES>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                        xmlstc1 = xmlstc1 + "<GROUP NAME=" + "\"" + LedgerGroup + "\" Action =" + "\"" + "Create" + "\">\r\n";
                        xmlstc1 = xmlstc1 + "<NAME.LIST TYPE = 'String'>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<NAME>" + LedgerGroup + "</NAME>\r\n";
                        xmlstc1 = xmlstc1 + "</NAME.LIST>" + "\r\n";
                        xmlstc1 = xmlstc1 + "<PARENT>" + "Loan Contracts Sanctioned" + "</PARENT>\r\n";
                        xmlstc1 = xmlstc1 + "</GROUP>\r\n";
                        xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                        xmlstc1 = xmlstc1 + "</REQUESTDATA>" + "\r\n";
                        xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                        xmlstc1 = xmlstc1 + "</BODY>";
                        xmlstc1 = xmlstc1 + "</ENVELOPE>";
                        //     HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
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
                            result1 = sr1.ReadToEnd();
                            log += result1 + "\r\n";
                            sr1.Close();
                        }
                        //   Label2.Text = result1;

                    }
                }


 if (result1.Contains("<CREATED>1</CREATED>"))
                {
                    j++;
                }

               else  if (result1.Contains("<ALTERED>1</ALTERED>"))
                {
                    k++;
                }

               else  if (result1.Contains("<ERRORS>1</ERRORS>"))
                {
                    l++;
                }
          



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
             //   xmlstc = xmlstc + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                        xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
                        xmlstc = xmlstc + "</REQUESTDESC>" + "\r\n";
                xmlstc = xmlstc + "<REQUESTDATA>" + "\r\n";
                xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                xmlstc = xmlstc + "<LEDGER NAME=" + "\"" + name + "\" Action =" + "\"" + "Create" + "\">\r\n";
                xmlstc = xmlstc + "<NAME.LIST TYPE = 'String'>" + "\r\n";
                xmlstc = xmlstc + "<NAME>" + name + "</NAME>\r\n";
                xmlstc = xmlstc + "<NAME>" + LOANNO + "</NAME>\r\n";
                xmlstc = xmlstc + "</NAME.LIST>" + "\r\n";
                xmlstc = xmlstc + "<COUNTRYNAME>" + country + "</COUNTRYNAME>\r\n";
                xmlstc = xmlstc + "<LEDSTATENAME>" + state + "</LEDSTATENAME>\r\n";
                xmlstc = xmlstc + "<PINCODE>" + pin + "</PINCODE>\r\n";
                xmlstc = xmlstc + "<MAILINGNAME.LIST TYPE = 'String'>" + "\r\n";
                xmlstc = xmlstc + "<MAILINGNAME>" + name + "</MAILINGNAME>\r\n";
                xmlstc = xmlstc + "</MAILINGNAME.LIST>" + "\r\n";
                xmlstc = xmlstc + "<ADDRESS.LIST TYPE = 'String'>" + "\r\n";
                xmlstc = xmlstc + "<ADDRESS>" + Address + "</ADDRESS>\r\n";
                xmlstc = xmlstc + "<ADDRESS>" + Address2 + "</ADDRESS>\r\n";
                xmlstc = xmlstc + "<ADDRESS>" + city + "</ADDRESS>\r\n";
                xmlstc = xmlstc + "</ADDRESS.LIST>" + "\r\n";
                xmlstc = xmlstc + "<INCOMETAXNUMBER>" + pan + "</INCOMETAXNUMBER>\r\n";
                xmlstc = xmlstc + "<PARENT>" + LedgerGroup + "</PARENT>\r\n";
                xmlstc = xmlstc + "</LEDGER>\r\n";
                xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
                xmlstc = xmlstc + "</REQUESTDATA>\r\n";
                xmlstc = xmlstc + "</IMPORTDATA>\r\n";
                xmlstc = xmlstc + "</BODY>";
                xmlstc = xmlstc + "</ENVELOPE>";

                //   HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://103.87.174.195:" + "9035");
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
 log1 += result + "\r\n";
                    sr.Close();
                }

  if (result.Contains("<CREATED>1</CREATED>"))
                {
                    m++;

                }
               else  if (result.Contains("<ALTERED>1</ALTERED>"))
                {
                    n++;
                }

               else  if (result.Contains("<ERRORS>1</ERRORS>"))
                {
                    o++;
                }


            }

	    log += "GROUP CREATION: CREATED:" + j + " ALTERED:" + k + " ERRORS:" + l + "\r\n";
            log1 += "CUSTOMER LEDGER CREATION: CREATED:" + m + " ALTERED: " + n + " ERRORS: " + o + " ";


            string data = log + " " + log1;
            // string path = "C:/Tally_Logs/";
            string path = @"\\192.168.0.16\C$\Tally_Logs\";
            VerifyDir(path);
            string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
            file.WriteLine(data);
            file.Close();

//  System.Diagnostics.Process.Start(@"notepad.exe", @"\\192.168.0.16\C$\Tally_Logs\2142021_Logs.txt");
            Label2.Text = "Updated Successfully!!!";

// Label2.Text = result;
            Response.AppendHeader("Refresh", "04;url=TallySyncCustomerLedger.aspx");

        }

        
        catch (Exception ex)
        {
            Label3.Text = "Failed :"+ex.Message;
            Response.AppendHeader("Refresh", "10;url=TallySyncCustomerLedger.aspx");
        }
        

    }

    public static void VerifyDir(string path)
    {
        try
        {
            DirectoryInfo dir = new DirectoryInfo(path);


            if (!dir.Exists)
            {
                dir.Create();
            }
            else
            {
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

}