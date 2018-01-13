<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CompanyManage.aspx.cs"
    Inherits="CY.IotM.WebClient.CompanyManage" %>

<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>企业管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.CompanyManage.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        $(function () {
            IotM.CheckLogin();
            IotM.CompanyManage.LoadCompanyDataGrid();
            IotM.Initiate.LoadCompanyOperatorStateComboBox('CNStatus', false, true);
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
    <div id="tb">
        <a href="javascript:void(0)" menucode="tjzcqy" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
            onclick="IotM.CompanyManage.OpenformAdd()">添加企业</a>
        <div style="margin: 5px 2px;">
        </div>
        <input class="easyui-searchbox" data-options="prompt:'请输入检索关键字',menu:'#mm',searcher:IotM.CompanyManage.SerachClick"
            style="width: 300px"></input>
        <div id="mm" style="width: 120px">
            <div data-options="name:'CompanyName'">
                企业名称
            </div>
            <div data-options="name:'CompanyID'">
                企业编号
            </div>
        </div>
    </div>
    <div id="dataGrid">
    </div>
    <div id="wAddCompany" class="easyui-window" title="添加企业" data-options="modal:true,closed:true,iconCls:'icon-add'"
        style="padding: 10px; width: 300px">
        <div class="demo-info">
            <div class="demo-tip icon-tip">
            </div>
            <div>
                企业编号4位数字或字母组合，新增后不可修改。
            </div>
        </div>
        <table>
            <tr>
                <td>企业编号:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNCompanyID" default="" edit="false"
                        required="true" validtype='companyCode' />
                </td>
            </tr>
            <tr>
                <td>企业名称:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNCompanyName" default="" required="true"
                        missingmessage="请输入企业名称" />
                </td>
            </tr>
            <tr>
                <td>所在城市:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNCity" default="郑州" required="true"
                        missingmessage="请输入企业所在城市名称" />
                </td>
            </tr>
            <tr>
                <td>状态:
                </td>
                <td>
                    <input name="CNStatus" id="CNStatus" class="easyui-combobox" />
                </td>
            </tr>
            <tr>
                <td>联系人:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNLinkman" default="" required="true"
                        missingmessage="请输入联系人" />
                </td>
            </tr>
            <tr>
                <td>联系电话:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNPhone" default="" required="true"
                        validtype="mobile" missingmessage="请输入联系人手机号码" />
                </td>
            </tr>
            <tr>
                <td>通讯地址:
                </td>
                <td>
                    <textarea name="CNAddress" default="" style="height: 30px;"></textarea>
                </td>
            </tr>
            <tr>
                <td>备注:
                </td>
                <td>
                    <textarea name="CNContext" default="" style="height: 60px;"></textarea>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                        id="btnOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                            id="btnCancel">取消</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="wEditCompanyMenu" class="easyui-window" title="分配企业报表" data-options="modal:true,closed:true,iconCls:'icon-add'"
        style="padding: 10px; width: 300px">


    
         <table>
            <tr>
                
                <td>   <div id="treeMenuCode"></div></td>
              
            </tr>
            <tr>
                 <td>
                        <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                            id="btnCompanyMenuOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                id="btnCompanyMenuCancel">取消</a>
                    </td>
            </tr>
         </table>



    </div>
</body>
</html>
