<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fintraxr.aspx.cs" Inherits="Fintraxr" %>

<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        From Date:&nbsp;
        <asp:TextBox ID="Date" runat="server"></asp:TextBox>
        <br />
        To Date:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="Date1" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="200px">
        </rsweb:ReportViewer>
    </form>
</body>
</html>
