IotM.namespace('IotM.MenuManage');
//加载用户列表控件
IotM.MenuManage.LoadMenuDataGrid = function () {
    var url = "../Handler/SystemManage/MenuManageHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "FatherCode",
        order: "ASC",
        columns: [[
                        { field: 'MenuCode', title: '菜单编号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'Name', title: '菜单名称', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                        {
                            field: 'Type', title: '菜单类型', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                            formatter: function (value, rec, index) {

                                if (value == "00") { return "一级菜单"; }
                                else if (value == "01") { return "二级菜单"; }
                                else if (value == "02") { return "按钮菜单"; }
                                else if (value == "03") { return "报表菜单"; }
                                else { return "未知"; }

                            }

                        },
                        { field: 'OrderNum', title: '菜单序号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'UrlClass', title: '菜单URL', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'left', sortable: true },
                        { field: 'FatherCode', title: '父菜单', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center',
                            formatter: function (value, rec, index) {
                                var e = '<a href="#" mce_href="#" menucode="bjcd" onclick="IotM.MenuManage.OpenformEdit(this)">编辑</a> ';
                                var f = '<a href="#" mce_href="#" menucode="sccd" onclick="IotM.MenuManage.RemoveClick(this)">删除</a> ';
                                return e + f ;
                            }
                        }
                    ]],
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
        },
        view: detailview,
        detailFormatter: function (index, row) {
            return '<div style="padding:2px"><table class="ddv"></table></div>';
        },

        onExpandRow: function (index, row) {
            var ddv = $(this).datagrid('getRowDetail', index).find('table.ddv');
          

            if (row["Type"] == "00") { return; }


            $.ajax({
                type: 'GET',
                async: false,
                url: '../Handler/SystemManage/MenuManageHandler.ashx?AType=Query&Code=' + row["MenuCode"],
                success: function (data, textStatus) {

                    data = eval('(' + data + ')')
                    var rows = eval('(' + data.TxtMessage + ')');
                    if (rows.rows.length == 0)
                    {
                        return;
                    }

                    IotM.MenuManage.DDVLoad(ddv, row,index);
                }

            });
        }

    });
};

IotM.MenuManage.DDVLoad = function (ddv, row,index1) {


    ddv.datagrid({
        url: '../Handler/SystemManage/MenuManageHandler.ashx?AType=Query&Code=' + row["MenuCode"],
        fitColumns: true,
        singleSelect: true,
        rownumbers: true,
        showHeader: true,
        loadMsg: '加载中...',
        height: 'auto',
        columns: [[
                { field: 'MenuCode', title: '菜单编号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                { field: 'Name', title: '菜单名称', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                {
                    field: 'Type', title: '菜单类型', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                    formatter: function (value, rec, index) {
                     
                        if (value == "00") { return "一级菜单"; }
                        else if (value == "01") { return "二级菜单"; }
                        else if (value == "02") { return "按钮菜单"; }
                        else { return "报表菜单"; }

                    }

                },
                { field: 'OrderNum', title: '菜单序号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                { field: 'UrlClass', title: '菜单URL', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'left', sortable: true },
                {
                    field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center',
                    formatter: function (value, rec, index) {
                        var e = '<a href="#" mce_href="#" menucode="bjcd" onclick="IotM.MenuManage.OpenformEdit(this,true,' + index1 + ')">编辑</a> ';
                        var f = '<a href="#" mce_href="#" menucode="sccd" onclick="IotM.MenuManage.RemoveClick(this,true,' + index1 + ')">删除</a> ';
                        return e + f;
                    }
                }
        ]],
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



IotM.MenuManage.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.MenuManage.AddClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddMenu').window('close'); });
    IotM.AddRelaseDisabled('wAddMenu');
    IotM.FormSetDefault('wAddMenu');

    $('#wAddMenu').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.35,
        title: '添加菜单',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () { IotM.checkisValid('wAddMenu'); }
    });
};
IotM.MenuManage.AddClick = function () {
    if (IotM.checkisValid('wAddMenu')) {
        var data = IotM.GetData('wAddMenu');

        if (data.Type == "03") {
            data.RID = $("#CNRID").combobox("getValue");
        }

        $.post("../Handler/SystemManage/MenuManageHandler.ashx?AType=Add",
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

IotM.MenuManage.OpenformEdit = function (obj, isGrid,gridIndex) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    var data = {};
    if (isGrid) {

        var ddv = $('#dataGrid').datagrid('getRowDetail', gridIndex).find('table.ddv');
        ddv.datagrid('selectRow', index);
        data = ddv.datagrid('getSelected');

    } else {
        $('#dataGrid').datagrid('selectRow', index);
        data = $('#dataGrid').datagrid('getSelected');

    }
    IotM.MenuManage.InitFatherCode(data.Type);
    IotM.SetData('wAddMenu', data);
    if (data.FatherCode == "") {
        $('#CNFatherCode').combobox('setValue', ' ');
    }

    $('#btnOk').unbind('click').bind('click', function () { IotM.MenuManage.EditClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddMenu').window('close'); });

    $('#wAddMenu').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.35,
        title: '编辑菜单',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () { IotM.EditDisabled('wAddMenu'); IotM.checkisValid('wAddMenu'); }
    });
};









IotM.MenuManage.EditClick = function () {
    if (IotM.checkisValid('wAddMenu')) {
        var data = IotM.GetData('wAddMenu');
        if (data.Type == "03") {
            data.RID = $("#CNRID").combobox("getValue");
        }

        $.post("../Handler/SystemManage/MenuManageHandler.ashx?AType=Edit",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('updateRow',
                                  { index: $('#dataGrid').datagrid('getRowIndex', $('#dataGrid').datagrid('getSelected')),
                                      row: eval('(' + data.TxtMessage + ')')
                                  });
                              $.messager.alert('提示', '操作成功！', 'info', function () {
                                  $('#wAddMenu').window('close');
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};



IotM.MenuManage.RemoveClick = function (obj, isGrid, gridIndex) {


    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    var dataPost = {};
    if (isGrid) {

        var ddv = $('#dataGrid').datagrid('getRowDetail', gridIndex).find('table.ddv');
        ddv.datagrid('selectRow', index);
        dataPost = ddv.datagrid('getSelected');

    } else {
        $('#dataGrid').datagrid('selectRow', index);
        dataPost = $('#dataGrid').datagrid('getSelected');

    }
    debugger;
    $.messager.confirm("确认", "是否真的删除菜单'" + dataPost.Name + "'?", function (r) {
        if (r == true) {
            $.post("../Handler/SystemManage/MenuManageHandler.ashx?AType=Delete",
                 dataPost,
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




IotM.MenuManage.SerachClick = function (value, name) {
    var Where = '';
    if (value && name) {
        Where = name + ' like \'%' + value + '%\'';
    }
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};


IotM.MenuManage.InitFatherCode=function(type){


    //一级菜单
    if (type == '00') {
        $('#CNFatherCode').combobox({ disabled: true });
        $('#CNFatherCode').combobox('setValue', ' ');
        $('#divReport').hide();
        return;
    }
    //二级菜单
    if (type == '01') {

        $('#CNFatherCode').combobox({ disabled: false, });
        $('#divReport').hide();
        IotM.Initiate.LoadFatherCodeFirstComboBox('CNFatherCode', false, true);
        return;
    }
    //按钮菜单
    if (type == '02') {
        $('#CNFatherCode').combobox({ disabled: false, });
        $('#divReport').hide();
        IotM.Initiate.LoadFatherCodeSecondComboBox('CNFatherCode', false, true);
        return;
    }
    //报表菜单
    if (type == '03') {
        $('#CNFatherCode').combobox({ disabled: false, });
        $('#divReport').show();
        

        IotM.Initiate.LoadFatherCodeFirstComboBox('CNFatherCode', false, true);
        return;
    }

}

