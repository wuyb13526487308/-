<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PriceParManage.aspx.cs" Inherits="CY.IotM.WebClient.PriceParManage" %>


<!DOCTYPE HTML>
<html>
<head>
    <title>价格参数管理</title>
    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.PricePar.js" type="text/javascript"></script>


 
   
 
    <script type="text/javascript">
    
        $(function () {
            IotM.CheckLogin();
            IotM.PricePar.LoadDataGrid();
            IotM.regvalidatebox("formAdd");

            IotM.Initiate.LoadSettlementTypeComboBox('CNSettlementType', false, true);

            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });


            $("#CNLadder").numberspinner({
                onChange: IotM.PricePar.LadderChange  
            })


        });
    </script>
  
</head>
<body>

    <div id="wrap">

         <div id="tb">
          <a href="javascript:void(0)" menucode="tjjglx" class="easyui-linkbutton" data-options="iconCls:'icon-add',plain:true"
            onclick="IotM.PricePar.OpenformAdd()">添加价格类型</a>
          <a href="javascript:void(0)" menucode="tjjglx" class="easyui-linkbutton" data-options="iconCls:'icon-edit',plain:true"
            onclick="IotM.PricePar.OpenformEdit()">编辑价格类型</a>
          <a href="javascript:void(0)" menucode="tjjglx" class="easyui-linkbutton" data-options="iconCls:'icon-remove',plain:true"
            onclick="IotM.PricePar.RemoveClick()">删除价格类型</a>
        </div>

      
        <div id="dataGrid">
        </div>

        <div>



        </div>



        <div id="wAdd" class="easyui-window" title="添加价格参数" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd'>
              
                    <table>
                        <tr>
                            <td>价格名称:
                            </td>
                            <td>
                                <input type="hidden" name="CNID" default="" />
                                <input type="hidden" name="CNCompanyID" default="" />
                                <input class="easyui-validatebox" default="" type="text" name="CNPriceName" required="true"
                                    missingmessage="请输入价格类型名称" />
                            </td>
                            <td colspan="2">

                                <input  type="checkbox" name="CNIsUsed"  id="CNIsUsed" onchange="IotM.PricePar.IsUsedChange()"/>  启用阶梯价

                            </td>
                        </tr>
                        <tr>
                            <td>结算周期:  </td>
                            <td>
                               <input name="CNSettlementType" id="CNSettlementType" class="easyui-combobox" />
                            </td>
                             <td>结算日:  </td>
                             <td>
                             
                                  <input class="easyui-validatebox" style="width:50px" type="text" name="CNSettlementMonth"  validtype="regNum[1,12]" default="1"   id="CNSettlementMonth"/>月
                                  <input class="easyui-validatebox" style="width:50px" type="text" name="CNSettlementDay"  validtype="regNum[1,31]" default="1"   id="CNSettlementDay"/>日

                             </td>

                        </tr>
                        <tr>
                            <td>阶梯数:
                            </td>
                            <td>
                            <input class="easyui-numberspinner" id="CNLadder"  name="CNLadder"  value="2" data-options="min:1,max:5"  required="true" />
                            </td>

                        </tr>

                        <tr>
                            <td>阶梯1--价格:
                            </td>
                            <td>
                                <input class="easyui-numberbox" type="text" name="CNPrice1" id="CNPrice1" default=""  precision="2"   required="true"/>
                            </td>

                            <td>用量:
                            </td>
                            <td>
                              <input class="easyui-validatebox" type="text" name="CNGas1" id="CNGas1"  validtype="number" default="" />
                            </td>

                        </tr>
                        
                          <tr>
                            <td>阶梯2--价格:
                            </td>
                            <td>
                                <input class="easyui-numberbox" type="text" name="CNPrice2" id="CNPrice2" default=""  precision="2"  />
                            </td>

                            <td>用量:
                            </td>
                            <td>
                              <input class="easyui-validatebox" type="text" id="CNGas2" name="CNGas2" default="" />
                            </td>

                        </tr>
                          <tr>
                            <td>阶梯3--价格:
                            </td>
                            <td>
                                <input class="easyui-numberbox" type="text" name="CNPrice3" id="CNPrice3" default=""  precision="2"  />
                            </td>

                            <td>用量:
                            </td>
                            <td>
                              <input class="easyui-validatebox" type="text" name="CNGas3" id="CNGas3" default="" />
                            </td>

                        </tr>



                          <tr>
                            <td>阶梯4--价格:
                            </td>
                            <td>
                                <input class="easyui-numberbox" type="text" name="CNPrice4" id="CNPrice4" default=""  precision="2"  />
                            </td>

                            <td>用量:
                            </td>
                            <td>
                              <input class="easyui-validatebox" type="text" name="CNGas4" id="CNGas4"  default="" />
                            </td>

                        </tr>


                          <tr>
                            <td>阶梯5--价格:
                            </td>
                            <td>
                                <input class="easyui-numberbox" type="text" name="CNPrice5" id="CNPrice5" default=""  precision="2"  />
                            </td>

                            <td>
                            </td>
                            <td>
                            </td>

                        </tr>


                          <tr>
                              <td>启用日期:
                            </td>
                            <td>
                                 <input class="easyui-datebox" default=""  data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser" id="CNPeriodStartDate"  />
                            </td>
                             <td colspan="2">
                                 <span style="color:red">气量表结算使用</span>
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
  </div>  
  
</body>
</html>
