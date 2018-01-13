<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HuanBiaoManege.aspx.cs" Inherits="CY.IotM.WebClient.IotM.HuanBiaoManege" %>


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
    <script src="../Scripts/IotM/IotM.HuanBiaoManege.js"></script>
    <script type="text/javascript">
        $(function () {
            IotM.CheckLogin();
            IotM.Initiate.LoadGasDirectionComboBox("CNDirection_Add", false, false);
            IotM.Initiate.LoadMeterTypeComboBox("CNMeterType_Add", false, false);
            IotM.Initiate.LoadGasDirectionComboBox("CNDirection_DJ", false, false);
            IotM.Initiate.LoadMeterTypeComboBox("CNMeterType_DJ", false, false);
            IotM.Initiate.LoadMeterTypeComboBox("CNNewMeterType_DJ", false, false);
            IotM.Initiate.LoadHuanBiaoComboBox("CNState", true, false);
            $('#CNUserID_Add').keydown(function (e) {
                if (e.keyCode == 13) {
                    IotM.HuanBiao.GetUserMessage($('#CNUserID_Add').val());
                }
            });
            IotM.HuanBiao.LoadDataGrid();
            IotM.regvalidatebox("formAdd");
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });
            //IotM.Initiate.LoadHuanBiaoTypeComboBox("CNTopUpType", true, false);
        });
        $.extend($.fn.validatebox.defaults.rules, {
            DayRex: {
                validator: function () {
                    if ($("#hidSure").val() == "true") {
                        return true;
                    } else {
                        return false;
                    }
                },
                message: '请选择用户！'
            }
        });
        $.extend($.fn.validatebox.defaults.rules, {
            MeterRex: {
                validator: function (value) {
                    var rex = /^([1-9][\d]{0,7}|0)(\.[\d]{1,2})?$/;
                    //var rex = /^[0-9]{1,6}(.[0-9]{1,2})?$/;
                    //var rex = /^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/;
                    if (rex.test(value)) {
                        if (value == "") {
                            return false;
                        } else {
                            return true;
                        }
                    } else {
                        return false;
                    }
                },
                message: "请输入正确表底数格式"
            }
        });
        $.extend($.fn.validatebox.defaults.rules, {
            ManeyRex: {
                validator: function (value) {
                    var strmessage = "";
                    var rex = /^([1-9][\d]{0,7}|0)(\.[\d]{1,2})?$/;
                    //var rex = /^[0-9]{1,6}(.[0-9]{1,2})?$/;
                    //var rex = /^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/;
                    if (rex.test(value)) {
                        if (value == "") {
                            strmessage = "请输入剩余金额";
                            return false;
                        } else {
                            return true;
                        }
                    } else {
                        strmessage = "请输入正确金额格式";
                        return false;
                    }
                },
                message: strmessage
            }
        });
    </script>
</head>
<body>
    <%--   <form id="form1" runat="server">--%>
    <div id="wrap">
        <div id="tb">
            &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="CNUserID" />
            &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="UserName" />
            &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="CNAdress" style="width: 260px" />
            &nbsp;&nbsp;&nbsp;&nbsp;状态&nbsp;&nbsp;<input class="easyui-combobox" type="text" id="CNState" />
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.HuanBiao.SerachClick()">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnShenQing" onclick="IotM.HuanBiao.OpenShenQing('')">申请</a>
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" id="btnDengJi" onclick="IotM.HuanBiao.OpenDengJi()">换表登记</a>

            <input type="hidden" name="CNCompanyID" default="" />
            <input type="hidden" name="CNState" default="0" />
        </div>

        <div id="dataGrid">
        </div>

        <div id="wAdd" class="easyui-window" title="换表申请" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd'>
                <table style="border-spacing: 10px;">
                    <tr>
                        <td align="right" nowrap="nowrap">户号:</td>
                        <td colspan="3">
                            <input type="hidden" name="CNCompanyID" default="" />
                            <input type="hidden" name="hidSure" id="hidSure" default="" />
                            <input type="hidden" name="CNState" default="0" />
                            <input class="easyui-validatebox" required="true" data-options="validType:'DayRex'" type="text" id="CNUserID_Add" default="" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                               <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'" id="btnChoise" onclick="IotM.HuanBiao.OpenUsers()">选择</a>
                        </td>

                    </tr>
                    <tr>

                        <td align="right" nowrap="nowrap">户名:</td>
                        <td colspan="3">
                            <input class="easyui-validatebox" type="text" id="CNUserName_Add" name="CNUserName_Add" disabled="disabled" default="" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">地址:</td>
                        <td colspan="3">
                            <input class="easyui-validatebox" id="CNAddress_Add" type="text" disabled="disabled" name="CNAddress" default="" style="width: 360px" disabled="disabled" />
                        </td>
                    </tr>

                    <tr>
                        <td align="right" nowrap="nowrap">表号:</td>
                        <td>

                            <input class="easyui-validatebox" type="text" id="CNMeterNo_Add" disabled="disabled" name="CNMeterNo" default="" />
                        </td>
                        <td align="right" nowrap="nowrap">表底数:</td>
                        <td>
                            <input class="easyui-validatebox" type="text" disabled="disabled" id="CNTotalAmount_Add" default="" />
                        </td>


                    </tr>

                    <tr>
                        <td align="right" nowrap="nowrap">进气方向:</td>
                        <td>

                            <input class="easyui-combobox" type="text" disabled="disabled" id="CNDirection_Add" />
                        </td>
                        <td align="right" nowrap="nowrap">表类型:</td>
                        <td>
                            <input class="easyui-combobox" type="text" disabled="disabled" id="CNMeterType_Add" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">换表原因:</td>
                        <td colspan="3">
                            <textarea id="CNReason_Add" style="width: 360px; height: 72px;"></textarea>

                        </td>
                    </tr>
                    <tr>

                        <td colspan="4" align="center">
                            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                id="btnOk">申请</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                    id="btnCancel">取消</a>
                        </td>
                    </tr>
                </table>


            </form>
        </div>


        <div id="divDengJi" class="easyui-window" title="换表登记" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formDengJi'>
                <table style="border-spacing: 10px;">
                    <tr>
                        <td align="right" nowrap="nowrap">户号:</td>
                        <td>
                            <input class="easyui-validatebox" type="text" disabled="disabled" id="CNUserID_DJ" name="CNUserID" default="" />
                        </td>
                        <td align="right" nowrap="nowrap">户名:</td>
                        <td>
                            <input class="easyui-validatebox" type="text" id="CNUserName_DJ" name="CNUserName" disabled="disabled" default="" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">地址:</td>
                        <td colspan="3">
                            <input class="easyui-validatebox" id="CNAddress_DJ" type="text" disabled="disabled" name="CNAddress" default="" style="width: 370px" disabled="disabled" />
                        </td>
                    </tr>

                    <tr>
                        <td align="right" nowrap="nowrap">表号:</td>
                        <td>

                            <input class="easyui-validatebox" type="text" id="CNMeterNo_DJ" disabled="disabled" name="CNOldMeterNo" validtype="regNumLength[14,14]" default="" />
                        </td>
                        <td align="right" nowrap="nowrap">表底数:</td>
                        <td>
                            <input class="easyui-validatebox" type="text" disabled="disabled" id="CNTotalAmount_DJ" name="CNOldGasSum" default="" />
                        </td>

                    </tr>

                    <tr>
                        <td align="right" nowrap="nowrap">进气方向:</td>
                        <td>

                            <input class="easyui-combobox" type="text" disabled="disabled" name="CNDirection_DJ" id="CNDirection_DJ" />
                        </td>

                        <td align="right" nowrap="nowrap">表类型:</td>
                        <td>
                            <input class="easyui-combobox" type="text" disabled="disabled" name="CNMeterType_DJ" id="CNMeterType_DJ" />
                        </td>

                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">换表原因:</td>
                        <td colspan="3">
                            <textarea id="CNReason_DJ" name="CNReason" disabled="disabled" style="width: 370px; height: 72px;"></textarea>

                        </td>
                    </tr>



                    <tr>
                        <td align="right" nowrap="nowrap">上期结算底数:</td>
                        <td>

                            <input class="easyui-validatebox" name="CNdayGas" type="text" disabled="disabled" id="CNLastTotal_DJ" />
                        </td>

                        <td align="right" nowrap="nowrap">本期用量:</td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNTotalAmountS" disabled="disabled" id="CNTotalAmountS_DJ" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">剩余金额:</td>
                        <td>

                            <input class="easyui-validatebox" type="text" name="CNRemainingAmount" id="CNRemainingAmount_DJ" />
                        </td>

                        <td align="right" nowrap="nowrap">换表底数:</td>
                        <td>
                            <input class="easyui-validatebox" data-options="validType:'MeterRex'" onblur="IotM.HuanBiao.GetChangeUserGas();" required="true" type="text" name="CNChangeGasSum" id="CNOldGasSum_DJ" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">新表号:</td>
                        <td>

                            <input class="easyui-validatebox" type="text" required="true" missingmessage="请输入新表号" validtype="regNumLength[14,14]" name="CNNewMeterNo" id="CNNewMeterNo_DJ" />
                        </td>

                        <td align="right" nowrap="nowrap">新表底数:</td>
                        <td>
                            <input class="easyui-validatebox" data-options="validType:'MeterRex'" required="true"   type="text" name="CNChangeUseSum" id="CNChangeGasSum_DJ" />
                        </td>
                    </tr>

                    <tr>
                        <td align="right" nowrap="nowrap">换表日期:</td>
                        <td>
                            <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
                                id="CNFinishedDate_DJ" />

                        </td>
                        <td align="right" nowrap="nowrap">新表类型:</td>
                        <td>
                            <input class="easyui-combobox" type="text" disabled="disabled" name="CNNewMeterType_DJ" id="CNNewMeterType_DJ" />
                        </td>
                    </tr>


















                    <tr>

                        <td colspan="4" align="center">
                            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'" id="btnOk_DJ">登记</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                id="btnCancel_DJ">取消</a>
                        </td>
                    </tr>
                </table>


            </form>
        </div>


    </div>

    <div id="GetUserDiv" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-search'"
        style="padding: 10px; width: auto">
        <div id="tb_user">
            <table>
                <tr>
                    <td nowrap="nowrap">户号:</td>
                    <td>
                        <input class="easyui-validatebox" type="text" id="CNUserID_user" name="CNUserID_user" default="" />
                    </td>
                    <td nowrap="nowrap">户名:</td>
                    <td>
                        <input class="easyui-validatebox" type="text" id="CNUserName_user" name="CNUserName_user" default="" />
                    </td>
                    <td nowrap="nowrap">地址:</td>
                    <td>
                        <input class="easyui-validatebox" id="CNAddress_user" type="text" name="CNAddress_user" default="" style="width: 293px" />
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap">表号:</td>
                    <td colspan="2">
                        <input class="easyui-validatebox" type="text" id="CNMeterNo_user" name="CNMeterNo_user" validtype="regNumLength[14,14]" default="" />
                    </td>
                    <td><a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'"
                        id="btnqry_User">查询</a>   </td>
                </tr>
            </table>
        </div>
        <div id="dataGrid_User">
        </div>
        <%--</td>
                    </tr>
                    <tr>
                        <td colspan="4">--%>
        <div style="text-align: center; padding-right: 50px; padding-top: 10px" align="left">
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                id="btnOk_User">确定选择</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                    id="btnCancel_User">取消</a>

        </div>
        <%-- </td>
                    </tr>
                </table>--%>
    </div>




    <div id="GetMeterHis" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-search'"
        style="padding: 10px; width: auto">

        <div id="dataGrid_Meter">
        </div>
        <%--<div style="text-align: center; padding-right: 50px; padding-top: 10px" align="left">
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                id="btnOk_Meter">确定选择</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                    id="btnCancel_Meter">取消</a>

        </div>--%>
    </div>

</body>
</html>

