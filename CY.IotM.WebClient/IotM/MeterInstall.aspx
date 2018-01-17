<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeterInstall.aspx.cs" Inherits="CY.IotM.WebClient.UserManage" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>表具安装</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.User.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.Meter.js" type="text/javascript"></script>


 
   
 
    <script type="text/javascript">
    
        $(function () {
            IotM.CheckLogin();
         
            IotM.Meter.LoadDataGrid();

            IotM.Initiate.LoadGasDirectionComboBox('CNDirection', false, true);
            IotM.Initiate.LoadMeterTypeComboBox('CNMeterType', false, true);


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
           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.Meter.SerachClick()">查询</a> 
      
        </div>
        <div id="dataGrid">
        </div>
        <div id="wAdd" class="easyui-window" title="添加燃气用户" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd'>
              
                     <table style="border-spacing:10px;">
                        <tr>
                            <td>户号:</td>
                            <td>
                                <input type="hidden" name="CNCompanyID" default="" />
                               
                                <input class="easyui-validatebox" type="text" name="CNUserID"   disabled="disabled" default="" />
                            </td>
                            <td>户名:</td>
                            <td>
                              <input class="easyui-validatebox" type="text" name="CNUserName"  disabled="disabled" default="" />
                            </td>

                        </tr>
                    
                

                         <tr>
                            <td>地址:</td>
                            <td colspan="3">
                                <input class="easyui-validatebox" id="CNAddress" type="text" name="CNAddress" disabled="disabled" default=""  style="width:362px"/>
                            </td>
                        </tr>

                            

                           <tr>
                            <td>表号:</td>
                            <td>
                             
                                <input class="easyui-validatebox" id="CNMeterNo" type="text" name="CNMeterNo" validtype="regNumLength[14,14]" default="" />
                            </td>
                             <td>表读数:</td>
                            <td>
                              <input class="easyui-numberbox" type="text" id="CNTotalAmount"  name="CNTotalAmount" precision="2"  default="" />
                            </td>

                        </tr>



                         
                         <tr>
                            <td>进气方向:</td>
                            <td>
                             
                                <input class="easyui-combobox" type="text" name="CNDirection" id="CNDirection" default="" />
                            </td>
                             <td>表类型:</td>
                            <td>
                              <input class="easyui-combobox" type="text" name="CNMeterType"  id="CNMeterType" default="" />
                            </td>

                        </tr>


                         <tr>
                            <td>安装日期:</td>
                            <td>

                                <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
                                 id="CNInstallDate" name="CNInstallDate" style="width: 108px"/>

                            </td>
                             <td></td>
                             <td></td>

                        </tr>
                    
                      
                        <tr>

                            <td colspan="4" align="center">
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
