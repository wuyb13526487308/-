<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CY.IotM.WebClient.Login" %>

<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>登录页面</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#loginID").val(IotM.cookie("loginID"))
            IotM.regvalidatebox('ff');
            $('#center').window({
                title: '用户登录',
                width: 380,
                height: 240,
                top: ($(window).height() - 320) * 0.15,
                shadow: true,
                modal: true,
                closed: true,
                minimizable: false,
                maximizable: false,
                collapsible: false,
                closable: false
            });
            $('#center').window('open');
        });
        function EnterKeyDown(e) {
            var evt = e || event;
            if (evt.keyCode == 13) {
                Login();
            }
        }
        function Login() {
            if (IotM.checkisValid('ff')) {
                $.post("../Handler/SystemManage/OperatorLoginManageHandler.ashx?AType=UserLogin",
                 { LoginID: $("#loginID").val(), LoginPsw: $("#loginPsw").val() },
                  function (data, textStatus) {
                      if (textStatus == 'success' && data.Result) {
                          IotM.cookie("loginID", $("#loginID").val(), { expires: new Date('2099-12-31') });
                          IotM.AddSystemLog({ LogType: 0, Context: '登陆系统。' });
                          window.top.location.href = "Index.aspx?r=" + Math.random();
                      }
                      else {                       
                          $.messager.alert('警告', data.TxtMessage, 'warn');
                      }
                  }, "json");
            }
        }

        function SFXTLogin() {
            if (IotM.checkisValid('ff')) {
                $.post("../Handler/SystemManage/OperatorLoginManageHandler.ashx?AType=UserLogin",
                 { LoginID: $("#loginID").val(), LoginPsw: $("#loginPsw").val() },
                  function (data, textStatus) {
                      if (textStatus == 'success' && data.Result) {
                          IotM.cookie("loginID", $("#loginID").val(), { expires: new Date('2099-12-31') });
                          IotM.AddSystemLog({ LogType: 0, Context: '登陆系统。' });
                          window.top.location.href = "Success.html?r=" + Math.random();
                      }
                      else {
                          window.top.location.href = "Fail.html?r=" + Math.random();
                      }
                  }, "json");
            }
        }

        function Test() {
            return $("#loginID").val();
        }
    </script>
</head>
<body>
    <div id="center" title="用户登录">
        <form id="ff">
            <div style="padding: 10px 0 10px 50px; height: auto">
                <table>
                    <tr>
                        <td colspan="2">
                          <%--  <img src="../Image/sysLog.gif" width="280px" height="36px" />--%>
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td>账号:
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" id="loginID" style="width: 80%" required="true"
                                validtype="loginName" onkeydown="EnterKeyDown(event)" value=""></input>
                        </td>
                    </tr>
                    <tr>
                        <td>密码:
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="password" id="loginPsw" style="width: 80%"
                                required="true" missingmessage="请输入您的密码" onkeydown="EnterKeyDown(event)" value=""></input>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <a href="javascript:Login();" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'">确定</a>
                            <a href="javascript:IotM.FormRedo('ff');" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-redo'">重置</a>
                        </td>
                    </tr>
                </table>
            </div>
        </form>
    </div>
</body>
</html>
