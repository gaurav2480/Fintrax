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

public partial class WebSite5_production_TallySync_DSR : System.Web.UI.Page
{
    

    [WebMethod]
    public static string data()
    {
       
        string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        SqlConnection sqlcon = new SqlConnection(conn);
        string JSON = "{\n \"names\":[";
        string query = "select distinct Profile_Venue from Tally_Sync_DSR  where  status='Active';";
        sqlcon.Open();
        SqlCommand cmd = new SqlCommand(query, sqlcon);
        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.HasRows)
        {
            while (reader.Read())
            {
              
                string Profile_Venue = reader.GetString(0);
               

                JSON += "[\"" + Profile_Venue + "\"],";

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


            string venue = Request.Form["venue"];
            string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection sqlcon1 = new SqlConnection(conn);
            sqlcon1.Open(); 

            string selectDataQuery = "select * from Tally_Sync_DSR where Status='Active' and Profile_Venue ='"+ venue + "'";
            SqlCommand cmd2 = new SqlCommand(selectDataQuery, sqlcon1);
            SqlDataReader reader1 = cmd2.ExecuteReader();
            while (reader1.Read())
            {
                string ledgerGroup = "";
                string ledgerType = "";
                string result = "";
                int ID = reader1.GetInt32(0);
                string contractNo = reader1.GetString(1);
                string panCard = reader1.GetString(2);
                string name = reader1.GetString(3);
                string contractType = reader1.GetString(4);
               // string venue = reader1.GetString(5);
                string financeDetails = reader1.GetString(6);
                double totalPurchasePrice = reader1.GetDouble(7);
                double CGST = reader1.GetDouble(8);
                double SGST = reader1.GetDouble(9);
                double totalPurchasePriceTax = reader1.GetDouble(10);
                DateTime Date = reader1.GetDateTime(12);

                
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

               
                if (contractType== "")
                {

                }
                else
                {
                    if (contractType == "Trade-In-Fractionals" || contractType == "Fractional")
                    {
                        ledgerType = "OPEN CONTRACT (KARMA)";
                        if (financeDetails == "Finance")
                        {
                            ledgerGroup = "SUNDRY DEBTORS (KARMA@18% FINANCE)";

                        }
                        else
                        {
                            ledgerGroup = "SUNDRY DEBTORS (KARMA @ 18% NF)";
                        }

                    }
                    else
                    {
                        ledgerType = "Open Contract (Pts)";
                        if (financeDetails == "Finance")
                        {
                            ledgerGroup = "Sundry Debtors Pts (Finance@18%)";

                        }
                        else
                        {
                            ledgerGroup = "Sundry Debtors Pts (NF@18%)";
                        }
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
                    xmlstc = xmlstc + "<SVCURRENTCOMPANY>Prestige Holiday Resorts LLP - (2feb2017-18)</SVCURRENTCOMPANY>" + "\r\n";
                    xmlstc = xmlstc + "</STATICVARIABLES>" + "\r\n";
                    xmlstc = xmlstc + "</REQUESTDESC>" + "\r\n";
                    xmlstc = xmlstc + "<REQUESTDATA>" + "\r\n";
                    xmlstc = xmlstc + "<TALLYMESSAGE xmlns:UDF=" + "\"" + "TallyUDF" + "\" >" + "\r\n";
                    xmlstc = xmlstc + "<LEDGER NAME=" + "\"" + contractNo + "\" Action =" + "\"" + "Create" + "\">\r\n";
                    xmlstc = xmlstc + "<NAME.LIST TYPE = 'String'>" + "\r\n";
                    xmlstc = xmlstc + "<NAME>" + contractNo + "</NAME>\r\n";
                    //xmlstc = xmlstc + "<NAME>" + LOANNO + "</NAME>\r\n";
                    xmlstc = xmlstc + "</NAME.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<COUNTRYNAME>" + "India" + "</COUNTRYNAME>\r\n";
                    xmlstc = xmlstc + "<LEDSTATENAME>" + "Goa" + "</LEDSTATENAME>\r\n";
                    xmlstc = xmlstc + "<GSTREGISTRATIONTYPE>" + "Regular" + "</GSTREGISTRATIONTYPE>\r\n";
                    //xmlstc = xmlstc + "<LEDSTATENAME>" + state + "</LEDSTATENAME>\r\n";
                    //xmlstc = xmlstc + "<PINCODE>" + pin + "</PINCODE>\r\n";
                    //xmlstc = xmlstc + "<MAILINGNAME.LIST TYPE = 'String'>" + "\r\n";
                    //xmlstc = xmlstc + "<MAILINGNAME>" + name + "</MAILINGNAME>\r\n";
                    //xmlstc = xmlstc + "</MAILINGNAME.LIST>" + "\r\n";
                    //xmlstc = xmlstc + "<ADDRESS.LIST TYPE = 'String'>" + "\r\n";
                    //xmlstc = xmlstc + "<ADDRESS>" + Address + "</ADDRESS>\r\n";
                    //xmlstc = xmlstc + "<ADDRESS>" + Address2 + "</ADDRESS>\r\n";
                    //xmlstc = xmlstc + "<ADDRESS>" + city + "</ADDRESS>\r\n";
                    //xmlstc = xmlstc + "</ADDRESS.LIST>" + "\r\n";
                    xmlstc = xmlstc + "<INCOMETAXNUMBER>" + panCard + "</INCOMETAXNUMBER>\r\n";
                    xmlstc = xmlstc + "<PARENT>" + ledgerGroup + "</PARENT>\r\n";
                    xmlstc = xmlstc + "</LEDGER>\r\n";
                    xmlstc = xmlstc + "</TALLYMESSAGE>\r\n";
                    xmlstc = xmlstc + "</REQUESTDATA>\r\n";
                    xmlstc = xmlstc + "</IMPORTDATA>\r\n";
                    xmlstc = xmlstc + "</BODY>";
                    xmlstc = xmlstc + "</ENVELOPE>";


                  //  HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
                    HttpWebRequest httpWebRequest1 = (HttpWebRequest)WebRequest.Create("http://192.168.0.9:" + "9029");
                    httpWebRequest1.Method = "POST";
                    httpWebRequest1.ContentLength = xmlstc.Length;
                    httpWebRequest1.ContentType = "application/x-www-form-urlencoded";
                    StreamWriter streamWriter1 = new StreamWriter(httpWebRequest1.GetRequestStream());
                    streamWriter1.Write(xmlstc);
                    streamWriter1.Close();

                    HttpWebResponse objResponse1 = (HttpWebResponse)httpWebRequest1.GetResponse();
                    using (StreamReader sr1 = new StreamReader(objResponse1.GetResponseStream()))
                    {
                        result = sr1.ReadToEnd();
                        sr1.Close();
                    }


                    int i = 3824;
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
                    xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Journal" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Accounting Voucher View" + "\" >\r\n";

                    xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                    xmlstc1 = xmlstc1 + "<NARRATION>" + name + "</NARRATION>\r\n";
                    xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>"+contractNo+"</PARTYLEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>Journal</VOUCHERTYPENAME>\r\n";
                    xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                    xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Accounting Voucher View" + "</PERSISTEDVIEW>\r\n";
                    xmlstc1 = xmlstc1 + "<ENTEREDBY>" + "kevin" + "</ENTEREDBY>\r\n";
                    xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                    xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>" + "Yes" + "</ISVATDUTYPAID>\r\n";


                    xmlstc1 = xmlstc1 + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + contractNo + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>-" + totalPurchasePriceTax + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>-" + totalPurchasePriceTax + "</VATEXPAMOUNT>\r\n";

                    xmlstc1 = xmlstc1 + "<BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<NAME>" + contractNo + "</NAME>\r\n";
                    xmlstc1 = xmlstc1 + "<BILLTYPE>" + "New Ref" + "</BILLTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<TDSDEDUCTEEISSPECIALRATE>" + "No" + "</TDSDEDUCTEEISSPECIALRATE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>-" + totalPurchasePriceTax + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</ALLLEDGERENTRIES.LIST>" + "\r\n";


                    xmlstc1 = xmlstc1 + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Output CGST Control A/c" + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + CGST + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + CGST + "</VATEXPAMOUNT>\r\n";

                    xmlstc1 = xmlstc1 + "<BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<NAME>" + contractNo + "</NAME>\r\n";
                    xmlstc1 = xmlstc1 + "<BILLTYPE>" + "New Ref" + "</BILLTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<TDSDEDUCTEEISSPECIALRATE>" + "No" + "</TDSDEDUCTEEISSPECIALRATE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + CGST + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</ALLLEDGERENTRIES.LIST>" + "\r\n";


                    xmlstc1 = xmlstc1 + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Output SGST Control A/c" + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + SGST + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + SGST + "</VATEXPAMOUNT>\r\n";

                    xmlstc1 = xmlstc1 + "<BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<NAME>" + contractNo + "</NAME>\r\n";
                    xmlstc1 = xmlstc1 + "<BILLTYPE>" + "New Ref" + "</BILLTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<TDSDEDUCTEEISSPECIALRATE>" + "No" + "</TDSDEDUCTEEISSPECIALRATE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + SGST + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</ALLLEDGERENTRIES.LIST>" + "\r\n";


                    xmlstc1 = xmlstc1 + "<ALLLEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + ledgerType + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + totalPurchasePrice + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "<VATEXPAMOUNT>" + totalPurchasePrice + "</VATEXPAMOUNT>\r\n";

                    xmlstc1 = xmlstc1 + "<BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<NAME>" + contractNo + "</NAME>\r\n";
                    xmlstc1 = xmlstc1 + "<BILLTYPE>" + "New Ref" + "</BILLTYPE>\r\n";
                    xmlstc1 = xmlstc1 + "<TDSDEDUCTEEISSPECIALRATE>" + "No" + "</TDSDEDUCTEEISSPECIALRATE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + totalPurchasePrice + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</BILLALLOCATIONS.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</ALLLEDGERENTRIES.LIST>" + "\r\n";
                    
                    xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                    xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                    xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                    xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                    xmlstc1 = xmlstc1 + "</BODY>";
                    xmlstc1 = xmlstc1 + "</ENVELOPE>";

                   // HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://localhost:" + "9028");
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("http://192.168.0.9:" + "9029");
                    httpWebRequest.Method = "POST";
                    httpWebRequest.ContentLength = xmlstc1.Length;
                    httpWebRequest.ContentType = "application/x-www-form-urlencoded";
                    StreamWriter streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
                    streamWriter.Write(xmlstc1);
                    streamWriter.Close();

                    HttpWebResponse objResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
                    {
                        result = sr.ReadToEnd();
                        sr.Close();
                    }


                }
                
           
                if (result.Contains("<CREATED>1</CREATED>"))
                {
                    string query1 = "update Tally_Sync_DSR set status='Inactive' where ContractNo='" + contractNo + "' and ID='"+ID+"' ";
                    SqlCommand cmd1 = new SqlCommand(query1, sqlcon1);
                    cmd1.ExecuteNonQuery();
                }

            }
              
            

            Label5.Text = "Updated Successfully!!!";
            Response.AppendHeader("Refresh", "03;url=TallySync_DSR.aspx");

        }

        catch (Exception ex)
        {
            Label6.Text = "Failed :" + ex.Message;
            Response.AppendHeader("Refresh", "10;url=TallySync_DSR.aspx");
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

                string selectQuery = "select * from Tally_Sync_DSR where ContractNo='" + row["ContractNo"] + "'";
                SqlCommand cmd1 = new SqlCommand(selectQuery, sqlcon);
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {

                }else
                {
                    string InsertQuery = "Insert into Tally_Sync_DSR values('" + row["ContractNo"] + "','" + row["Pan_Card"] + "','" + row["pname"] + "','" + row["ContractType"] + "','" + row["Profile_Venue"] + "','" + row["Finance_Details"] + "','" + row["Total_Purchase_Price"] + "','" + row["CGST"] + "','" + row["SGST"] + "','" + row["Total_Price_Including_Tax"] + "','Active','"+ row["Date"] + "')";
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
          Response.AppendHeader("Refresh", "10;url=TallySyncMrktg_Pay_Sales_O_CC_Avenue.aspx");
      }



    
    }

}

