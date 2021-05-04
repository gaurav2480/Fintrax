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

public partial class WebSite5_production_EditCybil : System.Web.UI.Page
{

    static string pname;
    
    //public string getdata()
    //{
    //    string user = (string)Session["username"];
    //    string htmlstr = "";
    //    string conn = ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString;
    //    string query = "select distinct parentnode,ReportOrder from user_group_access ug where username ='"+user+"' order by ReportOrder asc";
    //    SqlConnection sqlcon = new SqlConnection(conn);
    //    sqlcon.Open();
    //    SqlCommand cmd = new SqlCommand(query, sqlcon);
    //    SqlDataReader reader = cmd.ExecuteReader();

    //    while (reader.Read())
    //    {
    //        string name = reader.GetString(0);
    //        if (name == "")
    //        {

    //        }
    //        else
    //        {
    //            if (name == "Reports")
    //            {
    //                htmlstr += "<li><a href='reportSlider.aspx'><i class='fa fa-home'></i>" + name + " <span class='fa fa-chevron - down'></span> </a><ul class='nav child_menu'>";
    //                htmlstr += "</ul></li>";
    //                name = "";
    //            }
    //            if (name == "")
    //            {

    //            }
    //            else
    //            {
    //                htmlstr += "<li><a><i class='fa fa-home'></i>" + name + " <span class='fa fa-chevron - down'></span> </a><ul class='nav child_menu'>";
    //                SqlConnection sqlcon1 = new SqlConnection(conn);
    //                sqlcon1.Open();
    //                string query1 = "select * from user_group_access ug where ug.ParentNode='" + name + "' and username ='" + user + "' order By page_order asc";
    //                SqlCommand cmd1 = new SqlCommand(query1, sqlcon1);


    //                SqlDataReader reader1 = cmd1.ExecuteReader();
    //                while (reader1.Read())
    //                {
    //                    string pagename = reader1.GetString(1);
    //                    string pageurl = reader1.GetString(3);
    //                    string AccessName = reader1.GetString(6);

    //                    htmlstr += "<li><a href=" + pageurl + "?name=" + AccessName + ">" + pagename + " </a></li>";
    //                    Session["pagename"] = pagename;
    //                    string office = Queries.GetOffice(user);
    //                    Session["office"] = office;
    //                    Session["username"] = user;
    //                }

    //                htmlstr += "</ul></li>";



    //                reader1.Close();
    //                sqlcon1.Close();

    //            }

    //        }
    //    }
    //    reader.Close();
    //    sqlcon.Close();
    //    return htmlstr;

    //}

    protected void Page_Load(object sender, EventArgs e)
    {

        //string user = (string)Session["username"];
        // office = (string)Session["office"];
        //  if (user == null)
        //  {
        //     Response.Redirect("login.aspx");
        // }

        //string user = (string)Session["username"];
        // Label1.Text = "HI!! " + user;
        //   Label2.Text = user;
        // string val = getdata();
        if (!IsPostBack)
        {
            string getloanNo = Convert.ToString(Request.QueryString["LoanNo"]);
            DataSet ds = Fintrax.LoadCybilDetailsOnLoanNofromCybil(getloanNo);
            Session["loanNo"] = ""; Session["customerName"] = ""; Session["dateOfBirth"] = ""; Session["gender"]= "";
            Session["incomeTaxIDNumber"] = ""; Session["passportNo"] = ""; Session["passportIssueDate"] = ""; Session["passportExpiryDate"] = "";
            Session["voterIDNo"] = ""; Session["drivingLicenseNo"] = ""; Session["drivingLicenseIssueDate"] = ""; Session["drivingLicenseExpiryDate"] = "";
            Session["RationCardNumber"] = ""; Session["UniversalIDNumber"] = ""; Session["AdditionalID1"] = ""; Session["AdditionalID2"] = "";

            Session["TelephoneNoMobile"] = ""; Session["TelephoneNoResidence"] = ""; Session["TelephoneNoOffice"] = ""; Session["ExtensionOffice"] = "";
            Session["TelephoneNoOther"] = ""; Session["ExtensionOther"] = ""; Session["EmailID1"] = ""; Session["EmailID2"] = ""; Session["Address1"] = "";
            Session["StateCode1"] = ""; Session["PINCode1"] = ""; Session["AddressCategory1"] = ""; Session["ResidenceCode1"] = ""; Session["Address2"] = "";
            Session["StateCode2"] = ""; Session["PINCode2"] = ""; Session["AddressCategory2"] = ""; Session["ResidenceCode2"] = "";
            Session["CurrentNewMemberCode"] = ""; Session["CurrentNewMemberShortName"] = ""; Session["CurrNewAccountNo"] = ""; Session["AccountType"] = "";
            Session["OwnershipIndicator"] = ""; Session["DateOpenedDisbursed"] = ""; Session["DateofLastPayment"] = ""; Session["DateClosed"] = ""; Session["DateReported"] = "";
            Session["HighCreditSanctionedAmt"] = ""; Session["CurrentBalance"] = ""; Session["AmtOverdue"] = "";
            Session["NoOfDaysPastDue"] = ""; Session["OldMbrCode"] = ""; Session["OldMbrName"] = "";
            Session["OldAccountNumber"] = ""; Session["OldAccountType"] = ""; Session["OldOwnershipIndicator"] = "";
            Session["SuitFiled"] = ""; Session["WrittenOffSettled"] = ""; Session["AssetClassification"] = "";
            Session["CollateralValue"] = ""; Session["CollateralType"] = ""; Session["CreditLimit"] = "";
            Session["CashLimit"] = ""; Session["RateOfInterest"] = ""; Session["RepaymentTenure"] = "";
            Session["EMIAmount"] = ""; Session["WrittenOffTotalAmount"] = ""; Session["WrittenOffPrincipalAmount"] = "";
            Session["SettledAmount"] = ""; Session["PaymentFrequency"] = ""; Session["ActualPaymentAmount"] = "";
            Session["OccupationCode"] = ""; Session["Income"] = ""; Session["NetGrossIncome"] = ""; Session["MonthlyAnnualIncome"] = "";
            Session["RegisteredDate"] = "";

            Session["loanNo"] = ds.Tables[0].Rows[0]["loanNo"].ToString();
            Session["customerName"] = ds.Tables[0].Rows[0]["Consumer Name"].ToString();
            Session["dateOfBirth"] = ds.Tables[0].Rows[0]["Date of Birth"].ToString();
            Session["gender"] = ds.Tables[0].Rows[0]["Gender"].ToString();
            Session["incomeTaxIDNumber"] = ds.Tables[0].Rows[0]["Income Tax ID Number"].ToString();
            Session["passportNo"] = ds.Tables[0].Rows[0]["Passport Number"].ToString();
            Session["passportIssueDate"] = ds.Tables[0].Rows[0]["Passport Issue Date"].ToString();
            Session["passportExpiryDate"] = ds.Tables[0].Rows[0]["Passport Expiry Date"].ToString();

            string category = ds.Tables[0].Rows[0]["Address Category 1"].ToString();

            Session["voterIDNo"] = ds.Tables[0].Rows[0]["Voter ID Number"].ToString();
            Session["drivingLicenseNo"] = ds.Tables[0].Rows[0]["Driving License Number"].ToString();
            Session["drivingLicenseIssueDate"] = ds.Tables[0].Rows[0]["Driving License IssueDate"].ToString();
            Session["drivingLicenseExpiryDate"] = ds.Tables[0].Rows[0]["Driving License ExpiryDate"].ToString();
            Session["RationCardNumber"] = ds.Tables[0].Rows[0]["Ration Card Number"].ToString();
            Session["UniversalIDNumber"] = ds.Tables[0].Rows[0]["Universal ID Number"].ToString();
            Session["AdditionalID1"] = ds.Tables[0].Rows[0]["Additional ID #1"].ToString();
            Session["AdditionalID2"] = ds.Tables[0].Rows[0]["Additional ID #2"].ToString();

            Session["TelephoneNoMobile"] = ds.Tables[0].Rows[0]["Telephone No#Mobile"].ToString();
            Session["TelephoneNoResidence"] = ds.Tables[0].Rows[0]["Telephone No#Residence"].ToString();
            Session["TelephoneNoOffice"] = ds.Tables[0].Rows[0]["Telephone No#Office"].ToString();
            Session["ExtensionOffice"] = ds.Tables[0].Rows[0]["Extension Office"].ToString();
            Session["TelephoneNoOther"] = ds.Tables[0].Rows[0]["Telephone No#Other"].ToString();
            Session["ExtensionOther"] = ds.Tables[0].Rows[0]["ExtensionOther"].ToString();

            Session["EmailID1"] = ds.Tables[0].Rows[0]["Email ID 1"].ToString();
            Session["EmailID2"] = ds.Tables[0].Rows[0]["Email ID 2"].ToString();
            Session["Address1"] = ds.Tables[0].Rows[0]["Address 1"].ToString();
            Session["StateCode1"] = ds.Tables[0].Rows[0]["State Code 1"].ToString();
            Session["PINCode1"] = ds.Tables[0].Rows[0]["PIN Code 1"].ToString();
            Session["AddressCategory1"] = ds.Tables[0].Rows[0]["Address Category 1"].ToString();
            Session["ResidenceCode1"] = ds.Tables[0].Rows[0]["Residence Code 1"].ToString();
            Session["Address2"] = ds.Tables[0].Rows[0]["Address 2"].ToString();
            Session["StateCode2"] = ds.Tables[0].Rows[0]["State Code 2"].ToString();
            Session["PINCode2"] = ds.Tables[0].Rows[0]["PIN Code 2"].ToString();
            Session["AddressCategory2"] = ds.Tables[0].Rows[0]["Address Category 2"].ToString();
            Session["ResidenceCode2"] = ds.Tables[0].Rows[0]["Residence Code 2"].ToString();

            Session["CurrentNewMemberCode"] = ds.Tables[0].Rows[0]["Current/New Member Code"].ToString();
            Session["CurrentNewMemberShortName"] = ds.Tables[0].Rows[0]["Current/New Member Short Name"].ToString();
            Session["CurrNewAccountNo"] = ds.Tables[0].Rows[0]["Curr/New Account No"].ToString();
            Session["AccountType"] = ds.Tables[0].Rows[0]["Account Type"].ToString();
            Session["OwnershipIndicator"] = ds.Tables[0].Rows[0]["Ownership Indicator"].ToString();

            Session["DateOpenedDisbursed"] = ds.Tables[0].Rows[0]["Date Opened/Disbursed"].ToString();
            Session["DateofLastPayment"] = ds.Tables[0].Rows[0]["Date of Last Payment"].ToString();
            Session["DateClosed"] = ds.Tables[0].Rows[0]["Date Closed"].ToString();
            Session["DateReported"] = ds.Tables[0].Rows[0]["Date Reported"].ToString();

            Session["HighCreditSanctionedAmt"] = ds.Tables[0].Rows[0]["High Credit/Sanctioned Amt"].ToString();
            Session["CurrentBalance"] = ds.Tables[0].Rows[0]["Current  Balance"].ToString();
            Session["AmtOverdue"] = ds.Tables[0].Rows[0]["Amt Overdue"].ToString();

            Session["NoOfDaysPastDue"] = ds.Tables[0].Rows[0]["No of Days Past Due"].ToString();
            Session["OldMbrCode"] = ds.Tables[0].Rows[0]["Old Mbr Code"].ToString();
            Session["OldMbrName"] = ds.Tables[0].Rows[0]["Old Mbr Short Name"].ToString();

            Session["OldAccountNumber"] = ds.Tables[0].Rows[0]["Old Acc No"].ToString();
            Session["OldAccountType"] = ds.Tables[0].Rows[0]["Old Acc Type"].ToString();
            Session["OldOwnershipIndicator"] = ds.Tables[0].Rows[0]["Old Ownership Indicator"].ToString();

            Session["SuitFiled"] = ds.Tables[0].Rows[0]["Suit Filed / Wilful Default"].ToString();
            Session["WrittenOffSettled"] = ds.Tables[0].Rows[0]["Written-off and Settled Status"].ToString();
            Session["AssetClassification"] = ds.Tables[0].Rows[0]["Asset Classification"].ToString();
            
            Session["CollateralValue"] = ds.Tables[0].Rows[0]["Value of Collateral"].ToString();
            Session["CollateralType"] = ds.Tables[0].Rows[0]["Type of Collateral"].ToString();
            Session["CreditLimit"] = ds.Tables[0].Rows[0]["Credit Limit"].ToString();
            
          
            Session["CashLimit"] = ds.Tables[0].Rows[0]["Cash Limit"].ToString();
            Session["RateOfInterest"] = ds.Tables[0].Rows[0]["Rate of Interest"].ToString();
            Session["RepaymentTenure"] = ds.Tables[0].Rows[0]["RepaymentTenure"].ToString();

            Session["EMIAmount"] = ds.Tables[0].Rows[0]["EMI Amount"].ToString();
            Session["WrittenOffTotalAmount"] = ds.Tables[0].Rows[0]["Written- off Amount (Total)"].ToString();
            Session["WrittenOffPrincipalAmount"] = ds.Tables[0].Rows[0]["Written- off Principal Amount"].ToString();

           
            Session["SettledAmount"] = ds.Tables[0].Rows[0]["Settlement Amt"].ToString();
            Session["PaymentFrequency"] = ds.Tables[0].Rows[0]["Payment Frequency"].ToString();
            Session["ActualPaymentAmount"] = ds.Tables[0].Rows[0]["Actual Payment Amt"].ToString();

            Session["OccupationCode"] = ds.Tables[0].Rows[0]["Occupation Code"].ToString();
            Session["Income"] = ds.Tables[0].Rows[0]["Income"].ToString();
            Session["NetGrossIncome"] = ds.Tables[0].Rows[0]["Net/Gross Income Indicator"].ToString();
            Session["MonthlyAnnualIncome"] = ds.Tables[0].Rows[0]["Monthly/Annual Income Indicator"].ToString();
            Session["RegisteredDate"] = ds.Tables[0].Rows[0]["RegisteredDate"].ToString();

            loanNo.Text = Session["loanNo"].ToString();
            customerName.Text = Session["customerName"].ToString();
            TextBox4.Text = Session["dateOfBirth"].ToString();

            if (Session["gender"].ToString() == " " || Session["gender"].ToString() == null || Session["gender"].ToString() == "")
            {
                DataSet gen = Fintrax.LoadGender(Session["gender"].ToString());
                gender.DataSource = gen;
                gender.DataTextField = "Values";
                gender.DataValueField = "Code";
                gender.AppendDataBoundItems = true;
                gender.Items.Insert(0, new ListItem(Session["gender"].ToString(), ""));
                gender.DataBind();
            }
            else
            {
                DataSet gen = Fintrax.LoadGender1(Session["gender"].ToString());
                gender.DataSource = gen;
                gender.DataTextField = "Values";
                gender.DataValueField = "Code";
                gender.AppendDataBoundItems = true;
                //  gender.Items.Insert(0, new ListItem(Session["gender"].ToString(), ""));
                gender.DataBind();
            }
            
            incomeTaxIDNumber.Text = Session["incomeTaxIDNumber"].ToString();
            passportNo.Text = Session["passportNo"].ToString();
            passportIssueDate.Text = Session["passportIssueDate"].ToString();
            passportExpiryDate.Text = Session["passportExpiryDate"].ToString();
            voterIDNo.Text = Session["voterIDNo"].ToString();
            drivingLicenseNo.Text = Session["drivingLicenseNo"].ToString();
            drivingLicenseIssueDate.Text = Session["drivingLicenseIssueDate"].ToString();
            drivingLicenseExpiryDate.Text = Session["drivingLicenseExpiryDate"].ToString();
            rationCardNo.Text = Session["RationCardNumber"].ToString();
            universalIDNo.Text = Session["UniversalIDNumber"].ToString();
            additionalID1.Text = Session["AdditionalID1"].ToString();
            additionalID2.Text = Session["AdditionalID2"].ToString();

            telephoneNoMobile.Text = Session["TelephoneNoMobile"].ToString();
            telephoneNoResidence.Text = Session["TelephoneNoResidence"].ToString();
            telephoneNoOffice.Text = Session["TelephoneNoOffice"].ToString();
            extensionOffice.Text = Session["ExtensionOffice"].ToString();
            otherTelePhoneNo.Text = Session["TelephoneNoOther"].ToString();
            otherExtension.Text = Session["ExtensionOther"].ToString();
            emailID.Text = Session["EmailID1"].ToString();
            emailID2.Text = Session["EmailID2"].ToString();
            address1.Text = Session["Address1"].ToString();

            if (Session["StateCode1"].ToString()==" " || Session["StateCode1"].ToString()==null || Session["StateCode1"].ToString() == "")
            {
                DataSet state = Fintrax.LoadStateCode(Session["StateCode1"].ToString());
                state1.DataSource = state;
                state1.DataTextField = "Values";
                state1.DataValueField = "Code";
                state1.AppendDataBoundItems = true;
                state1.Items.Insert(0, new ListItem(Session["StateCode1"].ToString(), ""));
                state1.DataBind();
            }
            else
            {
                DataSet state = Fintrax.LoadStateCode1(Session["StateCode1"].ToString());
                state1.DataSource = state;
                state1.DataTextField = "Values";
                state1.DataValueField = "Code";
                state1.AppendDataBoundItems = true;
                //state1.Items.Insert(0, new ListItem(Session["StateCode1"].ToString(), ""));
                state1.DataBind();
            }
          

            pincode1.Text = Session["PINCode1"].ToString();

            if (Session["AddressCategory1"].ToString()==" " || Session["AddressCategory1"].ToString()==null || Session["AddressCategory1"].ToString() == "") {
                DataSet addCategory = Fintrax.LoadAddressCategory(Session["AddressCategory1"].ToString());
                category1.DataSource = addCategory;
                category1.DataTextField = "addValues";
                category1.DataValueField = "addCode";
                category1.AppendDataBoundItems = true;
                category1.Items.Insert(0, new ListItem(Session["AddressCategory1"].ToString(), ""));
                category1.DataBind();
            }
            else
            {
                DataSet addCategory = Fintrax.LoadAddressCategory1(Session["AddressCategory1"].ToString());
                category1.DataSource = addCategory;
                category1.DataTextField = "addValues";
                category1.DataValueField = "addCode";
                category1.AppendDataBoundItems = true;
                // category1.Items.Insert(0, new ListItem(Session["AddressCategory1"].ToString(), ""));
                category1.DataBind();
            }

            if (Session["ResidenceCode1"].ToString()==" " || Session["ResidenceCode1"].ToString()==null || Session["ResidenceCode1"].ToString() == "")
            {
                DataSet resiCode = Fintrax.LoadResidenceCode(Session["ResidenceCode1"].ToString());
                residence1.DataSource = resiCode;
                residence1.DataTextField = "resValues";
                residence1.DataValueField = "resCode";
                residence1.AppendDataBoundItems = true;
                residence1.Items.Insert(0, new ListItem(Session["ResidenceCode1"].ToString(), ""));
                residence1.DataBind();
            }
            else
            {
                DataSet resiCode = Fintrax.LoadResidenceCode1(Session["ResidenceCode1"].ToString());
                residence1.DataSource = resiCode;
                residence1.DataTextField = "resValues";
                residence1.DataValueField = "resCode";
                residence1.AppendDataBoundItems = true;
                //  residence1.Items.Insert(0, new ListItem(Session["ResidenceCode1"].ToString(), ""));
                residence1.DataBind();
            }
          

            address2.Text = Session["Address2"].ToString();

            if (Session["StateCode2"].ToString()==" " || Session["StateCode2"].ToString()== null || Session["StateCode2"].ToString() == "")
            {
                DataSet states2 = Fintrax.LoadStateCode(Session["StateCode2"].ToString());
                state2.DataSource = states2;
                state2.DataTextField = "Values";
                state2.DataValueField = "Code";
                state2.AppendDataBoundItems = true;
                state2.Items.Insert(0, new ListItem(Session["StateCode2"].ToString(), ""));
                state2.DataBind();
            }
            else
            {
                DataSet states2 = Fintrax.LoadStateCode1(Session["StateCode2"].ToString());
                state2.DataSource = states2;
                state2.DataTextField = "Values";
                state2.DataValueField = "Code";
                state2.AppendDataBoundItems = true;
                // state2.Items.Insert(0, new ListItem(Session["StateCode2"].ToString(), ""));
                state2.DataBind();
            }
          
            pincode2.Text = Session["PINCode2"].ToString();

            if (Session["AddressCategory2"].ToString()==" " || Session["AddressCategory2"].ToString()==null || Session["AddressCategory2"].ToString() == "")
            {
                DataSet addCategory2 = Fintrax.LoadAddressCategory(Session["AddressCategory2"].ToString());
                category2.DataSource = addCategory2;
                category2.DataTextField = "addValues";
                category2.DataValueField = "addCode";
                category2.AppendDataBoundItems = true;
                category2.Items.Insert(0, new ListItem(Session["AddressCategory2"].ToString(), ""));
                category2.DataBind();
            }
            else
            {
                DataSet addCategory2 = Fintrax.LoadAddressCategory1(Session["AddressCategory2"].ToString());
                category2.DataSource = addCategory2;
                category2.DataTextField = "addValues";
                category2.DataValueField = "addCode";
                category2.AppendDataBoundItems = true;
                // category2.Items.Insert(0, new ListItem(Session["AddressCategory2"].ToString(), ""));
                category2.DataBind();
            }

            if (Session["ResidenceCode2"].ToString()==" " || Session["ResidenceCode2"].ToString()==null || Session["ResidenceCode2"].ToString() == "")
            {
                DataSet resiCode2 = Fintrax.LoadResidenceCode(Session["ResidenceCode2"].ToString());
                residence2.DataSource = resiCode2;
                residence2.DataTextField = "resValues";
                residence2.DataValueField = "resCode";
                residence2.AppendDataBoundItems = true;
                residence2.Items.Insert(0, new ListItem(Session["ResidenceCode2"].ToString(), ""));
                residence2.DataBind();
            }
            else
            {
                DataSet resiCode2 = Fintrax.LoadResidenceCode1(Session["ResidenceCode2"].ToString());
                residence2.DataSource = resiCode2;
                residence2.DataTextField = "resValues";
                residence2.DataValueField = "resCode";
                residence2.AppendDataBoundItems = true;
                // residence2.Items.Insert(0, new ListItem(Session["ResidenceCode2"].ToString(), ""));
                residence2.DataBind();
            }
           

            currentNewMemberCode.Text = Session["CurrentNewMemberCode"].ToString();
            currentNewMemberName.Text = Session["CurrentNewMemberShortName"].ToString();
            currentNewAccNo.Text = Session["CurrNewAccountNo"].ToString();

            if (Session["AccountType"].ToString()==" " || Session["AccountType"].ToString()==null || Session["AccountType"].ToString() == "")
            {
                DataSet accType = Fintrax.LoadAccountType(Session["AccountType"].ToString());
                accountType.DataSource = accType;
                accountType.DataTextField = "AValues";
                accountType.DataValueField = "ACode";
                accountType.AppendDataBoundItems = true;
                accountType.Items.Insert(0, new ListItem(Session["AccountType"].ToString(), ""));
                accountType.DataBind();
            }
            else
            {
                DataSet accType = Fintrax.LoadAccountType1(Session["AccountType"].ToString());
                accountType.DataSource = accType;
                accountType.DataTextField = "AValues";
                accountType.DataValueField = "ACode";
                accountType.AppendDataBoundItems = true;
                // accountType.Items.Insert(0, new ListItem(Session["AccountType"].ToString(), ""));
                accountType.DataBind();
            }

            if (Session["OwnershipIndicator"].ToString() == " " || Session["OwnershipIndicator"].ToString() == null || Session["OwnershipIndicator"].ToString() == "")
            {
                DataSet ownershipInd = Fintrax.LoadOwnershipIndicator(Session["OwnershipIndicator"].ToString());
                ownershipIndicator.DataSource = ownershipInd;
                ownershipIndicator.DataTextField = "OValues";
                ownershipIndicator.DataValueField = "OCode";
                ownershipIndicator.AppendDataBoundItems = true;
                ownershipIndicator.Items.Insert(0, new ListItem(Session["OwnershipIndicator"].ToString(), ""));
                ownershipIndicator.DataBind();
            }
            else
            {
                DataSet ownershipInd = Fintrax.LoadOwnershipIndicator1(Session["OwnershipIndicator"].ToString());
                ownershipIndicator.DataSource = ownershipInd;
                ownershipIndicator.DataTextField = "OValues";
                ownershipIndicator.DataValueField = "OCode";
                ownershipIndicator.AppendDataBoundItems = true;
                // ownershipIndicator.Items.Insert(0, new ListItem(Session["OwnershipIndicator"].ToString(), ""));
                ownershipIndicator.DataBind();
            }
           

            dateOpenedDisbursed.Text = Session["DateOpenedDisbursed"].ToString();
            lastPaymentDate.Text = Session["DateofLastPayment"].ToString();
            dateClosed.Text = Session["DateClosed"].ToString();
            dateReported.Text = Session["DateReported"].ToString();
            highCreditSanctionedAmt.Text = Session["HighCreditSanctionedAmt"].ToString();
            currentBalance.Text = Session["CurrentBalance"].ToString();
            amountOverdue.Text = Session["AmtOverdue"].ToString();
            noOfDaysPastDue.Text = Session["NoOfDaysPastDue"].ToString();
            oldMemberCode.Text= Session["OldMbrCode"].ToString();
            oldMemberName.Text = Session["OldMbrName"].ToString();
            oldMemberAccountNo.Text= Session["OldAccountNumber"].ToString();

            if (Session["OldAccountType"].ToString()==" " || Session["OldAccountType"].ToString()==null || Session["OldAccountType"].ToString() == "")
            {
                DataSet oldAccType = Fintrax.LoadAccountType(Session["OldAccountType"].ToString());
                oldAccountType.DataSource = oldAccType;
                oldAccountType.DataTextField = "AValues";
                oldAccountType.DataValueField = "ACode";
                oldAccountType.AppendDataBoundItems = true;
                oldAccountType.Items.Insert(0, new ListItem(Session["OldAccountType"].ToString(), ""));
                oldAccountType.DataBind();
            }
            else
            {
                DataSet oldAccType = Fintrax.LoadAccountType1(Session["OldAccountType"].ToString());
                oldAccountType.DataSource = oldAccType;
                oldAccountType.DataTextField = "AValues";
                oldAccountType.DataValueField = "ACode";
                oldAccountType.AppendDataBoundItems = true;
                // oldAccountType.Items.Insert(0, new ListItem(Session["OldAccountType"].ToString(), ""));
                oldAccountType.DataBind();
            }

            if (Session["OldOwnershipIndicator"].ToString()==" " || Session["OldOwnershipIndicator"].ToString()==null || Session["OldOwnershipIndicator"].ToString() == "")
            {
                DataSet oldOwnershipInd = Fintrax.LoadOwnershipIndicator(Session["OldOwnershipIndicator"].ToString());
                oldOwnershipIndicator.DataSource = oldOwnershipInd;
                oldOwnershipIndicator.DataTextField = "OValues";
                oldOwnershipIndicator.DataValueField = "OCode";
                oldOwnershipIndicator.AppendDataBoundItems = true;
                oldOwnershipIndicator.Items.Insert(0, new ListItem(Session["OldOwnershipIndicator"].ToString(), ""));
                oldOwnershipIndicator.DataBind();
            }
            else
            {
                DataSet oldOwnershipInd = Fintrax.LoadOwnershipIndicator1(Session["OldOwnershipIndicator"].ToString());
                oldOwnershipIndicator.DataSource = oldOwnershipInd;
                oldOwnershipIndicator.DataTextField = "OValues";
                oldOwnershipIndicator.DataValueField = "OCode";
                oldOwnershipIndicator.AppendDataBoundItems = true;
                //  oldOwnershipIndicator.Items.Insert(0, new ListItem(Session["OldOwnershipIndicator"].ToString(), ""));
                oldOwnershipIndicator.DataBind();
            }

            if (Session["SuitFiled"].ToString()==" " || Session["SuitFiled"].ToString()==null || Session["SuitFiled"].ToString() == "")
            {
                DataSet Wilful = Fintrax.LoadWilful();
                wilfulStatus.DataSource = Wilful;
                wilfulStatus.DataTextField = "WLValues";
                wilfulStatus.DataValueField = "WLCode";
                wilfulStatus.AppendDataBoundItems = true;
                wilfulStatus.Items.Insert(0, new ListItem(Session["SuitFiled"].ToString(), ""));
                wilfulStatus.DataBind();
            }
            else
            {
                DataSet Wilful = Fintrax.LoadWilful1(Session["SuitFiled"].ToString());
                wilfulStatus.DataSource = Wilful;
                wilfulStatus.DataTextField = "WLValues";
                wilfulStatus.DataValueField = "WLCode";
                wilfulStatus.AppendDataBoundItems = true;
                // wilfulStatus.Items.Insert(0, new ListItem(Session["SuitFiled"].ToString(), ""));
                wilfulStatus.DataBind();
            }

            if (Session["WrittenOffSettled"].ToString()==" " || Session["WrittenOffSettled"].ToString()==null || Session["WrittenOffSettled"].ToString() == "")
            {
                DataSet writtenOffSettled = Fintrax.LoadWrittenOffSettled(Session["WrittenOffSettled"].ToString());
                writtenOffandSettled.DataSource = writtenOffSettled;
                writtenOffandSettled.DataTextField = "WValues";
                writtenOffandSettled.DataValueField = "WCode";
                writtenOffandSettled.AppendDataBoundItems = true;
                writtenOffandSettled.Items.Insert(0, new ListItem(Session["WrittenOffSettled"].ToString(), ""));
                writtenOffandSettled.DataBind();
            }
            else
            {
                DataSet writtenOffSettled = Fintrax.LoadWrittenOffSettled1(Session["WrittenOffSettled"].ToString());
                writtenOffandSettled.DataSource = writtenOffSettled;
                writtenOffandSettled.DataTextField = "WValues";
                writtenOffandSettled.DataValueField = "WCode";
                writtenOffandSettled.AppendDataBoundItems = true;
                //  writtenOffandSettled.Items.Insert(0, new ListItem(Session["WrittenOffSettled"].ToString(), ""));
                writtenOffandSettled.DataBind();
            }

            if (Session["AssetClassification"].ToString()==" " || Session["AssetClassification"].ToString()==null || Session["AssetClassification"].ToString() == "")
            {
                DataSet assetClass = Fintrax.LoadAssetClassification();
                assetClassification.DataSource = assetClass;
                assetClassification.DataTextField = "AssValues";
                assetClassification.DataValueField = "AssCode";
                assetClassification.AppendDataBoundItems = true;
                assetClassification.Items.Insert(0, new ListItem(Session["AssetClassification"].ToString(), ""));
                assetClassification.DataBind();
            }
            else
            {
                DataSet assetClass = Fintrax.LoadAssetClassification1(Session["AssetClassification"].ToString());
                assetClassification.DataSource = assetClass;
                assetClassification.DataTextField = "AssValues";
                assetClassification.DataValueField = "AssCode";
                assetClassification.AppendDataBoundItems = true;
                //  assetClassification.Items.Insert(0, new ListItem(Session["AssetClassification"].ToString(), ""));
                assetClassification.DataBind();
            }
           
            valueOfCollateral.Text = Session["CollateralValue"].ToString();

            if (Session["CollateralType"].ToString()==" " || Session["CollateralType"].ToString()==null || Session["CollateralType"].ToString() == "")
            {
                DataSet typeCollateral = Fintrax.LoadTypeOfCollateral();
                collateralType.DataSource = typeCollateral;
                collateralType.DataTextField = "coValues";
                collateralType.DataValueField = "coCode";
                collateralType.AppendDataBoundItems = true;
                collateralType.Items.Insert(0, new ListItem(Session["CollateralType"].ToString(), ""));
                collateralType.DataBind();
            }
            else
            {
                DataSet typeCollateral = Fintrax.LoadTypeOfCollateral1(Session["CollateralType"].ToString());
                collateralType.DataSource = typeCollateral;
                collateralType.DataTextField = "coValues";
                collateralType.DataValueField = "coCode";
                collateralType.AppendDataBoundItems = true;
                //    collateralType.Items.Insert(0, new ListItem(Session["CollateralType"].ToString(), ""));
                collateralType.DataBind();
            }
           
            creditLimit.Text= Session["CreditLimit"].ToString();
            cashLimit.Text = Session["CashLimit"].ToString();
            rateOfInterest.Text= Session["RateOfInterest"].ToString();
            repaymentTenure.Text = Session["RepaymentTenure"].ToString();
            EMIAmount.Text= Session["EMIAmount"].ToString();
            writtenOffTotalAmt.Text = Session["WrittenOffTotalAmount"].ToString();
            writtenOffPrincipalAmt.Text = Session["WrittenOffPrincipalAmount"].ToString();
            settlementAmt.Text = Session["SettledAmount"].ToString();

            if (Session["PaymentFrequency"].ToString()==" " || Session["PaymentFrequency"].ToString()==null || Session["PaymentFrequency"].ToString() == "")
            {
                DataSet payFreq = Fintrax.LoadPaymentFreq(Session["PaymentFrequency"].ToString());
                paymentFreq.DataSource = payFreq;
                paymentFreq.DataTextField = "payValues";
                paymentFreq.DataValueField = "payCode";
                paymentFreq.AppendDataBoundItems = true;
                paymentFreq.Items.Insert(0, new ListItem(Session["PaymentFrequency"].ToString(), ""));
                paymentFreq.DataBind();
            }
            else
            {
                DataSet payFreq = Fintrax.LoadPaymentFreq1(Session["PaymentFrequency"].ToString());
                paymentFreq.DataSource = payFreq;
                paymentFreq.DataTextField = "payValues";
                paymentFreq.DataValueField = "payCode";
                paymentFreq.AppendDataBoundItems = true;
                //   paymentFreq.Items.Insert(0, new ListItem(Session["PaymentFrequency"].ToString(), ""));
                paymentFreq.DataBind();
            }
          
            actualPaymentAmt.Text = Session["ActualPaymentAmount"].ToString();

            if (Session["OccupationCode"].ToString()==" " || Session["OccupationCode"].ToString()==null || Session["OccupationCode"].ToString() == "")
            {
                DataSet occuCode = Fintrax.LoadOccupationCode(Session["OccupationCode"].ToString());
                occupationCode.DataSource = occuCode;
                occupationCode.DataTextField = "occValues";
                occupationCode.DataValueField = "occCode";
                occupationCode.AppendDataBoundItems = true;
                occupationCode.Items.Insert(0, new ListItem(Session["OccupationCode"].ToString(), ""));
                occupationCode.DataBind();
            }
            else
            {
                DataSet occuCode = Fintrax.LoadOccupationCode1(Session["OccupationCode"].ToString());
                occupationCode.DataSource = occuCode;
                occupationCode.DataTextField = "occValues";
                occupationCode.DataValueField = "occCode";
                occupationCode.AppendDataBoundItems = true;
                //  occupationCode.Items.Insert(0, new ListItem(Session["OccupationCode"].ToString(), ""));
                occupationCode.DataBind();
            }
           
            income.Text = Session["Income"].ToString();

            if (Session["NetGrossIncome"].ToString()==" " || Session["NetGrossIncome"].ToString()==null || Session["NetGrossIncome"].ToString() == "")
            {
                DataSet netGrossIncome = Fintrax.LoadNetGrossIncomeIndicator(Session["NetGrossIncome"].ToString());
                netGrossIncomeIndicator.DataSource = netGrossIncome;
                netGrossIncomeIndicator.DataTextField = "grValues";
                netGrossIncomeIndicator.DataValueField = "grCode";
                netGrossIncomeIndicator.AppendDataBoundItems = true;
                netGrossIncomeIndicator.Items.Insert(0, new ListItem(Session["NetGrossIncome"].ToString(), ""));
                netGrossIncomeIndicator.DataBind();
            }
            else
            {
                DataSet netGrossIncome = Fintrax.LoadNetGrossIncomeIndicator1(Session["NetGrossIncome"].ToString());
                netGrossIncomeIndicator.DataSource = netGrossIncome;
                netGrossIncomeIndicator.DataTextField = "grValues";
                netGrossIncomeIndicator.DataValueField = "grCode";
                netGrossIncomeIndicator.AppendDataBoundItems = true;
                // netGrossIncomeIndicator.Items.Insert(0, new ListItem(Session["NetGrossIncome"].ToString(), ""));
                netGrossIncomeIndicator.DataBind();
            }

            if (Session["MonthlyAnnualIncome"].ToString()==" " || Session["MonthlyAnnualIncome"].ToString()==null || Session["MonthlyAnnualIncome"].ToString() == "")
            {
                DataSet monthAnnualIncome = Fintrax.LoadMonthlyAnnualIncome(Session["MonthlyAnnualIncome"].ToString());
                monthlyAnnualIncome.DataSource = monthAnnualIncome;
                monthlyAnnualIncome.DataTextField = "anValues";
                monthlyAnnualIncome.DataValueField = "anCode";
                monthlyAnnualIncome.AppendDataBoundItems = true;
                monthlyAnnualIncome.Items.Insert(0, new ListItem(Session["MonthlyAnnualIncome"].ToString(), ""));
                monthlyAnnualIncome.DataBind();
            }
            else
            {
                DataSet monthAnnualIncome = Fintrax.LoadMonthlyAnnualIncome1(Session["MonthlyAnnualIncome"].ToString());
                monthlyAnnualIncome.DataSource = monthAnnualIncome;
                monthlyAnnualIncome.DataTextField = "anValues";
                monthlyAnnualIncome.DataValueField = "anCode";
                monthlyAnnualIncome.AppendDataBoundItems = true;
                //   monthlyAnnualIncome.Items.Insert(0, new ListItem(Session["MonthlyAnnualIncome"].ToString(), ""));
                monthlyAnnualIncome.DataBind();
            }

            registeredDate.Text = Session["RegisteredDate"].ToString();
        }
    }


    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            string LoanNumber = loanNo.Text;
            string CustomerName = customerName.Text;
            string DateOfBirth = TextBox4.Text;
            string Gender = gender.SelectedItem.Value;
            string IncomeTaxIDNumber = incomeTaxIDNumber.Text;
            string PassportNumber = passportNo.Text;
            string PassportIssueDate = passportIssueDate.Text;
            string PassportExpiryDate = passportExpiryDate.Text;
            string VoterIDNumber = voterIDNo.Text;
            string DrivingLicenseNumber = drivingLicenseNo.Text;
            string DrivingLicenseIssueDate = drivingLicenseIssueDate.Text;
            string DrivingLicenseExpiryDate = drivingLicenseExpiryDate.Text;
            string RationCard = rationCardNo.Text;
            string UniversalIDNO = universalIDNo.Text;
            string AdditionalID1 = additionalID1.Text;
            string AdditionalID2 = additionalID2.Text;

            string TelephoneMobile = telephoneNoMobile.Text;
            string TelephoneResidence = telephoneNoResidence.Text;
            string TelephoneOffice = telephoneNoOffice.Text;
            string ExtensionOffice = extensionOffice.Text;
            string OtherTelephoneNo = otherTelePhoneNo.Text;
            string OtherExtension = otherExtension.Text;
            string Email1 = emailID.Text;
            string Email2 = emailID2.Text;

            string Address1 = address1.Text;
            string State1 = state1.SelectedItem.Value;
            string Pincode1 = pincode1.Text;
            string Category1 = category1.SelectedItem.Value;
            string Residence1 = residence1.SelectedItem.Value;
            string Address2 = address2.Text;
            string State2 = state2.SelectedItem.Value;
            string Pincode2 = pincode2.Text;
            string Category2 = category2.SelectedItem.Value;
            string Residence2 = residence2.SelectedItem.Value;


            string CurrentNewMemberCode = currentNewMemberCode.Text;
            string CurrentNewMemberName = currentNewMemberName.Text;
            string CurrentNewAccountNumber = currentNewAccNo.Text;
            string AccountType = accountType.SelectedItem.Value;
            string OwnershipIndicator = ownershipIndicator.SelectedItem.Value;
            string DateDisbursed = dateOpenedDisbursed.Text;
            string LastPaymentDate = lastPaymentDate.Text;
            string DateClosed = dateClosed.Text;
            string DateReported = dateReported.Text;
            string HighCreditSanctioned = highCreditSanctionedAmt.Text;
            string CurrentBalance = currentBalance.Text;
            string AmountOverdue = amountOverdue.Text;
            string NumberOfDaysPastDue = noOfDaysPastDue.Text;
            string OldMemberCode = oldMemberCode.Text;
            string OldMemberName = oldMemberName.Text;
            string oldAccountNumber = oldMemberAccountNo.Text;
            string OldAccountType = oldAccountType.SelectedItem.Value;
            string OldOwnershipIndicator = oldOwnershipIndicator.SelectedItem.Value;

            string SuitFiledDefaultStatus = wilfulStatus.SelectedItem.Value;
            string WrittenOffSettled = writtenOffandSettled.SelectedItem.Value;
            string AssetClassification = assetClassification.SelectedItem.Value;
            string ValueOfCollateral = valueOfCollateral.Text;
            string TypeOfCollateral = collateralType.SelectedItem.Value;
            string CreditLimit = creditLimit.Text;
            string CashLimit = cashLimit.Text;
            string RateOfInterest = rateOfInterest.Text;
            string Repaymenttenure = repaymentTenure.Text;
            string EMIamount = EMIAmount.Text;
            string WrittenOffTotalAmount = writtenOffTotalAmt.Text;
            string WrittenOffPrincipalAmount = writtenOffPrincipalAmt.Text;
            string SettlementAmount = settlementAmt.Text;
            string PaymentFrequency = paymentFreq.SelectedItem.Value;
            string ActualPayment = actualPaymentAmt.Text;
            string Occupation = occupationCode.SelectedItem.Value;
            string Income = income.Text;
            string NetGrossIncome = netGrossIncomeIndicator.SelectedItem.Value;
            string MonthlyAnnualIncome = monthlyAnnualIncome.SelectedItem.Value;
            string RegisteredDate = registeredDate.Text;
            int cybil = Fintrax.UpdateCbyil(CustomerName, DateOfBirth, Gender, IncomeTaxIDNumber, PassportNumber, PassportIssueDate, PassportExpiryDate, VoterIDNumber, DrivingLicenseNumber, DrivingLicenseIssueDate, DrivingLicenseExpiryDate, RationCard, UniversalIDNO, AdditionalID1, AdditionalID2, TelephoneMobile, TelephoneResidence, TelephoneOffice, ExtensionOffice, OtherTelephoneNo, OtherExtension, Email1, Email2, Address1, State1, Pincode1, Category1, Residence1, Address2, State2, Pincode2, Category2, Residence2, CurrentNewMemberCode, CurrentNewMemberName, CurrentNewAccountNumber, AccountType, OwnershipIndicator, DateDisbursed, LastPaymentDate, DateClosed, DateReported, HighCreditSanctioned, CurrentBalance, AmountOverdue, NumberOfDaysPastDue, OldMemberCode, OldMemberName, oldAccountNumber, OldAccountType, OldOwnershipIndicator, SuitFiledDefaultStatus, WrittenOffSettled, AssetClassification, ValueOfCollateral, TypeOfCollateral, CreditLimit, CashLimit, RateOfInterest, Repaymenttenure, EMIamount, WrittenOffTotalAmount, WrittenOffPrincipalAmount, SettlementAmount, PaymentFrequency, ActualPayment, Occupation, Income, NetGrossIncome, MonthlyAnnualIncome, LoanNumber,RegisteredDate);
            //  Response.Redirect(Request.Url.AbsoluteUri);
            string msg = "Updated Successfully";
            ClientScript.RegisterStartupScript(GetType(), "hwa", "alertk('" + msg + "');", true);
            ClientScript.RegisterStartupScript(GetType(), "hwa", "redirect('" + LoanNumber + "');", true);
        }
        catch (Exception ex)
        {


        }
    }

 }