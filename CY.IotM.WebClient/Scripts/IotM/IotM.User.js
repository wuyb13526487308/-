IotM.namespace('IotM.User');
//加载用户表具视图
IotM.User.LoadDataGridView = function () {
    var url = "../Handler/UserHandler.ashx?AType=QueryView";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        //fitColumns: true,
        pageSize:50,
        pagination: true,
        //rownumbers: true,
        singleSelect: true,
        sort: "UserID",
        order: "ASC",
        columns: [
                    [                     
                     { field: 'UserID', title: '户号', rowspan: 1, width: 100, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 1, width: 100, align: 'center', sortable: true },
                     { field: 'UserName1', title: '用户类型', rowspan: 1, width: 100, align: 'center', sortable: true },
                     { field: 'UserName1', title: '联系人电话', rowspan: 1, width: 100, align: 'center', sortable: true },
                     { field: 'UserName1', title: '身份证号', rowspan: 1, width: 120, align: 'center', sortable: true },
                     { field: 'UserName1', title: '安装日期', rowspan: 1, width: 120, align: 'center', sortable: true },
                     { field: 'State', title: '状态', rowspan: 1, width: 80, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {

                             if (value == "0") { return "等待安装"; }
                             else if (value == "1" || value == "2") { return "等待点火"; }
                             else if (value == "3") { return "正常使用"; }
                             else { return "未知"; }

                         }
                     },
                     { field: 'Address', title: '地址', rowspan: 1, width: 150, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 1, width: 120, align: 'center', sortable: true },
                     { field: 'UserName', title: '进气方向', rowspan: 1, width: 90, align: 'center', sortable: true },
                     { field: 'MeterType', title: '表类型', rowspan: 1, width: 100, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {

                             if (value == "00") { return "气量表"; }
                             else if (value == "01") { return "金额表"; }
                             else { return "未知"; }

                         }
                     },
                     { field: 'ValveState', title: '阀门状态', rowspan: 1, width: 80, align: 'center', sortable: true,
                             formatter: function (value, rec, index) {
                                 if (value == "0") { return "阀开"; }
                                 else if (value == "1") { return "阀关"; }
                                 else { return "未知"; }

                             }
                     },
                     { field: 'TotalAmount', title: '总用量', rowspan: 1, width: 120, align: 'center', sortable: true },
                     { field: 'TotalTopUp', title: '总充值金额', rowspan: 1, width: 100, align: 'center', sortable: true },
                     { field: 'RemainingAmount', title: '剩余金额', rowspan: 1, width: 100, align: 'center', formatter: function (value, rec, index) {
                             return IotM.NumberFormat(rec.RemainingAmount, 2, '--');
                         }
                     },
                     { field: 'UserName1', title: '备注', rowspan:1, width: 200, align: 'center', sortable: true },
                     { field: 'ReadDate', title: '最后抄表日期', rowspan: 1, width: 120, align: 'center', sortable: true },
                     {
                          field: 'opt', title: '操作', rowspan: 1, width: 100, align: 'center',
                          formatter: function (value, rec, index) {
                              var a = '<a href="#" mce_href="#" menucode="bjyh" onclick="IotM.User.OpenformAlarmParm(this)"><span style="color:blue">报警参数</span></a> ';
                              var e = '<a href="#" mce_href="#" menucode="bjyh" onclick="IotM.User.OpenformEdit(this)"><span style="color:blue">编辑</span></a> ';
                              var f = '<a href="#" mce_href="#" menucode="bjyh" onclick="IotM.User.RemoveClick(this)"><span style="color:blue">删除</span></a> ';
                              return a+e+f;
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
        },
        view: detailview,
        detailFormatter: function (index, row) {
            return '<div><table class="ddv"></table></div>';
        },
        onExpandRow: function (index, row) {
            var ddv = $(this).datagrid('getRowDetail', index).find('table.ddv');
            var rowData=[row];
            IotM.User.DDVLoad(ddv, rowData, index);
         
        }
    });
};


IotM.User.DDVLoad = function (ddv, row, index1) {

    ddv.datagrid({
        data: row,
        fitColumns: true,
        singleSelect: true,
        //rownumbers: true,
        showHeader: true,
        loadMsg: '加载中...',
        height: 'auto',
        columns: [[
                    {
                        field: 'IsUsed', title: '启用阶梯', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                        formatter: function (value, rec, index) {
                            if (value == "0") { return "未启用"; }
                            else if (value == "1") { return "启用"; }
                            else { return "未知"; }
                        }

                    },
                    {
                         field: 'SettlementType', title: '阶梯周期', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {

                             if (value == "00") { return "按月"; }
                             else if (value == "01") { return "按季度"; }
                             else if (value == "10") { return "按半年"; }
                             else if (value == "11") { return "按全年"; }
                             else { return "未知"; }
                         }

                     },

                     { field: 'Ladder', title: '阶梯数', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Price1', title: '价格1', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Gas1', title: '用量1', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Price2', title: '价格2', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Gas2', title: '用量2', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Price3', title: '价格3', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Gas3', title: '用量3', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Price4', title: '价格4', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Gas4', title: '用量4', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Price5', title: '价格5', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'UploadCycle', title: '上传周期', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'SettlementDay', title: '结算日', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
        
               
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



//加载用户视图
IotM.User.LoadDataGrid = function () {
    var url = "../Handler/UserHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        pageSize:50,
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
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {

                             if (value == "0") { return "等待安装"; }
                             else if (value == "1" || value == "2") { return "等待点火"; }
                             else if (value == "3") { return "正常使用"; }
                             else { return "未知"; }

                         }
                     },

                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.5, align: 'center', sortable: true },

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


IotM.User.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.User.AddClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    IotM.FormSetDefault('formAdd');
  
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '创建用户',
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
IotM.User.AddClick = function () {
    if (IotM.checkisValid('formAdd')) {

        $('#btnOk').linkbutton('disable');
        var data = IotM.GetData('formAdd');
        $.post("../Handler/UserHandler.ashx?AType=Add",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              if ($("#ContineCreate").attr("checked")) {
                                  IotM.FormSetDefault('formAdd');
                              } else {
                                  $('#btnCancel').click();
                              }
                              $('#dataGrid').datagrid('appendRow', eval('(' + data.TxtMessage + ')'));
                              $.messager.alert('提示', '操作成功！', 'info');
                          }
                          else {
                              $.messager.alert('警告', data.TxtMessage, 'warn');
                          }
                          $('#btnOk').linkbutton('enable');
                      }
                  }, "json");
    }
};

//查看报警参数
IotM.User.OpenformAlarmParm = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);

    var data = $('#dataGrid').datagrid('getSelected');
    //在此查询报警参数
    $.post("../Handler/UserHandler.ashx?AType=QueryAlarmParm",
     data,
      function (data, textStatus) {
          if (textStatus == 'success') {
              if (data.Result) {
                  //显示报警参数窗口
                  data = eval('(' + data.TxtMessage + ')');
                  IotM.SetData('formAlarmParm', data);
                  if (data.SwitchTag.length > 0) {

                      for (var i = 0; i < data.SwitchTag.length; i++) {

                          if (data.SwitchTag[i] == "0") {

                              $("#switch" + i).attr("checked", false)

                          } else {
                              $("#switch" + i).attr("checked", "checked")
                          }
                      }
                  }

                  $('#wAlarmParm').window({
                      resizable: false,
                      width: IotM.MainGridWidth * 0.5,
                      title: '报警参数',
                      modal: true,
                      shadow: true,
                      closed: false,
                      collapsible: false,
                      minimizable: false,
                      maximizable: false,
                      onOpen: function () {
                          IotM.EditDisabled('formAlarmParm');
                          IotM.checkisValid('formAlarmParm');
                      }
                  });
              }
              else
                  $.messager.alert('警告', data.TxtMessage, 'warn');

          }
      }, "json");


};

IotM.User.OpenformEdit = function (obj) {
    $('#btnOk').unbind('click').bind('click', function () { IotM.User.EditClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
  
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.SetData('formAdd', data);


    IotM.Initiate.LoadCommunityComboBox('CNCommunity', false, true, $('#CNStreet').combobox("getValue"));
    $('#CNCommunity').combobox("setValue", data.Community);

    //正常使用用户不能修改表号
    if (data.State == "3") {
        $("#CNMeterNo").attr("disabled", "disabled");
    } else {
        $("#CNMeterNo").attr("disabled", false);
    }


   
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '编辑用户',
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
IotM.User.EditClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');
       
        $.post("../Handler/UserHandler.ashx?AType=EditUserMeter",
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


IotM.User.RemoveClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r == true) {
            $.post("../Handler/UserHandler.ashx?AType=DeleteUserMeter",
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


IotM.User.SerachClick = function () {


    var Where="";
    if ($("#select_UserID").val() != "") {
        Where += '  AND  UserID  like \'%' + $("#select_UserID").val() + '%\'';
    }
    if ($("#select_UserName").val() != "") {
        Where += '  AND  UserName  like \'%' + $("#select_UserName").val() + '%\'';
    }
    if ($("#select_Adress").val() != "") {
        Where += '  AND  Address  like \'%' + $("#select_Adress").val() + '%\'';
    }
    if ($("#select_MeterNo").val()) {
        if ($("#select_MeterNo").val() != "")
        Where += '  AND  MeterNo  like \'%' + $("#select_MeterNo").val() + '%\'';
    }


    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};

IotM.User.AdressChange = function () {
    $("#CNAddress").val( $('#CNStreet').combobox("getText") + $('#CNCommunity').combobox("getText") + $("#CNDoor").val());
}




//加载批量删除用户视图
IotM.User.LoadDataGridDelete = function () {
    var url = "../Handler/UserHandler.ashx?AType=QueryView";
    $('#dataGrid_deleteUser').datagrid({
        title: '',
        toolbar: '#tb_deleteUser',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        pageSize:50,
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
                             else if (value == "1") { return "等待点火"; }
                             else if (value == "2") { return "正常使用"; }
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






IotM.User.OpenformDeleteUser = function () {

    $('#btnOk_deleteUser').unbind('click').bind('click', function () { IotM.User.BatchDeleteClick(); });
    $('#btnCancel_deleteUser').unbind('click').bind('click', function () { $('#deleteUserDiv').window('close'); });

    IotM.User.LoadDataGridDelete();

    $('#deleteUserDiv').window({
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
        top:100
       
    });
};



//确认批量删除
IotM.User.BatchDeleteClick = function () {

    var rows = $('#dataGrid_deleteUser').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }
    var strNo = "";
    var data = {};
    for (var i = 0; i < rows.length; i++) {
        var index = $('#dataGrid_deleteUser').datagrid('getRowIndex', rows[i]);
        if (i == rows.length - 1) {
            strNo += rows[i].UserID;
        }
        else {
            strNo += rows[i].UserID + ",";
        }
    }
    data.strNo = strNo;

    $.post("../Handler/UserHandler.ashx?AType=BatchDelete",
              data,
               function (data, textStatus) {
                   if (textStatus == 'success') {
                       if (data.Result) {

                           $.messager.alert('提示', '操作成功！', 'info', function () {
                               $('#dataGrid_deleteUser').datagrid('reload');
                               $('#dataGrid').datagrid('reload');
                           });
                       }
                       else
                           $.messager.alert('警告', data.TxtMessage, 'warn');

                   }
          }, "json");

      

}



IotM.User.DeleteSerachClick = function () {


    var Where = "";
    if ($("#select_Street").val() != "") {
        Where += '  AND  Street  like \'%' + $("#select_Street").val() + '%\'';
    }
    if ($("#select_Community").val() != "") {
        Where += '  AND  Community  like \'%' + $("#select_Community").val() + '%\'';
    }
    if ($("#select_AdressDel").val() != "") {
        Where += '  AND  Address  like \'%' + $("#select_AdressDel").val() + '%\'';
    }
 

    $('#dataGrid_deleteUser').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid_deleteUser').datagrid('load');

}



//批量创建用户
IotM.User.OpenformBatchAdd = function () {

    
    $('#btnOk_Batch').unbind('click').bind('click', function () { IotM.User.BatchStepClick(); });
    $('#btnCancel_Batch').unbind('click').bind('click', function () { $('#wAdd_Batch').window('close'); });

    $("#step1").show();
    $("#step2").hide();
  
    IotM.Initiate.LoadMeterTypeComboBox('CNMeterType', false, true);


    $("#CNMeterType").combobox({ width: 320 });

    IotM.Initiate.LoadUserDirectionComboBox('CNHuXiang', false, true);


    IotM.Initiate.LoadStreetComboBox('CNStreet_Batch', false, true);
    $('#CNStreet_Batch').combobox(
    {
        onSelect: function (rec) {
            IotM.Initiate.LoadCommunityComboBox('CNCommunity_Batch', false, true, rec.ID);
        }
    });

    IotM.Initiate.LoadCommunityComboBox('CNCommunity_Batch', false, true, $('#CNStreet_Batch').combobox("getValue"));




    $('#wAdd_Batch').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.6,
        title: '批量创建用户',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100
        
    });
}





//批量创建生成预览视图
IotM.User.BatchStepClick = function () {

    //楼层数
    var longceng = $("#CNLouCengNum").numberspinner('getValue');
    //开始楼层
    var startlongceng = $("#CNLouCengStart").numberspinner('getValue');

    if (startlongceng > longceng) {
        $.messager.alert('警告', "开始楼层不能大于楼层总数！", 'warn'); return;
    }

    IotM.User.LoadDataGridBatchAdd();

    $("#step1").hide();
    $("#step2").show();

 
    $('#btnOk_Batch_accept').unbind('click').bind('click', function () {
        if (endEditing()) {
            $('#dataGrid_Preview').datagrid('acceptChanges');
        }
    });
    $('#btnCancel_Batch_complete').unbind('click').bind('click', function () { $('#wAdd_Batch').window('close'); });
    $('#btnOk_Batch_complete').unbind('click').bind('click', function () { IotM.User.BatchCompleteClick(); });
    

}


IotM.User.BatchUserAdd = function () {

    //楼层数
    var longceng = $("#CNLouCengNum").numberspinner('getValue');
    //开始楼层
    var startlongceng = $("#CNLouCengStart").numberspinner('getValue');

 
    //道路
    var street = $("#CNStreet_Batch").combobox("getText");
    //小区
    var community = $("#CNCommunity_Batch").combobox("getText");
    //楼号
    var louNo = $("#CNLouNo").val() + "号楼";
    //单元
    var unitNo = $("#CNUnitNo").val() + "单元";
    //户向
    var huxiang = $("#CNHuXiang").combobox("getText") + "户";

    var adress = street + community + louNo + unitNo;


    for (startlongceng; startlongceng <= longceng; startlongceng++) {
        var data = {};
        data.Address = adress + startlongceng + "楼" + huxiang;
        data.UserID = "";
        data.UserName = "";
        data.MeterNo = "";

        $('#dataGrid_Preview').datagrid('appendRow', data);
    }
}




//加载批量用户视图
IotM.User.LoadDataGridBatchAdd = function () {
  
    $('#dataGrid_Preview').datagrid({
        url: "../Scripts/IotM/blankdata.js",
        method: 'GET',
        height: IotM.MainGridHeight * 0.5,
        width: IotM.MainGridWidth * 0.55,
        singleSelect: true,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        sort: "Address",
        order: "ASC",
        onClickCell:IotM.User.onClickCell,
        columns: [
                    [

                     //{ field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.15, editor:'text', align: 'center', sortable: true },
                     {
                         field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.2,
                         editor: { type: 'validatebox', options: { validType: 'regNumLength[14,14]', required: true } },
                         align: 'center', sortable: true
                     },

                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.5, editor: 'text', align: 'center', sortable: true }

                    ]

        ],
        onLoadSuccess: function () { IotM.User.BatchUserAdd()},
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_Preview').datagrid('getPager');
            $(p).pagination({
                //pageSize: parseInt($('.pagination-page-list').first().val()), //每页显示的记录条数，默认为10 
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


var editIndex = undefined;
var endEditing = function () {
    if (editIndex == undefined) { return true }
    if ($('#dataGrid_Preview').datagrid('validateRow', editIndex)) {

        $('#dataGrid_Preview').datagrid('endEdit', editIndex);
        editIndex = undefined;
        return true;
    } else {
        return false;
    }
}

//编辑预览表格
IotM.User.onClickCell = function (index, field) {


    if (editIndex != index) {
        if (endEditing()) {
            $('#dataGrid_Preview').datagrid('selectRow', index).datagrid('beginEdit', index);

            var ed = $('#dataGrid_Preview').datagrid('getEditor', { index: index, field: field });
            ($(ed.target).data('textbox') ? $(ed.target).textbox('textbox') : $(ed.target)).focus();
            editIndex = index;
        } else {
            $('#dataGrid_Preview').datagrid('selectRow', editIndex);
        }
    }

}


IotM.User.CheckFlag = false;


IotM.User.BatchCompleteClick = function () {


    if (IotM.User.CheckFlag) { return;}


    IotM.User.CheckFlag = true;

    $('#dataGrid_Preview').datagrid('acceptChanges');

    var data = {};

    //道路
    var Street = $("#CNStreet_Batch").combobox("getValue");
    //小区
    var Community = $("#CNCommunity_Batch").combobox("getValue");
    //表类型
    var MeterType = $("#CNMeterType").combobox("getValue");

   

    data.Rows = $('#dataGrid_Preview').datagrid("getRows");

    if (data.Rows.length <= 0) { $.messager.alert('警告', "没有找到要生成的用户！", 'warn'); return; }



    var post = { Rows: JSON.stringify(data) };//JSON.stringify(json)把json转化成字符串

    post.Street = Street;
    post.Community = Community;
    post.MeterType = MeterType;



    $.post("../Handler/UserHandler.ashx?AType=BatchAdd",
           post,
            function (data, textStatus) {
                if (textStatus == 'success') {
                    if (data.Result) {

                        $.messager.alert('提示', data.TxtMessage, 'info', function () {
                            $('#dataGrid').datagrid('reload');
                            $('#wAdd_Batch').window('close');
                            $('#btnOk_Batch_complete').attr("disabled", false);
                        });
                        IotM.User.CheckFlag = false;
                    }
                    else {
                        $.messager.alert('警告', data.TxtMessage, 'warn');
                        IotM.User.CheckFlag = false;
                    }

                }
            }, "json");


}




//打开excel导入用户窗体
IotM.User.OpenExcelAdd = function () {

    $('#btnUpLoad').unbind('click').bind('click', function () { IotM.User.UploadClick(); });
    $('#btnDown').unbind('click').bind('click', function () { IotM.User.DownTemplateClick(); });
    $('#btnUpLoadCancel').unbind('click').bind('click', function () { $('#importImg').window('close'); });
    

    //IotM.User.InitUploadFile();

    $('#importImg').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '导入用户',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false

    });


}


var isRender = false;

function getCookie_() {
    IotM.GetCookie();
    return IotM.WebCookie;
}
//初始化上传控件
IotM.User.InitUploadFile = function () {
    if (isRender) { return; }
    $("#UploadFile").uploadify({
        'uploader': '../Scripts/uploadify-v2.1.4/uploadify.swf',       //上传文件的进度条
        'cancelImg': '../Scripts/uploadify-v2.1.4/uploadify-cancel.png',     //取消上传的图片
        'script': '../Handler/UserUploadHandler.ashx?AType=Upload',
        'queueID': 'fileQueue',
        'auto': false,
        'multi': false,
        'fileDesc': '请选择文件类型',
        'fileExt': '*.xls;*.xlsx',
        'buttonText': '选择文件',
        'scriptData': {  NO_COOKIE_SessionId: getCookie_() },
        'onComplete': function (event, queueID, fileObj, response, data) {
            var tResult = eval('(' + response + ')');
            if (tResult.Result) {

                IotM.User.NextExcelAdd();

            }
            else {
                $.messager.alert('警告', tResult.TxtMessage, 'warn');
            }
        }
    });
    isRender = true;
};




//下载模板
IotM.User.DownTemplateClick = function () {

    $.messager.confirm('确认', '将下载用户导入模板，是否确认? ', function (r) {

        if (r == true) {

            var iframe = document.createElement("iframe");
            iframe.src = "../IotM/DownFrame.aspx";
            iframe.style.display = "none";
            document.body.appendChild(iframe);
        }
    });
}



//文件导入
IotM.User.UploadClick = function () {
    var fd = new FormData();
    fd.append("upload", 1);
    fd.append("upfile", $("#file1").get(0).files[0]);
    $.ajax({
        url: "../Handler/UserUploadHandler.ashx?AType=Upload",
        type: "POST",
        processData: false,
        contentType: false,
        data: fd,
        success: function (d) {
            var tResult = eval('(' + d + ')');
            if (tResult.Result) {

                IotM.User.NextExcelAdd();

            }
            else {
                $.messager.alert('警告', tResult.TxtMessage, 'warn');
            }
            alert(d);
            console.log(d);
        }
    });
    //$('#UploadFile').uploadifyUpload();

};





IotM.User.NextExcelAdd = function () {


    $('#btnOk_Import').unbind('click').bind('click', function () { IotM.User.SureImport(); });
    $('#btnCancel_Importr').unbind('click').bind('click', function () { $('#importImgStep').window('close'); });


    $('#importImg').window('close');

    IotM.User.LoadUserTempGrid();

    $('#importImgStep').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '选择导入用户',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left: 100,
        top: 100

    });

}


IotM.User.LoadUserTempGrid = function () {

    var url = "../Handler/UserHandler.ashx?AType=QueryTemp";
    $('#dataGrid_Import').datagrid({
        title: '',
        url: url,
        height: IotM.MainGridHeight * 0.5,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: false,
        sort: "MeterNo",
        order: "ASC",
        columns: [
                    [

                     { field: 'ck', checkbox: true },
                     { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'Street', title: '街道', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'Community', title: '小区', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.5, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                     { field: 'MeterNum', title: '表底', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }

                    ]

        ],
        queryParams: { TWhere: '' },
        onLoadSuccess: function () { IotM.CheckMenuCode(); },
        onBeforeLoad: function (param) {
            var p = $('#dataGrid_Import').datagrid('getPager');
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

}


//确定导入
IotM.User.SureImport = function () {


    if (IotM.User.CheckFlag) { return; }

    IotM.User.CheckFlag = true;


    var rows = $('#dataGrid_Import').datagrid('getSelections');

    if (rows && rows.length <= 0) {
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    }
    var strNo = "";
    var data = {};
    for (var i = 0; i < rows.length; i++) {
        var index = $('#dataGrid_Import').datagrid('getRowIndex', rows[i]);
        if (i == rows.length - 1) {
            strNo += rows[i].MeterNo;
        }
        else {
            strNo += rows[i].MeterNo + ",";
        }
    }
    data.strNo = strNo;

    $.post("../Handler/UserHandler.ashx?AType=BatchImport",
              data,
               function (data, textStatus) {
                   if (textStatus == 'success') {
                       if (data.Result) {

                           $.messager.alert('提示', '操作成功！', 'info', function () {
                              
                               $('#dataGrid').datagrid('reload');
                           });
                       }
                       else
                           $.messager.alert('警告', data.TxtMessage, 'warn');
                       IotM.User.CheckFlag = false;
                   }
                   IotM.User.CheckFlag = false;
               }, "json");



}