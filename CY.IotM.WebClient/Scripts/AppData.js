var DefuaultUrl = "../Handler/SystemManage/CompanyRightManageHandler.ashx";

ccflow = {}
Application = {}

DataFactory = function () {
    this.data = new ccflow.Data(DefuaultUrl);
    this.common = new ccflow.common();
}

jQuery(function ($) {
    Application = new DataFactory();
});
//公共方法
ccflow.common = function () {
    //sArgName表示要获取哪个参数的值
    this.getArgsFromHref = function (sArgName) {
        var sHref = window.location.href;
        var args = sHref.split("?");
        var retval = "";
        if (args[0] == sHref) /*参数为空*/
        {
            return retval; /*无需做任何处理*/
        }
        var str = args[1];
        args = str.split("&");
        for (var i = 0; i < args.length; i++) {
            str = args[i];
            var arg = str.split("=");
            if (arg.length <= 1) continue;
            if (arg[0] == sArgName) retval = arg[1];
        }
        return retval;
    }
}
//数据访问
ccflow.Data = function (url) {
    this.url = url;

    //获取左侧菜单
    this.getLeftMenu = function (callback, scope) {
        var tUrl = this.url;
        var params = { AType: "LOADNEWLEFTMENU" };
        queryData(tUrl, params, callback, scope);
    }


    //公共方法
    function queryData(url, param, callback, scope, method, showErrMsg) {
        if (!method) method = 'GET';
        $.ajax({
            type: method, //使用GET或POST方法访问后台
            dataType: "text", //返回json格式的数据
            contentType: "application/json; charset=utf-8",
            url: url, //要访问的后台地址
            data: param, //要发送的数据
            async: true,
            cache: false,
            complete: function () { }, //AJAX请求完成时隐藏loading提示
            error: function (XMLHttpRequest, errorThrown) {
                callback(XMLHttpRequest);
            },
            success: function (msg) {//msg为返回的数据，在这里做数据绑定
                var data = msg;
                callback(data, scope);
            }
        });
    }

    //公共方法
    function queryPostData(url, param, callback, scope) {
        $.post(url, param, callback);
    }
}