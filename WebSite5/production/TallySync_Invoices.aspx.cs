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

public partial class WebSite5_production_TallySync_Invoices : System.Web.UI.Page
{




    [WebMethod]
    public static string data()
    {
       
        string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
        SqlConnection sqlcon = new SqlConnection(conn);
        string JSON = "{\n \"names\":[";
        string query = "select distinct Type from Invoices_Sync  where  status='Active';";
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
            int j = 0;
            int k = 0;
            int l = 0;
            string log = "";
            string types = Request.Form["type"];
            string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
            SqlConnection sqlcon1 = new SqlConnection(conn);
            sqlcon1.Open(); 

            string selectDataQuery = "select distinct * from Invoices_Sync where status='Active'   and Type ='" + types + "'";
            SqlCommand cmd2 = new SqlCommand(selectDataQuery, sqlcon1);
            SqlDataReader reader1 = cmd2.ExecuteReader();
            while (reader1.Read())
            {

              
                string result = "";
                string parentAcc = "";
                string ledgerName = "";
                string partyName = "";
                
                int ID = reader1.GetInt32(0);
                string Contract_No = reader1.GetString(1);
                string Member_No = reader1.GetString(2);
                string Invoice_No = reader1.GetString(3);
                string Description = reader1.GetString(4);
                DateTime Due_Date = reader1.GetDateTime(5);
                DateTime Invoice_Date = reader1.GetDateTime(6);
                string Debtors = reader1.GetString(7);
                double Collection_Amount = reader1.GetDouble(8);
                double CGST = reader1.GetDouble(9);
                double SGST = reader1.GetDouble(10);
                double Round = reader1.GetDouble(11);
                double Total = reader1.GetDouble(12);
                string Type = reader1.GetString(13);
               

                int dayInt = Invoice_Date.Day;
                string day = Invoice_Date.Day.ToString();
                if (dayInt < 10)
                {
                    day = "0" + day;
                }
                int monthInt = Invoice_Date.Month;
                string month = Invoice_Date.Month.ToString();
                if (monthInt < 10)
                {
                    month = "0" + month;
                }
                int year = Invoice_Date.Year;

                if (Type=="Timeshare")
                {
                    parentAcc = "SUNDRY DEBTORS (GST)";
                    ledgerName = "COLLECTION PTS (GST)";
                    partyName = "SUNDRY DEBTORS GST";

                }
                else
                {
                    parentAcc = "SUNDRY DEBTORS KARMA (GST)";
                    ledgerName = "COLLECTION KARMA (GST)";
                    partyName = "SUNDRY DEBTORS KARMA GST";
                }

                int i = 430;
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
                xmlstc1 = xmlstc1 + "<VOUCHER    VCHTYPE =" + "\"" + "Sales - Goa" + "\"  Action =" + "\"" + "Create" + "\"  OBJVIEW=" + "\"" + "Invoice Voucher View" + "\" >\r\n";


                xmlstc1 = xmlstc1 + "<DATE>" + year + "" + month + "" + day + "</DATE>\r\n";
                xmlstc1 = xmlstc1 + "<NARRATION>" + Invoice_No + " "+ Contract_No+ "</NARRATION>\r\n";
                xmlstc1 = xmlstc1 + "<PARTYNAME>"+partyName+"</PARTYNAME>\r\n";
                xmlstc1 = xmlstc1 + "<PARTYLEDGERNAME>"+parentAcc+"</PARTYLEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<VOUCHERTYPENAME>Sales - Goa</VOUCHERTYPENAME>\r\n";
                xmlstc1 = xmlstc1 + "<REFERENCE>"+Invoice_No+"</REFERENCE>\r\n";
                xmlstc1 = xmlstc1 + "<VOUCHERNUMBER>" + i + "</VOUCHERNUMBER>\r\n";
                xmlstc1 = xmlstc1 + "<BASICBASEPARTYNAME>" + partyName + "</BASICBASEPARTYNAME>\r\n";
                xmlstc1 = xmlstc1 + "<FBTPAYMENTTYPE>" + "Default" + "</FBTPAYMENTTYPE>\r\n";
                xmlstc1 = xmlstc1 + "<PERSISTEDVIEW>" + "Invoice Voucher View" + "</PERSISTEDVIEW>\r\n";
                xmlstc1 = xmlstc1 + "<VOUCHERTYPEORIGNAME>" + "Sales" + "</VOUCHERTYPEORIGNAME>\r\n";
                xmlstc1 = xmlstc1 + "<EFFECTIVEDATE>" + year + "" + month + "" + day + "</EFFECTIVEDATE>\r\n";
                xmlstc1 = xmlstc1 + "<ISINVOICE>Yes</ISINVOICE>\r\n";
                xmlstc1 = xmlstc1 + "<ISVATDUTYPAID>Yes</ISVATDUTYPAID>\r\n";


                xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERNAME>" + parentAcc + "</LEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "Yes" + "</ISDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "Yes" + "</ISPARTYLEDGER>\r\n";
                xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "Yes" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>-" + Total + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "<BILLALLOCATIONS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<NAME>" +Contract_No+"</NAME>\r\n";
                xmlstc1 = xmlstc1 + "<BILLTYPE>" + "New Ref" + "</BILLTYPE>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>-" + Total + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "</BILLALLOCATIONS.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";


                xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERNAME>" + ledgerName + "</LEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>" + Collection_Amount + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Output CGST Liability A/c" + "</LEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>" + CGST + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "Output SGST Liability A/c" + "</LEDGERNAME>\r\n";
                xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                xmlstc1 = xmlstc1 + "<AMOUNT>" + SGST + "</AMOUNT>\r\n";
                xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";

                if (Round > 0)
                {
                    xmlstc1 = xmlstc1 + "<LEDGERENTRIES.LIST>" + "\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERNAME>" + "ROUND OFF" + "</LEDGERNAME>\r\n";
                    xmlstc1 = xmlstc1 + "<ISDEEMEDPOSITIVE>" + "No" + "</ISDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<LEDGERFROMITEM>" + "No" + "</LEDGERFROMITEM>\r\n";
                    xmlstc1 = xmlstc1 + "<REMOVEZEROENTRIES>" + "No" + "</REMOVEZEROENTRIES>\r\n";
                    xmlstc1 = xmlstc1 + "<ISPARTYLEDGER>" + "No" + "</ISPARTYLEDGER>\r\n";
                    xmlstc1 = xmlstc1 + "<ISLASTDEEMEDPOSITIVE>" + "No" + "</ISLASTDEEMEDPOSITIVE>\r\n";
                    xmlstc1 = xmlstc1 + "<AMOUNT>" + Round + "</AMOUNT>\r\n";
                    xmlstc1 = xmlstc1 + "</LEDGERENTRIES.LIST>" + "\r\n";
                   
  
                }

                
                xmlstc1 = xmlstc1 + "</VOUCHER>" + "\r\n";
                xmlstc1 = xmlstc1 + "</TALLYMESSAGE>\r\n";
                xmlstc1 = xmlstc1 + "</REQUESTDATA>\r\n";
                xmlstc1 = xmlstc1 + "</IMPORTDATA>\r\n";
                xmlstc1 = xmlstc1 + "</BODY>";
                xmlstc1 = xmlstc1 + "</ENVELOPE>";



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
                    log += result + "\r\n";
                    sr1.Close();
                        }

                        i++;

                

                if (result.Contains("<CREATED>1</CREATED>"))
                {
                    j++;
                    string query1 = "update Invoices_Sync set status='Inactive' where ID='" + ID + "'";
                    SqlCommand cmd1 = new SqlCommand(query1, sqlcon1);
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


            log += "INVOICES CREATION: CREATED:" + j + " ALTERED:" + k + " ERRORS:" + l + "\r\n";

            string path = @"\\192.168.0.9\C$\Tally_Logs\";
            VerifyDir(path);
            string fileName = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "_Logs.txt";
            System.IO.StreamWriter file = new System.IO.StreamWriter(path + fileName, true);
            file.WriteLine(log);
            file.Close();




            Label5.Text = "Updated Successfully!!!";
            Response.AppendHeader("Refresh", "03;url=TallySync_Invoices.aspx");

        }

        catch (Exception ex)
        {
            Label6.Text = "Failed :" + ex.Message;
            Response.AppendHeader("Refresh", "10;url=TallySync_Invoices.aspx");
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

                string selectQuery = "select * from Invoices_Sync where Invoice_No='" + row["InvoiceNo"] + "' and Total='"+row["TotalAmt"]+"'";
                SqlCommand cmd1 = new SqlCommand(selectQuery, sqlcon);
                SqlDataReader reader = cmd1.ExecuteReader();
                if (reader.HasRows)
                {

                }else
                {
                        string Name =row["Name"].ToString();
                    string InsertQuery = "Insert into Invoices_Sync values('" + row["ContractNo"] + "','" + row["Name"].ToString() + "','"+row["InvoiceNo"] +"','"+row["Description"] +"','"+row["Due Date"] +"','" + row["InvoiceDate"] + "','" + row["Debtors"] + "','" + row["CollectionAmt"] + "','" + row["CGST"] + "','"+row["SGST"] +"','"+row["Round"]+"','"+row["TotalAmt"] +"','"+row["Type"] +"','Active')";
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
          Response.AppendHeader("Refresh", "10;url=TallySync_Invoices.aspx");
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

