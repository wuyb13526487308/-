IotM.namespace('IotM.MeterGasBill');
//加载列表控件
IotM.MeterGasBill.LoadDataGrid = function () {
    var url = "../Handler/MeterGasBillHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "MeterNo",
        order: "ASC",
        columns: [
                    [

                        { field: 'UserID', title: '户号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                        { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.12, align: 'center', sortable: true },
                        { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.23, align: 'center', sortable: true },

                        { field: 'UseMonth', title: '结算月份', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'LastSum', title: '上次表底', rowspan: 2, width: IotM.MainGridWidth * 0.12, align: 'center', sortable: true },
                        { field: 'ThisSum', title: '本次表底', rowspan: 2, width: IotM.MainGridWidth * 0.12, align: 'center', sortable: true },
                        { field: 'UseGasSum', title: '用气量', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'GasFee', title: '气费', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                      
                        { field: 'ThisReadDate', title: '抄表时间', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true }
                       
                      
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
IotM.MeterGasBill.OpenformAdd = function () {
    $('#btnOk').unbind('click').bind('click', function () { IotM.MeterGasBill.AddClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });


    $('#progressbar').hide();
    $('#btnArea').show();

    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '气量表气费结算',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 100,
        left:200,
        onOpen: function () {
            IotM.checkisValid('formAdd');
          
        }
    });
};




IotM.MeterGasBill.AddClick = function () {
    if (IotM.checkisValid('formAdd')) {

        var params = {};
        params.PriceType = $('#CNPriceType').combobox('getValue');
        params.Month = $('#CNJieSuanDate').datebox('getValue');
       
      
        $.post("../Handler/MeterGasBillHandler.ashx?AType=GETSETTLE",
                 params,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                            

                              var objectData=eval('(' + data.TxtMessage + ')');

                              var meterList = objectData.rows;

                              IotM.MeterGasBill.Progressbar = 0;
                              IotM.MeterGasBill.ProgressbarAll = meterList.length;


                              if (meterList.length > 0) {

                                  $.messager.confirm('确认', '将对' + meterList.length + '个用户进行气费结算，是否确认?', function (r) {

                                      if (r == true) {

                                          $('#progressbar').show();
                                          $('#btnArea').hide();

                                          for (var i = 0; i < meterList.length; i++) {

                                              IotM.MeterGasBill.reSend(params.PriceType, params.Month, meterList[i].MeterNo);

                                          }


                                      }

                                  });
                              }
                              else {

                                  $.messager.alert('提示', '该价格下没有用户！', 'warn');

                              }
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');
                      }
                  }, "json");
    }
};



IotM.MeterGasBill.SerachClick = function (value, name) {
    var Where = "";

    if ($("#select_UserID").val() != "") {
        //户号
        Where += '  AND  UserID  like \'%' + $("#select_UserID").val() + '%\'';
    }
    if ($("#select_UserName").val() != "") {
        //户名
        Where += '  AND  UserName  like \'%' + $("#select_UserName").val() + '%\'';
    }
    if ($("#select_Date").datebox('getValue')!= "") {
        //结算月份
        Where += '  AND  UseMonth  = \'' + $("#select_Date").datebox('getValue') + '\'';
    }

    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};


IotM.MeterGasBill.reSend = function (PriceType, Month, MeterNo) {

    if (!MeterNo) { return; }

    var data = {};
    data.PriceType = PriceType;
    data.Month = Month;
    data.MeterNo = MeterNo;


    $.ajax({
        url: "../Handler/MeterGasBillHandler.ashx?AType=SETTLE",
        data: data,
        type: "POST",
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                if (data.Result) {

                    IotM.MeterGasBill.Progressbar += 1;

                    var value = $('#progressbar').progressbar('getValue');
                    if (value <= 100) {

                        value = parseInt(IotM.MeterGasBill.Progressbar / IotM.MeterGasBill.ProgressbarAll * 100);
                        $('#progressbar').progressbar('setValue', value);
                    }
                    if (IotM.MeterGasBill.Progressbar == IotM.MeterGasBill.ProgressbarAll) {

                        $.messager.alert('提示', '气费结算成功！', 'info', function () {

                            $('#dataGrid').datagrid('options').queryParams = { TWhere: ' AND  UseMonth  = \''+Month +'\'' };
                            $('#dataGrid').datagrid('load');
                            $('#wAdd').window('close');
                        });
                    }
                }
                else {
                    //setTimeout(IotM.MeterGasBill.reSend(data.TxtMessage, IotMid, dpid), 100);
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {

        },
        dataType: "json"
    });

}




IotM.MeterGasBill.OpenformExport = function () {


    $('#btnOkExport').unbind('click').bind('click', function () { IotM.MeterGasBill.ExportClick(); });
    $('#btnCancelExport').unbind('click').bind('click', function () { $('#wDownDiv').window('close'); });
  
    $('#wDownDiv').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '导出结算数据',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        top: 150,
        left: 250
      
    });

}



//确认导出结算数据
IotM.MeterGasBill.ExportClick = function () {


    var check = $("input[name='download']:checked").val();

    var exportMonth = $('#CNExportDate').datebox('getValue');

    //导出excel
    if (check == "1") {




        var ifram = document.createElement("iframe");
        ifram.src = "../IotM/ExportGasBill.aspx?Month=" + exportMonth ;
        ifram.style.display = "none";
        document.body.appendChild(ifram);
       





      
    } else {

       
    }


}






