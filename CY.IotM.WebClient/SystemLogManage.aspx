<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SystemLogManage.aspx.cs"
    Inherits="CY.IotM.WebClient.SystemLogManage" %>

<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>设备管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/highslide/highslide-full.min.js" type="text/javascript"></script>
    <script src="../Scripts/highslide/highslide.config.js" type="text/javascript"></script>
    <link href="../Scripts/highslide/highslide.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.SystemLogManage.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            IotM.CheckLogin();
            var oldValue = $('#select_LogType').combobox('getValue');
            if (oldValue == '') {
                IotM.Initiate.LoadSystemLogTypeComboBox('select_LogType', true, false);
                $('#select_LogType').combobox({
                    onChange: function (newValue, oldValue) {
                        IotM.SystemLogManage.SerachClick();
                    }
                });
            }
            IotM.regvalidatebox("formAdd");
            $("#txtDate1").datebox("setValue", IotM.MyDateformatter(new Date().dateAdd('d', -7)));
            $("#txtTime1").timespinner("setValue","00:00");
            $("#txtDate2").datebox("setValue", IotM.MyDateformatter(new Date()));
            $("#txtTime2").timespinner("setValue", "23:59");
            IotM.SystemLogManage.LoadDataGrid();
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });
        });
    </script>
</head>
<body>
    <div id="tb">
        &nbsp;&nbsp;日志类型：<input id="select_LogType" class="easyui-combobox" />
        &nbsp;&nbsp;开始时间：<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
            id="txtDate1" style="width: 90px"></input><input id="txtTime1" class="easyui-timespinner"
                style="width: 55px;">
        &nbsp;&nbsp;截止时间：<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
            id="txtDate2" style="width: 90px"></input><input id="txtTime2" class="easyui-timespinner"
                style="width: 55px;">
        <div style="margin: 5px 2px;">
        </div>
        &nbsp;&nbsp;<input class="easyui-searchbox" data-options="prompt:'请输入检索关键字',menu:'#mm',searcher:IotM.SystemLogManage.SerachClick"
            style="width: 300px"></input>
        <div id="mm" style="width: 120px">
            <div data-options="name:'OperID'">
                操作员编号
            </div>
            <div data-options="name:'OperName'">
                操作员姓名
            </div>
            <div data-options="name:'Context'">
                备注
            </div>
        </div>
    </div>
    <div id="dataGrid">
    </div>
</body>
</html>
