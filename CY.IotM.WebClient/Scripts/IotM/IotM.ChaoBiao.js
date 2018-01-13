// 指定命名空间
IotM.namespace('IotM.ChaoBiao');

/************************************
*方法名称：LoadDataGrid
*方法功能：加载页面Grid资料
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChaoBiao.LoadDataGrid = function () {
    var indexs;
    var url = "../Handler/ChaoBiaoHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "MeterNo",
        order: "ASC",
        columns: [
                    [

                        { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                        { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.23, align: 'center', sortable: true },
                        { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.12, align: 'center', sortable: true },
                        {
                            field: 'ValveState', title: '阀状态', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true,
                            formatter: function (value, rec, index) {
                                if (value == "0") { return "阀开"; }
                                else { return "阀关"; }
                            }
                        },
                       { field: 'TotalAmount', title: '总用量', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                       {
                           field: 'RemainingAmount', title: '余额', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', formatter: function (value, rec, index) {
                               return IotM.NumberFormat(rec.RemainingAmount, 2, '--');
                           }
                       },
                       { field: 'ReadDate', title: '抄表时间', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                    ]

        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () {
            IotM.CheckMenuCode();
            $('[field="_expander"]').tooltip({
                position: 'right',
                content: '<span style="color:#fff">点击展开抄表历史记录.</span>',
                onShow: function () {
                    $(this).tooltip('tip').css({ backgroundColor: '#666', borderColor: '#666' });
                }
            });

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
        ,
        view: detailview,
        detailFormatter: function (index, row) {
            return '<div style="padding:2px" ><table id="ddv_' + index + '"  class="ddv"></table></div><div id="dataGrid_user"></div> ';
        },
        onExpandRow: function (index, row) {
            //var ddv = $(this).datagrid('getRowDetail', index).find('table.ddv');
            //if (row["Type"] == "00") { return; }
            //$('[field="_expander"]').tooltip("hide");
            $.ajax({
                type: 'GET',
                async: false,
                url: "../Handler/ChaoBiaoHandler.ashx?AType=QUERYHistory&UserID=" + row["UserID"] + "",
                success: function (data, textStatus) {
                    data = eval('(' + data + ')')
                    var rows = eval('(' + data.TxtMessage + ')');
                    if (rows.rows.length == 0) {
                        return;
                    }
                    IotM.ChaoBiao.GridDVLoad(row, index);
                }
            });
        }
    });
    $.ajax({
        type: 'GET',
        async: false,
        url: "../Handler/ChaoBiaoHandler.ashx?AType=GETCOMPANY",
        success: function (data, textStatus) {
            data = JSON.parse(data);
            //判断在后台查询是否成功
            if (textStatus == 'success') {
                if (data.Result) {
                    //判断获取到的数据是否为空
                    $("#hidCompany").val(data.TxtMessage);
                    //$.messager.alert('提示', '操作成功！', 'info');
                }
                else {
                    //$.messager.alert('警告', data.TxtMessage, 'warn');
                }
            } else {
                //$.messager.alert('警告', data.TxtMessage, 'warn');
            }
        }
    });
};

/************************************
*方法名称：GridDDVLoad
*方法功能：加载gridview资料行的详细资料（）
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChaoBiao.GridDVLoad = function (row, index1) {
    $('#ddv_' + index1).datagrid({
        url: '../Handler/ChaoBiaoHandler.ashx?AType=QUERYHistory&UserID=' + row["UserID"],
        fitColumns: true,
        singleSelect: true,
        rownumbers: true,
        showHeader: true,
        pagination: true,
        loadMsg: '加载中...',
        height: 'auto',
        sort: "MeterNo",
        order: "ASC",
        columns: [[

                    { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                    { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.17, align: 'center', sortable: true },
                    { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.23, align: 'center', sortable: true },
                    { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.12, align: 'center', sortable: true },
                    {
                        field: 'ST1', title: '阀状态', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true,
                        formatter: function (value, rec, index) {
                            if (value.length > 0) {
                                if (value.substr(0, 2) == "00") { return "阀开"; }
                                else if (value.substr(0, 2) == "01") { return "阀关"; }
                                else if (value.substr(0, 2) == "10") { return "保留"; }
                                else if (value.substr(0, 2) == "11") { return "异常"; }
                                else {
                                    return "未知";
                                }
                            } else {
                                return "未知";
                            }
                        }
                    },
                   { field: 'Gas', title: '总用量', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                   {
                       field: 'RemainingAmount', title: '余额', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', formatter: function (value, rec, index) {
                           return IotM.NumberFormat(rec.RemainingAmount, 2, '--');
                       }
                   },
                   { field: 'ReadDate', title: '抄表时间', rowspan: 2, width: IotM.MainGridWidth * 0.13, align: 'center', sortable: true },

        ]],
        onResize: function () {
            $('#dataGrid').datagrid('fixDetailRowHeight', index1);
        },
        onLoadSuccess: function (row, data) {
            setTimeout(function () {
                $('#dataGrid').datagrid('fixDetailRowHeight', index1);
            }, 0);
        }
        //,
        //onBeforeLoad: function (param) {
        //    //var p = ddv.datagrid.methods.getPager;//divPageUser
        //    var p = $('#ddv_' + index1).datagrid('getPager');;
        //    $(p).pagination({
        //        //pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
        //        pageList: IotM.pageList, //可以设置每页记录条数的列表 
        //        beforePageText: '第', //页数文本框前显示的汉字 
        //        afterPageText: '页    共 {pages} 页',
        //        displayMsg: '当前显示 {from} - {to} 条记录   共 {total} 条记录',
        //        onBeforeRefresh: function () {
        //            $(this).pagination('loading');
        //            $(this).pagination('loaded');
        //        }
        //    });
        //}
    });

}

/************************************
*方法名称：SerachClick
*方法功能：点击查询按钮根据条件加载页面Grid资料
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChaoBiao.SerachClick = function () {
    var Where = "";
    if ($("#select_UserID").val() != "") {//户号
        Where += '  AND  UserID  like \'%' + $("#select_UserID").val() + '%\'';
    }
    if ($("#select_UserName").val() != "") {//户名
        Where += '  AND  UserName  like \'%' + $("#select_UserName").val() + '%\'';
    }
    if ($("#select_Adress").val() != "") {//地址
        Where += '  AND  Address  like \'%' + $("#select_Adress").val() + '%\'';
    }
    //设定查询条件及参数
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    //重新加载资料
    $('#dataGrid').datagrid('load');
};

/************************************
*方法名称：EXPOldData
*方法功能：将抄表讯息汇出到Excel中
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChaoBiao.EXPOldData = function () {
    if (IotM.checkisValid('formAdd')) {
        var TimeKinds = "";
        if ($("input[name='CNTime']:checked").val() == "0") {
            TimeKinds = "*";
        } else {
            TimeKinds = $('#CNDate').datebox('getValue');
        }
        var Companys = $("#hidCompany").val();
        var rows = $('#dataGridStreet').datagrid('getSelections');
        var UserKinds = "";
        if ($("input[name='CNUser']:checked").val() == "0") {
            UserKinds == "*";
        } else {
            for (var i = 0; i < rows.length; i++) {
                var index = $('#dataGridStreet').datagrid('getRowIndex', rows[i]);
                if (i == rows.length - 1) {
                    UserKinds += rows[i].ID + ",";
                }
                else {
                    UserKinds += rows[i].ID + ",";
                }
            }
        };
        $.messager.confirm("确认", "是否下载用户当前抄表数据？", function (r) {
            if (r == true) {
                $.ajax({
                    type: 'GET',
                    async: false,
                    url: "../Handler/ChaoBiaoHandler.ashx?AType=GETHISTORYCOUNT&Time=" + TimeKinds + "&User=" + UserKinds,
                    success: function (data) {
                        data = JSON.parse(data);
                        //判断在后台查询是否成功
                        //if (textStatus == 'success') {
                        if (!data.Result) {
                            //判断获取到的数据是否为空
                            //$("#hidCompany").val(data.TxtMessage);
                            //$.messager.alert('提示', '操作成功！', 'info');
                            var ifram = document.createElement("iframe");
                            ifram.src = "../IotM/EXPExcel.aspx?Time=" + TimeKinds + "&User=" + UserKinds + "&CompanyID=" + Companys;
                            ifram.style.display = "none";
                            document.body.appendChild(ifram);
                            setTimeout("$('#btnCancel').click()", 1000);
                        }
                        else {
                            $.messager.alert('警告', "没有抄表数据提供下载!", 'warn');
                        }
                        //} else {
                        //$.messager.alert('警告', data.TxtMessage, 'warn');
                        //}
                        setTimeout("$('#btnCancel').click()", 1000);
                    }
                });
            }
        })
    }
};
/************************************
*方法名称：OpenformEXP
*方法功能：打开导出数据页面
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChaoBiao.OpenformEXP = function () {
    $('#btnCancel').unbind('click').bind('click', function () { $('#Registration').window('close'); });
    IotM.FormSetDefault('fRegistration');
    IotM.ChaoBiao.fnChange();
    $('#Registration').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.6,
        title: '导出数据',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () {
            var curr_time = new Date();
            $('#CNDate').datebox('setValue', myformatter(curr_time));//点火日期
            IotM.ChaoBiao.loadDataGridStreet();//查询资料
            IotM.checkisValid('fRegistration');
        }
    });
};
/************************************
*方法名称：myformatter
*方法功能：时间格式化
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
function myformatter(date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}
/************************************
*方法名称：myparser
*方法功能：时间格式化
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
function myparser(s) {
    if (!s) return new Date();
    var ss = (s.split('-'));
    var y = parseInt(ss[0], 10);//CNPriceType
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}
/************************************
*方法名称：loadDataGridStreet
*方法功能：加载街道讯息
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChaoBiao.loadDataGridStreet = function () {
    var url = "../Handler/ChaoBiaoHandler.ashx?AType=QUERYSTREET";
    $('#dataGridStreet').datagrid({
        title: '',
        url: url,
        height: IotM.MainGridHeight * 0.38,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        sort: "ID",
        order: "ASC",

        columns: [
                    [{ field: 'ck', checkbox: true },
                        { field: 'ID', title: '街道111', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true, hidden: true },
                         { field: 'StreetName', title: '街道', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                        { field: 'CommunityName', title: '小区名称', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true }
                    ]
        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGridStreet').datagrid('getPager');
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
*方法名称：fnChange
*方法功能：单选按钮改变事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChaoBiao.fnChange = function () {
    if ($("input[name='CNUser']:checked").val() == "1") {
        $('#divButum').panel('open');
    } else {
        $('#divButum').panel('close');
    }
};

