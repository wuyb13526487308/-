IotM.namespace('IotM.CZTongJi');
//加载列表控件
IotM.CZTongJi.LoadDataGrid = function () {
    var type = $("#CNTopUpType").combobox("getValue");
    var url = "../Handler/CZTongJiHandler.ashx?AType=Query&Action=" + type;
    var obj;
    if (type == "0") {
        obj = [
                    { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                    { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                    {
                        field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                        formatter: function (value, rec, index) {

                            if (value == "0") { return "等待安装"; }
                            else if (value == "1" || value == "2") { return "等待点火"; }
                            else if (value == "3") { return "正常使用"; }
                            else { return "未知"; }

                        }
                    },
                    { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.3, align: 'center', sortable: true },
                    { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                    {
                        field: 'MeterType', title: '表类型', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,

                        formatter: function (value, rec, index) {

                            if (value == "00") { return "气量表"; }
                            else if (value == "01") { return "金额表"; }
                            else { return "未知"; }

                        }
                    },
                    {
                        field: 'ValveState', title: '阀门状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,

                        formatter: function (value, rec, index) {
                            if (value == "1") { return "阀开"; }
                            else if (value == "0") { return "阀关"; }
                            else { return "未知"; }

                        }
                    },
                    { field: 'TotalAmount', title: '总用量', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                    { field: 'TotalTopUp', title: '总充值金额', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                    { field: 'RemainingAmount', title: '剩余金额', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                    { field: 'ReadDate', title: '最后抄表日期', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }

        ];
    } else if (type == "1") {
        obj = [
                       { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
        ];
    } else if (type == "2") {
        obj = [
                    { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
        ];
    } else {
        $.messager.alert('警告', "没有此类型的统计方式!", 'warn');
        return false;
    }
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "UserID",
        order: "ASC",
        columns: [
                    obj
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
        },
        view: detailview,
        detailFormatter: function (index, row) {
            return '<div style="padding:2px"><table class="ddv"></table></div>';
        },
        onExpandRow: function (index, row) {
            var ddv = $(this).datagrid('getRowDetail', index).find('table.ddv');
            //if (row["Type"] == "00") { return; }
            $.ajax({
                type: 'GET',
                async: false,
                url: "../Handler/CZTongJiHandler.ashx?AType=USERCZHISTORY&UserID=" + row["UserID"] + "&MeterNo=" + row["MeterNo"],
                success: function (data, textStatus) {
                    data = eval('(' + data + ')')
                    var rows = eval('(' + data.TxtMessage + ')');
                    if (rows.rows.length == 0) {
                        return;
                    }
                    IotM.CZTongJi.GridDVLoad(ddv, row, index);
                }
            });
        }
    });
};

IotM.ChaoBiao.GridDVLoad = function (ddv, row, index1) {
    if (type == "0") {
        obj = [
                     { field: 'AID', title: '流水号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Amount', title: '充值金额', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'TopUpDate', title: '充值日期', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "0") { return "等待执行"; }
                             else if (value == "1") { return "充值完成"; }
                             else if (value == "2") { return "撤销"; }
                             else { return "未知"; }
                         }

                     },
                     {
                         field: 'TopUpType', title: '充值类型', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "0") { return "本地营业厅"; }
                             else if (value == "1") { return "接口"; }
                             else if (value == "2") { return "本地网站"; }
                             else if (value == "3") { return "换表补充"; }
                             else { return "未知"; }
                         }

                     },
                    { field: 'Oper', title: '操作员', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
        ];
    } else if (type == "1") {
        obj = [
                       { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
        ];
    } else if (type == "2") {
        obj = [
                    { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
        ];
    } else {
        $.messager.alert('警告', "没有此类型的统计方式!", 'warn');
        return false;
    }

    ddv.datagrid({
        url: '../Handler/ChaoBiaoHandler.ashx?AType=QUERYHistory&UserID=' + row["UserID"],
        fitColumns: true,
        singleSelect: true,
        rownumbers: true,
        showHeader: true,
        loadMsg: '加载中...',
        height: 'auto',
        columns: [obj],
        onResize: function () {
            $('#dataGrid').datagrid('fixDetailRowHeight', index1);
        },
        onLoadSuccess: function (row, data) {
            setTimeout(function () {
                $('#dataGrid').datagrid('fixDetailRowHeight', index1);
            }, 0);
        }
    });

}

//EasyUI行统计
IotM.CZTongJi.compute = function (colName) {
    var rows = $('#table').datagrid('getRows');
    var total = 0;
    for (var i = 0; i < rows.length; i++) {
        total += parseFloat(rows[i][colName]);
    }
    return total;
}
//数据加载成功事件
IotM.CZTongJi.onLoadSuccess = function () {
    //添加“合计”列
    $('#table').datagrid('appendRow', {
        Saler: '<span class="subtotal">合计</span>',
        TotalOrderCount: '<span class="subtotal">' + compute("TotalOrderCount") + '</span>',
        TotalOrderMoney: '<span class="subtotal">' + compute("TotalOrderMoney") + '</span>',
        TotalOrderScore: '<span class="subtotal">' + compute("TotalOrderScore") + '</span>',
        TotalTrailCount: '<span class="subtotal">' + compute("TotalTrailCount") + '</span>',
        Rate: '<span class="subtotal">' + ((IotM.CZTongJi.compute("TotalOrderScore") / IotM.CZTongJi.compute("TotalTrailCount")) * 100).toFixed(2) + '</span>'
    });
}

IotM.CZTongJi.ChangeTJType = function () {
    $("#dataGrid").html("");
    //var Where = "";
    ////if ($("#select_UserID").val() != "") {
    ////    Where += '  AND  UserID  like \'%' + $("#select_UserID").val() + '%\'';
    ////}
    //if ($("#CNTopUpType").combobox("getValue") != "") {
    //    Where += '  AND  UserName  like \'%' + $("#select_UserName").val() + '%\'';
    //}
    //if ($("#select_Adress").val() != "") {
    //    Where += '  AND  Address  like \'%' + $("#select_Adress").val() + '%\'';
    //}
    //if ($("#select_MeterNo").val()) {
    //    if ($("#select_MeterNo").val() != "")
    //        Where += '  AND  MeterNo  like \'%' + $("#select_MeterNo").val() + '%\'';
    //}


    //$('#dataGrid').datagrid('options').queryParams = {
    //    //TWhere: Where,//where条件
    //    Date1: $('#txtDate1').datebox('getValue'),//开始时间
    //    Date2: $('#txtDate2').datebox('getValue'),//截止时间
    //};

    //$('#dataGrid').datagrid('load');
};

IotM.CZTongJi.SerachClick = function () {


    //var Where = "";
    //if ($("#select_UserID").val() != "") {
    //    Where += '  AND  UserID  like \'%' + $("#select_UserID").val() + '%\'';
    //}
    //if ($("#select_UserName").val() != "") {
    //    Where += '  AND  UserName  like \'%' + $("#select_UserName").val() + '%\'';
    //}
    //if ($("#select_Adress").val() != "") {
    //    Where += '  AND  Address  like \'%' + $("#select_Adress").val() + '%\'';
    //}
    //if ($("#select_MeterNo").val()) {
    //    if ($("#select_MeterNo").val() != "")
    //        Where += '  AND  MeterNo  like \'%' + $("#select_MeterNo").val() + '%\'';
    //}


    $('#dataGrid').datagrid('options').queryParams = {
        //TWhere: Where,
        Date1: $('#txtDate1').datebox('getValue'),
        Date2: $('#txtDate2').datebox('getValue'),

    };

    $('#dataGrid').datagrid('load');
};






