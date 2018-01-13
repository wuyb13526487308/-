<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CZDuiLie.aspx.cs" Inherits="CY.IotM.WebClient.IotM.CZDuiLie" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>充值查询</title>

    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.CZDuiLie.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.ChongZhiManege.js"></script>

    <script type="text/javascript">
        $(function () {
            IotM.CheckLogin();
            IotM.CZDuiLie.LoadDataGridC();
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
    <form id="form1" runat="server">
        <div id="wrap">
            <div id="tb">
                &nbsp;&nbsp;充值日期&nbsp;&nbsp;
                <input id="hidType" type="hidden" />
            <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
                id="select_RegisterDateS" style="width: 90px" />至<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
                id="select_RegisterDateE" style="width: 90px" />
                
                &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="CNUserName" />
                &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="CNAdress" style="width: 260px" />

                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.CZDuiLie.SerachClick()">查询</a>

            </div>
            <div id="dataGrid">
            </div>

             <div id="wAdd" class="easyui-window" title="取消充值原因" data-options="modal:true,closed:true,iconCls:'icon-add'"
                style="padding: 10px; width: auto">
                <form id='formAdd'>

                    <table>

                      
                        <tr>
                            <td>取消原因:
                            </td>
                            <td>
                                <textarea id="CNContext" style="width: 260px"></textarea>
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
    </form>

</body>
</html>
