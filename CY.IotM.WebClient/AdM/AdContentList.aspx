<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdContentList.aspx.cs" Inherits="CY.IotM.WebClient.AdM.AdContentList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>广告内容管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />

    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.form.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/detailview.js" type="text/javascript" ></script>
     

    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Menu.js" type="text/javascript"></script>
    <script src="../Scripts/AdM/AdM.ContentList.js" type="text/javascript"></script>
    <script type="text/javascript">


    
        $(function () {
            IotM.CheckLogin();
            AdM.ContentList.LoadDataGridView();

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

    <style type="text/css">
        .auto-style1 {
            width: 98px;
        }
        .auto-style2 {
            height: 24px;
        }
        #CNLength {
            width: 79px;
        }
    </style>
</head>
<body>

    
    <div id="tb">
    <br>
        <a href="javascript:void(0)" menucode="tjzcqy" class="easyui-linkbutton" data-options="iconCls:'icon-back',plain:true"
            onclick="history.back()">返回主题</a> 
        &nbsp;&nbsp;&nbsp;&nbsp;
        <a href="javascript:void(0)" menucode="tjzcqy" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
            onclick="AdM.ContentList.OpenformAdd()">添加内容</a>
        &nbsp;&nbsp;
        <a href="javascript:void(0)" menucode="tjzcqy" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
            onclick="AdM.ContentList.Preview('<%=CompanyID %>','<%=ACID %>')">预览</a> 
        &nbsp;&nbsp;主题当前状态：<%=stateShowStr %>

    </div>
    <!--列表显示-->
    <div id="dataGrid"></div>

    <!--添加广告内容-->
    <div id="wAdd" class="easyui-window" title="添加广告内容" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd' name="formAdd"  method="post"  enctype="multipart/form-data" action="../Handler/AdmListHandler.ashx?AType=ADD">
              
                    <table style="border-spacing:10px;line-height:25px">
                         <tr>
                            <td>文件名称:
                            </td>
                            <td colspan="3">
                                <input type="hidden" name="AI_ID" id="AI_ID" default="" />
                                <input type="hidden" name="AC_ID" id="AC_ID" default="" runat="server" />
                                <input type="hidden" name="State" id="State" default="" runat="server" />
                                
                                <input class="easyui-validatebox" default="" type="text" name="FileName" id="FileName" />
                            </td>
                            
                        </tr>
                        <tr>
                            <td>文件序号:  </td>
                            <td colspan="3">
                              <input class="easyui-validatebox"  type="text" name="OrderID"    validtype="regNum[0,255]" default="0"  id="OrderID"  "/>
                            </td>
                        </tr>
                        <tr>
                            <td>启用时间:
                            </td>
                            <td>
                               <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser,editable:false" id="BDate"  name="BDate" style="width: 85px"/>
                            </td>

                              <td>停止时间:
                            </td>
                            <td class="auto-style1">
                             <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser,editable:false" id="EDate"  name="EDate" style="width: 85px"/>

                            </td>
                        </tr>
                        <tr style="display:none">
                            <td>显示状态:
                            </td>
                            <td colspan="3">
                               
                                <select id="IsDisplay" class="easyui-combobox" name="IsDisplay">
                                    <option value="true" selected="selected">显示</option>
                                    <option value="false" >不显示</option>
                                    
                                </select>
                            </td>

                        </tr>

                        
                          <tr>
                            <td class="auto-style2">显示时长:
                            </td>
                            <td colspan="3" class="auto-style2">
                                <input class="easyui-numberbox" type="text" name="Length" id="Length" min="1" max="120" value="10" /> (秒)
                            </td>

                          

                        </tr>

                  
                          <tr id="uploadTr">
                            <td >文件上传:
                            </td>
                            <td colspan="3"> 
                              <input id="UploadFile" type="file" name="UploadFile"   /><br/>
                            </td>
                          </tr>
                          <tr id="uploadShow" style="display:none;">
                            <td  >原地址:</td>
                            <td colspan="3"> 
                              <input id="StorePath" type="text" name="StorePath" style="border:none;width:300px;"  />
                              <input type="hidden" name="StoreName" id="StoreName"  />
                              <input type="hidden" name="FileLength" id="FileLength"  />

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


    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
</body>
</html>
