IotM.namespace('IotM.DianHuo');
/************************************
*方法名称：IotM.DianHuo.LoadDataGrid 
*方法功能：加载列表控件dataGrid
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.DianHuo.loadDataGrid = function () {
    var url = "../Handler/UserHandler.ashx?AType=QUERYVIEW";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        pageSize: 20,
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
                        {
                            field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                            formatter: function (value, rec, index) {

                                if (value == "0") { return "等待安装"; }
                                else if (value == "1" || value == "2") { return "等待点火"; }
                                else if (value == "3") { return "正常使用"; }
                                else { return "未知"; }

                            }
                        },
                        { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.4, align: 'center', sortable: true },
                        { field: 'InstallDate', title: '安装日期', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                        { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                        { field: 'EnableMeterOper', title: '参与人员', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        {
                            field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center',
                            formatter: function (value, rec, index) {
                                var e = '';
                                if (rec.State == "2") {
                                    e = '<a href="#" mce_href="#" menucode="bjyh" onclick="IotM.DianHuo.btnRevoke(this)"><span style="color:blue">撤销点火</span></a> ';
                                } else {
                                    e = '<span style="color:black"></span>';
                                }
                                return e;
                            }
                        }
                    ]
        ],
        queryParams: { TWhere: ' AND State=1 ' },
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

/************************************
*方法名称：IotM.DianHuo.SerachClick 
*方法功能：查询按钮事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.DianHuo.SerachClick = function () {
    var Where = "";
    if ($("#select_UserID").val() != "") {
        //户号
        Where += '  AND  UserID  like \'%' + $("#select_UserID").val() + '%\'';
    }
    if ($("#select_UserName").val() != "") {
        //户名
        Where += '  AND  UserName  like \'%' + $("#select_UserName").val() + '%\'';
    }
    if ($("#select_Adress").val() != "") {
        //地址
        Where += '  AND  Address  like \'%' + $("#select_Adress").val() + '%\'';
    }
    if (document.getElementById("CNState")) {
        //状态
        if ($("#CNState").combobox("getValue") != "")
            Where += '  AND  State  =  \'' + $("#CNState").combobox("getValue") + '\'';
    }
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    $('#dataGrid').datagrid('load');
};
/************************************
*方法名称：IotM.DianHuo.OpenformEdit 
*方法功能：打开选择用户页面
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.DianHuo.OpenformEdit = function () {
    var rows = $('#dataGrid').datagrid('getSelections');//获取选中行数据
    if (rows && rows.length <= 0) {//判断选中行数据是否有值
        $.messager.alert('提示', '没有选中行！', 'info'); return;
    };
    var strName = "";
    for (var i = 0; i < rows.length; i++) {
        if (rows[i].State == "3") { $.messager.alert('提示', '请选择等待点火用户！', 'info'); return; }
        var index = $('#dataGrid').datagrid('getRowIndex', rows[i]);
        if (i == rows.length - 1) {
            strName += rows[i].UserName;
        }
        else {
            strName += rows[i].UserName + ",";
        }
    }
    //给按钮注册单击事件
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    $('#btnOk').unbind('click').bind('click', function () { IotM.DianHuo.Registration(); });

    //IotM.SetData('formAdd', data);
    //点火户数
    $("#Ignitions").val(rows.length);
    //点火日期
    $('#CNInstallDate').datebox('setValue', IotM.MyDateformatter(new Date()));
    //开启页面
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '点火申请',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        onOpen: function () {
            //$('#CNMeterType').combobox('setValue', rows[0].MeterType + "")//赋值
            //$('#Description').val(strName);//参与人员
            //$('#CNPriceType').combobox('setValues', rows[0].PriceType)//赋值
            //IotM.checkisValid('fRegistration');
        }
    });
};
/************************************
*方法名称：IotM.DianHuo.Registration 
*方法功能：点火操作
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.DianHuo.Registration = function () {
    if (!($("#formAdd").form('validate'))) {
        return false;
    }
    var strUserID = "";
    var strNo = "";
    var strName = "";
    var data = {};
    var rows = $('#dataGrid').datagrid('getSelections');//获取选中行的数据
    for (var i = 0; i < rows.length; i++) {//循环将需要点火的用户串接起来
        var index = $('#dataGrid').datagrid('getRowIndex', rows[i]);
        if (i == rows.length - 1) {
            strUserID += rows[i].UserID;
            strNo += rows[i].MeterNo;
            strName += rows[i].UserName;
        }
        else {
            strNo += rows[i].MeterNo + ",";
            strName += rows[i].UserName + ",";
            strUserID += rows[i].UserID + ",";
        }
        if (rows[i].State == '2') {//在向表发送过点火命令后就不能重复发送点火命令
            $.messager.alert('警告', '用户' + rows[i].UserName + '已发送点火申请，请勿重复发送！', 'warn');
            return false;
        }
    }
    data.strNo = strNo;     
    data.UserID = strUserID;                                      //点火用户ID
    data.strName = strName;                                     //点火用户姓名
    data.MType = $('#CNMeterType').combobox('getValue');        //点火用户表类型
    data.PriceType = $('#CNPriceType').combobox('getValue');    //点火用户价格类型
    data.EnableDate = $('#CNInstallDate').datebox('getValue');; //点火用户
    data.EnableOper = $("#Description").val();                  //参与人员

    $.post("../Handler/DianHuoHandler.ashx?AType=REGISTRATION",
             data,
              function (data, textStatus) {
                  if (textStatus == 'success') {
                      if (data.Result) {
                          $.messager.alert('提示', '操作成功！', 'info', function () {
                              $('#wAdd').window('close');
                              $('#dataGrid').datagrid('reload');
                          });
                      }
                      else
                          $.messager.alert('警告', data.TxtMessage, 'warn');

                  }
              }, "json");
};
/************************************
*方法名称：IotM.DianHuo.btnRevoke 
*方法功能：撤销电火
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.DianHuo.btnRevoke = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    $.messager.confirm('确认', '是否真的撤销?', function (r) {
        if (r == true) {
            $.post("../Handler/DianHuoHandler.ashx?AType=REVOKE",
                 data,
                  function (data, textStatus) {
                      if (textStatus == 'success') {
                          if (data.Result) {
                              $('#dataGrid').datagrid('load');
                              //$('#dataGrid').datagrid('deleteRow', parseInt(index));
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


