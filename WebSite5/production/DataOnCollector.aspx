<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataOnCollector.aspx.cs" Inherits="WebSite5_production_DataOnCollector" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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




        .title{
           font-size:13px;
      font-style:oblique;
        }

    
         #update,#directory,#insert,#menu_toggle,#task-table2{
             display:none;
         }

           #success-alert,#danger-alert,#danger-alert1{
            display:none;
        }
           #TextBox5{
               text-align:center;
           }

 
        
    </style>
    <!-- Bootstrap -->
    <link href="../vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet">
    <!-- Font Awesome -->
    <link href="../vendors/font-awesome/css/font-awesome.min.css" rel="stylesheet">
  
   
     <link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet">
     <link href="https://cdn.datatables.net/1.10.19/css/dataTables.bootstrap.min.css" rel="stylesheet">
     <link href="https://cdn.datatables.net/scroller/2.0.0/css/scroller.bootstrap.min.css" rel="stylesheet">
    <link href="../vendors/google-code-prettify/bin/prettify.min.css" rel="stylesheet">

    <!-- Custom styling plus plugins -->
    <link href="../build/css/custom.min.css" rel="stylesheet">
    <script src="jquery-3.2.1.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jQuery/jquery-3.2.1.min.js"></script>
</head>
<body class="nav-md">
   <form id="form1" runat="server">
    <div class="container body">
      <div class="main_container">
        <div class="col-md-3 col-sm-3 col-xs-12 col-lg-3 left_col">
                  <div class="left_col scroll-view">
                    <div class="navbar nav_title" style="border-bottom: 2px; height:auto; color: #172D44;" id="img">

                          <%--<img src=""  alt="" style="margin-top:3px; margin-bottom:5px;"  width="200" height="53" /><br />--%>
                      
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
                              </ul>
                          </li>

                           <li><a><i class='fa fa-home'></i>Reports<span class='fa fa-chevron-down'></span> </a>
                              <ul class='nav child_menu'>
                                  <li><a href="Extract.aspx">Extract Cybil</a></li>
                                
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
                        <a data-toggle="tooltip" data-placement="top" title="" href="#">
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

        <!-- top navigation -->
        <div class="top_nav">
          <div class="nav_menu">
            <nav>
              <div class="nav toggle">
                <a id="menu_toggle"><i class="fa fa-bars"></i></a>
              </div>

              <ul class="nav navbar-nav navbar-right" >
                <li class="" id="val">
               
                  
                </li>
                  <li class="">

                     
                      <div class="main">

                          <div class="form-group has-feedback has-search">
                             <%-- <span class="glyphicon glyphicon-search form-control-feedback"></span>--%>
                              <%--<asp:TextBox ID="TextBox1" runat="server" class="form-control" placeholder="Search for Loan No.."></asp:TextBox>--%>
                              <select name="collector" id="collector" class="form-control pull-right">
                                  <option value="">Select An Option</option>
                                  <option value="Relmon">Relmon</option>
                                  <option value="Valerie Rodrigues">Valerie Rodrigues</option>
                                  <option value="Anupa Naik">Anupa Naik</option>
                                  <option value="AQUIB">AQUIB</option>
                                  <option value="ZAHAD">ZAHAD</option>
                              </select>

                          </div>
                        

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
                                                           <div style="overflow:scroll;height:200px;width:100%;overflow:auto">
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

                      <div class="table-responsive">
                         
                              <table class="table table-striped table-hover" id="task-table2">
                                  <thead>
                                      <tr>
                                         <th>LOAN NO</th>
                                          <th>NAME</th>
                                          <th>LOAN STATUS</th>
                                          <th>EMI VALUE</th>
                                          <th>OVERDUE</th>
                                           <th>LATE FEE</th>
                                            <th>COLLECTOR</th>
                                      </tr>
                                  </thead>
                                  <tbody>
                                  </tbody>
                              </table>
                      </div>


                  </div>

              </div>
            </div>
          </div>
        </div>

            
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
      <script src="https://cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
    <script src="https://cdn.datatables.net/1.10.19/js/dataTables.bootstrap.min.js"></script>
    <script src="https://cdn.datatables.net/scroller/2.0.0/js/dataTables.scroller.min.js "></script>
       
         <script>

            $('#form1').bind('keydown', function (e) {
                if (e.keyCode == 13) {
                    e.preventDefault();
                }
            });
        </script>
    
      

            <script>

                $(document).ready(function () {

                    $("#collector").change(function () {
                    
                        var Name = $("#collector").val();
                    
                        $.ajax({

                            type: 'post',
                            contentType: "application/json; charset=utf-8",
                            url: 'DataOnCollector.aspx/LoadLoanDetailsOnCollector',
                            data: "{'Name':'" + Name + "'}",
                            async: false,
                            success: function (data) {
                                $('#task-table2').DataTable().destroy();
                                $("#task-table2 tbody").empty();
                                $("#task-table2").show();
                                $("#myPager").empty();
                                subJson = JSON.parse(data.d);

                               
                                $.each(subJson, function (key, value) {
                                    $.each(value, function (index1, value1) {

                                        if (value1[0] == "") {
                                            $("#task-table2").hide();
                                        } else {

                                            $("#task-table2 tbody").append("<tr><td>" + value1[0] + "</td><td>" + value1[1] + "</td><td>" + value1[2] + "</td><td>" + value1[3] + "</td><td>" + value1[4] + "</td><td>" + value1[5] + "</td><td>" + value1[6] + "</td></tr>");
                                            

                                        }
                                    });
                                });
                                $("#val").html("<a href='Default.aspx?name="+Name+"'  target='_blank' >Extract</a>");
                               
                                $('#task-table2').DataTable({
                                    deferRender: true,
                                    scrollY: 400,
                                    "bDestroy": true,
                                    "bRetrieve": true
                                 
                                });

                            },
                            error: function () {
                                alert("wrong");
                            }



                        });
                        return false;
                        
                    });

                });

            </script>

       
       </form>
</body>
</html>
