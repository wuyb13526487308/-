<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ADUser.aspx.cs" Inherits="CY.IotM.WebClient.AdM.ADUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>广告用户管理</title>
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
    <script src="../Scripts/IotM.Menu.js" type="text/javascript"></script>


    <script src="../Scripts/AdM/AdM.User.js" type="text/javascript"></script>
        <script type="text/javascript">

        $(function () {
            IotM.CheckLogin();
            AdM.User.LoadDataGridView();
            //加载广告列表
            AdM.User.LoadADContextComboBox('CNAC_ID', true, true);
            //加载街道列表
            //AdM.User.LoadStreetComboBox('CNStreet', true, true);
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });
            

            //初始化街道信息

            IotM.Initiate.LoadStreetComboBox('CNStreet', true, true);
            $('#CNStreet').combobox(
            {
                onSelect: function (rec) {
                    IotM.Initiate.LoadCommunityComboBox('CNCommunity', true, true, rec.ID);
                }
            });
            IotM.Initiate.LoadCommunityComboBox('CNCommunity', true, true, $('#CNStreet').combobox("getValue"));



        });


    </script>
</head>
<body>
    <form id="form1" runat="server">
   
     <!--查询内容-->
    <div id="tb">&nbsp;&nbsp;
    街道：&nbsp;&nbsp;<input class="easyui-combobox" type="text" name="CNStreet" id="CNStreet" default=""  style="width:100px"/> &nbsp;&nbsp;&nbsp;&nbsp;
    小区：&nbsp;&nbsp;<input class="easyui-combobox" type="text" name="CNCommunity" id="CNCommunity" default=""  style="width:130px"/>&nbsp;&nbsp;&nbsp;&nbsp;
    表号：&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="MeterNo"  name="MeterNo"  style="width:130px"/> &nbsp;&nbsp;&nbsp;&nbsp;
    广告主题：&nbsp;&nbsp;<select class="easyui-combobox" id="CNAC_ID"  name="CNAC_ID" style="width:250px" ></select>  &nbsp;&nbsp;&nbsp;&nbsp;<br/>&nbsp;&nbsp;
    地址：&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="Adress"  name="Adress" style="width:300px" />
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="AdM.User.SerachClick()">查询</a> &nbsp;&nbsp;<br />&nbsp;&nbsp;        
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
            onclick="AdM.User.OpenformAddGroupUser()">添加广告屏用户</a>
        <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true"
            onclick="AdM.User.OpenformDelGroupUser()">移除广告屏用户</a>
    </div>
    <!--列表显示-->
    <div id="dataGrid"></div>
   
    </form>
   
    <div id="UserDiv" class="easyui-window"  data-options="modal:true,closed:true,iconCls:'icon-add'" style="padding: 10px; width: auto">

        <div id="tb_User">
            &nbsp;&nbsp;街道&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Street"  />
            &nbsp;&nbsp;&nbsp;&nbsp;小区&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Community"  />
            &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Adress"  style="width:260px" />
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelectDel" onclick="AdM.User.DataGridUserSerachClick()">查询</a> 
            &nbsp;&nbsp;&nbsp;&nbsp;
        </div>
        <div id="dataGrid_User"></div>
        <div style="text-align:right;padding-right:50px;padding-top:10px">
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'" id="btnOk_User">确定</a>&nbsp;&nbsp;&nbsp;&nbsp; 
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"  id="btnCancel_User">取消</a>
        </div>

    </div>


</body>
</html>
