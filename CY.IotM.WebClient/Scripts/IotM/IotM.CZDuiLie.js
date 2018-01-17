IotM.namespace('IotM.CZDuiLie');
//加载列表控件
/************************************
*方法名称：IotM.ChongZhi.LoadDataGrid 
*方法功能：加载列表控件dataGrid
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.CZDuiLie.LoadDataGridC = function () {
    var url = "../Handler/CZDuiLieHandler.ashx?AType=Query";
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: url,
        height: IotM.MainGridHeight,
        pageSize: 50,
        fitColumns: true,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "TopUpDate",
        order: "Desc",
        columns: [
                    [
                        { field: 'AID', title: '流水号', rowspan: 2, width: IotM.MainGridWidth * 0.07, align: 'center', sortable: true },
                        { field: 'MeterNo', title: '表号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true },
                        { field: 'UserName', title: '户名', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: true },
                        { field: 'Address', title: '地址', rowspan: 2, width: IotM.MainGridWidth * 0.2, align: 'center', sortable: true },
                        { field: 'Amount', title: '充值金额', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'right', sortable: true },
                        { field: 'TopUpDate', title: '充值日期', rowspan: 2, width: IotM.MainGridWidth * 0.13, align: 'center', sortable: true },
                        {
                            field: 'TopUpType', title: '充值类型', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                            formatter: function (value, rec, index) {
                                if (value == "0") { return "本地营业厅"; }
                                else if (value == "1") { return "接口"; }
                                else if (value == "2") { return "本地网站"; }
                                else if (value == "3") { return "换表补充"; }
                                else { return "未知"; }
                            }
                        },
                           {
                               field: 'State', title: '当前状态', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: true,
                               formatter: function (value, rec, index) {
                                   if (value == "0") { return "等待队列"; }
                                   else if (value == "1") { return "撤销充值"; }
                                   else if (value == "2") { return "充值成功"; }
                                   else {
                                       return "未知";
                                   }
                               }
                           },
                        {
                            field: 'opt', title: '操作', rowspan: 2, width: IotM.MainGridWidth * 0.05, align: 'center',
                            formatter: function (value, rec, index) {
                                var f = '<a href="#" mce_href="#"  onclick="IotM.CZDuiLie.ReView(this)"><span style="color:blue">刷新</span></a> ';
                                if (rec.State == "0" && rec.TopUpType == "0") {
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
*方法名称：IotM.CZDuiLie.SerachClick 
*方法功能：查询按钮点击事件
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.CZDuiLie.SerachClick = function () {
    //串接Where条件 Start
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
    //串接Where条件 End

    //调用Easyui的方法更新画面
    $('#dataGrid').datagrid('options').queryParams = {
        TWhere: Where
        ,
        DateS: $('#select_RegisterDateS').datebox('getValue')
        ,
        DateE: $('#select_RegisterDateE').datebox('getValue')
    };
    //重新加载Gridview的资料
    $('#dataGrid').datagrid('load');
};

/************************************
*方法名称：IotM.CZDuiLie.ReView 
*方法功能：行刷新事件（当点击行中的刷新按钮时，将改行资料刷新）
*创建时间：20150717
*创建人  ：
*参数    ：
*************************************/
IotM.CZDuiLie.ReView = function (obj) {
    //后去到当前行的索引号
    var indexs = $(obj).parent().parent().parent().attr("datagrid-row-index");
    //通过索引号，将该行设定为选中状态
    $('#dataGrid').datagrid('selectRow', indexs);
    //获取到选中行的数据
    var datas = $('#dataGrid').datagrid('getSelected');
    //加载遮罩LOGing效果
    $("<div class=\"datagrid-mask\"></div>").css({ display: "block", width: "100%", height: $(window).height() }).appendTo("body");
    $("<div class=\"datagrid-mask-msg\"></div>").html("努力加载中...").appendTo("body").css({ display: "block", left: ($(document.body).outerWidth(true) - 190) / 2, top: ($(window).height() - 45) / 2 });
    $.ajax({
        type: 'GET',
        async: false,
        url: "../Handler/CZDuiLieHandler.ashx?AType=REVIEW&ID=" + datas.AID,
        success: function (data, textStatus) {
            data = JSON.parse(data);
            //判断在后台查询是否成功
            if (textStatus == 'success') {
                if (data.Result) {
                    //判断获取到的数据是否为空
                    if (JSON.parse(data.TxtMessage).rows.length > 0) {
                        //获取需要更新的数据
                        var valueType = JSON.parse(data.TxtMessage).rows[0].TopUpType + "";
                        var Addressa = JSON.parse(data.TxtMessage).rows[0].Address;
                        var Amounta = JSON.parse(data.TxtMessage).rows[0].Amount;
                        var TopUpDatea = JSON.parse(data.TxtMessage).rows[0].TopUpDate;
                        var Statea = JSON.parse(data.TxtMessage).rows[0].State;
                        //更新数据（easyui）
                        $("#dataGrid").datagrid('updateRow', {
                            index: indexs,
                            row: {
                                TopUpType: valueType,
                                Address: Addressa,
                                Amount: Amounta,
                                TopUpDate: TopUpDatea,
                                State: Statea
                            }
                        });
                        //更新行号（解决更新数据后行号发生改变的问题）
                        $("[datagrid-row-index='" + indexs + "']").find(".datagrid-cell-rownumber").html(parseInt(indexs) + 1);
                    }
                    //$.messager.alert('提示', '操作成功！', 'info');
                }
                else {
                    $.messager.alert('警告', data.TxtMessage, 'warn');
                }
            } else {
                $.messager.alert('警告', data.TxtMessage, 'warn');
            }
        }
    });
    //移除LOGing遮罩效果
    setTimeout(function () {
        $('.datagrid-mask').remove();
        $('.datagrid-mask-msg').remove();
    }, 500);
}
