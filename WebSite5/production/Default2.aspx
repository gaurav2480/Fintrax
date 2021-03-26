<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="WebSite5_production_Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <br />Paging Enabled?

<asp:RadioButtonList ID="rbPaging" runat="server">

<asp:ListItem  Text = "Yes" Value = "True" Selected = "True">

</asp:ListItem>

<asp:ListItem  Text = "No" Value = "False"></asp:ListItem>

</asp:RadioButtonList>

<br />

Export Preference

<asp:RadioButtonList ID="rbPreference" runat="server">

      <asp:ListItem  Text = "Vertical" Value = "1" Selected = "True">

</asp:ListItem>

      <asp:ListItem  Text = "Horizontal" Value = "2"></asp:ListItem>

</asp:RadioButtonList>

        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
