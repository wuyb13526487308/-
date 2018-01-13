<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MenuManage.aspx.cs" Inherits="CY.IotM.WebClient.MenuManage" %>

<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE html>

<html>
<head>
    <title>菜单管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/easyui1.3.3/detailview.js"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.MenuManage.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            IotM.CheckLogin();

            IotM.MenuManage.LoadMenuDataGrid();
            IotM.Initiate.LoadMenuTypeComboBox('CNType', false, true);
            IotM.Initiate.LoadReportTemplateComboBox('CNRID', false, true);

            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });

            $('#CNType').combobox(
             {
                 onSelect: function (rec) {
                     IotM.MenuManage.InitFatherCode(rec.TypeID);
                 }
             });




        });
    </script>
</head>
<body>
    <div id="tb">
        <a href="javascript:void(0)" menucode="tjcd" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
            onclick="IotM.MenuManage.OpenformAdd()">添加菜单</a>
        <div style="margin: 5px 2px;">
        </div>
        <input class="easyui-searchbox" data-options="prompt:'请输入检索关键字',menu:'#mm',searcher:IotM.MenuManage.SerachClick"
            style="width: 300px"></input>
        <div id="mm" style="width: 120px">
            <div data-options="name:'Name'">
                菜单名称
            </div>
            <div data-options="name:'MenuCode'">
                菜单编号
            </div>
        </div>
    </div>
    <div id="dataGrid">
    </div>
    <div id="wAddMenu" class="easyui-window" title="添加菜单" data-options="modal:true,closed:true,iconCls:'icon-add'"
        style="padding: 10px; width: 300px">
    
        <table>
         
            <tr>
                <td>菜单名称:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNName" default="" required="true"
                        missingmessage="请输入菜单名称" />
                </td>
            </tr>
            <tr>
                <td>菜单编码:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNMenuCode" default="" required="true"
                        missingmessage="请输入菜单编码" />
                </td>
            </tr>

             <tr>
                <td>菜单类型:
                </td>
                <td>
                    <input name="CNType" id="CNType" class="easyui-combobox" />
                </td>
            </tr>


             <tr>
                <td>父菜单:
                </td>
                <td>
                    <input name="CNFatherCode" id="CNFatherCode" class="easyui-combobox" />
                </td>
            </tr>


              <tr id="divReport" style="display:none">
                <td>报表模板:
                </td>
                <td>
                    <input  id="CNRID" class="easyui-combobox" />
                </td>
            </tr>

             <tr>
                <td>菜单序号:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNOrderNum" default=""  />
                </td>
            </tr>



           <tr>
                <td>菜单URL:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNUrlClass"  style="width:200px" default=""  />
                </td>
            </tr>


             <tr>
                <td>图标URL:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNImageUrl"  style="width:200px" default=""  />
                </td>
            </tr>
           
            <tr>
                <td></td>
                <td>
                    <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                        id="btnOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                            id="btnCancel">取消</a>
                </td>
            </tr>
        </table>
    </div>



</body>
</html>
