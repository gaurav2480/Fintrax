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

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            TextInfo myTI = new CultureInfo("en-US", false).TextInfo;
            string startDate = fromDate.Text;
            string endDate = toDate.Text;
            string result = "";
            string result1 = "";
            string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection sqlcon = new SqlConnection(conn);
            sqlcon.Open();
            string query = "select distinct LRID,convert(datetime,lrdate-2,103),lr.DRLEDID,lr.CRLEDID,led2.LEDGERNAME,led.LEDGERNAME,cast(lr.LEDAMOUNT as float),LR.ATID,lr.TRID,lr.NARRATION,lr.LNID,lr.REMARKS,lr.SYNID from LEDGERREGISTER LR left join LEDGER Led on LR.CRLEDID=led.LEDID left join LEDGER Led2 on lr.DRLEDID=led2.LEDID where  SYNID=0 and  (LEDAMOUNT>0) and convert(datetime,lrdate-2,103)  between convert(datetime,'2021-03-02',120) and convert(datetime,'2021-03-02',120) and ATID=1";
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
                string NARRATION = reader.GetString(9);
                string REMARKS = reader.GetString(11); 
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
                  
                    xmlstc = xmlstc + "<VOUCHER  VCHTYPE =" + "\"" + "LMS JOURNAL" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Accounting Voucher View" + "\" >\r\n";
                    xmlstc = xmlstc + "<DATE>" + "20210302" + "</DATE>\r\n";
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
                    xmlstc = xmlstc + "<EFFECTIVEDATE>" + "20210302" + "</EFFECTIVEDATE>\r\n";
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

                    xmlstc = xmlstc + "<VOUCHER  VCHTYPE =" + "\"" + "LMS RECEIPT" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Accounting Voucher View" + "\" >\r\n";
                    xmlstc = xmlstc + "<DATE>" + "20210302" + "</DATE>\r\n";
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
                    xmlstc = xmlstc + "<EFFECTIVEDATE>" + "20210302" + "</EFFECTIVEDATE>\r\n";
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
                else if (TRANSTYPE==3)
                {

                }

               


                // REMOTEID = "+"\"" + "e2847e40-a9e0-11d5-9edd-0050bad06d31-000c7297" + "\"
              /*  int i =0;
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
                //  xmlstc1 = xmlstc1 + "<DATE>" + year+""+month+""+day + "</DATE>\r\n";
                xmlstc1 = xmlstc1 + "<DATE>" + "20210202"+ "</DATE>\r\n";
                // xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + "20210202" + "</EFFECTIVEDATE>\r\n";
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



                /*     xmlstc1 = xmlstc1 + "<ALLINVENTORYENTRIES.LIST>" + "\r\n";
                     //  xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                     // xmlstc1 = xmlstc1 + "<STOCKITEMNAME>" + "LOAN DOCUMENTATION FEE(GST)" + "</STOCKITEMNAME>\r\n";
                     //   xmlstc1 = xmlstc1 + "<AMOUNT>"  + amount + "</AMOUNT>\r\n";
                     xmlstc1 = xmlstc1 + "<ACCOUNTINGALLOCATIONS.LIST>" + "\r\n";
                     xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                     xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                     xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    // xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                     xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "LOAN DOCUMENTATION FEE(GST)" + "</LEDGERNAME>\r\n";
                     xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                     xmlstc1 = xmlstc1 + "<AMOUNT>" + amount + "</AMOUNT>\r\n";
                     //xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";
                     xmlstc1 = xmlstc1 + "<ACCOUNTINGALLOCATIONS.LIST>" + "\r\n";
                     xmlstc1 = xmlstc1 + "</ALLINVENTORYENTRIES.LIST>" + "\r\n";     
                     xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                     xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                     xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                     xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                     xmlstc1 = xmlstc1 + "</BODY>";
                     xmlstc1 = xmlstc1 + "</ENVELOPE>";*/



                /*   xmlstc1 = xmlstc1 + "<ADDRESS.LIST TYPE = 'String'>" + "\r\n";
                   xmlstc1 = xmlstc1 + "<ADDRESS>" + Address + "</ADDRESS>\r\n";
                   xmlstc1 = xmlstc1 + "<ADDRESS>" + Address2 + "</ADDRESS>\r\n";
                   xmlstc1 = xmlstc1 + "<ADDRESS>" + city + "</ADDRESS>\r\n";
                   xmlstc1 = xmlstc1 + "</ADDRESS.LIST>" + "\r\n";
                   xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS.LIST TYPE = 'String'>" + "\r\n";
                   xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>" + Address + "</BASICBUYERADDRESS>\r\n";
                   xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>" + Address2 + "</BASICBUYERADDRESS>\r\n";
                   xmlstc1 = xmlstc1 + "<BASICBUYERADDRESS>" + city + "</BASICBUYERADDRESS>\r\n";
                   xmlstc1 = xmlstc1 + "</BASICBUYERADDRESS.LIST>" + "\r\n";
                   xmlstc1 = xmlstc1 + "<DATE>" + "20210105" + "</DATE>\r\n";
                   xmlstc1 = xmlstc1 + "<STATENAME>" + state  + "</STATENAME>\r\n";
                   xmlstc1 = xmlstc1 + "<NARRATION>" + "BEING DOC FEE FOR"+loanNo + "</NARRATION>\r\n";
                   xmlstc1 = xmlstc1 + "<PARTYNAME>" + name + "</PARTYNAME>\r\n";
                   xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>" + "Sales" + "</VOUCHERTYPENAME>\r\n";
                   xmlstc1 = xmlstc1 + "<REFERENCE>" + loanNo + "</REFERENCE>\r\n";
                   xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + "DF0404/FY20-21" + "</VOUCHERNUMBER>\r\n";
                   xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>" + agreementNo+"</PARTYLEDGERNAME>\r\n";
                   xmlstc1 = xmlstc1 + "<BASICBASEPARTYNAME>" + agreementNo + "</BASICBASEPARTYNAME>\r\n";
                   xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                   xmlstc1 = xmlstc1 + "<PLACEOFSUPPLY>" + state + "</PLACEOFSUPPLY>\r\n";
                   xmlstc1 = xmlstc1 + "<BASICBUYERNAME>" + name + "</BASICBUYERNAME>\r\n";
                   xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";*/
                /* xmlstc1 = xmlstc1 + "<CATEGORYALLOCATIONS.LIST>" + "\r\n";
                 xmlstc1 = xmlstc1 + "<CATEGORY>" + "Accounts" + "</CATEGORY>\r\n";
                 xmlstc1 = xmlstc1 + "<COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                 xmlstc1 = xmlstc1 + "<NAME>" + "Accounts" + "</NAME>\r\n";
                 xmlstc1 = xmlstc1 + "<AMOUNT>" + "-25637.00" + "</AMOUNT>\r\n";
                 xmlstc1 = xmlstc1 + "</COSTCENTREALLOCATIONS.LIST>" + "\r\n";
                 xmlstc1 = xmlstc1 + "</CATEGORYALLOCATIONS.LIST>" + "\r\n";*/
                /*  xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "LOAN DOCUMENTATION FEE(GST)" + "</LEDGERNAME>\r\n";
                  xmlstc1 = xmlstc1 + "<AMOUNT>" + amount + "</AMOUNT>\r\n";
                  xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";
                  xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                  xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX.LIST TYPE = 'Number'>" + "\r\n";
                  xmlstc1 = xmlstc1 + "<BASICRATEOFINVOICETAX>" + "18" + "</BASICRATEOFINVOICETAX>\r\n";
                  xmlstc1 = xmlstc1 + "</BASICRATEOFINVOICETAX.LIST>" + "\r\n";
                  xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "IGST" + "</LEDGERNAME>\r\n";
                  xmlstc1 = xmlstc1 + "<AMOUNT>" + Gst + "</AMOUNT>\r\n";
                  xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";
                  xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                  xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                  xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                  xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                  xmlstc1 = xmlstc1 + "</BODY>";
                  xmlstc1 = xmlstc1 + "</ENVELOPE>";*/



              /*
                DateTime date =  DateTime.Now;
                       

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

                if (result1.Contains("<CREATED>1</CREATED>"))
                {
                    string query1 = "insert into [ContractSyncTally] values('" + agreementNo + "','"+ date + "')";
                    SqlCommand cmd1 = new SqlCommand(query1, sqlcon);
                    cmd1.ExecuteNonQuery();
                }*/
               

               

            }
            Label2.Text = "Updated Successfully!!!";
            Response.AppendHeader("Refresh", "04;url=TallySync_Transactions.aspx");

        }

        
        catch (Exception ex)
        {
            Label3.Text = "Failed :"+ex.Message;
            Response.AppendHeader("Refresh", "10;url=TallySync_Transactions.aspx");
        }
        

    }

}