<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserManage.aspx.cs" Inherits="CY.IotM.WebClient.UserManage" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>档案管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/easyui1.3.3/detailview.js"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.User.js" type="text/javascript"></script>





    <script type="text/javascript">

        $(function () {
            IotM.CheckLogin();

            IotM.User.LoadDataGridView();

            IotM.regvalidatebox("formAdd");

            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });


            IotM.Initiate.LoadStreetComboBox('CNStreet', false, true);
            $('#CNStreet').combobox(
            {
                onSelect: function (rec) {
                    IotM.Initiate.LoadCommunityComboBox('CNCommunity', false, true, rec.ID);
                    IotM.User.AdressChange();
                }
            });
            IotM.Initiate.LoadCommunityComboBox('CNCommunity', false, true, $('#CNStreet').combobox("getValue"));
            $('#CNCommunity').combobox(
            {
                onSelect: function (rec) { IotM.User.AdressChange(); }
            });


        });

    </script>

</head>
<body>

    <div id="wrap">
        <div id="tb">
            &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserID" />
            &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserName" />
            &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Adress" style="width: 260px" />
            &nbsp;&nbsp;&nbsp;&nbsp;表号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_MeterNo" />
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.User.SerachClick()">查询</a>
            &nbsp;&nbsp;&nbsp;&nbsp;
           <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" menucode="tjyh" onclick="IotM.User.OpenformDeleteUser()">批量删除</a>

        </div>
        <div id="dataGrid">
        </div>
        <div id="wAdd" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd'>

                <table style="border-spacing: 10px;">
                    <tr>
                        <td>户号:</td>
                        <td>
                            <input type="hidden" name="CNCompanyID" default="" />
                            <input type="hidden" name="CNState" default="0" />
                            <input class="easyui-validatebox" type="text" name="CNUserID" default="" disabled="disabled" />
                        </td>
                        <td>户名:</td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNUserName" default="" />
                        </td>

                    </tr>

                    <tr>
                        <td>道路:</td>
                        <td colspan="3">
                            <input class="easyui-combobox" type="text" name="CNStreet" id="CNStreet" default="" style="width: 320px" />
                        </td>
                    </tr>

                    <tr>
                        <td>小区:</td>

                        <td colspan="3">
                            <input class="easyui-combobox" type="text" name="CNCommunity" id="CNCommunity" default="" style="width: 320px" />
                        </td>
                    </tr>

                    <tr>
                        <td>门牌号:</td>
                        <td colspan="3">
                            <input class="easyui-validatebox" type="text" name="CNDoor" id="CNDoor" default="" onchange="IotM.User.AdressChange()" style="width: 320px" />
                        </td>

                    </tr>

                    <tr>
                        <td>地址:</td>
                        <td colspan="3">
                            <input class="easyui-validatebox" id="CNAddress" type="text" name="CNAddress" default="" style="width: 320px" disabled="disabled" />
                        </td>
                    </tr>




                    <tr>
                        <td>表号:</td>
                        <td>

                            <input class="easyui-validatebox" type="text" id="CNMeterNo" name="CNMeterNo" validtype="regNumLength[14,14]" default="" />
                        </td>
                        <td>表读数:</td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNTotalAmount" default="" disabled="disabled" />
                        </td>

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

        <div id="deleteUserDiv" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-remove'"
            style="padding: 10px; width: auto">

            <div id="tb_deleteUser">
                &nbsp;&nbsp;街道&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Street" />
                &nbsp;&nbsp;&nbsp;&nbsp;小区&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Community" />
                &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_AdressDel" style="width: 260px" />
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelectDel" onclick="IotM.User.DeleteSerachClick()">查询</a>
                &nbsp;&nbsp;&nbsp;&nbsp;
      
      
            </div>


            <div id="dataGrid_deleteUser">
            </div>


            <div style="text-align: right; padding-right: 50px; padding-top: 10px">

                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                    id="btnOk_deleteUser">确定删除</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                    id="btnCancel_deleteUser">取消</a>

            </div>

        </div>

        <div id="wAlarmParm" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAlarmParm'>
                <table>
                    <tr>
                        <td>
                            <input type="checkbox" id="switch0" />长期未与服务器通讯报警
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNPar1" default="30" />天
                        </td>
                        <td>
                            <input type="checkbox" id="switch1" />燃气泄漏切断报警
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNPar2" default="10" />秒
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <input type="checkbox" id="switch2" />燃气流量过载切断报警:
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNPar3" default="30" />秒
                        </td>
                        <td>
                            <input type="checkbox" id="switch3" />异常大流量切断报警:
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNPar5" default="30" />秒
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <input type="checkbox" id="switch4" />异常小流量切断报警:
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNPar6" default="30" />天
                        </td>


                        <td>
                            <input type="checkbox" id="switch5" />持续流量超时切断报警:
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNPar7" default="30" />小时
                        </td>
                    </tr>
                    <tr>

                        <td>
                            <input type="checkbox" id="switch7" />长期未使用切断报警:
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNPar8" default="30" />天
                        </td>
                    </tr>

                    <tr>
                        <td colspan="2">
                            <input type="checkbox" id="switch8" />移动报警/地址震感器动作切断报警
                        </td>
                        <td>
                            <input type="checkbox" id="switch6" />LCD状态检测
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>异常大流量:
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNPar4" default="" />m³/h &nbsp;&nbsp;
                        </td>
                        <td>
                            <%-- 燃气表公称流量:--%>
                        </td>
                        <td style="display: none">
                            <input class="easyui-validatebox" type="text" name="CNPar9" default="0" />m³/h
                        </td>
                    </tr>

                </table>
            </form>
        </div>
    </div>

</body>
</html>
