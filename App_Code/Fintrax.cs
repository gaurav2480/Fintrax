using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Globalization;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Web.Services;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;

/// <summary>
/// Summary description for Fintrax
/// </summary>
public class Fintrax
{

    public static SqlConnection GetDBConnection()
    {
        // Get the connection string from the configuration file
        string connectionString = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;

        // Create a new connection object
        SqlConnection connection = new SqlConnection(connectionString);

        // Open the connection, and return it
        connection.Open();
        return connection;
    }


  public static DataSet LoadCybilDetailsOnLoanNo(string loanNo)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {

            SqlCommand SqlCmd = new SqlCommand("SELECT distinct LN.LOANNO, LED.LEDGERNAME ConsumerName,'' DateofBirth,'' Gender,LADD.ADDRESS3 IncomeTaxIDNumber,'' PassportNumber,'' PassportIssueDate,''PassportExpiryDate,'' VoterIDNumber,'' DrivingLicenseNumber,'' DrivingLicenseIssueDate,''DrivingLicenseExpiryDate,''RationCardNumber,''UniversalIDNumber,''AdditionalID1,''AdditionalID2,LADD.MOBILE TelephoneNoMobile,LADD.LANDLINE TelephoneNoResidence,'' TelephoneNoOffice,'' ExtensionOffice,'' TelephoneNoOther ,'' ExtensionOther,LADD.EMAIL EmailID1,'' EmailID2,LADD.ADDRESS1 Address1,'' StateCode1,LADD.PIN PINCode1,'' AddressCategory1,'' ResidenceCode1,'' Address2,'' StateCode2,'' PINCode2,'' AddressCategory2,'' ResidenceCode2,'' CurrentNewMemberCode,'' CurrentNewMemberShortName,LED.LEDCODE CurrNewAccountNo,'' AccountType,'' OwnershipIndicator,(select top(1) REPLACE(CONVERT(VARCHAR(10),CONVERT(DATETIME,lsd.INSTALLMENTDATE,103)-2,103),'','') from LOANSCHEDULE ls join LOANSCHEDULEDETAILS lsd on ls.LSID=lsd.LSID where LNID=LN.LNID and ls.STATUS=0 and lsd.EMISTATUS=1 order by lsd.LOANBALANCE desc) DateOpenedDisbursed,(SELECT TOP 1 REPLACE(CONVERT(VARCHAR(10),CONVERT(DATETIME,BDS.REALISATIONDATE,103)-2,103),'','') FROM BANKDEPOSITSLIPDETAILS BDS WHERE BDS.LNID = LN.LNID ORDER BY BDS.REALISATIONDATE DESC) DateofLastPayment,ISNULL((SELECT TOP 1 REPLACE(CONVERT(VARCHAR(10),CONVERT(DATETIME,LCDDATE,103)-2,103),'','') FROM LOANCLOSUREDETAILS WHERE LNID = LN.LNID),'') DateClosed,REPLACE(CONVERT(VARCHAR(10),CONVERT(DATETIME,GETDATE(),103),103),'','') DateReported,LN.LOANAMOUNT HighCreditSanctionedAmt,(select top(1) lsd.LOANBALANCE from LOANSCHEDULE ls join LOANSCHEDULEDETAILS lsd on ls.LSID=lsd.LSID where LNID=LN.LNID and ls.STATUS=0 and lsd.EMISTATUS=0 order by lsd.LOANBALANCE desc) CurrentBalance,'' AmtOverdue,'' NoofDaysPastDue,'' OldMbrCode,'' OldMbrShortName,'' OldAccNo,'' OldAccType,'' OldOwnershipIndicator,'' SuitFiledWilfulDefault,'' WrittenoffandSettledStatus,'' AssetClassification,'' ValueofCollateral,'' TypeofCollateral,'' CreditLimit,'' CashLimit,LN.RATEOFINTERST RateofInterest,LN.NOOFINSTALLMENTS RepaymentTenure,LN.EMIAMOUNT EMIAmount,'' WrittenoffAmountTotal ,'' WrittenoffPrincipalAmount,'' SettlementAmt,'' PaymentFrequency,(SELECT top(1) BDS.AMOUNT FROM BANKDEPOSITSLIPDETAILS BDS WHERE BDS.LNID = LN.LNID ORDER BY BDS.REALISATIONDATE DESC) ActualPaymentAmt,'' OccupationCode,'' Income,'' NetGrossIncomeIndicator,'' MonthlyAnnualIncomeIndicator,(select top(1) REPLACE(CONVERT(VARCHAR(10),CONVERT(DATETIME,lsd.INSTALLMENTDATE,103)-2,103),'','') from LOANSCHEDULE ls join LOANSCHEDULEDETAILS lsd on ls.LSID=lsd.LSID where LNID=LN.LNID and ls.STATUS=0 and lsd.EMISTATUS=1 order by lsd.LOANBALANCE desc) RegisteredDate,(select LOANSTATUS from LOANSTATUS where LNSID=LN.ACTIVE) LoanStatus,LADD.[STATE] FROM LOANS LN,LEDGER LED,LEDGERADDRESS LADD WHERE LN.ACTIVE IN (2,0,1,3) AND LN.LEDID = LED.LEDID AND LED.LEDID = LADD.LEDID AND LN.DISBURSEMENTSTATUS =1 And LN.LOANNO=@loanNo", cs1);
            SqlCmd.Parameters.Add("@loanNo", SqlDbType.VarChar).Value = loanNo;
            da = new SqlDataAdapter(SqlCmd);
            ds = new DataSet();
            da.Fill(ds);
        }
        return (ds);
    }
    public static DataSet overdue(string loanNo)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {

            SqlCommand SqlCmd = new SqlCommand("SELECT sum(lsd.EMIVALUE) EMIAMOUNT FROM LOANS LN ,LOANSCHEDULE LS LEFT OUTER JOIN LOANSCHEDULEDETAILS LSD ON LS.LSID = LSD.LSID,LOANTYPE LT,LOANSTATUS LST WHERE LN.LOANNO =@loanNo AND LS.LNID = LN.LNID AND LT.LTID = LN.LTID AND LST.LNSID = LN.ACTIVE AND LS.STATUS = 0 AND LSD.INSTALLMENTDATE <= GETDATE() AND LSD.INSTALLMENTNO > LN.INSTALLMENTSPAID", cs1);
            SqlCmd.Parameters.Add("@loanNo", SqlDbType.VarChar).Value = loanNo;
            da = new SqlDataAdapter(SqlCmd);
            ds = new DataSet();
            da.Fill(ds);
        }
        return (ds);
    }

    public static DataSet LoadCybilDetailsOnLoanNofromCybil(string loanNo)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {

            SqlCommand SqlCmd = new SqlCommand("select distinct * from Cybil where loanNo=@loanNo", cs1);
            SqlCmd.Parameters.Add("@loanNo", SqlDbType.VarChar).Value = loanNo;
            da = new SqlDataAdapter(SqlCmd);
            ds = new DataSet();
            da.Fill(ds);
        }
        return (ds);
    }

    public static DataSet LoadGender(string genderCode)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select Code,[Values] from Gender where Code not in ('" + genderCode+"') ", cs1);
            SqlCommand SqlCmd = new SqlCommand("select Code,[Values],'1' orders from Gender where Code in('"+ genderCode + "') union select Code,[Values],'' orders from Gender where Code not in ('"+ genderCode + "') order by orders desc ", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }


    public static DataSet LoadGender1(string genderCode)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select Code,[Values] from Gender where Code not in ('" + genderCode+"') ", cs1);
            SqlCommand SqlCmd = new SqlCommand("select Code,[Values],'1' orders from Gender where Code in('" + genderCode + "') union select Code,[Values],'' orders from Gender where Code not in ('" + genderCode + "') union select '','','' order by orders desc ", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadStateCode(string stateCode)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //   SqlCommand SqlCmd = new SqlCommand("select Code,[Values]  from State_code where code not in ('"+ stateCode + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select Code,[Values],'1' orders ,[AlpValues] from State_code where code in ('" + stateCode + "') union select Code,[Values],'' orders,[AlpValues]  from State_code where code not in ('" + stateCode + "') order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadStateCode1(string stateCode)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //   SqlCommand SqlCmd = new SqlCommand("select Code,[Values]  from State_code where code not in ('"+ stateCode + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select Code,[Values],'1' orders  from State_code where code in ('" + stateCode + "') union select Code,[Values],'' orders  from State_code where code not in ('" + stateCode + "') union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadAddressCategory(string addressCategory)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
          //  SqlCommand SqlCmd = new SqlCommand("select addCode,addValues from Address_category where addCode not in ('"+ addressCategory + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select addCode,addValues,'' orders from Address_category where addCode in  ('" + addressCategory + "') union select addCode,addValues, orders from Address_category where addCode not in ('" + addressCategory + "') order by orders asc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadAddressCategory1(string addressCategory)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select addCode,addValues from Address_category where addCode not in ('"+ addressCategory + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select addCode,addValues,'1' orders from Address_category where addCode in  ('" + addressCategory + "') union select addCode,addValues,'' orders from Address_category where addCode not in ('" + addressCategory + "') union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadResidenceCode(string residenceCode)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select resCode,resValues from residence_code where resCode not in('"+residenceCode+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select resCode,resValues,'' orders from residence_code where resCode in('" + residenceCode + "') union select resCode,resValues, orders from residence_code where resCode not in('" + residenceCode + "')order by orders asc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadResidenceCode1(string residenceCode)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select resCode,resValues from residence_code where resCode not in('"+residenceCode+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select resCode,resValues,'1' orders from residence_code where resCode in('" + residenceCode + "') union select resCode,resValues,'' orders from residence_code where resCode not in('" + residenceCode + "') union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadAccountType(string accountType)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
           // SqlCommand SqlCmd = new SqlCommand("select ACode,AValues from Acct_type_code where ACode not in('"+accountType+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select ACode,AValues,'' orders from Acct_type_code where ACode  in('" + accountType + "') union select ACode,AValues, orders  from Acct_type_code where ACode not in('" + accountType + "') order by orders asc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadAccountType1(string accountType)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            // SqlCommand SqlCmd = new SqlCommand("select ACode,AValues from Acct_type_code where ACode not in('"+accountType+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select ACode,AValues,'1' orders from Acct_type_code where ACode  in('" + accountType + "') union select ACode,AValues,'' orders  from Acct_type_code where ACode not in('" + accountType + "') union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadOwnershipIndicator(string ownershipIndicator)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
           // SqlCommand SqlCmd = new SqlCommand("select OCode, OValues from Ownership_code where OCode not in('"+ownershipIndicator+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select OCode, OValues,'' orders from Ownership_code where OCode in('" + ownershipIndicator + "') union select OCode, OValues, orders from Ownership_code where OCode not in('" + ownershipIndicator + "')order by orders asc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadOwnershipIndicator1(string ownershipIndicator)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            // SqlCommand SqlCmd = new SqlCommand("select OCode, OValues from Ownership_code where OCode not in('"+ownershipIndicator+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select OCode, OValues,'1' orders from Ownership_code where OCode in('" + ownershipIndicator + "') union select OCode, OValues,'' orders from Ownership_code where OCode not in('" + ownershipIndicator + "') union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadWilful()
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
           // SqlCommand SqlCmd = new SqlCommand("select WLCode,WLValues from Wilful_default_status where WLCode not in('" + wilful + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select '' WLCode ,'' WLValues,'4' as orders union select WLCode,WLValues,orders from Wilful_default_status order by orders asc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadWilful1(string wilful)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            // SqlCommand SqlCmd = new SqlCommand("select WLCode,WLValues from Wilful_default_status where WLCode not in('" + wilful + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select WLCode,WLValues,'1' orders  from Wilful_default_status where WLCode  in('" + wilful + "') union select WLCode,WLValues,'' orders from Wilful_default_status where WLCode not in('" + wilful + "')union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadWrittenOffSettled(string writtenOffSettled)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select WCode,WValues from written_off_settled_status where WCode not in ('"+ writtenOffSettled + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select WCode,WValues,'' orders from written_off_settled_status where WCode in ('" + writtenOffSettled + "') union select WCode,WValues, orders  from written_off_settled_status where WCode not in ('" + writtenOffSettled + "')order by orders asc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    public static DataSet LoadWrittenOffSettledcancelled(string writtenOffSettled)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select WCode,WValues from written_off_settled_status where WCode not in ('"+ writtenOffSettled + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select WCode,WValues,'' orders from written_off_settled_status where WCode in ('" + writtenOffSettled + "') union select WCode,WValues, orders  from written_off_settled_status where WCode not in ('" + writtenOffSettled + "')order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadWrittenOffSettled1(string writtenOffSettled)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select WCode,WValues from written_off_settled_status where WCode not in ('"+ writtenOffSettled + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select WCode,WValues,'1' orders from written_off_settled_status where WCode in ('" + writtenOffSettled + "') union select WCode,WValues,'' orders  from written_off_settled_status where WCode not in ('" + writtenOffSettled + "')union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadAssetClassification()
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            // SqlCommand SqlCmd = new SqlCommand("select AssCode,AssValues from asset_classification where AssCode not in('"+ assetClassification + "')", cs1);
            // SqlCommand SqlCmd = new SqlCommand("select AssCode,AssValues,'1' orders from asset_classification where AssCode in ('" + assetClassification + "') union select AssCode,AssValues,'' orders  from asset_classification where AssCode not in('" + assetClassification + "') order by orders desc", cs1);
            SqlCommand SqlCmd = new SqlCommand("select '' AssCode ,'' AssValues,'6' as orders union select AssCode,AssValues,orders  from asset_classification order by orders asc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadAssetClassification1(string assetClassification)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            // SqlCommand SqlCmd = new SqlCommand("select AssCode,AssValues from asset_classification where AssCode not in('"+ assetClassification + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select AssCode,AssValues,'1' orders from asset_classification where AssCode in ('" + assetClassification + "') union select AssCode,AssValues,'' orders  from asset_classification where AssCode not in('" + assetClassification + "') union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadTypeOfCollateral()
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select coCode,coValues from collateral where coCode not in ('"+ typeCollateral + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select '' coCode ,'' coValues,'6' as orders union select coCode,coValues,orders from collateral order by orders asc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadTypeOfCollateral1(string typeCollateral)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select coCode,coValues from collateral where coCode not in ('"+ typeCollateral + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select coCode,coValues,'1' orders from collateral where coCode in ('" + typeCollateral + "') union select coCode,coValues,'' orders from collateral where coCode not in ('" + typeCollateral + "') union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadPaymentFreq(string paymentFreq)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            // SqlCommand SqlCmd = new SqlCommand("select payCode,payValues from payment_freq where payCode not in ('"+paymentFreq+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand(" select payCode,payValues,'' orders  from payment_freq where payCode in ('" + paymentFreq + "') union select payCode,payValues, orders from payment_freq where payCode not in ('" + paymentFreq + "')  order by orders asc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadPaymentFreq1(string paymentFreq)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            // SqlCommand SqlCmd = new SqlCommand("select payCode,payValues from payment_freq where payCode not in ('"+paymentFreq+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select payCode,payValues,'1' orders  from payment_freq where payCode in ('" + paymentFreq + "') union select payCode,payValues,'' orders from payment_freq where payCode not in ('" + paymentFreq + "')  union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadOccupationCode(string occupationCode)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            // SqlCommand SqlCmd = new SqlCommand("select occCode,occValues from Occp_date where occCode not in ('"+occupationCode+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select occCode,occValues,'' orders from Occp_date where occCode in ('" + occupationCode + "') union select occCode,occValues, orders from Occp_date where occCode not in ('" + occupationCode + "')order by orders asc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadOccupationCode1(string occupationCode)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            // SqlCommand SqlCmd = new SqlCommand("select occCode,occValues from Occp_date where occCode not in ('"+occupationCode+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select occCode,occValues,'1' orders from Occp_date where occCode in ('" + occupationCode + "') union select occCode,occValues,'' orders from Occp_date where occCode not in ('" + occupationCode + "') union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }

    //
    public static DataSet LoadNetGrossIncomeIndicator(string netGrossIncome)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select grCode, grValues from gross_income_indicator where grCode not in('"+ netGrossIncome + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select grCode, grValues,'1' orders from gross_income_indicator where grCode in('" + netGrossIncome + "') union select grCode, grValues,'' orders from gross_income_indicator where grCode not in('" + netGrossIncome + "')  order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadNetGrossIncomeIndicator1(string netGrossIncome)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select grCode, grValues from gross_income_indicator where grCode not in('"+ netGrossIncome + "')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select grCode, grValues,'1' orders from gross_income_indicator where grCode in('" + netGrossIncome + "') union select grCode, grValues,'0' orders from gross_income_indicator where grCode not in('" + netGrossIncome + "') union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }


    //
    public static DataSet LoadMonthlyAnnualIncome(string monthlyAnnualIncome)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select anCode,anValues from Annual_income_indicator where anCode not in ('"+monthlyAnnualIncome+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select anCode,anValues,'1' orders from Annual_income_indicator where anCode  in ('" + monthlyAnnualIncome + "') union select anCode,anValues,'' orders from Annual_income_indicator where anCode not in ('" + monthlyAnnualIncome + "') order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }
    //
    public static DataSet LoadMonthlyAnnualIncome1(string monthlyAnnualIncome)
    {
        SqlDataAdapter da;
        DataSet dt = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            //  SqlCommand SqlCmd = new SqlCommand("select anCode,anValues from Annual_income_indicator where anCode not in ('"+monthlyAnnualIncome+"')", cs1);
            SqlCommand SqlCmd = new SqlCommand("select anCode,anValues,'1' orders from Annual_income_indicator where anCode  in ('" + monthlyAnnualIncome + "') union select anCode,anValues,'0' orders from Annual_income_indicator where anCode not in ('" + monthlyAnnualIncome + "') union select '','','' order by orders desc", cs1);
            //SqlCmd.Parameters.Add("@office", SqlDbType.VarChar).Value = office;
            da = new SqlDataAdapter(SqlCmd);
            dt = new DataSet();
            da.Fill(dt);
        }
        return (dt);

    }



    public static int InsertCybil(string customerName,string dateOfBirth,string gender,string incometaxIDNumber,string passportNumber,string passportIssueDate,string passportExpiryDate,string voterIDNumber,string drivingLicenseNumber,string drivingLicenseIssueDate,string drivinglicenseExpiryDate,string rationCardNumber ,string universalIDNumber ,string additionalID1,string additionalID2,string telephoneNumberMobile,string telephoneNumberResidence,string telephoneNumberOffice,string extensionOffice,string telephoneNumberOther,string extensionOther,string email1,string email2,string address1,string state1,string pincode1,string category1,string residence1,string address2,string state2,string pincode2,string category2,string residence2,string currentNewMemberCode,string currentNewMemberName,string currentNewAccountNumber,string accountType,string ownershipIndicator,string dateOpenedDisbursed,string dateOfLastPayment,string dateClosed,string dateReported,string highCreditSanctionedAmount,string currentBalance,string overdueAmount,string numberOfDaysPastDue,string oldMemberCode,string oldMemberName,string oldAccountNumber,string oldAccountType,string oldOwnershipIndicator,string suitFiledWilfulDefault,string writtenOffSettled,string assetClassification,string valueCollateral,string collateralType,string creditLimit,string cashLimit,string rateOfInterest,string repaymentTenure,string emiAmount,string writtenOffTotalAmount,string writtenOffPrincipalAmount,string settlementAmount,string paymentFrequency,string actualPaymentAmount,string occupationCode,string income,string netGrossIncome,string monthlyAnnualIncome,string loanNo,string registeredDate)
    {

        int rowsAffected = 0;
        SqlDataAdapter da = new SqlDataAdapter();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            try
            {
                da.InsertCommand = new SqlCommand("insert into Cybil values ('"+customerName+"','"+dateOfBirth+"','"+gender+"','"+incometaxIDNumber+"','"+passportNumber+"','"+passportIssueDate+"','"+passportExpiryDate+"','"+voterIDNumber+"','"+drivingLicenseNumber+"','"+drivingLicenseIssueDate+"','"+drivinglicenseExpiryDate+"','"+rationCardNumber+"','"+universalIDNumber+"','"+additionalID1+"','"+additionalID2+"','"+telephoneNumberMobile+"','"+telephoneNumberResidence+"','"+telephoneNumberOffice+"','"+extensionOffice+"','"+telephoneNumberOther+"','"+extensionOther+"','"+email1+"','"+email2+"','"+address1+"','"+state1+"','"+pincode1+"','"+category1+"','"+residence1+"','"+address2+"','"+state2+"','"+pincode2+"','"+category2+"','"+residence2+"','"+currentNewMemberCode+"','"+currentNewMemberName+"','"+currentNewAccountNumber+"','"+accountType+"','"+ownershipIndicator+"','"+dateOpenedDisbursed+"','"+dateOfLastPayment+"','"+dateClosed+"','"+dateReported+"','"+highCreditSanctionedAmount+"','"+currentBalance+"','"+overdueAmount+"','"+numberOfDaysPastDue+"','"+oldMemberCode+"','"+oldMemberName+"','"+oldAccountNumber+"','"+oldAccountType+"','"+oldOwnershipIndicator+"','"+suitFiledWilfulDefault+"','"+writtenOffSettled+"','"+assetClassification+"','"+valueCollateral+"','"+collateralType+"','"+creditLimit+"','"+cashLimit+"','"+rateOfInterest+"','"+repaymentTenure+"','"+emiAmount+"','"+writtenOffTotalAmount+"','"+writtenOffPrincipalAmount+"','"+settlementAmount+"','"+paymentFrequency+"','"+actualPaymentAmount+"','"+occupationCode+"','"+income+"','"+netGrossIncome+"','"+monthlyAnnualIncome+"','"+loanNo+"','"+registeredDate+"','00')", cs1);
             
                rowsAffected = da.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                //MessageBox.Show("Error in Insert Profile Query " + ex.Message);

                string msg = "Error in Insert Cybil Query " + " " + ex.Message;

                throw new Exception(msg, ex);

                // HttpContext.Current.Response.Redirect(HttpContext.Current.Request.RawUrl);
            }
        }
        return (rowsAffected);

    }


    public static int checkLoanNumber(string LoanNo)
    {

        int startval;

        int finaiInstId = 0;
      
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {

          
                SqlCommand scmd = new SqlCommand("select count(*) from Cybil where loanNo=@LoanNo", cs1);
                scmd.Parameters.Add("@LoanNo", SqlDbType.VarChar).Value = LoanNo;
                startval = (int)scmd.ExecuteScalar();
                finaiInstId = startval;
           
        }

        return finaiInstId;
        
    }


    public static int UpdateCbyil(string customerName, string dateOfBirth, string gender, string incometaxIDNumber, string passportNumber, string passportIssueDate, string passportExpiryDate, string voterIDNumber, string drivingLicenseNumber, string drivingLicenseIssueDate, string drivinglicenseExpiryDate, string rationCardNumber, string universalIDNumber, string additionalID1, string additionalID2, string telephoneNumberMobile, string telephoneNumberResidence, string telephoneNumberOffice, string extensionOffice, string telephoneNumberOther, string extensionOther, string email1, string email2, string address1, string state1, string pincode1, string category1, string residence1, string address2, string state2, string pincode2, string category2, string residence2, string currentNewMemberCode, string currentNewMemberName, string currentNewAccountNumber, string accountType, string ownershipIndicator, string dateOpenedDisbursed, string dateOfLastPayment, string dateClosed, string dateReported, string highCreditSanctionedAmount, string currentBalance, string overdueAmount, string numberOfDaysPastDue, string oldMemberCode, string oldMemberName, string oldAccountNumber, string oldAccountType, string oldOwnershipIndicator, string suitFiledWilfulDefault, string writtenOffSettled, string assetClassification, string valueCollateral, string collateralType, string creditLimit, string cashLimit, string rateOfInterest, string repaymentTenure, string emiAmount, string writtenOffTotalAmount, string writtenOffPrincipalAmount, string settlementAmount, string paymentFrequency, string actualPaymentAmount, string occupationCode, string income, string netGrossIncome, string monthlyAnnualIncome, string loanNo,string registeredDate)
    {
        int rowsaffected = 0;
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            try
            {

                SqlCommand scd = new SqlCommand("update cybil set [Consumer Name]='"+customerName+"' ,[Date of Birth]='"+dateOfBirth+"',Gender='"+gender+"',[Income Tax ID Number]='"+incometaxIDNumber+"',[Passport Number]='"+passportNumber+"',[Passport Issue Date]='"+passportIssueDate+"',[Passport Expiry Date]='"+passportExpiryDate+"',[Voter ID Number]='"+voterIDNumber+"',[Driving License Number]='"+drivingLicenseNumber+"',[Driving License IssueDate]='"+drivingLicenseIssueDate+"',[Driving License ExpiryDate]='"+drivinglicenseExpiryDate+"',[Ration Card Number]='"+rationCardNumber+"',[Universal ID Number]='"+universalIDNumber+"',[Additional ID #1]='"+additionalID1+"',[Additional ID #2]='"+additionalID2+"',[Telephone No#Mobile]='"+telephoneNumberMobile+"',[Telephone No#Residence]='"+telephoneNumberResidence+"',[Telephone No#Office]='"+telephoneNumberOffice+"',[Extension Office]='"+extensionOffice+"',[Telephone No#Other]='"+telephoneNumberOther+"',ExtensionOther='"+extensionOther+"',[Email ID 1]='"+email1+"',[Email ID 2]='"+email2+"',[Address 1]='"+address1+"',[State Code 1]='"+state1+"',[PIN Code 1]='"+pincode1+"',[Address Category 1]='"+category1+"',[Residence Code 1]='"+residence1+"',[Address 2]='"+address2+"',[State Code 2]='"+state2+"',[PIN Code 2]='"+pincode2+"',[Address Category 2]='"+category2+"',[Residence Code 2]='"+residence2+"',[Current/New Member Code]='"+currentNewMemberCode+"',[Current/New Member Short Name]='"+currentNewMemberName+"',[Curr/New Account No]='"+currentNewAccountNumber+"',[Account Type]='"+accountType+"',[Ownership Indicator]='"+ownershipIndicator+"',[Date Opened/Disbursed]='"+dateOpenedDisbursed+"',[Date of Last Payment]='"+dateOfLastPayment+"',[Date Closed]='"+dateClosed+"',[Date Reported]='"+dateReported+"',[High Credit/Sanctioned Amt]='"+highCreditSanctionedAmount+"',[Current  Balance]='"+currentBalance+"',[Amt Overdue]='"+overdueAmount+"',[No of Days Past Due]='"+numberOfDaysPastDue+"',[Old Mbr Code]='"+oldMemberCode+"',[Old Mbr Short Name]='"+oldMemberName+"',[Old Acc No]='"+oldAccountNumber+"',[Old Acc Type]='"+accountType+"',[Old Ownership Indicator]='"+oldOwnershipIndicator+"',[Suit Filed / Wilful Default]='"+suitFiledWilfulDefault+"',[Written-off and Settled Status]='"+writtenOffSettled+"',[Asset Classification]='"+assetClassification+"',[Value of Collateral]='"+valueCollateral+"',[Type of Collateral]='"+collateralType+"',[Credit Limit]='"+creditLimit+"',[Cash Limit]='"+cashLimit+"',[Rate of Interest]='"+rateOfInterest+"',RepaymentTenure='"+repaymentTenure+"',[EMI Amount]='"+emiAmount+"',[Written- off Amount (Total)]='"+writtenOffTotalAmount+"',[Written- off Principal Amount]='"+writtenOffPrincipalAmount+"',[Settlement Amt]='"+settlementAmount+"',[Payment Frequency]='"+paymentFrequency+"',[Actual Payment Amt]='"+actualPaymentAmount+"',[Occupation Code]='"+occupationCode+"',Income='"+income+"',[Net/Gross Income Indicator]='"+netGrossIncome+"',[Monthly/Annual Income Indicator]='"+monthlyAnnualIncome+"',RegisteredDate='"+registeredDate+"' where LoanNo='"+loanNo+"'", cs1);
                scd.Parameters.Add("@loanNo", SqlDbType.VarChar).Value = loanNo;
                rowsaffected = scd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = "Error in UPDATE CYBIL Query " + " " + ex.Message;

                throw new Exception(msg, ex);
                
            }
        }
        return (rowsaffected);
    }


 

    public static string getCodeOnState(string state)
    {
        string val;
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            SqlCommand SqlCmd = new SqlCommand("select Code from State_code where [Values]='" + state + "'", cs1);
            SqlCmd.Parameters.Add("@state", SqlDbType.VarChar).Value = state;
            val = (string)SqlCmd.ExecuteScalar();

        }
        return val;
    }


    public static DataTable getCybil(string date, string date2)
    {

        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            SqlCommand cmd_sp = new SqlCommand("ExtractCybil", cs1);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@DATE", date);
            cmd_sp.Parameters.AddWithValue("@DATE2", date2);
         
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataTable datatable = new DataTable();
            da.Fill(datatable);
            return datatable;


        }


    }


    public static DataTable Report(string loanStatus,string disbursmentStatus, string count)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("sp_InsertFutureProjectedInterestDetails2", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@LOANSTATUS", loanStatus);
            cmd_sp.Parameters.AddWithValue("@DISBURSEMENT", disbursmentStatus);
            cmd_sp.Parameters.AddWithValue("@count", count);
	    
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataTable datatable = new DataTable();
            da.Fill(datatable);
            return datatable;

        }

    }


    public static DataSet AdvanceEmi(string date)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("AdvanceEmi", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@date", date);
         
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }


    public static DataSet LastPaidRowForClosedLoans(string fromDate,string toDate,string statusText,string statusValue)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            SqlCommand cmd_sp;
            if (statusText=="Closed")
            {
                 cmd_sp = new SqlCommand("LastPaidRowOfClosedLoans", con);
            }
            else
            {
                 cmd_sp = new SqlCommand("AdditionalPrinciplePaid", con);

            }
            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@fromDate", fromDate);
            cmd_sp.Parameters.AddWithValue("@toDate", toDate);
            cmd_sp.Parameters.AddWithValue("@statusText", statusText);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

  public static DataSet OverdueHistorical(string date,string disbursmentStatus)
    {


        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            SqlCommand cmd_sp;
            if (disbursmentStatus=="1")
            {
                 cmd_sp = new SqlCommand("Overdue_Historical", con);
            }
            else
            {
                cmd_sp = new SqlCommand("Overdue_Historical_Not_Disbursed", con);
            }
           
            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@date", date);
         

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

 public static DataSet InterestWaiver(string fromDate, string toDate)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("InterestWaiver", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@fromDate", fromDate);
            cmd_sp.Parameters.AddWithValue("@toDate", toDate);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }


    public static int InsertInterestDifference(string fromDate, string toDate)
    {

        int rowsAffected = 0;
        SqlDataAdapter da = new SqlDataAdapter();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            try
            {
                da.InsertCommand = new SqlCommand("Insert into demo1 (LoanNumber,InstallmentDate,InterestValue) SELECT LN.LOANNO,lsd.INSTALLMENTDATE,lsd.INTERSTVALUE FROM LOANSCHEDULEDETAILS LSD left JOIN LOANSCHEDULE LS ON LSD.LSID=LS.LSID left JOIN LOANS LN ON LS.LNID=LN.LNID WHERE LS.LSID in((select LSID from LOANSCHEDULE where Status=0 and LNID=Ln.LNID)) AND LN.ACTIVE IN (0) and CONVERT(DATETIME,LSD.INSTALLMENTDATE-2,103)>=CONVERT(datetime,'" + fromDate + "',120) and CONVERT(DATETIME,LSD.INSTALLMENTDATE-2,103)<=CONVERT(datetime,'" + toDate + "',120) and Ln.LNID=Ln.LNID and ln.DISBURSEMENTSTATUS=1 group by LN.LOANNO,lsd.INSTALLMENTDATE,lsd.INTERSTVALUE,lsd.PRINCIPALVALUE,LS.STATUS ORDER BY LN.LOANNO,INSTALLMENTDATE Insert into demo2 (LoanNumber,InstallmentDate,InterestValue) SELECT distinct LN.LOANNO,lsd.INSTALLMENTDATE,lsd.INTERSTVALUE FROM LOANSCHEDULEDETAILS LSD left JOIN LOANSCHEDULE LS ON LSD.LSID=LS.LSID left JOIN LOANS LN ON LS.LNID=LN.LNID WHERE LS.LSID in(select Top(1) Lsid from LOANSCHEDULE where LNID=ln.LNID and Status='1'  order by SCHEDULENO desc) AND LN.ACTIVE IN (0) and CONVERT(DATETIME,LSD.INSTALLMENTDATE-2,103)>=CONVERT(datetime,'" + fromDate + "',120) and CONVERT(DATETIME,LSD.INSTALLMENTDATE-2,103)<=CONVERT(datetime,'" + toDate + "',120) and Ln.LNID=Ln.LNID and ln.DISBURSEMENTSTATUS=1 group by LN.LOANNO,lsd.INSTALLMENTDATE,lsd.INTERSTVALUE,lsd.PRINCIPALVALUE ,LS.STATUS ORDER BY ln.LOANNO,INSTALLMENTDATE", cs1);

                rowsAffected = da.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                
                string msg = "Error" + " " + ex.Message;

                throw new Exception(msg, ex);
                
            }
        }
        return (rowsAffected);

    }

public static DataTable fintrax_max(string date, string date1)
{

using (SqlConnection cs1 = Fintrax.GetDBConnection())
{

SqlCommand cmd_sp = new SqlCommand("spfintrax", cs1);
// cmd.CommandText = "holiday_confirm";
cmd_sp.CommandType = CommandType.StoredProcedure;
cmd_sp.Parameters.AddWithValue("@LOGDATE", date);
cmd_sp.Parameters.AddWithValue("@LOGDATE1", date1);


SqlDataAdapter da = new SqlDataAdapter();
da.SelectCommand = cmd_sp;
DataTable datatable = new DataTable();
da.Fill(datatable);
return datatable;

}

}

public static DataTable fintrax_min(string date, string date1)
{

using (SqlConnection cs1 = Fintrax.GetDBConnection())
{

SqlCommand cmd_sp = new SqlCommand("spfintrax_min", cs1);
// cmd.CommandText = "holiday_confirm";
cmd_sp.CommandType = CommandType.StoredProcedure;
cmd_sp.Parameters.AddWithValue("@LOGDATE", date);
cmd_sp.Parameters.AddWithValue("@LOGDATE1", date1);


SqlDataAdapter da = new SqlDataAdapter();
da.SelectCommand = cmd_sp;
DataTable datatable = new DataTable();
da.Fill(datatable);
return datatable;

}

}
 public static DataSet ProjectedInterestDetail(string fromDate, string toDate, string statusValue, string disStatus,string overdueExCount)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            SqlCommand cmd_sp;
            cmd_sp = new SqlCommand("ProjectedInterestDetailsReport", con);
            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@fromDate", fromDate);
            cmd_sp.Parameters.AddWithValue("@toDate", toDate);
            cmd_sp.Parameters.AddWithValue("@status", statusValue);
            cmd_sp.Parameters.AddWithValue("@disbursement", disStatus);
  	    cmd_sp.Parameters.AddWithValue("@overdueCount", overdueExCount);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

   public static DataTable Debtors(string Date,string Status)
    {
        SqlCommand cmd_sp;
        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            if (Status=="1")
            {
                cmd_sp = new SqlCommand("DEBTORS", con);
            }
            else
            {
                cmd_sp = new SqlCommand("DEBTORSOPEN", con);
            }
            
            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@Date", Date);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataTable datatable = new DataTable();
            da.Fill(datatable);
            return datatable;

        }

    }
    public static DataTable getDebtorsDetails(string Status)
    {
        SqlCommand cmd;
        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            if (Status == "1")
            {
                cmd = new SqlCommand("select LOANNO,CONTRACTNO ,ACTUALAMOUNT, sum(INTEREST) INTEREST,UNADJUSTED from Loan_Debtors_Details group by LOANNO, ACTUALAMOUNT,UNADJUSTED,CONTRACTNO", con);
            }
            else
            {
                cmd = new SqlCommand("select LOANNO, CONTRACTNO,ACTUALAMOUNT, sum(INTEREST) INTEREST,UNADJUSTED from Loan_Debtors_Details_Open group by LOANNO, ACTUALAMOUNT,UNADJUSTED,CONTRACTNO", con);
            }
           
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            sda.Fill(ds);
            return ds;

        }
      
    }


     public static int InsertScheduleDetails(string loanNo,double loanAmount,double ROI,double EMI,double interest,double principal,int installmentNo,double unadjusted,string Status,string contractNO)
    {

        int rowsAffected = 0;
        SqlDataAdapter da = new SqlDataAdapter();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            try
            {
                if (Status=="1")
                {
                    da.InsertCommand = new SqlCommand("insert into [Loan_Debtors_Details] values('" + loanNo + "','" + loanAmount + "','" + EMI + "','" + interest + "','" + principal + "','" + installmentNo + "','" + unadjusted + "','"+contractNO+"')", cs1);
                }
                else
                {
                    da.InsertCommand = new SqlCommand("insert into [Loan_Debtors_Details_Open] values('" + loanNo + "','" + loanAmount + "','" + EMI + "','" + interest + "','" + principal + "','" + installmentNo + "','" + unadjusted + "','" + contractNO + "')", cs1);
                }
                

                rowsAffected = da.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = "Error in Insert Schedule Query " + " " + ex.Message;

                throw new Exception(msg, ex);

            }
        }
        return (rowsAffected);

    }
  public static SqlDataReader LoadLoanDetailsOnCollector(string Name)
    {
        string firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
        string lastdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
        string lastdayofpreviousmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(0).AddDays(-1).ToString("yyyy-MM-dd");

        SqlConnection cs1 = Fintrax.GetDBConnection();
        SqlCommand SqlCmd = new SqlCommand("LOANDETAILSONCOLLECTOR", cs1);
        SqlCmd.CommandType = CommandType.StoredProcedure;
        SqlCmd.Parameters.AddWithValue("@Name", Name);
        SqlCmd.Parameters.AddWithValue("@StartDate", firstdayofmonth);
        SqlCmd.Parameters.AddWithValue("@EndDate", lastdayofmonth);
        SqlCmd.Parameters.AddWithValue("@EndDatePreviousMonth", lastdayofpreviousmonth);
        SqlDataReader dr = SqlCmd.ExecuteReader(CommandBehavior.CloseConnection);

        return dr;

    }
    public static DataTable LoadLoanDetailsOnCollector1(string Name)
    {
        string firstdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("yyyy-MM-dd");
        string lastdayofmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
        string lastdayofpreviousmonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(0).AddDays(-1).ToString("yyyy-MM-dd");

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("LOANDETAILSONCOLLECTOR", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@Name", Name);
            cmd_sp.Parameters.AddWithValue("@StartDate", firstdayofmonth);
            cmd_sp.Parameters.AddWithValue("@EndDate", lastdayofmonth);
            cmd_sp.Parameters.AddWithValue("@EndDatePreviousMonth", lastdayofpreviousmonth);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataTable datatable = new DataTable();
            da.Fill(datatable);
            return datatable;

        }

    }

 public static DataSet Cancelled_Loans(string Startdate,string Enddate,string UPTODATE,string disbursmentStatus)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            SqlCommand cmd_sp = new SqlCommand("Cancelled_Loans", con);
            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@Startdate", Startdate);
            cmd_sp.Parameters.AddWithValue("@Enddate", Enddate);
            cmd_sp.Parameters.AddWithValue("@UPTODATE", UPTODATE);
	    cmd_sp.Parameters.AddWithValue("@STATUS", disbursmentStatus);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

  public static DataSet loanduefordisb()
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            SqlCommand cmd_sp = new SqlCommand("LOANDUEFORDISBURSEMENT", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
         
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

  public static DataSet collectionForMonth(string startDate, string endDate)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("CollectionOnMonth", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@startDate", startDate);
            cmd_sp.Parameters.AddWithValue("@endDate", endDate);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

  public static DataSet collectionBeforeDisb(string startDate, string endDate, string lastdayofpreviousmonth)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("CollectionBeforeDisb", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@startDate", startDate);
            cmd_sp.Parameters.AddWithValue("@endDate", endDate);
            cmd_sp.Parameters.AddWithValue("@lastdayofpreviousmonth", lastdayofpreviousmonth);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

  public static DataSet collectionForMonth_Rows(string startDate, string endDate)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("CollectionOnMonth_Rows", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@startDate", startDate);
            cmd_sp.Parameters.AddWithValue("@endDate", endDate);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }


 public static DataSet ProjectedInterestDetailsReport_Summary(string fromDate, string toDate, string statusValue, string disStatus,string US)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("ProjectedInterestDetailsReport_Summary", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@fromDate", fromDate);
            cmd_sp.Parameters.AddWithValue("@toDate", toDate);
            cmd_sp.Parameters.AddWithValue("@status", statusValue);
            cmd_sp.Parameters.AddWithValue("@disbursement", disStatus);
            cmd_sp.Parameters.AddWithValue("@USD", US);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;
        }

    }

  public static DataSet OVERDUE_WITH_SUMMARY_ASOFTODAY(string statusValue, string disStatus,string US)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("OVERDUE_WITH_SUMMARY_ASOFTODAY", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@status", statusValue);
            cmd_sp.Parameters.AddWithValue("@disbursement", disStatus);
            cmd_sp.Parameters.AddWithValue("@USD", US);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet DATASET = new DataSet();
            da.Fill(DATASET);
            return DATASET;
        }

    }

   public static DataSet LOANSANCTIONREGISTER(string startDate, string endDate)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("[LOANSANCTIONREGISTER]", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@FROMDATE", startDate);
            cmd_sp.Parameters.AddWithValue("@TODATE", endDate);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

  public static DataSet Pre_Disbused_Emi_Collection(string startDate, string endDate)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("[PRE_DISBURSEMENT_EMI_COLLECTION]", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@FROMDATE", startDate);
            cmd_sp.Parameters.AddWithValue("@TODATE", endDate);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

    public static DataTable InterestLoss_Closed(string fromDate, string toDate, string status)
    {
        SqlCommand cmd_sp;
        using (SqlConnection con = Fintrax.GetDBConnection())
        {
           
            cmd_sp = new SqlCommand("INTERESTLOSS_CLOSED", con);
          
            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@FROMDATE", fromDate);
            cmd_sp.Parameters.AddWithValue("@TODATE", toDate);
            cmd_sp.Parameters.AddWithValue("@STATUS", status);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataTable datatable = new DataTable();
            da.Fill(datatable);
            return datatable;

        }

    }

    public static DataTable getInterestLoss_Closed(string fromDate, string upToDate)
    {
        SqlCommand cmd;
        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            cmd = new SqlCommand("select LOANNO,CONVERT(VARCHAR,INSTALLMENTDATE,105)INSTALLMENTDATE,ACTUALAMOUNT, INTEREST,PRINCIPAL from INTERESTLOSS_CLOSED_TABLE where INSTALLMENTDATE between '" + fromDate + "' and '" + upToDate + "'", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            sda.Fill(ds);
            return ds;

        }

    }

    public static int InsertInterestLossClosed(string loanNo, double loanAmount, double ROI, double EMI, double interest, double principal, int installmentNo, DateTime DATE)
    {

        int rowsAffected = 0;
        SqlDataAdapter da = new SqlDataAdapter();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {
            try
            {

                da.InsertCommand = new SqlCommand("insert into INTERESTLOSS_CLOSED_TABLE values('" + loanNo + "','" + loanAmount + "','" + EMI + "','" + interest + "','" + principal + "','" + installmentNo + "','" + DATE + "')", cs1);

                rowsAffected = da.InsertCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string msg = "Error in Insert Schedule Query " + " " + ex.Message;

                throw new Exception(msg, ex);

            }
        }
        return (rowsAffected);

    }

   public static DataSet rollOver(string fromDate, string toDate)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("ROLLOVER", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@STARTDATE", fromDate);
            cmd_sp.Parameters.AddWithValue("@ENDDATE", toDate);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

  public static DataSet DueReport(string fromDate, string toDate)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("DueReport", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@STARTDATE", fromDate);
            cmd_sp.Parameters.AddWithValue("@ENDDATE", toDate);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

 public static DataSet overdueReport()
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("OverdueAsOfToday", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

  public static DataSet ProjectedInterestDetail_Histo(string fromDate, string toDate, string disStatus)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            SqlCommand cmd_sp;
            cmd_sp = new SqlCommand("ProjectedInterestDetailsReport_All", con);
            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@fromDate", fromDate);
            cmd_sp.Parameters.AddWithValue("@toDate", toDate);
            cmd_sp.Parameters.AddWithValue("@disbursement", disStatus);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

    public static DataSet ClientRegister(string US)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            SqlCommand cmd_sp;
            cmd_sp = new SqlCommand("CLIENTREGISTER", con);
            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@USD", US);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

 public static DataSet BlukReceiptTotal(string fromDate, string toDate)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            SqlCommand cmd_sp;
            cmd_sp = new SqlCommand("BULKRECEIPTREPORT", con);
            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@fromDate", fromDate);
            cmd_sp.Parameters.AddWithValue("@toDate", toDate);

            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

public static DataTable LoanRepaymentSchedule(string LOANNO)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("LOANREPAYMANTSCHEDULE", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@LOANNO", LOANNO);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataTable datatable = new DataTable();
            da.Fill(datatable);
            return datatable;

        }
    }


 public static DataSet LoadCybilUpdateDetailsOnLoanNo(string loanNo)
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        using (SqlConnection cs1 = Fintrax.GetDBConnection())
        {

            SqlCommand SqlCmd = new SqlCommand("declare @tempTable as table (LOANNO varchar(50),DateofLastPayment varchar(50),DateClosed varchar(50),DateReported varchar(50),CurrentBalance float,ActualPaymentAmt float,DateofLastPayment2 datetime) insert into @tempTable SELECT distinct LN.LOANNO,(SELECT TOP 1 REPLACE(CONVERT(VARCHAR(10),CONVERT(DATETIME,BDS.REALISATIONDATE,103)-2,103),'','') FROM BANKDEPOSITSLIPDETAILS BDS WHERE BDS.LNID = LN.LNID ORDER BY BDS.REALISATIONDATE DESC) DateofLastPayment,ISNULL((SELECT TOP 1 REPLACE(CONVERT(VARCHAR(10),CONVERT(DATETIME,LCDDATE,103)-2,103),'','') FROM LOANCLOSUREDETAILS WHERE LNID = LN.LNID),'') DateClosed,REPLACE(CONVERT(VARCHAR(10),CONVERT(DATETIME,GETDATE(),103),103),'','') DateReported,(select top(1) lsd.LOANBALANCE from LOANSCHEDULE ls join LOANSCHEDULEDETAILS lsd on ls.LSID=lsd.LSID where LNID=LN.LNID and ls.STATUS=0 and lsd.EMISTATUS=0 order by lsd.LOANBALANCE desc) CurrentBalance ,(SELECT top(1) BDS.AMOUNT FROM BANKDEPOSITSLIPDETAILS BDS WHERE BDS.LNID = LN.            LNID ORDER BY BDS.REALISATIONDATE DESC) ActualPaymentAmt,(SELECT TOP 1 CONVERT(DATETIME,BDS.REALISATIONDATE-2,103)FROM BANKDEPOSITSLIPDETAILS BDS WHERE BDS.LNID = LN.LNID ORDER BY BDS.REALISATIONDATE DESC) DateofLastPayment FROM LOANS LN,LEDGER LED,LEDGERADDRESS LADD WHERE LN.ACTIVE IN (2,0,1,3) AND LN.LEDID = LED.LEDID AND LED.LEDID = LADD.LEDID AND LN.DISBURSEMENTSTATUS =1 And LN.LOANNO=@loanNo  select *, DATEDIFF(day,t1.DateofLastPayment2 ,  GETDATE()) No_of_days_past_due from @tempTable T1", cs1);
            SqlCmd.Parameters.Add("@loanNo", SqlDbType.VarChar).Value = loanNo;
            da = new SqlDataAdapter(SqlCmd);
            ds = new DataSet();
            da.Fill(ds);
        }
        return (ds);
    }
public static DataSet GSTIN_Details(string name)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {
            SqlCommand cmd_sp;
            cmd_sp = new SqlCommand("GSTIN", con);
            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@Name", name);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }

 public static DataSet LOANDISBURSEMENTREGISTER(string startDate, string endDate)
    {

        using (SqlConnection con = Fintrax.GetDBConnection())
        {

            SqlCommand cmd_sp = new SqlCommand("[LOAN_DISBURSEMENT_REGISTER]", con);

            cmd_sp.CommandType = CommandType.StoredProcedure;
            cmd_sp.Parameters.AddWithValue("@STARTDATE", startDate);
            cmd_sp.Parameters.AddWithValue("@ENDDATE", endDate);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = cmd_sp;
            DataSet datatable = new DataSet();
            da.Fill(datatable);
            return datatable;

        }

    }
}