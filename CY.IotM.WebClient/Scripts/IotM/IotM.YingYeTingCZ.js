// 指定命名空间
IotM.namespace('IotM.YingYeTingCZ');

/************************************
*方法名称：LoadDataGrid
*方法功能：加载页面Grid资料
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.YingYeTingCZ.LoadDataGrid = function () {
    var url = "../Handler/YingYeTingCZHandler.ashx?AType=Query";
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
                        { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                        { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                        {
                            field: 'ValveState', title: '阀状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                            formatter: function (value, rec, index) {
                                if (value == "0") { return "阀开"; }
                                else { return "阀关"; }
                            }
                        },
                       { field: 'TotalAmount', title: '总用量', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                       //{ field: 'RemainingAmount', title: '余额', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                       //{ field: 'ReadDate', title: '抄表时间', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                       {
                           field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center',
                           formatter: function (value, rec, index) {
                               //var e = '<a href="#" mce_href="#" menucode="bjyh" onclick="IotM.SetUpLoadDate.OpenformEdit(this)"><span style="color:blue">编辑</span></a> ';
                               var f = '<a href="#" mce_href="#"  onclick="IotM.YingYeTingCZ.GetUserMessage(this)"><span style="color:blue">选择</span></a> ';
                               return f;
                           }
                       }
                    ]

        ],
        queryParams: { TWhere: "and MeterType='01'" },//条件只有金额表可以进行充值,所以只显示金额表
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
*方法名称：IotM.YingYeTingCZ.GetUserMessage
*方法功能：选择连接动作Grid资料
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.YingYeTingCZ.GetUserMessage = function (obj) {
    $('#btnChongZhi').unbind('click').bind('click', function () { IotM.YingYeTingCZ.ChongZhiClick(); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    IotM.FormSetDefault('formAdd');
    //$("#userListTR").show();
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.4,
        title: '充值',
        modal: true,
        shadow: true,
        closed: false,
        collapsible: false,
        minimizable: false,
        maximizable: false,
        left:300,
        top: 200,
        onOpen: function () {
            var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
            $('#dataGrid').datagrid('selectRow', index);
            var data = $('#dataGrid').datagrid('getSelected');
            $("#select_UserIDE").val(data.MeterNo);
            $("#select_UserNameE").val(data.UserName);
            $("#select_AdressE").val(data.Address);
            $("#hidMeterID").val(data.MeterID);
            $("#hidUserID").val(data.UserID);

            $("#CNAmount").val("");
            IotM.checkisValid('formAdd');
        }
    });
}

/************************************
*方法名称：ChongZhiClick
*方法功能：点击查询按钮根据条件加载页面Grid资料
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.YingYeTingCZ.ChongZhiClick = function () {
    if (!($("#formAdd").form('validate'))) {
        return false;
    }
    //return $("#form1").form('validate');
    var MeterNo = $("#select_UserIDE").val();
    var Amount = $("#CNAmount").val();
    var MeterID = $("#hidMeterID").val();
    var UserID = $("#hidUserID").val();
    if (Amount > 999999) {
        $.messager.alert('提示', '充值金额不能超过999999 ！', 'info');
        return false;
    }
    $.ajax({
        type: 'GET',
        async: false,
        url: "../Handler/YingYeTingCZHandler.ashx?AType=CHONGZHI&MeterNo=" + MeterNo + "&Amount=" + Amount + "&MeterID=" + MeterID + "&UserID=" + UserID,
        success: function (data, textStatus) {
            data = JSON.parse(data);
            if (textStatus == 'success') {
                if (data.Result) {
                    //$('#btnCancel').click();
                    //$('#dataGrid').datagrid('appendRow', eval('(' + data.TxtMessage + ')'));
                    $.messager.alert('提示', '操作成功！', 'info');
                }
                else { $.messager.alert('警告', data.TxtMessage, 'warn'); }
            } else {
                $.messager.alert('警告', data.TxtMessage, 'warn');
            }
            $('#btnCancel').click();
        }
    });
};
IotM.YingYeTingCZ.SerachClick = function () {
    var Where = "and MeterType='01'";//条件只有金额表可以进行充值
    if ($("#select_UserID").val() != "") {//户号
        Where += '  AND  UserID  like \'%' + $("#select_UserID").val() + '%\'';
    }
    if ($("#select_UserName").val() != "") {//户名
        Where += '  AND  UserName  like \'%' + $("#select_UserName").val() + '%\'';
    }
    if ($("#select_Adress").val() != "") {//地址
        Where += '  AND  Address  like \'%' + $("#select_Adress").val() + '%\'';
    }
    //设定查询条件及参数
    $('#dataGrid').datagrid('options').queryParams = { TWhere: Where };
    //重新加载资料
    $('#dataGrid').datagrid('load');
};
