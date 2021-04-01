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

public partial class WebSite5_production_TallySyncCustomerLedger_origi : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {




    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
           // string LoanNo = loanNo.Text;
            string result1 = "";
            string result = "";
            string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(conn);
            sqlcon.Open();
            // string query = "select LN.LOANNO,LED.LEDGERNAME,LEDAD.ADDRESS1,LEDAD.ADDRESS2,LEDAD.CITY,LEDAD.[STATE],LEDAD.PIN,LEDAD.COUNTRY,LEDAD.ADDRESS3 as Pancard,LT.LTID,month(LN.LNDATE),Year(LN.LNDATE)  from LOANS LN join LEDGER LED on LN.LEDID = LED.LEDID join LEDGERADDRESS LEDAD on LED.LEDID = LEDAD.LEDID join LOANTYPE LT on LT.LTID = LN.LTID  where LN.ACTIVE in(3,0) and ln.DISBURSEMENTSTATUS = 0 and LOANNO in('PS624Z3')";
            string query = "declare @Table as table ([Count] int, LOANNO varchar(50),LEDGERNAME varchar(150),[Address1] varchar(150),[Address2] varchar(150),[CITY] varchar(80),[STATE] varchar(80), PIN varchar(20), COUNTRY varchar(20), Pan varchar(30), LoanType bigint, loanMonth int, LoanYear int) insert into @Table select distinct  count(BDS.CSIDENTITY),LN.LOANNO,LED.LEDGERNAME,LEDAD.ADDRESS1,LEDAD.ADDRESS2,LEDAD.CITY,LEDAD.[STATE],LEDAD.PIN,LEDAD.COUNTRY,LEDAD.ADDRESS3 as Pancard,LT.LTID,month(convert(datetime,ln.lndate-2,103)),Year(convert(datetime,ln.lndate-2,103))  from LOANS LN join LEDGER LED on LN.LEDID = LED.LEDID join LEDGERADDRESS LEDAD on LED.LEDID = LEDAD.LEDID join LOANTYPE LT on LT.LTID = LN.LTID join BANKDEPOSITSLIPDETAILS BDS on LN.LNID = BDS.LNID join LEDGERREGISTER LR on LN.LNID=LR.LNID where LN.ACTIVE in(3,0) and ln.DISBURSEMENTSTATUS = 0 and BDS.CHSTATID in(0,1) and(BDS.INSTALLMENTNO like '%DOC FEE%' or BDS.INSTALLMENTNO like '%1ST EMI%' or BDS.INSTALLMENTNO like '%FULL AND FINAL%') and LR.SYNID=0  group by LN.LOANNO,LED.LEDGERNAME,LN.EMIAMOUNT,LEDAD.ADDRESS1,LEDAD.ADDRESS2,LEDAD.CITY ,LEDAD.[STATE],LEDAD.PIN,LN.LNDATE,LEDAD.COUNTRY,LEDAD.ADDRESS3,LT.LTID select LOANNO,LEDGERNAME,Address1,Address2,CITY,[STATE],PIN,COUNTRY,Pan,LoanType,loanMonth,LoanYear from @Table";
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string xmlstc1 = "";
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

                if(loanMonth >=4 && loanYear==2020 || loanMonth <= 3 && loanYear==2021)
                {
                    if (loantype == 26)
                    {
                        LedgerGroup = "FY-2020-21-25%Deposit 3 Months Int Waiver";
                    }
                    else
                    {
                        LedgerGroup = "FY-2020-21-10%Deposit 3 Months Int Waiver";
                    }

                    xmlstc1 = "<ENVELOPE>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REPORTNAME>ALL Masters</REPORTNAME>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                    xmlstc1 = xmlstc1 + "<GROUP NAME=" + "\"" + LedgerGroup + "\" Action =" + "\"" + "Create" + "\">\r\n";
                    xmlstc1 = xmlstc1 + "<NAME.LIST TYPE = 'String'>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<NAME>" + LedgerGroup + "</NAME>\r\n";
                    // xmlstc1 = xmlstc1 + "<NAME>" + LoanNo + "</NAME>\r\n";
                    xmlstc1 = xmlstc1 + "</NAME.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<PARENT>" + "Loan Contracts Sanctioned" + "</PARENT>\r\n";
                    xmlstc1 = xmlstc1 + "</GROUP>\r\n";
                    xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                    xmlstc1 = xmlstc1 + "</BODY>";
                    xmlstc1 = xmlstc1 + "</ENVELOPE>";

                

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

                    xmlstc1 = "<ENVELOPE>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<HEADER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<TALLYREQUEST>Import Data</TALLYREQUEST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</HEADER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<BODY>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<IMPORTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REQUESTDESC>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REPORTNAME>ALL Masters</REPORTNAME>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDESC>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<REQUESTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                    xmlstc1 = xmlstc1 + "<GROUP NAME=" + "\"" + LedgerGroup + "\" Action =" + "\"" + "Create" + "\">\r\n";
                    xmlstc1 = xmlstc1 + "<NAME.LIST TYPE = 'String'>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<NAME>" + LedgerGroup + "</NAME>\r\n";
                    // xmlstc1 = xmlstc1 + "<NAME>" + LoanNo + "</NAME>\r\n";
                    xmlstc1 = xmlstc1 + "</NAME.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<PARENT>" + "Loan Contracts Sanctioned" + "</PARENT>\r\n";
                    xmlstc1 = xmlstc1 + "</GROUP>\r\n";
                    xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDATA>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                    xmlstc1 = xmlstc1 + "</BODY>";
                    xmlstc1 = xmlstc1 + "</ENVELOPE>";


                }

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
                    result1 = sr1.ReadToEnd();
                    sr1.Close();
                }



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
                xmlstc = xmlstc + "<REPORTNAME>ALL Masters</REPORTNAME>" + "\r\n";
                xmlstc = xmlstc + "<STATICVARIABLES>" + "\r\n";
                xmlstc = xmlstc + "<SVCURRENTCOMPANY>Parshuram Finance Pvt. Ltd.</SVCURRENTCOMPANY>" + "\r\n";
                xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
                xmlstc = xmlstc + "</REQUESTDESC>" + "\r\n";
                xmlstc = xmlstc + "<REQUESTDATA>" + "\r\n";
                xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                //  xmlstc = xmlstc + "<VOUCHER VCHTYPE=" + "\"" + "Purchase" + "\" >" + "\r\n";
                //     xmlstc = xmlstc + "<DATE>" + strGRNDate + "</DATE>" + "\r\n";
                //    xmlstc = xmlstc + "<VOUCHERTYPENAME>Purchase</VOUCHERTYPENAME>" + "\r\n";
                //   xmlstc = xmlstc + "<VOUCHERNUMBER>" + strVoucherNo + "</VOUCHERNUMBER>" + "\r\n";
                //   xmlstc = xmlstc + "<REFERENCE>" + strGRNNo + "</REFERENCE>" + "\r\n";
                /* xmlstc = xmlstc + "<PARTYLEDGERNAME>" + strSupplierName + "</PARTYLEDGERNAME>" + "\r\n";
                 xmlstc = xmlstc + "<PARTYNAME>" + strSupplierName + "</PARTYNAME>" + "\r\n";
                 xmlstc = xmlstc + "<EFFECTIVEDATE>" + strGRNDate + "</EFFECTIVEDATE>" + "\r\n";
                 xmlstc = xmlstc + "<ISINVOICE>Yes</ISINVOICE>" + "\r\n";
                 xmlstc = xmlstc + "<INVOICEORDERLIST.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<BASICORDERDATE>" + strGRNDate + "</BASICORDERDATE>" + "\r\n";
                 xmlstc = xmlstc + "<BASICPURCHASEORDERNO>" + strPurchaseOrder + "</BASICPURCHASEORDERNO>" + "\r\n";
                 xmlstc = xmlstc + "</INVOICEORDERLIST.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<LEDGERNAME>" + strSupplierName + "</LEDGERNAME>" + "\r\n";
                 xmlstc = xmlstc + "<GSTCLASS/>" + "\r\n";
                 xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>No</ISDEEMEDPOSITIVE>" + "\r\n";
                 xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                 xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                 xmlstc = xmlstc + "<ISPARTYLEDGER>Yes</ISPARTYLEDGER>" + "\r\n";
                 xmlstc = xmlstc + "<AMOUNT>" + strGRN + "</AMOUNT>" + "\r\n";
                 xmlstc = xmlstc + "<BILLALLOCATIONS.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<NAME>" + strGRNNo + "</NAME>" + "\r\n";
                 xmlstc = xmlstc + "<BILLCREDITPERIOD>30 Days</BILLCREDITPERIOD>" + "\r\n";
                 xmlstc = xmlstc + "<BILLTYPE>New Ref</BILLTYPE>" + "\r\n";
                 xmlstc = xmlstc + "<AMOUNT>" + strGRN + "</AMOUNT>" + "\r\n";
                 xmlstc = xmlstc + "</BILLALLOCATIONS.LIST>" + "\r\n";
                 xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                 xmlstc = xmlstc + "<LEDGERNAME>Abhinav Sharma</LEDGERNAME>" + "\r\n";
                 xmlstc = xmlstc + "<GSTCLASS/>" + "\r\n";
                 xmlstc = xmlstc + "<ISDEEMEDPOSITIVE>Yes</ISDEEMEDPOSITIVE>" + "\r\n";
                 xmlstc = xmlstc + "<LEDGERFROMITEM>No</LEDGERFROMITEM>" + "\r\n";
                 xmlstc = xmlstc + "<REMOVEZEROENTRIES>No</REMOVEZEROENTRIES>" + "\r\n";
                 xmlstc = xmlstc + "<ISPARTYLEDGER>No</ISPARTYLEDGER>" + "\r\n";
                 xmlstc = xmlstc + "<AMOUNT>" + strGRNValueNtv + "</AMOUNT>" + "\r\n";
                 xmlstc = xmlstc + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                 xmlstc = xmlstc + "</VOUCHER>" + "\r\n";
                 xmlstc = xmlstc + "</TALLYMESSAGE>" + "\r\n";
                 xmlstc = xmlstc + "</REQUESTDATA>" + "\r\n";
                 xmlstc = xmlstc + "</IMPORTDATA>" + "\r\n";
                 xmlstc = xmlstc + "</BODY>" + "\r\n";
                 xmlstc = xmlstc + "</ENVELOPE>" + "\r\n";*/
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
                //  xmlstc = xmlstc + "<OPENINGBALANCE>" + "100000.00"+ "</OPENINGBALANCE>\r\n";
                // xmlstc = xmlstc + "<ISBILLWISEON>Yes</ISBILLWISEON>\r\n";
                xmlstc = xmlstc + "</LEDGER>\r\n";
                xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
                xmlstc = xmlstc + "</REQUESTDATA>\r\n";
                xmlstc = xmlstc + "</IMPORTDATA>\r\n";
                xmlstc = xmlstc + "</BODY>";
                xmlstc = xmlstc + "</ENVELOPE>";

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
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
                    sr.Close();
                }
            }
            Label2.Text = "Updated Successfully!!!";
            Response.AppendHeader("Refresh", "04;url=TallySyncCustomerLedger.aspx");

        }

        
        catch (Exception ex)
        {
            Label3.Text = "Failed :"+ex.Message;
            Response.AppendHeader("Refresh", "10;url=TallySyncCustomerLedger.aspx");
        }
        

    }

}