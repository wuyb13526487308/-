IotM.namespace('IotM.SystemLogManage');
//解析参数
IotM.SystemLogManage.Url = '../Handler/SystemManage/SystemLogManageHandler.ashx?AType=Query';
IotM.SystemLogManage.LoadDataGrid = function () {
    $('#dataGrid').datagrid({
        title: '',
        toolbar: '#tb',
        url: IotM.SystemLogManage.Url,
        height: IotM.MainGridHeight,
        fitColumns: true,
        nowrap: false,
        pagination: true,
        rownumbers: true,
        singleSelect: true,
        sort: "RevDate",
        order: "DESC",
        columns: [
                      [
                        {
                            field: 'LogType', title: '日志类别', width: IotM.MainGridWidth * 0.08, align: 'center', sortable: false,
                            formatter: function (value, rec, index) {
                                var data = IotM.Initiate.SystemLogTypes;
                                for (var i = 0; i < data.length; i++) {
                                    if (data[i].TypeID == value) {
                                        return data[i].TypeName;
                                    }
                                }
                                return 'LogType:' + value;
                            }
                        },
                        {
                            field: 'OperID', title: '登陆账号', rowspan: 2, width: IotM.MainGridWidth * 0.08, align: 'center', sortable: false,
                            formatter: function (value, rec, index) {
                                return rec.OperID+ '@' + rec.CompanyID;
                            }
                        },
                        { field: 'OperName', title: '操作员名称', rowspan: 2, width: IotM.MainGridWidth * 0.08, align: 'center', sortable: false },
                        { field: 'LogTime', title: '操作时间', rowspan: 2, width: IotM.MainGridWidth * 0.12, align: 'center', sortable: false },
                        { field: 'LoginIP', title: '登陆IP', rowspan: 2, width: IotM.MainGridWidth * 0.12, align: 'center', sortable: false },
                        { field: 'LoginBrowser', title: '浏览器', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                        { field: 'LoginSystem', title: '操作系统', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                        { field: 'Context', title: '备注', rowspan: 1, width: IotM.MainGridWidth * 0.40, align: 'center', sortable: false, nowrap: false }
                      ]
        ],
        queryParams: {
            TWhere: '',
            Date1: $('#txtDate1').datebox('getValue') + ' ' + $('#txtTime1').timespinner('getValue'),
            Date2: $('#txtDate2').datebox('getValue') + ' ' + $('#txtTime2').timespinner('getValue'),
            LogType: $('#select_LogType').combobox('getValue')
        },
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
IotM.SystemLogManage.SerachClick = function (value, name) {
    var Where = '';
    if (value && name) {
        Where += '   ' + name + ' like \'%' + value + '%\'';
    }
    $('#dataGrid').datagrid('options').queryParams = {
        TWhere: Where,
        Date1: $('#txtDate1').datebox('getValue') + ' ' + $('#txtTime1').timespinner('getValue'),
        Date2: $('#txtDate2').datebox('getValue') + ' ' + $('#txtTime2').timespinner('getValue'),
        LogType: $('#select_LogType').combobox('getValue')
    };
    $('#dataGrid').datagrid('load');
};

