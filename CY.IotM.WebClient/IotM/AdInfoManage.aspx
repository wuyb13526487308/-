<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdInfoManage.aspx.cs" Inherits="CY.IotM.WebClient.AdInfoManage" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>广告管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />

    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />


    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.form.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>


    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.AdInfo.js" type="text/javascript"></script>



    <script type="text/javascript">


    
        $(function () {
            IotM.CheckLogin();
         
            IotM.Initiate.LoadAdShowStateComboBox("select_Show", true, false);

            IotM.Initiate.LoadAdShowStateComboBox("ShowStatus", false, false);

            IotM.Initiate.LoadAdPublishStateComboBox('select_Publish', true, false);

            $("#txtDate1").datebox("setValue", IotM.MyDateformatter(new Date().dateAdd('m', -1)));
            $("#txtDate2").datebox("setValue", IotM.MyDateformatter(new Date().dateAdd('m', 1)));
       

            IotM.AdInfo.LoadDataGrid();

            IotM.regvalidatebox("formAdd");
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });


            $("#UploadFile").uploadPreview({
                Img: "ImgPr",
                Width: 180,
                Height: 120,
                ImgType: [ "jpg", "bmp", "txt"],
                Callback: function () {
                    $("#preview").show();
                }

            });
        });


    </script>
  
</head>
<body>

    <div id="wrap">
        <div id="tb">
          

            <div style="float:left;padding-right:50px">
             <a href="javascript:void(0)" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true" menucode="tjglwj" onclick="IotM.AdInfo.OpenformAdd()">新建广告</a>
              &nbsp;&nbsp;

            </div>
           &nbsp;&nbsp;文件名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_FileName"  />
           &nbsp;&nbsp;&nbsp;&nbsp;发布状态&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Publish"  />
           &nbsp;&nbsp;&nbsp;&nbsp;显示状态&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Show" />

            
                 &nbsp;&nbsp; &nbsp;&nbsp;开始时间：<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
            id="txtDate1" style="width: 90px"/>
        &nbsp;&nbsp;停止时间：<input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
            id="txtDate2" style="width: 90px"/>
             &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;

           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.AdInfo.SerachClick()">查询</a> 
            &nbsp;&nbsp;&nbsp;&nbsp;
          

        </div>
        <div id="dataGrid">
        </div>



        
       <div id="wAdd" class="easyui-window" title="新建广告" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd' name="formAdd"  method="post"  enctype="multipart/form-data" action="../Handler/AdInfoHandler.ashx?AType=Add">
              
                    <table style="border-spacing:10px;">


                         <tr>
                            <td>文件名称:
                            </td>
                            <td colspan="3">
                                <input type="hidden" name="ID" id="ID" default="" />
                                <input type="hidden" name="CompanyID" id="CompanyID" default="" />
                                <input type="hidden" name="PublishStatus" id="PublishStatus" default="" />
                                <input class="easyui-validatebox" default="" type="text" name="FileName" id="FileName" />
                            </td>
                            
                        </tr>

                    
                        <tr>
                            <td>文件序号:  </td>
                            <td colspan="3">
                              <input class="easyui-validatebox"  type="text" name="FileIndex"    validtype="regNum[1,255]" default=""  id="FileIndex"  required="true" missingmessage="请输入文件序号"/>
                            </td>
                           

                        </tr>
                      

                        <tr>
                            <td>开始时间:
                            </td>
                            <td>
                               <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser,editable:false" id="StartDate"  name="StartDate" style="width: 90px"/>
                            </td>

                              <td>结束时间:
                            </td>
                            <td>
                             <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser,editable:false" id="EndDate"  name="EndDate" style="width: 90px"/>

                            </td>
                           

                        </tr>


                        <tr>
                            <td>显示状态:
                            </td>
                            <td colspan="3">
                               <input class="easyui-validatebox" type="text" id="ShowStatus"  name="ShowStatus"/>
                            </td>

                        </tr>

                        
                          <tr>
                            <td>轮循时长:
                            </td>
                            <td colspan="3">
                                <input class="easyui-numberbox" type="text" name="CycleTime" id="CycleTime"  />秒
                            </td>

                          

                        </tr>

                  
                          <tr id="uploadTr">
                            <td>文件上传:
                            </td>
                            <td colspan="3"> 
                              <input id="UploadFile" type="file" name="UploadFile"   />

                            </td>
                        </tr>



                        <tr id="preview" style="display:none">
                            <td>预览:
                            </td>
                            <td colspan="3">

                               <div><img  id="ImgPr" width="180" height="120" /></div>
                               
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


        <div id="wPreView" class="easyui-window" title="预览图片" data-options="modal:true,closed:true,iconCls:'icon-search'"
            style="padding: 10px; width: auto">
        </div>


       
    </div>
  
</body>
</html>
