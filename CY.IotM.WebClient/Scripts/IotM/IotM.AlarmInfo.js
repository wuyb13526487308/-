IotM.namespace('IotM.AlarmInfo');
//加载列表控件
IotM.AlarmInfo.LoadDataGrid = function () {
    var url = "../Handler/AlarmInfoHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        pageSize: 50,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "UserID",
        order: "ASC",
        columns: [
                    [
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'ReportDate', title: '报警日期', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Item', title: '报警事项', rowspan: 2, width: IotM.MainGridWidth * 0.4, align: 'center', sortable: true }/*,
                     { field: 'AlarmValue', title: '报警值', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true }*/
                   
                     
                    ]
                   
        ],
        queryParams: { TWhere: '', AlermItem: $("#select_Item").combobox("getValue") },
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



IotM.AlarmInfo.SerachClick = function () {


    var Where = "";
    //if ($("#select_UserID").val() != "") {
    //    Where += '  AND  UserID  like \'%' + $("#select_UserID").val() + '%\'';
    //}
    if ($("#select_UserName").val() != "") {
        Where += '  AND  UserName  like \'%' + $("#select_UserName").val() + '%\'';
    }
    if ($("#select_Adress").val() != "") {
        Where += '  AND  Address  like \'%' + $("#select_Adress").val() + '%\'';
    }
    if ($("#select_MeterNo").val()) {
        if ($("#select_MeterNo").val() != "")
            Where += '  AND  MeterNo  like \'%' + $("#select_MeterNo").val() + '%\'';
    }

    $('#dataGrid').datagrid('options').queryParams = {
        TWhere: Where,
        Date1: $('#txtDate1').datebox('getValue') + ' ' + $('#txtTime1').timespinner('getValue'),
        Date2: $('#txtDate2').datebox('getValue') + ' ' + $('#txtTime2').timespinner('getValue'),
        AlermItem: $("#select_Item").combobox("getValue"),
    };

    $('#dataGrid').datagrid('load');
};






