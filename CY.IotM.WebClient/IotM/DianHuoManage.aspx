<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DianHuoManage.aspx.cs" Inherits="CY.IotM.WebClient.DianHuoManage" %>

<!DOCTYPE html>

<html>
<head>

    <title>点火通气</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.DianHuo.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {

            IotM.Initiate.LoadUserStateComboBox('CNState', true, true);
            $("#CNState").combobox("setValue", 1);
            IotM.DianHuo.loadDataGrid();

            IotM.Initiate.LoadMeterTypeComboBox('CNMeterType', false, true);

            IotM.Initiate.LoadComboxGridPriceType("CNPriceType", true, false);



            //if ($('#CNMeterType').combobox('getValue') == "00") {
            //    IotM.DianHuo.loadcombogrid(false, true);
            //    //$('#cmid').combogrid({ required: false,disable:true });
            //    //$('#CNPriceType').combogrid('disable');
            //    $('#CNPriceType').combogrid('setValue', '');
            //}
            //else {
            //    IotM.DianHuo.loadcombogrid(true, false);
            //}
            $("#CNMeterType").combobox({
                onChange: function () {
                    //气量表
                    if ($('#CNMeterType').combobox('getValue') == "00") {
                        //$('#CNPriceType').combogrid('disable');
                        $('#CNPriceType').combogrid({ required: true });
                        //$('#CNPriceType').combogrid('setValue', 'a');
                        setTimeout(function () { $('#CNPriceType').combogrid({ disabled: true }); }, 200);

                        $('#CNPriceType').combogrid('setValue', '');
                        //IotM.DianHuo.loadcombogrid(false, true);
                    }
                    else {
                        $('#CNPriceType').combogrid();
                        //IotM.DianHuo.loadcombogrid(true, false);
                        setTimeout(function () { $('#CNPriceType').combogrid({ disabled: false }); }, 200);
                        $('#CNPriceType').combogrid('setValue', '');
                    }
                }
            });

        });
    </script>
</head>

<body>

    <div id="wrap">

        <div id="tb">
            &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserID" />
            &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserName" />
            &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Adress" style="width: 160px" />
            &nbsp;&nbsp;&nbsp;&nbsp;表状态&nbsp;&nbsp; 
            <input class="easyui-combobox" type="text" name="CNState" id="CNState" default="" />
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.DianHuo.SerachClick()">查询</a>
            &nbsp;&nbsp;&nbsp;&nbsp;
         <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" onclick="IotM.DianHuo.OpenformEdit()">点火登记</a>
        </div>
        <div id="dataGrid">
        </div>

        <div id="wAdd" class="easyui-window" title="点火通气" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id="formAdd">
                <table>
                    <tr>
                        <td>点火户数：
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="Ignitions" id="Ignitions" disabled="disabled" default="" />
                        </td>
                        <td>表类型：</td>
                        <td>
                            <input class="easyui-combobox" type="text" name="CNMeterType" id="CNMeterType" default="" />
                        </td>
                    </tr>
                    <tr>
                        <td>价格类型：</td>
                        <td colspan="3">
                            <input class="easyui-combogrid" type="text" name="CNPriceType" style="width: 320px" id="CNPriceType" style="width: 300px" default="" />
                        </td>
                    </tr>
                    <%--<tr>
                       <td></td>
                       <td colspan ="3"><input class="easyui-validatebox" type="text" name="Description" style ="height:40px; width:300px" disabled="disabled" default="" /> </td>
                   </tr>--%>
                    <tr>
                        <td>
                            <label id="fileDH">点火日期：</label>
                        </td>
                        <td colspan="3">
                            <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
                                id="CNInstallDate" style="width: 90px" />
                        </td>
                    </tr>
                    <tr>
                        <td>参与人员：</td>
                        <td colspan="3">
                            <input class="easyui-validatebox" type="text" name="Description" id="Description" style="width: 320px" default="" />
                        </td>
                    </tr>
                    <tr>
                        <td>防盗卡号：</td>
                        <td>
                            <input class="easyui-validatebox" type="text" id="CNFDKH1" name="CNFDKH1" default="" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'" id="btnOk">完成</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                id="btnCancel">取消</a>
                        </td>

                    </tr>
                </table>

            </form>

        </div>

    </div>

</body>
</html>
