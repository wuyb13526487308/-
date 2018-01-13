IotM.namespace('IotM.SystemPar');

IotM.SystemPar.OpenformSystemPar = function () {


    var data = {};
    $.post("../Handler/SystemParHandler.ashx?AType=Query",
               data,
                function (data, textStatus) {
                    if (textStatus == 'success') {
                        data = eval('(' + data.TxtMessage + ')');
                        if (data != null && data.total > 0) {
                            IotM.SystemPar.data = data.rows;
                            IotM.SetData('formAdd', IotM.SystemPar.data[0]);
                        } 
                    }
                }, "json");



   
    $('#btnOk').unbind('click').bind('click', function () { IotM.SystemPar.BtnOkClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddSystemPar').window('close'); });
    $('#wAddSystemPar').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.7,
        title: '设置服务器参数',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false
    });
};

IotM.SystemPar.BtnOkClick = function () {
    if (IotM.checkisValid('formAdd')) {
        var data = IotM.GetData('formAdd');
        $.post("../Handler/SystemParHandler.ashx?AType=Edit",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $.messager.alert('提示', '操作成功！', 'info', function () {
                                  //$('#btnCancel').click();
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};
