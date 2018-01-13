<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyRightManage.aspx.cs"
    Inherits="CY.IotM.WebClient.CompanyRightManage" %>

<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>操作员管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.CompanyRightManage.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            IotM.CheckLogin();
            IotM.CompanyRightManage.LoadCompanyRightDataGrid();
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
            <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
                menucode="tjqxz" onclick="IotM.CompanyRightManage.OpenformAdd()">添加权限组</a>
            <div style="margin: 5px 2px;">
            </div>
        </div>
        <div id="dataGrid">
        </div>
        <div id="wAddCompanyRight" class="easyui-window" title="添加权限组" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px;">
            <form id='formAdd'>
            <table>
                <tr>
                    <td>
                        权限组编码:
                    </td>
                    <td>
                        <input type="hidden" name="CNCompanyID" default="" />
                        <input class="easyui-validatebox" type="text" name="CNRightCode" default="" edit="false"
                            required="true" validtype='rightCode' />
                    </td>                   
                </tr>
                <tr>
                    <td>
                        权限组名称:
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="CNRightName" default="" required="true"
                            missingmessage="请输入权限组名称" />
                    </td>
                </tr>
                <tr>
                    <td>
                        备注:
                    </td>
                    <td>
                        <textarea name="CNContext" default="" style="height: 60px;"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>
                        选择菜单
                    </td>
                    <td>
                        <div id="treeMenuCode">
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
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
