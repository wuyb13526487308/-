<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage.Master" CodeBehind="AccountSafe.aspx.cs" Inherits="CY.IoTM.OnLineBusiness.AccountSafe" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">


   <div class="panel-heading">修改密码</div>

     <div class="panel-body">



      <form>
            <div class="form-group">
                <label for="t_OldPassword">旧密码</label>
                <input type="password" class="form-control" id="t_OldPassword" >
            </div>
            <div class="form-group">
                <label for="t_NewPassword">新密码</label>
                <input type="password" class="form-control" id="t_NewPassword" >
            </div>

            <div class="form-group">
                <label for="ConfirmPassword">确认密码</label>
                <input type="password" class="form-control" id="ConfirmPassword" >
            </div>


           <div>
                <button type="button" class="btn btn-primary"  onclick="UpdatePwd()" >保存修改</button>
            </div>

             

        </form>

    </div>

   


    <script type="text/javascript">

     

        var UpdatePwd = function () {


            if ($("#t_NewPassword").val() != $("#ConfirmPassword").val()) {
                alert("新密码两次输入不一致"); return;

            }


            var data = { oldPwd: $("#t_OldPassword").val(), newPwd: $("#t_NewPassword").val() };

            $.ajax({
                url: "Handler/UserManage.ashx?AType=UpdatePwd",
                type: "POST",
                cache: false,
                dataType: "json",
                async: false,
                data: data,
                success: function (ret) {
                    if (ret.Result != true) {
                     
                        $.messager.alert('提示', ret.TxtMessage, 'info');
                    } else {
                      
                        $.messager.alert('提示', '修改成功！', 'info');
                    }
                }
            })
        }

    </script>

</asp:Content>