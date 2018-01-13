<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdPublish.aspx.cs" Inherits="CY.IotM.WebClient.AdM.AdPublish" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>广告发布管理</title>
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
    <script src="../Scripts/AdM/AdM.Publish.js" type="text/javascript"></script>
    <script src="../Scripts/AdM/easyui-lang-zh_CN.js" type="text/javascript"></script>
    <script type="text/javascript">

    $(function () {
        IotM.CheckLogin();
        AdM.Publish.LoadDataGridView();
        AdM.Publish.LoadDataGridList("");
        //加载广告列表
        AdM.Publish.LoadADContextComboBox('CNAC_ID', false, true);

        $(window).resize(function () {
            IotM.SetMainGridWidth(1);
            IotM.SetMainGridHeight(0.99);
            $("#dataGrid").datagrid("resize");
        });
        

        //$("#CNStateShow").change(function () {
        //    //alert($("#CNStateShow").attr("checked"));
        //    if ($("#CNStateShow").attr("checked") == "checked") { $("#CNState").val('1'); } else { $("#CNState").val('0'); }
        //    //alert($("#CNState").val())
        //});
    });

    </script>
</head>
<body>

     <!--查询内容-->
    <div id="tb">&nbsp;&nbsp;
     区域描述：&nbsp;&nbsp<input class="easyui-validatebox" type="text" id="seachAreaContext"  name="seachAreaContext"  style="width:150px"/> &nbsp;&nbsp;&nbsp;&nbsp;
     状态：&nbsp;&nbsp;
        <select id="seachState" class="easyui-combobox" name="seachState" style="width:90px" >
            <option value="" selected="selected">全部</option>
            <option value="0" >未发布</option>
            <option value="1" >已发布</option>
            <option value="2" >重新发布</option>
        </select> &nbsp;&nbsp;&nbsp;&nbsp;
    创建日期&nbsp;&nbsp;<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"  id="bDate" name="bDate" style="width: 90px"/>
        <input id="bDateTime" class="easyui-timespinner" style="width: 55px;"/>&nbsp;&nbsp;&nbsp;
     至&nbsp;&nbsp;&nbsp;<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"  id="eDate" name="eDate"style="width: 90px"/>
        <input id="eDateTime" class="easyui-timespinner" style="width: 55px;"/>
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="AdM.Publish.SerachClick()">查询</a> &nbsp;&nbsp;<br />&nbsp;&nbsp;        
    <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
            onclick="AdM.Publish.PublishAddUser()">添加发布</a>
    </div>
    <!--列表显示-->
    <div id="dataGrid"></div>


      <div id="wAdd" class="easyui-window" title="发布管理" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd' name="formAdd">
                    <table style="width:100%">
                         <tr>
                            <td style="height:25px">广告主题：</td>
                            <td>
                                <select id="CNAC_ID" class="easyui-combobox" id="CNAC_ID"  name="CNAC_ID" style="width:250px" ></select> 
                               <input type="hidden" id="CNContext"  name="CNContext" default="" />
                               <input type="hidden"  id="CNAP_ID"  name="CNAP_ID" default="0" />
                            </td>
                        </tr>
                       <tr id="userListTR">
                           <td style="width:70px">用户列表：</td>
                           <td>
                               <table style="width:100%">
                                   <tr>
                                       <td style="width:75%">
                                             <div id="dataGrid_list"></div>
                                       </td>
                                       <td style="width:25%">
                                             <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnSelAllUser" onclick="AdM.Publish.SelAllUser()">所有用户</a><br />
                                             <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnSelCommunity" onclick="AdM.Publish.SelCommunity()">选择小区</a><br />
                                             <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnSelUser" onclick="AdM.Publish.SelUser()">选择用户</a><br />
                                             <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-reload'" id="btnClean" onclick="AdM.Publish.CleanSelect()">清除选择</a><br />

                                       </td>
                                   </tr>

                               </table>
                           </td>

                        </tr>
                        <tr>
                            <td style="height:25px">总户数：
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNUserCount" id="CNUserCount"  disabled="disabled"  default=""/>    
                            </td>
                        </tr>


                         <tr>
                            <td style="height:60px">区域说明： </td>
                            <td>
                                <textarea id="CNAreaContext"  name="CNAreaContext" style="width:260px" default="" ></textarea>
                            </td>
                         
                        </tr>
                        <tr>
                                <td style="height:25px">
                                    发布日期：
                                </td>
                                <td>
                                   <input class="easyui-datetimebox"  style="width:150px" id="CNPublishDate" name="CNPublishDate"/>
                                    <input type="hidden"  id="PublishDateOld"  name="PublishDateOld" default="0" />
                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; <input type="checkbox"  id="CNStateShow"  name ="CNStateShow"  />&nbsp;&nbsp;直接发布
                                    <input type="hidden" name="CNState" value="0" id="CNState" />
                                </td>
                            </tr>
                        <tr>
                            <td colspan="2" align="center" style="height:30px">
                                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel">取消</a>
                            </td>
                        </tr>
                    </table>         
            </form>
        </div>

    
       <div id="GetCommunityDiv" class="easyui-window"  data-options="modal:true,closed:true,iconCls:'icon-search'"
            style="padding: 10px; width: auto">

              <div id="dataGrid_Community">
              </div>


           <div style="text-align:right;padding-right:50px;padding-top:10px">

                   <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk_Community">确定选择</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel_Community">取消</a>

            </div>

       </div>
    
       <div id="choseUserDiv" class="easyui-window"  data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">

             <div id="tb_choseUser">
                &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_StreetDelete"  />
                &nbsp;&nbsp;&nbsp;&nbsp;表号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Community"  />
                &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_AdressDel"  style="width:260px" />
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelectDel" onclick="IotM.SetAlarm.DeleteSerachClick()">查询</a> 
                &nbsp;&nbsp;&nbsp;&nbsp; 
              </div>
              <div id="dataGrid_choseUser">
              </div>


            <div style="text-align:right;padding-right:50px;padding-top:10px">

                   <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk_deleteUser">确定选择</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel_deleteUser">取消</a>

            </div>

        </div>

        
       <div id="AdPUserInfo" class="easyui-window"  data-options="modal:true,closed:true,iconCls:'icon-search'"
            style="padding: 10px; width: auto">

              <div id="dataGrid_AdPUserInfo">
              </div>
       </div>
    
    
</body>
</html>
