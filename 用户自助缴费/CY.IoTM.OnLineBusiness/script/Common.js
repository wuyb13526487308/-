function checkUser(str) {
    var re = /^[a-zA-z-_0-9]{6,20}$/;
    if (re.test(str)) {
        return true;
    } else {
        return false;
    }
}
//checkUser("jihua_cnblogs"); //调用
//手机验证
function checkMobile(str) {
    // /^((1[3-9][0-9]{1})+\d{8})$/; beiyong
    var re = /^1\d{10}$/
    if (re.test(str)) {
        return true;
    } else {
        return false;
    }
}
//校验全是数字
function checkallnum(str) {
    var patrn = /^\d+$/;
    return patrn.test(str);
}
//校验是否全是中文
function isChinese(str) {
    var patrn = /[\u4E00-\u9FA5\uF900-\uFA2D]+$/;
    return patrn.test(str);
}

//校验字符串：只能输入6-20个字母、数字、下划线(常用手校验用户名和密码)
function isString6_20(str) {
    var patrn = /^(\w){6,20}$/;
    return patrn.test(str);
}

//验证是否全为字母   
function isABC(str) {
    var patrn = /^[A-Za-z]+$/;
    return patrn.test(str);
}

function checkPhone(str) {
    var
 re = /^0\d{2,3}-?\d{7,8}$/;
    if (re.test(str)) {
        return true;
    } else {
        return false;
    }
}
//包含这个页面
function IsConDangerous(source) {
    if ((source.indexOf("^") >= 0) || (source.indexOf("(") >= 0) || (source.indexOf(")") >= 0) || (source.indexOf(")") >= 0) || (source.indexOf("=") >= 0) || (source.indexOf("<") >= 0) || (source.indexOf(">") >= 0)) {
        return true;
    }
    else {
        return false;
    }
}


//checkPhone("09557777777"); //调用
/******检查Email格式*****/
function checkEmail(str) {
    var re = /^(\w-*\.*)+@(\w-?)+(\.\w{2,})+$/
    if (re.test(str)) {
        return true;
    } else {
        return false;
    }
}
/******现实制定文本框和错误提示*****/
function ErrorInfo(objid, mes) {
    clearError();
    $("#" + objid).parent().css("border-color", "#f00");
    $("#" + objid).parent().after("<div class='wrm_txt' name='mesInfo' ><h3><small>" + mes + " &nbsp;&nbsp;</small></h3></div>");
    //if ($("#" + objid).next("img") != undefined && $("#" + objid).next().is("img")) {
    //    $("#" + objid).next().attr("src", "../../Content/imgIV/ico_tb4.jpg");
    //}
    return false;
}
/******清除页面所有的红色文本框和错误提示*****/


function clearError() {
    $("[name='mesInfo']").remove();
    $("div .writ").css("border-color", "#dadada");
    $("img").each(function () {
        if ($(this).attr("src") == "images/ico_tb4.jpg" && $(this).attr("defaultsrc") != undefined && $(this).attr("defaultsrc") != "") {
            $(this).attr("src", $(this).attr("defaultsrc"));
        }
    });
}

///校验密码强度 
//CharMode函数 
//测试某个字符是属于哪一类. 
function CharMode(iN) {
    if (iN >= 48 && iN <= 57) //数字 
        return 1;
    if (iN >= 65 && iN <= 90) //大写字母 
        return 2;
    if (iN >= 97 && iN <= 122) //小写 
        return 4;
    else
        return 8; //特殊字符 
}
//bitTotal函数 
//计算出当前密码当中一共有多少种模式 
function bitTotal(num) {
    modes = 0;
    for (i = 0; i < 4; i++) {
        if (num & 1) modes++;
        num >>>= 1;
    }
    return modes;
}
function checkStrong(sPW) {
    if (sPW.length < 6 || checkallnum(sPW) || sPW.length > 20)
        return 0; //密码太短 
    Modes = 0;
    for (i = 0; i < sPW.length; i++) {
        //测试每一个字符的类别并统计一共有多少种模式. 
        Modes |= CharMode(sPW.charCodeAt(i));
    }
    return bitTotal(Modes);
}
/************传入要校验密码的文本框Id************/
function checkPass(objId) {
    clearError();
    var Pass = $("#" + objId).val();
    var passstr = "<a class='yellow'>弱</a><a class='gray'>中</a><a class='gray'>强</a>";
    if (Pass == undefined || Pass == null || Pass == "") {
        ErrorInfo(objId, "密码为6-20个字符,建议由字母、数字、符号两种以上组合");
        return false;
    }

    if (Pass.length < 6 || Pass.length > 20) {
        ErrorInfo(objId, "密码长度为6-20个字符，请重新输入。");
        return false;
    }
    if (Pass.indexOf(" ") > -1) {
        ErrorInfo(objId, "密码为6-20个字符,建议由字母、数字、符号两种以上组合。");
        return false;
    }
    if (!isNaN(Pass)) {
        ErrorInfo(objId, "密码不能为纯数字，请重新输入。");
        return false;
    }
    var returnstr = "";
    switch (checkStrong(Pass)) {
        case 0:
            ErrorInfo(objId, "密码长度6-20个字符,请重新输入");
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
/*******密码一致性校验*******/
function checkConpass(passId, conPassId) {
    //清除所有的提示框
    var Password = $("#" + conPassId).val();
    var i_Password = $("#" + passId).val();
    if (i_Password == "" || i_Password == undefined || i_Password == null) {
        ErrorInfo(conPassId, "密码为6-20个字符,建议由字母、数字、符号两种以上组合。");
        return false;
    }
    if (Password == "" || Password == undefined || Password == null) {
        ErrorInfo(conPassId, "请重新输入确认密码。");
        return false;
    }
    if (!checkPass(passId)) {
        return false;
    }
    if (Password == i_Password) {
        isok(conPassId);
    } else {
        ErrorInfo(conPassId, "密码不一致，请重新输入。");
        return false;
    }
}
/*******显示通过图片*******/
function isok(objId) {
    $("#" + objId).parent().css("border-color", "#dadada");
    if ($("#" + objId).next("img") != undefined && $("#" + objId).next().is("img")) {
        $("#" + objId).next().attr("src", "images/ico_tb3.jpg");
    } else {
        $("#" + objId).after("<img src='images/ico_tb3.jpg' name='mesInfo' />");
    }
}
function deleteisok(objId) {
    //$("#" + objId).parent().css("border-color", "#dadada");
    if ($("#" + objId).next("img") != undefined && $("#" + objId).next().is("img")) {
        $("#" + objId).next().remove();
    }
}

function iserror(objId) {
    $("#" + objId).parent().css("border-color", "#f00");
}
