<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EditCybil.aspx.cs" Inherits="WebSite5_production_EditCybil" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <!-- Meta, title, CSS, favicons, etc. -->
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">

    <title> </title>
    <style>
            #sidebar-menu{
         position: fixed;
         width: 230px;
         margin-top:70px;
        }
         .main {
   
    margin: 13px auto;
}

/* Bootstrap 3 text input with search icon */

.has-search .form-control-feedback {
    right: initial;
    left: 0;
    color: #ccc;
}

.has-search .form-control {
    padding-right: 12px;
    padding-left: 34px;
}

        #searchPro{
            padding:8px 40px;
            border-radius: 25px;
            margin-right:10px;
            font-size:13px;
                text-align:center;
        }

        .title{
           font-size:13px;
      font-style:oblique;
        }

        #img {
            position:fixed;
            background: #04070B;
            text-align: center;
            -webkit-box-shadow: -1px 1px 3px 0px rgba(0,0,0,0.75);
            -moz-box-shadow: -1px 1px 3px 0px rgba(0,0,0,0.75);
            box-shadow: -1px 1px 3px 0px rgba(0,0,0,0.75);
        }


         #update,#head,#insert{
             display:none;
         }

           #success-alert,#danger-alert,#danger-alert1,#menu_toggle,#profileDetails{
            display:none;
        }
           #TextBox5{
               text-align:center;
           }


                      .hint-text {
        float: left;
        margin-top: 10px;
        font-size: 13px;
    }    
	/* Custom checkbox */
	
	/* Modal styles */
	.modal .modal-dialog {
		max-width: 400px;
	}
	.modal .modal-header, .modal .modal-body, .modal .modal-footer {
		padding: 20px 30px;
	}
	.modal .modal-content {
		border-radius: 3px;
	}
	.modal .modal-footer {
		background: #ecf0f1;
		border-radius: 0 0 3px 3px;
	}
    .modal .modal-title {
        display: inline-block;
    }
	.modal .form-control {
		border-radius: 2px;
		box-shadow: none;
		border-color: #dddddd;
	}
	.modal textarea.form-control {
		resize: vertical;
	}
	.modal .btn {
		border-radius: 2px;
		min-width: 100px;
	}	
	.modal form label {
		font-weight: normal;
	}

        #loanNo, #customerName, #TextBox4, #gender, #incomeTaxIDNumber, #passportNo, #passportIssueDate, #passportExpiryDate,
        #voterIDNo, #drivingLicenseNo, #drivingLicenseIssueDate, #rationCardNo, #universalIDNo, #additionalID1, #additionalID2,
        #drivingLicenseExpiryDate, #telephoneNoOffice, #telephoneNoResidence, #telephoneNoMobile,#address1,#address2,#lastPaymentDate,
        #otherExtension,#otherTelePhoneNo,#extensionOffice,#emailID,#emailID2,#residence1,#category1,#pincode1,#state1,#dateOpenedDisbursed,
        #residence2,#category2,#pincode2,#state2,#currentNewMemberCode,#currentNewMemberName,#currentNewAccNo,#accountType,#ownershipIndicator,
        #dateClosed,#dateReported,#highCreditSanctionedAmt,#currentBalance,#amountOverdue,#wilfulStatus,#writtenOffandSettled,#assetClassification,
        #valueOfCollateral,#collateralType,#creditLimit,#noOfDaysPastDue,#oldMemberCode,#oldMemberName,#oldMemberAccountNo,#oldAccountType,
        #oldOwnershipIndicator,#cashLimit,#rateOfInterest,#repaymentTenure,#EMIAmount,#writtenOffTotalAmt,#writtenOffPrincipalAmt,
        #settlementAmt,#paymentFreq,#actualPaymentAmt,#occupationCode,#income,#netGrossIncomeIndicator,#monthlyAnnualIncome,#registeredDate{
            height: 30px;
            font-size: 12px;
        }

    </style>
    <!-- Bootstrap -->
    <link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
  
   
    <link href="../vendors/google-code-prettify/bin/prettify.min.css" rel="stylesheet">

    <!-- Custom styling plus plugins -->
    <link href="../build/css/custom.min.css" rel="stylesheet">
    <script src="jquery-3.2.1.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.2.1.min.js"></script>

    
      <script type="text/javascript">
        function redirect(datam) {
            window.location.href = "EditCybil.aspx?LoanNo="+datam+"";

        }
       
        function alertk(datak) {
            alert(datak);
           
            return false;
        }

      
    </script>
</head>
<body class="nav-md">
   
    <div class="container body">
      <div class="main_container">
        <div class="col-md-3 col-sm-3 col-xs-12 col-lg-3 left_col">
          <div class="left_col scroll-view">
                    <div class="navbar nav_title" style="border-bottom: 2px; height:auto; color: #172D44;" id="img">

                          <img src="../production/images/" class="img-square" alt="" style="margin-top:3px; margin-bottom:5px;"  width="200" height="53" /><br />
                      
                     <!-- <span style="opacity: 0.5; font-size:16px; margin-bottom:150px; font-style:oblique; font-family:'Bookman Old Style'; color:#FCDE97">Karma Group</span>-->
                    </div>

                    <div class="clearfix"></div>

                    <!-- menu profile quick info -->
                  <%--  <div class="profile clearfix">

                        <div class="profile_pic">
                            <img src="images/img.jpg" alt="..." class="img-circle profile_img">
                        </div>
                        <div class="profile_info">
                            <span>Welcome,</span>
                            <h2>
                                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></h2>
                        </div>
                    </div>--%>
              <!-- /menu profile quick info -->

              <br />
              <br />
              <!-- sidebar menu -->
              <div id="sidebar-menu" class="main_menu_side hidden-print main_menu">
                  <div class="menu_section">

                      <h3>MENU</h3>
                      <ul class="nav side-menu">
                          <li><a><i class='fa fa-home'></i>Cybil<span class='fa fa-chevron-down'></span> </a>
                              <ul class='nav child_menu'>
                                  <li><a href="searchLoan.aspx">Insert Cybil</a></li>
                                  <li><a href="searchLoanEdit.aspx">Edit Cybil</a></li>
 <li><a href="UpdateCybil.aspx">Update Cybil</a></li>
                              </ul>
                          </li>

                           <li><a><i class='fa fa-home'></i>Reports<span class='fa fa-chevron-down'></span> </a>
                              <ul class='nav child_menu'>
                                  <li><a href="Extract.aspx">Extract Cybil</a></li>
                                 <li><a href="DueReport.aspx">Due Report</a></li>
 <li><a href="OverdueReport.aspx">Overdue Report</a></li>
 <li><a href="ClientRegister.aspx">Client Register</a></li>
                              </ul>
                          </li>
  <li><a><i class='fa fa-home'></i>Loan Data<span class='fa fa-chevron-down'></span> </a>
                              <ul class='nav child_menu'>
                                  <li><a href="DataOnCollector.aspx">Collector Wise</a></li>
                                
                              </ul>
                          </li>
                      </ul>
                  </div>

              </div>
              <!-- /sidebar menu -->

              <!-- /menu footer buttons -->

              <div class="sidebar-footer hidden-small">
                  <a data-toggle="tooltip" data-placement="top" title="" href="searchLoan.aspx">
                      <span class="glyphicon glyphicon-home" aria-hidden="true"></span>
                  </a>
                  <a data-toggle="" data-placement="top" title="Settings">
                      <span class="glyphicon glyphicon" aria-hidden="true"></span>
                  </a>


                  <a data-toggle="tooltip" data-placement="top" title="">
                      <span class="glyphicon glyphicon" aria-hidden="true"></span>
                  </a>


                  <a data-toggle="tooltip" data-placement="top" title="" href="#">
                      <span class="glyphicon glyphicon-off" aria-hidden="true"></span>
                  </a>
              </div>
              <!-- /menu footer buttons -->
          </div>
        </div>
          <form id="form1" runat="server">
              <!-- top navigation -->
              <div class="top_nav">
                  <div class="nav_menu">
                      <nav>
                          <div class="nav toggle">
                              <a id="menu_toggle"><i class="fa fa-bars"></i></a>
                          </div>

                          <ul class="nav navbar-nav navbar-right">
                              <li class="">
                                  <a href="javascript:;" class="user-profile dropdown-toggle" data-toggle="dropdown" aria-expanded="false">
                                      <!--<img src="images/img.jpg" alt=""/>-->
                                      <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                      <span class=" fa fa-angle-down"></span>
                                  </a>
                                  <ul class="dropdown-menu dropdown-usermenu pull-right">
                                      <li><a href="Profile_Page.aspx">Change Password</a></li>

                                      <li><a href="#addEmployeeModal" data-toggle="modal">Setting</a></li>
                                      <li><a href="logout.aspx"><i class="fa fa-sign-out pull-right"></i>Log Out</a></li>
                                  </ul>
                              </li>

                              <li class="">

                                  <div class="main">
                                  </div>

                              </li>
                          </ul>
                      </nav>
                  </div>
              </div>
              <!-- /top navigation -->

              <!-- page content -->

              <div class="right_col" role="main">
                  <div class="">
                      <div class="clearfix"></div>
                      <div class="row">
                          <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12">
                              <div class="x_panel">
                                  <div class="table-responsive" id="profileDetails">

                                      <table class="table table-striped table-hover" id="task-table2">
                                          <thead>
                                              <tr>
                                                  <th>PROFILE ID</th>
                                                  <th>TITLE</th>
                                                  <th>NAME</th>
                                                  <th>EMAIL</th>
                                                  <th>MOBILE</th>
                                                  <th>TOUR DATE</th>
                                                  <th>GUEST CARD</th>
                                              </tr>
                                          </thead>
                                          <tbody>
                                          </tbody>

                                      </table>

                                  </div>
                                  <div class="x_content" id="x_content">
                                      <div id="addEmployeeModal" class="modal fade">
                                          <div class="modal-dialog">
                                              <div class="modal-content">

                                                  <div class="modal-header">
                                                      <h4 class="modal-title">Admin</h4>
                                                      <!-- <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>-->
                                                  </div>
                                                  <div class="modal-body">
                                                      <div class="container-fluid">

                                                          <div class="table-responsive">
                                                              <div style="overflow: scroll; height: 200px; width: 100%; overflow: auto">
                                                                  <table class="table table-striped table-hover" id="task-table1">
                                                                      <thead>
                                                                          <tr>
                                                                              <th>Name</th>
                                                                          </tr>
                                                                      </thead>
                                                                      <tbody>
                                                                      </tbody>

                                                                  </table>
                                                              </div>
                                                          </div>

                                                      </div>

                                                  </div>
                                                  <div class="modal-footer">
                                                      <input type="button" class="btn btn-default" id="cancel" data-dismiss="modal" value="Cancel" />

                                                  </div>

                                              </div>
                                          </div>
                                      </div>
                                      <div class="alert alert-success" id="success-alert">
                                          <button type="button" class="close" data-dismiss="alert">x</button>
                                          <strong>Success! </strong>

                                      </div>
                                      <div class="alert alert-danger" id="danger-alert">
                                          <button type="button" class="close" data-dismiss="alert">x</button>
                                          <strong>Something went wrong! </strong>

                                      </div>
                                      <div class="alert alert-danger" id="danger-alert1">
                                          <button type="button" class="close" data-dismiss="alert">x</button>
                                          <strong>Enter Details! </strong>

                                      </div>
                                      <div class="container-fluid">
                                          <button type="button" id="insertButton" class="btn btn-primary pull-right btn-block" style="display: none;">Insert</button>
                                          <button type="button" id="view" class="btn btn-primary pull-right btn-block" style="display: none;">View</button>
                                          <div class="row">
                                              <div class="col-md-12 col-xs-12 col-sm-12 col-lg-12 " id="head">
                                                  <br />
                                                  <h3 class="text-center"></h3>
                                              </div>
                                          </div>
                                      </div>
                                      <br />
                                      <br />
                                      <div class="container-fluid">
                                          <div class="panel panel-default">
                                              <div class="panel-heading">
                                                  <label for="sel1" style="color: #73879C;"></label>
                                              </div>
                                          </div>

                                          <div class="row">
                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="LoanNumber">
                                                  <div class="form-group">
                                                      <label for="sel1">Loan No:</label>
                                                      <asp:TextBox ID="loanNo" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-4 col-xs-12 col-sm-4 col-lg-4" id="custName">
                                                  <div class="form-group">
                                                      <label for="sel1">Customer Name:</label>
                                                      <asp:TextBox ID="customerName" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <label for="sel1">Date Of Birth:</label>
                                                  <div class="input-group date" id="datepicker1" data-provide="datepicker">
                                                      <asp:TextBox ID="TextBox4" class="form-control pull-right" runat="server"></asp:TextBox>
                                                      <div class="input-group-addon">
                                                          <span class="glyphicon glyphicon-th"></span>
                                                      </div>
                                                  </div>

                                              </div>

                                              <div class="col-md-2 col-sm-2 col-xs-12 col-lg-2" id="custGender">
                                                  <div class="form-group">
                                                      <label for="sel1">Gender:</label>
                                                      <asp:DropDownList ID="gender" class="form-control pull-right" runat="server"></asp:DropDownList>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="incomeTaxNumber">
                                                  <div class="form-group">
                                                      <label for="sel1">Income Tax ID No:</label>
                                                      <asp:TextBox ID="incomeTaxIDNumber" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                          </div>
                                          <div class="row">
                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="passportNumber">
                                                  <div class="form-group">
                                                      <label for="sel1">Passport No:</label>
                                                      <asp:TextBox ID="passportNo" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <label for="sel1">Passport Issue Date:</label>
                                                  <div class="input-group date" id="datepicker2" data-provide="datepicker">
                                                      <asp:TextBox ID="passportIssueDate" class="form-control pull-right" runat="server"></asp:TextBox>
                                                      <div class="input-group-addon">
                                                          <span class="glyphicon glyphicon-th"></span>
                                                      </div>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <label for="sel1">Passport Expiry Date:</label>
                                                  <div class="input-group date" id="datepicker3" data-provide="datepicker">
                                                      <asp:TextBox ID="passportExpiryDate" class="form-control pull-right" runat="server"></asp:TextBox>
                                                      <div class="input-group-addon">
                                                          <span class="glyphicon glyphicon-th"></span>
                                                      </div>
                                                  </div>

                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="voterIDNumber">
                                                  <div class="form-group">
                                                      <label for="sel1">Voter ID No:</label>
                                                      <asp:TextBox ID="voterIDNo" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>


                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="drivingLicenseNumber">
                                                  <div class="form-group">
                                                      <label for="sel1">Driving Lic No:</label>
                                                      <asp:TextBox ID="drivingLicenseNo" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <label for="sel1">Driving lic Issue Date:</label>
                                                  <div class="input-group date" id="datepicker4" data-provide="datepicker">
                                                      <asp:TextBox ID="drivingLicenseIssueDate" class="form-control pull-right" runat="server"></asp:TextBox>
                                                      <div class="input-group-addon">
                                                          <span class="glyphicon glyphicon-th"></span>
                                                      </div>
                                                  </div>
                                              </div>

                                          </div>

                                          <div class="row">
                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <label for="sel1">Driving lic Expiry Date:</label>
                                                  <div class="input-group date" id="datepicker5" data-provide="datepicker">
                                                      <asp:TextBox ID="drivingLicenseExpiryDate" class="form-control pull-right" runat="server"></asp:TextBox>
                                                      <div class="input-group-addon">
                                                          <span class="glyphicon glyphicon-th"></span>
                                                      </div>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="rationCardNumber">
                                                  <div class="form-group">
                                                      <label for="sel1">Ration Card No:</label>
                                                      <asp:TextBox ID="rationCardNo" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="universalIDNumber">
                                                  <div class="form-group">
                                                      <label for="sel1">Universal ID No:</label>
                                                      <asp:TextBox ID="universalIDNo" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>


                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="additionalIDOne">
                                                  <div class="form-group">
                                                      <label for="sel1">Additional ID 1:</label>
                                                      <asp:TextBox ID="additionalID1" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="additionalIDTwo">
                                                  <div class="form-group">
                                                      <label for="sel1">Additional ID 2:</label>
                                                      <asp:TextBox ID="additionalID2" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                          </div>
                                      </div>
                                      <br />
                                      <div class="container-fluid">
                                          <div class="panel panel-default">
                                              <div class="panel-heading">
                                                  <label for="sel1" style="color: #73879C;"></label>
                                              </div>
                                          </div>

                                          <div class="row">

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="telephoneNumberMobile">
                                                  <div class="form-group">
                                                      <label for="sel1">Tele No Mobile:</label>
                                                      <asp:TextBox ID="telephoneNoMobile" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="telephoneNumberResidence">
                                                  <div class="form-group">
                                                      <label for="sel1">Tele No Residence:</label>
                                                      <asp:TextBox ID="telephoneNoResidence" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="telephoneNumberOffice">
                                                  <div class="form-group">
                                                      <label for="sel1">Tele No Office:</label>
                                                      <asp:TextBox ID="telephoneNoOffice" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="extOffice">
                                                  <div class="form-group">
                                                      <label for="sel1">Extension Office:</label>
                                                      <asp:TextBox ID="extensionOffice" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="otherTelePhoneNumber">
                                                  <div class="form-group">
                                                      <label for="sel1">Other Tele No:</label>
                                                      <asp:TextBox ID="otherTelePhoneNo" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="otherExt">
                                                  <div class="form-group">
                                                      <label for="sel1">Other Extension:</label>
                                                      <asp:TextBox ID="otherExtension" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>
                                          </div>
                                          <br />

                                          <div class="row">
                                              <div class="col-md-4 col-xs-12 col-sm-4 col-lg-4" id="emailIDOne">
                                                  <div class="form-group">
                                                      <label for="sel1">Email ID:</label>
                                                      <asp:TextBox ID="emailID" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-4 col-xs-12 col-sm-4 col-lg-4" id="emailIDTwo">
                                                  <div class="form-group">
                                                      <label for="sel1">Email ID 2:</label>
                                                      <asp:TextBox ID="emailID2" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                          </div>

                                      </div>
                                      <br />
                                      <div class="container-fluid">
                                          <div class="panel panel-default">
                                              <div class="panel-heading">
                                                  <label for="sel1" style="color: #73879C;"></label>
                                              </div>
                                          </div>

                                          <div class="row">
                                              <div class="col-md-4 col-xs-12 col-sm-4 col-lg-4" id="addressOne">
                                                  <div class="form-group">
                                                      <label for="sel1">Address 1:</label>
                                                      <asp:TextBox ID="address1" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-sm-2 col-xs-12 col-lg-2" id="stateOne">
                                                  <div class="form-group">
                                                      <label for="sel1">State 1:</label>
                                                      <asp:DropDownList ID="state1" class="form-control pull-right" runat="server"></asp:DropDownList>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="pincodeOne">
                                                  <div class="form-group">
                                                      <label for="sel1">Pincode 1:</label>
                                                      <asp:TextBox ID="pincode1" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="categoryOne">
                                                  <div class="form-group">
                                                      <label for="sel1">Category 1:</label>
                                                      <asp:DropDownList ID="category1" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="residenceOne">
                                                  <div class="form-group">
                                                      <label for="sel1">Residence 1:</label>
                                                      <asp:DropDownList ID="residence1" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>

                                          </div>
                                          <br />

                                          <div class="row">
                                              <div class="col-md-4 col-xs-12 col-sm-4 col-lg-4" id="address2One">
                                                  <div class="form-group">
                                                      <label for="sel1">Address 2:</label>
                                                      <asp:TextBox ID="address2" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-sm-2 col-xs-12 col-lg-2" id="state2One">
                                                  <div class="form-group">
                                                      <label for="sel1">State 2:</label>
                                                      <asp:DropDownList ID="state2" class="form-control pull-right" runat="server"></asp:DropDownList>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="pincode2One">
                                                  <div class="form-group">
                                                      <label for="sel1">Pincode 2:</label>
                                                      <asp:TextBox ID="pincode2" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="category2One">
                                                  <div class="form-group">
                                                      <label for="sel1">Category 2:</label>
                                                      <asp:DropDownList ID="category2" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="residence2One">
                                                  <div class="form-group">
                                                      <label for="sel1">Residence 2:</label>
                                                      <asp:DropDownList ID="residence2" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>

                                          </div>

                                      </div>

                                      <br />
                                      <div class="container-fluid">
                                          <div class="panel panel-default">
                                              <div class="panel-heading">
                                                  <label for="sel1" style="color: #73879C;"></label>
                                              </div>
                                          </div>

                                          <div class="row">
                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="currNewMemberCode">
                                                  <div class="form-group">
                                                      <label for="sel1">Curr New Memb Code:</label>
                                                      <asp:TextBox ID="currentNewMemberCode" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="currNewMemberName">
                                                  <div class="form-group">
                                                      <label for="sel1">Curr New Memb Name:</label>
                                                      <asp:TextBox ID="currentNewMemberName" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="currNewAccNo">
                                                  <div class="form-group">
                                                      <label for="sel1">Curr New Acc No:</label>
                                                      <asp:TextBox ID="currentNewAccNo" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="accType">
                                                  <div class="form-group">
                                                      <label for="sel1">Account Type:</label>
                                                      <asp:DropDownList ID="accountType" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="ownershipInd">
                                                  <div class="form-group">
                                                      <label for="sel1">Ownership Indicator:</label>
                                                      <asp:DropDownList ID="ownershipIndicator" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <label for="sel1">Date Disbursed:</label>
                                                  <div class="input-group date" id="datepicker6" data-provide="datepicker">
                                                      <asp:TextBox ID="dateOpenedDisbursed" class="form-control pull-right" runat="server"></asp:TextBox>
                                                      <div class="input-group-addon">
                                                          <span class="glyphicon glyphicon-th"></span>
                                                      </div>
                                                  </div>
                                              </div>

                                          </div>
                                      
                                          <div class="row">
                                               <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <label for="sel1">Last Payment Date:</label>
                                                  <div class="input-group date" id="datepicker7" data-provide="datepicker">
                                                      <asp:TextBox ID="lastPaymentDate" class="form-control pull-right" runat="server"></asp:TextBox>
                                                      <div class="input-group-addon">
                                                          <span class="glyphicon glyphicon-th"></span>
                                                      </div>
                                                  </div>
                                              </div>

                                                <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <label for="sel1">Date Closed:</label>
                                                  <div class="input-group date" id="datepicker8" data-provide="datepicker">
                                                      <asp:TextBox ID="dateClosed" class="form-control pull-right" runat="server"></asp:TextBox>
                                                      <div class="input-group-addon">
                                                          <span class="glyphicon glyphicon-th"></span>
                                                      </div>
                                                  </div>
                                              </div>


                                                <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <label for="sel1">Date Reported:</label>
                                                  <div class="input-group date" id="datepicker9" data-provide="datepicker">
                                                      <asp:TextBox ID="dateReported" class="form-control pull-right" runat="server"></asp:TextBox>
                                                      <div class="input-group-addon">
                                                          <span class="glyphicon glyphicon-th"></span>
                                                      </div>
                                                  </div>
                                              </div>

                                               <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="highCreditSancAmt">
                                                  <div class="form-group">
                                                      <label for="sel1">High Credit/Sanc Amt:</label>
                                                     <asp:TextBox ID="highCreditSanctionedAmt" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>

                                                  </div>
                                              </div>

                                               <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="currentBal">
                                                  <div class="form-group">
                                                      <label for="sel1">Current Bal:</label>
                                                      <asp:TextBox ID="currentBalance" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>

                                                  </div>
                                              </div>

                                               <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="amtOverdue">
                                                  <div class="form-group">
                                                      <label for="sel1">Amt Overdue:</label>
                                                     <asp:TextBox ID="amountOverdue" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>

                                                  </div>
                                              </div>

                                          </div>

                                          <div class="row">
                                              
                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">No Of Days Past Due:</label>
                                                      <asp:TextBox ID="noOfDaysPastDue" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Old Member Code:</label>
                                                      <asp:TextBox ID="oldMemberCode" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Old Member Name:</label>
                                                      <asp:TextBox ID="oldMemberName" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Old Acc No:</label>
                                                      <asp:TextBox ID="oldMemberAccountNo" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="oldAccType">
                                                  <div class="form-group">
                                                      <label for="sel1">Old Acc Type:</label>
                                                      <asp:DropDownList ID="oldAccountType" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Old Ownership Ind:</label>
                                                      <asp:DropDownList ID="oldOwnershipIndicator" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>
                                          </div>


                                      </div>

                                      <br />
                                      <div class="container-fluid">
                                          <div class="panel panel-default">
                                              <div class="panel-heading">
                                                  <label for="sel1" style="color: #73879C;"></label>
                                              </div>
                                          </div>
                                          
                                          <div class="row">
                                                <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Suit Filed/Wilful:</label>
                                                      <asp:DropDownList ID="wilfulStatus" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Written Off/Settled:</label>
                                                      <asp:DropDownList ID="writtenOffandSettled" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Asset Classification:</label>
                                                      <asp:DropDownList ID="assetClassification" class="form-control pull-right" runat="server"></asp:DropDownList>

                                                  </div>
                                              </div>

                                               <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Value Collateral:</label>
                                                     <asp:TextBox ID="valueOfCollateral" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>

                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Collateral Type:</label>
                                                      <asp:DropDownList ID="collateralType" class="form-control pull-right" runat="server"></asp:DropDownList>
                                                      
                                                  </div>
                                              </div>

                                               <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Credit Limit:</label>
                                                      <asp:TextBox ID="creditLimit" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                          </div>
                                          <br />
                                          <div class="row">
                                               <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Cash Limit:</label>
                                                      <asp:TextBox ID="cashLimit" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>
                                              
                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Rate Of Interest:</label>
                                                      <asp:TextBox ID="rateOfInterest" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Repayment Tenure:</label>
                                                      <asp:TextBox ID="repaymentTenure" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">EMI Amount:</label>
                                                      <asp:TextBox ID="EMIAmount" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Written Off Total Amt:</label>
                                                      <asp:TextBox ID="writtenOffTotalAmt" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Written Off Pric Amt:</label>
                                                      <asp:TextBox ID="writtenOffPrincipalAmt" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                          </div>
                                          <br />
                                          <div class="row">
                                               <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Settelment Amt:</label>
                                                      <asp:TextBox ID="settlementAmt" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>
                                              
                                                <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Payment Freq:</label>
                                                       <asp:DropDownList ID="paymentFreq" class="form-control pull-right" runat="server"></asp:DropDownList>
                                                  </div>
                                              </div>

                                               <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Actual Payment Amt:</label>
                                                      <asp:TextBox ID="actualPaymentAmt" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>


                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Occupation Code:</label>
                                                      <asp:DropDownList ID="occupationCode" class="form-control pull-right" runat="server"></asp:DropDownList>
                                                  </div>
                                              </div>

                                              <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Income:</label>
                                                      <asp:TextBox ID="income" class="form-control pull-right" placeholder="" runat="server"></asp:TextBox>
                                                  </div>
                                              </div>

                                               <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">Net/Gross Income:</label>
                                                      <asp:DropDownList ID="netGrossIncomeIndicator" class="form-control pull-right" runat="server"></asp:DropDownList>
                                                  </div>
                                              </div>


                                          </div>
                                          <br />
                                          <div class="row">
                                                <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <div class="form-group">
                                                      <label for="sel1">M/A Income:</label>
                                                      <asp:DropDownList ID="monthlyAnnualIncome" class="form-control pull-right" runat="server"></asp:DropDownList>
                                                  </div>
                                              </div>

                                                 <div class="col-md-2 col-xs-12 col-sm-2 col-lg-2" id="">
                                                  <label for="sel1">Reg Date:</label>
                                                  <div class="input-group date" id="datepicker11" data-provide="datepicker">
                                                      <asp:TextBox ID="registeredDate" class="form-control pull-right" runat="server"></asp:TextBox>
                                                      <div class="input-group-addon">
                                                          <span class="glyphicon glyphicon-th"></span>
                                                      </div>
                                                  </div>
                                              </div>
                                          </div>
                                          <br />
                                          <br />

                                           <div class="row">
                                          <div class="col-md-2 col-xs-12 col-sm-8 col-lg-2">
                                              <div class="form-group">
                                              </div>
                                          </div>
                                          <div class="col-md-3 col-xs-12 col-sm-8 col-lg-3">
                                              <div class="form-group">
                                              </div>
                                          </div>
                                          <div class="col-md-2 col-xs-12 col-sm-8 col-lg-2">
                                              <div class="form-group">
                                                  <asp:Button ID="Button1" class="btn btn-success" runat="server" OnClick="Button1_Click" Text="Update" />
                                              </div>
                                          </div>
                                          <div class="col-md-4 col-xs-12 col-sm-8 col-lg-4">
                                              <div class="form-group">
                                                 
                                              </div>
                                          </div>
                                      </div>



                                      </div>

                                  </div>
                              </div>
                          </div>
                      </div>
                  </div>
              </div>

          </form>
          <!-- /page content -->

          <!-- footer content -->
          <footer>
              <div class="pull-right">
              </div>
          <div class="clearfix"></div>
        </footer>
        <!-- /footer content -->
      </div>
    </div>

    <!-- compose -->
    <div class="compose col-md-6 col-xs-12">
      <div class="compose-header">
        New Message
        <button type="button" class="close compose-close">
          <span>×</span>
        </button>
      </div>

      <div class="compose-body">
        <div id="alerts"></div>

        <div class="btn-toolbar editor" data-role="editor-toolbar" data-target="#editor">
          <div class="btn-group">
            <a class="btn dropdown-toggle" data-toggle="dropdown" title="Font"><i class="fa fa-font"></i><b class="caret"></b></a>
            <ul class="dropdown-menu">
            </ul>
          </div>

          <div class="btn-group">
            <a class="btn dropdown-toggle" data-toggle="dropdown" title="Font Size"><i class="fa fa-text-height"></i>&nbsp;<b class="caret"></b></a>
            <ul class="dropdown-menu">
              <li>
                <a data-edit="fontSize 5">
                  <p style="font-size:17px">Huge</p>
                </a>
              </li>
              <li>
                <a data-edit="fontSize 3">
                  <p style="font-size:14px">Normal</p>
                </a>
              </li>
              <li>
                <a data-edit="fontSize 1">
                  <p style="font-size:11px">Small</p>
                </a>
              </li>
            </ul>
          </div>

        

          

         

        <div id="editor" class="editor-wrapper"></div>
      </div>

      <div class="compose-footer">
        <button id="send" class="btn btn-sm btn-success" type="button">Send</button>
      </div>
    </div>
        </div>
    <!-- /compose -->

    <!-- jQuery -->
    <script src="../vendors/jquery/dist/jquery.min.js"></script>
    <!-- Bootstrap -->
    <script src="../vendors/bootstrap/dist/js/bootstrap.min.js"></script>
    <!-- FastClick -->
    <script src="../vendors/fastclick/lib/fastclick.js"></script>
    <!-- NProgress -->
    <script src="../vendors/nprogress/nprogress.js"></script>
    <!-- bootstrap-wysiwyg -->
    <script src="../vendors/bootstrap-wysiwyg/js/bootstrap-wysiwyg.min.js"></script>
    <script src="../vendors/jquery.hotkeys/jquery.hotkeys.js"></script>
    <script src="../vendors/google-code-prettify/src/prettify.js"></script>

    <!-- Custom Theme Scripts -->
    <script src="../build/js/custom.min.js"></script>
  
         <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/css/bootstrap-datepicker.css" rel="stylesheet">  
   
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.5.0/js/bootstrap-datepicker.js"></script> 
               <script type="text/javascript">
           
            $(document).ready(function () {
                
                $('#datepicker1,#datepicker2,#datepicker3,#datepicker4,#datepicker5,#datepicker6,#datepicker7,#datepicker8,#datepicker9').datepicker({
                    format: "dd/mm/yyyy",
                    autoclose: true
                   
                });

                $('#datepicker11').datepicker({
                    format: "dd/mm/yyyy",
                    autoclose: true

                });

            

            });
        </script>
         <script>

            $('#form1').bind('keydown', function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                }
            });
        </script>
    
   <script>
        $(document).ready(function () {
            //var dates="20/01/2019"
            //var newdate = dates.split("/").reverse().join("-");
            //alert(newdate);

            $("#dateReported").change(function () {
                var lastpaymentdate = $("#lastPaymentDate").val();
                var dateReported = $("#dateReported").val();

                var lastpaymentdate1 = lastpaymentdate.split("/").reverse().join("-");
                var dateReported1 = dateReported.split("/").reverse().join("-");

                var startDate = Date.parse(lastpaymentdate1);
                var endDate = Date.parse(dateReported1);
                var timeDiff = endDate - startDate;
                daysDiff = Math.floor(timeDiff / (1000 * 60 * 60 * 24));
                //alert(daysDiff);

                $("#noOfDaysPastDue").val(daysDiff);
            });

        });

    </script>
</body>
</html>
