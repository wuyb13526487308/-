<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdContentMain.aspx.cs" Inherits="CY.IotM.WebClient.AdM.AdContentMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>广告主题管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/easyui1.3.3/detailview.js"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Menu.js" type="text/javascript"></script>
    <script src="../Scripts/AdM/AdM.ContentMain.js" type="text/javascript"></script>
<script type="text/javascript">
    
   $(function () {
        IotM.CheckLogin();
        AdM.ContentMain.LoadDataGridView();

        $(window).resize(function () {
            IotM.SetMainGridWidth(1);
            IotM.SetMainGridHeight(0.99);
            $("#dataGrid").datagrid("resize");
        });

    });

</script>

</head>
<body>
    <!--查询内容-->
    <div id="tb">&nbsp;&nbsp;
     主题&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="AD_Context"  name="AD_Context" /> &nbsp;&nbsp;&nbsp;&nbsp;
     状态&nbsp;&nbsp;
        <select id="State" class="easyui-combobox" name="State" >
                                    <option value="" selected="selected">全部</option>
                                    <option value="0" >草稿</option>
                                    <option value="1" >可发布</option>
                                    <option value="2" >已发布</option>
                                </select>
        &nbsp;&nbsp;&nbsp;&nbsp;
     创建日期&nbsp;&nbsp;<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"  id="bDate" name="bDate" style="width: 90px"/>
        <input id="bDataTime" class="easyui-timespinner" style="width: 55px;"/>&nbsp;&nbsp;&nbsp;
     至&nbsp;&nbsp;&nbsp;<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"  id="eDate" name="eDate"style="width: 90px"/>
        <input id="eDataTime" class="easyui-timespinner" style="width: 55px;"/>
    <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="AdM.ContentMain.SerachClick()">查询</a> 
    &nbsp;&nbsp;&nbsp;&nbsp;
    <br><br>&nbsp;&nbsp;
        
        <a href="javascript:void(0)" menucode="tjzcqy" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
            onclick="AdM.ContentMain.OpenformAdd()">新建主题</a>
    </div>
    <!--列表显示-->
    <div id="dataGrid"></div>

    <!--添加主题-->
    <div id="wAddContext" class="easyui-window" title="新建主题" data-options="modal:true,closed:true,iconCls:'icon-add'"
        style="padding: 10px; width: 350px">
        <table>
            <tr><td style="height:35px">广告主题：</td></tr>
            <tr>
                <td>
                    <textarea name="CNContext" default="" style="height: 200px;width:340px"></textarea>
                    <input  name="CNAC_ID"  id="CNAC_ID" type="hidden" />
                </td>
            </tr>
            <tr>
                <td style="height:40px; text-align: right;" >
                    <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                        id="btnOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                            id="btnCancel">取消</a>
                </td>
            </tr>
        </table>
    </div>

<div id="wPreView" class="easyui-window" title="预览图片" data-options="modal:true,closed:true,iconCls:'icon-search'"
style="padding: 10px; width: auto">
</div>

</body>
</html>
