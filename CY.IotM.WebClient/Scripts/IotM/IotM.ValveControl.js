IotM.namespace('IotM.ValveControl');
//加载列表控件
IotM.ValveControl.LoadDataGrid = function () {
    var url = "../Handler/ValveControlHandler.ashx?AType=Query";
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
                       
                        
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.3, align: 'center', sortable: true },

                     { field: 'RegisterDate', title: '申请日期', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             //0 申请 1 撤销 2 完成 3 失败
                             if (value == "0") { return "申请"; }
                             else if (value == "1") { return "撤销"; }
                             else if (value == "2") { return "完成"; }
                             else if (value == "3") { return "失败"; }
                             else { return "未知"; }
                         }


                     },
                     {
                         field: 'ControlType', title: '控制类型', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                         
                             if (value == "0") { return "关阀"; }
                             else if (value == "1") { return "开阀"; }
                             else { return "未知"; }
                         }
                     
                     
                     },
                     { field: 'Oper', title: '操作员', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Reason', title: '原因', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'FinishedDate', title: '完成时间', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     { field: 'Context', title: '备注', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     {
                         field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center',
                         formatter: function (value, rec, index) {
                             var e;
                             if (rec.State == '0') {
                                 e = '<a href="#" mce_href="#"  onclick="IotM.ValveControl.Undo(this)"><span style="color:blue">撤销</span></a> ';
                             } else if (rec.State == '2' || rec.State == '3') {
                                 e = '<a href="#" mce_href="#"  onclick="IotM.ValveControl.GetLog(this)"><span style="color:blue">日志</span></a> ';
                             }
                             return e;
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



//阀门控制列表
IotM.ValveControl.LoadDataGridFaMen = function () {
    var url = "../Handler/UserHandler.ashx?AType=QueryView";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
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
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                   

                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.5, align: 'center', sortable: true },
                     {
                        field: 'ValveState', title: '阀门状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                        formatter: function (value, rec, index) {

                            if (value == "1") { return "阀关"; }
                            else if (value == "0") { return "阀开"; }
                            else { return "未知"; }
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






IotM.ValveControl.SerachClick = function () {


    var Where = "";
    if ($("#select_UserID").val() != "") {
        Where += '  AND  UserID  like \'%' + $("#select_UserID").val() + '%\'';
    }
    if ($("#select_UserName").val() != "") {
        Where += '  AND  UserName  like \'%' + $("#select_UserName").val() + '%\'';
    }
    if ($("#select_Adress").val() != "") {
        Where += '  AND  Address  like \'%' + $("#select_Adress").val() + '%\'';
    }

    if (document.getElementById("select_ControlType")) {
        if ($("#select_ControlType").combobox("getValue") != "")
            Where += '  AND  ControlType  =  \'' + $("#select_ControlType").combobox("getValue") + '\'';
    }
    if (document.getElementById("select_State")) {
        if ($("#select_State").combobox("getValue") != "")
            Where += '  AND  State  =  \'' + $("#select_State").combobox("getValue") + '\'';
    }

    
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};




//开阀操作
IotM.ValveControl.KaiFaFormOpen = function () {



    var rows = $('#dataGrid').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }

    $('#btnOk').unbind('click').bind('click', function () { IotM.ValveControl.KaiFaClick(rows); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd_KaiFa').window('close'); });



    $("#UserNum").val(rows.length);
    $("#FaMenContext").html("开阀说明:");

    $('#wAdd_KaiFa').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.5,
        title: '开阀操作申请',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100

    });


    IotM.ValveControl.LoadDataGridList();

    $('#dataGrid_list').datagrid("loadData", rows);


}


//加载选择用户列表
IotM.ValveControl.LoadDataGridList = function () {



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
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1,  align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
                    ]

        ],
        onLoadSuccess: function () {  },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_list').datagrid('getPager');
            $(p).pagination({
                // pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
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


//确定开阀
IotM.ValveControl.KaiFaClick = function (rows) {


    var strNo = "";
    var data = {};
    for (var i = 0; i < rows.length; i++) {
      
        if (i == rows.length - 1) {
            strNo += rows[i].MeterNo;
        }
        else {
            strNo += rows[i].MeterNo + ",";
        }
    }

    data.strNo = strNo;
    data.Reason = $("#KaiFaContext").val();


    $.post("../Handler/ValveControlHandler.ashx?AType=KaiFa",
              data,
               function (data, textStatus) {
                   if (textStatus == 'success') {
                       if (data.Result) {

                           $.messager.alert('提示', '操作成功！', 'info', function () {
                               $('#wAdd_KaiFa').window('close');
                           });
                       }
                       else
                           $.messager.alert('警告', data.TxtMessage, 'warn');

                   }
               }, "json");

}




//确定关阀
IotM.ValveControl.GuanFaClick = function (rows) {

    var strNo = "";
    var data = {};
    for (var i = 0; i < rows.length; i++) {

        if (i == rows.length - 1) {
            strNo += rows[i].MeterNo;
        }
        else {
            strNo += rows[i].MeterNo + ",";
        }
    }

    data.strNo = strNo;
    data.Reason = $("#KaiFaContext").val();


    $.post("../Handler/ValveControlHandler.ashx?AType=GuanFa",
              data,
               function (data, textStatus) {
                   if (textStatus == 'success') {
                       if (data.Result) {

                           $.messager.alert('提示', '操作成功！', 'info', function () {
                               $('#wAdd_KaiFa').window('close');
                           });
                       }
                       else
                           $.messager.alert('警告', data.TxtMessage, 'warn');

                   }
               }, "json");


}





//关阀操作
IotM.ValveControl.GuanFaFormOpen = function () {


    var rows = $('#dataGrid').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }

    $('#btnOk').unbind('click').bind('click', function () { IotM.ValveControl.GuanFaClick(rows); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd_KaiFa').window('close'); });

    $("#UserNum").val(rows.length);
    $("#FaMenContext").html("关阀原因:");

    $('#wAdd_KaiFa').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.5,
        title: '关阀操作申请',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100

    });


    IotM.ValveControl.LoadDataGridList();

    $('#dataGrid_list').datagrid("loadData", rows);


}


IotM.ValveControl.GetLog = function (obj) {
    $('#dlg').dialog('open');

}

//撤销开关阀门任务
IotM.ValveControl.Undo = function (obj) {

    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    $.messager.confirm('确认', '是否真的撤销?', function (r) {
        if (r == true) {
            $.post("../Handler/ValveControlHandler.ashx?AType=UnDo",
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

}

