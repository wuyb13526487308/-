<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserCreate.aspx.cs" Inherits="CY.IotM.WebClient.UserCreate" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>创建用户</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>

     <script src="../Scripts/uploadify-v2.1.4/swfobject.js" type="text/javascript"></script>
     <script src="../Scripts/uploadify-v2.1.4/jquery.uploadify.v2.0.3.js" type="text/javascript"></script>

    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.User.js" type="text/javascript"></script>

   

 
   
 
    <script type="text/javascript">
    
        $(function () {
            IotM.CheckLogin();
          
            IotM.User.LoadDataGrid();

            IotM.User.LoadDataGridBatchAdd();

            IotM.regvalidatebox("formAdd");
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });


            IotM.Initiate.LoadStreetComboBox('CNStreet', false, true);
            $('#CNStreet').combobox(
            {
               onSelect: function (rec) {
                   IotM.Initiate.LoadCommunityComboBox('CNCommunity', false, true, rec.ID);
                   IotM.User.AdressChange();
               }
            });
            IotM.Initiate.LoadCommunityComboBox('CNCommunity', false, true, $('#CNStreet').combobox("getValue"));
            $('#CNCommunity').combobox(
            {
                onSelect: function (rec) { IotM.User.AdressChange(); }
            });

        });


    </script>
  
</head>
<body>
    <div id="wrap">
        <div id="tb">

       
         <div style="float:left;padding-right:50px">
         <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" menucode="tjyh" onclick="IotM.User.OpenformAdd()">单户创建</a>
              &nbsp;&nbsp;
         <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" menucode="tjyh" onclick="IotM.User.OpenformBatchAdd()">批量创建</a>
              &nbsp;&nbsp;
         <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" menucode="tjyh" onclick="IotM.User.OpenExcelAdd()">Excel导入</a>
         </div>

            &nbsp;&nbsp; &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserID"  />
           &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserName"  />
           &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Adress"  style="width:260px" />
           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.User.SerachClick()">查询</a> 
            
        </div>
        <div id="dataGrid">
        </div>

  
        <div id="wAdd" class="easyui-window" title="创建用户" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd'>
              
                    <table style="border-spacing:10px; ">
                        <tr>
                            <td>户号:</td>
                            <td>
                                <input type="hidden" name="CNCompanyID" default="" />
                                <input type="hidden" name="CNState" default="0" />
                                <input class="easyui-validatebox" type="text" name="CNUserID" default=""  disabled="disabled"/>
                            </td>
                             <td>户名:</td>
                            <td>
                              <input class="easyui-validatebox" type="text" name="CNUserName" default="" />
                            </td>

                        </tr>
                    
                        <tr>
                            <td>道路:</td>
                            <td colspan="3">
                                <input class="easyui-combobox" type="text" name="CNStreet" id="CNStreet" default=""  style="width:320px"/>
                            </td>
                        </tr>

                          <tr>
                            <td>小区:</td>
                            
                            <td colspan="3">
                               <input class="easyui-combobox" type="text" name="CNCommunity" id="CNCommunity" default=""  style="width:320px"/>
                            </td>
                        </tr>

                          <tr>
                            <td>门牌号:</td>
                            <td colspan="3">
                                <input class="easyui-validatebox" type="text" name="CNDoor" id="CNDoor" default="" onchange="IotM.User.AdressChange()" style="width:320px"/>
                            </td>
                             
                        </tr>

                         <tr>
                            <td>地址:</td>
                            <td colspan="3">
                                <input class="easyui-validatebox" id="CNAddress" type="text" name="CNAddress" default=""  style="width:320px" disabled="disabled"/>
                            </td>
                        </tr>



                         <tr>
                            <td></td>
                            <td colspan="3">
                                <input type="checkbox" id="ContineCreate"/>连续创建
                            </td>
                        </tr>
                      
                        <tr>

                            <td colspan="4" align="center">
                                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel">取消</a>
                            </td>
                        </tr>

                    </table>
                       
             
            </form>
        </div>



         <div id="wAdd_Batch" class="easyui-window" title="批量创建用户" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd_Batch'>
              
                    <table id="step1" style="border-spacing:10px; ">
                       
                    
                        <tr>
                            <td>道路:</td>
                            <td>
                                <input class="easyui-combobox" type="text" name="CNStreet" id="CNStreet_Batch"  style="width:320px"/>
                            </td>
                        </tr>

                          <tr>
                            <td>小区:</td>
                            
                            <td >
                               <input class="easyui-combobox" type="text" name="CNCommunity" id="CNCommunity_Batch"   style="width:320px"/>
                            </td>
                        </tr>

                          <tr>
                            <td>楼号:</td>
                            <td >
                                <input class="easyui-validatebox" type="text" name="CNLouNo" id="CNLouNo"   style="width:310px"/>
                            </td>
                             
                        </tr>

                            <tr>
                            <td>单元号:</td>
                            <td >
                                <input class="easyui-validatebox" type="text" name="CNUnitNo" id="CNUnitNo"   style="width:310px"/>
                            </td>
                             
                        </tr>

                            <tr>
                            <td>楼层数:</td>
                            <td >
                                <input class="easyui-numberspinner" type="text" name="CNLouCengNum" id="CNLouCengNum" 
                                    
                                    data-options="min:1,max:100"style="width:320px"/>
                            </td>
                             
                        </tr>

                            <tr>
                            <td>开始楼层:</td>
                            <td>
                                <input class="easyui-numberspinner" type="text" name="CNLouCengStart" id="CNLouCengStart"  data-options="min:1,max:100" style="width:320px"/>  
                            </td>
                             
                        </tr>


                         <tr>
                            <td>户向:</td>
                            <td >
                                <input class="easyui-combobox" id="CNHuXiang" type="text" name="CNHuXiang"  style="width:320px" />
                            </td>
                        </tr>

                         <tr>
                             <td>表类型:</td>
                            <td>
                              <input class="easyui-combobox" type="text" name="CNMeterType"  id="CNMeterType" style="width:320px" />
                            </td>
                         </tr> 
                      
                        <tr>

                            <td colspan="2" align="center">
                                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk_Batch">下一步</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel_Batch">取消</a>
                            </td>
                        </tr>

                    </table>
                       
            </form>


             <table id="step2">
                 <tr>

                     <td>
                         <div id="dataGrid_Preview"></div>
                     </td>
                 </tr>
                 <tr>
                     <td align="center">

                         
                          <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk_Batch_accept">确认编辑</a>
                          <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk_Batch_complete">完成</a> 
                         <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel_Batch_complete">取消</a>
                     </td>

                 </tr>


             </table>


        </div>




         <div id="importImg" class="easyui-window" title="Excel导入用户" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px;">


          <div id="unLoadDiv">

            <table id="fileTb" style="width: 100%">
                <tr>
                    <td colspan="2">
                        <div id="fileQueue"></div>
                    </td>
                </tr>

                <tr>
                    <td>
                        <input id="UploadFile" type="file" name="UploadFile" />
                   
                    </td>

                    <td>
                       <a href="#"  id="btnDown"><span style="color:blue" >点击下载Exccel文件模板</span></a>
                   
                    </td>
                  
                </tr>

                <tr>
                    <td  colspan="2" align="center">
                        <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'" id="btnUpLoad">下一步</a>
                         <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnUpLoadCancel">取消</a>
 
                    </td>
                </tr>

            </table>
          </div>

        </div>




         <div id="importImgStep" class="easyui-window" title="选择导入用户" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px;">


             <div id="dataGrid_Import"> </div>


            <div style="text-align:right;padding-right:50px;padding-top:10px">

                   <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk_Import">确定导入</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel_Importr">取消</a>

            </div>

     

        </div>


 </div>
  
</body>
</html>
