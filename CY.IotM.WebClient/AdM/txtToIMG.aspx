<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="txtToIMG.aspx.cs" Inherits="CY.IotM.WebClient.AdM.txtToIMG" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
        <asp:Image ID="pbTextView" runat="server" />
    </form>
</body>
</html>
