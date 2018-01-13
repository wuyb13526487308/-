IotM.namespace('IotM.JieSuanRi');

/************************************
*方法名称：IotM.JieSuanRi.LoadDataGrid
*方法功能：加载列表控件Grid资料
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.LoadDataGrid = function () {
    var url = "../Handler/JieSuanRiHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "RegisterDate",
        order: "DASC",
        columns: [
                    [
                     //{ field: 'TaskID', title: '任务号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'RegisterDate', title: '申请时间', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Scope', title: '区域', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Total', title: '总户数', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'SettlementMonth', title: '结算月', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'SettlementDay', title: '结算日', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "0") { return "申请"; }
                             else if (value == "1") { return "撤销"; }
                             else if (value == "2") { return "完成"; }
                             else if (value == "3") { return "失败"; }
                             else { return "未知"; }
                         }
                     },
                     { field: 'Context', title: '备注', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     { field: 'Oper', title: '操作员', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     {
                         field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center',
                         formatter: function (value, rec, index) {
                             //var e = '<a href="#" mce_href="#" menucode="bjyh" onclick="IotM.JieSuanRi.OpenformEdit(this)"><span style="color:blue">编辑</span></a> ';
                             var f = '<a href="#" mce_href="#"  onclick="IotM.JieSuanRi.GetUserDetail(this)"><span style="color:blue">查看用户明细</span></a> ';
                             if (rec.State != null && rec.State.replace(" ", "") == "0") {
                                 f += '<a href="#" mce_href="#"  onclick="IotM.JieSuanRi.revoke(this)"><span style="color:blue">撤销</span></a> ';
                             } if (rec.FailCount != null && rec.FailCount > 0) {
                                 f += '<a href="#" mce_href="#"  onclick="IotM.JieSuanRi.GetUserFile(this)"><span style="color:blue">失败' + rec.FailCount + '户</span></a> ';
                             }
                             return f;
                         }
                     }
                    ]

        ],
        queryParams: { TWhere: '' },
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
*方法名称：IotM.JieSuanRi.OpenformAdd
*方法功能：打开新增结算日的窗口
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.JieSuanRi.AddClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    IotM.FormSetDefault('formAdd');
    $("#userListTR").show();

    IotM.JieSuanRi.LoadDataGridList();

    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '设置结算日',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100,
        onOpen: function () {
            IotM.checkisValid('formAdd');

        }
    });
};

/************************************
*方法名称：IotM.JieSuanRi.FileClick
*方法功能：查看上传周期
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.FileClick = function (obj) {
    IotM.JieSuanRi.OpenformAdd();
    var rows = $('#dataGrid_getUser').datagrid('getSelections');
    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return false;
    }
    $('#dataGrid_list').datagrid("loadData", rows);
};

/************************************
*方法名称：IotM.JieSuanRi.OpenformParam
*方法功能：打开设置结算日窗口
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.OpenformParam = function (obj) {
    $('#btnOk').unbind('click').bind('click', function () { IotM.JieSuanRi.EditClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.SetData('formAdd', data);
    $("#userListTR").hide();
    if (data.SwitchTag.length > 0) {
        for (var i = 0; i < data.SwitchTag.length; i++) {
            if (data.SwitchTag[i] == "0") {
                $("#switch" + i).attr("checked", false)
            } else {
                $("#switch" + i).attr("checked", "checked")
            }
        }
    }
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '设置结算日',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () {
            IotM.EditDisabled('formAdd');
            IotM.checkisValid('formAdd');
        }
    });
};

/************************************
*方法名称：IotM.JieSuanRi.SerachClick
*方法功能：查询按钮事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.SerachClick = function (value, name) {
    var Where = "";
    if ($("#select_Street").val() != "") {
        Where += '  AND  Scope  like \'%' + $("#select_Street").val() + '%\'';
    }
    if (document.getElementById("select_State")) {
        if ($("#select_State").combobox("getValue") != "")
            Where += '  AND  State  =  \'' + $("#select_State").combobox("getValue") + '\'';
    }

    $('#dataGrid').datagrid('options').queryParams = {
        TWhere: Where,
        Date1: $('#select_RegisterDate').datebox('getValue')
    };
    $('#dataGrid').datagrid('load');
};

/************************************
*方法名称：IotM.JieSuanRi.SerachClickUser
*方法功能：查询用户按钮事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.SerachClickUser = function () {    
    var Where = " and MeterType='01' ";
    if ($("#select_userIDDelete").val() != "") {
        Where += '  AND  UserID  like \'%' + $("#select_userIDDelete").val() + '%\'';
    }
    if ($("#select_UserNameDel").val() != "") {
        Where += '  AND  UserName  like \'%' + $("#select_UserNameDel").val() + '%\'';
    }
    if ($("#select_AdressDel").val() != "") {
        Where += '  AND  Address  like \'%' + $("#select_AdressDel").val() + '%\'';
    }
    if ($("#select_MeterNoDel").val()) {
        if ($("#select_MeterNo").val() != "")
            Where += '  AND  MeterNo  like \'%' + $("#select_MeterNoDel").val() + '%\'';
    }
    
    $('#dataGrid_deleteUser').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid_deleteUser').datagrid('load');
};

/************************************
*方法名称：IotM.JieSuanRi.LoadDataGridList
*方法功能：加载选择用户列表
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.LoadDataGridList = function () {

    $('#dataGrid_list').datagrid({

        height: IotM.MainGridHeight * 0.3,
        width: IotM.MainGridWidth * 0.35,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        sort: "MeterNo",
        order: "ASC",

        columns: [
                    [

                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true }
                    ]

        ],
        onLoadSuccess: function () { },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_list').datagrid('getPager');
            $(p).pagination({
                // pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
                pageList: IotM.pageList, //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页',
                displayMsg: ' {from} - {to} 共 {total}',
                onBeforeRefresh: function () {
                    $(this).pagination('loading');
                    $(this).pagination('loaded');
                }
            });

        }
    });

}

/************************************
*方法名称：IotM.JieSuanRi.SelAllUser
*方法功能：选择所有用户
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.SelAllUser = function () {
    $("#hidType").val("1");
    var url = "../Handler/UserHandler.ashx?AType=QueryView";
    $('#dataGrid_list').datagrid({
        url: url,
        onLoadSuccess: function (data) { $("#CNTotal").val(data.total); },
        queryParams: { TWhere: "and MeterType='01'" },//条件只有金额表可以进行充值,所以只显示金额表
    });

}

/************************************
*方法名称：IotM.JieSuanRi.LoadDataGridDelete
*方法功能：加载用户
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.LoadDataGridDelete = function () {
    var url = "../Handler/UserHandler.ashx?AType=QueryView";
    $('#dataGrid_deleteUser').datagrid({
        title: '',
        toolbar: '#tb_deleteUser',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        sort: "UserID",
        order: "ASC",
        columns: [
                    [

                     { field: 'ck', checkbox: true },
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.12, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.23, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.4, align: 'center', sortable: true }

                    ]

        ],
        queryParams: { TWhere: "and MeterType='01'" },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_deleteUser').datagrid('getPager');
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
*方法名称：IotM.JieSuanRi.SelUser
*方法功能：选择用户按钮事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.SelUser = function () {

    $('#btnOk_deleteUser').unbind('click').bind('click', function () { IotM.JieSuanRi.BatchDeleteClick(); });
    $('#btnCancel_deleteUser').unbind('click').bind('click', function () { $('#deleteUserDiv').window('close'); });

    IotM.JieSuanRi.LoadDataGridDelete();

    $('#deleteUserDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '选择用户',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 150,
        top: 150,
    });
};

/************************************
*方法名称：IotM.JieSuanRi.BatchDeleteClick
*方法功能：确认选择用户
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.BatchDeleteClick = function () {
    $("#hidType").val("2");
    var rows = $('#dataGrid_deleteUser').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }

    $('#dataGrid_list').datagrid("loadData", rows);
    $('#deleteUserDiv').window('close');

    $("#CNTotal").val($('#dataGrid_list').datagrid('getRows').length);
}

/************************************
*方法名称：IotM.JieSuanRi.DeleteSerachClick
*方法功能：用户资料查询按钮事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.DeleteSerachClick = function () {
    var Where = "";
    if ($("#select_Street").val() != "") {
        Where += '  AND  Street  like \'%' + $("#select_StreetDelete").val() + '%\'';
    }
    if ($("#select_Community").val() != "") {
        Where += '  AND  Community  like \'%' + $("#select_Community").val() + '%\'';
    }
    if ($("#select_AdressDel").val() != "") {
        Where += '  AND  Address  like \'%' + $("#select_AdressDel").val() + '%\'';
    }
    $('#dataGrid_deleteUser').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid_deleteUser').datagrid('load');

}

/************************************
*方法名称：IotM.JieSuanRi.CleanSelect
*方法功能：清除选择
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.CleanSelect = function () {
    $('#dataGrid_list').datagrid('loadData', { total: 0, rows: [] });
    $("#CNTotal").val($('#dataGrid_list').datagrid('getRows').length);

}

/************************************
*方法名称：IotM.JieSuanRi.AddClick
*方法功能：新增事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.AddClick = function () {
    if (!($("#formAdd").form('validate'))) {
        return false;
    }
    if ($('#dataGrid_list').datagrid('getRows').length <= 0) {
        $.messager.alert('提示', '请选择用户！', 'info');
        return false;
    }
    if (!($("#formAdd").form('validate'))) {
        return false;
    }
    //if (IotM.checkisValid('formAdd')) {
    var data = IotM.GetData('formAdd');
    data.SwitchTag = "";
    data.hidType = $("#hidType").val();
    for (var i = 0; i < 9; i++) {
        if ($("#switch" + i).attr("checked")) {
            data.SwitchTag += "1";
        } else {
            data.SwitchTag += "0";
        }
    }
    var rows = $('#dataGrid_list').datagrid('getRows');
    var strNo = "";
    for (var i = 0; i < rows.length; i++) {
        if (i == rows.length - 1) {
            strNo += rows[i].MeterNo;
        }
        else {
            strNo += rows[i].MeterNo + ",";
        }
    }
    data.strNo = strNo;
    data.communityStr = IotM.JieSuanRi.communityStr;
    data.Total = $("#CNTotal").val();
    data.Context = $("#CNContext").val();
    data.strDay = $('#txtDate').numberspinner('getValue');
    data.strMonth = $('#txtMouth').numberspinner('getValue');
    $.post("../Handler/JieSuanRiHandler.ashx?AType=Add",
             data,
              function (data, textStatus) {
                  if (textStatus == 'success') {
                      if (data.Result) {
                          $('#btnCancel').click();
                          $('#btnCancel_Set').click();
                          $('#dataGrid').datagrid('appendRow', eval('(' + data.TxtMessage + ')'));
                          $.messager.alert('提示', '操作成功！', 'info');
                      }
                      else
                          $.messager.alert('警告', data.TxtMessage, 'warn');
                  }
              }, "json");
    //}
};

/************************************
*方法名称：IotM.JieSuanRi.GetUserDetail
*方法功能：获取设定结算日的用户明细
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.GetUserDetail = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    document.getElementById("divdisplay").style.display = "none";
    var id = data.DayID == null ? data.ID : data.DayID;
    IotM.JieSuanRi.LoadDataGridUser(id);

    $('#GetUserDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '查看用户明细',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 150,
        top: 150,
    });
}

/************************************
*方法名称：IotM.JieSuanRi.LoadDataGridUserfILE
*方法功能：加载失败用户详细视图
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.LoadDataGridUserfILE = function (id) {
    var url = "../Handler/JieSuanRiHandler.ashx?AType=QueryUserFile";
    $('#dataGrid_getUser').datagrid({
        title: '',
        toolbar: '',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        sort: "UserID",
        order: "ASC",
        columns: [
                    [
                     { field: 'ck', checkbox: true },
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.4, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true }
                    ]

        ],
        queryParams: { TWhere: '', MeterID: id },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_getUser').datagrid('getPager');
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
}

/************************************
*方法名称：IotM.JieSuanRi.revoke
*方法功能：撤销上传周期
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.revoke = function (obj) {

    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    var id = data.DayID == null ? data.ID : data.DayID;
    $.messager.confirm('确认', '是否真的撤销?', function (r) {
        if (r == true) {
            $.ajax({
                type: 'POST',
                async: false,
                url: "../Handler/JieSuanRiHandler.ashx?AType=REVOKE&ID=" + id,
                success: function (data, textStatus) {
                    if (textStatus == 'success') {
                        data = JSON.parse(data);
                        if (data.Result) {
                            $('#dataGrid').datagrid('load');
                            $('#btnCancel').click();
                            $('#dataGrid').datagrid('appendRow', eval('(' + data.TxtMessage + ')'));
                            $.messager.alert('提示', '操作成功！', 'info');
                        }
                        else {
                            $.messager.alert('警告', data.TxtMessage, 'warn');
                        }
                    } else {
                        $.messager.alert('警告', data.TxtMessage, 'warn');
                    }
                }
            });
        }
    });
}

/************************************
*方法名称：IotM.JieSuanRi.LoadDataGridUser
*方法功能：加载用户详细视图
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.LoadDataGridUser = function (id) {
    var url = "../Handler/JieSuanRiHandler.ashx?AType=QueryUser";
    $('#dataGrid_getUser').datagrid({
        title: '',
        toolbar: '',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        sort: "UserID",
        order: "ASC",
        columns: [
                    [

                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "0") { return "申请"; }
                             else if (value == "1") { return "撤销"; }
                             else if (value == "2") { return "完成"; }
                             else if (value == "3") { return "失败"; }
                             else { return "未知"; }
                         }
                     },

                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.3, align: 'center', sortable: true },
                     { field: 'FinishedDate', title: '完成时间', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true }

                    ]

        ],
        queryParams: { TWhere: '', MeterID: id },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_getUser').datagrid('getPager');
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
}

/************************************
*方法名称：IotM.JieSuanRi.SelCommunity
*方法功能：选择小区
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.SelCommunity = function () {
    $('#btnOk_Community').unbind('click').bind('click', function () { IotM.JieSuanRi.SelCommunityClick(); });
    $('#btnCancel_Community').unbind('click').bind('click', function () { $('#GetCommunityDiv').window('close'); });
    IotM.JieSuanRi.LoadDataGridCommunity();
    $('#GetCommunityDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '选择小区',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 150,
        top: 150,
    });
}

/************************************
*方法名称：IotM.JieSuanRi.LoadDataGridCommunity
*方法功能：加载小区资料
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.LoadDataGridCommunity = function () {

    var url = "../Handler/CommunityHandler.ashx?AType=QueryView";
    $('#dataGrid_Community').datagrid({
        title: '',
        toolbar: '',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        sort: "ID",
        order: "ASC",
        columns: [
                    [
                     { field: 'ck', checkbox: true },
                     { field: 'StreetName', title: '所属街道', rowspan: 2, width: IotM.MainGridWidth * 0.35, align: 'center', sortable: true },
                     { field: 'CommunityName', title: '小区名称', rowspan: 2, width: IotM.MainGridWidth * 0.35, align: 'center', sortable: true },
                     { field: 'JEBNum', title: '金额表户数', rowspan: 2, width: IotM.MainGridWidth * 0.3, align: 'center', sortable: true }

                    ]
        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_Community').datagrid('getPager');
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
}

IotM.JieSuanRi.communityStr = "";//记录所选小区资料

/************************************
*方法名称：IotM.JieSuanRi.SelCommunityClick
*方法功能：获取小区讯息
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.SelCommunityClick = function () {
    var rows = $('#dataGrid_Community').datagrid('getSelections');
    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }
    var communityStr = "";
    var communityNameStr = "";
    for (var i = 0; i < rows.length; i++) {
        if (parseInt(rows[i].JEBNum) > 0) {
            communityStr += rows[i].ID + ",";
            communityNameStr += rows[i].CommunityName + ",";
        }
    }
    communityStr = communityStr.substr(0, communityStr.length - 1);
    communityNameStr = communityNameStr.substr(0, communityNameStr.length - 1);
    $("#hidType").val(communityNameStr);
    IotM.JieSuanRi.communityStr = communityStr;
    var url = "../Handler/UserHandler.ashx?AType=QueryView";
    $('#dataGrid_list').datagrid({
        url: url,
        queryParams: { TWhere: " AND Community in ( " + communityStr + ") and MeterType='01' " },
        onLoadSuccess: function (data) { $("#CNTotal").val(data.total); }
    });
    $('#GetCommunityDiv').window('close');
}

/************************************
*方法名称：IotM.JieSuanRi.GetUserFile
*方法功能：获取失败用户明细
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.JieSuanRi.GetUserFile = function (obj) {
    document.getElementById("divdisplay").style.display = "block";
    $('#btnOk_Set').unbind('click').bind('click', function () { IotM.JieSuanRi.FileClick(); });
    $('#btnCancel_Set').unbind('click').bind('click', function () { $('#GetUserDiv').window('close'); });

    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.JieSuanRi.LoadDataGridUserfILE(data.DayID);

    $('#GetUserDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '失败用户详细',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 150,
        top: 150,
    });
}