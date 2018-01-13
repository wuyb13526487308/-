IotM.namespace('AdM.User');

//加载列表
AdM.User.LoadDataGridView = function () {
    var url = "../Handler/ADUserHandler.ashx?AType=QUERYVIEW";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        //rownumbers: true,
        singleSelect: true,
        sort: "UserID",
        order: "asc",
        columns: [
                    [
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.07, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.07, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.08, align: 'center', sortable: true },
                     { field: 'CompanyID', title: '企业编号', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Adress', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'left', sortable: true },
                     { field: 'AddTime', title: '登记时间', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Context', title: '广告主题', rowspan: 2, width: IotM.MainGridWidth * 0.12, align: 'center', sortable: true },
                     { field: 'PublishDate', title: '发布时间', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                    
                      {
                          field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center',
                          formatter: function (value, rec, index) {
                              var b = '<a href="#" mce_href="#" menucode="bjyh" onclick="AdM.User.OpenformDelOneUser(this)"><span style="color:blue">移除</span></a> ';
                              return b
                          }
                      }
                     
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


//查询数据
AdM.User.SerachClick = function () {
    var Where = "";

    if ($("#CNStreet").combobox("getText") != "" && $("#CNStreet").combobox("getText") != "全部") {
        Where += '  AND  Street  like \'%' + $("#CNStreet").combobox("getText") + '%\'';
    }
    if ($("#CNCommunity").combobox("getText") != "" && $("#CNCommunity").combobox("getText")!="全部") {
        Where += '  AND  Community  like \'%' + $("#CNCommunity").combobox("getText") + '%\'';
    }
    if ($("#MeterNo").val() != "") {
        Where += '  AND  MeterNo  like \'%' + $("#MeterNo").val() + '%\'';
    }
    if ($("#CNAC_ID").combobox("getValue") != "") {
        Where += '  AND  AC_ID  = ' + $("#CNAC_ID").combobox("getValue") + ' ';
    }
    if ($("#Adress").val() != "") {
        Where += '  AND  Adress  like \'%' + $("#Adress").val() + '%\'';
    }
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};



AdM.User.OpenformDelOneUser = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    
    $.messager.confirm('确认', '是否真的删除？', function (r) {
        if (r == true) {
            $.post("../Handler/ADUserHandler.ashx?AType=DELETE",
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

//批量添加用户
AdM.User.OpenformAddGroupUser = function () {

    $('#btnOk_User').unbind('click').bind('click', function () { AdM.User.BatchAddClick(); });
    $('#btnCancel_User').unbind('click').bind('click', function () { $('#UserDiv').window('close'); });

    //AdM.User.LoadDataGridUserInfo(" AND UserID+CompanyID not in (select UserID+CompanyID from ADUser)");
    AdM.User.LoadDataGridUserInfo("QUERYVIEWUSERADD");
    $('#UserDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '批量添加用户',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100

    });
};

//批量删除用户
AdM.User.OpenformDelGroupUser = function () {

    $('#btnOk_User').unbind('click').bind('click', function () { AdM.User.BatchDelClick(); });
    $('#btnCancel_User').unbind('click').bind('click', function () { $('#UserDiv').window('close'); });

    //AdM.User.LoadDataGridUserInfo(" AND UserID+CompanyID in (select UserID+CompanyID from ADUser)");
    AdM.User.LoadDataGridUserInfo("QUERYVIEWUSER");
    $('#UserDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '批量删除用户',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100

    });
};



//查询数据
AdM.User.DataGridUserSerachClick = function () {
    var Where = "";
    if ($("#select_Community").val() != "") {
        Where += '  AND  CommunityName  like \'%' + $("#select_Community").val() + '%\'';
    }
    if ($("#select_Street").val() != "") {
        Where += '  AND  StreetName  like \'%' + $("#select_Street").val() + '%\'';
    }
    if ($("#select_Adress").val() != "") {
        Where += '  AND  Address  like \'%' + $("#select_Adress").val() + '%\'';
    }

    $('#dataGrid_User').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid_User').datagrid('load');
};

//加载用户列表视图
AdM.User.LoadDataGridUserInfo = function (typeStr) {

    var url = "../Handler/ADUserHandler.ashx?AType=" + typeStr;
    $('#dataGrid_User').datagrid({
        title: '',
        toolbar: '#tb_User',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        sort: "UserID",
        order: "ASC",
        columns: [
                    [

                     { field: 'ck', checkbox: true },
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     { field: 'CompanyID', title: '企业编号', hidden:true},
                     { field: 'StreetName', title: '街道', hidden: true },
                     { field: 'CommunityName', title: '小区', hidden: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.5, align: 'center', sortable: true }

                    ]

        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_User').datagrid('getPager');
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


//确认批量添加
AdM.User.BatchAddClick = function () {

    var rows = $('#dataGrid_User').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }
    var strNo = "";
    var data = {};
    for (var i = 0; i < rows.length; i++) {
        var index = $('#dataGrid_User').datagrid('getRowIndex', rows[i]);
        if (i == rows.length - 1) {
            strNo += rows[i].UserID + "|" + rows[i].CompanyID + "|" + rows[i].StreetName + "|" + rows[i].CommunityName + "|" + rows[i].Address;
        }
        else {
            strNo += rows[i].UserID + "|" + rows[i].CompanyID + "|" + rows[i].StreetName + "|" + rows[i].CommunityName + "|" + rows[i].Address + ",";
        }
    }
    data.strNo = strNo;

    $.post("../Handler/ADUserHandler.ashx?AType=GROUPADD",
              data,
               function (data, textStatus) {
                   if (textStatus == 'success') {
                       if (data.Result) {

                           $.messager.alert('提示', '操作成功！', 'info', function () {
                               $('#dataGrid_User').datagrid('reload');
                               $('#dataGrid').datagrid('reload');
                           });
                       }
                       else
                           $.messager.alert('警告', data.TxtMessage, 'warn');

                   }
               }, "json");
}


//确认批量删除
AdM.User.BatchDelClick = function () {

    var rows = $('#dataGrid_User').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }
    var strNo = "";
    var data = {};
    for (var i = 0; i < rows.length; i++) {
        var index = $('#dataGrid_User').datagrid('getRowIndex', rows[i]);
        if (i == rows.length - 1) {
            strNo += rows[i].UserID + "|" + rows[i].CompanyID ;
        }
        else {
            strNo += rows[i].UserID + "|" + rows[i].CompanyID + ",";
        }
    }
    data.strNo = strNo;

    $.post("../Handler/ADUserHandler.ashx?AType=GROUPDEL",
              data,
               function (data, textStatus) {
                   if (textStatus == 'success') {
                       if (data.Result) {

                           $.messager.alert('提示', '操作成功！', 'info', function () {
                               $('#dataGrid_User').datagrid('reload');
                               $('#dataGrid').datagrid('reload');
                           });
                       }
                       else
                           $.messager.alert('警告', data.TxtMessage, 'warn');

                   }
               }, "json");
}

//数据初始化-------------------------------------------------------------------------
//提取街道信息
AdM.User.Street = [];
AdM.User.InitStreet = function () {
    $.ajax({
        url: "../Handler/StreetHandler.ashx?AType=QUERY",
        async: false,
        type: "POST",
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data.TxtMessage + ')');
                if (data != null && data.total > 0) {
                    AdM.User.Street = data.rows;
                } else {
                    AdM.User.Street = [{ ID: '', Name: '' }];
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "获取街道失败。", 'warn');
        },
        dataType: "json"
    }
   );
};

AdM.User.LoadStreetComboBox = function (id, withAll, required) {
    AdM.User.InitStreet();
    var data = IotM.ObjectClone(AdM.User.Street);
    if (withAll) {
        data.push({ ID: '', Name: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].ID,
        valueField: 'ID',
        textField: 'Name',
        //required: required,
        editable: false

    }
    );
};




//提取信息
AdM.User.Context = [];
AdM.User.InitAdContext = function () {
    $.ajax({
        url: "../Handler/AdMHandler.ashx?AType=QUERYVIEWLIST",
        async: false,
        type: "POST",
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data.TxtMessage + ')');
                if (data != null && data.total > 0) {
                    AdM.User.Context = data.rows;
                } else {
                    AdM.User.Context = [{ AC_ID: '', Context: '' }];
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "获取主题失败。", 'warn');
        },
        dataType: "json"
    }
   );
};
//加载广告主题列表

AdM.User.LoadADContextComboBox = function (id, withAll, required) {
    AdM.User.InitAdContext();
    var data = IotM.ObjectClone(AdM.User.Context);
    if (withAll) {
        data.push({ AC_ID: '', Context: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].ID,
        valueField: 'AC_ID',
        textField: 'Context',
        //required: required,
        editable: false
    });
};