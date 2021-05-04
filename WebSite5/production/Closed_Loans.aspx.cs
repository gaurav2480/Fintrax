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

using System.IO;

public partial class WebSite5_production_Closed_Loans : System.Web.UI.Page
{




    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

        string fromDate = Request.Form["fromDate"];
        string toDate = Request.Form["toDate"];
        string upToDate = Request.Form["upToDate"];

        string status = Request.Form["disbursmentStatus"];

        DataTable ds = Fintrax.InterestLoss_Closed(fromDate, toDate, status);
        string loanNo = "";
        double loanAmount = 0;
        double loanAmount2 = 0;
        double ROI = 0;
        double EMI = 0;
        double interetVal = 0;
        double principalVal = 0;
        int installment2 = 0;
        int waiver = 0;
        double unadjusted = 0;

        DateTime DATE;

        var len = ds.Rows.Count;

        for (int j = 0; j < len; j++)
        {
            loanAmount = Convert.ToDouble(ds.Rows[j]["LOANBALANCE"].ToString());
            if (loanAmount == 0.00)
            {

            }else
            {
                loanNo = ds.Rows[j]["LOANNO"].ToString();
                DATE = (DateTime)ds.Rows[j]["INSTALLMENTDATE"];
                loanAmount2 = Convert.ToDouble(ds.Rows[j]["LOANBALANCE"].ToString());
                ROI = Convert.ToDouble(ds.Rows[j]["RATEOFINTERST"].ToString());
                EMI = Convert.ToDouble(ds.Rows[j]["EMIVALUE"].ToString());
                installment2 = Convert.ToInt32(ds.Rows[j]["INSTALLMENTSPAID"].ToString());
                waiver = Convert.ToInt32(ds.Rows[j]["NOOFFREEEMI"].ToString());
                //unadjusted = Convert.ToDouble(ds.Rows[j]["UNADJUSTED"].ToString());
                double IntPerMonth = ROI / 1200;
                long instNo = (long)(-1 * (Math.Log(1 - ((IntPerMonth * loanAmount) / EMI)) / Math.Log(1 + IntPerMonth)));
                for (int i = 0; i < instNo + 1; i++)
                {
                    installment2++;

                    if (installment2 <= waiver && waiver != 0)
                    {
                        interetVal = Math.Round(loanAmount * ROI / 1200, 2);

                        principalVal = Math.Round(EMI - interetVal, 2);
                        double interetVal2 = 0;
                        if (interetVal < 0)
                        {

                        }
                        else
                        {
                            DATE = DATE.AddMonths(1);
                            int loan_Debtors = Fintrax.InsertInterestLossClosed(loanNo, loanAmount2, ROI, EMI, interetVal2, principalVal, installment2, DATE);
                            loanAmount = Math.Round(loanAmount - principalVal - interetVal, 2);
                        }


                    }
                    else
                    {
                        interetVal = Math.Round(loanAmount * ROI / 1200, 2);
                        principalVal = Math.Round(EMI - interetVal, 2);
                        if (interetVal < 0)
                        {

                        }
                        else
                        {
                            DATE = DATE.AddMonths(1);
                            int loan_Debtors = Fintrax.InsertInterestLossClosed(loanNo, loanAmount2, ROI, EMI, interetVal, principalVal, installment2, DATE);
                            loanAmount = Math.Round(loanAmount - principalVal, 2);
                        }
                    }



                }
            }

       
        }

        DataTable ds2 = Fintrax.getInterestLoss_Closed(fromDate, upToDate);
        string attachment = "attachment; filename=Interest_Loss_Closed.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = "";
        foreach (DataColumn dc in ds2.Columns)
        {
             Response.Write(tab + dc.ColumnName);
             tab = "\t";
        }
        Response.Write("\n");
        int k;
        foreach (DataRow dr in ds2.Rows)
        {
             tab = "";
             for (k = 0; k< ds2.Columns.Count; k++)
             {
                 Response.Write(tab + dr[k].ToString());
                 tab = "\t";
             }
         Response.Write("\n");
         }
         Response.End();
    }



}