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



Olb = {
    version: '1.0'
};
Olb.namespace = function () {
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

Olb.CheckLogin = function () {
    $.ajax(
    {
        url: "../Handler/UserManage.ashx?AType=LoadLoginer",
        async: false,
        type: "POST",
        data: {},
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data + ')');
                if (data == null || !data.Result) {
                    window.top.location.href = "../Logon.aspx";
                }
                else {
                    data = eval('(' + data.TxtMessage + ')');
                    Olb.loginCompanyOperator = data;
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
Olb.WebCookie = "";

//用户注销登陆
Olb.LoginOut = function () {
  
    $.ajax(
    {
        url: "../Handler/LogonManage.ashx?AType=UserLoginOut",
        async: false,
        type: "POST",
        data: {},
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                if (data.Result) {
                    window.top.location.href = "../Logon.aspx";
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



Olb.ObjectClone = function (sObj) {
    if (typeof sObj !== "object") {
        return sObj;
    }
    var s = {};
    if (sObj.constructor == Array) {
        s = [];
    }
    for (var i in sObj) {
        s[i] = Olb.ObjectClone(sObj[i]);
    }
    return s;
};



Olb.MyDateformatter = function (date) {
    var y = date.getFullYear();
    var m = date.getMonth() + 1;
    var d = date.getDate();
    return y + '-' + (m < 10 ? ('0' + m) : m) + '-' + (d < 10 ? ('0' + d) : d);
}

Olb.MyDateTimeformatter = function (datetime) {
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
Olb.MyDateparserJson = function (s) {
    if (!s) return new Date();
    return new Date(parseInt(s.replace("/Date(", "").replace(")/", ""), 10));
}
Olb.MyDateparser = function (s) {
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
Olb.MyTimeFormatter = function (ss) {
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
Olb.DateTimeCompare = function (s1, s2) {
    var start = new Date(s1.replace("-", "/").replace("-", "/"));
    var end = new Date(s2.replace("-", "/").replace("-", "/"));
    if (end < start) {
        return false;
    }
    return true;

}


//=================================================================

/*燃气用户列表*/
Olb.GasUserList = [];
Olb.InitGasUserList = function () {
    $.ajax({
        url: "../Handler/GasUserManage.ashx?AType=GetGasUserList",
        async: false,
        type: "POST",
        success: function (data, textStatus) {
            if (textStatus == 'success') {
                data = eval('(' + data.TxtMessage + ')');
                if (data != null && data.total > 0) {
                    Olb.GasUserList = data.rows;
                } else {
                    Olb.GasUserList = [{ MenuCode: '', Name: '' }];
                }
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            $.messager.alert('警告', "获取用户失败。", 'warn');
        },
        dataType: "json"
    }
   );
};
Olb.LoadGasUserComboBox = function (id, withAll, required) {
    Olb.InitGasUserList();
    var data = Olb.ObjectClone(Olb.GasUserList);
    if (withAll) {
        data.push({ MenuCode: '', Name: '全部' });
    }
    $('#' + id).combobox(
    {
        data: data,
        value: withAll ? '' : data[0].UserID,
        valueField: 'UserID',
        textField: 'UserName',
        required: required,
        editable: false
    }
    );
};








