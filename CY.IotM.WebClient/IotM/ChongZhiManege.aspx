<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChongZhiManege.aspx.cs" Inherits="CY.IotM.WebClient.IotM.ChongZhiManege" %>

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
    <script src="../Scripts/IotM/IotM.ChongZhiManege.js"></script>

    <script type="text/javascript">

        $(function () {
            IotM.CheckLogin();
            IotM.ChongZhi.LoadDataGrid(false);
            IotM.regvalidatebox("formAdd");
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });
            IotM.Initiate.LoadChongZhiTypeComboBox("CNTopUpType", true, false);
        });
        $.extend($.fn.validatebox.defaults.rules, {
            DayRex: {
                validator: function (value) {
                    var rex = /^\d{2}$/;
                    if (rex.test(value)) {
                        if (parseInt(value, 10) <= 31) {
                            return true;
                        } else {
                            return false;
                        }
                    } else {
                        return false;
                    }
                },
                message: '请输入正确的日期'
            }
        });
        $.extend($.fn.validatebox.defaults.rules, {
            HourRex: {
                validator: function (value) {
                    var rex = /^\d{2}$/;
                    if (rex.test(value)) {
                        if (parseInt(value, 10) <= 59) {
                            return true;
                        } else {
                            return false;
                        }
                    } else {
                        return false;
                    }
                },
                message: '请输入正确的日期'
            }
        });
        $.extend($.fn.validatebox.defaults.rules, {
            ManeyRex: {
                validator: function (value) {
                    var rex = /^(([1-9]{1}\\d*)|([0]{1}))(\\.(\\d){0,2})?$/;
                    if (rex.test(value)) {
                        if (parseInt(value, 10) <= 59) {
                            return true;
                        } else {
                            return false;
                        }
                    } else {
                        return false;
                    }
                },
                message: '请输入正确金额格式'
            }
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
                
                &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="CNUserName" style="width: 260px" />
                &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="CNAdress" style="width: 260px" />
                &nbsp;&nbsp;&nbsp;&nbsp;充值类型&nbsp;&nbsp;<input class="easyui-combobox" type="text" id="CNTopUpType" />

                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.ChongZhi.SerachClick()">查询</a>

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





        <div id="deleteUserDiv" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">

            <div id="tb_deleteUser">
                &nbsp;&nbsp;街道&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_StreetDelete" />
                &nbsp;&nbsp;&nbsp;&nbsp;小区&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Community" />
                &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_AdressDel" style="width: 260px" />
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelectDel" onclick="IotM.User.DeleteSerachClick()">查询</a>
                &nbsp;&nbsp;&nbsp;&nbsp;
      
      
            </div>


            <div id="dataGrid_deleteUser">
            </div>


            <div style="text-align: right; padding-right: 50px; padding-top: 10px">

                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                    id="btnOk_deleteUser">确定选择</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                    id="btnCancel_deleteUser">取消</a>

            </div>

        </div>



        <div id="GetUserDiv" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-search'"
            style="padding: 10px; width: auto">

            <div id="dataGrid_getUser">
            </div>


        </div>


        <div id="GetCommunityDiv" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-search'"
            style="padding: 10px; width: auto">

            <div id="dataGrid_Community">
            </div>


            <div style="text-align: right; padding-right: 50px; padding-top: 10px">

                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                    id="btnOk_Community">确定选择</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                    id="btnCancel_Community">取消</a>

            </div>

        </div>

    </form>
</body>
</html>
