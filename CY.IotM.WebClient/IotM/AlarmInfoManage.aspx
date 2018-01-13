<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlarmInfoManage.aspx.cs" Inherits="CY.IotM.WebClient.AlarmInfoManage" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>报警信息管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.AlarmInfo.js" type="text/javascript"></script>


 
   
 
    <script type="text/javascript">
    
        $(function () {
            IotM.CheckLogin();


            $("#txtDate1").datebox("setValue", IotM.MyDateformatter(new Date().dateAdd('d', -7)));
            $("#txtTime1").timespinner("setValue", "00:00");
            $("#txtDate2").datebox("setValue", IotM.MyDateformatter(new Date()));
            $("#txtTime2").timespinner("setValue", "23:59");
            IotM.Initiate.LoadAlermItemComboBox("select_Item", false, false);
            IotM.AlarmInfo.LoadDataGrid();
            IotM.regvalidatebox("formAdd");
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });
        });
    </script>
  
</head>
<body>

    <div id="wrap">
        <div id="tb">

              &nbsp;&nbsp;开始时间：<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
            id="txtDate1" style="width: 90px"/><input id="txtTime1" class="easyui-timespinner"
                style="width: 55px;"/>
        &nbsp;&nbsp;截止时间：<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
            id="txtDate2" style="width: 90px"/><input id="txtTime2" class="easyui-timespinner"
                style="width: 55px;"/>

            &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserName"  />
           &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Adress"  style="width:260px" />
           &nbsp;&nbsp;&nbsp;&nbsp;表号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_MeterNo" />
             &nbsp;&nbsp;&nbsp;&nbsp;项目&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Item"  />
           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.AlarmInfo.SerachClick()">查询</a> 
            &nbsp;&nbsp;&nbsp;&nbsp;
          
           
        </div>
        <div id="dataGrid"></div>
        
    </div>
  
</body>
</html>
