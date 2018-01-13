IotM.namespace('AdM.Publish');
//加载主题列表
AdM.Publish.LoadDataGridView = function () {
    var url = "../Handler/AdPublishHandler.ashx?AType=QUERYVIEW";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        //rownumbers: true,
        singleSelect: true,
        sort: "AP_ID",
        order: "asc",
        columns: [
                    [
                     { field: 'AP_ID', title: '序号', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'Context', title: '广告主题', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     { field: 'AreaContext', title: '区域描述', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center',sortable: true },
                     { field: 'PublishDate', title: '发布时间', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'UserCount', title: '总户数', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {

                             if (value == "1") { return "已发布"; }
                             else if (value == "0" ) { return "未发布"; }
                             else if (value == "2" ) { return "重新发布"; }
                             else { return "未知"; }
                         }
                     },
                    
                      {
                          field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center',
                          formatter: function (value, rec, index) {
                              var a = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.Publish.OpenformEdit(this)"><span style="color:blue">编辑</span></a>  ';
                              
                              if (rec.State == "0") {
                                  var b = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.Publish.RemoveClick(this)"><span style="color:blue">删除</span></a>  ';
                                  var c = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.Publish.PublishClick(this)"><span style="color:blue">发布</span></a> ';

                              } else if (rec.State == "2") {
                                  var b = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.Publish.RemoveClick(this)"><span style="color:blue">删除</span></a>  ';
                                  var c = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.Publish.PublishClick(this)"><span style="color:blue">发布</span></a> ';
                              } else {
                                  var b = ' <span style="color:#5b5b5b">删除</span>';
                                  var c = ' <span style="color:#5b5b5b">发布</span>';

                              }
                              var d = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.Publish.PublishUserInfoUser(this)"><span style="color:blue">查看发布详情</span></a>  ';
                              return a+b+c+d;
                          }
                      }
                     
                    ]  
        ],
        queryParams: { TWhere: ' ' },
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
AdM.Publish.SerachClick = function () {
    var Where = "";

    if ($("#seachAreaContext").val() != "" ) {
        Where += '  AND  AreaContext  like \'%' + $("#seachAreaContext").val() + '%\'';
    }
    if ($("#bDate").datebox('getValue') != "" && $("#bDateTime").timespinner('getValue') != "") {
        Where += '  AND  PublishDate  >= \'' + $("#bDate").datebox('getValue') + ' ' + $("#bDateTime").timespinner('getValue') + '\'';

    } else if ($("#bDate").datebox('getValue') != "") {
        Where += '  AND  PublishDate  >= \'' + $("#bDate").datebox('getValue') + ' 00:00:00\'';
    }
    if ($("#eDate").datebox('getValue') != "" && $("#eDateTime").timespinner('getValue') != "") {
        Where += '  AND  PublishDate  <= \'' + $("#eDate").datebox('getValue') + ' ' + $("#eDateTime").timespinner('getValue') + '\'';

    } else if ($("#eDate").datebox('getValue') != "") {
        Where += '  AND  PublishDate  <= \'' + $("#eDate").datebox('getValue') + ' 00:00:00\'';
    }

    if ($("#seachState").combobox("getValue") != "") {
        Where += '  AND  State  = ' + $("#seachState").combobox("getValue") + ' ';
    }

    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};



//打开修改
AdM.Publish.OpenformEdit = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.SetData('wAdd', data);

    $('#btnOk').unbind('click').bind('click', function () { AdM.Publish.EditContextClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    AdM.Publish.CleanSelect();
    AdM.Publish.LoadDataGridList(data.AP_ID);
    
    $('#PublishDateOld').val(data.PublishDate);
    
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '编辑',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 20,
        onOpen: function () { IotM.EditDisabled('wAdd'); IotM.checkisValid('wAdd'); }
    });
};

AdM.Publish.EditContextClick = function () {
    
    if (IotM.checkisValid('formAdd')) {

        if ($("#CNStateShow").attr("checked") == "checked") { $('#CNState').val('1'); } //编辑
        if ($('#PublishDateOld').val()!=$('#PublishDate').val() ) { $('#CNState').val('2'); } //编辑_重新发布,当修改了发布时间并且原状态为已发布状态时;
        $("#CNContext").val($("#CNAC_ID").combobox("getText"));
        var data = IotM.GetData('formAdd');
        var rows = $('#dataGrid_list').datagrid('getRows');
        if (rows && rows.length <= 0) { $.messager.alert('提示', '没有选中用户！', 'info'); return; }
        var strNo = "";

        for (var i = 0; i < rows.length; i++) {

            if (i == rows.length - 1) {
                strNo += "'" + rows[i].UserID + "'";
            }
            else {
                strNo += "'" + rows[i].UserID + "'" + ",";
            }
        }
        data.strNo = strNo;
        data.Scope = AdM.Publish.Scope;
        data.strArea = AdM.Publish.strArea;

        $.post("../Handler/AdPublishHandler.ashx?AType=EDIT",
                    data,
                    function (data, textStatus) {
                        if (textStatus == 'success') {
                            if (data.Result) {
                                if (data.TxtMessage.indexOf("APP接口") >= 0) {
                                    $('#dataGrid').datagrid('reload');
                                    $.messager.alert('提示', '信息修改成功,但服务端接口调用出错！人员添加失败！(' + data.TxtMessage + ') ', 'info');
                                    $('#btnCancel').click();
                                }else{
                                    $('#btnCancel').click();
                                    $('#dataGrid').datagrid('reload');
                                    $.messager.alert('提示', '操作成功！', 'info');
                                }
                                  
                            }
                            else
                                $.messager.alert('警告', data.TxtMessage, 'warn');

                        }
                    }, "json");
        }
    };





AdM.Publish.PublishAddUser = function () {
    $('#btnOk').unbind('click').bind('click', function () { AdM.Publish.AddClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    IotM.FormSetDefault('formAdd');

    $("#userListTR").show(); $('#btnOk').show(); $('#btnCancel').show();
    AdM.Publish.CleanSelect();
    
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '发布广告',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 20,
        onOpen: function () {
            IotM.checkisValid('formAdd');

        }
    });
};


//提交数拓
AdM.Publish.AddClick = function () {
    if (IotM.checkisValid('formAdd')) {

        if ($("#CNStateShow").attr("checked") == "checked") { $('#CNState').val('1'); } else { $('#CNState').val('0'); }
        $("#CNContext").val($("#CNAC_ID").combobox("getText"));
        var data = IotM.GetData('formAdd');
        var rows = $('#dataGrid_list').datagrid('getRows');
        if (rows && rows.length <= 0) { $.messager.alert('提示', '没有选中用户！', 'info'); return; }
        var strNo = "";

        for (var i = 0; i < rows.length; i++) {

            if (i == rows.length - 1) {
                strNo += "'" + rows[i].UserID + "'";
            }
            else {
                strNo += "'" + rows[i].UserID + "'" + ",";
            }
        }
        data.strNo = strNo;
        data.Scope = AdM.Publish.Scope;
        data.strArea = AdM.Publish.strArea;
        
        $.post("../Handler/AdPublishHandler.ashx?AType=Add",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {

                              if (data.TxtMessage.indexOf("APP接口") >= 0) {
                                  $('#dataGrid').datagrid('reload');
                                  $.messager.alert('提示', '信息写入成功,但服务端接口调用出错！人员添加失败！(' + data.TxtMessage + ') ', 'info');
                                  $('#btnCancel').click();
                              } else {
                                  $('#btnCancel').click();
                                  $('#dataGrid').datagrid('reload');
                                  $.messager.alert('提示', '操作成功！', 'info');
                              }
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};

//删除数据
AdM.Publish.RemoveClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    $.messager.confirm('确认', '是否真的删除？', function (r) {
        if (r == true) {
            $.post("../Handler/AdPublishHandler.ashx?AType=DELETINFO",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('deleteRow', parseInt(index));
                              //$.messager.alert('提示', data.TxtMessage, 'info', function () {});
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
        }
    }
   );
};

//发布数据
AdM.Publish.PublishClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    //判断当用户数据为0时不能发布;
    if (data.UserCount == "0") { alert("当前用户总数为0，不可以发布！"); return;}

    $.messager.confirm('确认', '确认是否发布信息？', function (r) {
        if (r == true) {
            $.post("../Handler/AdPublishHandler.ashx?AType=UPDATESTATE",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              if (data.TxtMessage.indexOf("APP接口") >= 0) {
                                  $('#dataGrid').datagrid('reload');
                                  $.messager.alert('提示',  data.TxtMessage , 'info');
                                  $('#btnCancel').click();
                              } else {
                                  $('#btnCancel').click();
                                  $('#dataGrid').datagrid('reload');
                                  $.messager.alert('提示', '操作成功！', 'info');
                              }

                             
                              //$.messager.alert('提示', data.TxtMessage, 'info', function () {});
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
        }
    }
   );
};


//清除选择
AdM.Publish.CleanSelect = function () {
    $('#dataGrid_list').datagrid('loadData', { total: 0, rows: [] });
    $("#CNUserCount").val($('#dataGrid_list').datagrid('getRows').length);
}


//加载用户列表(二级)
AdM.Publish.LoadDataGridList = function (AP_ID) {

    var Where;
    var url
    if (AP_ID != "") {
        url = "../Handler/AdPublishHandler.ashx?AType=QUERYVIEWPUINFO";
        Where = " and AP_ID = " + AP_ID;
    }
    $('#dataGrid_list').datagrid({
        url: url,
        height: IotM.MainGridHeight * 0.4,
        width: IotM.MainGridWidth * 0.5,
        fitColumns: true,
        pagination: true,        
        rownumbers: true,
        sort: "MeterNo",
        order: "ASC",

        columns: [
                    [
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Adress', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true }
                    ]
        ],
        queryParams: { TWhere: Where },
        onLoadSuccess: function (data) {
            $("#CNUserCount").val(data.total);
        },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_list').datagrid('getPager');
            $(p).pagination({
                //pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
                //pageSize: IotM.windowPageSize,
                pageList: IotM.pageList, //可以设置每页记录条数的列表 
                beforePageText: '第', //页数文本框前显示的汉字 
                afterPageText: '页    共 {pages} 页',
                displayMsg: ' {from} - {to} 共 {total}',
                onBeforeRefresh: function () {
                    $(this).pagination('loading');
                    $(this).pagination('loaded');
                }
            });

        }
    });

}


//所有用户
AdM.Publish.SelAllUser = function () {

    var url = "../Handler/ADUserHandler.ashx?AType=QueryView";
    $('#dataGrid_list').datagrid({
        url: url,
        queryParams: { TWhere: '' },
        onLoadSuccess: function (data) {
            $("#CNUserCount").val(data.total);
        }
    });
    AdM.Publish.Scope = "所有用户";

}


//选择小区
AdM.Publish.SelCommunity = function () {


    $('#btnOk_Community').unbind('click').bind('click', function () { AdM.Publish.SelCommunityClick(); });
    $('#btnCancel_Community').unbind('click').bind('click', function () { $('#GetCommunityDiv').window('close'); });

    AdM.Publish.LoadDataGridCommunity();

    $('#GetCommunityDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '选择小区',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 150,
        top: 150,
    });
}

//选择小区列表

AdM.Publish.LoadDataGridCommunity = function () {

    var url = "../Handler/ADUserHandler.ashx?AType=QueryViewSC";
    $('#dataGrid_Community').datagrid({
        title: '',
        toolbar: '',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        sort: "ID",
        order: "ASC",
        columns: [
                    [
                     { field: 'ck', checkbox: true },
                     { field: 'Street', title: '所属街道', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'Community', title: '小区名称', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Num', title: '总户数', rowspan: 2, width: IotM.MainGridWidth * 0.5, align: 'center', sortable: true }

                    ]

        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_Community').datagrid('getPager');
            $(p).pagination({
                // pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
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

}
//选择小区用户
AdM.Publish.SelCommunityClick = function () {

    var rows = $('#dataGrid_Community').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }

    var communityStr = ""; var communityNameStr = "";
    for (var i = 0; i < rows.length; i++) { communityStr += rows[i].ID + ","; communityNameStr += "'"+rows[i].Community + "'," }
    communityStr = communityStr.substr(0, communityStr.length - 1);
    communityNameStr = communityNameStr.substr(0, communityNameStr.length - 1);

    AdM.Publish.strArea = communityStr;

    AdM.Publish.Scope = communityNameStr;


    var url = "../Handler/ADUserHandler.ashx?AType=QueryView";
    $('#dataGrid_list').datagrid({
        url: url,
        queryParams: { TWhere: ' AND Community in ( ' + communityNameStr + ')' },
        onLoadSuccess: function (data) { $("#CNUserCount").val(data.total); }
    });



    $('#GetCommunityDiv').window('close');



}



//选择用户
AdM.Publish.SelUser = function () {

    $('#btnOk_deleteUser').unbind('click').bind('click', function () { AdM.Publish.BatchChoseClick(); });
    $('#btnCancel_deleteUser').unbind('click').bind('click', function () { $('#choseUserDiv').window('close'); });

    AdM.Publish.LoadDataGridChoseUser();

    $('#choseUserDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '选择用户',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 150,
        top: 50,
    });
};


//加载选择用户
AdM.Publish.LoadDataGridChoseUser = function () {
    var url = "../Handler/ADUserHandler.ashx?AType=QueryView";
    $('#dataGrid_choseUser').datagrid({
        title: '',
        toolbar: '#tb_choseUser',
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
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Adress', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true }

                    ]

        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_choseUser').datagrid('getPager');
            param.rows = IotM.windowPageSize;
            $(p).pagination({
                //pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
                //pageSize: IotM.windowPageSize,
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

//确认选择用户
AdM.Publish.BatchChoseClick = function () {

    var rows = $('#dataGrid_choseUser').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }

    $('#dataGrid_list').datagrid("loadData", rows);
    $('#choseUserDiv').window('close');

    AdM.Publish.Scope = "选择用户";

    $("#CNUserCount").val($('#dataGrid_list').datagrid('getRows').length);
}


//打开发布用户窗口
AdM.Publish.PublishUserInfoUser = function (obj) {
    //alert(obj);
    AdM.Publish.LoadDataGridPublish(obj);

    $('#AdPUserInfo').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.8,
        title: '查看发布详情',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 150,
        top: 50,
    });
};
//加载发布用户
AdM.Publish.LoadDataGridPublish = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    var url = "../Handler/AdPublishHandler.ashx?AType=QueryViewPUINFO";
    $('#dataGrid_AdPUserInfo').datagrid({
        title: '',
        toolbar: '#dataGrid_AdPUserInfo',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        sort: " UserID",
        order: "ASC",
        columns: [
                    [
                     //{ field: 'AP_ID', title: '发布ID', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     { field: 'Adress', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.5, align: 'center', sortable: true },
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {

                             if (value == "0") { return "等待调度"; }
                             else if (value == "1") { return "已发布"; }
                             else if (value == "2") { return "需重新发布"; }
                             else { return "未知"; }

                         }
                     },
                     { field: 'FinishedDate', title: '完成时间', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true }

                    ]

        ],
        queryParams: { TWhere: ' and AP_ID=' + data.AP_ID },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_AdPUserInfo').datagrid('getPager');
            param.rows = IotM.windowPageSize;
            $(p).pagination({
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






AdM.Publish.PublishAddUser2 = function () {

    $('#btnOk_User').unbind('click').bind('click', function () { AdM.Publish.PublishAddClick(); });
    $('#btnCancel_User').unbind('click').bind('click', function () { $('#UserDiv').window('close'); });

    AdM.Publish.LoadDataGridUserInfo("AND 1=1 ");

    $('#UserDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '发布广告',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 50

    });
};

//确认添加
AdM.Publish.PublishAddClick = function () {

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



//加载用户列表视图
AdM.Publish.LoadDataGridUserInfo = function (whereStr) {
    var url = "../Handler/ADUserHandler.ashx?AType=QueryViewUSER";
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
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.5, align: 'center', sortable: true }

                    ]

        ],
        queryParams: { TWhere: whereStr },
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



//数据初始化,广告主题-------------------------------------------------------------------------

//提取信息
AdM.Publish.Context = [];
AdM.Publish.InitAdContext = function () {
    $.ajax({
        url: "../Handler/AdMHandler.ashx?AType=QUERYVIEWLIST",
        async: false,
        type: "POST",
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data.TxtMessage + ')');
                if (data != null && data.total > 0) {
                    AdM.Publish.Context = data.rows;
                } else {
                    AdM.Publish.Context = [{ AC_ID: '', Context: '' }];
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

AdM.Publish.LoadADContextComboBox = function (id, withAll, required) {
    AdM.Publish.InitAdContext();
    var data = IotM.ObjectClone(AdM.Publish.Context);
    if (withAll) {
        data.push({ AC_ID: '', Context: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].ID,
        valueField: 'AC_ID',
        textField: 'Context',
        required: required,
        editable: false
    });
};
