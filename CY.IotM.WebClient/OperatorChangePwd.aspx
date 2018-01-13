<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperatorChangePwd.aspx.cs"
    Inherits="CY.IotM.WebClient.OperatorChangePwd" %>

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
    <script src="../Scripts/IotM.CompanyOperatorManage.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            IotM.CheckLogin();
            IotM.regvalidatebox("formAdd");
            IotM.CompanyOperatorManage.OpenformChangePwd();
            $(window).resize(function () {
                IotM.SetMainGridWidth(0.99);
                IotM.SetMainGridHeight(0.99);
            });
        });  
    </script>
</head>
<body>
    <div id="wAddCompanyOperator" class="easyui-window" title="修改密码" data-options="modal:true,closed:true,iconCls:'icon-edit'"
        style="padding: 10px">
        <form id='formAdd'>
        <div class="demo-info">
            <div class="demo-tip icon-tip">
            </div>
            <div>
                修改用户登录密码</div>
        </div>
        <table>
            <tr>
                <td>
                    编号:
                </td>
                <td>
                    <input type="hidden" name="CNCompanyID" />
                    <input class="easyui-validatebox" type="text" name="CNOperID" required="true" validtype='loginCode'
                        disabled="disabled" style="width: 80%" />
                </td>
            </tr>
            <tr>
                <td>
                    姓名:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNName" required="true" disabled="disabled"
                        style="width: 80%" />
                </td>
            </tr>
            <tr>
                <td>
                    原密码:
                </td>
                <td>
                    <input name="CNOldPwd" type="password" class="easyui-validatebox" required="true"
                        missingmessage="请输入原密码" style="width: 80%"></input>
                </td>
            </tr>
            <tr>
                <td>
                    新密码:
                </td>
                <td>
                    <input name="CNPwd" id="CNPwd" type="password" class="easyui-validatebox" required="true"
                        missingmessage="请输入新密码" style="width: 80%"></input>
                </td>
            </tr>
            <tr>
                <td>
                    确认新密码:
                </td>
                <td>
                    <input id="NewPwd" type="password" class="easyui-validatebox" required="true" validtype='equalTo["#CNPwd"]'
                        missingmessage="请再次输入新密码" style="width: 80%"></input>
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
</body>
</html>
