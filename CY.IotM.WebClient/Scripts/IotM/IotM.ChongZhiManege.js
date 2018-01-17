IotM.namespace('IotM.ChongZhi');

/************************************
*方法名称：IotM.ChongZhi.LoadDataGrid 
*方法功能：加载列表控件dataGrid
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChongZhi.LoadDataGrid = function () {
    var url = "../Handler/ChongZhiHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        pageSize:50,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "TopUpDate",
        order: "DASC",
        columns: [
                    [
                     { field: 'AID', title: '流水号', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                     { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.18, align: 'center', sortable: true },
                     { field: 'Amount', title: '充值金额', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'right', sortable: true },
                     { field: 'TopUpDate', title: '充值日期', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                     {
                         field: 'State', title: '状态', rowspan: 2, width: IotM.MainGridWidth * 0.06, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "0") { return "等待执行"; }
                             else if (value == "1") { return "撤销充值"; }
                             else if (value == "2") { return "充值完成"; }
                             else { return "未知"; }
                         }

                     },
                     {
                         field: 'TopUpType', title: '充值类型', rowspan: 2, width: IotM.MainGridWidth * 0.06, align: 'center', sortable: true,
                         formatter: function (value, rec, index) {
                             if (value == "0") { return "本地营业厅"; }
                             else if (value == "1") { return "接口"; }
                             else if (value == "2") { return "本地网站"; }
                             else if (value == "3") { return "换表补充"; }
                             else { return "未知"; }
                         }

                     },
                    { field: 'Oper', title: '操作员', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center', sortable: true },
                     {
                         field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center',
                         formatter: function (value, rec, index) {
                             //var e = '<a href="#" mce_href="#" menucode="bjyh" onclick="IotM.ChongZhi.OpenformEdit(this)"><span style="color:blue">编辑</span></a> ';
                             var f = '';// '<a href="#" mce_href="#"  onclick="IotM.ChongZhi.GetUserDetail(this)"><span style="color:blue">补打发票</span></a> ';
                             if (rec.State == "0" && rec.TopUpType=="0") {
                                 f += '<a href="#" mce_href="#"  onclick="IotM.ChongZhi.OpenformAdd(this)"><span style="color:blue">撤销</span></a> ';
                             }
                             return f;
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

/************************************
*方法名称：IotM.ChongZhi.SerachClick
*方法功能：查询按钮事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChongZhi.SerachClick = function () {
    var Where = "";
    if ($("#CNUserName").val() != "") {
        Where += '  AND  UserName  like \'%' + $("#CNUserName").val() + '%\'';
    }
    if ($("#CNAdress").val() != "") {
        Where += '  AND  Address  like \'%' + $("#CNAdress").val() + '%\'';
    }
    if ($("#CNUserName").val() != "") {
        Where += '  AND  UserName  like \'%' + $("#CNUserName").val() + '%\'';
    }
    if ($("#CNTopUpType").combobox("getValue") != "")
        Where += '  AND  TopUpType  =  \'' + $("#CNTopUpType").combobox("getValue") + '\'';
    $('#dataGrid').datagrid('options').queryParams = {
        TWhere: Where
        ,
        DateS: $('#select_RegisterDateS').datebox('getValue')
        ,
        DateE: $('#select_RegisterDateE').datebox('getValue')
    };
    $('#dataGrid').datagrid('load');
};
/************************************
*方法名称：IotM.ChongZhi.revoke
*方法功能：撤销充值
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChongZhi.revoke = function (obj) {
    var index = $(obj).parent().parent().parent().attr("datagrid-row-index");
    $('#dataGrid').datagrid('selectRow', index);
    var data = $('#dataGrid').datagrid('getSelected');
    var MeterNo = data.MeterNo;
    var Amount = data.Amount;
    var MeterID = data.MeterID;
    var UserID = data.UserID;
    var TaskID = data.TaskID;
    var Context = $('#CNContext').val();

    $.ajax({
        type: 'GET',
        async: false,
        url: "../Handler/ChongZhiHandler.ashx?AType=CXCHONGZHI&AID=" + data.AID + "&MeterNo=" + MeterNo + "&Amount=" + Amount + "&MeterID=" + MeterID + "&UserID=" + UserID + "&Context=" + Context + "&TaskID=" + TaskID,
        success: function (data, textStatus) {
            data = JSON.parse(data);
            if (textStatus == 'success') {
                if (data.Result) {
                    $('#dataGrid').datagrid('load');
                    //$('#btnCancel').click();
                    //$('#dataGrid').datagrid('appendRow', eval('(' + data.TxtMessage + ')'));
                    $.messager.alert('提示', '操作成功！', 'info');
                }
                else {
                    $.messager.alert('警告', data.TxtMessage, 'warn');
                }
            } else {
                $.messager.alert('警告', data.TxtMessage, 'warn');
            }
            $('#btnCancel').click();
        }
    });
}
/************************************
*方法名称：IotM.ChongZhi.OpenformAdd
*方法功能：打开撤销充值页面
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.ChongZhi.OpenformAdd = function (obj) {
    $('#btnOk').unbind('click').bind('click', function () { IotM.ChongZhi.revoke(obj); });
    $('#btnCancel').unbind('click').bind('click', function () { $('#wAdd').window('close'); });
    IotM.FormSetDefault('formAdd');
    $("#userListTR").show();
    $('#wAdd').window({
        resizable: false,
        width: IotM.MainGridWidth * 0.3,
        title: '撤销充值',
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



