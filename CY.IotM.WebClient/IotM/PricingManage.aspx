<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PricingManage.aspx.cs" Inherits="CY.IotM.WebClient.PricingManage" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>调价计划管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.Pricing.js" type="text/javascript"></script>

    <script type="text/javascript">
    
        $(function () {
            IotM.CheckLogin();


            $("#select_RegisterDate").datebox("setValue", IotM.MyDateformatter(new Date()));

            $("#CNUseDate").datebox("setValue", IotM.MyDateformatter(new Date()));

            IotM.Initiate.LoadFaMenStatusComboBox("select_State", true, false);

            IotM.Initiate.LoadComboxGridPriceType('CNPriceType', true, false);

            IotM.Pricing.LoadDataGrid();

            IotM.Pricing.LoadDataGridList();
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

           &nbsp;&nbsp;申请日期&nbsp;&nbsp;

            <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
            id="select_RegisterDate" style="width: 90px"/>

           &nbsp;&nbsp;&nbsp;&nbsp;状态&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_State"  />
           &nbsp;&nbsp;&nbsp;&nbsp;区域&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Street"  style="width:260px" />

           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.Pricing.SerachClick()">查询</a> 
           &nbsp;&nbsp;&nbsp;&nbsp;
           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" id="btnKaiFa" onclick="IotM.Pricing.OpenformAdd()">调价</a> 
   

           
        </div>
        <div id="dataGrid">
        </div>
        <div id="wAdd" class="easyui-window" title="调价计划申请" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd'>
              
                    <table>

                         <tr>
                            <td>任务号:
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNTaskID"  disabled="disabled" />    
                               <input type="hidden" name="CNCompanyID" default="" />
                               <input type="hidden" name="CNState" default="0" />
                            </td>
                        </tr>

                       <tr id="userListTR">

                           <td>用户列表:</td>
                           <td>
                               <table>

                                   <tr>
                                       <td>
                                             <div id="dataGrid_list"></div>

                                       </td>

                                       <td>
                                             <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnSelAllUser" onclick="IotM.Pricing.SelAllUser()">所有用户</a><br />
                                             <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnSelCommunity" onclick="IotM.Pricing.SelCommunity()">选择小区</a><br />
                                             <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnSelUser" onclick="IotM.Pricing.SelUser()">选择用户</a><br />
                                             <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-reload'" id="btnClean" onclick="IotM.Pricing.CleanSelect()">清除选择</a><br />

                                       </td>
                                   </tr>

                               </table>
                           </td>

                        </tr>


                        <tr>
                            <td>总户数:
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNTotal" id="CNTotal"  disabled="disabled"  default=""/>    
                            </td>
                        </tr>


                         <tr>
                            <td>备注:
                            </td>
                            <td>
                                <textarea id="CNContext"  name="CNContext" style="width:260px" default="" ></textarea>
                            </td>
                         
                        </tr>


                        
                         <tr>
                            <td>价格类型:
                            </td>
                            <td>
                               <%--  <input class="easyui-combobox" type="text" name="CNPriceType" id ="CNPriceType" style ="width:300px" default=""  />--%>
                               <input class="easyui-combogrid"  type="text" name="CNPriceType" style ="width:300px"  id ="CNPriceType"  default=""  />
                            </td>
                         
                        </tr>


                       <tr style="display:none">
                       <td></td>
                       <td colspan ="3">
                           <input class="easyui-validatebox" type="text" name="Description" style ="height:40px; width:300px" disabled="disabled" default="" />

                       </td>
                       </tr>



                       <tr>
                            <td>启用日期:
                            </td>
                            <td>
                                <input id="CNUseDate"  name="CNUseDate"  class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
                                     default=""  />
                            </td>
                         
                        </tr>
                   

                        <tr>

                            <td colspan="2" align="center">
                                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel">取消</a>
                            </td>
                        </tr>

                    </table>
                       
             
            </form>
        </div>
  </div>  



      <div id="deleteUserDiv" class="easyui-window"  data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">

             <div id="tb_deleteUser">

            &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_StreetDelete"  />
            &nbsp;&nbsp;&nbsp;&nbsp;表号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Community"  />
            &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_AdressDel"  style="width:260px" />
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelectDel" onclick="IotM.Pricing.DeleteSerachClick()">查询</a> 
            &nbsp;&nbsp;&nbsp;&nbsp;
      
      
              </div>


              <div id="dataGrid_deleteUser">
              </div>


            <div style="text-align:right;padding-right:50px;padding-top:10px">

                   <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOk_deleteUser">确定选择</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancel_deleteUser">取消</a>

            </div>

        </div>



       <div id="GetUserDiv" class="easyui-window"  data-options="modal:true,closed:true,iconCls:'icon-search'"
            style="padding: 10px; width: auto">

              <div id="dataGrid_getUser">
              </div>


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
   
  
</body>
</html>
