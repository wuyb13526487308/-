<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StreetManage.aspx.cs" Inherits="CY.IotM.WebClient.StreetManage" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>街道管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.Street.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.Community.js" type="text/javascript"></script>


 
   
 
    <script type="text/javascript">
    
        $(function () {
            IotM.CheckLogin();
            IotM.Initiate.InitStreet();
            IotM.Street.LoadDataGrid();
            IotM.Community.LoadDataGrid(IotM.Initiate.Street);
            IotM.regvalidatebox("formAdd");
          
        });


    </script>
  
</head>
<body>

    <div id="wrap" class="easyui-layout" fit="true">
      

        <div id="StreetDiv" data-options="region:'west',title:'街道列表',collapsible:false,border:false,split:true" style="width:600px;padding:1px">

         <div id="tb">
         <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" menucode="tjyh" onclick="IotM.Street.OpenformAdd()">添加街道</a>
         <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" menucode="tjyh" onclick="IotM.Street.OpenformEdit()">编辑街道</a>
         <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" menucode="tjyh" onclick="IotM.Street.RemoveClick()">删除街道</a>
        
        </div>
        <div id="dataGrid">
        </div>

           
        </div>



        <div  id="CommunityDiv" data-options="region:'center',title:'小区列表',border:false"  style="padding:1px">
           

         <div id="tbCommunity">
         <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" menucode="tjyh" onclick="IotM.Community.OpenformAdd()">添加小区</a>
         <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true" menucode="tjyh" onclick="IotM.Community.OpenformEdit()">编辑小区</a>
         <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true" menucode="tjyh" onclick="IotM.Community.RemoveClick()">删除小区</a>
        
        </div>
        <div id="dataGridCommunity">
        </div>


        </div>


           <div id="wAdd" class="easyui-window" title="添加街道" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd'>
              
                    <table>
                        <tr>
                            <td>街道编号:
                            </td>
                            <td>
                                <input type="hidden" name="CNID" default="" />
                                <input type="hidden" name="CNCompanyID" default="" />
                                <input class="easyui-validatebox" type="text" name="CNSer" default="" />
                                
                            </td>
                         
                        </tr>

                         <tr>
                            <td>街道名称:
                            </td>
                            <td> 
                            <input class="easyui-validatebox" type="text" name="CNName" default="" />
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


          <div id="wAddCommunity" class="easyui-window" title="添加小区" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAddCommunity'>
              
                    <table>
                        <tr>
                            <td>小区编号:
                            </td>
                            <td>
                                <input type="hidden" name="CNID" default="" />
                                <input type="hidden" name="CNCompanyID" default="" />
                                <input class="easyui-validatebox" type="text" name="CNSer" default="" />
                                
                            </td>
                         
                        </tr>


                         <tr>
                            <td>所属街道:
                            </td>
                            <td> 
                            <input class="easyui-combobox" type="text" name="CNStreetID" id="CNStreetID" />

                            </td>
                         
                        </tr>

                         <tr>
                            <td>小区名称:
                            </td>
                            <td> 
                            <input class="easyui-validatebox" type="text" name="CNName" default="" />
                            </td>
                         
                        </tr>
                      
                        <tr>

                            <td colspan="2" align="center">
                                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                                    id="btnOkC">确定</a> <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                                        id="btnCancelC">取消</a>
                            </td>
                        </tr>

                    </table>

            </form>
        </div>






    </div>
  
</body>
</html>
