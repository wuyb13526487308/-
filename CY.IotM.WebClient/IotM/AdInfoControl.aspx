<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdInfoControl.aspx.cs" Inherits="CY.IotM.WebClient.AdInfoControl" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>广告管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />

    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>

    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.AdInfo.js" type="text/javascript"></script>



    <script type="text/javascript">


    
        $(function () {
            IotM.CheckLogin();
         
            IotM.AdInfo.LoadControlGrid();

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
          

          
           &nbsp;&nbsp;文件名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_FileName"  />
         

           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.AdInfo.SerachHistoryClick()">查询</a> 
            &nbsp;&nbsp;&nbsp;&nbsp;
          

        </div>
        <div id="dataGrid">
        </div>


       
    </div>

    <div id="GetUserDiv" class="easyui-window"  data-options="modal:true,closed:true,iconCls:'icon-search'"
        style="padding: 10px; width: auto">

            <div id="dataGrid_getUser">
            </div>


    </div>
  
</body>
</html>
