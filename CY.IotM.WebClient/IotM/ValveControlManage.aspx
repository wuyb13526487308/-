<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValveControlManage.aspx.cs" Inherits="CY.IotM.WebClient.ValveControlManage" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>阀门控制管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.ValveControl.js" type="text/javascript"></script>

 
    <script type="text/javascript">
    
        $(function () {
            IotM.CheckLogin();
         
            IotM.ValveControl.LoadDataGrid();

            IotM.Initiate.LoadFaMenStatusComboBox("select_State", true, false);
            IotM.Initiate.LoadFaMenControlTypeComboBox("select_ControlType", true, false);
            $('#dlg').dialog('close');
          
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
           &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserID"  />
           &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserName"  />
           &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Adress"  style="width:260px" />

           &nbsp;&nbsp;&nbsp;&nbsp;控制类型&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_ControlType"  />
           &nbsp;&nbsp;&nbsp;&nbsp;状态&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_State"  />


           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.ValveControl.SerachClick()">查询</a> 
           &nbsp;&nbsp;&nbsp;&nbsp;

 
        </div>
        <div id="dataGrid">
        </div>

        <div id="dlg" class="easyui-dialog" title="控制信息" data-options="iconCls:'icon-save'" style="width:400px;height:200px;padding:10px">
		The dialog content.
	</div>
     </div>
</body>
</html>
