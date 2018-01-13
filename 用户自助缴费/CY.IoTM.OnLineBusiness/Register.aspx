<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="CY.IoTM.OnLineBusiness.Register" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head>
<meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/>


<title>注册</title>

<link href="script/css/New.css" type="text/css" rel="stylesheet"/>
<script src="script/easyui1.3.3/jquery.min.js" type="text/javascript"></script>


</head>
<body>


<div class="wrapper zhuce">
  <div>
    <div><span style="font-size:25px; margin:15px 10px; font-family:微软雅黑;"> 创源网上营业厅 | 新用户注册</span></div>
    <div class="clear"></div>
  </div>
<div class="user">
    <h1> </h1>

    <div id="con_personal_2" style="display: block;">
        <div class="ulwrap">
            <ul class="ul1">
                <li><span><font>*</font>用户名：</span>
                    <div class="writ">
                        <input class="phone" value="" id="i_Email" name="i_Email" placeholder="自定义用户名、手机号或邮箱" type="text"/>
                     </div>
                      
                </li>
                <li><span><font>*</font>密码：</span>
                    <div>
                        <div class="writ">
                            <input class="mima" id="EmailFPass" value="" maxlength="20" placeholder="6-20位字母、数字和符号" type="password"/>
                         </div>
                        <div class="clear">
                        </div>
                    </div>
                </li>
                <li><span><font>*</font>确认密码：</span>
                    <div class="writ">
                        <input class="mima" id="EmailConPass" value="" maxlength="20" placeholder="6-20位字母、数字和符号" type="password"/>
                      </div>
                </li>
                <li class="zcyzm"><span><font>*</font>验证码：</span>
                    <div class="writ2">
                        <input class="yzhg" name="Code" id="i_picCode" value="" maxlength="6" type="text"/>
                    </div>
                    <div class="wrm_txt">
                        <img src="CheckCode.aspx" id="imgValidateCode" style="margin: 5px 20px 0px 5px;cursor: pointer;"  align="left"/>
                        <a id="linka" href="javascript:void()">换一个</a>

                    </div>
                </li>
                  <li>
                    <div class="cofirm_ifo" style="float: left;">
                        <input id="agreeok" type="checkbox">
                         <p>
                           <a class="f_left">我已阅读并同意 </a><a class="f_right" href="#" target="_top">《创源网上营业厅服务协议》</a></p>
                        <p class="clear">
                        </p>
                    </div>
                </li>
                <li><button class="btn_register" id="btn_Email">同意协议并注册</button></li>
              
            </ul>

            <ul class="ul2">
                <li>
                    <a href="Logon.aspx">
                        <input id="hidurl" type="hidden" value="" name="hidurl"/>
                        <img src="images/payimg.jpg"/>
                    </a>
                </li>
            </ul>
         
            <div class="clear">
            </div>
        </div>
    </div>
</div>
 
 </div>
    <script type="text/javascript"  src="script/Common.js"></script>
    <script type="text/javascript" src="script/Register.js"></script> 
    <script type="text/javascript">
        $(function () {
            $("#linka").on("click", function () {
                document.getElementById('imgValidateCode').src = 'CheckCode.aspx?' + Math.random();
            });

        });
    </script>

</body>
</html>
