<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetUpLoadDate.aspx.cs" Inherits="CY.IotM.WebClient.IotM.SetUpLoadDate" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>设置上传周期</title>

    <link href="../Scripts/easyui1.3.3/themes/default/easyui.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/uploadify-v2.1.4/uploadify.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/icon.css" rel="stylesheet" type="text/css" />
    <link href="../Scripts/easyui1.3.3/themes/demo.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/jquery.easyui.min.js" type="text/javascript"></script>
    <script src="../Scripts/easyui1.3.3/plugins/jquery.validatebox.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.js" type="text/javascript"></script>
    <script src="../Scripts/IotM.Initiate.js" type="text/javascript"></script>
    <script src="../Scripts/IotM/IotM.SetUpLoadDate.js"></script>

    <script type="text/javascript">
        //$(function () {
        //    IotM.CheckLogin();
        //    IotM.SetUpLoadDate.LoadDataGrid();
        //    IotM.regvalidatebox("formAdd");
        //    $(window).resize(function () {
        //        IotM.SetMainGridWidth(1);
        //        IotM.SetMainGridHeight(0.99);
        //        $("#dataGrid").datagrid("resize");
        //    });
        //});


        $(function () {
            IotM.CheckLogin();
            IotM.SetUpLoadDate.LoadDataGrid();
            IotM.regvalidatebox("formAdd");
            $(window).resize(function () {
                IotM.SetMainGridWidth(1);
                IotM.SetMainGridHeight(0.99);
                $("#dataGrid").datagrid("resize");
            });
            $("#select_RegisterDate").datebox("setValue", IotM.MyDateformatter(new Date()));
            IotM.Initiate.LoadFaMenStatusComboBox("select_State", true, false);
            IotM.Initiate.LoadJieSuanZhouQiTypeComboBox("select_Day", false, false);
        });
        $.extend($.fn.validatebox.defaults.rules, {
            DayRex: {
                validator: function (value) {
                    var rex = /^\d{1,2}$/;
                    if (rex.test(value)) {
                        if (parseInt(value, 10) <= 31) {
                            return true;
                        } else {
                            return false;
                        }
                    } else {
                        return false;
                    }
                },
                message: '请输入正确的日期'
            }
        });
        $.extend($.fn.validatebox.defaults.rules, {
            HourRex: {
                validator: function (value) {
                    var rex = /^\d{1,2}$/;
                    if (rex.test(value)) {
                        if (parseInt(value, 10) <= 59) {
                            return true;
                        } else {
                            return false;
                        }
                    } else {
                        return false;
                    }
                },
                message: '请输入正确的小时'
            }
        });
        $.extend($.fn.validatebox.defaults.rules, {
            MinuRex: {
                validator: function (value) {
                    var rex = /^\d{1,2}$/;
                    if (rex.test(value)) {
                        if (parseInt(value, 10) <= 59) {
                            return true;
                        } else {
                            return false;
                        }
                    } else {
                        return false;
                    }
                },
                message: '请输入正确的分钟'
            }
        });
    </script>
</head>
<body>
    <%-- <form id="form1" runat="server">--%>
    <div id="wrap">
        <div id="tb">
            &nbsp;&nbsp;申请日期&nbsp;&nbsp;
                <input id="hidType" type="hidden" />
            <input class="easyui-datebox" data-options="formatter:IotM.MyDateformatter,parser:IotM.MyDateparser"
                id="select_RegisterDate" style="width: 90px" />

            &nbsp;&nbsp;&nbsp;&nbsp;状态&nbsp;&nbsp;<input class="easyui-combobox" type="text" id="select_State" />
            &nbsp;&nbsp;&nbsp;&nbsp;区域&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_Street" style="width: 260px" />

            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelect" onclick="IotM.SetUpLoadDate.SerachClick()">查询</a>
            &nbsp;&nbsp;&nbsp;&nbsp;
           <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-edit'" id="btnKaiFa" onclick="IotM.SetUpLoadDate.OpenformAdd()">设置上传周期</a>



        </div>
        <div id="dataGrid">
        </div>
        <div id="wAdd" class="easyui-window" title="设置上传周期" data-options="modal:true,closed:true,iconCls:'icon-add'"
            style="padding: 10px; width: auto">
            <form id='formAdd'>

                <table>

                    <tr>
                        <td>任务号:
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNTaskID" disabled="disabled" />
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
                                        <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnSelAllUser" onclick="IotM.SetUpLoadDate.SelAllUser()">所有用户</a><br />
                                        <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnSelCommunity" onclick="IotM.SetUpLoadDate.SelCommunity()">选择小区</a><br />
                                        <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-add'" id="btnSelUser" onclick="IotM.SetUpLoadDate.SelUser()">选择用户</a><br />
                                        <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-reload'" id="btnClean" onclick="IotM.SetUpLoadDate.CleanSelect()">清除选择</a><br />

                                    </td>
                                </tr>

                            </table>
                        </td>

                    </tr>


                    <tr>
                        <td>总户数:
                        </td>
                        <td>
                            <input class="easyui-validatebox" type="text" name="CNTotal" id="CNTotal" disabled="disabled" />
                        </td>
                    </tr>


                    <tr>
                        <td>区域描述:
                        </td>
                        <td>
                            <textarea id="CNContext" style="width: 260px"></textarea>
                        </td>

                    </tr>


                    <%--报警参数--%>

                    <%--第0 长期未与服务器通讯报警             0：关闭    1：开启
                        第1 燃气漏气切断报警                 0：关闭    1：开启
                        第2 流量过载切断报警                 0：关闭    1：开启
                        第3 异常大流量切断报警                     0：关闭    1：开启
                        第4 异常微小流量切断报警                 0：关闭    1：开启
                        第5 持续流量超时切断报警                 0：关闭    1：开启
                        第6 燃气压力过低切断报警                 0：关闭    1：开启
                        第7 长期未使用切断报警                     0：关闭    1：开启
                        第8 移动报警/地址震感器动作切断报警0：关闭    1：开启--%>


                    <tr>
                        <td valign="top" style="">上传周期:
                        </td>
                        <td valign="top">
                            <table>

                                <tr>
                                    <td>


                                        <input class="easyui-combobox" type="text" id="select_Day" />
                                    </td>

                                    <td>
                                        <input class="easyui-validatebox" required="true" missingmessage="请输入日" data-options="validType:'DayRex'" type="text" name="txtDay" id="txtDay" />日
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>

                                    <td>
                                        <input class="easyui-validatebox" required="true" missingmessage="请输入时，输入范围：00~23" data-options="validType:'HourRex'" type="text" name="txtHour" id="txtHour" />时
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>

                                    <td>
                                        <input class="easyui-validatebox" required="true" missingmessage="请输入分，输入范围：00~59" data-options="validType:'MinuRex'" type="text" name="txtMinute" id="txtMinute" />分
                                    </td>
                                </tr>
                            </table>
                        </td>



                        <%-- <td colspan="2">

                                <table>

                        <tr>
                            <td>
                                 <input  type="checkbox" id="switch0" />长期未与服务器通讯报警
                            </td>
                            <td>
                                   <input class="easyui-validatebox" type="text" name="CNPar1" default="30" />天
                            </td>
                         
                    
                            <td>
                                  <input  type="checkbox"  id="switch1" />燃气漏泄切断报警
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNPar2" default="30" />秒
                            </td>
                         
                        </tr>

                        <tr>
                            <td>
                                 <input  type="checkbox"  id="switch2"/>燃气流量过载切断报警:
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNPar3" default="30" />秒
                            </td>
                         
                 
                            <td>
                                <input  type="checkbox" id="switch3"/>异常大流量切断报警:
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNPar5" default="30" />秒
                            </td>
                         
                        </tr>
                             

                       <tr>
                            <td>
                                <input  type="checkbox" id="switch4" />异常小流量切断报警:
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNPar6" default="30" />h
                            </td>
                         
                    
                            <td>
                                <input  type="checkbox" id="switch5" />持续流量超时切断报警:
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNPar7" default="30" />h
                            </td>
                         
                        </tr>




                        
                       <tr>
                           
                            <td>
                                <input  type="checkbox" id="switch7"/>长期未使用切断报警:
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNPar8" default="30" />天
                            </td>
                         
                        </tr>


                        
                       <tr>
                            <td colspan="2">
                                <input  type="checkbox" id="switch8"/>移动报警/地址震感器动作切断报警
                            </td>
                          


                            <td>
                                <input  type="checkbox" id="switch6"/>燃气压力过低切断报警
                            </td>
                            <td>
                            
                            </td>


 
                        </tr>


                          <tr>

                            <td>
                                异常大流量:
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNPar4" default="" />m³/h &nbsp;&nbsp;
                            </td>


                            <td>
                                燃气表公称流量:
                            </td>
                            <td>
                               <input class="easyui-validatebox" type="text" name="CNPar9" default="" />m³/h
                            </td>
                         
                        </tr>

                                </table>

                            </td>--%>
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





    <div id="deleteUserDiv" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-add'"
        style="padding: 10px; width: auto">

        <div id="tb_deleteUser">
            &nbsp;&nbsp;户号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_userIDDelete" />
            &nbsp;&nbsp;&nbsp;&nbsp;户名&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_UserNameDel" />
            &nbsp;&nbsp;&nbsp;&nbsp;表号&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_MeterNoDel" />
            &nbsp;&nbsp;&nbsp;&nbsp;地址&nbsp;&nbsp;<input class="easyui-validatebox" type="text" id="select_AdressDel" />
            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-search'" id="btnSelectDel" onclick="IotM.SetUpLoadDate.SerachClickUser()">查询</a>
            &nbsp;&nbsp;&nbsp;&nbsp;
      
      
        </div>


        <div id="dataGrid_deleteUser">
        </div>


        <div style="text-align: right; padding-right: 50px; padding-top: 10px">

            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                id="btnOk_deleteUser">确定选择</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                    id="btnCancel_deleteUser">取消</a>

        </div>

    </div>



    <div id="GetUserDiv" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-search'"
        style="padding: 10px; width: auto">

        <div id="dataGrid_getUser">
        </div>

        <div id="divdisplay" style="text-align: right; padding-right: 50px; padding-top: 10px">

            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                id="btnOk_Set">设定计算日</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                    id="btnCancel_Set">取消</a>

        </div>
    </div>


    <div id="GetCommunityDiv" class="easyui-window" data-options="modal:true,closed:true,iconCls:'icon-search'"
        style="padding: 10px; width: auto">

        <div id="dataGrid_Community">
        </div>


        <div style="text-align: right; padding-right: 50px; padding-top: 10px">

            <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-ok'"
                id="btnOk_Community">确定选择</a>&nbsp;&nbsp;&nbsp;&nbsp; 
                <a href="#" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-cancel'"
                    id="btnCancel_Community">取消</a>

        </div>

    </div>

    </form>
</body>
</html>
