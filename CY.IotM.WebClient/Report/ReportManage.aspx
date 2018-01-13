<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportManage.aspx.cs" Inherits="CY.IotM.WebClient.ReportManage" %>

<%@ OutputCache CacheProfile="WebClientForEver" %>
<!DOCTYPE HTML>
<html>
<head>
    <title>报表模板管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify-v2.1.4/jquery.uploadify.v2.0.3.js" type="text/javascript"></script>
    <script src="../Scripts/uploadify-v2.1.4/swfobject.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.ReportManage.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function getCookie_() {
            IotM.GetCookie();
            return IotM.WebCookie;
        }
        $(function () {
            IotM.CheckLogin();
            IotM.ReportManage.LoadCompanyReportDataGrid();
            $('#UploadFile').uploadify({
                'uploader': '../Scripts/uploadify-v2.1.4/uploadify.swf',       //上传文件的进度条
                'script': '../Handler/Report/UploadReportTemplateHandler.ashx',         //上传文件的后台处理页面 
                'scriptData': { isUpdate: $("#chkUpdate").attr("checked"), NO_COOKIE_SessionId: getCookie_() },
                'cancelImg': '../Scripts/uploadify-v2.1.4/uploadify-cancel.png',     //取消上传的图片
                'auto': false,
                'multi': false,
                'sizeLimit': 1024 * 1024 * 3,
                'simUploadLimit': 1, //上传文件大小的限制            
                'fileDesc': '报表模板.pkf',
                'fileExt': '*.pkf',
                'buttonText': '选择模板',
                'onComplete': function (event, queueID, fileObj, response, data) {
                    try {
                        var tResult = eval('(' + response + ')');
                        if (tResult.Result) {
                            $("#txtInfo").html(tResult.TxtMessage);
                            IotM.ReportManage.LoadCompanyReportDataGrid();
                        }
                        else {
                            $("#txtInfo").html("上传失败。");
                        }

                    } catch (e) {

                    }

                }
            });
        });  
    </script>
</head>
<body>
    <div id="tb">
        <a href="javascript:void(0)" menucode="drbbmb" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
            onclick="IotM.ReportManage.OpenformImport()">导入报表</a>
        <div style="margin: 5px 2px;">
        </div>
        <input class="easyui-searchbox" data-options="prompt:'请输入检索关键字',menu:'#mm',searcher:IotM.ReportManage.SerachClick"
            style="width: 300px"></input>
        <div id="mm" style="width: 120px">
            <div data-options="name:'RID'">
                报表编号</div>
            <div data-options="name:'ReportName'">
                报表名称</div>
        </div>
    </div>
    <div id="dataGrid">
    </div>
    <form id='formAdd'>
    <div id="wAddReport" class="easyui-window" title="导入报表" data-options="modal:true,closed:true,iconCls:'icon-add'"
        style="padding: 10px;">
        <div class="demo-info">
            <div class="demo-tip icon-tip">
            </div>
            <div id="txtInfo">
                请选择要导入的报表模板，点击确定进行新建报表或更新报表的操作。</div>
        </div>
        <table>
            <tr>
                <td style="width: 128px">
                    <input id="UploadFile" type="file" name="UploadFile" />
                </td>
                <td>
                    <input type="checkbox" id="chkUpdate" menucode="gxbbmb" />更新
                </td>
            </tr>
            <tr>
                <td align="right" colspan="2">
                    <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                        id="btnOk">确定</a>&nbsp; &nbsp; &nbsp; &nbsp; <a href="#" class="easyui-linkbutton"
                            data-options="plain:true,iconCls:'icon-cancel'" id="btnCancel">取消</a>
                </td>
            </tr>
        </table>
    </div>
    <div id="wEditReportName" class="easyui-window" title="编辑报表名称" data-options="modal:true,closed:true,iconCls:'icon-add'"
        style="padding: 10px;">
        <table>
            <tr>
                <td>
                    报表编号:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNRID" default="" edit="false"
                        required="true" validtype='regNum[1]' />
                </td>
            </tr>
            <tr>
                <td>
                    报表名称:
                </td>
                <td>
                    <input class="easyui-validatebox" type="text" name="CNReportName" default="" required="true"
                        missingmessage="请输入报表名称" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                        id="btnEditReportNameOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                            id="btnEditReportNameCancel">取消</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
