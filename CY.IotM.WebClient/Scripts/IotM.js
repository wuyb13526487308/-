window.onerror = function () { return true; }
String.prototype.GetValue = function (parm) {
    var reg = new RegExp("(^|&)" + parm + "=([^&]*)(&|$)");
    var r = this.substr(this.indexOf("\?") + 1).match(reg);
    if (r != null) return unescape(r[2]); return null;
}
String.prototype.padLeft = function (totalWidth, paddingChar) {
    if (this.length >= totalWidth || (paddingChar == undefined || paddingChar.length == 0))
        return this;
    else {
        left = "";
        while (left.length < (totalWidth - this.length))
            left += paddingChar.toString();
        return left.toString() + this;
    }
};
String.prototype.padRight = function (totalWidth, paddingChar) {
    if (this.length >= totalWidth || (paddingChar == undefined || paddingChar.length == 0))
        return this;
    else {
        right = "";
        while (right.length < (totalWidth - this.length))
            right += paddingChar.toString();
        return this + right.toString();
    }
};
//此处为string类添加三个成员
String.prototype.Trim = function () { return Trim(this); }
String.prototype.LTrim = function () { return LTrim(this); }
String.prototype.RTrim = function () { return RTrim(this); }

//此处为独立函数
function LTrim(str) {
    var i;
    for (i = 0; i < str.length; i++) {
        if (str.charAt(i) != " " && str.charAt(i) != " ") break;
    }
    str = str.substring(i, str.length);
    return str;
}
function RTrim(str) {
    var i;
    for (i = str.length - 1; i >= 0; i--) {
        if (str.charAt(i) != " " && str.charAt(i) != " ") break;
    }
    str = str.substring(0, i + 1);
    return str;
}
function Trim(str) {
    return LTrim(RTrim(str));
}
/* 得到日期年月日等加数字后的日期 */
Date.prototype.dateAdd = function (interval, number) {
    var d = this;
    var k = { 'y': 'FullYear', 'q': 'Month', 'm': 'Month', 'w': 'Date', 'd': 'Date', 'h': 'Hours', 'n': 'Minutes', 's': 'Seconds', 'ms': 'MilliSeconds' };
    var n = { 'q': 3, 'w': 7 };
    eval('d.set' + k[interval] + '(d.get' + k[interval] + '()+' + ((n[interval] || 1) * number) + ')');
    return d;
}
/* 计算两日期相差的日期年月日等 */
Date.prototype.dateDiff = function (interval, objDate2) {
    var d = this, i = {}, t = d.getTime(), t2 = objDate2.getTime();
    i['y'] = objDate2.getFullYear() - d.getFullYear();
    i['q'] = i['y'] * 4 + Math.floor(objDate2.getMonth() / 4) - Math.floor(d.getMonth() / 4);
    i['m'] = i['y'] * 12 + objDate2.getMonth() - d.getMonth();
    i['ms'] = objDate2.getTime() - d.getTime();
    i['w'] = Math.floor((t2 + 345600000) / (604800000)) - Math.floor((t + 345600000) / (604800000));
    i['d'] = Math.floor(t2 / 86400000) - Math.floor(t / 86400000);
    i['h'] = Math.floor(t2 / 3600000) - Math.floor(t / 3600000);
    i['n'] = Math.floor(t2 / 60000) - Math.floor(t / 60000);
    i['s'] = Math.floor(t2 / 1000) - Math.floor(t / 1000);
    return i[interval];
}
//=========================================================================================================================================
IotM = {
    version: '1.0'
};
IotM.namespace = function () {
    var a = arguments, o = null, i, j, d, rt;
    for (i = 0; i < a.length; ++i) {
        d = a[i].split('.');
        rt = d[0];
        eval('if (typeof ' + rt + ' == "undefined"){' + rt + ' = {};} o = ' + rt + ';');
        for (j = 1; j < d.length; ++j) {
            o[d[j]] = o[d[j]] || {};
            o = o[d[j]];
        }
    }
};
IotM.IsNaN = function (val) {
    if (val.toString().Trim() == "0") {
        return false;
    }
    if (val == null || val == 'undefined' || val == '') {
        return true;
    }
    return isNaN(parseFloat(val.toString()));
}
IotM.NumberFormat = function (val, fixedNum, nanStr) {
    if (IotM.IsNaN(val)) {
        return nanStr;
    }
    else {
        return val.toFixed(fixedNum)
    }
}
IotM.cookie = function (name, value, options) {
    if (typeof value != 'undefined') {
        options = options || {};
        if (value === null) {
            value = '';
            options = $.extend({}, options);
            options.expires = -1;
        }
        var expires = '';
        if (options.expires && (typeof options.expires == 'number' || options.expires.toUTCString)) {
            var date;
            if (typeof options.expires == 'number') {
                date = new Date();
                date.setTime(date.getTime() + (options.expires * 24 * 60 * 60 * 1000));
            } else {
                date = options.expires;
            }
            expires = '; expires=' + date.toUTCString();
        }
        var path = options.path ? '; path=' + (options.path) : '';
        var domain = options.domain ? '; domain=' + (options.domain) : '';
        var secure = options.secure ? '; secure' : '';
        document.cookie = [name, '=', encodeURIComponent(value), expires, path, domain, secure].join('');
    } else {
        var cookieValue = null;
        if (document.cookie && document.cookie != '') {
            var cookies = document.cookie.split(';');
            for (var i = 0; i < cookies.length; i++) {
                var cookie = $.trim(cookies[i]);
                if (cookie.substring(0, name.length + 1) == (name + '=')) {
                    cookieValue = decodeURIComponent(cookie.substring(name.length + 1));
                    break;
                }
            }
        }
        return cookieValue;
    }
};
IotM.MainGridWidth = $(window).width() * 0.99;
IotM.SetMainGridWidth = function (percent) {
    IotM.MainGridWidth = $(window).width() * percent;
}
IotM.MainGridHeight = $(window).height() * 0.99;
IotM.SetMainGridHeight = function (percent) {
    IotM.MainGridHeight = $(window).height() * percent;
}
IotM.loginOperator = []; //当前登录者信息
IotM.pageList = [10, 20, 50, 100, 200];
IotM.windowPageSize = 10;
//注册vilooxvalidatebox验证事件
IotM.regvalidatebox = function (formId) {
    try {
        $('#' + formId + ' input').each(function () {
            if ($(this).attr('required') || $(this).attr('validType'))
                $(this).validatebox();
        })

    } catch (e) {

    }
}
//验证vilooxvalidatebox
IotM.checkisValid = function (formId) {
    var flag = true;
    var index = 0;
    $('#' + formId + ' input').each(function () {
        if ($(this).attr('required') || $(this).attr('validType')) {
            try {
                if (!$(this).validatebox('isValid')) {
                    flag = false;
                    if (index == 0) {
                        $(this).focus();
                    }
                    index++;
                }

            } catch (e) {
            }

        }
        if ($(this).attr('class') == 'combo-text validatebox-text validatebox-invalid') {
            flag = false;
            if (index == 0) {
                $(this).focus();
            }
            index++;
        }
    })
    return flag;
}
//页面重置事件
IotM.FormRedo = function (formId) {
    $("#" + formId).form('clear');
}
//新增获取默认值
IotM.FormSetDefault = function (formID) {
    $('#' + formID + ' input[name^=\'CN\']').each(function () {
        if ($(this).attr('default') != undefined) {
            var value = $.trim($(this).attr('default').toString());
            IotM.SetObjectData(this, value);
        }
    });
    $('#' + formID + ' textarea[name^=\'CN\']').each(function () {
        if ($(this).attr('default') != undefined) {
            var value = $.trim($(this).attr('default').toString());
            IotM.SetObjectData(this, value);
        }
    });
    $("#" + formID + " input[class='easyui-numberbox numberbox-f validatebox-text']").each(function () {
        try {

            if ($(this).attr('default') != undefined) {
                var value = $.trim($(this).attr('default').toString());
                var itemName = $(this).attr('id');
                $('#' + itemName).numberbox('setValue', value);
            }
        } catch (e) {

        }
    });
}
//检测用户是否登录
IotM.CheckLogin = function () {
    $.ajax(
    {
        url: "../Handler/SystemManage/CompanyOperatorManageHandler.ashx?AType=LoadLoginer",
        async: false,
        type: "POST",
        data: {},
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data + ')');
                if (data == null || !data.Result) {
                    document.title = "relogin";
                    window.top.location.href = "../Login.aspx";
                }
                else {
                    data = eval('(' + data.TxtMessage + ')');
                    IotM.loginCompanyOperator = data;
                    var topDivSpan = $("#txtCompany");
                    if (topDivSpan) {
                        topDivSpan.append('<strong>');
                        topDivSpan.text(data.Name + '(' + data.OperID + '@' + data.CompanyID + ')');
                        topDivSpan.append('| <span onclick="IotM.LoginOut()" style="cursor:pointer" >注销</span>');
                        topDivSpan.append('| <span onclick="IotM.CallUs()" style="cursor:pointer">联系我们</span>');
                        topDivSpan.append('</strong>');
                        //加载企业名称
                        var data = {};
                        data.TWhere = "CompanyID='" + IotM.loginCompanyOperator.CompanyID + "'";
                        $.post("../Handler/SystemManage/CompanyManageHandler.ashx?AType=Query",
                             data,
                              function (data, textStatus) {
                                  if (textStatus == 'success') {
                                      data = eval('(' + data.TxtMessage + ')');
                                      if (!data.rows) { return; }
                                      if (!data.rows[0]) { return; }
                                      var companyInfo = data.rows[0];
                                      if (companyInfo) {
                                          if ($("#txtCompanyInfo"))
                                          { $("#txtCompanyInfo").text(companyInfo.CompanyName); }
                                          if ($("#CompanyName")) {
                                              $("#CompanyName").text(companyInfo.CompanyName + " 物联网表管理系统");
                                          }
                                      }
                                  }
                              }, "json");
                    }
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "访问数据中心失败。", 'warn');
        }
    }
  );
};
//获取用户cookie
IotM.WebCookie = "";
IotM.GetCookie = function () {
    $.ajax(
    {
        url: "../Handler/SystemManage/CompanyOperatorManageHandler.ashx?AType=HeartCookie",
        async: false,
        type: "POST",
        data: {},
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data + ')');
                if (!data.Result || data.TxtMessage == '') {
                    IotM.LoginOut();
                }
                else {
                    IotM.WebCookie = data.TxtMessage;
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            IotM.WebCookie = "";
            $.messager.show({
                title: "警告",
                msg: IotM.MyDateTimeformatter(new Date()) + "\n访问数据中心失败。请检查您的网络连接。",
                width: 210,
                height: 150,
                timeout: 5000,
                showType: 'fade'
            });
        }
    }
  );
};
//用户注销登陆
IotM.LoginOut = function () {
    IotM.AddSystemLog({ LogType: 1, Context: '注销系统。' });
    $.ajax(
    {
        url: "../Handler/SystemManage/OperatorLoginManageHandler.ashx?AType=UserLoginOut",
        async: false,
        type: "POST",
        data: {},
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                if (data.Result) {
                    window.top.location.href = "../Login.aspx";
                }
                else {
                    $.messager.alert('警告', data.TxtMessage, 'warn');
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "访问数据中心失败。", 'warn');
        },
        dataType: "json"
    }
  );
};
IotM.CallUs = function () {
    window.open("http://www.zzcyc.cn");
}
IotM.GetData = function (formID) {
    var returnRec = '';
    $('#' + formID + ' input[name^=\'CN\']').each(function () {
        var value = $(this).val();
        var itemName = $(this).attr('name');
        if ($(this).attr('class') == 'combo-value') {
            value = $('#' + itemName).combobox('getValue');
        }
        if ($(this).attr('type') == 'checkbox') {
            value = $(this).attr("checked") == "checked";
        }
        returnRec += ('"' + itemName.replace('CN', '') + '":"' + value + '",');
    });
    $('#' + formID + ' textarea[name^=\'CN\']').each(function () {
        var value = $(this).val();
        //var itemName = $(this).attr('name');
        //if ($(this).attr('class') == 'combo-value') {
        //    value = $('#' + itemName).combobox('getValue');
        //}
        returnRec += ('"' + $(this).attr('name').replace('CN', '') + '":"' + $(this).val() + '",');
    });
    returnRec = '({' + returnRec.substring(0, returnRec.length - 1) + '})';
    return eval(returnRec);
}
IotM.SetObjectData = function (obj, value) {
    try {
        $(obj).val(value);
        if ($(obj).attr('class') == 'combo-value') {
            var itemName = $(obj).attr('name');
            try {
                $('#' + itemName).combobox('select', value);

            } catch (e) {
                $('#' + itemName).datebox('setValue', value);

            }
        }
        if ($(obj).attr('type') == 'checkbox') {
            if (value.toString() == 'true') {
                $(obj).attr("checked", true);
            } else {
                $(obj).removeAttr("checked");
            }
        }
    }
    catch (e) {

    }
};
IotM.SetData = function (formID, rec) {
    $('#' + formID + ' input[name^=\'CN\']').each(function () {
        try {
            var itemName = $(this).attr('name');
            var value = $.trim(rec[itemName.replace('CN', '')].toString());
            IotM.SetObjectData(this, value);

        } catch (e) {


        }

    });
    $('#' + formID + ' textarea[name^=\'CN\']').each(function () {
        try {
            var itemName = $(this).attr('name');
            var value = $.trim(rec[itemName.replace('CN', '')].toString());
            IotM.SetObjectData(this, value);

        } catch (e) {

        }
    });

    $("#" + formID + " input[class='easyui-numberbox numberbox-f validatebox-text']").each(function () {
        try {
            var itemName = $(this).attr('id');
            var value = $.trim(rec[itemName.replace('CN', '')].toString());
            $('#' + itemName).numberbox('setValue', value);

        } catch (e) {

        }
    });

};
IotM.AddRelaseDisabled = function (formID) {
    $('#' + formID + ' input[name^=\'CN\']').each(function () {
        if ($(this).attr('edit') == 'false') {
            $(this).attr('disabled', false);
        }
        if ($(this).attr('class') == 'combo-value') {
            var itemName = $(this).attr('name');
            if ($('#' + itemName).attr('edit') == 'false') {
                try {
                    $('#' + itemName).combobox('enable');

                } catch (e) {
                    $('#' + itemName).datebox('enable');
                }
            }
        }
    });
    $('#' + formID + ' textarea[name^=\'CN\']').each(function () {
        if ($(this).attr('edit') == 'false') {
            $(this).attr('disabled', false);
        }
    });
};
IotM.EditDisabled = function (formID) {
    $('#' + formID + ' input[name^=\'CN\']').each(function () {
        if ($(this).attr('edit') == 'false') {
            $(this).attr('disabled', 'disabled');           
        }
        if ($(this).attr('class') == 'combo-value') {
            var itemName = $(this).attr('name');
            if ($('#'+itemName).attr('edit') == 'false') {
                try {
                    $('#' + itemName).combobox('disable');

                } catch (e) {
                    $('#' + itemName).datebox('disable');
                }
            }          
        }
    });
    $('#' + formID + ' textarea[name^=\'CN\']').each(function () {
        if ($(this).attr('edit') == 'false') {
            $(this).attr('disabled', 'disabled');
        }
    });
};
IotM.ObjectClone = function (sObj) {
    if (typeof sObj !== "object") {
        return sObj;
    }
    var s = {};
    if (sObj.constructor == Array) {
        s = [];
    }
    for (var i in sObj) {
        s[i] = IotM.ObjectClone(sObj[i]);
    }
    return s;
};
IotM.MyDateformatter = function (date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}

IotM.MyDateTimeformatter = function (datetime) {
    var date = new Date(datetime);
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    var h = date.getHours();
    var mm = date.getMinutes();
    var ss = date.getSeconds();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d)
    + ' ' + (h < 10 ? ('0' + h) : h)
    + ':' + (mm < 10 ? ('0' + mm) : mm)
    + ':' + (ss < 10 ? ('0' + ss) : ss);
}
IotM.MyDateparserJson = function (s) {
    if (!s) return new Date();
    return new Date(parseInt(s.replace("/Date(", "").replace(")/", ""), 10));
}
IotM.MyDateparser = function (s) {
    if (!s) return new Date();
    var ss = (s.split('-')).length > 1 ? (s.split('-')) : (s.split('/'));
    var y = parseInt(ss[0], 10);
    var m = parseInt(ss[1], 10);
    var d = parseInt(ss[2], 10);
    if (!isNaN(y) && !isNaN(m) && !isNaN(d)) {
        return new Date(y, m - 1, d);
    } else {
        return new Date();
    }
}
//由秒转换为时间显示串
IotM.MyTimeFormatter = function (ss) {
    if (ss < 60) {
        return ss.toString() + "秒";
    }
    if (ss < 60 * 60) {
        return Math.floor((ss / 60)).toString() + "分钟" + (ss % 60).toFixed(0) + "秒";
    }
    if (ss < 60 * 60 * 24) {
        return Math.floor(ss / (60 * 60)).toString() + "小时"
        + Math.floor(ss % (60 * 60) / 60).toString() + "分钟";
    }
    return Math.floor(ss / (60 * 60 * 24)).toString() + "天"
+ Math.floor(ss % (60 * 60 * 24) / (60 * 60)).toString() + "小时";
}
IotM.DateTimeCompare = function (s1, s2) {
    var start = new Date(s1.replace("-", "/").replace("-", "/"));
    var end = new Date(s2.replace("-", "/").replace("-", "/"));
    if (end < start) {
        return false;
    }
    return true;

}
IotM.WindowResize = function () {
    $(window).resize();
}
IotM.DataGridMergeCells = function (tableID, colList, key) {
    var ColArray = colList.split(",");
    var tTable = $('#' + tableID);
    var TableRowCnts = tTable.datagrid("getRows").length;
    var tmpA;
    var PerKeyTxt;
    var CurKeyTxt;
    //从第一行（表头为第0行）开始循环，循环至行尾(溢出一位)
    for (i = 0; i <= TableRowCnts; i++) {
        if (i == 0) {
            PerKeyTxt = tTable.datagrid("getRows")[i][key];
            CurKeyTxt = tTable.datagrid("getRows")[i][key];
            tmpA = 0;
        }
        else {
            if (i == TableRowCnts) {
                CurKeyTxt = '';
            }
            else {
                CurKeyTxt = tTable.datagrid("getRows")[i][key];
            }
            if (PerKeyTxt == CurKeyTxt) {
                continue;
            }
            else {

                $.each(ColArray, function () {
                    tTable.datagrid('mergeCells', {
                        index: tmpA,
                        field: this,
                        rowspan: i - tmpA,
                        colspan: null
                    });
                });
                tmpA = i;
                PerKeyTxt = CurKeyTxt;
            }
        }
    }
}
IotM.ShowWarnMessage = function (title, msg, timeout, id) {
    var showTime = 5000;
    if (timeout != null && timeout != undefined && !isNaN(timeout)) {
        showTime = timeout;
    }
    $.messager.show({
        title: title,
        id: id,
        msg: msg,
        width: 220,
        height: 190,
        doSize:true,
        timeout: showTime,
        showType: 'fade'
    });

}
IotM.GetUrlParmter = function (parm) {
    var url = location.href;
    var name = url.GetValue(parm);
    return name;
}
IotM.CheckMenuCode = function () {
    try {
        var hidMenuCode = $(window.parent.parent.document).find('#hidMenuCode').val();
        if(!hidMenuCode){
            hidMenuCode = $(window.parent.parent.parent.document).find('#hidMenuCode').val();
        }

        $('a').each(function () {
            var menucode = $(this).attr('menucode');
            if (menucode) {
                //判断是否有相关权限
                if (hidMenuCode.indexOf(',' + menucode + ',') == -1) {
                    var tmpa = $(this)[0];
                    tmpa.disabled = true;
                    tmpa.onclick = function () { return false; }
                    $(this).css("cursor", "default");
                    $(this).css("color", "#CCCCCC");
                }
            }
        });
        $('input[type=checkbox]').each(function () {
            var menucode = $(this).attr('menucode');
            if (menucode) {
                //判断是否有相关权限
                if (hidMenuCode.indexOf(',' + menucode + ',') == -1) {
                    var tmpa = $(this)[0];
                    tmpa.disabled = true;
                    tmpa.onclick = function () { return false; }
                }
            }
        });

    } catch (e) {

    }

}
IotM.DownLoadIframe;
IotM.DownLoad = function (url) {
    if (typeof (IotM.DownLoadIframe) == "undefined") {
        var iframe = document.createElement("iframe");
        IotM.DownLoadIframe = iframe;
        document.body.appendChild(IotM.DownLoadIframe);
    }
    IotM.DownLoadIframe.src = url;
    IotM.DownLoadIframe.style.display = "none";
}
IotM.AddSystemLog = function (logData) {
    var result = false;
    $.ajax({
        url: "../Handler/SystemManage/SystemLogManageHandler.ashx?AType=Add",
        async: false,
        type: "POST",
        data: logData,
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data + ')');
                if (!data.Result) {
                    result = false;
                    $.messager.alert('警告', '记录日志失败，'+data.TxtMessage, 'warn');
                }
                else {
                    result = true;
                }

            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "访问数据中心失败。", 'warn');
        }
    }
);
    return result;
}
IotM.LoadComboBox = function (data,id, withAll, required,width) {
    if (withAll) {
        data.push({ TypeID: '', TypeName: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].TypeID,
        valueField: 'TypeID',
        textField: 'TypeName',
        width: width,
        required: required,
        editable: false
    }
    );
};
IotM.browser = {
    versions: function () {
        var u = navigator.userAgent, app = navigator.appVersion;
        return {
            trident: u.indexOf('Trident') > -1, //IE内核               
            presto: u.indexOf('Presto') > -1, //opera内核               
            webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核               
            gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1, //火狐内核
            mobile: !!u.match(/AppleWebKit.*Mobile/) || !!u.match(/Windows Phone/) || !!u.match(/Android/) || !!u.match(/MQQBrowser/),
            ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端               
            android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器               
            iPhone: u.indexOf('iPhone') > -1 || u.indexOf('Mac') > -1, //是否为iPhone或者QQHD浏览器               
            iPad: u.indexOf('iPad') > -1, //是否iPad               
            webApp: u.indexOf('Safari') == -1 //是否web应该程序，没有头部与底部           
        };
    }()
}








