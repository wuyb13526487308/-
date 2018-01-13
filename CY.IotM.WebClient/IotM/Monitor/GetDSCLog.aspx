<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GetDSCLog.aspx.cs" Inherits="CY.IotM.WebClient.IotM.Monitor.GetDSCLog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            Inti();
            $("#select_date").datebox("setValue", new Date().toLocaleDateString())
        });
        function Inti() {
            $.post("../../Handler/Monitor/DSCLog.ashx?AType=list",
                 function (data, textStatus) {
                     if (textStatus == 'success') {
                         var comdata = [];
                         if (data.Result) {
                             var info = eval('(' + data.TxtMessage + ')');
                             comdata = info.rows;
                             $('#com').combobox(
                               {
                                   value: comdata[0].ID,
                                   data: comdata,
                                   valueField: 'ID',
                                   textField: 'Name'
                               }
                           );
                         }
                     }
                 }, "json");
        }
        function myformatter(date) {
            var y = date.getFullYear();
            var m = date.getMonth() + 1;
            var d = date.getDate();
            return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
        }
        function myparser(s) {
            if (!s) return new Date();
            var ss = (s.split('-'));
            var y = parseInt(ss[0], 10);
            var m = parseInt(ss[1], 10);
            var d = parseInt(ss[2], 10);
            if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
                return new Date(y, m - 1, d);
            } else {
                return new Date();
            }
        }
        function SerachClick()
        {  
            var dscid = $('#com').combobox('getValue');// 获取当前选中的值           
            var mac = $("#select_mac").val();
            if (mac == "")
            {
                $.messager.alert("操作提示", "请输入表地址！");
                return;
            }
            var datetime = $("#select_date").datebox("getValue");    // 得到 datebox值;  
            var url = "../../Handler/Monitor/DSCLog.ashx?AType=ONEINFO";
            $('#dataGrid').datagrid({
                title: '应用系统列表',
                iconCls: 'icon-edit',//图标 
                width: 700,
                height: 660,
                nowrap: false,
                striped: true,
                border: true,
                collapsible: false,//是否可折叠的  
                url: url,
                remoteSort: false,
                columns: [[{ field: 'Message', title: '内容', rowspan: 2, width: 580, align: 'center', sortable: true }]],
                singleSelect: false,//是否单选 
                pagination: true,//分页控件 
                rownumbers: true//行号  
                ,queryParams: { dscid: dscid, mac: mac, datetime: datetime}
                  
            });
            var p = $('#dataGrid').datagrid('getPager');
            $(p).pagination({
                pageSize: 20,//每页显示的记录条数，默认为10 
                pageList: [20],//可以设置每页记录条数的列表 
                beforePageText: '第',//页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                /*onBeforeRefresh:function(){
                    $(this).pagination('loading');
                    alert('before refresh');
                    $(this).pagination('loaded');
                }*/
            });
        }
        
         
    </script>
</head>
<body>
   <div id="wrap">

        <div id="tb">
            &nbsp;&nbsp;采集服务器:&nbsp;&nbsp; <input id="com" class="easyui-combobox" name="state" style="width:200px;"/>
            &nbsp;&nbsp;&nbsp;&nbsp;日期&nbsp;&nbsp;<input type="text" class="easyui-datebox" id="select_date" data-options="formatter:myformatter,parser:myparser" />
            &nbsp;&nbsp;&nbsp;&nbsp;表号&nbsp;&nbsp;<input type="text" id="select_mac" style="width: 160px" />

            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="SerachClick()">查询</a> 
        </div>
        <div id="dataGrid">
        </div> 

    </div>
</body>
</html>
