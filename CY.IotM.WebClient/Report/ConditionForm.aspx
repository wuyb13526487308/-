<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConditionForm.aspx.cs"
    Inherits="ReportWebLib.ConditionForm" %>

<%@ Register Assembly="DevExpress.Web.v12.2, Version=12.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title></title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" enableviewstate="false">
    <div class="demo-info">
        <div class="demo-tip icon-tip">
        </div>
        <div>
            默认条件下只统计当天，或者当月数据，如需统计其他时间段内的数据，请选择或输入不同的日期月份。</div>
    </div>
    <div>
        <asp:Table ID="tabCondition" runat="server" EnableViewState="false">
        </asp:Table>
    </div>
    <br />
    <div align="center">
        <dx:ASPxButton ID="btnQuery" runat="server" Width="60" Text="查询" OnClick="btnQuery_Click"
            EnableViewState="false">
        </dx:ASPxButton>
    </div>
    <br />
    </form>
</body>
</html>
