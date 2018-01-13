IotM.namespace('AdM.ContentMain');
//加载主题列表
AdM.ContentMain.LoadDataGridView = function () {
    var url = "../Handler/AdMHandler.ashx?AType=QueryView";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pagination: true,
        //rownumbers: true,
        singleSelect: true,
        sort: "AC_ID",
        order: "DASC",
        columns: [
                    [
                     { field: 'CompanyID', title: '单位帐号', hidden: true },
                     { field: 'Context', title: '广告主题', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'CreateDate', title: '创建日期', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "0") { return "草稿"; }
                             else if (value == "1" ) { return "可发布"; }
                             else if (value == "2") { return "已发布"; }
                             else { return "未知"; }
                             return new String(row.opt);
                         }
                     },
                    
                      {
                          field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center',
                          formatter: function (value, rec, index) {
                              var a = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentMain.OpenformEdit(this)"><span style="color:blue">编辑</span></a> ';
                              var b = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentMain.ContentListHerf(this)"><span style="color:blue">内容管理</span></a> ';
                              //var c = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentMain.Preview(this)"><span style="color:blue">预览</span></a> ';
                              var c = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentMain.Preview(this)"><span style="color:blue">预览</span></a> ';

                              
                              if (rec.State == "1") {
                                  var d = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentMain.RemoveClick(this)"><span style="color:blue">删除</span></a> ';
                                  var e = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentMain.UpdateNO(this)"><span style="color:blue">草稿</span></a> ';
                              } else if (rec.State == "2") {
                                  var d = '<span style="color:#5b5b5b">删除</span> ';
                                  var e = '<span style="color:#5b5b5b">提交</span>';
                              } else {
                                  var d = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentMain.RemoveClick(this)"><span style="color:blue">删除</span></a> ';
                                  var e = ' <a href="#" mce_href="#" menucode="bjyh" onclick="AdM.ContentMain.UpdateOK(this)"><span style="color:blue">提交</span></a> ';

                              }
                              return a+b+c+d+e;
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
AdM.ContentMain.SerachClick = function () {
    var Where = "";
    if ($("#AD_Context").val() != "") {
        Where += '  AND  Context  like \'%' + $("#AD_Context").val() + '%\'';
    }
    if ($("#State").combobox('getValue') != "") {
        Where += '  AND  State  = ' + $("#State").combobox('getValue');
    }
    if ($("#bDate").datebox('getValue') != "") {
        Where += '  AND  CreateDate  > \'' + $("#bDate").datebox('getValue') + ' ' + $("#bDataTime").timespinner('getValue') + '\'';
    }
    if ($("#eDate").datebox('getValue')) {
        Where += '  AND  CreateDate  < \'' + $("#eDate").datebox('getValue') + ' ' + $("#eDataTime").timespinner('getValue') + '\'';
    }


    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};



//打开窗口
AdM.ContentMain.OpenformAdd = function () {

    $('#btnOk').unbind('click').bind('click', function () { AdM.ContentMain.AddContextClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddContext').window('close'); });
    IotM.AddRelaseDisabled('wAddContext');
    IotM.FormSetDefault('wAddContext');
    $('#wAddContext').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '添加主题',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () { IotM.checkisValid('wAddContext'); }
    });
};
//添加主题
AdM.ContentMain.AddContextClick = function () {

    if (IotM.checkisValid('wAddContext')) {
        var data = IotM.GetData('wAddContext');
        if (data.Context == "") { alert("主题不能为空！"); return; }
        $.post("../Handler/AdMHandler.ashx?AType=Add",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#btnCancel').click();
                              //$('#dataGrid').datagrid('appendRow', eval('(' + data.TxtMessage + ')'));
                              $('#dataGrid').datagrid('reload');
                              $.messager.alert('提示', '操作成功！', 'info');
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};



//打开修改主题
AdM.ContentMain.OpenformEdit = function (obj) {

    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    IotM.SetData('wAddContext', data);
    if (data.Context == "") { alert("主题不能为空！"); return; }

    $('#btnOk').unbind('click').bind('click', function () { AdM.ContentMain.EditContextClick() });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAddContext').window('close'); });

    $('#wAddContext').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '编辑主题',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () { IotM.EditDisabled('wAddContext'); IotM.checkisValid('wAddContext'); }
    });
};
AdM.ContentMain.EditContextClick = function () {
    if (IotM.checkisValid('wAddContext')) {
        var data = IotM.GetData('wAddContext');
        $.post("../Handler/AdMHandler.ashx?AType=EDIT",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              //$('#dataGrid').datagrid('updateRow',
                              //    {
                              //        index: $('#dataGrid').datagrid('getRowIndex', $('#dataGrid').datagrid('getSelected')),
                              //        row: eval('(' + data.TxtMessage + ')')
                              //    });
                              $('#dataGrid').datagrid('reload');
                              $.messager.alert('提示', '操作成功！', 'info', function () {
                                  $('#wAddContext').window('close');
                              });
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
    }
};


AdM.ContentMain.ContentListHerf = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    //alert('AdContentList.aspx');
    window.location.href = "AdContentList.aspx?CompanyID="+ data.CompanyID+"&AC_ID=" + data.AC_ID + "&State=" + data.State;
};




AdM.ContentMain.RemoveClick = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    if (data.State == 2) { alert("主题已经发布不能删除！"); return; }
    $.messager.confirm('确认', '删除主题,其所属内容也将一起删除！是否真的删除？', function (r) {
        if (r == true) {
            $.post("../Handler/AdMHandler.ashx?AType=DELCONTENT",
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

//草稿->可发布
AdM.ContentMain.UpdateOK = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '确认把主题修改为可发布状态吗？', function (r) {
        if (r == true) {
            $.post("../Handler/AdMHandler.ashx?AType=UPDATEOK",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('reload');
                              //$.messager.alert('提示', data.TxtMessage, 'info', function () {
                              //});
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
        }
    }
   );
};

//草稿->可发布
AdM.ContentMain.UpdateNO = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '确认把主题修改为草稿状态吗？', function (r) {
        if (r == true) {
            $.post("../Handler/AdMHandler.ashx?AType=UPDATENO",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('reload');
                              //$.messager.alert('提示', data.TxtMessage, 'info', function () {
                              //});
                          }
                          else
                              $.messager.alert('警告', data.TxtMessage, 'warn');

                      }
                  }, "json");
        }
    }
   );
};

//预览
AdM.ContentMain.Preview = function (obj) {


    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');

    window.open('AdMPlay.aspx?CompanyID='+ data.CompanyID+'&AC_ID=' + data.AC_ID, '预览广告', 'height=500 width=730, top=150, left=200, toolbar=no, menubar=no, scrollbars=no, resizable=no, location=no, status=no')

    //$('#wPreView').window({
    //    resizable: false,
    //    width: 720,
    //    height: 500,
    //    title: '预览广告',
    //    modal: true,
    //    shadow: true,
    //    closed: false,
    //    collapsible: false,
    //    minimizable: false,
    //    maximizable: false,
    //    top: 20,
    //    left: 30,
    //    onOpen: function () {

    //        var winPreView = document.getElementById("wPreView");

    //        var iframe = document.createElement("iframe");
    //        iframe.src = "AdMPlay.aspx?AC_ID=" + data.AC_ID;
    //        //iframe.style.display = "none";

    //        iframe.frameBorder = 0;
    //        iframe.scrolling = "no";

    //        iframe.marginheight = 0;
    //        iframe.marginwidth = 0;

    //        iframe.height = 430;
    //        iframe.width = 680;

    //        winPreView.innerHTML = "";
    //        winPreView.appendChild(iframe);

    //    }
    //});

};