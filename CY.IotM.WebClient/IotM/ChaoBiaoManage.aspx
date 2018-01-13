<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChaoBiaoManage.aspx.cs" Inherits="CY.IotM.WebClient.IotM.ChaoBiaoManage" %>

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
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/easyui1.3.3/detailview.js"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.ChaoBiao.js"></script>
    <script type="text/javascript">
        $(function () {
            //监视登陆
            IotM.CheckLogin();
            //加载资料
            IotM.ChaoBiao.LoadDataGrid();
            $('#btnOk').unbind('click').bind('click', IotM.ChaoBiao.EXPOldData());

        });
    </script>
    <title>查询抄表数据</title>
</head>
<body>
    <div id="wrap">

        <div id="tb">
            &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserID" />
            &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserName" />
            &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Adress" style="width: 160px" />

            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.ChaoBiao.SerachClick()">查询</a>
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-print'" id="btnRegistration" onclick="IotM.ChaoBiao.OpenformEXP()">导出数据</a>
        </div>
        <div id="dataGrid">
        </div>
            
        <div id="Registration" class="easyui-window" title="导出数据" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: 600px">
            <form id="fRegistration">
                <div id="cc" class="easyui-layout" style="width: 99%; height: 400px;">
                    <div data-options="region:'north',title:'时间条件'" style="height: 70px;">
                        <input type="radio" value='0' name='CNTime' checked="checked" />最新数据
                    <input type="radio" value='1' name='CNTime' />指定日期
                    <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
                        id="CNDate" style="width: 90px" />

                    </div>
                    <input id="hidCompany" type="hidden" />
                    <div data-options="region:'center',title:'导出范围'" style="height: 70px;">
                        <input type="radio" onchange="IotM.ChaoBiao.fnChange();" value='0' name='CNUser' checked="checked" />所有用户
                    <input type="radio" value='1' onchange="IotM.ChaoBiao.fnChange();" name='CNUser' />指定小区
                    </div>
                    <div data-options="region:'south',title:''" class="easyui-panel" id="divButum" style="height: 260px;">
                        <div id="dataGridStreet">
                        </div>
                    </div>
                </div>


                <table>
                    <tr>
                        <%--onclick="IotM.ChaoBiao.EXPOldData();"--%>
                        <td colspan="4" align="center">

                            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                id="btnOk" onclick="IotM.ChaoBiao.EXPOldData();">开始导出</a>
                            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                id="btnCancel">取消</a>
                        </td>

                    </tr>
                </table>

            </form>

        </div>

    </div>
</body>
</html>
