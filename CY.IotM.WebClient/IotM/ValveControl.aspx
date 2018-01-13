<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ValveControl.aspx.cs" Inherits="CY.IotM.WebClient.ValveControl" %>


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
         
            IotM.ValveControl.LoadDataGridFaMen();


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
           &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserID"  />
           &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserName"  />
           &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Adress"  style="width:260px" />

           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.ValveControl.SerachClick()">查询</a> 
           &nbsp;&nbsp;&nbsp;&nbsp;
           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" id="btnKaiFa" onclick="IotM.ValveControl.KaiFaFormOpen()">开阀</a> 
           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" id="btnGuanFa" onclick="IotM.ValveControl.GuanFaFormOpen()">关阀</a> 
 
        </div>
        <div id="dataGrid">
        </div>



          <div id="wAdd_KaiFa" class="easyui-window" title="开阀操作申请" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd'>
              
                    <table style="border-spacing:10px; ">
                        <tr>
                            <td>共选择:</td>
                            <td>
                                
                                <input class="easyui-validatebox" type="text" id="UserNum"  disabled="disabled" />    &nbsp;&nbsp;&nbsp;&nbsp;户
                            </td>
                        </tr>
                        <tr>
                             
                            <td>用户列表:</td>
                            <td>
                                <div id="dataGrid_list"></div>
                             
                            </td>

                        </tr>
                    
                        <tr>
                            <td id="FaMenContext">开阀说明:</td>
                            <td>
                                <textarea id="KaiFaContext"  style="height: 80px;width:422px"></textarea>
                            </td>
                        </tr>

                        <tr>

                            <td colspan="2" align="center">
                                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel">取消</a>
                            </td>
                        </tr>

                    </table>
                       
             
            </form>
        </div>


     </div>
</body>
</html>
