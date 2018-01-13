<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportViewForm.aspx.cs"
    Inherits="ReportWebLib.ReportViewForm" %>

<%@ Register Assembly="DevExpress.XtraReports.v12.2.Web, Version=12.2.5.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<!DOCTYPE HTML>
<html>
<head>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ReportToolbar ID="ReportToolbar1" runat="server" ShowDefaultButtons="False" ReportViewerID="ReportViewer1">
                <Items>
                    <dx:ReportToolbarButton ItemKind="Search" />
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton ItemKind="PrintReport" />
                    <dx:ReportToolbarButton ItemKind="PrintPage" />
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton Enabled="False" ItemKind="FirstPage" />
                    <dx:ReportToolbarButton Enabled="False" ItemKind="PreviousPage" />
                    <dx:ReportToolbarLabel ItemKind="PageLabel" />
                    <dx:ReportToolbarComboBox ItemKind="PageNumber" Width="65px">
                    </dx:ReportToolbarComboBox>
                    <dx:ReportToolbarLabel ItemKind="OfLabel" />
                    <dx:ReportToolbarTextBox IsReadOnly="True" ItemKind="PageCount" />
                    <dx:ReportToolbarButton ItemKind="NextPage" />
                    <dx:ReportToolbarButton ItemKind="LastPage" />
                    <dx:ReportToolbarSeparator />
                    <dx:ReportToolbarButton ItemKind="SaveToDisk" />
                    <dx:ReportToolbarButton ItemKind="SaveToWindow" />
                    <dx:ReportToolbarComboBox ItemKind="SaveFormat" Width="70px">
                        <Elements>
                            <dx:ListElement Value="xls" />
                            <dx:ListElement Value="xlsx" />
                            <dx:ListElement Value="pdf" />
                            <dx:ListElement Value="rtf" />
                            <dx:ListElement Value="mht" />
                            <dx:ListElement Value="html" />
                            <dx:ListElement Value="txt" />
                            <dx:ListElement Value="csv" />
                            <dx:ListElement Value="png" />
                        </Elements>
                    </dx:ReportToolbarComboBox>
                </Items>
                <Styles>
                    <LabelStyle>
                        <Margins MarginLeft='3px' MarginRight='3px' />
                    </LabelStyle>
                </Styles>
            </dx:ReportToolbar>
            <dx:ReportViewer ID="ReportViewer1" runat="server">
            </dx:ReportViewer>
            <br />
        </div>
    </form>
</body>
</html>
