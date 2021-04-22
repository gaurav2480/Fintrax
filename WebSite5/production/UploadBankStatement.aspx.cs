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

public partial class WebSite5_production_UploadBankStatement : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {




    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {

        
        string bank = Request.Form["bank"];
        string ID = Request.Form["statementType"];

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
               
                string value = row["Value Date"].ToString();
                string drcr = row["Cr/Dr"].ToString();
                string description = row["Description"].ToString();
                    if (value == "")
                {

                }
                else
                {
                   
                    if (drcr=="DR")
                    {

                    }else
                    {
                        string dateData = value.Replace('-', '/');
                            description = description.Replace(',', ' ');
                            description = description.Replace('&', ' ');

                        DateTime data = DateTime.ParseExact(dateData, "MM/dd/yyyy", null);
                        double date = data.ToOADate();
                        SqlConnection sqlcon = new SqlConnection(conn);
                        sqlcon.Open();

                        string selectQuery = "select * from BANKDEPOSITSLIPDETAILS where CHEQUENO='" + description + "' and AMOUNT='" + row["Transaction Amount(INR)"] + "' and CHEQUEDATE='"+date+"'";
                        SqlCommand cmd1 = new SqlCommand(selectQuery, sqlcon);
                        SqlDataReader reader = cmd1.ExecuteReader();
                        if (reader.HasRows)
                        {

                        }
                        else
                        {
                            string selectCsidQuery = "select top(1) CSID from BANKDEPOSITSLIP order by CSID desc";
                            SqlCommand csidCmd = new SqlCommand(selectCsidQuery, sqlcon);
                            SqlDataReader csidReader = csidCmd.ExecuteReader();
                            while (csidReader.Read())
                            {
                                long csid = csidReader.GetInt64(0);
                                csid = csid + 1;

                                string CHTID = "";
                                if (bank == "5")
                                {
                                    CHTID = "5";
                                }
                                else
                                {
                                    CHTID = "0";

                                }

                                string IDF = "";
                                if (bank == "5")
                                {
                                    IDF = "1";
                                }
                                else
                                {
                                    IDF = "0";

                                }


                                string InsertQuery = "Insert into BANKDEPOSITSLIP values('" + csid + "','" + date + "','" + bank + "','" + CHTID + "')";
                                SqlCommand cmd = new SqlCommand(InsertQuery, sqlcon);
                                cmd.ExecuteNonQuery();


                                string InsertStatementQuery = "Insert into BANKDEPOSITSLIPDETAILS(CSID,LNID,INSTALLMENTNO,CHEQUENO,CHEQUEDATE,MICRCODE,BANKNAME,BRANCH,CITY,AMOUNT,BALANCEAMOUNT,REALISATIONDATE,RETURNDATE,CHSTATID,IDENTIFIEDCHQ,DOCFEEINCLUDED,BULKRECEIPT,OTHERCHARGES,RECEIPTREF,PAIDDATE,LEDID) values('" + csid + "','0','" + description + "','" + description + "','" + date + "','0','','','','" + row["Transaction Amount(INR)"] + "','" + row["Transaction Amount(INR)"] + "','0','0','0','" + IDF + "','0','0','0.00','0','0','" + ID + "')";
                                SqlCommand statementCmd = new SqlCommand(InsertStatementQuery, sqlcon);
                                statementCmd.ExecuteNonQuery();

                                    string selectTridQuery = "select top(1) TRID from LEDGERREGISTER where DRLEDID=5 and CRLEDID=4023 order by LRID desc";
                                    SqlCommand tridCmd = new SqlCommand(selectTridQuery, sqlcon);
                                    SqlDataReader tridReader = tridCmd.ExecuteReader();
                                    while (tridReader.Read())
                                    {
                                        long trid = tridReader.GetInt64(0);
                                        trid = trid + 1;

                                        string InsertLedgerReg = "insert into LEDGERREGISTER(LRDATE,DRLEDID,CRLEDID,LEDAMOUNT,ATID,TRID,NARRATION,LNID,REMARKS,SYNID) values('"+date+"','5','4023','"+ row["Transaction Amount(INR)"] + "','1','"+trid+"','','0','"+ description + "','0')";
                                        SqlCommand ledgerRegCmd = new SqlCommand(InsertLedgerReg, sqlcon);
                                        ledgerRegCmd.ExecuteNonQuery();

                                    }
                                }
                        }

                        reader.Close();
                        sqlcon.Close();
                    }
                   

                }
               

            }

            Label2.Text = "Updated Successfully!!!";
            Response.AppendHeader("Refresh", "04;url=UploadBankStatement.aspx");



        }

        }
        catch (Exception ex)
        {
            Label3.Text = "Failed :" + ex.Message;
            Response.AppendHeader("Refresh", "10;url=UploadBankStatement.aspx");
        }



    }

}

