IotM.namespace('IotM.Meter');
//加载列表控件
IotM.Meter.LoadDataGrid = function () {
    var url = "../Handler/UserHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        pageSize:20,
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

                        {
                            field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center',
                            formatter: function (value, rec, index) {
                                var e = '';
                                if (rec.State == "0") {
                                    e = '<a href="#" mce_href="#" menucode="bjyh" onclick="IotM.Meter.OpenformEdit(this)"><span style="color:blue">安装</span></a> ';
                                } else {
                                    e = '<span style="color:black">已安装</span>';
                                }
                                return e;
                            }
                        }
                    ]
                   
        ],
        queryParams: { TWhere: 'AND State=0 ' },
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

IotM.Meter.OpenformEdit = function (obj) {
    $('#btnOk').unbind('click').bind('click', function () { IotM.Meter.EditClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
  
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.SetData('formAdd', data);
    $("#CNMeterNo").val("");
    $("#CNTotalAmount").numberbox('clear');

    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.5,
        title: '安装登记',
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
IotM.Meter.EditClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');
        if (data.MeterNo === "") {
            data.TxtMessage = "表号不能为空，请填写表号，然后重试!";
            $.messager.alert('提示', data.TxtMessage, 'warn');
            return;
        }
       
        $.post("../Handler/MeterHandler.ashx?AType=Add",
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
                                  $('#dataGrid').datagrid('reload');
                                  $('#wAdd').window('close');
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};



IotM.Meter.SerachClick = function () {


    var Where = " AND State=0 ";
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










