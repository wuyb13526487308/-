IotM.namespace('IotM.Community');
//加载列表控件
IotM.Community.LoadDataGrid = function (streetArr) {
    var url = "../Handler/CommunityHandler.ashx?AType=Query";
    $('#dataGridCommunity').datagrid({
        title: '',
        toolbar: '#tbCommunity',
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
                        { field: 'Ser', title: '小区编码', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        {
                            field: 'StreetID', title: '所属街道', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                            formatter: function (value, row) {
                                for (var i = 0; i < streetArr.length; i++) {
                                    if (streetArr[i].ID == value) {
                                        return streetArr[i].Name
                                    }
                                }
                                return "Status:" + value
                            }

                        },
                        { field: 'Name', title: '小区名称', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                      
                    ]
                   
        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGridCommunity').datagrid('getPager');
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
IotM.Community.OpenformAdd = function () {
    $('#btnOkC').unbind('click').bind('click', function () { IotM.Community.AddClick(); });
    $('#btnCancelC').unbind('click').bind('click', function () { $('#wAddCommunity').window('close'); });
    IotM.FormSetDefault('formAddCommunity');

    IotM.Initiate.LoadStreetComboBox('CNStreetID', false, true);
   
  
    $('#wAddCommunity').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '添加小区',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () {
            IotM.checkisValid('formAddCommunity');
          
        }
    });
};
IotM.Community.AddClick = function () {
    if (IotM.checkisValid('formAddCommunity')) {
        var data = IotM.GetData('formAddCommunity');

       
        $.post("../Handler/CommunityHandler.ashx?AType=Add",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#btnCancelC').click();
                              $('#dataGridCommunity').datagrid('appendRow', eval('(' + data.TxtMessage + ')'));
                              $.messager.alert('提示', '操作成功！', 'info');
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};
IotM.Community.OpenformEdit = function (obj) {
    $('#btnOkC').unbind('click').bind('click', function () { IotM.Community.EditClick(); });
    $('#btnCancelC').unbind('click').bind('click', function () { $('#wAddCommunity').window('close'); });

    IotM.Initiate.LoadStreetComboBox('CNStreetID', false, true);
  
    //var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    //$('#dataGridCommunity').datagrid('selectRow', index);
    var data = $('#dataGridCommunity').datagrid('getSelected');

    if (!data) { $.messager.alert('提示', '没有选中行！', 'info'); return; }

    IotM.SetData('formAddCommunity', data);

   
    $('#wAddCommunity').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '编辑小区',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () {
            IotM.EditDisabled('formAddCommunity');
            IotM.checkisValid('formAddCommunity');
         
        }
    });
};
IotM.Community.EditClick = function () {
    if (IotM.checkisValid('formAddCommunity')) {
        var data = IotM.GetData('formAddCommunity');
       
        $.post("../Handler/CommunityHandler.ashx?AType=Edit",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGridCommunity').datagrid('updateRow',
                                  {
                                      index: $('#dataGridCommunity').datagrid('getRowIndex', $('#dataGridCommunity').datagrid('getSelected')),
                                      row: eval('(' + data.TxtMessage + ')')
                                  });
                              $.messager.alert('提示', '操作成功！', 'info', function () {
                                  $('#wAddCommunity').window('close');
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};


IotM.Community.RemoveClick = function (obj) {
    //var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    //$('#dataGridCommunity').datagrid('selectRow', index);
    var data = $('#dataGridCommunity').datagrid('getSelected');

    if (!data) { $.messager.alert('提示', '没有选中行！', 'info'); return; }

    var index = $('#dataGridCommunity').datagrid('getRowIndex', $('#dataGridCommunity').datagrid('getSelected'));

    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r == true) {
            $.post("../Handler/CommunityHandler.ashx?AType=Delete",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGridCommunity').datagrid('deleteRow', parseInt(index));
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


IotM.Community.SerachClick = function (value, name) {
    var Where="";
    if (value && name) {
        Where += '  AND ' + name + ' like \'%' + value + '%\'';
    }
    $('#dataGridCommunity').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGridCommunity').datagrid('load');
};







