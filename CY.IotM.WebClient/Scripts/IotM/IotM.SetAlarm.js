IotM.namespace('IotM.SetAlarm');

IotM.SetAlarm.Scope = "";
//加载列表控件
IotM.SetAlarm.LoadDataGrid = function () {
    var url = "../Handler/SetAlarmHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        pageSize: 50,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "RegisterDate",
        order: "DASC",
        columns: [
                    [
                     //{ field: 'TaskID', title: '任务号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'RegisterDate', title: '申请时间', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Scope', title: '区域', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Total', title: '总户数', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     {
                          field: 'param', title: '设置参数', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false,
                          formatter: function (value, rec, index) {
                              var e = '<a href="#" mce_href="#"  onclick="IotM.SetAlarm.OpenformParam(this)"><span style="color:blue">参数</span></a> ';
                              return e;
                          }
                      },
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                        formatter: function (value, rec, index) {
                            if (value == "0") { return "申请"; }
                            else if (value == "1") { return "撤销"; }
                            else if (value == "2") { return "完成"; }
                            else if (value == "3") { return "失败"; }
                            else { return "未知"; }
                        }

                     },
                     { field: 'Context', title: '备注', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Oper', title: '操作员', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     {
                        field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center',
                        formatter: function (value, rec, index) {
                            var e = "";
                            if (rec.State == "0") {
                                e = '<a href="#" mce_href="#"  onclick="IotM.SetAlarm.CheXiao(this)"><span style="color:blue">撤销</span></a> ';
                            }
                            var f = '<a href="#" mce_href="#"  onclick="IotM.SetAlarm.GetUserDetail(this)"><span style="color:blue">查看用户明细</span></a> ';
                            return e+ f;
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
IotM.SetAlarm.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.SetAlarm.AddClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    IotM.FormSetDefault('formAdd');

    $("#userListTR").show(); $('#btnOk').show(); $('#btnCancel').show();

   

    IotM.SetAlarm.CleanSelect();

    

    for (var i = 0; i < 9; i++) { $("#switch" + i).attr("checked", false)}


    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '设置报警参数',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100,
        onOpen: function () {
            IotM.checkisValid('formAdd');
          
        }
    });
};


//查看报警参数
IotM.SetAlarm.OpenformParam = function (obj) {
    $('#btnOk').unbind('click').bind('click', function () { IotM.SetAlarm.EditClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
  
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.SetData('formAdd', data);
    $('#CNPar4').numberbox('setValue',parseFloat(data.Par4)/100);
    $("#userListTR").hide();
    $('#btnOk').hide(); $('#btnCancel').hide();


    if (data.SwitchTag.length>0) {

        for (var i = 0; i < data.SwitchTag.length; i++) {

            if (data.SwitchTag[i] == "0") {

                $("#switch" + i).attr("checked", false)

            } else {
                $("#switch" + i).attr("checked", "checked")

            }
        }

    }
  

    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '查看报警参数',
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



IotM.SetAlarm.SerachClick = function (value, name) {
    var Where = "";
    if ($("#select_Street").val() != "") {
        Where += '  AND  Scope  like \'%' + $("#select_Street").val() + '%\'';
    }
    if (document.getElementById("select_State")) {
        if ($("#select_State").combobox("getValue") != "")
            Where += '  AND  State  =  \'' + $("#select_State").combobox("getValue") + '\'';
    }

    $('#dataGrid').datagrid('options').queryParams = {
        TWhere: Where,
        Date1: $('#select_RegisterDate').datebox('getValue')
    };
    $('#dataGrid').datagrid('load');
};



//加载选择用户列表
IotM.SetAlarm.LoadDataGridList = function () {


    $('#dataGrid_list').datagrid({

        height: IotM.MainGridHeight * 0.3,
        width: IotM.MainGridWidth * 0.35,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        sort: "MeterNo",
        order: "ASC",

        columns: [
                    [
                    
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true }
                    ]

        ],
        onLoadSuccess: function () {  },
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
IotM.SetAlarm.SelAllUser = function () {

    var url = "../Handler/UserHandler.ashx?AType=QueryView";
    $('#dataGrid_list').datagrid({
        url: url,
        queryParams: { TWhere: '' },
        onLoadSuccess: function (data) {
            $("#CNTotal").val(data.total);
        }
    });
    IotM.SetAlarm.Scope = "所有用户";

}


//加载选择用户
IotM.SetAlarm.LoadDataGridDelete = function () {
    var url = "../Handler/UserHandler.ashx?AType=QueryView";
    $('#dataGrid_deleteUser').datagrid({
        title: '',
        toolbar: '#tb_deleteUser',
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
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {

                             if (value == "0") { return "等待安装"; }
                             else if (value == "1" || value == "2") { return "等待点火"; }
                             else if (value == "3") { return "正常使用"; }
                             else { return "未知"; }

                         }
                     },

                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.5, align: 'center', sortable: true }

                    ]

        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_deleteUser').datagrid('getPager');
            param.rows = IotM.windowPageSize;
            $(p).pagination({
                pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
                pageSize: IotM.windowPageSize,
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




//选择用户
IotM.SetAlarm.SelUser = function () {

    $('#btnOk_deleteUser').unbind('click').bind('click', function () { IotM.SetAlarm.BatchDeleteClick(); });
    $('#btnCancel_deleteUser').unbind('click').bind('click', function () { $('#deleteUserDiv').window('close'); });

    IotM.SetAlarm.LoadDataGridDelete();

    $('#deleteUserDiv').window({
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
        top: 150,
    });
};



//确认选择用户
IotM.SetAlarm.BatchDeleteClick = function () {

    var rows = $('#dataGrid_deleteUser').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }

    $('#dataGrid_list').datagrid("loadData", rows);
    $('#deleteUserDiv').window('close');

    IotM.SetAlarm.Scope = "选择用户";
 
    $("#CNTotal").val($('#dataGrid_list').datagrid('getRows').length);
}



IotM.SetAlarm.DeleteSerachClick = function () {


    var Where = "";
    if ($("#select_StreetDelete").val() != "") {
        Where += '  AND  UserID  like \'%' + $("#select_StreetDelete").val() + '%\'';
    }
    if ($("#select_Community").val() != "") {
        Where += '  AND  MeterNo  like \'%' + $("#select_Community").val() + '%\'';
    }
    if ($("#select_AdressDel").val() != "") {
        Where += '  AND  Address  like \'%' + $("#select_AdressDel").val() + '%\'';
    }


    $('#dataGrid_deleteUser').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid_deleteUser').datagrid('load');

}



//清除选择
IotM.SetAlarm.CleanSelect = function () {


    $('#dataGrid_list').datagrid('loadData', { total: 0, rows: [] });

    $("#CNTotal").val($('#dataGrid_list').datagrid('getRows').length);
    IotM.SetAlarm.Scope = "";
}



//设置参数
IotM.SetAlarm.AddClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');

        data.SwitchTag = "";

        for (var i = 0; i < 9; i++) {

            if ($("#switch" + i).attr("checked")) {
                data.SwitchTag += "1";
            } else {
                data.SwitchTag += "0";
            }
        }

        var rows = $('#dataGrid_list').datagrid('getRows');

        if (rows && rows.length <= 0) { $.messager.alert('提示', '没有选中用户！', 'info'); return; }



        var strNo = "";
      
        for (var i = 0; i < rows.length; i++) {
           
            if (i == rows.length - 1) {
                strNo += rows[i].MeterNo;
            }
            else {
                strNo += rows[i].MeterNo + ",";
            }
        }
        data.strNo = strNo;

        data.Scope = IotM.SetAlarm.Scope;
       
        data.strArea = IotM.SetAlarm.strArea;

        $.post("../Handler/SetAlarmHandler.ashx?AType=Add",
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



//获取用户明细
IotM.SetAlarm.GetUserDetail = function (obj) {


    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');


    IotM.SetAlarm.LoadDataGridUser(data.ID);

    $('#GetUserDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '查看用户明细',
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


//加载用户详细视图
IotM.SetAlarm.LoadDataGridUser = function (id) {

    var url = "../Handler/SetAlarmHandler.ashx?AType=QueryUser";
    $('#dataGrid_getUser').datagrid({
        title: '',
        toolbar: '',
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

                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "0") { return "申请"; }
                             else if (value == "1") { return "撤销"; }
                             else if (value == "2") { return "完成"; }
                             else if (value == "3") { return "失败"; }
                             else { return "未知"; }
                         }
                     },

                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.5, align: 'center', sortable: true },
                     { field: 'FinishedDate', title: '完成时间', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true }

                    ]

        ],
        queryParams: { TWhere: '', ID: id },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_getUser').datagrid('getPager');
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





//选择小区
IotM.SetAlarm.SelCommunity = function () {


    $('#btnOk_Community').unbind('click').bind('click', function () { IotM.SetAlarm.SelCommunityClick(); });
    $('#btnCancel_Community').unbind('click').bind('click', function () { $('#GetCommunityDiv').window('close'); });

    IotM.SetAlarm.LoadDataGridCommunity();

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


IotM.SetAlarm.LoadDataGridCommunity = function () {

    var url = "../Handler/CommunityHandler.ashx?AType=QueryView";
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
                     { field: 'StreetName', title: '所属街道', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'CommunityName', title: '小区名称', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
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

IotM.SetAlarm.strArea = "";

IotM.SetAlarm.SelCommunityClick = function () {

    var rows = $('#dataGrid_Community').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }

    var communityStr = ""; var communityNameStr = "";
    for (var i = 0; i < rows.length; i++) { communityStr += rows[i].ID + ","; communityNameStr += rows[i].CommunityName + "," }
    communityStr= communityStr.substr(0, communityStr.length - 1);
    communityNameStr = communityNameStr.substr(0, communityNameStr.length - 1);

    IotM.SetAlarm.strArea = communityStr;

    IotM.SetAlarm.Scope = communityNameStr;


    var url = "../Handler/UserHandler.ashx?AType=QueryView";
    $('#dataGrid_list').datagrid({
        url: url,
        queryParams: { TWhere: ' AND Community in ( ' + communityStr +')' },
        onLoadSuccess: function (data) { $("#CNTotal").val(data.total); }
    });



    $('#GetCommunityDiv').window('close');



}



IotM.SetAlarm.CheXiao = function (obj) {


    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    $.messager.confirm('确认', '是否真的撤销?', function (r) {
        if (r == true) {
            $.post("../Handler/SetAlarmHandler.ashx?AType=UnDo",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {

                              $('#dataGrid').datagrid('reload');
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


}