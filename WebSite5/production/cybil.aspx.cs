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

public partial class WebSite5_production_cybil : System.Web.UI.Page
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

    public string getLoanNo()
    {
        string getloanNo = Convert.ToString(Request.QueryString["loanNo"]);
        return getloanNo;

    }

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
           
            string getloanNo = Convert.ToString(Request.QueryString["loanNo"]);
            DataSet ds = Fintrax.LoadCybilDetailsOnLoanNo(getloanNo);
            DataSet dss = Fintrax.overdue(getloanNo);


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
            Session["HighCreditSanctionedAmt"] = ""; Session["CurrentBalance"] = ""; Session["AmtOverdue"] = ""; Session["rateOfInterest"] = "";
            Session["EMIAmount"] = ""; Session["RepaymentTenure"] = ""; Session["LoanStatus"] = ""; Session["State"] = ""; Session["RegisteredDate"] = ""; Session["ActualPaymentAmt"] = "";
            Session["overdue"] = "";

            Session["loanNo"] = ds.Tables[0].Rows[0]["LOANNO"].ToString();
            Session["customerName"] = ds.Tables[0].Rows[0]["ConsumerName"].ToString();
            Session["dateOfBirth"] = ds.Tables[0].Rows[0]["DateofBirth"].ToString();
            Session["gender"] = ds.Tables[0].Rows[0]["Gender"].ToString();
            Session["incomeTaxIDNumber"] = ds.Tables[0].Rows[0]["IncomeTaxIDNumber"].ToString();
            Session["passportNo"] = ds.Tables[0].Rows[0]["passportNumber"].ToString();
            Session["passportIssueDate"] = ds.Tables[0].Rows[0]["passportIssueDate"].ToString();
            Session["passportExpiryDate"] = ds.Tables[0].Rows[0]["passportExpiryDate"].ToString();

            Session["voterIDNo"] = ds.Tables[0].Rows[0]["VoterIDNumber"].ToString();
            Session["drivingLicenseNo"] = ds.Tables[0].Rows[0]["DrivingLicenseNumber"].ToString();
            Session["drivingLicenseIssueDate"] = ds.Tables[0].Rows[0]["DrivingLicenseIssueDate"].ToString();
            Session["drivingLicenseExpiryDate"] = ds.Tables[0].Rows[0]["DrivingLicenseExpiryDate"].ToString();
            Session["RationCardNumber"] = ds.Tables[0].Rows[0]["RationCardNumber"].ToString();
            Session["UniversalIDNumber"]= ds.Tables[0].Rows[0]["UniversalIDNumber"].ToString();
            Session["AdditionalID1"] = ds.Tables[0].Rows[0]["AdditionalID1"].ToString();
            Session["AdditionalID2"] = ds.Tables[0].Rows[0]["AdditionalID2"].ToString();

            Session["TelephoneNoMobile"] = ds.Tables[0].Rows[0]["TelephoneNoMobile"].ToString();
            Session["TelephoneNoResidence"]= ds.Tables[0].Rows[0]["TelephoneNoResidence"].ToString();
            Session["TelephoneNoOffice"] = ds.Tables[0].Rows[0]["TelephoneNoOffice"].ToString();
            Session["ExtensionOffice"] = ds.Tables[0].Rows[0]["ExtensionOffice"].ToString();
            Session["TelephoneNoOther"] = ds.Tables[0].Rows[0]["TelephoneNoOther"].ToString();
            Session["ExtensionOther"] = ds.Tables[0].Rows[0]["ExtensionOther"].ToString();

            Session["EmailID1"] = ds.Tables[0].Rows[0]["EmailID1"].ToString();
            Session["EmailID2"] = ds.Tables[0].Rows[0]["EmailID2"].ToString();
            Session["Address1"]= ds.Tables[0].Rows[0]["Address1"].ToString();
            Session["StateCode1"] = ds.Tables[0].Rows[0]["StateCode1"].ToString();
            Session["PINCode1"] = ds.Tables[0].Rows[0]["PINCode1"].ToString();
            Session["AddressCategory1"] = ds.Tables[0].Rows[0]["AddressCategory1"].ToString();
            Session["ResidenceCode1"] = ds.Tables[0].Rows[0]["ResidenceCode1"].ToString();
            Session["Address2"] = ds.Tables[0].Rows[0]["Address2"].ToString();
            Session["StateCode2"] = ds.Tables[0].Rows[0]["StateCode2"].ToString();
            Session["PINCode2"] = ds.Tables[0].Rows[0]["PINCode2"].ToString();
            Session["AddressCategory2"] = ds.Tables[0].Rows[0]["AddressCategory2"].ToString();
            Session["ResidenceCode2"] = ds.Tables[0].Rows[0]["ResidenceCode2"].ToString();

            Session["CurrentNewMemberCode"] = ds.Tables[0].Rows[0]["CurrentNewMemberCode"].ToString();
            Session["CurrentNewMemberShortName"] = ds.Tables[0].Rows[0]["CurrentNewMemberShortName"].ToString();
            Session["CurrNewAccountNo"] = ds.Tables[0].Rows[0]["CurrNewAccountNo"].ToString();
            Session["AccountType"] = ds.Tables[0].Rows[0]["AccountType"].ToString();
            Session["OwnershipIndicator"] = ds.Tables[0].Rows[0]["OwnershipIndicator"].ToString();

            Session["DateOpenedDisbursed"] = ds.Tables[0].Rows[0]["DateOpenedDisbursed"].ToString();
            Session["DateofLastPayment"] = ds.Tables[0].Rows[0]["DateofLastPayment"].ToString();
            Session["DateClosed"] = ds.Tables[0].Rows[0]["DateClosed"].ToString();
            Session["DateReported"] = ds.Tables[0].Rows[0]["DateReported"].ToString();

            Session["HighCreditSanctionedAmt"] = ds.Tables[0].Rows[0]["HighCreditSanctionedAmt"].ToString();
            Session["CurrentBalance"] = ds.Tables[0].Rows[0]["CurrentBalance"].ToString();
            Session["AmtOverdue"] = ds.Tables[0].Rows[0]["AmtOverdue"].ToString();
            Session["rateOfInterest"] = ds.Tables[0].Rows[0]["RateofInterest"].ToString();
            Session["EMIAmount"] = ds.Tables[0].Rows[0]["EMIAmount"].ToString();
            Session["RepaymentTenure"]= ds.Tables[0].Rows[0]["RepaymentTenure"].ToString();
            Session["LoanStatus"] = ds.Tables[0].Rows[0]["LoanStatus"].ToString();

            Session["State"]= ds.Tables[0].Rows[0]["STATE"].ToString();
            Session["RegisteredDate"] = ds.Tables[0].Rows[0]["RegisteredDate"].ToString();
            Session["ActualPaymentAmt"] = ds.Tables[0].Rows[0]["ActualPaymentAmt"].ToString();
         
            Session["overdue"] = dss.Tables[0].Rows[0]["EMIAMOUNT"].ToString();

            loanNo.Text = Session["loanNo"].ToString();
            customerName.Text = Session["customerName"].ToString();
            TextBox4.Text = Session["dateOfBirth"].ToString();
            DataSet gen = Fintrax.LoadGender(Session["gender"].ToString());
            gender.DataSource = gen;
            gender.DataTextField = "Values";
            gender.DataValueField = "Code";
            gender.AppendDataBoundItems = true;
            gender.Items.Insert(0, new ListItem(Session["gender"].ToString(), ""));
            gender.DataBind();
            incomeTaxIDNumber.Text = Session["incomeTaxIDNumber"].ToString();
            passportNo.Text = Session["passportNo"].ToString();
            passportIssueDate.Text = Session["passportIssueDate"].ToString();
            passportExpiryDate.Text = Session["passportExpiryDate"].ToString();
            voterIDNo.Text = Session["voterIDNo"].ToString();
            drivingLicenseNo.Text = Session["drivingLicenseNo"].ToString();
            drivingLicenseIssueDate.Text = Session["drivingLicenseIssueDate"].ToString();
            drivingLicenseExpiryDate.Text= Session["drivingLicenseExpiryDate"].ToString();
            rationCardNo.Text= Session["RationCardNumber"].ToString();
            universalIDNo.Text= Session["UniversalIDNumber"].ToString();
            additionalID1.Text = Session["AdditionalID1"].ToString();
            additionalID2.Text = Session["AdditionalID2"].ToString();

            telephoneNoMobile.Text = Session["TelephoneNoMobile"].ToString();
            telephoneNoResidence.Text = Session["TelephoneNoResidence"].ToString();
            telephoneNoOffice.Text = Session["TelephoneNoOffice"].ToString();
            extensionOffice.Text = Session["ExtensionOffice"].ToString();
            otherTelePhoneNo.Text = Session["TelephoneNoOther"].ToString();
            otherExtension.Text = Session["ExtensionOther"].ToString();
            emailID.Text = Session["EmailID1"].ToString();
            emailID2.Text= Session["EmailID2"].ToString();
            address1.Text= Session["Address1"].ToString();

            DataSet state = Fintrax.LoadStateCode(Session["State"].ToString());
            state1.DataSource = state;
            state1.DataTextField = "Values";
            state1.DataValueField = "Code";
            state1.AppendDataBoundItems = true;
            state1.Items.Insert(0, new ListItem(Session["State"].ToString(), ""));
            state1.DataBind();

            pincode1.Text= Session["PINCode1"].ToString();


            DataSet addCategory = Fintrax.LoadAddressCategory(Session["AddressCategory1"].ToString());
            category1.DataSource = addCategory;
            category1.DataTextField = "addValues";
            category1.DataValueField = "addCode";
            category1.AppendDataBoundItems = true;
           // category1.Items.Insert(0, new ListItem(Session["AddressCategory1"].ToString(), ""));
            category1.DataBind();


            DataSet resiCode = Fintrax.LoadResidenceCode(Session["ResidenceCode1"].ToString());
            residence1.DataSource = resiCode;
            residence1.DataTextField = "resValues";
            residence1.DataValueField = "resCode";
            residence1.AppendDataBoundItems = true;
          //  residence1.Items.Insert(0, new ListItem(Session["ResidenceCode1"].ToString(), ""));
            residence1.DataBind();

            address2.Text = Session["Address2"].ToString();
            DataSet states2 = Fintrax.LoadStateCode(Session["StateCode2"].ToString());
            state2.DataSource = states2;
            state2.DataTextField = "Values";
            state2.DataValueField = "Code";
            state2.AppendDataBoundItems = true;
            state2.Items.Insert(0, new ListItem(Session["StateCode2"].ToString(), ""));
            state2.DataBind();
            pincode1.Text = Session["PINCode1"].ToString();
            DataSet addCategory2 = Fintrax.LoadAddressCategory(Session["AddressCategory2"].ToString());
            category2.DataSource = addCategory2;
            category2.DataTextField = "addValues";
            category2.DataValueField = "addCode";
            category2.AppendDataBoundItems = true;
            category2.Items.Insert(0, new ListItem(Session["AddressCategory2"].ToString(), ""));
            category2.DataBind();
            DataSet resiCode2 = Fintrax.LoadResidenceCode(Session["ResidenceCode2"].ToString());
            residence2.DataSource = resiCode2;
            residence2.DataTextField = "resValues";
            residence2.DataValueField = "resCode";
            residence2.AppendDataBoundItems = true;
            residence2.Items.Insert(0, new ListItem(Session["ResidenceCode2"].ToString(), ""));
            residence2.DataBind();

            currentNewMemberCode.Text = Session["CurrentNewMemberCode"].ToString();
            currentNewMemberName.Text = Session["CurrentNewMemberShortName"].ToString();
            currentNewAccNo.Text = Session["loanNo"].ToString();

            DataSet accType = Fintrax.LoadAccountType(Session["AccountType"].ToString());
            accountType.DataSource = accType;
            accountType.DataTextField = "AValues";
            accountType.DataValueField = "ACode";
            accountType.AppendDataBoundItems = true;
            //accountType.Items.Insert(0, new ListItem(Session["AccountType"].ToString(), ""));
            accountType.DataBind();

            DataSet ownershipInd = Fintrax.LoadOwnershipIndicator(Session["OwnershipIndicator"].ToString());
            ownershipIndicator.DataSource = ownershipInd;
            ownershipIndicator.DataTextField = "OValues";
            ownershipIndicator.DataValueField = "OCode";
            ownershipIndicator.AppendDataBoundItems = true;
          //  ownershipIndicator.Items.Insert(0, new ListItem(Session["OwnershipIndicator"].ToString(), ""));
            ownershipIndicator.DataBind();

            dateOpenedDisbursed.Text= Session["DateOpenedDisbursed"].ToString();
            lastPaymentDate.Text= Session["DateofLastPayment"].ToString();
            dateClosed.Text = Session["DateClosed"].ToString();
            dateReported.Text= Session["DateReported"].ToString();

            highCreditSanctionedAmt.Text= Session["HighCreditSanctionedAmt"].ToString();
            currentBalance.Text= Session["CurrentBalance"].ToString();
            amountOverdue.Text= Session["overdue"].ToString();

            rateOfInterest.Text = Session["rateOfInterest"].ToString();
            EMIAmount.Text = Session["EMIAmount"].ToString();
            repaymentTenure.Text= Session["RepaymentTenure"].ToString();

            registeredDate.Text = Session["RegisteredDate"].ToString();
            actualPaymentAmt.Text = Session["ActualPaymentAmt"].ToString();

           

            DataSet oldAccType = Fintrax.LoadAccountType("");
            oldAccountType.DataSource = oldAccType;
            oldAccountType.DataTextField = "AValues";
            oldAccountType.DataValueField = "ACode";
            oldAccountType.AppendDataBoundItems = true;
            oldAccountType.Items.Insert(0, new ListItem("", ""));
            oldAccountType.DataBind();

            DataSet oldOwnershipInd = Fintrax.LoadOwnershipIndicator("");
            oldOwnershipIndicator.DataSource = ownershipInd;
            oldOwnershipIndicator.DataTextField = "OValues";
            oldOwnershipIndicator.DataValueField = "OCode";
            oldOwnershipIndicator.AppendDataBoundItems = true;
            oldOwnershipIndicator.Items.Insert(0, new ListItem("", ""));
            oldOwnershipIndicator.DataBind();

            DataSet Wilful = Fintrax.LoadWilful();
            wilfulStatus.DataSource = Wilful;
            wilfulStatus.DataTextField = "WLValues";
            wilfulStatus.DataValueField = "WLCode";
            wilfulStatus.AppendDataBoundItems = true;
         //   wilfulStatus.Items.Insert(0, new ListItem("", ""));
            wilfulStatus.DataBind();


            if (Session["LoanStatus"].ToString()=="Closed")
            {
                DataSet writtenOffSettled = Fintrax.LoadWrittenOffSettled("");
                writtenOffandSettled.DataSource = writtenOffSettled;
                writtenOffandSettled.DataTextField = "WValues";
                writtenOffandSettled.DataValueField = "WCode";
                writtenOffandSettled.AppendDataBoundItems = true;
                //writtenOffandSettled.Items.Insert(0, new ListItem("", ""));
                writtenOffandSettled.DataBind();
            }
            else if (Session["LoanStatus"].ToString() == "Cancelled")
            {
                DataSet writtenOffSettled = Fintrax.LoadWrittenOffSettledcancelled("");
                writtenOffandSettled.DataSource = writtenOffSettled;
                writtenOffandSettled.DataTextField = "WValues";
                writtenOffandSettled.DataValueField = "WCode";
                writtenOffandSettled.AppendDataBoundItems = true;
                //writtenOffandSettled.Items.Insert(0, new ListItem("", ""));
                writtenOffandSettled.DataBind();
            }
            else
            {

                DataSet writtenOffSettled = Fintrax.LoadWrittenOffSettled("");
                writtenOffandSettled.DataSource = writtenOffSettled;
                writtenOffandSettled.DataTextField = "WValues";
                writtenOffandSettled.DataValueField = "WCode";
                writtenOffandSettled.AppendDataBoundItems = true;
                writtenOffandSettled.Items.Insert(0, new ListItem("", ""));
                writtenOffandSettled.DataBind();
            }

          

            DataSet assetClass = Fintrax.LoadAssetClassification();
            assetClassification.DataSource = assetClass;
            assetClassification.DataTextField = "AssValues";
            assetClassification.DataValueField = "AssCode";
            assetClassification.AppendDataBoundItems = true;
           // assetClassification.Items.Insert(0, new ListItem("", ""));
            assetClassification.DataBind();

            DataSet typeCollateral = Fintrax.LoadTypeOfCollateral();
            collateralType.DataSource = typeCollateral;
            collateralType.DataTextField = "coValues";
            collateralType.DataValueField = "coCode";
            collateralType.AppendDataBoundItems = true;
           // collateralType.Items.Insert(0, new ListItem("", ""));
            collateralType.DataBind();

            DataSet payFreq = Fintrax.LoadPaymentFreq("");
            paymentFreq.DataSource = payFreq;
            paymentFreq.DataTextField = "payValues";
            paymentFreq.DataValueField = "payCode";
            paymentFreq.AppendDataBoundItems = true;
          //  paymentFreq.Items.Insert(0, new ListItem("", ""));
            paymentFreq.DataBind();

            DataSet occuCode = Fintrax.LoadOccupationCode("");
            occupationCode.DataSource = occuCode;
            occupationCode.DataTextField = "occValues";
            occupationCode.DataValueField = "occCode";
            occupationCode.AppendDataBoundItems = true;
           // occupationCode.Items.Insert(0, new ListItem("", ""));
            occupationCode.DataBind();

            DataSet netGrossIncome = Fintrax.LoadNetGrossIncomeIndicator("");
            netGrossIncomeIndicator.DataSource = netGrossIncome;
            netGrossIncomeIndicator.DataTextField = "grValues";
            netGrossIncomeIndicator.DataValueField = "grCode";
            netGrossIncomeIndicator.AppendDataBoundItems = true;
            netGrossIncomeIndicator.Items.Insert(0, new ListItem("", ""));
            netGrossIncomeIndicator.DataBind();

            DataSet monthAnnualIncome = Fintrax.LoadMonthlyAnnualIncome("");
            monthlyAnnualIncome.DataSource = monthAnnualIncome;
            monthlyAnnualIncome.DataTextField = "anValues";
            monthlyAnnualIncome.DataValueField = "anCode";
            monthlyAnnualIncome.AppendDataBoundItems = true;
            monthlyAnnualIncome.Items.Insert(0, new ListItem("", ""));
            monthlyAnnualIncome.DataBind();

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
            string State1s = state1.SelectedItem.Value;
            string State1="" ;
            if (State1s == "" || State1s == null)
            {
                State1 = Fintrax.getCodeOnState(state1.SelectedItem.Text);
            }else
            {
                State1= state1.SelectedItem.Value;
            }
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

            int check = Fintrax.checkLoanNumber(LoanNumber);
            if (check == 0)
            {
                string msg = "Inserted Successfully";
                int cybil = Fintrax.InsertCybil(CustomerName, DateOfBirth, Gender, IncomeTaxIDNumber, PassportNumber, PassportIssueDate, PassportExpiryDate, VoterIDNumber, DrivingLicenseNumber, DrivingLicenseIssueDate, DrivingLicenseExpiryDate, RationCard, UniversalIDNO, AdditionalID1, AdditionalID2, TelephoneMobile, TelephoneResidence, TelephoneOffice, ExtensionOffice, OtherTelephoneNo, OtherExtension, Email1, Email2, Address1, State1, Pincode1, Category1, Residence1, Address2, State2, Pincode2, Category2, Residence2, CurrentNewMemberCode, CurrentNewMemberName, CurrentNewAccountNumber, AccountType, OwnershipIndicator, DateDisbursed, LastPaymentDate, DateClosed, DateReported, HighCreditSanctioned, CurrentBalance, AmountOverdue, NumberOfDaysPastDue, OldMemberCode, OldMemberName, oldAccountNumber, OldAccountType, OldOwnershipIndicator, SuitFiledDefaultStatus, WrittenOffSettled, AssetClassification, ValueOfCollateral, TypeOfCollateral, CreditLimit, CashLimit, RateOfInterest, Repaymenttenure, EMIamount, WrittenOffTotalAmount, WrittenOffPrincipalAmount, SettlementAmount, PaymentFrequency, ActualPayment, Occupation, Income, NetGrossIncome, MonthlyAnnualIncome, LoanNumber,RegisteredDate);
                ClientScript.RegisterStartupScript(GetType(), "hwa", "alertk('" + msg+"');", true);
              
            }
            else
            {
                Response.Redirect("EditCybil.aspx?LoanNo=" + LoanNumber + "");
            }
        }
        catch (Exception ex)
        {
            string msg = "Something Went Wrong";
            ClientScript.RegisterStartupScript(GetType(), "hwa", "error('" + msg + "');", true);
           // Response.Redirect("searchLoan.aspx");
        }
        
    }

 }