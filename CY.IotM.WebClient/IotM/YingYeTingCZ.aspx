<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YingYeTingCZ.aspx.cs" Inherits="CY.IotM.WebClient.IotM.YingYeTingCZ" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
    <script src="../Scripts/IotM/IotM.YingYeTingCZ.js"></script>
    <script src="../Scripts/IotM/IotM.User.js"></script>

    <script type="text/javascript">
        $(function () {
            //监视登陆
            IotM.CheckLogin();
            //加载资料
            IotM.YingYeTingCZ.LoadDataGrid();
            //$('#btnOk').unbind('click').bind('click', IotM.YingYeTingCZ.EXPOldData());

        });
        $.extend($.fn.validatebox.defaults.rules, {
            ManeyRex: {
                validator: function (value) {
                    var rex = /^([1-9][\d]{0,7}|0)(\.[\d]{1,2})?$/;
                    //var rex = /^[0-9]{1,6}(.[0-9]{1,2})?$/;
                    //var rex = /^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/;
                    if (rex.test(value)) {
                        if (value == 0) {
                            return false;
                        } else {
                            return true;
                        }
                        //if (parseInt(value, 10) <= 59) {

                        //} else {
                        //return false;
                        //}
                    } else {
                        return false;
                    }
                },
                message: '请输入正确金额格式'
            }
        });
    </script>
    <title>查询抄表数据</title>
</head>
<body>
    <div id="wrap">

        <form id="form1">
            <div id="tb">
                &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserID" />
                &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserName" />
                &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Adress" style="width: 160px" />

                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.YingYeTingCZ.SerachClick()">查询</a>
                <%--                    <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnRegistration" onclick="IotM.YingYeTingCZ.OpenformEXP()">导出数据</a>--%>
            </div>
            <div id="dataGrid">
            </div>
            <%--
                    &nbsp;&nbsp;户号&nbsp;&nbsp;
                    &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" required="true" missingmessage="请输入户名" type="text" id="select_UserNameE" />
                    &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_AdressE" style="width: 160px" />
                    &nbsp;&nbsp;&nbsp;&nbsp;充值金额&nbsp;&nbsp;<input class="easyui-validatebox" required="true" missingmessage="请输入充值金额" data-options="validType:'ManeyRex'" type="text" id="CNAmount" style="width: 160px" />
                    
                    <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" id="btnChongZhi" onclick="IotM.YingYeTingCZ.ChongZhiClick()">充值</a>
                </div>--%>
        </form>
        <div id="wAdd" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-search'"
            style="padding: 10px; width: auto">
            <form id="formAdd">
                <table>
                    <tr>
                        <td nowrap="nowrap" align="right">户号:</td>
                        <td>
                            <input class="easyui-validatebox" type="text" disabled="disabled" required="true" missingmessage="请输入户号" id="select_UserIDE" /></td>
                        <td nowrap="nowrap" align="right">户名:</td>
                        <td>
                            <input class="easyui-validatebox" type="text" disabled="disabled" required="true" missingmessage="请输入户名" id="select_UserNameE" /></td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">地址:</td>
                        <td colspan="3">
                            <input class="easyui-validatebox" type="text" id="select_AdressE" style="width: 332px" /></td>
                        <input id="hidMeterID" type="hidden" /><input id="hidUserID" type="hidden" />
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">充值金额:</td>
                        <td colspan="3">
                            <input class="easyui-validatebox" required="true" missingmessage="请输入充值金额" data-options="validType:'ManeyRex'" type="text" id="CNAmount" /></td>

                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-save'" id="btnChongZhi">充值</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                id="btnCancel">取消</a>
                        </td>

                    </tr>
                </table>
            </form>
        </div>




        <%--    <div id="Registration" class="easyui-window" title="导出数据" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: 600px">
            <form id="fRegistration">
                <div id="cc" class="easyui-layout" style="width: 99%; height: 400px;">
                    <div data-options="region:'north',title:'时间条件'" style="height: 70px;">
                        <input type="radio" value='0' name='CNTime' checked="checked" />最新数据
                    <input type="radio" value='1' name='CNTime' />指定日期
                    <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
                        id="CNDate" style="width: 90px" />

                    </div>
                    <div data-options="region:'center',title:'导出范围'" style="height: 70px;">
                        <input type="radio" onchange="IotM.YingYeTingCZ.fnChange();" value='0' name='CNUser' checked="checked" />所有用户
                    <input type="radio" value='1' onchange="IotM.YingYeTingCZ.fnChange();" name='CNUser' />指定小区
                    </div>
                    <div data-options="region:'south',title:''" class="easyui-panel" id="divButum" style="height: 260px;">
                        <div id="dataGridStreet">
                        </div>
                    </div>
                </div>


                <table>
                    <tr>
                        <%--onclick="IotM.YingYeTingCZ.EXPOldData();"--%>
        <%-- <td colspan="4" align="center">
                            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                id="btnOk" onclick="IotM.YingYeTingCZ.EXPOldData();">开始导出</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                    id="btnCancel">取消</a>
                        </td>

                    </tr>
                </table>

            </form>

        </div>--%>
    </div>
</body>
</html>
