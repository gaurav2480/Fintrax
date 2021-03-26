<%@ Page Language="C#" AutoEventWireup="true" CodeFile="flybuygoa.aspx.cs" Inherits="WebSite5_production_Inhousesumm" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">


*, :after, :before {
    box-sizing: border-box;
}

*{-webkit-box-sizing:border-box;-moz-box-sizing:border-box;box-sizing:border-box}*,:after,:before{color:#000!important;text-shadow:none!important;background:0 0!important;-webkit-box-shadow:none!important;box-shadow:none!important}

h2 {
    font-size: 18px;
    font-weight: 400;
}

.h1, .h2, .h3, h1, h2, h3 {
    margin-top: 10px;
    margin-bottom: 10px;
}

.h2,h2{font-size:30px}.h1,.h2,.h3,h1,h2,h3{margin-top:20px;margin-bottom:10px}.h1,.h2,.h3,.h4,.h5,.h6,h1,h2,h3,h4,h5,h6{font-family:inherit;font-weight:500;line-height:1.1;color:inherit}h2,h3{page-break-after:avoid}table{background-color:transparent}table{border-spacing:0;border-collapse:collapse}td,th{padding:0}

td span {
    line-height: 28px;
}

button,select{text-transform:none}

    .btn.active.focus, .btn.active:focus, .btn.focus, .btn:active.focus, .btn:active:focus, .btn:focus, :active, :focus, :visited, a, a:active, a:focus, a:visited {
        outline: 0;
    }

a {
    color: #5A738E;
}

    .detail a, .expand, .jqstooltip, .paging_full_numbers a:hover, .site_title:focus, .site_title:hover, a, a:focus, a:hover {
        text-decoration: none;
    }

a{color:#337ab7;text-decoration:none}a,a:visited{text-decoration:underline}a{background-color:transparent}
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <p>
            Date</p>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <p>
            Venue</p>
        <p>
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        </p>
        <p>
            Group</p>
        <p>
            <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        </p>
        <p>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" style="height: 26px" />
                             <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

                            </p>
                          <rsweb:ReportViewer ID="ReportViewer2" runat="server" Height="600px" Width="916px">
            </rsweb:ReportViewer>
                           </form>
</body>
</html>
