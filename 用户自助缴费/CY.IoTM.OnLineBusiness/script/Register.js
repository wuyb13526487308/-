var Register = {};
Register.phone = {};
Register.email = {};
Register.passw = {};
Register.code = {};
Register.agree = {};

////检验是否是手机号
//Register.phone.check = function () {
//    var elementId = "Li_Phone";
//    var s = false;
//    Register.clear(elementId);
//    deleteisok(elementId);
//    var value = $.trim($("#" + elementId).val());
//    (value == "" && function () {
//        Register.show(elementId, "请输入手机号码");
//        return true;
//    }()) || (Register.phone.isPhone(value) && function () {
//        Register.show(elementId, "手机号码格式不正确,请重新输入。");
//        s = false;
//        return true;
//    }()) || function () { s = Register.phone.isHave(elementId) }();
//    return s;
//}

//Register.phone.isPhone = function (value) {
//    if ((/^(13|15|18|17)[0-9]{9}$/.test(value)))
//        return false;
//    else
//        return true;
//}
////验证是否在系统中是否存在
//Register.phone.isHave = function (elementId) {
//    var cName = $("#" + elementId).val();
//    var result = false;
//    $.ajax({
//        url: "/Account/checkNewRegister",
//        type: "POST",
//        cache: false,
//        async: false,
//        data: { "Cname": cName },
//        success: function (ret) {
//            if (ret.Data == "ok") {
//                isok(elementId);
//                result = true;
//            } else {
//                if (elementId == "Li_Phone") {
//                    Register.show(elementId, "该手机号已被注册,请更换其它手机号,或使用该手机号<a href='/account/logon' style='color:#1aa7c5'>登录</a>。");
//                } else if (elementId == "i_Email") {
//                    Register.show(elementId, "该邮箱已被注册,请更换其它邮箱,或使用该邮箱<a href='/account/logon'style='color:#1aa7c5'>登录</a>。");
//                }
//                return false;
//            }
//        }
//    });
//    return result;
//}


//检验密码
Register.passw.checkPassw = function (objId) {
    Register.clear(objId);
    deleteisok(objId);
    var Passw = $("#" + objId).val();
    var passstr = "<a class='yellow'>弱</a><a class='gray'>中</a><a class='gray'>强</a>";
    if (Passw == undefined || Passw == null || Passw == "") {
        Register.show(objId, "密码为6-20个字符,建议由字母、数字、符号两种以上组合。");
        return false;
    }

    if (Passw.length < 6 || Passw.length > 20) {
        Register.show(objId, "密码长度为6-20个字符，请重新输入。");
        return false;
    }
    if (Register.passw.checkallnum(Passw)) {
        Register.show(objId, "密码不能为纯数字，请重新输入。");
        return false;
    }
    var returnstr = "";
    switch (Register.passw.checkStrong(Passw)) {
        case 0:
            Register.show(objId, "密码长度6-20个字符,请重新输入。");
            break;
        case 1:
            returnstr = "<div class='wrm_txt' name='mesInfo'><p><small>安全程度</small><a class='yellow'>弱</a><a class='gray'>中</a><a class='gray'>强</a></p></div>";
            isok(objId); break;
        case 2:
            returnstr = "<div class='wrm_txt' name='mesInfo'><p><small>安全程度</small><a class='gray'>弱</a><a class='yellow'>中</a><a class='gray'>强</a></p></div>";
            isok(objId); break;
        case 3:
            returnstr = "<div class='wrm_txt' name='mesInfo'><p><small>安全程度</small><a class='gray'>弱</a><a class='gray'>中</a><a class='yellow'>强</a></p></div>";
            isok(objId); break;
        default: returnstr = "<div class='wrm_txt' name='mesInfo'><h3><small>密码为6-20个字符,建议由字母、数字、符号两种以上组合。</small></h3></div>";
    }

    $("#" + objId).parent().after(returnstr);
    return true;
}

//检验密码是否一样
Register.passw.checkPasswSame = function (elementId1, elementId2) {
    Register.clear(elementId2);
    deleteisok(elementId2);
    var Password = $("#" + elementId1).val();
    var i_Password = $("#" + elementId2).val();
    if (Password != null && Password != undefined && Password != "" && Password != i_Password) {
        Register.show(elementId2, "密码不一致，请重新输入。");
        return false;
    } else if (i_Password != "") {
        isok(elementId2);
        return true;
    }
}


//密码的健壮性
Register.passw.checkStrong = function (val) {

    if (val.length < 6 || Register.passw.checkallnum(val) || val.length > 20)
        return 0; //密码太短 
    var index = 0;
    val == null || val.match(/[a-z]/g) && function () {
        index++;
    }()
    val == null || val.match(/[0-9]/g) && function () {
        index++;
    }()
    val == null || val.match(/(.[^a-z0-9])/g) && function () {
        index++;
    }()

    return index;






    //if (sPW.length < 6 || Register.passw.checkallnum(sPW) || sPW.length > 20)
    //    return 0; //密码太短 
    //Modes = 0;
    //for (i = 0; i < sPW.length; i++) {
    //    //测试每一个字符的类别并统计一共有多少种模式. 
    //    Modes |= Register.passw.CharMode(sPW.charCodeAt(i));
    //}
    //return Register.passw.bitTotal(Modes);
}

//判断是否是全数字
Register.passw.checkallnum = function (str) {
    var patrn = /^\d+$/;
    return patrn.test(str);
}

///校验密码强度 
//测试某个字符是属于哪一类. 
Register.passw.CharMode = function (iN) {
    if (iN >= 48 && iN <= 57) //数字 
        return 1;
    if (iN >= 65 && iN <= 90) //大写字母 
        return 2;
    if (iN >= 97 && iN <= 122) //小写 
        return 4;
    else
        return 8; //特殊字符 
}

Register.passw.bitTotal = function (num) {
    modes = 0;
    for (i = 0; i < 4; i++) {
        if (num & 1) modes++;
        num >>>= 1;
    }
    return modes;
}

//Register.phone.subPhone = function () {


//    if (Register.phone.check() && Register.passw.checkPassw('i_Password') && Register.passw.checkPasswSame('i_Password', 'i_ConfirmPassword') && Register.code.check('i_PhoneCode') && Register.agree.check('agreePhoneok')) {
//        var name = $("#Li_Phone").val();
//        var i_Password = $("#i_Password").val();
//        var Password = $("#i_ConfirmPassword").val();
//        var iPhoneCode = $("#i_PhoneCode").val();

//        $.ajax({
//            url: "/Account/NewRegister",
//            type: "POST",
//            cache: false,
//            async: false,
//            data: { "Name": name, "Password": i_Password, "ConfirmPassword": Password, "Li_Phone": name, "Code": iPhoneCode, "hidType": 1 },
//            success: function (ret) {
//                if (ret.success != true) {
//                    Register.show(ret.obj, ret.mess);
//                } else {
//                    if ($("#hidurl").val() != undefined && $("#hidurl").val() != "") {
//                        window.location.href = $("#hidurl").val();
//                    } else {
//                        window.location.href = "/Account/NewOk"
//                    }
//                }
//            }
//        })
//    }

//}

Register.code.check = function (objID) {
    Register.clear(objID);
    var cName = $("#" + objID).val();
    if (cName == undefined || cName == null || $.trim(cName) == "") {
        Register.show(objID, "请输入验证码");
        //$("#btn_Phone").removeAttr("disabled");
        //$("#btn_Email").removeAttr("disabled");
        return false;
    } else {
        var result = false;
        $.ajax({
            url: "Handler/LogonManage.ashx?Atype=checkCode",
            type: "POST",
            cache: false,
            async: false,
            dataType: "json",
            data: { "Code": cName },
            success: function (ret) {
                if (ret.Result) {
                    isok(objID);
                    result = true;
                } else {
                    Register.show(objID, "验证码错误，请重新输入。");
                    //$("#btn_Phone").removeAttr("disabled");
                    //$("#btn_Email").removeAttr("disabled");
                    result = false;
                }
            }
        })
        return result;
    }
}

Register.agree.check = function (objid) {
    Register.clear(objid);
    if (!$("#" + objid).is(":checked")) {
        Register.show(objid, "您需要查看并同意以下协议。");
        return false;
    }
    return true;
}


Register.show = function (objid, mes) {
    $("#" + objid).parent().css("border-color", "#f00");
    if (objid == 'i_PhoneCode' || objid == "i_picCode") {
        $("#" + objid).parent().next().after("<div class='wrm_txt' name='mesInfo' ><h3><small>" + mes + " &nbsp;&nbsp;</small></h3></div>");
    }
    else {
        $("#" + objid).parent().after("<div class='wrm_txt' name='mesInfo' ><h3><small>" + mes + " &nbsp;&nbsp;</small></h3></div>");
    }
    return false;
}

Register.clear = function (objid) {
    $("#" + objid).parent().css("border-color", "#dadada");
    if (objid == 'i_PhoneCode' || objid == 'i_picCode') {
        $("#" + objid).parent().parent().children('div[name="mesInfo"]').remove();
    } else {
        $("#" + objid).parent().parent().children('.wrm_txt').remove();
    }
}

//Register.phone.sendPhoneCode = function () {
//    Register.clear('Li_Phone');
//    if (Register.phone.check('Li_Phone')) {
//        $.ajax({
//            url: "/Account/SendPhoneCodeNew",
//            type: "POST",
//            cache: false,
//            async: false,
//            data: { "mobilePhone": $("#Li_Phone").val() },
//            success: function (ret) {
//                if (ret == "ok") {
//                    Register.phone.chengeCode(); //首次调用add函数
//                } else {
//                    if (!isNaN(ret)) {
//                        timerc = 60 - ret;
//                        if (ret > 60) {
//                            Register.phone.sendPhoneCode();
//                        } else {
//                            alert("您还需要" + timerc + "秒后才能发送");
//                            Register.phone.chengeCode();
//                        }
//                    } else {
//                        alert("" + ret);
//                    }
//                }
//            }
//        })
//    }
//}

//var timerc = 60; //全局时间变量（秒数）
//Register.phone.chengeCode = function () {
//    $("#i_btnCode").attr("disabled", "disabled");
//    timerc = parseInt(timerc) - 1; //时间变量自增1
//    if (timerc < 1) {
//        timerc = 60;
//        $("#i_btnCode").removeAttr("disabled");
//        $("#i_btnCode").html("重新发送"); //写入秒数（两位）
//    } else {
//        if (0 < timerc < 60) { //如果不到5分钟
//            $("#i_btnCode").html("重新发送(" + Number(parseInt(timerc % 60 / 10)).toString() + (timerc % 10) + ")"); //写入秒数（两位）
//            setTimeout("Register.phone.chengeCode()", 1000); //设置1000毫秒以后执行一次本函数
//        } else {
//            timerc = 60;
//            $("#i_btnCode").html("重新发送"); //写入秒数（两位）
//            $("#i_btnCode").removeAttr("disabled");
//        }
//    }
//}

Register.email.check = function () {

    var objId = "i_Email";
    Register.clear(objId);
    deleteisok(objId);
    var str = $("#" + objId).val();
    var re = /^[a-z0-9]+[\._-]?[a-z0-9]+@[a-z0-9]+-?[a-z0-9]*(\.[a-z0-9]+-?[a-z0-9]+)?\.(com|cn)$/i
    //  var re = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/
    if (str == "") {
        Register.show(objId, "请填写注册邮箱");
        return false;
    }
    if (!re.test(str)) {
        Register.show(objId, "邮箱格式不正确，请重新输入。");
        return false;
    } else {
        return Register.phone.isHave(objId);
    }
}

Register.email.getyanzheng = function () {
    $("#imgyanzheng").attr("src", "/Account/CreateLogOnCode?id=" + new Date());
    return false;
}

Register.email.subEmail = function () {

    if ( //Register.email.check()&&
         Register.passw.checkPassw('EmailFPass')
        &&Register.passw.checkPasswSame('EmailFPass', 'EmailConPass')
        &&Register.code.check('i_picCode')
        && Register.agree.check('agreeok'))

    {
        var name = $("#i_Email").val();
        var i_Password = $("#EmailFPass").val();
        var Password = $("#EmailConPass").val();
        var iPhoneCode = $("#i_picCode").val();
        $.ajax({
            url: "Handler/LogonManage.ashx?AType=UserRegister",
            type: "POST",
            cache: false,
            dataType: "json",
            async: false,
            data: { "Name": name, "Password": i_Password, "ConfirmPassword": Password, "Li_Phone": name, "Code": iPhoneCode, "hidType": 0 },
            success: function (ret) {
                if (!ret.Result) {
                    //Register.show(ret.obj, ret.mess);
                    alert(ret.TxtMessage);

                } else {
                    if ($("#hidurl").val() != undefined && $("#hidurl").val() != "") {
                        window.location.href = $("#hidurl").val();
                    } else {
                        window.location.href = "UserMessage.aspx"
                    }
                }
            }
        })
    }
}


$(function () {
    Register.email.getyanzheng();
    //$("#Li_Phone").bind("blur", function () { Register.phone.check() });
    //$("#Li_Phone").bind("focus", function () { Register.clear(this.id) });

    $("#i_Password").bind("blur", function () { Register.passw.checkPassw(this.id) });
    $("#i_Password").bind("focus", function () { Register.clear(this.id) });

    $("#i_ConfirmPassword").bind("blur", function () { Register.passw.checkPasswSame('i_Password', this.id) });
    $("#i_ConfirmPassword").bind("focus", function () { Register.clear(this.id) });

    //$("#i_PhoneCode").bind("blur", function () { Register.code.check(this.id) });
    //$("#i_PhoneCode").bind("focus", function () { Register.clear(this.id) });

    ////发送手机验证码
    //$("#i_btnCode").bind("click", function () { Register.phone.sendPhoneCode() });

    //$("#btn_Phone").bind("click", function () { Register.phone.subPhone() });




    //$("#i_Email").bind("blur", function () { Register.email.check() });
    $("#i_Email").bind("focus", function () { Register.clear(this.id) });

    $("#EmailFPass").bind("blur", function () { Register.passw.checkPassw(this.id) });
    $("#EmailFPass").bind("focus", function () { Register.clear(this.id) });

    $("#EmailConPass").bind("blur", function () { Register.passw.checkPasswSame('EmailFPass', this.id) });
    $("#EmailConPass").bind("focus", function () { Register.clear(this.id) });

    $("#i_picCode").bind("blur", function () { Register.code.check(this.id) });
    $("#i_picCode").bind("focus", function () { Register.clear(this.id) });


    $("#linka").bind("click", function () { Register.email.getyanzheng() });

    $("#btn_Email").bind("click", function () { Register.email.subEmail() });
});