IotM.namespace('IotM.CompanyOperatorManage');
//加载操作员列表控件
IotM.CompanyOperatorManage.LoadCompanyOperatorDataGrid = function () {
    var url = "../Handler/SystemManage/CompanyOperatorManageHandler.ashx?AType=Query";
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
                    [
                       { field: 'OperID', title: '人员编号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'Name', title: '姓名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        {
                            field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true, formatter: function (value, row) {
                                for (var i = 0; i < IotM.Initiate.CompanyOperatorStates.length; i++) {
                                    if (IotM.Initiate.CompanyOperatorStates[i].Value == value) {
                                        return IotM.Initiate.CompanyOperatorStates[i].Name
                                    }
                                }
                                return "State:" + value
                            }
                        },
                        {
                            field: 'OperType', title: '企业主账号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true, formatter: function (value, row) {
                                return value == 1 ? "√" : "×"
                            }
                        },
                        { title: '联系方式', colspan: 2 },
                        {
                            field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center',
                            formatter: function (value, rec, index) {
                                var e = '<a href="#" mce_href="#" menucode="bjczy" onclick="IotM.CompanyOperatorManage.OpenformEdit(this)">编辑</a> ';
                                var d = '<a href="#" mce_href="#" menucode="scczy" onclick="IotM.CompanyOperatorManage.RemoveCompanyOperatorClick(this)">删除</a> ';
                                var f = '<a href="#" mce_href="#" menucode="czmm"  onclick="IotM.CompanyOperatorManage.ResetPwdClick(this)">重置密码</a> ';
                                var g = '<a href="#" mce_href="#" menucode="fpqxz" onclick="IotM.CompanyOperatorManage.OpenformManageOperRight(this)">权限组</a> ';
                               
                                return e + d + f + g ;
                            }
                        }
                    ],
                    [
                        { field: 'Phone', title: '电话', width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                        { field: 'Mail', title: '邮箱', width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true }
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
IotM.CompanyOperatorManage.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.CompanyOperatorManage.AddCompanyOperatorClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddCompanyOperator').window('close'); });
    $('#TreeRow').hide();
    IotM.AddRelaseDisabled('formAdd');
    IotM.FormSetDefault('formAdd');
    $('#wAddCompanyOperator').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '添加操作员',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () { IotM.checkisValid('formAdd'); }
    });
};
IotM.CompanyOperatorManage.AddCompanyOperatorClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');
        $.post("../Handler/SystemManage/CompanyOperatorManageHandler.ashx?AType=Add",
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
IotM.CompanyOperatorManage.OpenformEdit = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.FormSetDefault('formAdd');
    IotM.SetData('formAdd', data);

    $('#btnOk').unbind('click').bind('click', function () { IotM.CompanyOperatorManage.EditCompanyOperatorClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddCompanyOperator').window('close'); });
    $('#wAddCompanyOperator').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '编辑操作员',
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
IotM.CompanyOperatorManage.EditCompanyOperatorClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');
        var nodeStr = "";
        data.nodes = nodeStr;
        $.post("../Handler/SystemManage/CompanyOperatorManageHandler.ashx?AType=Edit",
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
                                  $('#wAddCompanyOperator').window('close');
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};
IotM.CompanyOperatorManage.RemoveCompanyOperatorClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r == true) {
            $.post("../Handler/SystemManage/CompanyOperatorManageHandler.ashx?AType=Delete",
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
IotM.CompanyOperatorManage.ResetPwdClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '是否真的要重置密码?', function (r) {
        if (r == true) {
            $.post("../Handler/SystemManage/CompanyOperatorManageHandler.ashx?AType=ResetPwd",
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
IotM.CompanyOperatorManage.SerachCompanyOperatorClick = function (value, name) {
    var Where = '';
    if (value && name) {
        Where += '  AND ' + name + ' like \'%' + value + '%\'';
    }
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};
IotM.CompanyOperatorManage.OpenformChangePwd = function () {
    IotM.SetData('formAdd', IotM.loginCompanyOperator);
    $('#btnOk').unbind('click').bind('click', function () { IotM.CompanyOperatorManage.ChangePwdClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddCompanyOperator').window('close'); });
    $('#wAddCompanyOperator').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '修改密码',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false
    });
};
IotM.CompanyOperatorManage.ChangePwdClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');
        $.post("../Handler/SystemManage/CompanyOperatorManageHandler.ashx?AType=EditPwd",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $.messager.alert('提示', '操作成功！', 'info', function () {
                                  $('#btnCancel').click();
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};
IotM.CompanyOperatorManage.OperID = "";
IotM.CompanyOperatorManage.OperRight = "";
IotM.CompanyOperatorManage.OpenformManageOperRight = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $('#wAddOperRight').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.6,
        top: ($(window).height() - 400) * 0.25,
        left: ($(window).width() - 400) * 0.25,
        title: '权限组',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () {
            IotM.CompanyOperatorManage.OperID = data.OperID;
            IotM.CompanyOperatorManage.LoadCompanyOperRight();
            IotM.CompanyOperatorManage.LoadCompanyRightDataGrid();
        }
    });
};

IotM.CompanyOperatorManage.LoadCompanyOperRight = function () {
    $.ajax({
        url: "../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=LoadCompanyOperRight",
        async: true,
        type: "POST",
        data: { OperID: IotM.CompanyOperatorManage.OperID },
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                IotM.CompanyOperatorManage.OperRight = data;
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(errorThrown);
            $.messager.alert('警告', "访问数据中心失败。", 'warn');
        }
    });
};
IotM.CompanyOperatorManage.LoadCompanyRightDataGrid = function () {
    var url = "../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=LoadCompanyRight";
    $('#dataGridCompanyRight').datagrid({
        title: '',
        toolbar: '#tbOperRight',
        url: url,
        fitColumns: true,
        nowrap: false,
        pagination: false,
        rownumbers: true,
        singleSelect: true,
        sort: "UserID",
        order: "ASC",
        columns: [
                    [
                       {
                           field: 'P', title: '选择', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false,
                           formatter: function (value, rec, index) {
                               var isChecked = (',' + IotM.CompanyOperatorManage.OperRight + ',').indexOf(rec.RightCode) != -1 ? 'checked="checked"' : '';
                               var e = '<input type="checkbox" name="rightcode_' + rec.RightCode + '" ' + isChecked + '/>';
                               return e;
                           }
                       },
                       { field: 'CompanyID', title: '单位编码', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                       { field: 'RightCode', title: '权限编码', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                       { field: 'RightName', title: '权限名称', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                       { field: 'Context', title: '备注', width: IotM.MainGridWidth * 0.5, align: 'center', sortable: false }
                    ]
        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { }
    });
};
IotM.CompanyOperatorManage.SaveCompanyOperRight = function () {
    var dataPost = { OperID: IotM.CompanyOperatorManage.OperID };
    var rightCode = "";
    $('#wAddOperRight input[name^=\'rightcode_\']').each(function () {
        if ($(this).attr("checked")) {
            rightCode += $(this).attr("name").replace("rightcode_", "") + ",";
        }
    });
    if (rightCode != "") {
        rightCode = rightCode.substring(0, rightCode.length - 1);
    }
    dataPost.RightCode = rightCode;
    $.messager.confirm('确认', '确定修改吗?', function (r) {
        if (r == true) {
            $.post("../Handler/SystemManage/CompanyRightManageHandler.ashx?AType=EditOperRight",
            dataPost,
            function (data, textStatus) {
                if (textStatus == 'success') {
                    if (data.Result) {
                        IotM.AddSystemLog({ LogType: 5, Context: '分配操作员【' + dataPost.OperID + '】权限组【' + dataPost.RightCode + '】。' })
                        $.messager.alert('提示', '操作成功！', 'info', function () {
                            $('#btnCancel').click();
                        });

                    }
                    else
                        $.messager.alert('警告', data.TxtMessage, 'warn');

                }
            }, "json");
        }
    }
   );

}

//加载用户树
IotM.CompanyOperatorManage.LoadAreaTree = function (operId) {
    $.ajax({
        url: "../Handler/SystemManage/CompanyOperatorManageHandler.ashx?AType=LoadAreaTree",
        async: true,
        type: "POST",
        data: { OperID: operId },
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                var treeJson = eval('(' + decodeURI(data) + ')');
                //绑定菜单列表
                $('#UserTree').tree({
                    checkbox: true,
                    cascadeCheck: false,
                    data: eval('(' + treeJson.TxtMessage + ')'),
                    onLoadSuccess: function () {
                        $('#UserTree').tree('expandAll');
                    },
                    onCheck: function (node, checked) {
                        if (node.id == 0) {
                            if (checked) {
                                var nodeList = $('#UserTree').tree("getChildren", node.target);
                                for (var i = 0; i < nodeList.length; i++) {
                                    $('#UserTree').tree("check", nodeList[i].target);
                                }
                            } else {
                                var nodeList = $('#UserTree').tree("getChildren", node.target);
                                for (var i = 0; i < nodeList.length; i++) {
                                    $('#UserTree').tree("uncheck", nodeList[i].target);
                                }
                            }
                        }
                    }
                });

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "访问数据中心失败。", 'warn');
        }
    });

};
