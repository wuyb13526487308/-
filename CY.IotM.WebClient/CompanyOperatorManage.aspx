<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyOperatorManage.aspx.cs"
    Inherits="CY.IotM.WebClient.CompanyOperatorManage" %>

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
    <script src="../Scripts/IotM.CompanyOperatorManage.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            IotM.CheckLogin();
            IotM.CompanyOperatorManage.LoadCompanyOperatorDataGrid();
            IotM.Initiate.LoadSexsComboBox('CNSex', false, false);
            IotM.Initiate.LoadCompanyOperatorStateComboBox('CNState', false, true);
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
                menucode="tjczy" onclick="IotM.CompanyOperatorManage.OpenformAdd()">添加操作员</a>
            <div style="margin: 5px 2px;">
            </div>
            <input class="easyui-searchbox" data-options="prompt:'请输入检索关键字',menu:'#mm',searcher:IotM.CompanyOperatorManage.SerachCompanyOperatorClick"
                style="width: 300px"></input>
            <div id="mm" style="width: 120px">
                <div data-options="name:'OperID'">
                    编号</div>
                <div data-options="name:'Name'">
                    操作员姓名</div>
                <div data-options="name:'Phone'">
                    电话</div>
            </div>
        </div>
        <div id="dataGrid">
        </div>
        <div id="wAddCompanyOperator" class="easyui-window" title="添加操作员" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px;">
            <form id='formAdd'>
            <div class="demo-info">
                <div class="demo-tip icon-tip">
                </div>
                <div>
                    添加操作员后，登录账号为（编号@企业编号）,初始密码（企业编号）</div>
            </div>
            <table>
                <tr>
                    <td>
                        编号:
                    </td>
                    <td>
                        <input type="hidden" name="CNCompanyID" default="" />
                        <input class="easyui-validatebox" type="text" name="CNOperID" default="" edit="false"
                            required="true" validtype='loginCode' />
                    </td>
                </tr>
                <tr>
                    <td>
                        姓名:
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="CNName" default="" required="true"
                            missingmessage="请输入操作员姓名" />
                    </td>
                </tr>
                <tr>
                    <td>
                        状态:
                    </td>
                    <td>
                        <input name="CNState" id="CNState" class="easyui-combobox"></input>
                    </td>
                </tr>
                <tr>
                    <td>
                        性别:
                    </td>
                    <td>
                        <input name="CNSex" id="CNSex" class="easyui-combobox"></input>
                    </td>
                </tr>
                <tr>
                    <td>
                        手机号:
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="CNPhone" default="" validtype="mobile"
                            missingmessage="请输入正确的手机号" /><input name="CNPhoneLogin" default="false" type="checkbox" />启用手机号登陆
                    </td>
                </tr>
                <tr>
                    <td>
                        邮箱:
                    </td>
                    <td>
                        <input class="easyui-validatebox" type="text" name="CNMail" default="" validtype="email"
                            missingmessage="请输入正确的邮箱" />
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
        <div id="wAddOperRight" class="easyui-window" title="权限组" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px;">
            <div id="tbOperRight">
                <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-save',plain:true"
                    menucode="fpqxz" onclick="IotM.CompanyOperatorManage.SaveCompanyOperRight()">保存</a>

            </div>
            <div id="dataGridCompanyRight">
            </div>
        </div>

        <div id="wAddOperArea" class="easyui-window" title="操作区域" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px;">
          
          
               <a href="javascript:void(0)" id="btnAddArea" class="easyui-linkbutton" data-options="iconCls:'icon-save',plain:true" >保存</a>
               <div id="UserTree" style="height:250px"></div>

        </div>

         
        
       


    </div>
    
</body>
</html>
