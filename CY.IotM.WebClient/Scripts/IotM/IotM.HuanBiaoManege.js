IotM.namespace('IotM.HuanBiao');

IotM.HuanBiao.Action = "";//换标申请的状态 “ADD”为新增“UPD”为修改
/************************************
*方法名称：IotM.HuanBiao.LoadDataGrid 
*方法功能：加载列表控件dataGrid
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.LoadDataGrid = function () {
    var url = "../Handler/HuanBiaoHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: false,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "TopUpDate",
        order: "DASC",
        columns: [
                      [
                     { field: 'ck', checkbox: true },
                     { field: 'UserID', title: '户号', rowspan: 2, width: 100, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: 200, align: 'center', sortable: true },
                     {
                         field: 'changeState', title: '状态', rowspan: 2, width: 70, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "1") { return "换表申请"; }
                             else if (value == "2") { return "换表登记"; }
                             else if (value == "3") { return "换表完成"; }
                             else if (value == "4") { return "撤销"; }
                             else { return "未知"; }
                         }
                     },
                     { field: 'Address', title: '地址', rowspan: 2, width: 200, align: 'center', sortable: true },
                     { field: 'RegisterDate', title: '申请日期', rowspan: 2, width: 130, align: 'center', sortable: true },
                     { field: 'OldMeterNo', title: '原表号', rowspan: 2, width: 120, align: 'center', sortable: true },
                     { field: 'OldGasSum', title: '原底数', rowspan: 2, width: 70, align: 'center', sortable: true },
                     {
                         field: 'Direction', title: '进气方向', rowspan: 2, width:70, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "左") { return "左进气"; }
                             else if (value == "右") { return "右进气"; }
                             else { return "未知"; }
                         }

                     },
                     {
                         field: 'MeterType', title: '原表类型', rowspan: 2, width: 70, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "00") { return "气量表"; }
                             else if (value == "01") { return "金额表"; }
                             else { return "未知"; }
                         }

                     },
                    { field: 'Reason', title: '换表原因', rowspan: 2, width: 150, align: 'center', sortable: true },
                    { field: 'ChangeGasSum', title: '换表底数', rowspan: 2, width: 70, align: 'center', sortable: true },
                    { field: 'NewMeterNo', title: '新表号', rowspan: 2, width: 120, align: 'center', sortable: true },
                    {
                        field: 'MeterType', title: '新表类型', rowspan: 2, width: 70, align: 'center', sortable: true,
                        formatter: function (value, rec, index) {
                            if (value == "00") { return "气量表"; }
                            else if (value == "01") { return "金额表"; }
                            else { return "未知"; }
                        }
                    },
                    { field: 'NEWTotalAmount', title: '新表底数', rowspan: 2, width: 70, align: 'center', sortable: true },
                    { field: 'FinishedDate', title: '换表日期', rowspan: 2, width: 130, align: 'center', sortable: true },
                    {
                        field: 'opt', title: '操作', rowspan: 2, width: 160, align: 'center',
                        formatter: function (value, rec, index) {
                            //var e = '<a href="#" mce_href="#" menucode="bjyh" onclick="IotM.HuanBiao.OpenformEdit(this)"><span style="color:blue">编辑</span></a> ';
                            var f = '<a href="#" mce_href="#"  onclick="IotM.HuanBiao.OpenShenQing(this)"><span style="color:blue">修改</span></a> ';
                            f += '<a href="#" mce_href="#"  onclick="IotM.HuanBiao.PrintDetail(this)"><span style="color:blue">打印</span></a> ';
                            if (rec.changeState == "1" || rec.changeState == "2") {
                                f += '<a href="#" mce_href="#"  onclick="IotM.HuanBiao.revoke(this)"><span style="color:blue">撤销</span></a> ';
                            }
                            if (rec.changeState != "1") {

                                f += '<a href="#" mce_href="#"  onclick="IotM.HuanBiao.MeterHistory(this)"><span style="color:blue">历史查询</span></a> ';
                            }
                            return f;
                        }
                    },
                     

                    ]
        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () {
            IotM.CheckMenuCode();
        },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid').datagrid('getPager');
            $(p).pagination({
                pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
                pageList: IotM.pageList, //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                onBeforeRefresh: function () {
                    $(this).pagination('loading');
                    $(this).pagination('loaded');
                }
            });
        }
    });
};

/************************************
*方法名称：IotM.HuanBiao.SerachClick
*方法功能：查询按钮事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.SerachClick = function () {
    var Where = "";
    //户号
    if ($("#CNUserID").val() != "" && $("#CNUserID").val() != null) {
        Where += '  AND  UserID  like \'%' + $("#CNUserID").val() + '%\'';
    }
    //地址
    if ($("#CNAdress").val() != "" && $("#CNAdress").val() != null) {
        Where += '  AND  Address  like \'%' + $("#CNAdress").val() + '%\'';
    }
    //户名
    if ($("#UserName").val() != "" && $("#UserName").val() != null) {
        Where += '  AND  UserName  like \'%' + $("#UserName").val() + '%\'';
    }
    //状态
    if ($("#CNState").combobox("getValue") != "" && $("#CNState").combobox("getValue") != null)
        Where += '  AND  changeState  =  \'' + $("#CNState").combobox("getValue") + '\'';
    //实现查询条件筛选数据
    $('#dataGrid').datagrid('options').queryParams = {
        TWhere: Where
    };
    //重新加载dataGrid
    $('#dataGrid').datagrid('load');
};

/************************************
*方法名称：IotM.HuanBiao.OpenShenQing
*方法功能：打开申请窗口
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.OpenShenQing = function (obj) {
    $("#hidSure").val("");//判定是否有勾选或者有Enter键查询出用户申请
    var UserID = "";
    if (obj != null && obj != "") {//判断传入的值是否为空，若有值就是修改进入则可以获取UserID
        IotM.HuanBiao.Action = "UPD";
        var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
        $('#dataGrid').datagrid('selectRow', index);
        var data = $('#dataGrid').datagrid('getSelected');
        UserID = data.UserID;
        document.getElementById("CNUserID_Add").style.disabled = "disabled";
        document.getElementById("btnChoise").style.display = "none";
        $("#CNUserID_Add").attr("disabled", "disabled");
    } else {
        $("#CNUserID_Add").removeAttr("disabled");
        document.getElementById("CNUserID_Add").style.disabled = "";
        document.getElementById("btnChoise").style.display = "";
        IotM.HuanBiao.Action = "ADD";
        $('#btnChoise').unbind('click').bind('click', function () { IotM.HuanBiao.OpenUsers(); });
    }
    $('#btnOk').unbind('click').bind('click', function () { IotM.HuanBiao.ShenQing(obj); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    //开启换表申请窗口
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '换表申请',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100,
        onOpen: function () {
            //IotM.checkisValid('formAdd');
        }
    });
    //给窗口中控件赋值
    if (UserID != "") {//判断UserID是否为空，若不为空则是修改打开弹窗，此时可以给弹窗控件赋值
        IotM.HuanBiao.GetUserMessage(UserID);
    } else {
        $("#CNUserID_Add").val("");
        $("#CNUserName_Add").val("");
        $("#CNAddress_Add").val("");
        $("#CNMeterNo_Add").val("");
        $("#CNTotalAmount_Add").val("");
        $("#CNReason_Add").val("");
        $('#CNDirection_Add').combobox('setValue', "");
        $('#CNMeterType_Add').combobox('setValue', "");
    }
    IotM.checkisValid('formAdd');
};

/************************************
*方法名称：IotM.HuanBiao.MeterHistory
*方法功能：换表历史资料查询
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.MeterHistory = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    UserID = data.UserID;

    $('#GetMeterHis').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.98,
        title: '换表历史',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 30,
        top: 100,
        onOpen: function () {
        }
    });
    if (UserID != "") {//判断UserID是否为空，若不为空则是修改打开弹窗，此时可以给弹窗控件赋值
        IotM.HuanBiao.LoadMeterHistoryGridView(UserID);
    }
};

/************************************
*方法名称：IotM.HuanBiao.LoadMeterHistoryGridView
*方法功能：用户历史换表资料显示dataGrid_Meter
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.LoadMeterHistoryGridView = function (obj) {
    var url = "../Handler/HuanBiaoHandler.ashx?AType=HISTORYMETER";
    $('#dataGrid_Meter').datagrid({
        title: '',
        //toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight * 0.7,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "MeterNo",
        order: "ASC",
        columns: [
                    [
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.08, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     {
                         field: 'MeterType', title: '表类型', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {

                             if (value == "00") { return "气量表"; }
                             else if (value == "01") { return "金额表"; }
                             else { return "未知"; }

                         }
                     },
                     { field: 'TotalAmount', title: '总用量', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'TotalTopUp', title: '总充值金额', rowspan: 2, width: IotM.MainGridWidth * 0.07, align: 'center', sortable: true },
                     { field: 'RemainingAmount', title: '剩余金额', rowspan: 2, width: IotM.MainGridWidth * 0.07, align: 'center', sortable: true },
                     //{ field: 'ReadDate', title: '最后抄表日期', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Ladder', title: '阶梯数', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Price1', title: '价格1', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Gas1', title: '用量1', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Price2', title: '价格2', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Gas2', title: '用量2', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Price3', title: '价格3', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Gas3', title: '用量3', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Price4', title: '价格4', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Gas4', title: '用量4', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Price5', title: '价格5', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     //{ field: 'UploadCycle', title: '抄表周期', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'SettlementDay', title: '抄表日', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true }
                    ]

        ],
        queryParams: { TWhere: "and UserID='" + obj + "'" },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid').datagrid('getPager');
            $(p).pagination({
                pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
                pageList: IotM.pageList, //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                onBeforeRefresh: function () {
                    $(this).pagination('loading');
                    $(this).pagination('loaded');
                }
            });
        }
    });
};

/************************************
*方法名称：IotM.HuanBiao.OpenDengJi
*方法功能：开启申请换表的弹窗
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.OpenDengJi = function () {
    var rows = $('#dataGrid').datagrid('getSelections');
    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return false;
    }
    if (rows.changeState == "2") {//如果该申请单的状态时在换表登记状态，则提示该用户"不能重复提交换表登记"
        $.messager.alert('提示', '不能重复填写换表登记！', 'info'); return false;
    }
    $('#btnOk_DJ').unbind('click').bind('click', function () { IotM.HuanBiao.DengJi(rows); });
    $('#btnCancel_DJ').unbind('click').bind('click', function () { $('#divDengJi').window('close'); });
    //开启换表登记窗口
    $('#divDengJi').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.45,
        title: '换表登记',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100,
        onOpen: function () {
            //给页面赋值
            oldLastTotal = rows[0].oldLastTotal == null ? "" : rows[0].oldLastTotal;
            TotalAmount = rows[0].TotalAmount == null ? "" : rows[0].TotalAmount;
            LastTotal = rows[0].LastTotal == null ? "" : rows[0].LastTotal;
            $("#CNUserID_DJ").val(rows[0].UserID);       //户号
            $("#CNUserName_DJ").val(rows[0].UserName);   //户名
            $("#CNAddress_DJ").val(rows[0].Address);     //地址
            $("#CNMeterNo_DJ").val(rows[0].MeterNo);     //标号
            //$("#CNTotalAmount_DJ").val(rows[0].TotalAmount == null ? 0 : rows[0].TotalAmount);
            $("#CNReason_DJ").val(rows[0].Reason);        //换表原因
            $('#CNDirection_DJ').combobox('setValue', rows[0].Direction);        //表进气方向
            $('#CNMeterType_DJ').combobox('setValue', rows[0].MeterType);        //表类型
            $('#CNNewMeterType_DJ').combobox('setValue', rows[0].MeterType);     //新表类型
            $("#CNTotalAmount_DJ").val(TotalAmount == "" ? 0 : TotalAmount);   //
            $("#CNLastTotal_DJ").val(LastTotal == "" ? 0 : LastTotal);
            //$("#CNTotalAmountS_DJ").val(rows[0].Total);
            IotM.checkisValid('formDengJi');
            $('#CNRemainingAmount_DJ').val("");
            $('#CNOldGasSum_DJ').val("");
            $('#CNNewMeterNo_DJ').val("");
            $('#CNChangeGasSum_DJ').val("");
            $('#CNChangeGasSum_DJ').val("");
            $("#CNFinishedDate_DJ").datebox("setValue", "");
            if (rows[0].MeterType == '01') {
                $('#CNRemainingAmount_DJ').validatebox({
                    required: true,
                    validType: 'AmountRex'
                });
                document.getElementById("CNRemainingAmount_DJ").style.disabled = "";
                $('#CNRemainingAmount_DJ').removeAttr("disabled");
            } else {
                $('#CNRemainingAmount_DJ').validatebox({
                    required: false,
                    validType: 'AmountRex'
                });
                document.getElementById("CNRemainingAmount_DJ").style.disabled = "disabled";
                $('#CNRemainingAmount_DJ').attr("disabled", "disabled");
            }
            IotM.checkisValid('formDengJi');
            setTimeout("$('#CNRemainingAmount_DJ').focus()", 20);

            //换表底数
            //OldGasSum = $("#CNOldGasSum_DJ").val() == "" ? 0 : parseFloat($("#CNOldGasSum_DJ").val());
            //上期结算底数
            CNLastTotal_DJ = $("#CNLastTotal_DJ").val() == "" ? 0 : parseFloat($("#CNLastTotal_DJ").val());
            //表底数
            TotalAmount = $("#CNTotalAmount_DJ").val() == "" ? 0 : parseFloat($("#CNTotalAmount_DJ").val());
            //表底数
            RemainingAmount = $("#CNRemainingAmount_DJ").val() == "" ? 0 : parseFloat($("#CNRemainingAmount_DJ").val());
            //$('#CNNewMeterType_DJ').combobox('getValue');
            //$("#CNOldGasSum_DJ").val(OldGasSum);
            $("#CNLastTotal_DJ").val(CNLastTotal_DJ);
            $("#CNTotalAmount_DJ").val(TotalAmount);
            $("#CNRemainingAmount_DJ").val(RemainingAmount);
        }
    });
};

$.extend($.fn.validatebox.defaults.rules, {
    AmountRex: {
        validator: function (value) {
            var rex = /^(-|\+)?([1-9][\d]{0,7}|0)(\.[\d]{1,2})?$/;
            return rex.test(value);
        },
        message: "请输入正确金额格式"
    }
});

/************************************
*方法名称：IotM.HuanBiao.OpenUsers
*方法功能：开启申请换表的弹窗
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.OpenUsers = function () {
    $('#btnqry_User').unbind('click').bind('click', function () { IotM.HuanBiao.SerachUserClick(); });
    $('#btnOk_User').unbind('click').bind('click', function () { IotM.HuanBiao.SureChoiseUser(); });
    $('#btnCancel_User').unbind('click').bind('click', function () { $('#GetUserDiv').window('close'); });

    IotM.HuanBiao.LoadUserDataGridList();
    $('#GetUserDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.65,
        title: '查询用户',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100,
        onOpen: function () {
            //IotM.checkisValid('formAdd');
        }
    });
};

/************************************
*方法名称：IotM.HuanBiao.GetUserMessage
*方法功能：enter 键事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.GetUserMessage = function (obj) {
    if (obj != null) {
        obj = obj.replace(/(^\s*)|(\s*$)/g, "");
    }
    $.ajax({
        type: 'POST',
        async: false,
        url: "../Handler/HuanBiaoHandler.ashx?AType=QUERY",
        data: { TWhere: "and UserID='" + obj + "'" },
        success: function (data, textStatus) {
            data = JSON.parse(data);
            if (textStatus == 'success') {
                if (data.Result) {
                    data = JSON.parse(data.TxtMessage);
                    if (data.rows.length > 0) {
                        $("#CNUserID_Add").val(data.rows[0].UserID);
                        $("#CNUserName_Add").val(data.rows[0].UserName);
                        $("#CNAddress_Add").val(data.rows[0].Address);
                        $("#CNMeterNo_Add").val(data.rows[0].MeterNo);
                        $("#CNTotalAmount_Add").val(data.rows[0].TotalAmount);
                        $("#CNReason_Add").val(data.rows[0].Reason);
                        $('#CNDirection_Add').combobox('setValue', data.rows[0].Direction);
                        $('#CNMeterType_Add').combobox('setValue', data.rows[0].MeterType);
                        $("#hidSure").val("true");
                    } else {
                        $.ajax({
                            type: 'POST',
                            async: false,
                            url: "../Handler/UserHandler.ashx?AType=QueryView",
                            data: { TWhere: "and UserID='" + obj + "'" },
                            success: function (data, textStatus) {
                                data = JSON.parse(data);
                                if (textStatus == 'success') {
                                    if (data.Result) {
                                        data = JSON.parse(data.TxtMessage);
                                        if (data.rows.length > 0) {
                                            $("#CNUserID_Add").val(data.rows[0].UserID);
                                            $("#CNUserName_Add").val(data.rows[0].UserName);
                                            $("#CNAddress_Add").val(data.rows[0].Address);
                                            $("#CNMeterNo_Add").val(data.rows[0].MeterNo);
                                            $("#CNTotalAmount_Add").val(data.rows[0].TotalAmount);
                                            $("#CNReason_Add").val("");
                                            $('#CNDirection_Add').combobox('setValue', data.rows[0].Direction);
                                            $('#CNMeterType_Add').combobox('setValue', data.rows[0].MeterType);
                                            $("#hidSure").val("true");
                                        } else {
                                            $.messager.alert('提示', '用户不存在！', 'info');
                                            //$("#CNUserID_Add").val("");
                                            $("#CNUserName_Add").val("");
                                            $("#CNAddress_Add").val("");
                                            $("#CNMeterNo_Add").val("");
                                            $("#CNTotalAmount_Add").val("");
                                            $("#CNReason_Add").val("");
                                            $('#CNDirection_Add').combobox('setValue', "");
                                            $('#CNMeterType_Add').combobox('setValue', "");
                                            $("#hidSure").val("false");
                                        }
                                        //$.messager.alert('提示', '操作成功！', 'info');
                                    }
                                    else {
                                        $.messager.alert('警告', data.TxtMessage, 'warn');
                                    }
                                } else {
                                    $.messager.alert('警告', data.TxtMessage, 'warn');
                                }
                                //$('#btnCancel').click();
                            }
                        });
                    }
                    //$.messager.alert('提示', '操作成功！', 'info');
                }
                else {
                    $.messager.alert('警告', data.TxtMessage, 'warn');
                }
            } else {
                $.messager.alert('警告', data.TxtMessage, 'warn');
            }
        }
    });
};

/************************************
*方法名称：IotM.HuanBiao.LoadUserDataGridList
*方法功能：加载用户Gridview
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.LoadUserDataGridList = function () {
    var url = "../Handler/UserHandler.ashx?AType=QUERYVIEW";
    var WHERE = '';
    $('#dataGrid_User').datagrid({
        title: '',
        toolbar: '#tb_user',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "TopUpDate",
        order: "DASC",
        columns: [
                    [
                     { field: 'ck', checkbox: true },
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
                    ]
        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid').datagrid('getPager');
            $(p).pagination({
                //pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
                pageList: IotM.pageList, //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页',
                displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
                onBeforeRefresh: function () {
                    $(this).pagination('loading');
                    $(this).pagination('loaded');
                }
            });
        }
    });
};

/************************************
*方法名称：IotM.HuanBiao.SerachUserClick
*方法功能：人员查询按钮事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.SerachUserClick = function () {
    var Where = "";
    if ($("#CNUserID_user").val() != "" && $("#CNUserID_user").val() != null) {
        Where += '  AND  UserID  like \'%' + $("#CNUserID_user").val() + '%\'';
    }
    if ($("#CNAddress_user").val() != "" && $("#CNAddress_user").val() != null) {
        Where += '  AND  Address  like \'%' + $("#CNAddress_user").val() + '%\'';
    }
    if ($("#CNUserName_user").val() != "" && $("#CNUserName_user").val() != null) {
        Where += '  AND  UserName  like \'%' + $("#CNUserName_user").val() + '%\'';
    }
    if ($("#CNMeterNo_user").val() != "" && $("#CNMeterNo_user").val() != null) {
        Where += '  AND  MeterNo  like \'%' + $("#CNMeterNo_user").val() + '%\'';
    }
    $('#dataGrid_User').datagrid('options').queryParams = {
        TWhere: Where
    };
    $('#dataGrid_User').datagrid('load');
};

/************************************
*方法名称：IotM.HuanBiao.ShenQing
*方法功能：申请按钮事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.ShenQing = function (obj) {
    if (!($("#formAdd").form('validate'))) {
        return false;
    }
    if ($("#CNUserID_Add").val() == "") {
        IotM.checkisValid('formAdd');
        return false;
    }
    if ($("#hidSure").val() == "") {
        IotM.checkisValid('formAdd');
        return false;
    }
    var Reason = $('#CNReason_Add').val();
    var uri = "";
    var UserID = $("#CNUserID_Add").val();
    var MeterNo = $("#CNMeterNo_Add").val();
    if (IotM.HuanBiao.Action == "UPD" && obj != "" && obj != null) {
        //修改
        var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
        $('#dataGrid').datagrid('selectRow', index);
        var data = $('#dataGrid').datagrid('getSelected');
        var HID = data.HID;
        var uri = "../Handler/HuanBiaoHandler.ashx?AType=EDIT&HID=" + HID + "&UserID=" + UserID + "&MeterNo=" + MeterNo + "&Reason=" + Reason;
    } else if (IotM.HuanBiao.Action == "ADD") {
        //新增
        var uri = "../Handler/HuanBiaoHandler.ashx?AType=ADD&UserID=" + UserID + "&MeterNo=" + MeterNo + "&Reason=" + Reason;
    }
    $.ajax({
        type: 'GET',
        async: false,
        url: uri,
        success: function (data, textStatus) {
            data = JSON.parse(data);
            if (textStatus == 'success') {
                if (data.Result) {
                    if (IotM.HuanBiao.Action == "UPD") {
                        $("#dataGrid").datagrid("reload");
                        $.messager.alert('提示', '修改成功！', 'info');
                    } else {
                        $('#dataGrid').datagrid('appendRow', eval('(' + data.TxtMessage + ')'));
                        $("#dataGrid").datagrid("reload");
                        $.messager.alert('提示', '新增成功！', 'info');
                    }
                }
                else {
                    $.messager.alert('警告', data.TxtMessage, 'warn');
                }
            } else {
                $.messager.alert('警告', data.TxtMessage, 'warn');
            }
            $('#btnCancel').click();
        }
    });
}

/************************************
*方法名称：IotM.HuanBiao.revoke
*方法功能：撤销事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.revoke = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    //var MeterNo = data.MeterNo;
    //var Amount = data.Amount;
    //var MeterID = data.MeterID;
    //var UserID = data.UserID;
    //var TaskID = data.TaskID;
    //var Context = $('#CNContext').val();
    $.messager.confirm('确认', '是否真的撤销?', function (r) {
        if (r == true) {
            $.ajax({
                type: 'GET',
                async: false,
                url: "../Handler/HuanBiaoHandler.ashx?AType=REVOKE&HID=" + data.HID,
                success: function (data, textStatus) {
                    data = JSON.parse(data);
                    if (textStatus == 'success') {
                        if (data.Result) {
                            $('#dataGrid').datagrid('load');
                            $.messager.alert('提示', '操作成功！', 'info');
                        }
                        else {
                            $.messager.alert('警告', data.TxtMessage, 'warn');
                        }
                    } else {
                        $.messager.alert('警告', data.TxtMessage, 'warn');
                    }
                    //$('#btnCancel').click();
                }
            });
        }
    });

}

/************************************
*方法名称：IotM.HuanBiao.DengJi
*方法功能：换标登记登记按钮事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.DengJi = function (obj) {
    if (!($("#formDengJi").form('validate'))) {
        return false;
    }
        var data = IotM.GetData('formDengJi');
        data.FinishedDate = $('#CNFinishedDate_DJ').datebox('getValue');
        data.ID = obj[0].HID;
        //alert(data);
        $.post("../Handler/HuanBiaoHandler.ashx?AType=DENGJI",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#btnCancel_DJ').click();
                              $('#dataGrid').datagrid('load');
                              $.messager.alert('提示', '操作成功！', 'info');
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');
                      }
                  }, "json");
};

/************************************
*方法名称：IotM.HuanBiao.SureChoiseUser
*方法功能：人员选择事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.SureChoiseUser = function () {
    var rows = $('#dataGrid_User').datagrid('getSelections');
    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return false;
    }
    $("#hidSure").val("true");
    $("#CNUserID_Add").val(rows[0].UserID);
    $("#CNUserName_Add").val(rows[0].UserName);
    $("#CNAddress_Add").val(rows[0].Address);
    $("#CNMeterNo_Add").val(rows[0].MeterNo);
    $("#CNTotalAmount_Add").val(rows[0].TotalAmount);
    $("#CNReason_Add").val(rows[0].Reason);
    $('#CNDirection_Add').combobox('setValue', rows[0].Direction);
    $('#CNMeterType_Add').combobox('setValue', rows[0].MeterType);
    $('#btnCancel_User').click();
};

/************************************
*方法名称：IotM.HuanBiao.SureChoiseUser
*方法功能：人员选择事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.HuanBiao.GetChangeUserGas = function () {
    //新表底数
    //ChangeGasSum = $("CNChangeGasSum_DJ").val() == "" ? 0 : parseInt($("CNChangeGasSum_DJ").val());
    if (!$('#CNOldGasSum_DJ').validatebox("isValid")) {
        return false;
    }
    //换表底数
    OldGasSum = $("#CNOldGasSum_DJ").val() == "" ? 0 : parseFloat($("#CNOldGasSum_DJ").val(),2);
    //上期结算底数
    CNLastTotal_DJ = $("#CNLastTotal_DJ").val() == "" ? 0 : parseFloat($("#CNLastTotal_DJ").val(),2);
    //表底数
    TotalAmount = $("#CNTotalAmount_DJ").val() == "" ? 0 : parseFloat($("#CNTotalAmount_DJ").val(),2);
    //$('#CNNewMeterType_DJ').combobox('getValue');
    $("#CNOldGasSum_DJ").val();
    $("#CNLastTotal_DJ").val();
    $("#CNTotalAmount_DJ").val();
    if ($('#CNNewMeterType_DJ').combobox('getValue')=='00') {
        //气量表
        $("#CNTotalAmountS_DJ").val((parseInt(OldGasSum * 100) - parseInt(TotalAmount * 100)) / 100);
    } else {
        //金额表
        $("#CNTotalAmountS_DJ").val((parseInt(OldGasSum * 100) - parseInt(CNLastTotal_DJ * 100)) / 100);
    }
};








