using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebSite5_production_demo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string result="";
            string conn = ConfigurationManager.ConnectionStrings["Fintrax"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(conn);
            sqlcon.Open();
            //     string query = "declare @Table as table ([Count] int,LOANNO varchar(50),LEDGERNAME varchar(150),[Address] varchar(150),[STATE] varchar(50),PIN varchar(20),COUNTRY varchar(20),Pan varchar(30),LoanType varchar(10)) insert into @Table select count(BDS.CSIDENTITY),LN.LOANNO,LED.LEDGERNAME+'('+cast(LN.EMIAMOUNT as varchar)+')',LEDAD.ADDRESS1+' '+LEDAD.ADDRESS2+' '+LEDAD.CITY as [Address],LEDAD.[STATE],LEDAD.PIN,LEDAD.COUNTRY,LEDAD.ADDRESS3 as Pancard,LT.LTID  from LOANS LN join LEDGER LED on LN.LEDID=LED.LEDID join LEDGERADDRESS LEDAD on LED.LEDID=LEDAD.LEDID join LOANTYPE LT on LT.LTID=LN.LTID join BANKDEPOSITSLIPDETAILS BDS on LN.LNID=BDS.LNID where LN.ACTIVE in(3,0) and ln.DISBURSEMENTSTATUS =0 group by LN.LOANNO,LED.LEDGERNAME,LN.EMIAMOUNT,LEDAD.ADDRESS1,LEDAD.ADDRESS2,LEDAD.CITY ,LEDAD.[STATE],LEDAD.PIN,LEDAD.COUNTRY,LEDAD.ADDRESS3,LT.LTID select * from @Table where [Count]=1 and LOANNO in('PS624Z3','PS2480Z3')";
            string query = "select LN.LOANNO,LED.LEDGERNAME,LEDAD.ADDRESS1,LEDAD.ADDRESS2,LEDAD.CITY,LEDAD.[STATE],LEDAD.PIN,LEDAD.COUNTRY,LEDAD.ADDRESS3 as Pancard,LT.LTID from LOANS LN join LEDGER LED on LN.LEDID = LED.LEDID join LEDGERADDRESS LEDAD on LED.LEDID = LEDAD.LEDID join LOANTYPE LT on LT.LTID = LN.LTID  where LN.ACTIVE in(3,0) and ln.DISBURSEMENTSTATUS = 0 and LOANNO in('P2K1921Z3')";
            SqlCommand cmd = new SqlCommand(query, sqlcon);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string LedgerGroup = "";
                string LoanNo = reader.GetString(1);
                string Name = reader.GetString(2);
                string Address = reader.GetString(3);
                string state = reader.GetString(4);
                string pin = reader.GetString(5);
                string country = reader.GetString(6);
                string pan = reader.GetString(7);
                string loantype = reader.GetString(8);

                if (loantype == "26")
                {
                    LedgerGroup = "FY-2020-21-25%Deposit 3 Months Int Waiver";
                }
                else
                {
                    LedgerGroup = "FY-2020-21-10%Deposit 3 Months Int Waiver";
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
                //  xmlstc = xmlstc + "<STATICVARIABLES>" + "\r\n";
                //  xmlstc = xmlstc + "<SVCURRENTCOMPANY>SVCURRENTCOMPANY</SVCURRENTCOMPANY>" + "\r\n";
                //  xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
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
                xmlstc = xmlstc + "<LEDGER NAME=" + "\"" + Name + "\" Action =" + "\"" + "Create" + "\">\r\n";
                xmlstc = xmlstc + "<NAME.LIST TYPE = 'String'>" + "\r\n";
                xmlstc = xmlstc + "<NAME>" + Name + "</NAME>\r\n";
                xmlstc = xmlstc + "<NAME>" + LoanNo + "</NAME>\r\n";
                xmlstc = xmlstc + "</NAME.LIST>" + "\r\n";
                xmlstc = xmlstc + "<COUNTRYNAME>" + country + "</COUNTRYNAME>\r\n";
                xmlstc = xmlstc + "<LEDSTATENAME>" + state + "</LEDSTATENAME>\r\n";
                xmlstc = xmlstc + "<PINCODE>" + pin + "</PINCODE>\r\n";
                xmlstc = xmlstc + "<MAILINGNAME.LIST TYPE = 'String'>" + "\r\n";
                xmlstc = xmlstc + "<MAILINGNAME>" + Name + "</MAILINGNAME>\r\n";
                xmlstc = xmlstc + "</MAILINGNAME.LIST>" + "\r\n";
                xmlstc = xmlstc + "<ADDRESS.LIST TYPE = 'String'>" + "\r\n";
                xmlstc = xmlstc + "<ADDRESS>" + Address + "</ADDRESS>\r\n";
              //  xmlstc = xmlstc + "<ADDRESS>" + "CUNCOLIM" + "</ADDRESS>\r\n";
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
            Label1.Text = result;

        }



           
        catch (Exception ex)
        {

        }
    }
}