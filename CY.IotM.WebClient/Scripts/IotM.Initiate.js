//页面初始化及初始化参数js
IotM.namespace('IotM.Initiate');

/*性别选项开始*/
IotM.Initiate.Sexs = [{ Name: '男', Value: '0' }, { Name: '女', Value: '1' }, { Name: '保密', Value: '2' }];
IotM.Initiate.LoadSexsComboBox = function (id, withAll, required) {
    var data = IotM.ObjectClone(IotM.Initiate.Sexs);
    if (withAll) {
        data.push({ Value: '', Name: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].Value,
        valueField: 'Value',
        textField: 'Name',
        width: 105,
        required: required,
        editable: false
    }
    );
};



/*企业操作员状态开始*/
IotM.Initiate.CompanyOperatorStates = [{ Name: '正常', Value: '0' }, { Name: '已停用', Value: '1' }];
IotM.Initiate.LoadCompanyOperatorStateComboBox = function (id, withAll, required) {
    var data = IotM.ObjectClone(IotM.Initiate.CompanyOperatorStates);
    if (withAll) {
        data.push({ Value: '', Name: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].Value,
        valueField: 'Value',
        textField: 'Name',
        width: 105,
        required: required,
        editable: false
    }
    );
};



/*日志类别*/
IotM.Initiate.SystemLogTypes =
[
{ TypeID: '0', TypeName: '登陆' },
{ TypeID: '1', TypeName: '注销' },
{ TypeID: '2', TypeName: '增加权限组' },
{ TypeID: '3', TypeName: '修改权限组' },
{ TypeID: '4', TypeName: '删除权限组' },
{ TypeID: '5', TypeName: '分配权限组' },
{ TypeID: '99', TypeName: '其他' }
];
IotM.Initiate.LoadSystemLogTypeComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.SystemLogTypes, id, withAll, required, 105);
};




/*菜单类型*/
IotM.Initiate.MenuTypes =
[
{ TypeID: '00', TypeName: '一级菜单' },
{ TypeID: '01', TypeName: '二级菜单' },
{ TypeID: '02', TypeName: '按钮菜单' },
{ TypeID: '03', TypeName: '报表菜单' }
];
IotM.Initiate.LoadMenuTypeComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.MenuTypes, id, withAll, required, 105);
};





/*一级菜单*/
IotM.Initiate.FatherCodeFirst = [];
IotM.Initiate.InitFatherCodeFirst = function () {
    $.ajax({
        url: "../Handler/SystemManage/MenuManageHandler.ashx?AType=QUERYFATHER&MenuType=00",
        async: false,
        type: "POST",
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data.TxtMessage + ')');
                if (data != null && data.total > 0) {
                    IotM.Initiate.FatherCodeFirst = data.rows;
                } else {
                    IotM.Initiate.FatherCodeFirst = [{ MenuCode: '', Name: '' }];
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "获取菜单失败。", 'warn');
        },
        dataType: "json"
    }
   );
};
IotM.Initiate.LoadFatherCodeFirstComboBox = function (id, withAll, required) {
    IotM.Initiate.InitFatherCodeFirst();
    var data = IotM.ObjectClone(IotM.Initiate.FatherCodeFirst);
    if (withAll) {
        data.push({ MenuCode: '', Name: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].MenuCode,
        valueField: 'MenuCode',
        textField: 'Name',
      
        required: required,
        editable: false

    }
    );
};



/*二级菜单*/
IotM.Initiate.FatherCodeSecond = [];
IotM.Initiate.InitFatherCodeSecond = function () {
    $.ajax({
        url: "../Handler/SystemManage/MenuManageHandler.ashx?AType=QUERYFATHER&MenuType=01",
        async: false,
        type: "POST",
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data.TxtMessage + ')');
                if (data != null && data.total > 0) {
                    IotM.Initiate.FatherCodeSecond = data.rows;
                } else {
                    IotM.Initiate.FatherCodeSecond = [{ MenuCode: '', Name: '' }];
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "获取菜单失败。", 'warn');
        },
        dataType: "json"
    }
   );
};
IotM.Initiate.LoadFatherCodeSecondComboBox = function (id, withAll, required) {
    IotM.Initiate.InitFatherCodeSecond();
    var data = IotM.ObjectClone(IotM.Initiate.FatherCodeSecond);
    if (withAll) {
        data.push({ MenuCode: '', Name: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].MenuCode,
        valueField: 'MenuCode',
        textField: 'Name',
      
        required: required,
        editable: false

    }
    );
};


/*报表模板*/
IotM.Initiate.ReportTemplate = [];
IotM.Initiate.InitReportTemplate = function () {
    $.ajax({
        url: "../Handler/Report/ReportManageHandler.ashx?AType=QUERY",
        async: false,
        type: "POST",
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data.TxtMessage + ')');
                if (data != null && data.total > 0) {
                    IotM.Initiate.ReportTemplate = data.rows;
                } else {
                    IotM.Initiate.ReportTemplate = [{ RID: '', ReportName: '' }];
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "获取菜单失败。", 'warn');
        },
        dataType: "json"
    }
   );
};
IotM.Initiate.LoadReportTemplateComboBox = function (id, withAll, required) {
    IotM.Initiate.InitReportTemplate();
    var data = IotM.ObjectClone(IotM.Initiate.ReportTemplate);
    if (withAll) {
        data.push({ RID: '', ReportName: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].RID,
        valueField: 'RID',
        textField: 'ReportName',
        required: required,
        editable: false

    }
    );
};





/*街道列表*/
IotM.Initiate.Street = [];
IotM.Initiate.InitStreet = function () {
    $.ajax({
        url: "../Handler/StreetHandler.ashx?AType=QUERY",
        async: false,
        type: "POST",
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data.TxtMessage + ')');
                if (data != null && data.total > 0) {
                    IotM.Initiate.Street = data.rows;
                } else {
                    IotM.Initiate.Street = [{ ID: '', Name: '' }];
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "获取街道失败。", 'warn');
        },
        dataType: "json"
    }
   );
};
IotM.Initiate.LoadStreetComboBox = function (id, withAll, required) {
    IotM.Initiate.InitStreet();
    var data = IotM.ObjectClone(IotM.Initiate.Street);
    if (withAll) {
        data.push({ ID: '', Name: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].ID,
        valueField: 'ID',
        textField: 'Name',
        required: required,
        editable: false

    }
    );
};




/*小区列表*/
IotM.Initiate.Community = [];
IotM.Initiate.InitCommunity = function (streetId) {
    $.ajax({
        url: "../Handler/CommunityHandler.ashx?AType=QUERY&StreetID=" + streetId,
        async: false,
        type: "POST",
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data.TxtMessage + ')');
                if (data != null && data.total > 0) {
                    IotM.Initiate.Community = data.rows;
                } else {
                    IotM.Initiate.Community = [{ ID: '', Name: '' }];
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "获取小区失败。", 'warn');
        },
        dataType: "json"
    }
   );
};
IotM.Initiate.LoadCommunityComboBox = function (id, withAll, required,streetId) {
    IotM.Initiate.InitCommunity(streetId);
    var data = IotM.ObjectClone(IotM.Initiate.Community);
    if (withAll) {
        data.push({ ID: '', Name: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].ID,
        valueField: 'ID',
        textField: 'Name',
        required: required,
        editable: false

    }
    );
};




/*结算周期*/
IotM.Initiate.SettlementType =
[
{ TypeID: '00', TypeName: '按月' },
{ TypeID: '01', TypeName: '按季度' },
{ TypeID: '10', TypeName: '按半年' },
{ TypeID: '11', TypeName: '按全年' }
];
IotM.Initiate.LoadSettlementTypeComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.SettlementType, id, withAll, required, 105);
};



/*进气方向*/
IotM.Initiate.GasDirection =
[
{ TypeID: '左', TypeName: '左进气' },
{ TypeID: '右', TypeName: '右进气' },

];
IotM.Initiate.LoadGasDirectionComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.GasDirection, id, withAll, required, 105);
};

/*换表状态*/
IotM.Initiate.HuanBiao =
[
{ TypeID: '1', TypeName: '换表申请' },
{ TypeID: '2', TypeName: '换表登记' },
{ TypeID: '3', TypeName: '换表完成' },
{ TypeID: '4', TypeName: '撤销' },

];
IotM.Initiate.LoadHuanBiaoComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.HuanBiao, id, withAll, required, 105);
};




/*表具类型*/
IotM.Initiate.MeterType =
[
   { TypeID: '01', TypeName: '金额表' },
   { TypeID: '00', TypeName: '气量表' }
];
IotM.Initiate.LoadMeterTypeComboBox = function (id, withAll, required) {
    var data = IotM.ObjectClone(IotM.Initiate.MeterType);
    IotM.LoadComboBox(data, id, withAll, required, 105);
};




/*户向类型*/
IotM.Initiate.UserDirection =
[
{ TypeID: '东', TypeName: '东' },
{ TypeID: '西', TypeName: '西' },
{ TypeID: '南', TypeName: '南' },
{ TypeID: '北', TypeName: '北' },
{ TypeID: '中', TypeName: '中' },
{ TypeID: '东南', TypeName: '东南' },
{ TypeID: '西南', TypeName: '西南' },
{ TypeID: '中南', TypeName: '中南' },
{ TypeID: '东北', TypeName: '东北' },
{ TypeID: '西北', TypeName: '西北' },
{ TypeID: '中北', TypeName: '中北' }

];
IotM.Initiate.LoadUserDirectionComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.UserDirection, id, withAll, required, 320);
};


/* 用户状态*/
IotM.Initiate.UserState =
    [
   // { TypeID: '0', TypeName: '等待安装' },
    { TypeID: '1', TypeName: '等待点火' },
    { TypeID: '2', TypeName: '撤销点火' },
    { TypeID: '3', TypeName: '正常使用' }
   
    ];
IotM.Initiate.LoadUserStateComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.UserState, id, withAll, required, 105);
}



//阀门状态
IotM.Initiate.FaMenStatus =
[
{ TypeID: '0', TypeName: '申请' },
{ TypeID: '1', TypeName: '撤销' },
{ TypeID: '2', TypeName: '完成' },
{ TypeID: '3', TypeName: '失败' }

];
IotM.Initiate.LoadFaMenStatusComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.FaMenStatus, id, withAll, required, 105);
};

//阀门操作类型
IotM.Initiate.FaMenControlType = 
[
{ TypeID: '0', TypeName: '关阀' },
{ TypeID: '1', TypeName: '开阀' }

];
IotM.Initiate.LoadFaMenControlTypeComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.FaMenControlType, id, withAll, required, 105);
};

/*价格类别*/
IotM.Initiate.PriceType = [];
IotM.Initiate.InitPriceType = function () {
    $.ajax({
        url: "../Handler/PriceParHandler.ashx?AType=QUERY",
        async: false,
        type: "POST",
        success: function (data, textStatus) {
          
            if (textStatus == 'success') {
   
                data = eval('(' + data.TxtMessage + ')');
                if (data != null && data.total > 0) {
                    IotM.Initiate.PriceType = data.rows;
                } else {
                    IotM.Initiate.PriceType = [{ ID: '', PriceName: '' }];
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "获取价格类别失败。", 'warn');
        },
        dataType: "json"
    }
   );
};
IotM.Initiate.LoadPriceTypeComboBox = function (id, withAll, required) {
    IotM.Initiate.InitPriceType();
    var data = IotM.ObjectClone(IotM.Initiate.PriceType);
    if (withAll) {
        data.push({ ID: '', Name: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].ID,
        valueField: 'ID',
        textField: 'PriceName',
        required: required,
        editable: false

    }
    );
};

//价格类型列表
IotM.Initiate.LoadComboxGridPriceType = function (id,objrequired, objdisabled) {
    $('#' + id).combogrid("clear");
    $('#' + id).combogrid({
        panelWidth: 900,
        idField: 'ID',
        textField: 'PriceName',
        url: '../Handler/PriceParHandler.ashx?AType=QUERY',
        method: 'get',
        required: objrequired == true ? true : false,
        editable: false,
        disabled: objdisabled,
        columns: [[
            { field: 'Ser', title: '编号', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                 { field: 'PriceName', title: '价格类型名称', rowspan: 2, width: IotM.MainGridWidth * 0.15, align: 'center', sortable: false },

                 {
                     field: 'IsUsed', title: '启用阶梯价', rowspan: 2, width: IotM.MainGridWidth * 0.12, align: 'center', sortable: false,

                     formatter: function (value, rec, index) {
                         if (value == '0') { return '未启用'; }
                         else if (value == '1') { return '启用'; }
                         else { return '未知'; }
                     }

                 },
                 {
                     field: 'SettlementType', title: '结算周期', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false,
                     formatter: function (value, rec, index) {

                         if (value == '00') { return '按月'; }
                         else if (value == '01') { return '按季度'; }
                         else if (value == '10') { return '按半年'; }
                         else if (value == '11') { return '按全年'; }
                         else { return '未知'; }
                     }

                 },
                 { field: 'SettlementDay', title: '结算日', rowspan: 2, width: IotM.MainGridWidth * 0.08, align: 'center', sortable: false },

                 { field: 'Ladder', title: '阶梯数', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                 { field: 'Price1', title: '价格1', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                 { field: 'Gas1', title: '用量1', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                 { field: 'Price2', title: '价格2', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                 { field: 'Gas2', title: '用量2', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                 { field: 'Price3', title: '价格3', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                 { field: 'Gas3', title: '用量3', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                 { field: 'Price4', title: '价格4', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                 { field: 'Gas4', title: '用量4', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false },
                 { field: 'Price5', title: '价格5', rowspan: 2, width: IotM.MainGridWidth * 0.1, align: 'center', sortable: false }

        ]],
        fitColumns: true
    });
};





//上传周期
IotM.Initiate.JieSuanZhouQiType =
[
{ TypeID: '00', TypeName: '月周期' },
{ TypeID: '01', TypeName: '日周期' },
{ TypeID: '02', TypeName: '时周期' },
{ TypeID: '03', TypeName: '分钟周期' }
];
IotM.Initiate.LoadJieSuanZhouQiTypeComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.JieSuanZhouQiType, id, withAll, required, 105);
};
//充值方式
IotM.Initiate.ChongZhiType =
[
{ TypeID: '0', TypeName: '本地营业厅' },
{ TypeID: '1', TypeName: '接口' },
{ TypeID: '2', TypeName: '本地网站' },
{ TypeID: '3', TypeName: '换表补充' }
];
IotM.Initiate.LoadChongZhiTypeComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.ChongZhiType, id, withAll, required, 105);
};
//统计类型
IotM.Initiate.CZTongJiType =
[
{ TypeID: '0', TypeName: '依据用户' },
{ TypeID: '1', TypeName: '依据充值方式' },
{ TypeID: '2', TypeName: '依据操作员' }
];
IotM.Initiate.LoadCZTongJiTypeComboBox = function (id, withAll, required) {
    IotM.LoadComboBox(IotM.Initiate.CZTongJiType, id, withAll, required, 105);
};




//广告发布状态
IotM.Initiate.AdPublishState =
[
{ TypeID: '0', TypeName: '未发布' },
{ TypeID: '1', TypeName: '已发布' },
{ TypeID: '2', TypeName: '发布中' },
{ TypeID: '3', TypeName: '重新发布' }
];
IotM.Initiate.LoadAdPublishStateComboBox = function (id, withAll, required) {
    var data =  IotM.ObjectClone(IotM.Initiate.AdPublishState);
    IotM.LoadComboBox(data, id, withAll, required, 105);
};

//广告显示状态
IotM.Initiate.AdShowState =
[
    { TypeID: '1', TypeName: '显示' },
    { TypeID: '0', TypeName: '不显示' }
];
IotM.Initiate.LoadAdShowStateComboBox = function (id, withAll, required) {
    var data =  IotM.ObjectClone(IotM.Initiate.AdShowState);
    IotM.LoadComboBox(data, id, withAll, required, 105);
};


IotM.Initiate.AlermItem = [
    { TypeID: '报警', TypeName: '报警' },
    { TypeID: '电源状态', TypeName: '电源状态' },
    { TypeID: '', TypeName: '全部' }
];
IotM.Initiate.LoadAlermItemComboBox = function (id, withAll, required) {
    var data = IotM.ObjectClone(IotM.Initiate.AlermItem);
    IotM.LoadComboBox(data, id, withAll, required, 105);
};