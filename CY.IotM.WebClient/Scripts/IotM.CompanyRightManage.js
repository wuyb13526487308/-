IotM.namespace('IotM.CompanyRightManage');
//加载操作员列表控件
IotM.CompanyRightManage.LoadCompanyRightDataGrid = function () {
    var url = "../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=LoadCompanyRight";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "RightCode",
        order: "ASC",
        columns: [
                    [
                       { field: 'CompanyID', title: '单位编码', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                       { field: 'RightCode', title: '权限编码', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                       { field: 'RightName', title: '权限名称', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                       { field: 'Context', title: '备注', width: IotM.MainGridWidth * 0.15, align: 'center', sortable: false },
                       { field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center',
                           formatter: function (value, rec, index) {
                               var e = '<a href="#" mce_href="#" menucode="bjqxz" onclick="IotM.CompanyRightManage.OpenformEdit(this)">编辑</a> ';
                               var d = '<a href="#" mce_href="#" menucode="scqxz" onclick="IotM.CompanyRightManage.RemoveCompanyRightClick(this)">删除</a> ';
                               return e + d;
                           }
                       }
                    ]],
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
IotM.CompanyRightManage.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.CompanyRightManage.AddCompanyRightClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddCompanyRight').window('close'); });
    IotM.AddRelaseDisabled('formAdd');
    IotM.FormSetDefault('formAdd');
    $('#wAddCompanyRight').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        height: IotM.MainGridHeight * 0.6,
        title: '添加权限组',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 100,
        left:150,
        onOpen: function () { IotM.checkisValid('formAdd'); IotM.CompanyRightManage.LoadCompanyMenu(''); }
    });
};
IotM.CompanyRightManage.AddCompanyRightClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var dataPost = IotM.GetData('formAdd');
        dataPost.RightMenuCode = IotM.CompanyRightManage.GetTreeMenuCode();
        var rightMenuName = IotM.CompanyRightManage.GetTreeMenuName();
        $.post("../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=AddQxz",
                 dataPost,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#btnCancel').click();
                              $('#dataGrid').datagrid('appendRow', eval('(' + data.TxtMessage + ')'));                           
                              $.messager.alert('提示', '操作成功！', 'info');
                              IotM.AddSystemLog({ LogType: 2, Context: '增加权限组编码【' + dataPost.RightCode + '】，菜单【' + rightMenuName + '】。' });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};
IotM.CompanyRightManage.OpenformEdit = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.FormSetDefault('formAdd');
    IotM.SetData('formAdd', data);

    $('#btnOk').unbind('click').bind('click', function () { IotM.CompanyRightManage.EditCompanyRightClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddCompanyRight').window('close'); });
    $('#wAddCompanyRight').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        height: IotM.MainGridHeight * 0.6,
        title: '编辑权限组',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 100,
        left: 150,
        onOpen: function () { IotM.EditDisabled('formAdd'); IotM.checkisValid('formAdd'); IotM.CompanyRightManage.LoadCompanyMenu(data.RightCode); }
    });
};
IotM.CompanyRightManage.EditCompanyRightClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var dataPost = IotM.GetData('formAdd');
        dataPost.RightMenuCode = IotM.CompanyRightManage.GetTreeMenuCode();
        var rightMenuName = IotM.CompanyRightManage.GetTreeMenuName();
        $.messager.confirm('确认', '确定修改吗?', function (r) {
            if (r == true) {
                //获取树权限
                $.post("../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=EditQxz",
                 dataPost,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('updateRow',
                                  { index: $('#dataGrid').datagrid('getRowIndex', $('#dataGrid').datagrid('getSelected')),
                                      row: eval('(' + data.TxtMessage + ')')
                                  });
                              IotM.AddSystemLog({ LogType: 3, Context: '修改权限组编码【' + dataPost.RightCode + '】，菜单【' + rightMenuName + '】。' });
                              $.messager.alert('提示', '操作成功！', 'info', function () {
                                  $('#wAddCompanyRight').window('close');
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");

            }
        });
    }
};
IotM.CompanyRightManage.RemoveCompanyRightClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var dataPost = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r == true) {
            $.post("../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=DeleteQxz",
                 dataPost,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('deleteRow', parseInt(index));
                              IotM.AddSystemLog({ LogType: 4, Context: '删除权限组编码【' + dataPost.RightCode + '】。' });
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
IotM.CompanyRightManage.LoadCompanyMenu = function (rightCode) {
    $.ajax({
        url: "../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=LoadCompanyMenu",
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
                    url: "../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=LoadRightMenu",
                    async: true,
                    type: "POST",
                    data: { rightCode: rightCode },
                    success: function (data, textStatus) {
                        if (textStatus == 'success') {
                            var nodes = $('#treeMenuCode').tree('getChildren');
                            nodes = $(nodes).sort(function (a, b) {
                                return a.attributes.type > b.attributes.type;
                            });
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
IotM.CompanyRightManage.GetTreeMenuCode = function () {
    var nodes = $('#treeMenuCode').tree('getChildren');
    var nodeStr = "";
    for (var i = 0; i < nodes.length; i++) {
        var node = nodes[i];
        if (IotM.CompanyRightManage.GetTreeMenuChecked(node)) {
            nodeStr += node.attributes.menucode;
            nodeStr += ",";
        }
    }
    if (nodeStr != "") {
        nodeStr = nodeStr.substring(0, nodeStr.length - 1);
    }
    return nodeStr;

}
IotM.CompanyRightManage.GetTreeMenuName = function () {
    var nodes = $('#treeMenuCode').tree('getChildren');
    var nodeStr = "";
    for (var i = 0; i < nodes.length; i++) {
        var node = nodes[i];
        if (IotM.CompanyRightManage.GetTreeMenuChecked(node)) {
            nodeStr += node.text;
            nodeStr += "，";
        }
    }
    if (nodeStr != "") {
        nodeStr = nodeStr.substring(0, nodeStr.length - 1);
    }
    return nodeStr;

}
IotM.CompanyRightManage.GetTreeMenuChecked = function (node) {
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