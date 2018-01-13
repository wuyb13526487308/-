IotM.namespace('IotM.ReportManage');
IotM.ReportManage.LoadCompanyReportDataGrid = function () {
    var url = "../Handler/Report/ReportManageHandler.ashx?AType=Query";
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
                    { field: 'RID', title: '报表编号', rowspan: 1, width: IotM.MainGridWidth * 0.1, sortable: true },
                    { field: 'ReportName', title: '报表名称', rowspan: 1, width: IotM.MainGridWidth * 0.1, sortable: true },
                    { field: 'opt', title: '操作', rowspan: 1, width: IotM.MainGridWidth * 0.2, align: 'center',
                        formatter: function (value, rec, index) {
                            var e = '<a href="#" mce_href="#" menucode="xgbbmc" onclick="IotM.ReportManage.OpenformEditReportName(this)">修改名称</a> ';
                            var d = '<a href="#" mce_href="#" menucode="scbbmb" onclick="IotM.ReportManage.RemoveReportClick(this)">删除</a> ';
                            var g = '<a href="#" mce_href="#" menucode="dcbbmb" onclick="IotM.ReportManage.ExportReportClick(this)">导出报表</a> ';
                            return e + d + g;
                        }
                    }
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
IotM.ReportManage.OpenformImport = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.ReportManage.AddReportClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#UploadFile').uploadifyClearQueue(); $('#wAddReport').window('close'); });
    IotM.AddRelaseDisabled('formAdd');
    IotM.FormSetDefault('formAdd');
  
    $('#wAddReport').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.3,
        title: '导入报表',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () { IotM.checkisValid('formAdd'); }
    });
};
IotM.ReportManage.AddReportClick = function () {
    $("#UploadFile").uploadifySettings('scriptData', { isUpdate: $("#chkUpdate").attr("checked") == "checked" });
    $('#UploadFile').uploadifyUpload();
};

IotM.ReportManage.OpenformEditReportName = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.SetData('wEditReportName', data);
    $('#btnEditReportNameOk').unbind('click').bind('click', function () { IotM.ReportManage.EditReportNameClick() });
    $('#btnEditReportNameCancel').unbind('click').bind('click', function () { $('#wEditReportName').window('close'); });
    $('#wEditReportName').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.2,
        title: '修改报表名称',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () { IotM.EditDisabled('wEditReportName'); IotM.checkisValid('wEditReportName'); }
    });
};
IotM.ReportManage.EditReportNameClick = function () {
    if (IotM.checkisValid('wEditReportName')) {
        var data = IotM.GetData('wEditReportName');
        $.post("../Handler/Report/ReportManageHandler.ashx?AType=EditName",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('updateRow',
                                  { index: $('#dataGrid').datagrid('getRowIndex', $('#dataGrid').datagrid('getSelected')),
                                      row: eval('(' + data.TxtMessage + ')')
                                  });
                              $.messager.alert('提示', '操作成功！', 'info', function () {
                                  $('#wEditReportName').window('close');
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};
IotM.ReportManage.RemoveReportClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r == true) {
            $.post("../Handler/Report/ReportManageHandler.ashx?AType=Delete",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('deleteRow', parseInt(index));
                              $.messager.alert('提示', data.TxtMessage, 'info', function () {
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");


        }
    }
   );
};
IotM.ReportManage.ExportReportClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '确定要导出吗?', function (r) {
        if (r == true) {
            var url = '../Handler/Report/ReportManageHandler.ashx?AType=Export&RID=' + data.RID + '&RName=' + encodeURI(data.ReportName);
            IotM.DownLoad(url);
        }
    }
   );
};
IotM.ReportManage.SerachClick = function (value, name) {
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