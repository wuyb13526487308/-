<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MeterGasBillManage.aspx.cs" Inherits="CY.IotM.WebClient.MeterGasBillManage" %>


<!DOCTYPE html>
<html>
<head>
    <title>用气账单管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.MeterGasBill.js" type="text/javascript"></script>


 
    <script type="text/javascript">
    

        $(function () {
            IotM.CheckLogin();
         
            IotM.MeterGasBill.LoadDataGrid();
            IotM.regvalidatebox("formAdd");
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });

            IotM.Initiate.LoadComboxGridPriceType("CNPriceType", true, false);

            $("#select_Date").datebox({
                onSelect: function (date) {
                    $("#select_Date").datebox("setValue", date.getFullYear() + (date.getMonth() + 1).toString().padLeft(2, '0'));
                }
            });

            $("#CNJieSuanDate").datebox({
                onSelect: function (date) {
                    $("#CNJieSuanDate").datebox("setValue", date.getFullYear() + (date.getMonth() + 1).toString().padLeft(2, '0'));
                }
            });

            $("#CNExportDate").datebox({
                onSelect: function (date) {
                    $("#CNJieSuanDate").datebox("setValue", date.getFullYear() + (date.getMonth() + 1).toString().padLeft(2, '0'));
                }
            });

            var date = new Date();
            $("#CNJieSuanDate").datebox("setValue", date.getFullYear() + (date.getMonth() + 1).toString().padLeft(2, '0'));
            $("#select_Date").datebox("setValue", date.getFullYear() + (date.getMonth() + 1).toString().padLeft(2, '0'));
            $("#CNExportDate").datebox("setValue", date.getFullYear() + (date.getMonth() + 1).toString().padLeft(2, '0'));

        });
    </script>
  
</head>
<body>

    <div id="wrap">
        <div id="tb">


             &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserID" />
            &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserName" />
            &nbsp;&nbsp;&nbsp;&nbsp;结算月份&nbsp;&nbsp;

           <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"  id="select_Date"/>
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.MeterGasBill.SerachClick()">查询</a>

                &nbsp;&nbsp;&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" id="btnJieSuan" onclick="IotM.MeterGasBill.OpenformAdd()">气费结算</a>

              &nbsp;&nbsp;&nbsp;&nbsp;
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-print'" id="btnExport" onclick="IotM.MeterGasBill.OpenformExport()">导出数据</a>
     
        </div>


        <div id="dataGrid">
        </div>

        <div id="wAdd" class="easyui-window" title="气费结算" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd'>
              
                  <table style="border-spacing:10px; ">
                      
                        <tr>
                            <td>价格类型:</td>
                            <td colspan="3">
                               <input class="easyui-combogrid" type="text" name="CNPriceType" style ="width:320px"  id ="CNPriceType" style ="width:300px" default=""  />
                            </td>
                        </tr>

                          <tr>
                            <td>结算月份:</td>
                            
                            <td colspan="3">
                              <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"  id="CNJieSuanDate"/>
                            </td>
                        </tr>


                        <tr id="btnArea">
                            <td colspan="4" align="center">
                                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel">取消</a>
                            </td>
                        </tr>
                  
                    </table>

                        <div id="progressbar" class="easyui-progressbar" style="width:400px;display:none"></div>
             
            </form>
        </div>


        <div id="wDownDiv" class="easyui-window" title="气费结算" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">

            <table style="border-spacing:10px; ">

                <tr>
                     <td>
                            <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"  id="CNExportDate"/>
                     </td>
                
                </tr>

                <tr>
                    <td>
                            <input type="radio" name="download" id="defalutRadio"  value="1" />导出excel文件
                    </td>

                    <td>
                           <input type="radio" name="download"  value="2" />导出到收费系统
                    </td>
    
                </tr>

                  <tr>
                    <td colspan="2" align="center">
                        <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                            id="btnOkExport">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                id="btnCancelExport">取消</a>
                    </td>
                </tr>

            </table>

          
        </div>


    </div>
  
</body>
</html>
