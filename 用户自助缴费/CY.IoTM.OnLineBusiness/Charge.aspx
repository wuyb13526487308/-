<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master" CodeBehind="Charge.aspx.cs" Inherits="CY.IoTM.OnLineBusiness.Charge" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

   
 <style type="text/css">

   .gred { color: red; }

  .inline_block_fix { font-size: 12px; line-height: normal; letter-spacing: normal; display: inline-block; vertical-align: top; }

  .amount li { padding: 8px 0px; }
  .amount li span { vertical-align: middle; color: rgb(87, 87, 87); }
  .amount li span em.gred { display: inline-block; width: 10px; text-align: center; }
  #qitamoney { display: none; }
  .amount li ul.money_list { vertical-align: middle; }
  .amount li ul.money_list li { padding: 0px; margin-right: 5px; }
  .amount li ul.money_list li a { display: block; height: 30px; width: 65px; border: 2px solid rgb(119, 195, 187); line-height: 30px; text-align: center; font-size: 16px; color: rgb(119, 195, 187); font-family: "微软雅黑"; font-weight: bold; }
  .amount li ul.money_list li.selected a { border-color: rgb(255, 130, 50); background: url('money_selectbg.gif') no-repeat scroll right bottom transparent; }
  .amount li ul.money_list li.qitamoney a { width: 100px; }

</style>


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">


     <div class="panel-heading">充值缴费</div>

     <div class="panel-body">

          
         <form class="form-horizontal">
      
            <div class="form-group">

                <label for="GasUserBox" class="control-label col-md-2">燃气账户:</label>
                <div class="row col-md-5">
                  
                    <input id="GasUserBox"  class="form-control"  type="text" />
                </div>
             
            </div>

            <div class="form-group">

                 <label for="consNo" class="control-label col-md-2">户号:</label>
                <div class="row col-md-5">
                  
                    <input id="consNo"  class="form-control"  type="text" />
                </div>
               
            </div>

              <div class="form-group">

                 <label for="consName" class="control-label col-md-2">户名:</label>
                <div class="row col-md-5">
                  
                    <input id="consName"  class="form-control"  type="text" />
                </div>
               
            </div>

             <div class="form-group">

                 <label for="elecAddr" class="control-label col-md-2">地址:</label>
                <div class="row col-md-7">
                  
                    <input id="elecAddr"  class="form-control"  type="text" />
                </div>
               
            </div>

            <div class="form-group">

                 <label for="acctBal" class="control-label col-md-2">账户余额:</label>
                <div class="row col-md-5">
                  
                    <input id="acctBal"  class="form-control"  type="text" />
                </div>
               
            </div>


          <div class="form-group">

              <label for="paymoney" class="control-label col-md-2">充值金额:</label>
               <div class="row col-md-3">
              <select class="form-control" id="paymoney">
                   <option value="100">￥100</option>
                   <option value="150">￥150</option>
                   <option value="200">￥200</option>
                   <option value="300">￥300</option>
                   <option value="qitamoney">其他金额</option>
             </select>
            </div>

           </div>


           <div class="form-group" style="display:none">

                <label for="money" class="control-label col-md-2">其他金额:</label>
                <div class="row col-md-5">
                  
                    <input id="money"  class="form-control"  type="text" />
                </div>
               
            </div>


           <div class="form-group">

                 <label for="acctBal" class="control-label col-md-2">账户余额:</label>
                <div class="row col-md-5">
                  
                    <input id="acctBal"  class="form-control"  type="text" />
                </div>
               
            </div>

           





         
             
           </form>



          

     </div>


    

<%--    <div style="padding-left:20px">
   
 

    <div id="tb" style="margin-top:10px;font-size:14px">
       
        <h3 class="f18 g7 fm l">充值缴费</h3>
        <br />   <br />
        &nbsp;&nbsp;选择燃气账户： <input id="GasUserBox"   style="height:32px;width:220px" />
    </div>

       
    <div class="clear"></div>

          <div class="jiaofei_form mt20">

 
           <div class="core">
                <ul class="basic_info">
                    <li><span><em class="gred">*</em>户号：</span><input id="consNo" name="consNo"  readonly="readonly" type="text"/>
                        
                    </li>
                    <li><span><em class="gred">*</em>户名：</span><input id="consName" readonly="readonly" type="text"/>
                    </li>
                    <li><span><em class="gred">*</em>地址：</span><input id="elecAddr" readonly="readonly" type="text"/>
                    </li>
                </ul>
            </div>

        <!--充值金额-->
    <div class="core">
      <ul class="amount">
        <li><span><em class="gred"></em>账户余额：</span>
            <input id="acctBal" readonly="readonly" type="text"/>
        </li>
     
    
        <li><span><em class="gred"></em>充值金额：</span>
            <ul class="inline_block_fix money_list">
                <li class="inline_block_fix" val="100"><a href="javascript:void(0)"><b class="f10">￥</b>100</a></li>
                <li class="inline_block_fix" val="150"><a href="javascript:void(0)"><b class="f10">￥</b>150</a></li>
                <li class="inline_block_fix" val="200"><a href="javascript:void(0)"><b class="f10">￥</b>200</a></li>
                <li class="inline_block_fix" val="300"><a href="javascript:void(0)"><b class="f10">￥</b>300</a></li>
                <li class="inline_block_fix qitamoney" val=""><a href="javascript:void(0)"><b class="f10"></b>其他金额</a></li>
            </ul>
        </li>
        <li style="display: none;" id="qitamoney">
            <span><em class="gred"></em>其他金额：</span>
            <input id="money" name="money" value="" onblur="checkPayment();" type="text"/>
            <span id="moneyMsg" class="tips pl20 gb"> </span>
        </li>
    </ul>

      <input id="paymoney" value="" type="hidden"/>
      <input id="companyId" value="" type="hidden"/>
</div>


        <!--选择支付账户-->
<div class="core">
    <div class="bank">
        <span class="inline_block_fix"><em class="gred"></em>支付方式：</span>
        
        <div class="inline_block_fix" style="margin-top:10px">

            <table>
                <tr>
                    <td><input value="0011" type="radio" style="width:20px;height:30px" /></td>
                    <td><img src="images/alipay.png" style="width:90px;height:30px;" /></td>
                </tr>

            </table>
       
       </div>
                
    </div>
</div>



   <div class="core">
    <div class="yzm">
        <span class="inline_block_fix w70"><em class="gred"></em>验证码：</span>
        
         <input value="" id="CheckCode" class="inputTxt" type="text" style="width:80px"/>
         <img src="CheckCode.aspx" id="imgValidateCode" style="cursor: pointer;"  align="center"/>
         <a id="linka" href="javascript:void()">换一个</a>
       
    </div>

    <div class="next_btn inline_block" id="chargeBankNoSubmit">
        <span class="inline_block_fix w70">&nbsp;</span>
        <a href="javascript:void;" onclick="App.Charge()" class="inline_block_fix next_abtn">下一步</a>
    </div>
     <div class="next_btn inline_block" id="chargeCardSubmit" style="display:none">
        <span class="inline_block_fix w70">&nbsp;</span>
        <a href="javascript:payByPrepaidCard();" class="inline_block_fix next_abtn">下一步</a>
    </div>
</div>





    </div>


    </div>--%>
   

  <script type="text/javascript">

      var App = {};



      App.check = function (objID) {

          var cName = $("#" + objID).val();
          if (cName == undefined || cName == null || $.trim(cName) == "") {
              $.messager.alert('提示', "请输入验证码", 'info');
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
                          result = true;
                      } else {
                          $.messager.alert('警告', "验证码错误，请重新输入。", 'warn');
                          result = false;
                      }
                  }
              })
              return result;
          }
      }

    

      App.Charge = function () {


          if (!App.check("CheckCode")) {
              return;
          }

          if ($("#paymoney").val() == "") {
              $.messager.alert("金额不能为空");
              return;
          }
          var data = { companyId: $("#companyId").val(), userId: $("#consNo").val(), money: $("#paymoney").val(), account: Olb.loginCompanyOperator.Account };


          location.href = "alipay/requset_url.aspx?companyId=" + data.companyId + "&userId=" + data.userId + "&money=" + data.money + "&account=" + data.account;

          //$.ajax({
          //    url: "../Handler/GasUserManage.ashx?AType=Charge",
          //    type: "POST",
          //    cache: false,
          //    dataType: "json",
          //    async: false,
          //    data: data,
          //    success: function (ret) {
          //        if (ret.Result) {
          //            $.messager.alert('提示', '充值成功！', 'info');
          //        } else {
          //        }
          //    }
          //});
      }



      $(function () {
        
          Olb.LoadGasUserComboBox("GasUserBox", false, false);

          $("#GasUserBox").combobox({
              onSelect: function (data) {

                  $("#consNo").val(data.UserID);
                  $("#consName").val(data.UserName);
                  $("#elecAddr").val(data.Address);
                  $("#acctBal").val(data.Balance);
                  $("#companyId").val(data.CompanyID);
              }
          });


          var selectData = $("#GasUserBox").combobox("getData");

          var tempUserId = GetQueryString("UserID");
        
          if (tempUserId) {
              selectData = $.grep(selectData, function (n, i) {
                  if (n.UserID == tempUserId) {
                     return n;
                  }
              });
          }
       

          $("#consNo").val(selectData[0].UserID);
          $("#consName").val(selectData[0].UserName);
          $("#elecAddr").val(selectData[0].Address);
          $("#acctBal").val(selectData[0].Balance);
          $("#companyId").val(selectData[0].CompanyID);
          $("#GasUserBox").combobox("setValue", selectData[0].UserID);




          $("#linka").on("click", function () {
              document.getElementById('imgValidateCode').src = 'CheckCode.aspx?' + Math.random();
          });


          $("ul > .inline_block_fix").on("click", function () {

              $("ul > .inline_block_fix").removeClass("selected");
              $(this).addClass("selected");

              $("#paymoney").val($(this).attr("val"));

              if ($(this).hasClass("qitamoney")) {
                  $("#qitamoney").show();
              } else {
                  $("#qitamoney").hide();
              }

          });


      });

      function GetQueryString(name) {
          var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
          var r = window.location.search.substr(1).match(reg);
          if (r != null) return unescape(r[2]); return null;
      }


      function trim(str) { //删除左右两端的空格
          return str.replace(/(^\s*)|(\s*$)/g, "");
      }
    
      function checkPayment() {

          var MIN_MONEY = 10;//最小缴费金额
          var MAX_MONEY = 1000;//最大缴费金额
          $("#moneyMsg").hide();
          var res = false;
          var money = trim($("#money").val());// 输入金额框对象 去掉左右空格
          var moneyNum = new Number(money);//转换为数字类型
       
          var MIN_MONEY_NUM = new Number(MIN_MONEY);//转换为数字类型
          var MAX_MONEY_NUM = new Number(MAX_MONEY);//转换为数字类型
          if (money == "" || money == null || money.length == 0) {
              $("#moneyMsg").show();
              $("#money").addClass("inputError");
              $("#moneyMsg").removeClass("onWarn");
              $("#moneyMsg").addClass("onError");
              $("#moneyMsg").html("金额不能为空");
          } else if (money <= 0) {
              $("#moneyMsg").show();
              $("#money").addClass("inputError");
              $("#moneyMsg").removeClass("onWarn");
              $("#moneyMsg").addClass("onError");
              $("#moneyMsg").html("所输金额需大于0元！");
          } else if (!/^-?(?:\d+|\d{1,3}(?:,\d{3})+)(?:\.\d+)?$/.test(money)) {
              $("#moneyMsg").show();
              $("#money").addClass("inputError");
              $("#moneyMsg").removeClass("onWarn");
              $("#moneyMsg").addClass("onError");
              $("#moneyMsg").html("金额必须为数字");
          } else if (moneyNum <= MIN_MONEY_NUM) {
              $("#moneyMsg").show();
              $("#money").addClass("inputError");
              $("#moneyMsg").removeClass("onWarn");
              $("#moneyMsg").addClass("onError");
              $("#moneyMsg").html("金额必须大于" + MIN_MONEY_NUM);
          } else if (moneyNum > MAX_MONEY_NUM) {
              $("#moneyMsg").show();
              $("#money").addClass("inputError");
              $("#moneyMsg").removeClass("onWarn");
              $("#moneyMsg").addClass("onError");
              $("#moneyMsg").html("金额必须小于" + MAX_MONEY_NUM);
          } else if (/^0+\d+/.test(money)) {//缴费时，当不带小数位时，不能以0开始
              $("#moneyMsg").show();
              $("#money").addClass("inputError");
              $("#moneyMsg").removeClass("onWarn");
              $("#moneyMsg").addClass("onError");
              $("#moneyMsg").html("所输金额不能以0开始");
          } else if (/\.\d{3}/.test(money)) {//缴费时，带小数位时不能超过两位
              $("#moneyMsg").show();
              $("#money").addClass("inputError");
              $("#moneyMsg").removeClass("onWarn");
              $("#moneyMsg").addClass("onError");
              $("#moneyMsg").html("所输金额小数位不能多于两位");
          }  else {
              res = true;
          }

          $("#paymoney").val(money);
          return res;
      }




  </script>
    


</asp:Content>
