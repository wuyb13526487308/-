<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="ReportWebLib.Report" %>
<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>日用气量</title>
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/highcharts3.0.5/highcharts.js" type="text/javascript"></script>
    <script src="../Scripts/highcharts3.0.5/modules/exporting.js" type="text/javascript"></script>
    <script src="../Scripts/highslide/highslide-full.min.js" type="text/javascript"></script>
    <script src="../Scripts/highslide/highslide.config.js" type="text/javascript"></script>
    <link href="../Scripts/highslide/highslide.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
     <script type="text/javascript" language="javascript">
         $(function () {
             IotM.CheckLogin();
             //重新给条件页面传递参数
             document.getElementById("menuFrame").src = "ConditionForm.aspx?RID=" + IotM.GetUrlParmter("id");
           
         });  
    </script>
</head>
<body style="height: 100%"> 
    <div id="wrap" class="easyui-layout" fit="true">
        <div id="menuDiv" data-options="region:'west',split:true,border:false,onCollapse:IotM.WindowResize,onExpand:IotM.WindowResize"
            title="报表查询条件" style="width: 242px; height: auto">
            <iframe src="" frameborder="0" scrolling="auto" id="menuFrame" style="width: 100%;
                height: 99%"></iframe>
        </div>
        <div id="mainDiv" data-options="region:'center',split:true,scroll:true" style="width: 100%; height: 100%">
            <iframe src="ReportViewForm.aspx" frameborder="0" scrolling="auto" name="ReportRight" style="width: 100%;
                height: 99%"></iframe>
        </div>
    </div>
</body>
</html>
