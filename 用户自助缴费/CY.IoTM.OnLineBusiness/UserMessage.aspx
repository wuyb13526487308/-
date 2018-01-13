<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPage.Master"  CodeBehind="UserMessage.aspx.cs" Inherits="CY.IoTM.OnLineBusiness.UserMessage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">


</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlace" Runat="Server">


     <div class="panel-heading">个人资料</div>

     <div class="panel-body">



           <form>
            <div class="form-group">
                <label for="userAccount"><font color="#d90000">*</font>用户名</label>
                <input type="text" class="form-control" id="userAccount"  disabled>
            </div>
            <div class="form-group">
                <label for="Realname">真实姓名</label>
                <input type="text" class="form-control" id="Realname" >
            </div>

            <div class="form-group">
                <label for="UserCodenumber">身份证号</label>
                <input type="text" class="form-control" id="UserCodenumber" >
            </div>

            <div class="form-group">
                <label for="address">家庭住址</label>
                <textarea  class="form-control" id="address"  rows="3"></textarea>
            </div>

           <div>
                <button type="button" class="btn btn-primary"  onclick="EditUserMessage()" >保存修改</button>
            </div>

             

        </form>



     </div>



   


    <script type="text/javascript">

        $(function () {

            $("#userAccount").val(Olb.loginCompanyOperator.Account);
            $("#Realname").val(Olb.loginCompanyOperator.Name);
            $("#UserCodenumber").val(Olb.loginCompanyOperator.IdentityCard);
            $("#address").val(Olb.loginCompanyOperator.Address);
        })



        var EditUserMessage = function () {

            var data = { Name: $("#Realname").val(), IdentityCard: $("#UserCodenumber").val(), Address: $("#address").val() };

            $.ajax({
                url: "Handler/UserManage.ashx?AType=UserEdit",
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