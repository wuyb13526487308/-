<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyReportManage.aspx.cs" Inherits="CY.IotM.WebClient.CompanyReportManage" %>

<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>企业报表查询</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify-v2.1.4/jquery.uploadify.v2.0.3.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify-v2.1.4/swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.CompanyReportManage.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function getCookie_() {
            IotM.GetCookie();
            return IotM.WebCookie;
        }

        $(function () {
            IotM.CheckLogin();
            IotM.CompanyReportManage.LoadCompanyReportDataGrid();
        });

       


         
    </script>
</head>
<body>
    <div id="tb">
     
        <div style="margin: 5px 2px;">
        </div>
        <input class="easyui-searchbox" data-options="prompt:'请输入检索关键字',menu:'#mm',searcher:IotM.CompanyReportManage.SerachClick"
            style="width: 300px"></input>
        <div id="mm" style="width: 120px">
            <div data-options="name:'MenuCode'">
                报表编号</div>
            <div data-options="name:'Name'">
                报表名称</div>
        </div>
    </div>
    <div id="dataGrid">
    </div>
    <form id='formAdd'>
  
    </form>
</body>
</html>
