IotM.namespace('IotM.CompanyReportManage');
IotM.CompanyReportManage.LoadCompanyReportDataGrid = function () {
    var url = "../Handler/Report/ReportManageHandler.ashx?AType=QueryCompanyReport";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "RID",
        order: "ASC",
        columns: [
                   [

                    { field: 'CompanyName', title: '公司名称', rowspan: 1, width: IotM.MainGridWidth * 0.1, sortable: true },
                    { field: 'Name', title: '报表菜单名称', rowspan: 1, width: IotM.MainGridWidth * 0.1, sortable: true },
                    { field: 'MenuCode', title: '报表菜单编号', rowspan: 1, width: IotM.MainGridWidth * 0.1, sortable: true },
                    { field: 'RID', title: '报表模板编号', rowspan: 1, width: IotM.MainGridWidth * 0.1, sortable: true },
                   ]
                  ],
        queryParams: {},
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

IotM.CompanyReportManage.SerachClick = function (value, name) {
    var Where = '';
    if (value && name) {
        Where = name + ' like \'' + value + '\'';
    }
    //页面转到第一页
    $('.pagination-num').first().val(parseInt(1));
    var pageCurrent = parseInt($('.pagination-num').first().val());
    if (!isNaN(pageCurrent)) {
        $('#dataGrid').datagrid('getPager').pagination({ pageNumber: pageCurrent > '{pages}' ? '{pages}' : pageCurrent });
    }
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('reload');
};