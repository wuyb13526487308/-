IotM.namespace('IotM.PricePar');
//加载列表控件
IotM.PricePar.LoadDataGrid = function () {
    var url = "../Handler/PriceParHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "Ser",
        order: "ASC",
        columns: [
                    [


                       
                     { field: 'Ser', title: '编号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'PriceName', title: '价格类型名称', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },

                     {
                         field: 'IsUsed', title: '启用阶梯价', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,

                         formatter: function (value, rec, index) {
                             if (value == "0") { return "未启用"; }
                             else if (value == "1") { return "启用"; }
                             else { return "未知"; }
                        }

                     },

                     {
                         field: 'SettlementType', title: '结算周期', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {

                             if (value == "00") { return "按月"; }
                             else if (value == "01") { return "按季度"; }
                             else if (value == "10") { return "按半年"; }
                             else if (value == "11") { return "按全年"; }
                             else { return "未知"; }
                        }

                     },
                     { field: 'SettlementDay', title: '结算日', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },

                     { field: 'Ladder', title: '阶梯数', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Price1', title: '价格1', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Gas1', title: '用量1', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Price2', title: '价格2', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Gas2', title: '用量2', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Price3', title: '价格3', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Gas3', title: '用量3', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Price4', title: '价格4', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Gas4', title: '用量4', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'Price5', title: '价格5', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
                       
                       
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
IotM.PricePar.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.PricePar.AddClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    IotM.FormSetDefault('formAdd');
   
    IotM.PricePar.IsUsedChange();
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '添加价格参数',
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
IotM.PricePar.AddClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');
        data.Ladder = $("#CNLadder").numberspinner("getValue");
        data.PeriodStartDate = $("#CNPeriodStartDate").datebox('getValue');

        $.post("../Handler/PriceParHandler.ashx?AType=Add",
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
IotM.PricePar.OpenformEdit = function (obj) {
    $('#btnOk').unbind('click').bind('click', function () { IotM.PricePar.EditClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
  
    //var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    //$('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    if (!data) { $.messager.alert('提示', '没有选中行！', 'info'); return; }

    IotM.SetData('formAdd', data);

    IotM.PricePar.IsUsedChange();

    $("#CNLadder").numberspinner("setValue", data.Ladder);
    $("#CNPeriodStartDate").datebox('setValue', data.PeriodStartDate);

   
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '编辑价格参数',
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
IotM.PricePar.EditClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');
        data.Ladder = $("#CNLadder").numberspinner("getValue");
        $.post("../Handler/PriceParHandler.ashx?AType=Edit",
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


IotM.PricePar.RemoveClick = function (obj) {
    //var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    //$('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    if (!data) { $.messager.alert('提示', '没有选中行！', 'info'); return; }

    var index = $('#dataGrid').datagrid('getRowIndex', $('#dataGrid').datagrid('getSelected'));


    $.messager.confirm('确认', '是否真的删除?', function (r) {
        if (r == true) {
            $.post("../Handler/PriceParHandler.ashx?AType=Delete",
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


IotM.PricePar.SerachClick = function (value, name) {
    var Where="";
    if (value && name) {
        Where += '  AND ' + name + ' like \'%' + value + '%\'';
    }
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};



IotM.PricePar.IsUsedChange = function () {



    if ($("#CNIsUsed").attr("checked") == "checked") {

        $("#CNLadder").attr("disabled",false);
        $("#CNLadder").numberspinner("setValue", 2);
        $("#CNLadder").numberspinner("enable");

        

    } else {

        $("#CNPrice2").attr("disabled", "disabled");
        $("#CNPrice3").attr("disabled", "disabled");
        $("#CNPrice4").attr("disabled", "disabled");
        $("#CNPrice5").attr("disabled", "disabled");


        $("#CNGas1").attr("disabled", "disabled");
        $("#CNGas2").attr("disabled", "disabled");
        $("#CNGas3").attr("disabled", "disabled");
        $("#CNGas4").attr("disabled", "disabled");


        $("#CNLadder").numberspinner("setValue", 1);
        $("#CNLadder").numberspinner("disable");


    }


}



IotM.PricePar.DisablePrice = function () {

  
    $("#CNPrice2").attr("disabled", "disabled");
    $("#CNPrice3").attr("disabled", "disabled");
    $("#CNPrice4").attr("disabled", "disabled");
    $("#CNPrice5").attr("disabled", "disabled");


    $("#CNGas1").attr("disabled", "disabled");
    $("#CNGas2").attr("disabled", "disabled");
    $("#CNGas3").attr("disabled", "disabled");
    $("#CNGas4").attr("disabled", "disabled");


}


IotM.PricePar.LadderChange = function (value) {


    if (value == 1) {

        IotM.PricePar.DisablePrice();
        $("#CNIsUsed").attr("checked", false);
        $("#CNLadder").numberspinner("disable");
        

    }
    if (value == 2) {

        IotM.PricePar.DisablePrice();
        $("#CNPrice2").attr("disabled", false);
        $("#CNGas1").attr("disabled", false);

        $("#CNIsUsed").attr("checked", "checked");
        $("#CNLadder").numberspinner("enable");

    } else if (value == 3) {

        IotM.PricePar.DisablePrice();
      
        $("#CNPrice2").attr("disabled", false);
        $("#CNPrice3").attr("disabled", false);

        $("#CNGas1").attr("disabled", false);
        $("#CNGas2").attr("disabled", false);

        $("#CNIsUsed").attr("checked", "checked");
        $("#CNLadder").numberspinner("enable");

    }
    else if (value == 4) {

        IotM.PricePar.DisablePrice();
       
        $("#CNPrice2").attr("disabled", false);
        $("#CNPrice3").attr("disabled", false);
        $("#CNPrice4").attr("disabled", false);

        $("#CNGas1").attr("disabled", false);
        $("#CNGas2").attr("disabled", false);
        $("#CNGas3").attr("disabled", false);

        $("#CNIsUsed").attr("checked", "checked");
        $("#CNLadder").numberspinner("enable");

    }
    else if (value == 5) {
        $("#CNPrice2").attr("disabled", false);
        $("#CNPrice3").attr("disabled", false);
        $("#CNPrice4").attr("disabled", false);
        $("#CNPrice5").attr("disabled", false);

        $("#CNGas1").attr("disabled", false);
        $("#CNGas2").attr("disabled", false);
        $("#CNGas3").attr("disabled", false);
        $("#CNGas4").attr("disabled", false);

        $("#CNIsUsed").attr("checked", "checked");
        $("#CNLadder").numberspinner("enable");
    }


}


