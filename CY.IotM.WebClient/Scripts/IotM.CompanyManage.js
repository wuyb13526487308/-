IotM.namespace('IotM.CompanyManage');
//加载用户列表控件
IotM.CompanyManage.LoadCompanyDataGrid = function () {
    var url = "../Handler/SystemManage/CompanyManageHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "AreaID",
        order: "ASC",
        columns: [
                   [
                       { field: 'CompanyID', title: '企业编号', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                        { field: 'CompanyName', title: '企业名称', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                        { field: 'Status', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true, formatter: function (value, row) {
                            for (var i = 0; i < IotM.Initiate.CompanyOperatorStates.length; i++) {
                                if (IotM.Initiate.CompanyOperatorStates[i].Value == value) {
                                    return IotM.Initiate.CompanyOperatorStates[i].Name
                                }
                            }
                            return "Status:" + value
                        }
                        },
                        { title: '联系方式', colspan: 3 },
                        { field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center',
                            formatter: function (value, rec, index) {
                                var e = '<a href="#" mce_href="#" menucode="bjzcqy" onclick="IotM.CompanyManage.OpenformEdit(this)">编辑</a> ';
                                var f = '<a href="#" mce_href="#" menucode="qyzzhcsh" onclick="IotM.CompanyManage.ResetCompanyAdminClick(this)">初始化</a> ';
                                var g = '<a href="#" mce_href="#" onclick="IotM.CompanyManage.OpenFormEditCompanyMenu(this)">分配菜单</a> ';
                                var h = '<a href="#" mce_href="#" menucode="qcqyqxhc" onclick="IotM.CompanyManage.RemoveCompanyCacheClick(this)">更新缓存</a> ';
                                return e + f + g + h;
                            }
                        }
                    ],
                    [
                        { field: 'Linkman', title: '联系人', width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                        { field: 'Phone', title: '联系电话', width: IotM.MainGridWidth * 0.10, align: 'center', sortable: true },
                        { field: 'Address', title: '通讯地址', width: IotM.MainGridWidth * 0.3, align: 'center', sortable: true }
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
IotM.CompanyManage.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.CompanyManage.AddCompanyClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddCompany').window('close'); });
    IotM.AddRelaseDisabled('wAddCompany');
    IotM.FormSetDefault('wAddCompany');
    $('#wAddCompany').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '添加企业',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () { IotM.checkisValid('wAddCompany'); }
    });
};
IotM.CompanyManage.AddCompanyClick = function () {
    if (IotM.checkisValid('wAddCompany')) {
        var data = IotM.GetData('wAddCompany');
        $.post("../Handler/SystemManage/CompanyManageHandler.ashx?AType=Add",
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

IotM.CompanyManage.OpenformEdit = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.SetData('wAddCompany', data);

    $('#btnOk').unbind('click').bind('click', function () { IotM.CompanyManage.EditCompanyClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddCompany').window('close'); });

    $('#wAddCompany').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '编辑企业',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () { IotM.EditDisabled('wAddCompany'); IotM.checkisValid('wAddCompany'); }
    });
};
IotM.CompanyManage.EditCompanyClick = function () {
    if (IotM.checkisValid('wAddCompany')) {
        var data = IotM.GetData('wAddCompany');
        $.post("../Handler/SystemManage/CompanyManageHandler.ashx?AType=Edit",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('updateRow',
                                  { index: $('#dataGrid').datagrid('getRowIndex', $('#dataGrid').datagrid('getSelected')),
                                      row: eval('(' + data.TxtMessage + ')')
                                  });
                              $.messager.alert('提示', '操作成功！', 'info', function () {
                                  $('#wAddCompany').window('close');
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};
IotM.CompanyManage.RemoveCompanyCacheClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '确定更新企业权限菜单缓存吗', function (r) {
        if (r == true) {
            $.post("../Handler/SystemManage/CompanyManageHandler.ashx?AType=RemoveCache",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
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
IotM.CompanyManage.ResetCompanyAdminClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '确定要对企业主账号进行初始化吗', function (r) {
        if (r == true) {
            $.post("../Handler/SystemManage/CompanyManageHandler.ashx?AType=ResetCompanyAdmin",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
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
IotM.CompanyManage.SerachClick = function (value, name) {
    var Where = '';
    if (value && name) {
        Where = name + ' like \'' + value + '\'';
    }
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};




IotM.CompanyManage.OpenFormEditCompanyMenu = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    $('#btnCompanyMenuOk').unbind('click').bind('click', function () { IotM.CompanyManage.EditCompanyMenuClick(data) });

    $('#wEditCompanyMenu').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '分配企业菜单',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () { IotM.CompanyManage.LoadCompanyMenu(data.CompanyID) }
    });

};




IotM.CompanyManage.LoadCompanyMenu = function (companyId) {
    $.ajax({
        url: "../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=LoadNewCompanyMenu",
        async: true,
        type: "POST",
        data: {},
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                var treeJson = eval('(' + decodeURI(data) + ')');
                //绑定菜单列表
                $('#treeMenuCode').tree({ checkbox: true, data: eval('(' + treeJson.TxtMessage + ')'), cascadeCheck: true });
                $('#treeMenuCode').tree('collapseAll');
                $.ajax({
                    url: "../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=LoadMenuByCompany",
                    async: true,
                    type: "POST",
                    data: { CompanyID: companyId },
                    success: function (data, textStatus) {
                        if (textStatus == 'success') {
                            var nodes = $('#treeMenuCode').tree('getChildren');
                            //nodes = $(nodes).sort(function (a, b) {
                            //    return a.attributes.type > b.attributes.type;
                            //});
                            for (var i = 0; i < nodes.length; i++) {
                                var node = nodes[i];
                                if (node) {
                                    var nData = $('#treeMenuCode').tree('getData', node.target);
                                    if (nData.attributes) {
                                        var nMenuCode = node.attributes.menucode;
                                        if (data.indexOf(',' + nMenuCode + ',') > -1) {
                                            $('#treeMenuCode').tree('check', node.target);
                                        }
                                        else {
                                            $('#treeMenuCode').tree('uncheck', node.target);
                                        }
                                    }
                                }
                            }
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        alert(errorThrown);
                        $.messager.alert('警告', "访问数据中心失败。", 'warn');
                    }
                });
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "访问数据中心失败。", 'warn');
        }
    });

};
IotM.CompanyManage.GetTreeMenuCode = function () {
    var nodes = $('#treeMenuCode').tree('getChildren');
    var nodeStr = "";
    for (var i = 0; i < nodes.length; i++) {
        var node = nodes[i];
        if (IotM.CompanyManage.GetTreeMenuChecked(node)) {

            nodeStr += node.attributes.menucode;
            nodeStr += ",";
        }
    }
    if (nodeStr != "") {
        nodeStr = nodeStr.substring(0, nodeStr.length - 1);
    }
    return nodeStr;

}
IotM.CompanyManage.GetTreeMenuName = function () {
    var nodes = $('#treeMenuCode').tree('getChildren');
    var nodeStr = "";
    for (var i = 0; i < nodes.length; i++) {
        var node = nodes[i];
        if (IotM.CompanyManage.GetTreeMenuChecked(node)) {
            nodeStr += node.text;
            nodeStr += "，";
        }
    }
    if (nodeStr != "") {
        nodeStr = nodeStr.substring(0, nodeStr.length - 1);
    }
    return nodeStr;

}
IotM.CompanyManage.GetTreeMenuChecked = function (node) {
    var result = false;
    if (node.checked)
    { result = true; }
    else {
        var nodes = $('#treeMenuCode').tree('getChildren', node.target);
        for (var i = 0; i < nodes.length; i++) {
            var node1 = nodes[i];
            if (node1.checked) {
                result = true;
                break;
            }
        }
    }
    return result;
}

IotM.CompanyManage.EditCompanyMenuClick = function (obj) {
    
    var dataPost = {};
    dataPost.CompanyID = obj.CompanyID;
    dataPost.CompanyMenuCode = IotM.CompanyManage.GetTreeMenuCode();
    var CompanyMenuName = IotM.CompanyManage.GetTreeMenuName();
    $.messager.confirm('确认', '确定修改吗?', function (r) {
        if (r == true) {
            //获取树权限
            $.post("../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=EditCompanyMenu",
                dataPost,
                function (data, textStatus) {
                    if (textStatus == 'success') {
                        if (data.Result) {
                           
                            IotM.AddSystemLog({ LogType: 3, Context: '修改公司菜单【' + dataPost.CompanyMenuCode + '】，菜单【' + CompanyMenuName + '】。' });
                            $.messager.alert('提示', '操作成功！', 'info', function () {
                                $('#wEditCompanyMenu').window('close');
                            });
                        }
                        else
                            $.messager.alert('警告', data.TxtMessage, 'warn');

                    }
                }, "json");

        }
    });
    
};