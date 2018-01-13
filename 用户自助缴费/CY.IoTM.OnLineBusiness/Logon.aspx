<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Logon.aspx.cs" Inherits="CY.IoTM.OnLineBusiness.Logon" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>创源网上营业厅-登录</title>
<link type="text/css" rel="stylesheet" href="script/css/New.css" />
<script src="script/easyui1.3.3/jquery.min.js" type="text/javascript"></script>
<script src="script/Common.js" type="text/javascript"></script>

</head>
<body >



<div class="wrapper zhuce">
 <script type="text/javascript">
   

    //点击提交按钮时的验证
     function LoginOn() {

        $("#td_Name").hide();
        $("#td_Error1").hide();
        $("#td_Password").hide();
        $("#td_Error2").hide();
        $("#label1").hide();
        $("#label2").hide();
        $("#label3").hide();
        if ($.trim($("#i_Name").val()) == '') {
            $("#td_Name").html('请输入用户名！');
            $("#td_Name").show();
            return;
        }
        if ($.trim($("#i_Name").val()).length < 2) {
            $("#td_Name").html('用户名长度最少2位');
            $("#td_Name").show();
            return;
        }
        if (/!|#|\$|%|\^|&|\*|\(|\)|\/|\\|`|\~|\?|,|<|>|\;|'|:|\"|\{|\}|\[|\]|\+|\=|\_|！|￥|………|（|）|——|—|【|】|：|“|；|‘|，|。|？|《|》|、|·/.test($("#i_Name").val())) {
            $("#td_Name").html('用户名不能包括特殊字符');
            $("#td_Name").show();
            return;
        }
        //if ($.trim($("#i_Password").val()) == '') {
        //    $("#td_Password").html('请输入密码！');
        //    $("#td_Password").show();
        //    return;
        //}
        //if (!(/^[\w!@#\$%\^&\*\(\)]+$/.test($("#i_Password").val()))) {
        //    $("#td_Password").html('特殊字符只能包括：!@#$%^&*()');
        //    $("#td_Password").show();
        //    return;
        //}


        

        $.ajax({
            url: "Handler/LogonManage.ashx?AType=UserLogin",
            type: "POST",
            cache: false,
            dataType: "json",
            async: false,
            data: $('#form33').serialize(),
            success: function (ret) {
                if (ret.Result != true) {
                    $("#td_Password").html(ret.TxtMessage);
                    $("#td_Password").show();

                } else {
                    window.location.href = "Index.aspx"
                }
            }
        })
        
    }

    function clearName() {
        if ($("#i_Name").val() == '手机号/用户名/邮箱') {
            $("#i_Name").val('');
        }
    }
    function addName() {
        if ($("#i_Name").val() == '手机号/用户名/邮箱') {
            $("#i_Name").val('');
        }
        if ($("#i_Name").val()=="") {
            $("#i_Name").val('手机号/用户名/邮箱');
        }
    
    }
</script>
  <div class="img_logo"><a href="#"></a>
      <font style="margin-top:10px;"></font><span><a href="#">创源网上营业厅</a></span><font>|</font><span>欢迎登录</span></div>

    <div class="widow_login">
      <div class="img_left">
        <img src="images/login.jpg" />
    </div>
    <div class="infor_right">


   <form id="form33" method="post">        
       <div class="sr_wrap">
          <input name="Name" id="i_Name" type="text" value="手机号/用户名/邮箱" maxlength="30" onfocus="clearName();"  onblur="addName()"/>
          <label id="td_Name"></label>
        </div>
        <div class="sr_wrap">
           <input name="Password" id="i_Password" type="password" maxlength="20" />
                <label id="td_Password"></label>
        </div>
        <ul>
          <li>
               <input id="i_Remember" name="Remember" align="bottom" type="checkbox" value="1" checked="checked" />
               <span><a href="#">自动登录</a></span>
          </li>

            <li style="text-align:right;"><a href="/PassAccount/GetUserPass">忘记密码</a></li>
        </ul>
         <input type="button" class="btn_log" value="登 录" onclick="LoginOn()" />
         <input id="returnUrl" name="returnUrl" type="hidden" value="" />

   </form>    

    </div>

    <div class="clear"></div>
  </div>
<div class="btnGren"><a href="Register.aspx">免费注册</a></div>
 
 </div>

</body>
</html>
