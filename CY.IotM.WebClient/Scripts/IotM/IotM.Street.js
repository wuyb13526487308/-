IotM.namespace('IotM.Street');
//加载列表控件
IotM.Street.LoadDataGrid = function () {
    var url = "../Handler/StreetHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        fit:true,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "Ser",
        order: "ASC",
        columns: [
                    [
                      
                        { field: 'Ser', title: '街道编码', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'Name', title: '街道名称', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
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
IotM.Street.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.Street.AddClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    IotM.FormSetDefault('formAdd');
   
  
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '添加街道',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () {
            IotM.checkisValid('formAdd');
          
        }
    });
};
IotM.Street.AddClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');

       
        $.post("../Handler/StreetHandler.ashx?AType=Add",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#btnCancel').click();
                              $('#dataGrid').datagrid('appendRow', eval('(' + data.TxtMessage + ')'));
                              $.messager.alert('提示', '操作成功！', 'info');
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};
IotM.Street.OpenformEdit = function (obj) {

    $('#btnOk').unbind('click').bind('click', function () { IotM.Street.EditClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
  
    //var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    //$('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    if (!data) { $.messager.alert('提示', '没有选中行！', 'info'); return;}

    IotM.SetData('formAdd', data);

   
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '编辑街道',
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
IotM.Street.EditClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');
       
        $.post("../Handler/StreetHandler.ashx?AType=Edit",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('updateRow',
                                  {
                                      index: $('#dataGrid').datagrid('getRowIndex', $('#dataGrid').datagrid('getSelected')),
                                      row: eval('(' + data.TxtMessage + ')')
                                  });
                              $.messager.alert('提示', '操作成功！', 'info', function () {
                                  $('#wAdd').window('close');
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};


IotM.Street.RemoveClick = function (obj) {
    //var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    //$('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    if (!data) { $.messager.alert('提示', '没有选中行！', 'info'); return; }

    var index = $('#dataGrid').datagrid('getRowIndex', $('#dataGrid').datagrid('getSelected'));

    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r == true) {
            $.post("../Handler/StreetHandler.ashx?AType=Delete",
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


IotM.Street.SerachClick = function (value, name) {
    var Where="";
    if (value && name) {
        Where += '  AND ' + name + ' like \'%' + value + '%\'';
    }
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};







